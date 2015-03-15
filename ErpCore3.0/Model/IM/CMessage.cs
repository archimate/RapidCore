// File:    CMessage.cs
// Created: 2012/10/9 22:08:37
// Purpose: Definition of Class CMessage

using System;
using System.Text;
using ErpCoreModel.Framework;

namespace ErpCoreModel.IM
{
    //按创建时间排序
    public class CMessage : CBaseObject,IComparable
    {

        public CMessage()
        {
            TbCode = "IM_Message";
            ClassName = "ErpCoreModel.IM.CMessage";
            //默认为新消息
            IsNew = true;
        }
        #region 实现比较接口的CompareTo方法
        public int CompareTo(object obj)
        {
            int res = 0;
            try
            {
                CMessage sObj = (CMessage)obj;
                if (this.Created > sObj.Created)
                {
                    res = 1;
                }
                else if (this.Created < sObj.Created)
                {
                    res = -1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("比较异常", ex.InnerException);
            }
            return res;
        }
        #endregion

        
        public Guid Sender
        {
            get
            {
                if (m_arrNewVal.ContainsKey("sender"))
                    return m_arrNewVal["sender"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {        
                if (m_arrNewVal.ContainsKey("sender"))
                    m_arrNewVal["sender"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("sender", val);
                }
            }
        }
        public Guid Receiver
        {
            get
            {
                if (m_arrNewVal.ContainsKey("receiver"))
                    return m_arrNewVal["receiver"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {        
                if (m_arrNewVal.ContainsKey("receiver"))
                    m_arrNewVal["receiver"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("receiver", val);
                }
            }
        }
        public string Content
        {
            get
            {
                if (m_arrNewVal.ContainsKey("content"))
                    return m_arrNewVal["content"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("content"))
                    m_arrNewVal["content"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("content", val);
                }
            }
        }
        public bool IsNew
        {
            get
            {
                if (m_arrNewVal.ContainsKey("isnew"))
                    return m_arrNewVal["isnew"].BoolVal;
                else
                    return true;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("isnew"))
                    m_arrNewVal["isnew"].BoolVal = value;
                else
                {
                    CValue val = new CValue();
                    val.BoolVal = value;
                    m_arrNewVal.Add("isnew", val);
                }
            }
        }
    }
}