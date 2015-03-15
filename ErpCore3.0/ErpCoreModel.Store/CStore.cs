// File:    CStore.cs
// Author:  甘孝俭
// email:   ganxiaojian@hotmail.com 
// QQ:      154986287
// http://www.8088net.com
// 协议声明：本软件为开源系统，遵循国际开源组织协议。任何单位或个人可以使用或修改本软件源码，
//          可以用于作为非商业或商业用途，但由于使用本源码所引起的一切后果与作者无关。
//          未经作者许可，禁止任何企业或个人直接出售本源码或者把本软件作为独立的功能进行销售活动，
//          作者将保留追究责任的权利。
// Created: 2012/11/28 21:13:40
// Purpose: Definition of Class CStore

using System;
using System.Collections.Generic;
using System.Text;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Store
{
    public class CStore : CBaseObject
    {
        private CCustomerMgr customerMgr = null;
        private COrderMgr orderMgr = null;
        private CUnitMgr unitMgr = null;
        private CSpecificationMgr specificationMgr = null;
        private CColorMgr colorMgr = null;
        private CProductMgr productMgr = null;
        private CProductCategoryMgr productCategoryMgr = null;
        private CTypeInCategoryMgr typeInCategoryMgr = null;
        private CProductTypeMgr productTypeMgr = null;
        private CProductInTypeMgr productInTypeMgr = null;
        private CPromotionMgr promotionMgr = null;

        public CPromotionMgr PromotionMgr
        {
            get
            {
                if (promotionMgr == null)
                {
                    promotionMgr = new CPromotionMgr();
                    promotionMgr.Ctx = Ctx;
                    promotionMgr.Load("", false);
                    Ctx.AddBaseObjectMgrCache(promotionMgr.TbCode, Guid.Empty, promotionMgr);
                }
                return promotionMgr;
            }
            set
            {
                this.promotionMgr = value;
            }
        }
        public CProductInTypeMgr ProductInTypeMgr
        {
            get
            {
                if (productInTypeMgr == null)
                {
                    productInTypeMgr = new CProductInTypeMgr();
                    productInTypeMgr.Ctx = Ctx;
                    productInTypeMgr.Load("", false);
                    Ctx.AddBaseObjectMgrCache(productInTypeMgr.TbCode, Guid.Empty, productInTypeMgr);
                }
                return productInTypeMgr;
            }
            set
            {
                this.productInTypeMgr = value;
            }
        }
        public CProductTypeMgr ProductTypeMgr
        {
            get
            {
                if (productTypeMgr == null)
                {
                    productTypeMgr = new CProductTypeMgr();
                    productTypeMgr.Ctx = Ctx;
                    productTypeMgr.Load("", false);
                    Ctx.AddBaseObjectMgrCache(productTypeMgr.TbCode, Guid.Empty, productTypeMgr);
                }
                return productTypeMgr;
            }
            set
            {
                this.productTypeMgr = value;
            }
        }
        public CTypeInCategoryMgr TypeInCategoryMgr
        {
            get
            {
                if (typeInCategoryMgr == null)
                {
                    typeInCategoryMgr = new CTypeInCategoryMgr();
                    typeInCategoryMgr.Ctx = Ctx;
                    typeInCategoryMgr.Load("", false);
                    Ctx.AddBaseObjectMgrCache(typeInCategoryMgr.TbCode, Guid.Empty, typeInCategoryMgr);
                }
                return typeInCategoryMgr;
            }
            set
            {
                this.typeInCategoryMgr = value;
            }
        }
        public CProductCategoryMgr ProductCategoryMgr
        {
            get
            {
                if (productCategoryMgr == null)
                {
                    productCategoryMgr = new CProductCategoryMgr();
                    productCategoryMgr.Ctx = Ctx;
                    productCategoryMgr.Load("", false);
                    Ctx.AddBaseObjectMgrCache(productCategoryMgr.TbCode, Guid.Empty, productCategoryMgr);
                }
                return productCategoryMgr;
            }
            set
            {
                this.productCategoryMgr = value;
            }
        }
        public CProductMgr ProductMgr
        {
            get
            {
                if (productMgr == null)
                {
                    productMgr = new CProductMgr();
                    productMgr.Ctx = Ctx;
                    productMgr.Load("", false);
                    Ctx.AddBaseObjectMgrCache(productMgr.TbCode, Guid.Empty, productMgr);
                }
                return productMgr;
            }
            set
            {
                this.productMgr = value;
            }
        }
        public CColorMgr ColorMgr
        {
            get
            {
                if (colorMgr == null)
                {
                    colorMgr = new CColorMgr();
                    colorMgr.Ctx = Ctx;
                    colorMgr.Load("", false);
                    Ctx.AddBaseObjectMgrCache(colorMgr.TbCode, Guid.Empty, colorMgr);
                }
                return colorMgr;
            }
            set
            {
                this.colorMgr = value;
            }
        }
        public CSpecificationMgr SpecificationMgr
        {
            get
            {
                if (specificationMgr == null)
                {
                    specificationMgr = new CSpecificationMgr();
                    specificationMgr.Ctx = Ctx;
                    specificationMgr.Load("", false);
                    Ctx.AddBaseObjectMgrCache(specificationMgr.TbCode, Guid.Empty, specificationMgr);
                }
                return specificationMgr;
            }
            set
            {
                this.specificationMgr = value;
            }
        }
        public CUnitMgr UnitMgr
        {
            get
            {
                if (unitMgr == null)
                {
                    unitMgr = new CUnitMgr();
                    unitMgr.Ctx = Ctx;
                    unitMgr.Load("", false);
                    Ctx.AddBaseObjectMgrCache(unitMgr.TbCode, Guid.Empty, unitMgr);
                }
                return unitMgr;
            }
            set
            {
                this.unitMgr = value;
            }
        }
        public COrderMgr OrderMgr
        {
            get
            {
                if (orderMgr == null)
                {
                    orderMgr = new COrderMgr();
                    orderMgr.Ctx = Ctx;
                    orderMgr.Load("", false);
                    Ctx.AddBaseObjectMgrCache(orderMgr.TbCode, Guid.Empty, orderMgr);
                }
                return orderMgr;
            }
            set
            {
                this.orderMgr = value;
            }
        }
        public CCustomerMgr CustomerMgr
        {
            get
            {
                if (customerMgr == null)
                {
                    customerMgr = new CCustomerMgr();
                    customerMgr.Ctx = Ctx;
                    customerMgr.Load("", false);
                    Ctx.AddBaseObjectMgrCache(customerMgr.TbCode, Guid.Empty, customerMgr);
                }
                return customerMgr;
            }
            set
            {
                this.customerMgr = value;
            }
        }
    }
}
