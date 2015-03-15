using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.Report;

public partial class Report_SelectCatalog : System.Web.UI.Page
{
    public CCompany m_Company = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null)
        {
            Response.Redirect("../Login.aspx");
            Response.End();
        }

        string B_Company_id = Request["B_Company_id"];
        if (string.IsNullOrEmpty(B_Company_id))
            m_Company = Global.GetCtx(Session["TopCompany"].ToString()).CompanyMgr.FindTopCompany();
        else
            m_Company = (CCompany)Global.GetCtx(Session["TopCompany"].ToString()).CompanyMgr.Find(new Guid(B_Company_id));

        if (Request.Params["Action"] == "GetData")
        {
            GetData();
            Response.End();
        }
    }

    void GetData()
    {
        string pid = Request["pid"];
        Guid Parent_id = Guid.Empty;
        if (!string.IsNullOrEmpty(pid))
            Parent_id = new Guid(pid);
        //context.Response.Write(@"[{text: '工作流'}]");
        string sJson = "[";
        List<CBaseObject> lstCatalog = m_Company.ReportCatalogMgr.GetList();
        foreach (CBaseObject obj in lstCatalog)
        {
            CReportCatalog catalog = (CReportCatalog)obj;
            if (catalog.Parent_id == Parent_id)
            {
                string sItem = string.Format("{{ isexpand: \"false\",\"id\":\"{0}\", name: \"{1}\",\"text\": \"{1}\", children: [] }},",
                    catalog.Id,
                    catalog.Name);
                sJson += sItem;
            }
        }
        sJson = sJson.TrimEnd(",".ToCharArray());
        sJson += "]";
        Response.Write(sJson);
    }
}
