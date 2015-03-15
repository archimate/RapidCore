// File:    CReportMgr.cs
// Created: 2012/5/9 13:26:26
// Purpose: Definition of Class CReportMgr

using System;
using System.Text;
using System.Collections.Generic;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Report
{
    public class CReportMgr : CBaseObjectMgr
    {

        public CReportMgr()
        {
            TbCode = "RPT_Report";
            ClassName = "ErpCoreModel.Report.CReport";
        }
    }
}