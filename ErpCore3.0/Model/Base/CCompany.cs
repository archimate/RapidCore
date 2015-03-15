// File:    CCompany.cs
// Created: 2012/3/22 12:31:00
// Purpose: Definition of Class CCompany

using System;
using System.Text;
using System.Collections.Generic;
using ErpCoreModel.Framework;
using ErpCoreModel.Report;
using ErpCoreModel.Workflow;

namespace ErpCoreModel.Base
{
    //注册状态：注册用户，付费用户，过期用户
    public enum enumRegState { Reg, Charge, OutTime }
    //企业类型：总部，分店（分公司）；加盟店（子公司）；仓库
    public enum enumCompanyType { Headquarters, Branch, Subsidiary, Warehouse }
    public class CCompany : CBaseObject
    {
        public CCompany()
        {
            TbCode = "B_Company";
            ClassName = "ErpCoreModel.Base.CCompany";

        }

        public Guid Parent_id
        {
            get
            {
                if (m_arrNewVal.ContainsKey("parent_id"))
                    return m_arrNewVal["parent_id"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("parent_id"))
                    m_arrNewVal["parent_id"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("parent_id", val);
                }
            }
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
        public string Addr
        {
            get
            {
                if (m_arrNewVal.ContainsKey("addr"))
                    return m_arrNewVal["addr"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("addr"))
                    m_arrNewVal["addr"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("addr", val);
                }
            }
        }
        public string Zipcode
        {
            get
            {
                if (m_arrNewVal.ContainsKey("zipcode"))
                    return m_arrNewVal["zipcode"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("zipcode"))
                    m_arrNewVal["zipcode"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("zipcode", val);
                }
            }
        }
        public string Tel
        {
            get
            {
                if (m_arrNewVal.ContainsKey("tel"))
                    return m_arrNewVal["tel"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("tel"))
                    m_arrNewVal["tel"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("tel", val);
                }
            }
        }
        public string Contact
        {
            get
            {
                if (m_arrNewVal.ContainsKey("contact"))
                    return m_arrNewVal["contact"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("contact"))
                    m_arrNewVal["contact"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("contact", val);
                }
            }
        }
        public string Email
        {
            get
            {
                if (m_arrNewVal.ContainsKey("email"))
                    return m_arrNewVal["email"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("email"))
                    m_arrNewVal["email"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("email", val);
                }
            }
        }
        public enumCompanyType SType
        {
            get
            {
                if (m_arrNewVal.ContainsKey("stype"))
                    return (enumCompanyType)m_arrNewVal["stype"].IntVal;
                else
                    return enumCompanyType.Headquarters;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("stype"))
                    m_arrNewVal["stype"].IntVal = (int)value;
                else
                {
                    CValue val = new CValue();
                    val.IntVal = (int)value;
                    m_arrNewVal.Add("stype", val);
                }
            }
        }
        public enumRegState RegState
        {
            get
            {
                if (m_arrNewVal.ContainsKey("regstate"))
                    return (enumRegState)m_arrNewVal["regstate"].IntVal;
                else
                    return enumRegState.Reg;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("regstate"))
                    m_arrNewVal["regstate"].IntVal = (int)value;
                else
                {
                    CValue val = new CValue();
                    val.IntVal = (int)value;
                    m_arrNewVal.Add("regstate", val);
                }
            }
        }
        public DateTime ChargeDate
        {
            get
            {
                if (m_arrNewVal.ContainsKey("chargedate"))
                    return m_arrNewVal["chargedate"].DatetimeVal;
                else
                    return DateTime.Now;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("chargedate"))
                    m_arrNewVal["chargedate"].DatetimeVal = value;
                else
                {
                    CValue val = new CValue();
                    val.DatetimeVal = value;
                    m_arrNewVal.Add("chargedate", val);
                }
            }
        }

        public COrgMgr OrgMgr
        {
            get
            {
                return (COrgMgr)this.GetSubObjectMgr("B_Org", typeof(COrgMgr));
            }
            set
            {
                this.SetSubObjectMgr("B_Org", value);
            }
        }
        public CRoleMgr RoleMgr
        {
            get
            {
                return (CRoleMgr)this.GetSubObjectMgr("B_Role", typeof(CRoleMgr));
            }
            set
            {
                this.SetSubObjectMgr("B_Role", value);
            }
        }

        public CReportCatalogMgr ReportCatalogMgr
        {
            get
            {
                return (CReportCatalogMgr)this.GetSubObjectMgr("RPT_ReportCatalog", typeof(CReportCatalogMgr));
            }
            set
            {
                this.SetSubObjectMgr("RPT_ReportCatalog", value);
            }
        }
        public CReportMgr ReportMgr
        {
            get
            {
                return (CReportMgr)this.GetSubObjectMgr("RPT_Report", typeof(CReportMgr));
            }
            set
            {
                this.SetSubObjectMgr("RPT_Report", value);
            }
        }

        public CWorkflowCatalogMgr WorkflowCatalogMgr
        {
            get
            {
                return (CWorkflowCatalogMgr)this.GetSubObjectMgr("WF_WorkflowCatalog", typeof(CWorkflowCatalogMgr));
            }
            set
            {
                this.SetSubObjectMgr("WF_WorkflowCatalog", value);
            }
        }
        public CWorkflowDefMgr WorkflowDefMgr
        {
            get
            {
                return (CWorkflowDefMgr)this.GetSubObjectMgr("WF_WorkflowDef", typeof(CWorkflowDefMgr));
            }
            set
            {
                this.SetSubObjectMgr("WF_WorkflowDef", value);
            }
        }

    }
}