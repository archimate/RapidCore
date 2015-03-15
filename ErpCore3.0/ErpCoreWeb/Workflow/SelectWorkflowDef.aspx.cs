using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.UI;
using ErpCoreModel.Workflow;

public partial class Workflow_SelectWorkflowDef : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["Action"] == "GetData")
        {
            GetData();
            Response.End();
        }
    }
    void GetData()
    {
        string B_Company_id = Request["B_Company_id"];
        string FW_Table_id = Request["FW_Table_id"];
        if (string.IsNullOrEmpty(B_Company_id)
            || string.IsNullOrEmpty(FW_Table_id))
            return;

        CCompany Company = (CCompany)Global.GetCtx(Session["TopCompany"].ToString()).CompanyMgr.Find(new Guid(B_Company_id));
        if (Company == null)
            return;

        string sData = "";
        List<CBaseObject> lstObj = Company.WorkflowDefMgr.GetList();

        int iCount = 0;
        foreach (CBaseObject obj in lstObj)
        {
            CWorkflowDef WorkflowDef = (CWorkflowDef)obj;
            if (WorkflowDef.FW_Table_id != new Guid(FW_Table_id))
                continue;

            sData += string.Format("{{ \"id\": \"{0}\",\"Name\":\"{1}\"}},"
                , WorkflowDef.Id, WorkflowDef.Name);
            iCount++;
        }
        sData = sData.TrimEnd(",".ToCharArray());
        sData = "[" + sData + "]";
        string sJson = string.Format("{{\"Rows\":{0},\"Total\":\"{1}\"}}"
            , sData, iCount);

        Response.Write(sJson);
    }
}
