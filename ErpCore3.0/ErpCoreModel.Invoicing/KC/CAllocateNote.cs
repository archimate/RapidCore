// File:    CAllocateNote.cs
// Author:  甘孝俭
// email:   ganxiaojian@hotmail.com 
// QQ:      154986287
// http://www.8088net.com
// 协议声明：本软件为开源系统，遵循国际开源组织协议。任何单位或个人可以使用或修改本软件源码，
//          可以用于作为非商业或商业用途，但由于使用本源码所引起的一切后果与作者无关。
//          未经作者许可，禁止任何企业或个人直接出售本源码或者把本软件作为独立的功能进行销售活动，
//          作者将保留追究责任的权利。
// Created: 2013/3/1 10:56:13
// Purpose: Definition of Class CAllocateNote

using System;
using System.Text;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Invoicing
{
    
    public class CAllocateNote : CBaseObject
    {

        public CAllocateNote()
        {
            TbCode = "KC_AllocateNote";
            ClassName = "ErpCoreModel.Invoicing.CAllocateNote";

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
        public DateTime AllocateDate
        {
            get
            {
                if (m_arrNewVal.ContainsKey("allocatedate"))
                    return m_arrNewVal["allocatedate"].DatetimeVal;
                else
                    return DateTime.Now;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("allocatedate"))
                    m_arrNewVal["allocatedate"].DatetimeVal = value;
                else
                {
                    CValue val = new CValue();
                    val.DatetimeVal = value;
                    m_arrNewVal.Add("allocatedate", val);
                }
            }
        }
        public Guid OutCompany
        {
            get
            {
                if (m_arrNewVal.ContainsKey("outcompany"))
                    return m_arrNewVal["outcompany"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {        
                if (m_arrNewVal.ContainsKey("outcompany"))
                    m_arrNewVal["outcompany"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("outcompany", val);
                }
            }
        }
        public Guid InCompany
        {
            get
            {
                if (m_arrNewVal.ContainsKey("incompany"))
                    return m_arrNewVal["incompany"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {        
                if (m_arrNewVal.ContainsKey("incompany"))
                    m_arrNewVal["incompany"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("incompany", val);
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

        public CAllocateNoteDetailMgr AllocateNoteDetailMgr
        {
            get
            {
                return (CAllocateNoteDetailMgr)this.GetSubObjectMgr("KC_AllocateNoteDetail", typeof(CAllocateNoteDetailMgr));
            }
            set
            {
                this.SetSubObjectMgr("KC_AllocateNoteDetail", value);
            }
        }
    }
}