using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.UI;

public partial class View_MasterDetailViewInfo2 : System.Web.UI.Page
{
    public CView m_View = null;
    public Guid m_Catalog_id = Guid.Empty;
    bool m_bIsNew = false;

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
            if (Session["NewMasterDetailView"] == null)
            {
                btPrev_Click(null, null);
                return;
            }
            else
            {
                SortedList<Guid, CView> sortObj = (SortedList<Guid, CView>)Session["NewMasterDetailView"];
                m_View = sortObj.Values[0];
            }
        }

        string catalog_id = Request["catalog_id"];
        if (!string.IsNullOrEmpty(catalog_id))
        {
            m_Catalog_id = new Guid(catalog_id);
        }

        if (!IsPostBack)
        {
            LoadMasterColumn();
            LoadDetailColumn();
        }
    }

    void LoadMasterColumn()
    {
        CTable table = (CTable)Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.Find(m_View.FW_Table_id);
        txtMasterTable.Text = table.Name;
        listMasterColumn.Items.Clear();
        List<CBaseObject> lstObj = table.ColumnMgr.GetList();
        foreach (CBaseObject obj in lstObj)
        {
            CColumn column = (CColumn)obj;
            ListItem it = new ListItem(column.Name, column.Id.ToString());
            listMasterColumn.Items.Add(it);
            if (m_View.ColumnInViewMgr.FindByColumn(column.Id) != null)
                it.Selected = true;

        }

    }
    void LoadDetailColumn()
    {
        CViewDetail ViewDetail = (CViewDetail)m_View.ViewDetailMgr.GetFirstObj();
        if (ViewDetail == null)
            return;
        CTable table = (CTable)Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.Find(ViewDetail.FW_Table_id);
        txtDetailTable.Text = table.Name;

        listDetailColumn.Items.Clear();
        List<CBaseObject> lstObj = table.ColumnMgr.GetList();
        foreach (CBaseObject obj in lstObj)
        {
            CColumn column = (CColumn)obj;
            ListItem it = new ListItem(column.Name, column.Id.ToString());
            listDetailColumn.Items.Add(it);
            if (ViewDetail.ColumnInViewDetailMgr.FindByColumn(column.Id) != null)
                it.Selected = true;
        }
    }

    protected void btNext_Click(object sender, EventArgs e)
    {
        if (!ValidatePage2())
            return;
        if (!SavePage2())
            return;
        Response.Redirect("MasterDetailViewInfo3.aspx?id=" + Request["id"] + "&catalog_id=" + Request["catalog_id"]);
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
        bHas = false;
        for (int i = 0; i < listDetailColumn.Items.Count; i++)
        {
            if (listDetailColumn.Items[i].Selected)
            {
                bHas = true;
                break;
            }
        }
        if (!bHas)
        {
            RegisterStartupScript("starup", "<script>alert('请选择从表字段！');</script>");
            return false;
        }
        return true;
    }
    bool SavePage2()
    {
        //主表
        CTable tableM = (CTable)Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.Find(m_View.FW_Table_id);
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
                    civ.FW_Table_id = tableM.Id;
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
        //从表
        CViewDetail ViewDetail = (CViewDetail)m_View.ViewDetailMgr.GetFirstObj();
        for (int i = 0; i < listDetailColumn.Items.Count; i++)
        {
            if (listDetailColumn.Items[i].Selected)
            {
                CColumnInViewDetail civd = ViewDetail.ColumnInViewDetailMgr.FindByColumn(new Guid(listDetailColumn.Items[i].Value));
                if (civd == null)
                {
                    civd = new CColumnInViewDetail();
                    civd.Ctx = Global.GetCtx(Session["TopCompany"].ToString());
                    civd.UI_ViewDetail_id = ViewDetail.Id;
                    civd.FW_Table_id = ViewDetail.FW_Table_id;
                    civd.FW_Column_id = new Guid(listDetailColumn.Items[i].Value);
                    civd.Caption = listDetailColumn.Items[i].Text;
                    civd.Idx = i;
                    ViewDetail.ColumnInViewDetailMgr.AddNew(civd);
                }
            }
            else
            {
                CColumnInViewDetail civd = ViewDetail.ColumnInViewDetailMgr.FindByColumn(new Guid(listDetailColumn.Items[i].Value));
                if (civd != null)
                    ViewDetail.ColumnInViewDetailMgr.Delete(civd);
            }
        }
        return true;
    }
    protected void btCancel_Click(object sender, EventArgs e)
    {
        Session["NewMasterDetailView"] = null;
        RegisterStartupScript("starup", "<script>parent.$.ligerDialog.close();</script>");
    }
    protected void btPrev_Click(object sender, EventArgs e)
    {
        Response.Redirect("MasterDetailViewInfo1.aspx?id=" + Request["id"] + "&catalog_id=" + Request["catalog_id"]);
    }
}
