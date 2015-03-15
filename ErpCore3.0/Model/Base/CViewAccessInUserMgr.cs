// File:    CViewAccessInUserMgr.cs
// Created: 2012/7/3 23:07:26
// Purpose: Definition of Class CViewAccessInUserMgr

using System;
using System.Text;
using System.Collections.Generic;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Base
{
    public class CViewAccessInUserMgr : CBaseObjectMgr
    {

        public CViewAccessInUserMgr()
        {
            TbCode = "B_ViewAccessInUser";
            ClassName = "ErpCoreModel.Base.CViewAccessInUser";
        }

        public CViewAccessInUser FindByView(Guid UI_View_id)
        {
            List<CBaseObject> lstObj = GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CViewAccessInUser vaiu = (CViewAccessInUser)obj;
                if (vaiu.UI_View_id == UI_View_id)
                    return vaiu;
            }
            return null;
        }
    }
}