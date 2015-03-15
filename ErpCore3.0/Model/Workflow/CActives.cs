// File:    CActives.cs
// Created: 2012/7/21 21:38:30
// Purpose: Definition of Class CActives

using System;
using System.Text;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Workflow
{
    
    public class CActives : CBaseObject
    {

        public CActives()
        {
            TbCode = "WF_Actives";
            ClassName = "ErpCoreModel.Workflow.CActives";

        }

        
        public Guid WF_Workflow_id
        {
            get
            {
                if (m_arrNewVal.ContainsKey("wf_workflow_id"))
                    return m_arrNewVal["wf_workflow_id"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {        
                if (m_arrNewVal.ContainsKey("wf_workflow_id"))
                    m_arrNewVal["wf_workflow_id"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("wf_workflow_id", val);
                }
            }
        }
        public Guid WF_ActivesDef_id
        {
            get
            {
                if (m_arrNewVal.ContainsKey("wf_activesdef_id"))
                    return m_arrNewVal["wf_activesdef_id"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {        
                if (m_arrNewVal.ContainsKey("wf_activesdef_id"))
                    m_arrNewVal["wf_activesdef_id"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("wf_activesdef_id", val);
                }
            }
        }
        public string Comment
        {
            get
            {
                if (m_arrNewVal.ContainsKey("comment"))
                    return m_arrNewVal["comment"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("comment"))
                    m_arrNewVal["comment"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("comment", val);
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
        public Guid B_User_id
        {
            get
            {
                if (m_arrNewVal.ContainsKey("b_user_id"))
                    return m_arrNewVal["b_user_id"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("b_user_id"))
                    m_arrNewVal["b_user_id"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("b_user_id", val);
                }
            }
        }
        public string AType
        {
            get
            {
                if (m_arrNewVal.ContainsKey("atype"))
                    return m_arrNewVal["atype"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("atype"))
                    m_arrNewVal["atype"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("atype", val);
                }
            }
        }
        public Guid B_Role_id
        {
            get
            {
                if (m_arrNewVal.ContainsKey("b_role_id"))
                    return m_arrNewVal["b_role_id"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("b_role_id"))
                    m_arrNewVal["b_role_id"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("b_role_id", val);
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