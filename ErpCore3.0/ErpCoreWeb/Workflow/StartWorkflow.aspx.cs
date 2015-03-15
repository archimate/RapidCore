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

public partial class Workflow_StartWorkflow : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null)
        {
            Response.Redirect("../Login.aspx");
            Response.End();
        }
        string TbCode = Request["TbCode"];
        string id = Request["id"];
        string WF_WorkflowDef_id = Request["WF_WorkflowDef_id"];

        if (string.IsNullOrEmpty(TbCode)
            || string.IsNullOrEmpty(id)
            || string.IsNullOrEmpty(WF_WorkflowDef_id))
        {
            Response.Write("数据不完整！");
            Response.End();
            return;
        }

        Guid guidParentId = Guid.Empty;
        string ParentId = Request["ParentId"];
        if (!string.IsNullOrEmpty(ParentId))
            guidParentId = new Guid(ParentId);

        CBaseObjectMgr BaseObjectMgr = Global.GetCtx(Session["TopCompany"].ToString()).FindBaseObjectMgrCache(TbCode, guidParentId);
        if (BaseObjectMgr == null)
        {
            BaseObjectMgr = new CBaseObjectMgr();
            BaseObjectMgr.TbCode = TbCode;
            BaseObjectMgr.Ctx = Global.GetCtx(Session["TopCompany"].ToString());
        }
        //只能存在一个运行的工作流实例
        Guid objId = new Guid(id);
        List<CWorkflow> lstWF = BaseObjectMgr.WorkflowMgr.FindLastByRowid(objId);
        foreach (CWorkflow wf in lstWF)
        {
            if (wf.WF_WorkflowDef_id == new Guid(WF_WorkflowDef_id)
                && wf.State == enumApprovalState.Running)
            {
                Response.Write("该工作流已经启动！");
                Response.End();
                return;
            }
        }
        //创建工作流实例并运行
        CUser user = (CUser)Session["User"];
        CWorkflow Workflow = new CWorkflow();
        Workflow.Ctx = Global.GetCtx(Session["TopCompany"].ToString());
        Workflow.WF_WorkflowDef_id = new Guid(WF_WorkflowDef_id);
        Workflow.State = enumApprovalState.Init;
        Workflow.Row_id = objId;
        Workflow.Creator = user.Id;
        Workflow.B_Company_id = user.B_Company_id;
        string sErr = "";
        if (!BaseObjectMgr.WorkflowMgr.StartWorkflow(Workflow, out sErr))
        {
            Response.Write(string.Format("启动工作流失败：{0}", sErr));
            Response.End();
            return;
        }
        BaseObjectMgr.WorkflowMgr.AddNew(Workflow);
        if (!BaseObjectMgr.WorkflowMgr.Save(true))
        {
            Response.Write("创建工作流失败！");
            Response.End();
            return;
        }
        Response.End();
    }
}
