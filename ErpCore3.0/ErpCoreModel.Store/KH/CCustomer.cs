// File:    CCustomer.cs
// Author:  甘孝俭
// email:   ganxiaojian@hotmail.com 
// QQ:      154986287
// http://www.8088net.com
// 协议声明：本软件为开源系统，遵循国际开源组织协议。任何单位或个人可以使用或修改本软件源码，
//          可以用于作为非商业或商业用途，但由于使用本源码所引起的一切后果与作者无关。
//          未经作者许可，禁止任何企业或个人直接出售本源码或者把本软件作为独立的功能进行销售活动，
//          作者将保留追究责任的权利。
// Created: 2012/11/28 21:13:40
// Purpose: Definition of Class CCustomer

using System;
using System.Text;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Store
{
    
    public class CCustomer : CBaseObject
    {

        public CCustomer()
        {
            TbCode = "KH_Customer";
            ClassName = "ErpCoreModel.Store.CCustomer";

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
        public string Pwd
        {
            get
            {
                if (m_arrNewVal.ContainsKey("pwd"))
                    return m_arrNewVal["pwd"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("pwd"))
                    m_arrNewVal["pwd"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("pwd", val);
                }
            }
        }
        public string TName
        {
            get
            {
                if (m_arrNewVal.ContainsKey("tname"))
                    return m_arrNewVal["tname"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("tname"))
                    m_arrNewVal["tname"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("tname", val);
                }
            }
        }
        public bool Sex
        {
            get
            {
                if (m_arrNewVal.ContainsKey("sex"))
                    return m_arrNewVal["sex"].BoolVal;
                else
                    return false;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("sex"))
                    m_arrNewVal["sex"].BoolVal = value;
                else
                {
                    CValue val = new CValue();
                    val.BoolVal = value;
                    m_arrNewVal.Add("sex", val);
                }
            }
        }
        public DateTime Birthday
        {
            get
            {
                if (m_arrNewVal.ContainsKey("birthday"))
                    return m_arrNewVal["birthday"].DatetimeVal;
                else
                    return DateTime.Now;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("birthday"))
                    m_arrNewVal["birthday"].DatetimeVal = value;
                else
                {
                    CValue val = new CValue();
                    val.DatetimeVal = value;
                    m_arrNewVal.Add("birthday", val);
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
        public string Zipcode
        {
            get
            {
                if (m_arrNewVal.ContainsKey("zipcode"))
                    return m_arrNewVal["zipcode"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("zipcode"))
                    m_arrNewVal["zipcode"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("zipcode", val);
                }
            }
        }
        public string Company
        {
            get
            {
                if (m_arrNewVal.ContainsKey("company"))
                    return m_arrNewVal["company"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("company"))
                    m_arrNewVal["company"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("company", val);
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
        public string Phone
        {
            get
            {
                if (m_arrNewVal.ContainsKey("phone"))
                    return m_arrNewVal["phone"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("phone"))
                    m_arrNewVal["phone"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("phone", val);
                }
            }
        }
        public string E_mail
        {
            get
            {
                if (m_arrNewVal.ContainsKey("e_mail"))
                    return m_arrNewVal["e_mail"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("e_mail"))
                    m_arrNewVal["e_mail"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("e_mail", val);
                }
            }
        }
        public string QQ
        {
            get
            {
                if (m_arrNewVal.ContainsKey("qq"))
                    return m_arrNewVal["qq"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("qq"))
                    m_arrNewVal["qq"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("qq", val);
                }
            }
        }
        public string WW
        {
            get
            {
                if (m_arrNewVal.ContainsKey("ww"))
                    return m_arrNewVal["ww"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("ww"))
                    m_arrNewVal["ww"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("ww", val);
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


        public CAccountMgr AccountMgr
        {
            get
            {
                return (CAccountMgr)this.GetSubObjectMgr("KH_Account", typeof(CAccountMgr));
            }
            set
            {
                this.SetSubObjectMgr("KH_Account", value);
            }
        }
    }
}