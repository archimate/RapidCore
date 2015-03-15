// File:    CSystemAccessInUserMgr.cs
// Created: 2012/7/3 23:07:26
// Purpose: Definition of Class CSystemAccessInUserMgr

using System;
using System.Text;
using System.Collections.Generic;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Base
{
    public class CSystemAccessInUserMgr : CBaseObjectMgr
    {

        public CSystemAccessInUserMgr()
        {
            TbCode = "B_SystemAccessInUser";
            ClassName = "ErpCoreModel.Base.CSystemAccessInUser";
        }

        public CSystemAccessInUser FindBySystem(Guid S_System_id)
        {
            List<CBaseObject> lstObj = GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CSystemAccessInUser saiu = (CSystemAccessInUser)obj;
                if (saiu.S_System_id == S_System_id)
                    return saiu;
            }
            return null;
        }
    }
}