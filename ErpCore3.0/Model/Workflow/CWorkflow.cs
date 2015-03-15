// File:    CWorkflow.cs
// Created: 2012/7/21 21:38:30
// Purpose: Definition of Class CWorkflow

using System;
using System.Text;
using System.Collections.Generic;
using ErpCoreModel.Framework;
using ErpCoreModel.Base;

namespace ErpCoreModel.Workflow
{
    //审批状态
    public enum enumApprovalState {Init,Running,Accept,Reject,Cancel}
    public class CWorkflow : CBaseObject
    {

        public CWorkflow()
        {
            TbCode = "WF_Workflow";
            ClassName = "ErpCoreModel.Workflow.CWorkflow";

        }


        public Guid B_Company_id
        {
            get
            {
                if (m_arrNewVal.ContainsKey("b_company_id"))
                    return m_arrNewVal["b_company_id"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("b_company_id"))
                    m_arrNewVal["b_company_id"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("b_company_id", val);
                }
            }
        }
        public Guid WF_WorkflowDef_id
        {
            get
            {
                if (m_arrNewVal.ContainsKey("wf_workflowdef_id"))
                    return m_arrNewVal["wf_workflowdef_id"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {        
                if (m_arrNewVal.ContainsKey("wf_workflowdef_id"))
                    m_arrNewVal["wf_workflowdef_id"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("wf_workflowdef_id", val);
                }
            }
        }
        public Guid Row_id
        {
            get
            {
                if (m_arrNewVal.ContainsKey("row_id"))
                    return m_arrNewVal["row_id"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {        
                if (m_arrNewVal.ContainsKey("row_id"))
                    m_arrNewVal["row_id"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("row_id", val);
                }
            }
        }
        public enumApprovalState State
        {
            get
            {
                if (m_arrNewVal.ContainsKey("state"))
                    return (enumApprovalState)m_arrNewVal["state"].IntVal;
                else
                    return enumApprovalState.Init;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("state"))
                    m_arrNewVal["state"].IntVal = (int)value;
                else
                {
                    CValue val = new CValue();
                    val.IntVal = (int)value;
                    m_arrNewVal.Add("state", val);
                }
            }
        }

        public string GetStateString()
        {
            if (State == enumApprovalState.Init)
                return "初始化";
            else if (State == enumApprovalState.Running)
                return "进行中";
            else if (State == enumApprovalState.Accept)
                return "已接受";
            else if (State == enumApprovalState.Reject)
                return "已拒绝";
            else //if (State == enumApprovalState.Cancel)
                return "已撤销";

        }

        //获取工作流
        public CWorkflowDef GetWorkflowDef()
        {
            CWorkflowDefMgr workflowDefMgr = new CWorkflowDefMgr();
            workflowDefMgr.Ctx = this.Ctx;
            string sWhere = string.Format(" id='{0}'", WF_WorkflowDef_id);
            workflowDefMgr.Load(sWhere, false);
            CWorkflowDef WorkflowDef = (CWorkflowDef)workflowDefMgr.GetFirstObj();
            if (WorkflowDef == null)
                return null;

            CCompany Company = null;
            if (WorkflowDef.B_Company_id == Guid.Empty)
                Company = (CCompany)Ctx.CompanyMgr.FindTopCompany();
            else
                Company = (CCompany)Ctx.CompanyMgr.Find(WorkflowDef.B_Company_id);
            CWorkflowDef obj = (CWorkflowDef)Company.WorkflowDefMgr.Find(WF_WorkflowDef_id);
            return obj;
        }

        public CActivesMgr ActivesMgr
        {
            get
            {
                return (CActivesMgr)this.GetSubObjectMgr("WF_Actives", typeof(CActivesMgr));
            }
            set
            {
                this.SetSubObjectMgr("WF_Actives", value);
            }
        }

    }
}