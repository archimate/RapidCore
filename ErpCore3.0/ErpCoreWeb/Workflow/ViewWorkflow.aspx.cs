using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.Workflow;

public partial class Workflow_ViewWorkflow : System.Web.UI.Page
{
    public CBaseObjectMgr m_BaseObjectMgr = null;
    public CBaseObject m_BaseObject = null;
    public Guid m_guidParentId = Guid.Empty;

    public CUser m_User = null;
    CCompany m_Company = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null)
        {
            Response.Redirect("../Login.aspx");
            Response.End();
        }
        m_User = (CUser)Session["User"];

        m_Company = (CCompany)Global.GetCtx(Session["TopCompany"].ToString()).CompanyMgr.Find(m_User.B_Company_id);
        

        string TbCode = Request["TbCode"];
        string id = Request["id"];

        if (string.IsNullOrEmpty(TbCode)
            || string.IsNullOrEmpty(id))
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
        m_BaseObjectMgr.GetList();

        m_BaseObject = m_BaseObjectMgr.Find(new Guid(id));

        if (Request.Params["Action"] == "GetData")
        {
            GetData();
            Response.End();
        }
        else if (Request.Params["Action"] == "GetActivesData")
        {
            GetActivesData();
            Response.End();
        }
        else if (Request.Params["Action"] == "CancelWF")
        {
            CancelWF();
            Response.End();
        }
        else if (Request.Params["Action"] == "CanApproval")
        {
            CanApproval();
            Response.End();
        }
    }

    void GetData()
    {
        string sData = "";
        int iCount = 0;
        List<CWorkflow> lstWF = m_BaseObjectMgr.WorkflowMgr.FindByRowid(m_BaseObject.Id);
        foreach (CWorkflow wf in lstWF)
        {
            CWorkflowDef WorkflowDef = wf.GetWorkflowDef();
            if (WorkflowDef == null)
                continue;
            sData += string.Format("{{ \"id\": \"{0}\",\"Name\":\"{1}\",\"Created\":\"{2}\", \"State\":\"{3}\"}},"
                , wf.Id
                , WorkflowDef.Name
                , wf.Created
                , wf.GetStateString());
            iCount++;
        }
        sData = "[" + sData + "]";
        string sJson = string.Format("{{\"Rows\":{0},\"Total\":\"{1}\"}}"
            , sData, iCount);

        Response.Write(sJson);
    }

    void GetActivesData()
    {
        string WF_Workflow_id = Request["WF_Workflow_id"];
        CWorkflow wf = (CWorkflow)m_BaseObjectMgr.WorkflowMgr.Find(new Guid(WF_Workflow_id));

        string sData = "";
        CWorkflowDef WorkflowDef = wf.GetWorkflowDef();
        List<CBaseObject> lstObj = wf.ActivesMgr.GetList();
        foreach (CBaseObject obj in lstObj)
        {
            CActives Actives = (CActives)obj;

            CActivesDef ActivesDef = (CActivesDef)WorkflowDef.ActivesDefMgr.Find(Actives.WF_ActivesDef_id);
            CUser User = (CUser)Global.GetCtx(Session["TopCompany"].ToString()).UserMgr.Find(Actives.B_User_id);
            CRole Role = (CRole)Global.GetCtx(Session["TopCompany"].ToString()).CompanyMgr.FindTopCompany().RoleMgr.Find(Actives.B_Role_id);
            string UserName = "", RoleName = "";
            UserName = (User != null) ? User.Name : "";
            RoleName = (Role != null) ? Role.Name : "";

            sData += string.Format("{{ \"id\": \"{0}\",\"Name\":\"{1}\", \"Result\":\"{2}\", \"Comment\":\"{3}\", \"UserName\":\"{4}\", \"RoleName\":\"{5}\" }},"
                , Actives.Id
                , ActivesDef.Name
                , Actives.GetResultString()
                , Actives.Comment
                , UserName
                , RoleName);
        }

        sData = "[" + sData + "]";
        string sJson = string.Format("{{\"Rows\":{0},\"Total\":\"{1}\"}}"
            , sData, lstObj.Count);

        Response.Write(sJson);
    }

    void CancelWF()
    {
        string WF_Workflow_id = Request["WF_Workflow_id"];

        CWorkflow wf = (CWorkflow)m_BaseObjectMgr.WorkflowMgr.Find(new Guid(WF_Workflow_id));
        //只有启动者或管理员才能撤销
        if (wf.Creator != m_User.Id
            && !m_User.IsRole("管理员"))
        {
            Response.Write("没有权限撤销！");
            return;
        }

        if (!m_BaseObjectMgr.WorkflowMgr.CancelWorkflow(wf))
        {
            Response.Write("撤销失败！");
            return;
        }
    }

    void CanApproval()
    {
        string WF_Workflow_id = Request["WF_Workflow_id"];

        CWorkflow wf = (CWorkflow)m_BaseObjectMgr.WorkflowMgr.Find(new Guid(WF_Workflow_id));
        if (wf.State != enumApprovalState.Running)
        {
            Response.Write("只有进行中的工作流才能审批！");
            return;
        }
        CActives Actives = wf.ActivesMgr.FindNotApproval();
        if (Actives == null)
        {
            Response.Write("没有审批的活动！");
            return;
        }

        if (Actives.AType == "按用户")
        {
            if (Actives.B_User_id != m_User.Id)
            {
                Response.Write("没有权限审批！");
                return;
            }
        }
        else //按角色
        {
            CRole Role = (CRole)m_Company.RoleMgr.Find(Actives.B_Role_id);
            if (Role==null)
            {
                Response.Write("角色不存在！");
                return;
            }
            CUserInRole UserInRole = Role.UserInRoleMgr.FindByUserid(m_User.Id);
            if (UserInRole == null)
            {
                Response.Write("没有权限审批！");
                return;
            }
        }
    }
}
