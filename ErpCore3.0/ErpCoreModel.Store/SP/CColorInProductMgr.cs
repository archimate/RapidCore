// File:    CColorInProductMgr.cs
// Created: 2012/11/28 21:13:41
// Purpose: Definition of Class CColorInProductMgr

using System;
using System.Text;
using System.Collections.Generic;
using System.Reflection;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Store
{
    public class CColorInProductMgr : CBaseObjectMgr
    {

        public CColorInProductMgr()
        {
            TbCode = "SP_ColorInProduct";
            ClassName = "ErpCoreModel.Store.CColorInProduct";
            Assembly assembly = Assembly.LoadFrom(Assembly.GetExecutingAssembly().Location);
            ObjType = assembly.GetType(ClassName);
        }

    }
}