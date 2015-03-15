// File:    CProductImgMgr.cs
// Created: 2012/11/28 21:13:41
// Purpose: Definition of Class CProductImgMgr

using System;
using System.Text;
using System.Collections.Generic;
using System.Reflection;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Store
{
    public class CProductImgMgr : CBaseObjectMgr
    {

        public CProductImgMgr()
        {
            TbCode = "SP_ProductImg";
            ClassName = "ErpCoreModel.Store.CProductImg";
            Assembly assembly = Assembly.LoadFrom(Assembly.GetExecutingAssembly().Location);
            ObjType = assembly.GetType(ClassName);
        }

    }
}