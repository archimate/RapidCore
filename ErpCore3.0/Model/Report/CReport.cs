// File:    CReport.cs
// Created: 2012/5/9 13:26:26
// Purpose: Definition of Class CReport

using System;
using System.Text;
using System.Collections.Generic;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Report
{
    public enum enumAlign { Center,Left,Right}
    
    public class CReport : CBaseObject
    {

        public CReport()
        {
            TbCode = "RPT_Report";
            ClassName = "ErpCoreModel.Report.CReport";

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
        public string Filter
        {
            get
            {
                if (m_arrNewVal.ContainsKey("filter"))
                    return m_arrNewVal["filter"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("filter"))
                    m_arrNewVal["filter"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("filter", val);
                }
            }
        }

        public Guid RPT_ReportCatalog_id
        {
            get
            {
                if (m_arrNewVal.ContainsKey("rpt_reportcatalog_id"))
                    return m_arrNewVal["rpt_reportcatalog_id"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {        
                if (m_arrNewVal.ContainsKey("rpt_reportcatalog_id"))
                    m_arrNewVal["rpt_reportcatalog_id"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("rpt_reportcatalog_id", val);
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


        public CStatItemMgr StatItemMgr
        {
            get
            {
                return (CStatItemMgr)this.GetSubObjectMgr("RPT_StatItem", typeof(CStatItemMgr));
            }
            set
            {
                this.SetSubObjectMgr("RPT_StatItem", value);
            }
        }

    }
}