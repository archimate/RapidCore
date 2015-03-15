// File:    CSyncTime.cs
// Author:  甘孝俭
// email:   ganxiaojian@hotmail.com 
// QQ:      154986287
// http://www.8088net.com
// 协议声明：本软件为开源系统，遵循国际开源组织协议。任何单位或个人可以使用或修改本软件源码，
//          可以用于作为非商业或商业用途，但由于使用本源码所引起的一切后果与作者无关。
//          未经作者许可，禁止任何企业或个人直接出售本源码或者把本软件作为独立的功能进行销售活动，
//          作者将保留追究责任的权利。
// Created: 2014/5/27 11:34:35
// Purpose: Definition of Class CSyncTime

using System;
using System.Text;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Base
{
    
    public class CSyncTime : CBaseObject
    {

        public CSyncTime()
        {
            TbCode = "B_SyncTime";
            ClassName = "ErpCoreModel.Base.CSyncTime";

        }

        
        public string TableCode
        {
            get
            {
                if (m_arrNewVal.ContainsKey("tablecode"))
                    return m_arrNewVal["tablecode"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("tablecode"))
                    m_arrNewVal["tablecode"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("tablecode", val);
                }
            }
        }
        public DateTime LastTime
        {
            get
            {
                if (m_arrNewVal.ContainsKey("lasttime"))
                    return m_arrNewVal["lasttime"].DatetimeVal;
                else
                    return DateTime.Now;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("lasttime"))
                    m_arrNewVal["lasttime"].DatetimeVal = value;
                else
                {
                    CValue val = new CValue();
                    val.DatetimeVal = value;
                    m_arrNewVal.Add("lasttime", val);
                }
            }
        }
        public string SystemName
        {
            get
            {
                if (m_arrNewVal.ContainsKey("systemname"))
                    return m_arrNewVal["systemname"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("systemname"))
                    m_arrNewVal["systemname"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("systemname", val);
                }
            }
        }
    }
}