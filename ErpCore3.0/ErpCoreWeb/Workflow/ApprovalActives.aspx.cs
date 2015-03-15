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

public partial class Workflow_ApprovalActives : System.Web.UI.Page
{
    public CWorkflow m_Workflow = null;
    public CActives m_Actives = null;
    public CBaseObjectMgr m_BaseObjectMgr = null;
    public CBaseObject m_BaseObject = null;
    public Guid m_guidParentId = Guid.Empty;
    public CUser m_User = null;
    CCompany m_Company = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if(Session["User"]==null)
        {
            Response.Redirect("../Login.aspx");
            Response.End();
        }
        m_User = (CUser)Session["User"];

        m_Company = (CCompany)Global.GetCtx(Session["TopCompany"].ToString()).CompanyMgr.Find(m_User.B_Company_id);
        
        string TbCode = Request["TbCode"];
        string id = Request["id"];
        string WF_Workflow_id = Request["WF_Workflow_id"];

        if (string.IsNullOrEmpty(TbCode)
            || string.IsNullOrEmpty(id)
            || string.IsNullOrEmpty(WF_Workflow_id))
        {
            Response.Write("数据不完整！");
            Response.End();
            return;
        }

        string ParentId = Request["ParentId"];
        if (!string.IsNullOrEmpty(ParentId))
            m_guidParentId = new Guid(ParentId);

        m_BaseObjectMgr = Global.GetCtx(Session["TopCompany"].ToString()).FindBaseObjectMgrCache(TbCode, m_guidParentId);
        if (m_BaseObjectMgr == null)
        {
            m_BaseObjectMgr = new CBaseObjectMgr();
            m_BaseObjectMgr.TbCode = TbCode;
            m_BaseObjectMgr.Ctx = Global.GetCtx(Session["TopCompany"].ToString());
        }

        m_BaseObject = m_BaseObjectMgr.Find(new Guid(id));

        m_Workflow = (CWorkflow)m_BaseObjectMgr.WorkflowMgr.Find(new Guid(WF_Workflow_id));
        if (m_Workflow.State != enumApprovalState.Running)
        {
            Response.Write("只有进行中的工作流才能审批！");
            Response.End();
            return;
        }
        m_Actives = m_Workflow.ActivesMgr.FindNotApproval();
        if (m_Actives == null)
        {
            Response.Write("没有审批的活动！");
            Response.End();
            return;
        }

        if (m_Actives.AType == "按用户")
        {
            if (m_Actives.B_User_id != m_User.Id)
            {
                Response.Write("没有权限审批！");
                Response.End();
                return;
            }
        }
        else //按角色
        {
            CRole Role =(CRole) m_Company.RoleMgr.Find(m_Actives.B_Role_id);
            if (Role == null || Role.UserInRoleMgr.FindByUserid(m_User.Id)==null)
            {
                Response.Write("没有权限审批！");
                Response.End();
                return;
            }
        }

        if (Request.Params["Action"] == "Accept")
        {
            Accept();
            Response.End();
        }
        else if (Request.Params["Action"] == "Reject")
        {
            Reject();
            Response.End();
        }
    }
    void Accept()
    {
        m_Actives.Result = enumApprovalResult.Accept;
        m_Actives.Comment = Request["Comment"];
        m_Actives.B_User_id = m_User.Id;

        string sErr = "";
        CWorkflowMgr WorkflowMgr = (CWorkflowMgr)m_Workflow.m_ObjectMgr;
        if (!WorkflowMgr.Approval(m_Workflow, m_Actives, out sErr))
        {
            Response.Write(sErr);
            return;
        }
    }
    void Reject()
    {
        m_Actives.Result = enumApprovalResult.Reject;
        m_Actives.Comment = Request["Comment"];
        m_Actives.B_User_id = m_User.Id;

        string sErr = "";
        CWorkflowMgr WorkflowMgr = (CWorkflowMgr)m_Workflow.m_ObjectMgr;
        if (!WorkflowMgr.Approval(m_Workflow, m_Actives, out sErr))
        {
            Response.Write(sErr);
            return;
        }
    }
}
