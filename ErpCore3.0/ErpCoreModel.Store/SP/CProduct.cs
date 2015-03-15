// File:    CProduct.cs
// Author:  甘孝俭
// email:   ganxiaojian@hotmail.com 
// QQ:      154986287
// http://www.8088net.com
// 协议声明：本软件为开源系统，遵循国际开源组织协议。任何单位或个人可以使用或修改本软件源码，
//          可以用于作为非商业或商业用途，但由于使用本源码所引起的一切后果与作者无关。
//          未经作者许可，禁止任何企业或个人直接出售本源码或者把本软件作为独立的功能进行销售活动，
//          作者将保留追究责任的权利。
// Created: 2012/11/28 21:13:41
// Purpose: Definition of Class CProduct

using System;
using System.Text;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Store
{
    
    public class CProduct : CBaseObject
    {

        public CProduct()
        {
            TbCode = "SP_Product";
            ClassName = "ErpCoreModel.Store.CProduct";

        }

        
        public string Name
        {
            get
            {
                if (m_arrNewVal.ContainsKey("name"))
                    return m_arrNewVal["name"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("name"))
                    m_arrNewVal["name"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("name", val);
                }
            }
        }
        public string Code
        {
            get
            {
                if (m_arrNewVal.ContainsKey("code"))
                    return m_arrNewVal["code"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("code"))
                    m_arrNewVal["code"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("code", val);
                }
            }
        }
        public string Unit
        {
            get
            {
                if (m_arrNewVal.ContainsKey("unit"))
                    return m_arrNewVal["unit"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("unit"))
                    m_arrNewVal["unit"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("unit", val);
                }
            }
        }
        public string Brand
        {
            get
            {
                if (m_arrNewVal.ContainsKey("brand"))
                    return m_arrNewVal["brand"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("brand"))
                    m_arrNewVal["brand"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("brand", val);
                }
            }
        }
        public string Detail
        {
            get
            {
                if (m_arrNewVal.ContainsKey("detail"))
                    return m_arrNewVal["detail"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("detail"))
                    m_arrNewVal["detail"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("detail", val);
                }
            }
        }
        public string Factory
        {
            get
            {
                if (m_arrNewVal.ContainsKey("factory"))
                    return m_arrNewVal["factory"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("factory"))
                    m_arrNewVal["factory"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("factory", val);
                }
            }
        }


        public CProductImgMgr ProductImgMgr
        {
            get
            {
                return (CProductImgMgr)this.GetSubObjectMgr("SP_ProductImg", typeof(CProductImgMgr));
            }
            set
            {
                this.SetSubObjectMgr("SP_ProductImg", value);
            }
        }
        public CPriceMgr PriceMgr
        {
            get
            {
                return (CPriceMgr)this.GetSubObjectMgr("SP_Price", typeof(CPriceMgr));
            }
            set
            {
                this.SetSubObjectMgr("SP_Price", value);
            }
        }
        public CSpecificationInProductMgr SpecificationInProductMgr
        {
            get
            {
                return (CSpecificationInProductMgr)this.GetSubObjectMgr("SP_SpecificationInProduct", typeof(CSpecificationInProductMgr));
            }
            set
            {
                this.SetSubObjectMgr("SP_SpecificationInProduct", value);
            }
        }
        public CColorInProductMgr ColorInProductMgr
        {
            get
            {
                return (CColorInProductMgr)this.GetSubObjectMgr("SP_ColorInProduct", typeof(CColorInProductMgr));
            }
            set
            {
                this.SetSubObjectMgr("SP_ColorInProduct", value);
            }
        }
    }
}