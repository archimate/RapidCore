// File:    CColorMgr.cs
// Created: 2012/11/28 21:13:41
// Purpose: Definition of Class CColorMgr

using System;
using System.Text;
using System.Collections.Generic;
using System.Reflection;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Store
{
    public class CColorMgr : CBaseObjectMgr
    {

        public CColorMgr()
        {
            TbCode = "SP_Color";
            ClassName = "ErpCoreModel.Store.CColor";
            Assembly assembly = Assembly.LoadFrom(Assembly.GetExecutingAssembly().Location);
            ObjType = assembly.GetType(ClassName);
        }

    }
}