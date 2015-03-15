// File:    CSystemAccessInRoleMgr.cs
// Created: 2012/7/3 23:07:26
// Purpose: Definition of Class CSystemAccessInRoleMgr

using System;
using System.Text;
using System.Collections.Generic;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Base
{
    public class CSystemAccessInRoleMgr : CBaseObjectMgr
    {

        public CSystemAccessInRoleMgr()
        {
            TbCode = "B_SystemAccessInRole";
            ClassName = "ErpCoreModel.Base.CSystemAccessInRole";
        }

        public CSystemAccessInRole FindBySystem(Guid S_System_id)
        {
            List<CBaseObject> lstObj = GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CSystemAccessInRole sair = (CSystemAccessInRole)obj;
                if (sair.S_System_id == S_System_id)
                    return sair;
            }
            return null;
        }
    }
}