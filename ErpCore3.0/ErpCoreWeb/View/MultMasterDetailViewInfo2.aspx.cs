using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.UI;

public partial class View_MultMasterDetailViewInfo2 : System.Web.UI.Page
{
    public CView m_View = null;
    public Guid m_Catalog_id = Guid.Empty;
    bool m_bIsNew = false;
    bool m_bIsLoadingData = false;

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
            if (Session["NewMultMasterDetailView"] == null)
            {
                btPrev_Click(null, null);
                return;
            }
            else
            {
                SortedList<Guid, CView> sortObj = (SortedList<Guid, CView>)Session["NewMultMasterDetailView"];
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
            LoadDetailTable();
            cbDetailTable_SelectedIndexChanged(null, null);
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
    void LoadDetailTable()
    {
        List<CBaseObject> lstObj = m_View.ViewDetailMgr.GetList();
        List<CViewDetail> sortObj = new List<CViewDetail>();
        foreach (CBaseObject obj in lstObj)
        {
            CViewDetail vd = (CViewDetail)obj;
            sortObj.Add(vd);
        }
        sortObj.Sort();
        cbDetailTable.Items.Clear();
        foreach (CViewDetail ViewDetail in sortObj)
        {
            CTable table = (CTable)Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.Find(ViewDetail.FW_Table_id);
            ListItem it = new ListItem(table.Name, table.Id.ToString());
            cbDetailTable.Items.Add(it);
        }
        listDetailColumn.Items.Clear();
    }
    protected void cbDetailTable_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDetailColumn();
    }
    void LoadDetailColumn()
    {
        if (cbDetailTable.SelectedIndex == -1)
            return;
        CTable table = (CTable)Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.Find(new Guid(cbDetailTable.SelectedItem.Value));
        CViewDetail ViewDetail = m_View.ViewDetailMgr.FindByTable(table.Id);
        
        m_bIsLoadingData = true;
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
        m_bIsLoadingData = false;
    }

    protected void btNext_Click(object sender, EventArgs e)
    {
        if (!ValidatePage2())
            return;
        if (!SavePage2())
            return;
        Response.Redirect("MultMasterDetailViewInfo3.aspx?id=" + Request["id"] + "&catalog_id=" + Request["catalog_id"]);
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
        List<CBaseObject> lstObj = m_View.ViewDetailMgr.GetList();
        foreach (CBaseObject obj in lstObj)
        {
            CViewDetail vd = (CViewDetail)obj;
            if (vd.ColumnInViewDetailMgr.GetList().Count == 0)
            {
                CTable table = (CTable)Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.Find(vd.FW_Table_id);
                string strMsg = string.Format("请选择从表 {0} 字段！", table.Name);
                RegisterStartupScript("starup", string.Format("<script>alert('{0}');</script>",strMsg));
                return false;
            }
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

        return true;
    }
    protected void btCancel_Click(object sender, EventArgs e)
    {
        Session["NewMultMasterDetailView"] = null;
        RegisterStartupScript("starup", "<script>parent.$.ligerDialog.close();</script>");
    }
    protected void btPrev_Click(object sender, EventArgs e)
    {
        Response.Redirect("MultMasterDetailViewInfo1.aspx?id=" + Request["id"] + "&catalog_id=" + Request["catalog_id"]);
    }
    protected void listDetailColumn_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (m_bIsLoadingData)
            return;
        if(cbDetailTable.SelectedIndex==-1)
            return;
        string s = Request.Form["__EVENTTARGET"]; 
        int index = Convert.ToInt32(s.Substring(s.LastIndexOf("$") + 1));

        CTable table = (CTable)Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.Find(new Guid(cbDetailTable.SelectedItem.Value));
        CColumn column = (CColumn)table.ColumnMgr.Find(new Guid(listDetailColumn.Items[index].Value));
        CViewDetail ViewDetail = (CViewDetail)m_View.ViewDetailMgr.FindByTable(column.FW_Table_id);

        if (listDetailColumn.Items[index].Selected)
        {
            CColumnInViewDetail civd = (CColumnInViewDetail)ViewDetail.ColumnInViewDetailMgr.FindByColumn(column.Id);
            if (civd == null)
            {
                civd = new CColumnInViewDetail();
                civd.Ctx = Global.GetCtx(Session["TopCompany"].ToString());
                civd.FW_Column_id = column.Id;
                civd.FW_Table_id = column.FW_Table_id;
                civd.Caption = column.Name;
                civd.UI_ViewDetail_id = ViewDetail.Id;
                civd.Idx = ViewDetail.ColumnInViewDetailMgr.NewIdx();
                ViewDetail.ColumnInViewDetailMgr.AddNew(civd);
            }
        }
        else
        {
            CColumnInViewDetail obj = (CColumnInViewDetail)ViewDetail.ColumnInViewDetailMgr.FindByColumn(column.Id);
            if (obj != null)
                ViewDetail.ColumnInViewDetailMgr.Delete(obj);
        }
    }
}
