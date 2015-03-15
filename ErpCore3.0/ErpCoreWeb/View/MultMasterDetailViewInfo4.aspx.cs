using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.UI;

public partial class View_MultMasterDetailViewInfo4 : System.Web.UI.Page
{
    public CUser m_User = null;
    public CView m_View = null;
    public Guid m_Catalog_id = Guid.Empty;
    bool m_bIsNew = false;
    public CTable m_MTable = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null)
        {
            Response.Redirect("../Login.aspx");
            Response.End();
        }
        m_User = (CUser)Session["User"];

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
                Response.Redirect("MultMasterDetailViewInfo1.aspx?id=" + Request["id"] + "&catalog_id=" + Request["catalog_id"]);
                return;
            }
            else
            {
                SortedList<Guid, CView> sortObj = (SortedList<Guid, CView>)Session["NewMultMasterDetailView"];
                m_View = sortObj.Values[0];
            }
        }
        m_MTable = (CTable)Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.Find(m_View.FW_Table_id);

        string catalog_id = Request["catalog_id"];
        if (!string.IsNullOrEmpty(catalog_id))
        {
            m_Catalog_id = new Guid(catalog_id);
        }


        if (Request.Params["Action"] == "GetData")
        {
            GetData();
            Response.End();
        }
        else if (Request.Params["Action"] == "PostData")
        {
            PostData();
            Response.End();
        }
        else if (Request.Params["Action"] == "Cancel")
        {
            Cancel();
            Response.End();
        }
        else if (Request.Params["Action"] == "Delete")
        {
            Delete();
            Response.End();
        }

        if (!IsPostBack)
        {
            LoadColumn();
        }
    }
    void LoadColumn()
    {
        cbColumn.Items.Clear();
        List<CBaseObject> lstObj = m_MTable.ColumnMgr.GetList();
        foreach (CBaseObject obj in lstObj)
        {
            CColumn col = (CColumn)obj;
            ListItem item = new ListItem(col.Name,col.Id.ToString());
            cbColumn.Items.Add(item);
        }
    }
    
    void GetData()
    {
        string sData = "";
        int iCount = 0;

        List<CBaseObject> lstObj = m_View.ViewFilterMgr.GetList();
        List<CViewFilter> sortObj = new List<CViewFilter>();
        foreach (CBaseObject obj in lstObj)
        {
            CViewFilter ViewFilter = (CViewFilter)obj;
            sortObj.Add(ViewFilter);
        }
        sortObj.Sort();

        foreach (CViewFilter ViewFilter in sortObj)
        {
            CColumn col = (CColumn)m_MTable.ColumnMgr.Find(ViewFilter.FW_Column_id);
            if (col == null)
                continue;
            
            sData += string.Format("{{ \"id\": \"{0}\",\"AndOr\":\"{1}\",\"Column\":\"{2}\",\"Sign\":\"{3}\", \"Val\":\"{4}\" }},"
                , ViewFilter.Id
                , ViewFilter.AndOr.Equals("and", StringComparison.OrdinalIgnoreCase)?"与":"或"
                , col.Name
                , ViewFilter.GetSignName()
                , ViewFilter.Val);

            iCount++;
        }

        sData = sData.TrimEnd(",".ToCharArray());
        sData = "[" + sData + "]";
        string sJson = string.Format("{{\"Rows\":{0},\"Total\":\"{1}\"}}"
            , sData, iCount);

        Response.Write(sJson);
    }
    void Delete()
    {
        string delid = Request["delid"];
        if (string.IsNullOrEmpty(delid))
        {
            Response.Write("请选择一项！");
            return;
        }
        
        m_View.ViewFilterMgr.Delete(new Guid(delid));

    }
    void PostData()
    {
        if (m_bIsNew)
        {
            m_View.Creator = m_User.Id;
            m_View.Updated = DateTime.Now;
            Global.GetCtx(Session["TopCompany"].ToString()).ViewMgr.AddNew(m_View);
        }
        else
        {
            m_View.Updator = m_User.Id;
            m_View.Updated = DateTime.Now;
            Global.GetCtx(Session["TopCompany"].ToString()).ViewMgr.Update(m_View);
        }
        if (!Global.GetCtx(Session["TopCompany"].ToString()).ViewMgr.Save(true))
        {
            Response.Write("保存失败！");
        }
    }
    void Cancel()
    {
        Session["NewMultMasterDetailView"] = null;
    }
   

    protected void btAdd_Click(object sender, EventArgs e)
    {
        CViewFilter Filter = new CViewFilter();
        Filter.Ctx = Global.GetCtx(Session["TopCompany"].ToString());
        Filter.UI_View_id = m_View.Id;
        Filter.AndOr = cbAndOr.SelectedItem.Value;
        Filter.FW_Table_id = m_MTable.Id;
        Filter.FW_Column_id =new Guid( cbColumn.SelectedItem.Value);
        Filter.Sign = (CompareSign)cbSign.SelectedIndex;
        Filter.Val = txtVal.Text.Trim();
        Filter.Idx = m_View.ViewFilterMgr.GetList().Count;

        m_View.ViewFilterMgr.AddNew(Filter);

        txtVal.Text = "";
    }

}
