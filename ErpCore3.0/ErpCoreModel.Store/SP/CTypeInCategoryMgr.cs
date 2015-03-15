// File:    CTypeInCategoryMgr.cs
// Created: 2012/12/1 10:31:54
// Purpose: Definition of Class CTypeInCategoryMgr

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Store
{
    public class CTypeInCategoryMgr : CBaseObjectMgr
    {

        public CTypeInCategoryMgr()
        {
            TbCode = "SP_TypeInCategory";
            ClassName = "ErpCoreModel.Store.CTypeInCategory";
            Assembly assembly = Assembly.LoadFrom(Assembly.GetExecutingAssembly().Location);
            ObjType = assembly.GetType(ClassName);
        }

        public List<CBaseObject> GetList(Guid SP_ProductCategory_id)
        {
            List<CBaseObject> lstObj = GetList();
            var lstTIC = from obj in lstObj where (obj as CTypeInCategory).SP_ProductCategory_id == SP_ProductCategory_id orderby (obj as CTypeInCategory).Idx select obj;
            return lstTIC.ToList();
        }
    }
}