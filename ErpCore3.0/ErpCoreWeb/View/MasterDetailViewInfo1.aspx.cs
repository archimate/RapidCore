using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.UI;

public partial class View_MasterDetailViewInfo1 : System.Web.UI.Page
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
                m_View = new CView();
                m_View.Ctx = Global.GetCtx(Session["TopCompany"].ToString());
                m_View.VType = enumViewType.MasterDetail;
                CViewDetail ViewDetail = new CViewDetail();
                ViewDetail.Ctx = m_View.Ctx;
                ViewDetail.UI_View_id = m_View.Id;
                m_View.ViewDetailMgr.AddNew(ViewDetail);

                SortedList<Guid, CView> sortObj = new SortedList<Guid, CView>();
                sortObj.Add(m_View.Id, m_View);
                Session["NewMasterDetailView"] = sortObj;
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
            LoadCatalog();
            LoadTable();
            LoadData();
        }
    }

    void LoadCatalog()
    {
        cbCatalog.Items.Clear();
        cbCatalog.Items.Add("");
        int iDefaultIdx = 0;
        List<CBaseObject> lstObj = Global.GetCtx(Session["TopCompany"].ToString()).ViewCatalogMgr.GetList();
        foreach (CBaseObject obj in lstObj)
        {
            CViewCatalog catalog = (CViewCatalog)obj;
            ListItem item = new ListItem(catalog.Name, catalog.Id.ToString());
            cbCatalog.Items.Add(item);
            if (catalog.Id == m_Catalog_id)
                iDefaultIdx = cbCatalog.Items.Count-1;
        }
        cbCatalog.SelectedIndex = iDefaultIdx;
    }
    void LoadTable()
    {
        cbMasterTable.Items.Clear();
        cbDetailTable.Items.Clear();
        List<CBaseObject> lstObj = Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.GetList();
        foreach (CBaseObject obj in lstObj)
        {
            CTable table = (CTable)obj;
            ListItem item = new ListItem(table.Name, table.Id.ToString());
            cbMasterTable.Items.Add(item);
            ListItem item2 = new ListItem(table.Name, table.Id.ToString());
            cbDetailTable.Items.Add(item2);
        }
    }
    void LoadData()
    {
        txtName.Text = m_View.Name;
        //默认表
        for (int i = 0; i < cbMasterTable.Items.Count; i++)
        {
            if (cbMasterTable.Items[i].Value == m_View.FW_Table_id.ToString())
            {
                cbMasterTable.SelectedIndex = i;
                cbMasterTable_SelectedIndexChanged(null, null);
                break;
            }
        }

        CViewDetail ViewDetail = (CViewDetail)m_View.ViewDetailMgr.GetFirstObj();
        if (ViewDetail != null)
        {
            for (int i = 0; i < cbDetailTable.Items.Count; i++)
            {
                if (cbDetailTable.Items[i].Value == ViewDetail.FW_Table_id.ToString())
                {
                    cbDetailTable.SelectedIndex = i;
                    cbDetailTable_SelectedIndexChanged(null, null);
                    break;
                }
            }
            for (int i = 0; i < cbPrimaryKey.Items.Count; i++)
            {
                if (cbPrimaryKey.Items[i].Value == ViewDetail.PrimaryKey.ToString())
                {
                    cbPrimaryKey.SelectedIndex = i;
                    break;
                }
            }
            for (int i = 0; i < cbForeignKey.Items.Count; i++)
            {
                if (cbForeignKey.Items[i].Value == ViewDetail.ForeignKey.ToString())
                {
                    cbForeignKey.SelectedIndex = i;
                    break;
                }
            }
        }

    }

    protected void btNext_Click(object sender, EventArgs e)
    {
        if (!ValidatePage1())
            return;
        if (!SavePage1())
            return;
        Response.Redirect("MasterDetailViewInfo2.aspx?id=" + Request["id"] + "&catalog_id=" + m_Catalog_id.ToString());
    }
    bool ValidatePage1()
    {
        if (txtName.Text.Trim() == "")
        {
            RegisterStartupScript("starup", "<script>alert('名称不能空！');</script>");
            return false;
        }
        if (cbMasterTable.SelectedIndex == -1)
        {
            RegisterStartupScript("starup", "<script>alert('请选择主表！');</script>");
            return false;
        }
        string sTbId = cbMasterTable.SelectedItem.Value;
        CTable tbM = (CTable)Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.Find(new Guid(sTbId));
        if (cbDetailTable.SelectedIndex == -1)
        {
            RegisterStartupScript("starup", "<script>alert('请选择从表！');</script>");
            return false;
        }
        sTbId = cbDetailTable.SelectedItem.Value;
        CTable tbD = (CTable)Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.Find(new Guid(sTbId));
        if (tbM.Id == tbD.Id)
        {
            RegisterStartupScript("starup", "<script>alert('主表不能与从表相同！');</script>");
            return false;
        }
        if (cbPrimaryKey.SelectedIndex == -1)
        {
            RegisterStartupScript("starup", "<script>alert('请设置外键关联！');</script>");
            return false;
        }
        if (cbForeignKey.SelectedIndex == -1)
        {
            RegisterStartupScript("starup", "<script>alert('请设置外键关联！');</script>");
            return false;
        }
        
        return true;
    }

    bool SavePage1()
    {
        string sName = txtName.Text.Trim();
        if (!sName.Equals(m_View.Name))
        {
            CView view2 = Global.GetCtx(Session["TopCompany"].ToString()).ViewMgr.FindByName(sName);
            if (view2 != null)
            {
                RegisterStartupScript("starup", "<script>alert('名称重复！');</script>");
                txtName.Focus();
                return false;
            }
        }
        m_View.Name = sName;
        if (cbCatalog.SelectedIndex > 0)
        {
            string catalog_id = cbCatalog.SelectedItem.Value;
            CViewCatalog catalog = (CViewCatalog)Global.GetCtx(Session["TopCompany"].ToString()).ViewCatalogMgr.Find(new Guid(catalog_id));
            m_View.UI_ViewCatalog_id = catalog.Id;
        }
        else
            m_View.UI_ViewCatalog_id = Guid.Empty;
        //如果是修改，并修改了主表，则清空旧字段
        Guid guidMT = new Guid(cbMasterTable.SelectedItem.Value);
        if (!m_bIsNew && guidMT != m_View.FW_Table_id)
        {
            m_View.ColumnInViewMgr.RemoveAll();
        }
        m_View.FW_Table_id = guidMT;
        CViewDetail ViewDetail = (CViewDetail)m_View.ViewDetailMgr.GetFirstObj();
        //如果是修改，并修改了从表，则清空旧字段
        Guid guidDT = new Guid(cbDetailTable.SelectedItem.Value);
        if (!m_bIsNew && guidDT != ViewDetail.FW_Table_id)
        {
            ViewDetail.ColumnInViewDetailMgr.RemoveAll();
        }
        ViewDetail.FW_Table_id = guidDT;
        ViewDetail.PrimaryKey = new Guid(cbPrimaryKey.SelectedItem.Value);
        ViewDetail.ForeignKey = new Guid(cbForeignKey.SelectedItem.Value);

        m_View.ViewDetailMgr.Update(ViewDetail);

        return true;
    }
    protected void btCancel_Click(object sender, EventArgs e)
    {
        Session["NewMasterDetailView"] = null;
        RegisterStartupScript("starup", "<script>parent.$.ligerDialog.close();</script>");
    }
    protected void cbMasterTable_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadPrimaryKey();
    }
    void LoadPrimaryKey()
    {
        if (cbMasterTable.SelectedIndex == -1)
            return;
        string sTbId = cbMasterTable.SelectedItem.Value;
        CTable table = (CTable)Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.Find(new Guid(sTbId));
        cbPrimaryKey.Items.Clear();
        List<CBaseObject> lstObj = table.ColumnMgr.GetList();
        foreach (CBaseObject obj in lstObj)
        {
            CColumn column = (CColumn)obj;
            ListItem it = new ListItem(column.Name, column.Id.ToString());
            cbPrimaryKey.Items.Add(it);
        }
    }
    protected void cbDetailTable_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadForeignKey();
    }
    void LoadForeignKey()
    {
        if (cbDetailTable.SelectedIndex == -1)
            return;
        string sTbId = cbDetailTable.SelectedItem.Value;
        CTable table = (CTable)Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.Find(new Guid(sTbId));
        cbForeignKey.Items.Clear();
        List<CBaseObject> lstObj = table.ColumnMgr.GetList();
        foreach (CBaseObject obj in lstObj)
        {
            CColumn column = (CColumn)obj;
            ListItem it = new ListItem(column.Name, column.Id.ToString());
            cbForeignKey.Items.Add(it);
        }
    }
}
