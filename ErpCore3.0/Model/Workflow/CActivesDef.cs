// File:    CActivesDef.cs
// Created: 2012/7/21 21:38:30
// Purpose: Definition of Class CActivesDef

using System;
using System.Text;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Workflow
{
    //活动类型：启动，中间活动，成功结束，失败结束
    public enum ActivesType {Start,Middle,Success,Failure }
    public class CActivesDef : CBaseObject
    {

        public CActivesDef()
        {
            TbCode = "WF_ActivesDef";
            ClassName = "ErpCoreModel.Workflow.CActivesDef";

        }

        
        public string Name
        {
            get
            {
                if (m_arrNewVal.ContainsKey("name"))
                    return m_arrNewVal["name"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("name"))
                    m_arrNewVal["name"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("name", val);
                }
            }
        }
        public ActivesType WType
        {
            get
            {
                if (m_arrNewVal.ContainsKey("wtype"))
                    return (ActivesType)m_arrNewVal["wtype"].IntVal;
                else
                    return ActivesType.Middle;
            }
            set
            {           
                if (m_arrNewVal.ContainsKey("wtype"))
                    m_arrNewVal["wtype"].IntVal = (int)value;
                else
                {
                    CValue val = new CValue();
                    val.IntVal = (int)value;
                    m_arrNewVal.Add("wtype", val);
                }
            }
        }
        public int Idx
        {
            get
            {
                if (m_arrNewVal.ContainsKey("idx"))
                    return m_arrNewVal["idx"].IntVal;
                else
                    return 0;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("idx"))
                    m_arrNewVal["idx"].IntVal = value;
                else
                {
                    CValue val = new CValue();
                    val.IntVal = value;
                    m_arrNewVal.Add("idx", val);
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
    }
}