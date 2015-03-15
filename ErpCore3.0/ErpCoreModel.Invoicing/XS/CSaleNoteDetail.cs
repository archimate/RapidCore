// File:    CSaleNoteDetail.cs
// Author:  甘孝俭
// email:   ganxiaojian@hotmail.com 
// QQ:      154986287
// http://www.8088net.com
// 协议声明：本软件为开源系统，遵循国际开源组织协议。任何单位或个人可以使用或修改本软件源码，
//          可以用于作为非商业或商业用途，但由于使用本源码所引起的一切后果与作者无关。
//          未经作者许可，禁止任何企业或个人直接出售本源码或者把本软件作为独立的功能进行销售活动，
//          作者将保留追究责任的权利。
// Created: 2013/3/1 10:56:13
// Purpose: Definition of Class CSaleNoteDetail

using System;
using System.Text;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Invoicing
{
    
    public class CSaleNoteDetail : CBaseObject
    {

        public CSaleNoteDetail()
        {
            TbCode = "XS_SaleNoteDetail";
            ClassName = "ErpCoreModel.Invoicing.CSaleNoteDetail";

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
        public bool IsGive
        {
            get
            {
                if (m_arrNewVal.ContainsKey("isgive"))
                    return m_arrNewVal["isgive"].BoolVal;
                else
                    return false;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("isgive"))
                    m_arrNewVal["isgive"].BoolVal = value;
                else
                {
                    CValue val = new CValue();
                    val.BoolVal = value;
                    m_arrNewVal.Add("isgive", val);
                }
            }
        }
        public bool IsExchange
        {
            get
            {
                if (m_arrNewVal.ContainsKey("isexchange"))
                    return m_arrNewVal["isexchange"].BoolVal;
                else
                    return false;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("isexchange"))
                    m_arrNewVal["isexchange"].BoolVal = value;
                else
                {
                    CValue val = new CValue();
                    val.BoolVal = value;
                    m_arrNewVal.Add("isexchange", val);
                }
            }
        }
        public Guid XS_SaleNote_id
        {
            get
            {
                if (m_arrNewVal.ContainsKey("xs_salenote_id"))
                    return m_arrNewVal["xs_salenote_id"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {        
                if (m_arrNewVal.ContainsKey("xs_salenote_id"))
                    m_arrNewVal["xs_salenote_id"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("xs_salenote_id", val);
                }
            }
        }
    }
}