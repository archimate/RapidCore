// File:    COrderMgr.cs
// Created: 2012/11/28 21:13:40
// Purpose: Definition of Class COrderMgr

using System;
using System.Text;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Store
{
    public class COrderMgr : CBaseObjectMgr
    {

        public COrderMgr()
        {
            TbCode = "DD_Order";
            ClassName = "ErpCoreModel.Store.COrder";
            Assembly assembly = Assembly.LoadFrom(Assembly.GetExecutingAssembly().Location);
            ObjType = assembly.GetType(ClassName);
        }

        public COrder FindByCode(string sCode)
        {
            List<CBaseObject> lstObj = GetList();
            var varObj = from obj in lstObj where (obj as COrder).Code == sCode select obj;
            if (varObj.Count() > 0)
                return varObj.First() as COrder;
            else
                return null;
        }
    }
}