// File:    CProductMgr.cs
// Created: 2012/11/28 21:13:41
// Purpose: Definition of Class CProductMgr

using System;
using System.Text;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using ErpCoreModel.Framework;


namespace ErpCoreModel.Store
{
    public class CProductMgr : CBaseObjectMgr
    {

        public CProductMgr()
        {
            TbCode = "SP_Product";
            ClassName = "ErpCoreModel.Store.CProduct";
            Assembly assembly = Assembly.LoadFrom(Assembly.GetExecutingAssembly().Location);
            ObjType = assembly.GetType(ClassName);
        }

        public CProduct FindByCode(string sCode)
        {
            List<CBaseObject> lstObj = GetList();
            var varObj = from obj in lstObj where (obj as CProduct).Code == sCode select obj;
            if (varObj.Count() > 0)
                return varObj.First() as CProduct;
            else
                return null;
        }
    }
}