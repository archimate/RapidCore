using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.UI;

public partial class View_SetViewFilter : System.Web.UI.Page
{
    public CUser m_User = null;
    public CTable m_Table = null;
    public CView m_View = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null)
        {
            Response.End();
        }
        m_User = (CUser)Session["User"];

        string vid = Request["vid"];
        if (string.IsNullOrEmpty(vid))
        {
            Response.End();
        }
        m_View = (CView)Global.GetCtx(Session["TopCompany"].ToString()).ViewMgr.Find(new Guid(vid));
        if (m_View==null)
        {
            Response.End();
        }
        m_Table = (CTable)Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.Find(m_View.FW_Table_id);

        if (Request.Params["Action"] == "GetData")
        {
            GetData();
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
        List<CBaseObject> lstObj = m_Table.ColumnMgr.GetList();
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

        if (Session["ViewFilterMgr"] != null)
        {
            SortedList<Guid, CViewFilterMgr> sortObj = (SortedList<Guid, CViewFilterMgr>)Session["ViewFilterMgr"];
            if (sortObj.ContainsKey(m_View.Id))
            {
                CViewFilterMgr ViewFilterMgr = sortObj[m_View.Id];
                ViewFilterMgr.IsLoad = true; //避免从数据库装载
                List<CBaseObject> lstObj = ViewFilterMgr.GetList();
                foreach (CBaseObject obj in lstObj)
                {
                    CViewFilter ViewFilter = (CViewFilter)obj;

                    CColumn col = (CColumn)m_Table.ColumnMgr.Find(ViewFilter.FW_Column_id);
                    if (col == null)
                        continue;

                    sData += string.Format("{{ \"id\": \"{0}\",\"AndOr\":\"{1}\",\"Column\":\"{2}\",\"Sign\":\"{3}\", \"Val\":\"{4}\" }},"
                        , ViewFilter.Id
                        , ViewFilter.AndOr.Equals("and", StringComparison.OrdinalIgnoreCase) ? "与" : "或"
                        , col.Name
                        , ViewFilter.GetSignName()
                        , ViewFilter.Val);

                    iCount++;
                }
            }
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
        
        
        if (Session["ViewFilterMgr"] == null)
            return;
        SortedList<Guid, CViewFilterMgr> sortObj = (SortedList<Guid, CViewFilterMgr>)Session["ViewFilterMgr"];
        if (sortObj.ContainsKey(m_View.Id))
        {
            CViewFilterMgr ViewFilterMgr = sortObj[m_View.Id];

            ViewFilterMgr.Delete(new Guid(delid));
        }
    }
   

    protected void btAdd_Click(object sender, EventArgs e)
    {
        if (Session["ViewFilterMgr"] == null)
        
        {
            SortedList<Guid, CViewFilterMgr> sortObj0 = new SortedList<Guid,CViewFilterMgr>();
            CViewFilterMgr ViewFilterMgr0 = new CViewFilterMgr();
            ViewFilterMgr0.Ctx = Global.GetCtx(Session["TopCompany"].ToString());
            sortObj0.Add(m_View.Id,ViewFilterMgr0);
            Session["ViewFilterMgr"] = sortObj0;
        }
        SortedList<Guid, CViewFilterMgr> sortObj = (SortedList<Guid, CViewFilterMgr>)Session["ViewFilterMgr"];
        if (!sortObj.ContainsKey(m_View.Id))
        {
            CViewFilterMgr ViewFilterMgr0 = new CViewFilterMgr();
            ViewFilterMgr0.Ctx = Global.GetCtx(Session["TopCompany"].ToString());
            sortObj.Add(m_View.Id, ViewFilterMgr0);
        }
        CViewFilterMgr ViewFilterMgr = sortObj[m_View.Id];

        CViewFilter Filter = new CViewFilter();
        Filter.Ctx = ViewFilterMgr.Ctx;
        Filter.UI_View_id = m_View.Id;
        Filter.AndOr = cbAndOr.SelectedItem.Value;
        Filter.FW_Table_id = m_Table.Id;
        Filter.FW_Column_id =new Guid( cbColumn.SelectedItem.Value);
        Filter.Sign = (CompareSign)cbSign.SelectedIndex;
        Filter.Val = txtVal.Text.Trim();
        Filter.Idx = m_View.ViewFilterMgr.GetList().Count;

        ViewFilterMgr.AddNew(Filter);

        txtVal.Text = "";
    }
}
