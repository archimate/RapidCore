// File:    CViewDetailMgr.cs
// Created: 2012-08-28 13:20:30
// Purpose: Definition of Class CViewDetailMgr

using System;
using System.Text;
using System.Collections.Generic;
using ErpCoreModel.Framework;

namespace ErpCoreModel.UI
{
    public class CViewDetailMgr : CBaseObjectMgr
    {

        public CViewDetailMgr()
        {
            TbCode = "UI_ViewDetail";
            ClassName = "ErpCoreModel.UI.CViewDetail";
        }

        public CViewDetail FindByTable(Guid FW_Table_id)
        {
            List<CBaseObject> lstObj = GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CViewDetail vd = (CViewDetail)obj;
                if (vd.FW_Table_id == FW_Table_id)
                    return vd;
            }
            return null;
        }

    }
}