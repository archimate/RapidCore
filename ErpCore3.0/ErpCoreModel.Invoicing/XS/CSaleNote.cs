// File:    CSaleNote.cs
// Author:  甘孝俭
// email:   ganxiaojian@hotmail.com 
// QQ:      154986287
// http://www.8088net.com
// 协议声明：本软件为开源系统，遵循国际开源组织协议。任何单位或个人可以使用或修改本软件源码，
//          可以用于作为非商业或商业用途，但由于使用本源码所引起的一切后果与作者无关。
//          未经作者许可，禁止任何企业或个人直接出售本源码或者把本软件作为独立的功能进行销售活动，
//          作者将保留追究责任的权利。
// Created: 2013/3/1 10:56:13
// Purpose: Definition of Class CSaleNote

using System;
using System.Text;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Invoicing
{
    
    public class CSaleNote : CBaseObject
    {

        public CSaleNote()
        {
            TbCode = "XS_SaleNote";
            ClassName = "ErpCoreModel.Invoicing.CSaleNote";

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
        public DateTime SaleDate
        {
            get
            {
                if (m_arrNewVal.ContainsKey("saledate"))
                    return m_arrNewVal["saledate"].DatetimeVal;
                else
                    return DateTime.Now;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("saledate"))
                    m_arrNewVal["saledate"].DatetimeVal = value;
                else
                {
                    CValue val = new CValue();
                    val.DatetimeVal = value;
                    m_arrNewVal.Add("saledate", val);
                }
            }
        }
        public string Customer
        {
            get
            {
                if (m_arrNewVal.ContainsKey("customer"))
                    return m_arrNewVal["customer"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("customer"))
                    m_arrNewVal["customer"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("customer", val);
                }
            }
        }
        public string Contacts
        {
            get
            {
                if (m_arrNewVal.ContainsKey("contacts"))
                    return m_arrNewVal["contacts"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("contacts"))
                    m_arrNewVal["contacts"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("contacts", val);
                }
            }
        }
        public string Tel
        {
            get
            {
                if (m_arrNewVal.ContainsKey("tel"))
                    return m_arrNewVal["tel"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("tel"))
                    m_arrNewVal["tel"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("tel", val);
                }
            }
        }
        public string Addr
        {
            get
            {
                if (m_arrNewVal.ContainsKey("addr"))
                    return m_arrNewVal["addr"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("addr"))
                    m_arrNewVal["addr"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("addr", val);
                }
            }
        }
        public double OtherCharge
        {
            get
            {
                if (m_arrNewVal.ContainsKey("othercharge"))
                    return m_arrNewVal["othercharge"].DoubleVal;
                else
                    return 0;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("othercharge"))
                    m_arrNewVal["othercharge"].DoubleVal = value;
                else
                {
                    CValue val = new CValue();
                    val.DoubleVal = value;
                    m_arrNewVal.Add("othercharge", val);
                }
            }
        }
        public double ShipCharge
        {
            get
            {
                if (m_arrNewVal.ContainsKey("shipcharge"))
                    return m_arrNewVal["shipcharge"].DoubleVal;
                else
                    return 0;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("shipcharge"))
                    m_arrNewVal["shipcharge"].DoubleVal = value;
                else
                {
                    CValue val = new CValue();
                    val.DoubleVal = value;
                    m_arrNewVal.Add("shipcharge", val);
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
        public Guid B_Company_id
        {
            get
            {
                if (m_arrNewVal.ContainsKey("b_company_id"))
                    return m_arrNewVal["b_company_id"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {        
                if (m_arrNewVal.ContainsKey("b_company_id"))
                    m_arrNewVal["b_company_id"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("b_company_id", val);
                }
            }
        }
        public string Attn
        {
            get
            {
                if (m_arrNewVal.ContainsKey("attn"))
                    return m_arrNewVal["attn"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("attn"))
                    m_arrNewVal["attn"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("attn", val);
                }
            }
        }
        public string Remarks
        {
            get
            {
                if (m_arrNewVal.ContainsKey("remarks"))
                    return m_arrNewVal["remarks"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("remarks"))
                    m_arrNewVal["remarks"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("remarks", val);
                }
            }
        }

        public CSaleNoteDetailMgr SaleNoteDetailMgr
        {
            get
            {
                return (CSaleNoteDetailMgr)this.GetSubObjectMgr("XS_SaleNoteDetail", typeof(CSaleNoteDetailMgr));
            }
            set
            {
                this.SetSubObjectMgr("XS_SaleNoteDetail", value);
            }
        }
    }
}