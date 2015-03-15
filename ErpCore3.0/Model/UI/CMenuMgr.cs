// File:    CMenuMgr.cs
// Created: 2012-08-13 23:16:13
// Purpose: Definition of Class CMenuMgr

using System;
using System.Text;
using System.Collections.Generic;
using ErpCoreModel.Framework;

namespace ErpCoreModel.UI
{
    public class CMenuMgr : CBaseObjectMgr
    {

        public CMenuMgr()
        {
            TbCode = "UI_Menu";
            ClassName = "ErpCoreModel.UI.CMenu";
        }

        public override bool Delete(CBaseObject obj, bool bSave)
        {
            //É¾³ý×ÓÄ¿Â¼
            List<CBaseObject> lstObj2 = GetList();
            foreach (CBaseObject obj2 in lstObj2)
            {
                CMenu menu = (CMenu)obj2;
                if (menu.Parent_id == obj.Id)
                {
                    Delete(obj2, bSave);
                }
            }
            return base.Delete(obj, bSave);
        }

        public CMenu FindByName(string sName, Guid Parent_id)
        {
            List<CBaseObject> lstObj = GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CMenu menu = (CMenu)obj;
                if (menu.Name.Equals(sName, StringComparison.OrdinalIgnoreCase)
                    && menu.Parent_id == Parent_id)
                    return menu;
            }
            return null;
        }
    }
}