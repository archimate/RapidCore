// File:    CReportCatalog.cs
// Created: 2012/5/9 13:26:26
// Purpose: Definition of Class CReportCatalog

using System;
using System.Text;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Report
{
    
    public class CReportCatalog : CBaseObject
    {

        public CReportCatalog()
        {
            TbCode = "RPT_ReportCatalog";
            ClassName = "ErpCoreModel.Report.CReportCatalog";

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
    }
}