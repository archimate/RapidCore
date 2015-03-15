// File:    CViewAccessInRoleMgr.cs
// Created: 2012/7/3 23:07:26
// Purpose: Definition of Class CViewAccessInRoleMgr

using System;
using System.Text;
using System.Collections.Generic;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Base
{
    public class CViewAccessInRoleMgr : CBaseObjectMgr
    {

        public CViewAccessInRoleMgr()
        {
            TbCode = "B_ViewAccessInRole";
            ClassName = "ErpCoreModel.Base.CViewAccessInRole";
        }

        public CViewAccessInRole FindByView(Guid UI_View_id)
        {
            List<CBaseObject> lstObj = GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CViewAccessInRole vair = (CViewAccessInRole)obj;
                if (vair.UI_View_id == UI_View_id)
                    return vair;
            }
            return null;
        }
    }
}