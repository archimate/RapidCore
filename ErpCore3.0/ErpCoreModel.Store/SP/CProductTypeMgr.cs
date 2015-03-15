// File:    CProductTypeMgr.cs
// Created: 2012/11/28 21:13:41
// Purpose: Definition of Class CProductTypeMgr

using System;
using System.Text;
using System.Collections.Generic;
using System.Reflection;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Store
{
    public class CProductTypeMgr : CBaseObjectMgr
    {

        public CProductTypeMgr()
        {
            TbCode = "SP_ProductType";
            ClassName = "ErpCoreModel.Store.CProductType";
            Assembly assembly = Assembly.LoadFrom(Assembly.GetExecutingAssembly().Location);
            ObjType = assembly.GetType(ClassName);
        }

    }
}