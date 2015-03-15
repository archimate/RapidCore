// File:    CProductInTypeMgr.cs
// Created: 2012/11/28 21:13:41
// Purpose: Definition of Class CProductInTypeMgr

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Store
{
    public class CProductInTypeMgr : CBaseObjectMgr
    {

        public CProductInTypeMgr()
        {
            TbCode = "SP_ProductInType";
            ClassName = "ErpCoreModel.Store.CProductInType";
            Assembly assembly = Assembly.LoadFrom(Assembly.GetExecutingAssembly().Location);
            ObjType = assembly.GetType(ClassName);
        }

        public List<CBaseObject> GetList(Guid SP_ProductType_id)
        {
            List<CBaseObject> lstObj = GetList();
            var lstPIT = from obj in lstObj where (obj as CProductInType).SP_ProductType_id == SP_ProductType_id orderby (obj as CProductInType).Idx select obj;
            return lstPIT.ToList();
        }
    }
}