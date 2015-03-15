// File:    CReportCatalogMgr.cs
// Created: 2012/5/9 13:26:26
// Purpose: Definition of Class CReportCatalogMgr

using System;
using System.Text;
using System.Collections.Generic;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Report
{
    public class CReportCatalogMgr : CBaseObjectMgr
    {

        public CReportCatalogMgr()
        {
            TbCode = "RPT_ReportCatalog";
            ClassName = "ErpCoreModel.Report.CReportCatalog";
        }

    }
}