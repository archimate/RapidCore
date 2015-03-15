// File:    COrderDetail.cs
// Author:  甘孝俭
// email:   ganxiaojian@hotmail.com 
// QQ:      154986287
// http://www.8088net.com
// 协议声明：本软件为开源系统，遵循国际开源组织协议。任何单位或个人可以使用或修改本软件源码，
//          可以用于作为非商业或商业用途，但由于使用本源码所引起的一切后果与作者无关。
//          未经作者许可，禁止任何企业或个人直接出售本源码或者把本软件作为独立的功能进行销售活动，
//          作者将保留追究责任的权利。
// Created: 2012/11/28 21:13:41
// Purpose: Definition of Class COrderDetail

using System;
using System.Text;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Store
{
    
    public class COrderDetail : CBaseObject
    {

        public COrderDetail()
        {
            TbCode = "DD_OrderDetail";
            ClassName = "ErpCoreModel.Store.COrderDetail";

        }

        
        public string Specification
        {
            get
            {
                if (m_arrNewVal.ContainsKey("specification"))
                    return m_arrNewVal["specification"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("specification"))
                    m_arrNewVal["specification"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("specification", val);
                }
            }
        }
        public string Color
        {
            get
            {
                if (m_arrNewVal.ContainsKey("color"))
                    return m_arrNewVal["color"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("color"))
                    m_arrNewVal["color"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("color", val);
                }
            }
        }
        public double Num
        {
            get
            {
                if (m_arrNewVal.ContainsKey("num"))
                    return m_arrNewVal["num"].DoubleVal;
                else
                    return 0;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("num"))
                    m_arrNewVal["num"].DoubleVal = value;
                else
                {
                    CValue val = new CValue();
                    val.DoubleVal = value;
                    m_arrNewVal.Add("num", val);
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
        public double Discount
        {
            get
            {
                if (m_arrNewVal.ContainsKey("discount"))
                    return m_arrNewVal["discount"].DoubleVal;
                else
                    return 0;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("discount"))
                    m_arrNewVal["discount"].DoubleVal = value;
                else
                {
                    CValue val = new CValue();
                    val.DoubleVal = value;
                    m_arrNewVal.Add("discount", val);
                }
            }
        }
        public Guid DD_Order_id
        {
            get
            {
                if (m_arrNewVal.ContainsKey("dd_order_id"))
                    return m_arrNewVal["dd_order_id"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("dd_order_id"))
                    m_arrNewVal["dd_order_id"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("dd_order_id", val);
                }
            }
        }
        public int State
        {
            get
            {
                if (m_arrNewVal.ContainsKey("state"))
                    return m_arrNewVal["state"].IntVal;
                else
                    return 0;
            }
            set
            {           
                if (m_arrNewVal.ContainsKey("state"))
                    m_arrNewVal["state"].IntVal = value;
                else
                {
                    CValue val = new CValue();
                    val.IntVal = value;
                    m_arrNewVal.Add("state", val);
                }
            }
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
    }
}