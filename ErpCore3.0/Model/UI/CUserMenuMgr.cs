// File:    CUserMenuMgr.cs
// Created: 2012-08-13 23:16:13
// Purpose: Definition of Class CUserMenuMgr

using System;
using System.Text;
using System.Collections.Generic;
using ErpCoreModel.Framework;

namespace ErpCoreModel.UI
{
    public class CUserMenuMgr : CBaseObjectMgr
    {

        public CUserMenuMgr()
        {
            TbCode = "UI_UserMenu";
            ClassName = "ErpCoreModel.UI.CUserMenu";
        }
        public CUserMenu FindByMenu(Guid UI_Menu_id, Guid UI_DesktopGroup_id)
        {
            List<CBaseObject> lstObj = GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CUserMenu UserMenu = (CUserMenu)obj;
                if (UserMenu.UI_Menu_id == UI_Menu_id
                    && UserMenu.UI_DesktopGroup_id==UI_DesktopGroup_id)
                    return UserMenu;
            }
            return null;
        }

        public void RemoveByDesktopGroupId(Guid UI_DesktopGroup_id)
        {
            List<CBaseObject> lstDel = new List<CBaseObject>();
            List<CBaseObject> lstObj = GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CUserMenu UserMenu = (CUserMenu)obj;
                if (UserMenu.UI_DesktopGroup_id == UI_DesktopGroup_id)
                    lstDel.Add( UserMenu);
            }
            foreach (CBaseObject obj in lstDel)
                Delete(obj);
        }
    }
}