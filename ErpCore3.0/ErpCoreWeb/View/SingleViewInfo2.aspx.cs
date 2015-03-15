using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.UI;

public partial class View_SingleViewInfo2 : System.Web.UI.Page
{
    public CView m_View = null;
    public Guid m_Catalog_id = Guid.Empty;
    bool m_bIsNew = false;
    public CTable m_Table = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null)
        {
            Response.Redirect("../Login.aspx");
            Response.End();
        }
        string id = Request["id"];
        if (!string.IsNullOrEmpty(id))
        {
            m_View = (CView)Global.GetCtx(Session["TopCompany"].ToString()).ViewMgr.Find(new Guid(id));
        }
        else
        {
            m_bIsNew = true;
            if (Session["NewView"] == null)
            {
                btPrev_Click(null, null);
                return;
            }
            else
            {
                SortedList<Guid, CView> sortObj = (SortedList<Guid, CView>)Session["NewView"];
                m_View = sortObj.Values[0];
            }
        }
        m_Table = (CTable)Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.Find(m_View.FW_Table_id);

        string catalog_id = Request["catalog_id"];
        if (!string.IsNullOrEmpty(catalog_id))
        {
            m_Catalog_id = new Guid(catalog_id);
        }

        if (!IsPostBack)
        {
            LoadData();
        }
    }

    void LoadData()
    {
        txtMasterTable.Text = m_Table.Name;
        listMasterColumn.Items.Clear();
        List<CBaseObject> lstObj = m_Table.ColumnMgr.GetList();
        foreach (CBaseObject obj in lstObj)
        {
            CColumn column = (CColumn)obj;
            ListItem it = new ListItem(column.Name, column.Id.ToString());
            listMasterColumn.Items.Add(it);
            if (m_View.ColumnInViewMgr.FindByColumn(column.Id) != null)
                it.Selected = true;

        }

    }

    protected void btNext_Click(object sender, EventArgs e)
    {
        if (!ValidatePage2())
            return;
        if (!SavePage2())
            return;
        Response.Redirect("SingleViewInfo3.aspx?id=" + Request["id"] + "&catalog_id=" + Request["catalog_id"]);
    }
    bool ValidatePage2()
    {
        bool bHas=false;
        for(int i=0;i<listMasterColumn.Items.Count;i++)
        {
            if(listMasterColumn.Items[i].Selected)
            {
                bHas=true;
                break;
            }
        }
        if (!bHas)
        {
            RegisterStartupScript("starup", "<script>alert('请选择主表字段！');</script>");
            return false;
        }
        return true;
    }
    bool SavePage2()
    {
        for (int i = 0; i < listMasterColumn.Items.Count; i++)
        {
            if (listMasterColumn.Items[i].Selected)
            {
                CColumnInView civ = m_View.ColumnInViewMgr.FindByColumn(new Guid(listMasterColumn.Items[i].Value));
                if (civ == null)
                {
                    civ = new CColumnInView();
                    civ.Ctx = Global.GetCtx(Session["TopCompany"].ToString());
                    civ.UI_View_id = m_View.Id;
                    civ.FW_Table_id = m_Table.Id;
                    civ.FW_Column_id = new Guid(listMasterColumn.Items[i].Value);
                    civ.Caption = listMasterColumn.Items[i].Text;
                    civ.Idx = i;
                    m_View.ColumnInViewMgr.AddNew(civ);
                }
            }
            else
            {
                CColumnInView civ = m_View.ColumnInViewMgr.FindByColumn(new Guid(listMasterColumn.Items[i].Value));
                if (civ != null)
                    m_View.ColumnInViewMgr.Delete(civ);
            }
        }
        return true;
    }
    protected void btCancel_Click(object sender, EventArgs e)
    {
        Session["NewView"] = null;
        RegisterStartupScript("starup", "<script>parent.$.ligerDialog.close();</script>");
    }
    protected void btPrev_Click(object sender, EventArgs e)
    {
        Response.Redirect("SingleViewInfo1.aspx?id=" + Request["id"] + "&catalog_id=" + Request["catalog_id"]);
    }
}
