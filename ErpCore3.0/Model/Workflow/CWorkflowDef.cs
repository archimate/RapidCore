// File:    CWorkflowDef.cs
// Created: 2012/7/21 21:38:30
// Purpose: Definition of Class CWorkflowDef

using System;
using System.Text;
using System.Collections.Generic;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Workflow
{
    
    public class CWorkflowDef : CBaseObject
    {

        public CWorkflowDef()
        {
            TbCode = "WF_WorkflowDef";
            ClassName = "ErpCoreModel.Workflow.CWorkflowDef";

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
        public Guid WF_WorkflowCatalog_id
        {
            get
            {
                if (m_arrNewVal.ContainsKey("wf_workflowcatalog_id"))
                    return m_arrNewVal["wf_workflowcatalog_id"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {        
                if (m_arrNewVal.ContainsKey("wf_workflowcatalog_id"))
                    m_arrNewVal["wf_workflowcatalog_id"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("wf_workflowcatalog_id", val);
                }
            }
        }
        public Guid FW_Table_id
        {
            get
            {
                if (m_arrNewVal.ContainsKey("fw_table_id"))
                    return m_arrNewVal["fw_table_id"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("fw_table_id"))
                    m_arrNewVal["fw_table_id"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("fw_table_id", val);
                }
            }
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

        public CActivesDefMgr ActivesDefMgr
        {
            get
            {
                return (CActivesDefMgr)this.GetSubObjectMgr("WF_ActivesDef", typeof(CActivesDefMgr));
            }
            set
            {
                this.SetSubObjectMgr("WF_ActivesDef", value);
            }
        }

        public CLinkMgr LinkMgr
        {
            get
            {
                return (CLinkMgr)this.GetSubObjectMgr("WF_Link", typeof(CLinkMgr));
            }
            set
            {
                this.SetSubObjectMgr("WF_Link", value);
            }
        }
    }
}