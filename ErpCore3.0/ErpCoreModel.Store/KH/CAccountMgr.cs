// File:    CAccountMgr.cs
// Created: 2012/11/28 21:13:40
// Purpose: Definition of Class CAccountMgr

using System;
using System.Text;
using System.Collections.Generic;
using System.Reflection;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Store
{
    public class CAccountMgr : CBaseObjectMgr
    {

        public CAccountMgr()
        {
            TbCode = "KH_Account";
            ClassName = "ErpCoreModel.Store.CAccount";
            Assembly assembly = Assembly.LoadFrom(Assembly.GetExecutingAssembly().Location);
            ObjType = assembly.GetType(ClassName);
        }

    }
}