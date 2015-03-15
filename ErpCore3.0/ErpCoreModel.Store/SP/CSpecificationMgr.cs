// File:    CSpecificationMgr.cs
// Created: 2012/11/28 21:13:41
// Purpose: Definition of Class CSpecificationMgr

using System;
using System.Text;
using System.Collections.Generic;
using System.Reflection;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Store
{
    public class CSpecificationMgr : CBaseObjectMgr
    {

        public CSpecificationMgr()
        {
            TbCode = "SP_Specification";
            ClassName = "ErpCoreModel.Store.CSpecification";
            Assembly assembly = Assembly.LoadFrom(Assembly.GetExecutingAssembly().Location);
            ObjType = assembly.GetType(ClassName);
        }

    }
}