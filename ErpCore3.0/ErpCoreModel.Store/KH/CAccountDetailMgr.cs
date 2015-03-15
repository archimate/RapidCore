// File:    CAccountDetailMgr.cs
// Created: 2012/11/28 21:13:40
// Purpose: Definition of Class CAccountDetailMgr

using System;
using System.Text;
using System.Collections.Generic;
using System.Reflection;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Store
{
    public class CAccountDetailMgr : CBaseObjectMgr
    {

        public CAccountDetailMgr()
        {
            TbCode = "KH_AccountDetail";
            ClassName = "ErpCoreModel.Store.CAccountDetail";
            Assembly assembly = Assembly.LoadFrom(Assembly.GetExecutingAssembly().Location);
            ObjType = assembly.GetType(ClassName);
        }

    }
}