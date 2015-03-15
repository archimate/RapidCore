// File:    CStatItemMgr.cs
// Created: 2012/5/9 13:26:26
// Purpose: Definition of Class CStatItemMgr

using System;
using System.Text;
using System.Collections.Generic;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Report
{
    public class CStatItemMgr : CBaseObjectMgr
    {

        public CStatItemMgr()
        {
            TbCode = "RPT_StatItem";
            ClassName = "ErpCoreModel.Report.CStatItem";
        }
        public CStatItem FindByColumn(Guid FW_Table_id, Guid FW_Column_id)
        {
            GetList();
            foreach (CBaseObject obj in m_lstObj)
            {
                CStatItem StatItem = (CStatItem)obj;
                if (StatItem.FW_Table_id == FW_Table_id
                    && StatItem.FW_Column_id == FW_Column_id)
                    return StatItem;
            }
            return null;
        }
    }
}