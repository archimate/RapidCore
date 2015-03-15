// File:    CProductCategoryMgr.cs
// Created: 2012/11/28 21:13:41
// Purpose: Definition of Class CProductCategoryMgr

using System;
using System.Text;
using System.Collections.Generic;
using System.Reflection;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Store
{
    public class CProductCategoryMgr : CBaseObjectMgr
    {

        public CProductCategoryMgr()
        {
            TbCode = "SP_ProductCategory";
            ClassName = "ErpCoreModel.Store.CProductCategory";
            Assembly assembly = Assembly.LoadFrom(Assembly.GetExecutingAssembly().Location);
            ObjType = assembly.GetType(ClassName);
        }

    }
}