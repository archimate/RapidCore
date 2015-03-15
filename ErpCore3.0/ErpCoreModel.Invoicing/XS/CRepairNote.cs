// File:    CRepairNote.cs
// Author:  甘孝俭
// email:   ganxiaojian@hotmail.com 
// QQ:      154986287
// http://www.8088net.com
// 协议声明：本软件为开源系统，遵循国际开源组织协议。任何单位或个人可以使用或修改本软件源码，
//          可以用于作为非商业或商业用途，但由于使用本源码所引起的一切后果与作者无关。
//          未经作者许可，禁止任何企业或个人直接出售本源码或者把本软件作为独立的功能进行销售活动，
//          作者将保留追究责任的权利。
// Created: 2013/3/1 10:56:13
// Purpose: Definition of Class CRepairNote

using System;
using System.Text;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Invoicing
{
    
    public class CRepairNote : CBaseObject
    {

        public CRepairNote()
        {
            TbCode = "XS_RepairNote";
            ClassName = "ErpCoreModel.Invoicing.CRepairNote";

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
        public string Cause
        {
            get
            {
                if (m_arrNewVal.ContainsKey("cause"))
                    return m_arrNewVal["cause"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("cause"))
                    m_arrNewVal["cause"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("cause", val);
                }
            }
        }
        public string Result
        {
            get
            {
                if (m_arrNewVal.ContainsKey("result"))
                    return m_arrNewVal["result"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("result"))
                    m_arrNewVal["result"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("result", val);
                }
            }
        }
        public string RepairPerson
        {
            get
            {
                if (m_arrNewVal.ContainsKey("repairperson"))
                    return m_arrNewVal["repairperson"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("repairperson"))
                    m_arrNewVal["repairperson"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("repairperson", val);
                }
            }
        }
        public DateTime RepairDate
        {
            get
            {
                if (m_arrNewVal.ContainsKey("repairdate"))
                    return m_arrNewVal["repairdate"].DatetimeVal;
                else
                    return DateTime.Now;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("repairdate"))
                    m_arrNewVal["repairdate"].DatetimeVal = value;
                else
                {
                    CValue val = new CValue();
                    val.DatetimeVal = value;
                    m_arrNewVal.Add("repairdate", val);
                }
            }
        }
        public double RepairCharge
        {
            get
            {
                if (m_arrNewVal.ContainsKey("repaircharge"))
                    return m_arrNewVal["repaircharge"].DoubleVal;
                else
                    return 0;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("repaircharge"))
                    m_arrNewVal["repaircharge"].DoubleVal = value;
                else
                {
                    CValue val = new CValue();
                    val.DoubleVal = value;
                    m_arrNewVal.Add("repaircharge", val);
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
    }
}