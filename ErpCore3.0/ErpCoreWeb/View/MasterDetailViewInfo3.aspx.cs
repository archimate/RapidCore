using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.UI;

public partial class View_MasterDetailViewInfo3 : System.Web.UI.Page
{
    public CView m_View = null;
    public Guid m_Catalog_id = Guid.Empty;
    bool m_bIsNew = false;
    public CTable m_MTable = null;
    public CTable m_DTable = null;

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
                Response.Redirect("MasterDetailViewInfo1.aspx?id=" + Request["id"] + "&catalog_id=" + Request["catalog_id"]);
                return;
            }
            else
            {
                SortedList<Guid, CView> sortObj = (SortedList<Guid, CView>)Session["NewMasterDetailView"];
                m_View = sortObj.Values[0];
            }
        }
        m_MTable = (CTable)Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.Find(m_View.FW_Table_id);
        CViewDetail ViewDetail = (CViewDetail)m_View.ViewDetailMgr.GetFirstObj();
        if (ViewDetail == null)
        {
            Response.Redirect("MasterDetailViewInfo1.aspx?id=" + Request["id"] + "&catalog_id=" + Request["catalog_id"]);
            return;
        }
        m_DTable = (CTable)Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.Find(ViewDetail.FW_Table_id);

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
        else if (Request.Params["Action"] == "GetDetail")
        {
            GetDetail();
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
    }

    void GetData()
    {
        List<CBaseObject> lstObj = m_View.ColumnInViewMgr.GetList();
        List<CColumnInView> sortObj = new List<CColumnInView>();
        foreach (CBaseObject obj in lstObj)
        {
            CColumnInView civ = (CColumnInView)obj;
            sortObj.Add(civ);
        }
        sortObj.Sort();

        string sData = "";

        int iCount = 0;
        foreach(CColumnInView civ in sortObj)
        {
            CColumn col = (CColumn)m_MTable.ColumnMgr.Find(civ.FW_Column_id);
            if (col == null)
                continue;

            sData += string.Format("{{ \"id\": \"{0}\",\"ColName\":\"{1}\",\"Caption\":\"{2}\"}},"
                , civ.Id, col.Name, civ.Caption);

            iCount++;
        }
        sData = sData.TrimEnd(",".ToCharArray());
        sData = "[" + sData + "]";
        string sJson = string.Format("{{\"Rows\":{0},\"Total\":\"{1}\"}}"
            , sData, iCount);

        Response.Write(sJson);
    }
    void GetDetail()
    {
        CViewDetail ViewDetail =(CViewDetail)m_View.ViewDetailMgr.GetFirstObj();
        if (ViewDetail == null)
            return;
        List<CBaseObject> lstObj = ViewDetail.ColumnInViewDetailMgr.GetList();
        List<CColumnInViewDetail> sortObj = new List<CColumnInViewDetail>();
        foreach (CBaseObject obj in lstObj)
        {
            CColumnInViewDetail civd = (CColumnInViewDetail)obj;
            sortObj.Add(civd);
        }
        sortObj.Sort();

        string sData = "";

        int iCount = 0;
        foreach (CColumnInViewDetail civd in sortObj)
        {
            CColumn col = (CColumn)m_DTable.ColumnMgr.Find(civd.FW_Column_id);
            if (col == null)
                continue;

            sData += string.Format("{{ \"id\": \"{0}\",\"ColName\":\"{1}\",\"Caption\":\"{2}\"}},"
                , civd.Id, col.Name, civd.Caption);

            iCount++;
        }
        sData = sData.TrimEnd(",".ToCharArray());
        sData = "[" + sData + "]";
        string sJson = string.Format("{{\"Rows\":{0},\"Total\":\"{1}\"}}"
            , sData, iCount);

        Response.Write(sJson);
    }
    void PostData()
    {
        string GridData = Request["GridData"];
        if (string.IsNullOrEmpty(GridData))
        {
            Response.Write("主表字段不能空！");
            return;
        }
        string GridData2 = Request["GridData2"];
        if (string.IsNullOrEmpty(GridData2))
        {
            Response.Write("从表字段不能空！");
            return;
        }

        string[] arr1 = GridData.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < arr1.Length; i++)
        {
            string[] arr2 = arr1[i].Split(":".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            CColumnInView civ = (CColumnInView)m_View.ColumnInViewMgr.Find(new Guid(arr2[0]));
            civ.Caption = arr2[1];
            civ.Idx = i;
            m_View.ColumnInViewMgr.Update(civ);
        }


        CViewDetail ViewDetail = (CViewDetail)m_View.ViewDetailMgr.GetFirstObj();
        arr1 = GridData2.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < arr1.Length; i++)
        {
            string[] arr2 = arr1[i].Split(":".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            CColumnInViewDetail civd = (CColumnInViewDetail)ViewDetail.ColumnInViewDetailMgr.Find(new Guid(arr2[0]));
            civd.Caption = arr2[1];
            civd.Idx = i;
            ViewDetail.ColumnInViewDetailMgr.Update(civd);
        }
    }
    void Cancel()
    {
        Session["NewMasterDetailView"] = null;
    }
}
