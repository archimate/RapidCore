// File:    CDesktopGroupMgr.cs
// Created: 2013/1/1 11:25:49
// Purpose: Definition of Class CDesktopGroupMgr

using System;
using System.Text;
using System.Collections.Generic;
using System.Reflection;
using ErpCoreModel.Framework;

namespace ErpCoreModel.UI
{
    public class CDesktopGroupMgr : CBaseObjectMgr
    {

        public CDesktopGroupMgr()
        {
            TbCode = "UI_DesktopGroup";
            ClassName = "ErpCoreModel.UI.CDesktopGroup";
        }

        public CDesktopGroup FindByName(string sName)
        {
            List<CBaseObject> lstObj = GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CDesktopGroup group = (CDesktopGroup)obj;
                if (group.Name.Equals(sName, StringComparison.OrdinalIgnoreCase))
                    return group;
            }
            return null;
        }
    }
}