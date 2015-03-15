// File:    CRoleMenuMgr.cs
// Created: 2012-08-15 08:58:33
// Purpose: Definition of Class CRoleMenuMgr

using System;
using System.Text;
using System.Collections.Generic;
using ErpCoreModel.Framework;

namespace ErpCoreModel.UI
{
    public class CRoleMenuMgr : CBaseObjectMgr
    {

        public CRoleMenuMgr()
        {
            TbCode = "UI_RoleMenu";
            ClassName = "ErpCoreModel.UI.CRoleMenu";
        }

        public CRoleMenu FindByMenu(Guid UI_Menu_id, Guid UI_DesktopGroup_id)
        {
            List<CBaseObject> lstObj = GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CRoleMenu RoleMenu = (CRoleMenu)obj;
                if (RoleMenu.UI_Menu_id == UI_Menu_id
                    && RoleMenu.UI_DesktopGroup_id == UI_DesktopGroup_id)
                    return RoleMenu;
            }
            return null;
        }

        public void RemoveByDesktopGroupId(Guid UI_DesktopGroup_id)
        {
            List<CBaseObject> lstDel = new List<CBaseObject>();
            List<CBaseObject> lstObj = GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CRoleMenu RoleMenu = (CRoleMenu)obj;
                if (RoleMenu.UI_DesktopGroup_id == UI_DesktopGroup_id)
                    lstDel.Add(RoleMenu);
            }
            foreach (CBaseObject obj in lstDel)
                Delete(obj);
        }
    }
}