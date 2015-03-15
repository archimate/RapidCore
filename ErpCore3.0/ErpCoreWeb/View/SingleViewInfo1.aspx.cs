using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.UI;

public partial class View_SingleViewInfo1 : System.Web.UI.Page
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
            if (Session["NewView"] == null)
            {
                m_View = new CView();
                m_View.Ctx = Global.GetCtx(Session["TopCompany"].ToString());
                m_View.VType = enumViewType.Single;
                SortedList<Guid, CView> sortObj = new SortedList<Guid, CView>();
                sortObj.Add(m_View.Id, m_View);
                Session["NewView"] = sortObj;
            }
            else
            {
                SortedList<Guid, CView> sortObj = (SortedList<Guid, CView>)Session["NewView"];
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
        List<CBaseObject> lstObj = Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.GetList();
        foreach (CBaseObject obj in lstObj)
        {
            CTable table = (CTable)obj;
            ListItem item = new ListItem(table.Name, table.Id.ToString());
            cbMasterTable.Items.Add(item);
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
                break;
            }
        }

    }

    protected void btNext_Click(object sender, EventArgs e)
    {
        if (!ValidatePage1())
            return;
        if (!SavePage1())
            return;
        Response.Redirect("SingleViewInfo2.aspx?id=" + Request["id"] + "&catalog_id=" + m_Catalog_id.ToString());
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
            m_Catalog_id = new Guid(cbCatalog.SelectedItem.Value);
            m_View.UI_ViewCatalog_id = m_Catalog_id;
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

        return true;
    }
    protected void btCancel_Click(object sender, EventArgs e)
    {
        Session["NewView"] = null;
        RegisterStartupScript("starup", "<script>parent.$.ligerDialog.close();</script>");
    }
}
