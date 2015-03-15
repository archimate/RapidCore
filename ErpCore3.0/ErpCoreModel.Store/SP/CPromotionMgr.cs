// File:    CPromotionMgr.cs
// Created: 2012/12/1 12:57:25
// Purpose: Definition of Class CPromotionMgr

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Store
{
    public class CPromotionMgr : CBaseObjectMgr
    {

        public CPromotionMgr()
        {
            TbCode = "SP_Promotion";
            ClassName = "ErpCoreModel.Store.CPromotion";
            Assembly assembly = Assembly.LoadFrom(Assembly.GetExecutingAssembly().Location);
            ObjType = assembly.GetType(ClassName);
        }

        public List<CBaseObject> GetOrderList()
        {
            List<CBaseObject> lstObj = GetList();
            var lstP = from obj in lstObj orderby (obj as CPromotion).Idx select obj;
            return lstP.ToList();
        }

        public CPromotion FindByProduct(Guid SP_Product_id)
        {
            List<CBaseObject> lstObj = GetList();
            var lstP = from obj in lstObj where (obj as CPromotion).SP_Product_id == SP_Product_id select obj;
            if (lstP.Count() > 0)
                return lstP.First() as CPromotion;
            else
                return null;
        }
    }
}