using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.Workflow;

public partial class Workflow_AddActivesDef : System.Web.UI.Page
{
    public CTable m_Table = null;
    public CWorkflowDef m_WorkflowDef = null;
    public CActivesDefMgr m_ActivesDefMgr = null;
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

        string wfid = Request["wfid"];
        if (string.IsNullOrEmpty(wfid))
        {
            Response.End();
            return;
        }
        m_WorkflowDef = (CWorkflowDef)m_Company.WorkflowDefMgr.Find(new Guid(wfid));
        if (m_WorkflowDef == null) //可能是新建的
        {
            if (Session["AddWorkflowDef"] == null)
            {
                Response.End();
                return;
            }
            m_WorkflowDef = (CWorkflowDef)Session["AddWorkflowDef"];
        }
        m_ActivesDefMgr = (CActivesDefMgr)m_WorkflowDef.ActivesDefMgr;

        m_Table = m_ActivesDefMgr.Table;

        if (Request.Params["Action"] == "Cancel")
        {
            m_ActivesDefMgr.Cancel();
            Response.End();
        }
        else if (Request.Params["Action"] == "PostData")
        {
            PostData();
            Response.End();
        }
    }
    void PostData()
    {
        CUser user = (CUser)Session["User"];
        CActivesDef BaseObject = new CActivesDef();
        BaseObject.Ctx = Global.GetCtx(Session["TopCompany"].ToString());
        BaseObject.Creator = user.Id;
        //默认链接==
        CLink link1 = new CLink();
        link1.Ctx = Global.GetCtx(Session["TopCompany"].ToString());
        link1.WF_WorkflowDef_id = m_WorkflowDef.Id;
        link1.PreActives = BaseObject.Id;
        link1.Result = enumApprovalResult.Accept;
        CActivesDef adSuccess = m_WorkflowDef.ActivesDefMgr.FindSuccess();
        if (adSuccess != null)
            link1.NextActives = adSuccess.Id;
        else
            link1.NextActives = Guid.Empty;
        link1.Creator = user.Id;
        m_WorkflowDef.LinkMgr.AddNew(link1);
        CLink link2 = new CLink();
        link2.Ctx = Global.GetCtx(Session["TopCompany"].ToString());
        link2.WF_WorkflowDef_id = m_WorkflowDef.Id;
        link2.PreActives = BaseObject.Id;
        link2.Result = enumApprovalResult.Reject;
        CActivesDef adFailure = m_WorkflowDef.ActivesDefMgr.FindFailure();
        if (adFailure != null)
            link2.NextActives = adFailure.Id;
        else
            link2.NextActives = Guid.Empty;
        link2.Creator = user.Id;
        m_WorkflowDef.LinkMgr.AddNew(link2);
        //==
        List<CBaseObject> lstCol = m_Table.ColumnMgr.GetList();
        bool bHasVisible = false;
        foreach (CBaseObject obj in lstCol)
        {
            CColumn col = (CColumn)obj;

            if (col.Code.Equals("id", StringComparison.OrdinalIgnoreCase))
                continue;
            else if (col.Code.Equals("Created", StringComparison.OrdinalIgnoreCase))
                continue;
            else if (col.Code.Equals("Creator", StringComparison.OrdinalIgnoreCase))
            {
                //BaseObject.SetColValue(col, Program.User.Id);
                continue;
            }
            else if (col.Code.Equals("Updated", StringComparison.OrdinalIgnoreCase))
                continue;
            else if (col.Code.Equals("Updator", StringComparison.OrdinalIgnoreCase))
            {
                //BaseObject.SetColValue(col, Program.User.Id);
                continue;
            }

            BaseObject.SetColValue(col, Request.Params[col.Code]);
            bHasVisible = true;
        }
        if (!bHasVisible)
        {
            Response.Write("没有可修改字段！");
            return ;
        }
        m_ActivesDefMgr.AddNew(BaseObject);
    }
}
