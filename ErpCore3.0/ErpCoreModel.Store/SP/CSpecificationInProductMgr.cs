// File:    CSpecificationInProductMgr.cs
// Created: 2012/11/28 21:13:41
// Purpose: Definition of Class CSpecificationInProductMgr

using System;
using System.Text;
using System.Collections.Generic;
using System.Reflection;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Store
{
    public class CSpecificationInProductMgr : CBaseObjectMgr
    {

        public CSpecificationInProductMgr()
        {
            TbCode = "SP_SpecificationInProduct";
            ClassName = "ErpCoreModel.Store.CSpecificationInProduct";
            Assembly assembly = Assembly.LoadFrom(Assembly.GetExecutingAssembly().Location);
            ObjType = assembly.GetType(ClassName);
        }

    }
}