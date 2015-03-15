// File:    CPriceMgr.cs
// Created: 2012/11/28 21:13:41
// Purpose: Definition of Class CPriceMgr

using System;
using System.Text;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Store
{
    public class CPriceMgr : CBaseObjectMgr
    {

        public CPriceMgr()
        {
            TbCode = "SP_Price";
            ClassName = "ErpCoreModel.Store.CPrice";
            Assembly assembly = Assembly.LoadFrom(Assembly.GetExecutingAssembly().Location);
            ObjType = assembly.GetType(ClassName);
        }

        public List<CBaseObject> FindByType(PriceType type)
        {
            List<CBaseObject> lstObj = GetList();
            var lstP = from obj in lstObj where (obj as CPrice).Type == type orderby (obj as CPrice).Idx select obj;
            return lstP.ToList();
        }
    }
}