// File:    CPriceNote.cs
// Author:  甘孝俭
// email:   ganxiaojian@hotmail.com 
// QQ:      154986287
// http://www.8088net.com
// 协议声明：本软件为开源系统，遵循国际开源组织协议。任何单位或个人可以使用或修改本软件源码，
//          可以用于作为非商业或商业用途，但由于使用本源码所引起的一切后果与作者无关。
//          未经作者许可，禁止任何企业或个人直接出售本源码或者把本软件作为独立的功能进行销售活动，
//          作者将保留追究责任的权利。
// Created: 2013/3/1 10:56:13
// Purpose: Definition of Class CPriceNote

using System;
using System.Text;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Invoicing
{
    
    public class CPriceNote : CBaseObject
    {

        public CPriceNote()
        {
            TbCode = "XS_PriceNote";
            ClassName = "ErpCoreModel.Invoicing.CPriceNote";

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
        public string To
        {
            get
            {
                if (m_arrNewVal.ContainsKey("to"))
                    return m_arrNewVal["to"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("to"))
                    m_arrNewVal["to"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("to", val);
                }
            }
        }
        public string ToContacts
        {
            get
            {
                if (m_arrNewVal.ContainsKey("tocontacts"))
                    return m_arrNewVal["tocontacts"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("tocontacts"))
                    m_arrNewVal["tocontacts"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("tocontacts", val);
                }
            }
        }
        public string ToTel
        {
            get
            {
                if (m_arrNewVal.ContainsKey("totel"))
                    return m_arrNewVal["totel"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("totel"))
                    m_arrNewVal["totel"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("totel", val);
                }
            }
        }
        public string ToFax
        {
            get
            {
                if (m_arrNewVal.ContainsKey("tofax"))
                    return m_arrNewVal["tofax"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("tofax"))
                    m_arrNewVal["tofax"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("tofax", val);
                }
            }
        }
        public string From
        {
            get
            {
                if (m_arrNewVal.ContainsKey("from"))
                    return m_arrNewVal["from"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("from"))
                    m_arrNewVal["from"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("from", val);
                }
            }
        }
        public string FromContacts
        {
            get
            {
                if (m_arrNewVal.ContainsKey("fromcontacts"))
                    return m_arrNewVal["fromcontacts"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("fromcontacts"))
                    m_arrNewVal["fromcontacts"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("fromcontacts", val);
                }
            }
        }
        public string FromTel
        {
            get
            {
                if (m_arrNewVal.ContainsKey("fromtel"))
                    return m_arrNewVal["fromtel"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("fromtel"))
                    m_arrNewVal["fromtel"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("fromtel", val);
                }
            }
        }
        public string FromFax
        {
            get
            {
                if (m_arrNewVal.ContainsKey("fromfax"))
                    return m_arrNewVal["fromfax"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("fromfax"))
                    m_arrNewVal["fromfax"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("fromfax", val);
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

        public CPriceNoteDetailMgr PriceNoteDetailMgr
        {
            get
            {
                return (CPriceNoteDetailMgr)this.GetSubObjectMgr("XS_PriceNoteDetail", typeof(CPriceNoteDetailMgr));
            }
            set
            {
                this.SetSubObjectMgr("XS_PriceNoteDetail", value);
            }
        }
    }
}