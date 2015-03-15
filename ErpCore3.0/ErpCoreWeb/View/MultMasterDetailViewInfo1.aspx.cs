using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.UI;

public partial class View_MultMasterDetailViewInfo1 : System.Web.UI.Page
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
            if (Session["NewMultMasterDetailView"] == null)
            {
                m_View = new CView();
                m_View.Ctx = Global.GetCtx(Session["TopCompany"].ToString());
                m_View.VType = enumViewType.MultMasterDetail;

                SortedList<Guid, CView> sortObj = new SortedList<Guid, CView>();
                sortObj.Add(m_View.Id, m_View);
                Session["NewMultMasterDetailView"] = sortObj;
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

        listViewDetail.Items.Clear();
        List<CBaseObject> lstObj = m_View.ViewDetailMgr.GetList();
        foreach (CBaseObject obj in lstObj)
        {
            CViewDetail ViewDetail = (CViewDetail)obj;
            CTable table = (CTable)Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.Find(ViewDetail.FW_Table_id);
            if (table == null)
                continue;
            ListItem item = new ListItem(table.Name,ViewDetail.Id.ToString());
            listViewDetail.Items.Add(item);
        }
    }

    protected void btNext_Click(object sender, EventArgs e)
    {
        if (!ValidatePage1())
            return;
        if (!SavePage1())
            return;
        Response.Redirect("MultMasterDetailViewInfo2.aspx?id=" + Request["id"] + "&catalog_id=" + m_Catalog_id.ToString());
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
        CTable tbM = (CTable)Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.Find(new Guid(cbMasterTable.SelectedItem.Value));
        if (listViewDetail.Items.Count == 0)
        {
            RegisterStartupScript("starup", "<script>alert('请选择从表！');</script>");
            return false;
        }
        for (int i = 0; i < listViewDetail.Items.Count; i++)
        {
            CViewDetail vd = (CViewDetail)m_View.ViewDetailMgr.Find(new Guid(listViewDetail.Items[i].Value));
            if (vd.FW_Table_id == tbM.Id)
            {
                RegisterStartupScript("starup", "<script>alert('主表不能与从表相同！");
                return false;
            }
            if (vd.PrimaryKey == Guid.Empty || vd.ForeignKey == Guid.Empty)
            {
                string strMsg = string.Format("请设置 {0} 外键关联！", listViewDetail.Items[i].Text);
                RegisterStartupScript("starup", string.Format("<script>alert('{0}');</script>",strMsg));
                return false;
            }
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
        for (int i = 0; i < listViewDetail.Items.Count; i++)
        {
            CViewDetail vd = (CViewDetail)m_View.ViewDetailMgr.Find(new Guid(listViewDetail.Items[i].Value));
            vd.Idx = i;
            m_View.ViewDetailMgr.Update(vd);
        }


        return true;
    }
    protected void btCancel_Click(object sender, EventArgs e)
    {
        Session["NewMultMasterDetailView"] = null;
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

    protected void listViewDetail_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadForeignKey();

        if (listViewDetail.SelectedIndex == -1)
            return;
        string sVdId = listViewDetail.SelectedItem.Value;
        CViewDetail ViewDetail = (CViewDetail)m_View.ViewDetailMgr.Find(new Guid(sVdId));
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
    void LoadForeignKey()
    {
        if (listViewDetail.SelectedIndex == -1)
            return;
        string sVdId = listViewDetail.SelectedItem.Value;
        CViewDetail ViewDetail = (CViewDetail)m_View.ViewDetailMgr.Find(new Guid(sVdId));
        CTable table = (CTable)Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.Find(ViewDetail.FW_Table_id);
        cbForeignKey.Items.Clear();
        List<CBaseObject> lstObj = table.ColumnMgr.GetList();
        foreach (CBaseObject obj in lstObj)
        {
            CColumn column = (CColumn)obj;
            ListItem it = new ListItem(column.Name, column.Id.ToString());
            cbForeignKey.Items.Add(it);
        }
    }
    protected void btUp_Click(object sender, EventArgs e)
    {
        if (listViewDetail.SelectedIndex == -1)
        {
            RegisterStartupScript("starup", "<script>alert('请选择一项！');</script>");
            return;
        }
        if (listViewDetail.SelectedIndex == 0)
            return;
        int idx = listViewDetail.SelectedIndex;
        ListItem item = listViewDetail.Items[idx];
        listViewDetail.Items.RemoveAt(idx);
        listViewDetail.Items.Insert(idx - 1, item);
    }
    protected void btDown_Click(object sender, EventArgs e)
    {
        if (listViewDetail.SelectedIndex == -1)
        {
            RegisterStartupScript("starup", "<script>alert('请选择一项！');</script>");
            return;
        }
        if (listViewDetail.SelectedIndex == listViewDetail.Items.Count - 1)
            return;

        int idx = listViewDetail.SelectedIndex;
        ListItem item = listViewDetail.Items[idx];
        listViewDetail.Items.RemoveAt(idx);
        listViewDetail.Items.Insert(idx + 1, item);
    }
    protected void btDel_Click(object sender, EventArgs e)
    {
        if (listViewDetail.SelectedIndex == -1)
        {
            RegisterStartupScript("starup", "<script>alert('请选择一项！');</script>");
            return;
        }
        listViewDetail.Items.RemoveAt(listViewDetail.SelectedIndex);
    }
    protected void btAdd_Click(object sender, EventArgs e)
    {
        if (cbMasterTable.SelectedIndex == -1)
        {
            RegisterStartupScript("starup", "<script>alert('请选择主表！');</script>");
            return;
        }
        if (cbDetailTable.SelectedIndex == -1)
        {
            RegisterStartupScript("starup", "<script>alert('请选择从表！');</script>");
            return;
        }
        CTable table = (CTable)Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.Find(new Guid(cbDetailTable.SelectedItem.Value));
        for (int i = 0; i < listViewDetail.Items.Count; i++)
        {
            if (listViewDetail.Items[i].Value == table.Id.ToString())
            {
                RegisterStartupScript("starup", "<script>alert('从表已经存在！');</script>");
                return;
            }
        }
        if (cbMasterTable.SelectedIndex != -1)
        {
            if (cbMasterTable.SelectedItem.Value == table.Id.ToString())
            {
                RegisterStartupScript("starup", "<script>alert('从表不能与主表相同！');</script>");
                return;
            }
        }
        CViewDetail ViewDetail = new CViewDetail();
        ViewDetail.Ctx = Global.GetCtx(Session["TopCompany"].ToString());
        ViewDetail.FW_Table_id = table.Id;
        ViewDetail.Name = table.Name;
        ViewDetail.UI_View_id = m_View.Id;
        ViewDetail.Idx = listViewDetail.Items.Count;
        m_View.ViewDetailMgr.AddNew(ViewDetail);

        ListItem it = new ListItem(ViewDetail.Name, ViewDetail.Id.ToString());
        listViewDetail.Items.Add(it);

    }
    protected void cbPrimaryKey_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (listViewDetail.SelectedIndex == -1)
            return;
        CViewDetail ViewDetail = (CViewDetail)m_View.ViewDetailMgr.Find(new Guid( listViewDetail.SelectedItem.Value));
        if (cbPrimaryKey.SelectedIndex == -1)
            ViewDetail.PrimaryKey = Guid.Empty;
        else
            ViewDetail.PrimaryKey = new Guid(cbPrimaryKey.SelectedItem.Value);
        if (cbForeignKey.SelectedIndex == -1)
            ViewDetail.ForeignKey = Guid.Empty;
        else
            ViewDetail.ForeignKey = new Guid(cbForeignKey.SelectedItem.Value);

    }
    protected void cbForeignKey_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (listViewDetail.SelectedIndex == -1)
            return;
        CViewDetail ViewDetail = (CViewDetail)m_View.ViewDetailMgr.Find(new Guid(listViewDetail.SelectedItem.Value));
        if (cbPrimaryKey.SelectedIndex == -1)
            ViewDetail.PrimaryKey = Guid.Empty;
        else
            ViewDetail.PrimaryKey = new Guid(cbPrimaryKey.SelectedItem.Value);
        if (cbForeignKey.SelectedIndex == -1)
            ViewDetail.ForeignKey = Guid.Empty;
        else
            ViewDetail.ForeignKey = new Guid(cbForeignKey.SelectedItem.Value);
    }
}
