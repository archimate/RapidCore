// File:    CUnitMgr.cs
// Created: 2012/11/28 21:13:41
// Purpose: Definition of Class CUnitMgr

using System;
using System.Text;
using System.Collections.Generic;
using System.Reflection;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Store
{
    public class CUnitMgr : CBaseObjectMgr
    {

        public CUnitMgr()
        {
            TbCode = "SP_Unit";
            ClassName = "ErpCoreModel.Store.CUnit";
            Assembly assembly = Assembly.LoadFrom(Assembly.GetExecutingAssembly().Location);
            ObjType = assembly.GetType(ClassName);
        }

    }
}