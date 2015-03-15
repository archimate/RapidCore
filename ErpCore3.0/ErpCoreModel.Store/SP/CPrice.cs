// File:    CPrice.cs
// Author:  甘孝俭
// email:   ganxiaojian@hotmail.com 
// QQ:      154986287
// http://www.8088net.com
// 协议声明：本软件为开源系统，遵循国际开源组织协议。任何单位或个人可以使用或修改本软件源码，
//          可以用于作为非商业或商业用途，但由于使用本源码所引起的一切后果与作者无关。
//          未经作者许可，禁止任何企业或个人直接出售本源码或者把本软件作为独立的功能进行销售活动，
//          作者将保留追究责任的权利。
// Created: 2012/11/28 21:13:41
// Purpose: Definition of Class CPrice

using System;
using System.Text;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Store
{
    //零售价；批发价；促销价
    public enum PriceType { Retail, Wholesale, Promotion }

    public class CPrice : CBaseObject
    {

        public CPrice()
        {
            TbCode = "SP_Price";
            ClassName = "ErpCoreModel.Store.CPrice";

        }

        
        public Guid SP_Product_id
        {
            get
            {
                if (m_arrNewVal.ContainsKey("sp_product_id"))
                    return m_arrNewVal["sp_product_id"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {        
                if (m_arrNewVal.ContainsKey("sp_product_id"))
                    m_arrNewVal["sp_product_id"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("sp_product_id", val);
                }
            }
        }
        public int MinOrderNum
        {
            get
            {
                if (m_arrNewVal.ContainsKey("minordernum"))
                    return m_arrNewVal["minordernum"].IntVal;
                else
                    return 0;
            }
            set
            {           
                if (m_arrNewVal.ContainsKey("minordernum"))
                    m_arrNewVal["minordernum"].IntVal = value;
                else
                {
                    CValue val = new CValue();
                    val.IntVal = value;
                    m_arrNewVal.Add("minordernum", val);
                }
            }
        }
        public int MaxOrderNum
        {
            get
            {
                if (m_arrNewVal.ContainsKey("maxordernum"))
                    return m_arrNewVal["maxordernum"].IntVal;
                else
                    return 0;
            }
            set
            {           
                if (m_arrNewVal.ContainsKey("maxordernum"))
                    m_arrNewVal["maxordernum"].IntVal = value;
                else
                {
                    CValue val = new CValue();
                    val.IntVal = value;
                    m_arrNewVal.Add("maxordernum", val);
                }
            }
        }
        public double Price
        {
            get
            {
                if (m_arrNewVal.ContainsKey("price"))
                    return m_arrNewVal["price"].DoubleVal;
                else
                    return 0;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("price"))
                    m_arrNewVal["price"].DoubleVal = value;
                else
                {
                    CValue val = new CValue();
                    val.DoubleVal = value;
                    m_arrNewVal.Add("price", val);
                }
            }
        }
        public int Idx
        {
            get
            {
                if (m_arrNewVal.ContainsKey("idx"))
                    return m_arrNewVal["idx"].IntVal;
                else
                    return 0;
            }
            set
            {           
                if (m_arrNewVal.ContainsKey("idx"))
                    m_arrNewVal["idx"].IntVal = value;
                else
                {
                    CValue val = new CValue();
                    val.IntVal = value;
                    m_arrNewVal.Add("idx", val);
                }
            }
        }
        public PriceType Type
        {
            get
            {
                if (m_arrNewVal.ContainsKey("type"))
                    return (PriceType)m_arrNewVal["type"].IntVal;
                else
                    return PriceType.Retail;
            }
            set
            {           
                if (m_arrNewVal.ContainsKey("type"))
                    m_arrNewVal["type"].IntVal = (int)value;
                else
                {
                    CValue val = new CValue();
                    val.IntVal = (int)value;
                    m_arrNewVal.Add("type", val);
                }
            }
        }
    }
}