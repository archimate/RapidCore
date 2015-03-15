// File:    CLink.cs
// Created: 2012/7/21 21:38:30
// Purpose: Definition of Class CLink

using System;
using System.Text;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Workflow
{
    public enum enumApprovalResult {Init, Accept, Reject }
    public class CLink : CBaseObject
    {

        public CLink()
        {
            TbCode = "WF_Link";
            ClassName = "ErpCoreModel.Workflow.CLink";

        }

        
        public Guid PreActives
        {
            get
            {
                if (m_arrNewVal.ContainsKey("preactives"))
                    return m_arrNewVal["preactives"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {        
                if (m_arrNewVal.ContainsKey("preactives"))
                    m_arrNewVal["preactives"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("preactives", val);
                }
            }
        }
        public Guid NextActives
        {
            get
            {
                if (m_arrNewVal.ContainsKey("nextactives"))
                    return m_arrNewVal["nextactives"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {        
                if (m_arrNewVal.ContainsKey("nextactives"))
                    m_arrNewVal["nextactives"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("nextactives", val);
                }
            }
        }
        public string Condiction
        {
            get
            {
                if (m_arrNewVal.ContainsKey("condiction"))
                    return m_arrNewVal["condiction"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("condiction"))
                    m_arrNewVal["condiction"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("condiction", val);
                }
            }
        }
        public enumApprovalResult Result
        {
            get
            {
                if (m_arrNewVal.ContainsKey("result"))
                    return (enumApprovalResult)m_arrNewVal["result"].IntVal;
                else
                    return enumApprovalResult.Accept;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("result"))
                    m_arrNewVal["result"].IntVal = (int)value;
                else
                {
                    CValue val = new CValue();
                    val.IntVal = (int)value;
                    m_arrNewVal.Add("result", val);
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

        public string GetResultString()
        {
            if (Result == enumApprovalResult.Init)
                return "未审批";
            else if (Result == enumApprovalResult.Accept)
                return "已接受";
            else
                return "已拒绝";
        }
    }
}