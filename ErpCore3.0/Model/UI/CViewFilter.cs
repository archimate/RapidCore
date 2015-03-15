// File:    CViewFilter.cs
// Author:  甘孝俭
// email:   ganxiaojian@hotmail.com 
// QQ:      154986287
// http://www.8088net.com
// 协议声明：本软件为开源系统，遵循国际开源组织协议。任何单位或个人可以使用或修改本软件源码，
//          可以用于作为非商业或商业用途，但由于使用本源码所引起的一切后果与作者无关。
//          未经作者许可，禁止任何企业或个人直接出售本源码或者把本软件作为独立的功能进行销售活动，
//          作者将保留追究责任的权利。
// Created: 2013/2/6 13:32:41
// Purpose: Definition of Class CViewFilter

using System;
using System.Text;
using ErpCoreModel.Framework;

namespace ErpCoreModel.UI
{
    //比较符号 = > < >= <= != like
    public enum CompareSign { Equals, Greater, Less, GreaterAndEquals, LessAndEquals, NotEquals, Like }
    //从IComparable继承可以方便List 的排序
    public class CViewFilter : CBaseObject, IComparable
    {

        public CViewFilter()
        {
            TbCode = "UI_ViewFilter";
            ClassName = "ErpCoreModel.UI.CViewFilter";

        }

        
        public Guid UI_View_id
        {
            get
            {
                if (m_arrNewVal.ContainsKey("ui_view_id"))
                    return m_arrNewVal["ui_view_id"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {        
                if (m_arrNewVal.ContainsKey("ui_view_id"))
                    m_arrNewVal["ui_view_id"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("ui_view_id", val);
                }
            }
        }
        public string AndOr
        {
            get
            {
                if (m_arrNewVal.ContainsKey("andor"))
                    return m_arrNewVal["andor"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("andor"))
                    m_arrNewVal["andor"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("andor", val);
                }
            }
        }
        public Guid FW_Table_id
        {
            get
            {
                if (m_arrNewVal.ContainsKey("fw_table_id"))
                    return m_arrNewVal["fw_table_id"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {        
                if (m_arrNewVal.ContainsKey("fw_table_id"))
                    m_arrNewVal["fw_table_id"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("fw_table_id", val);
                }
            }
        }
        public Guid FW_Column_id
        {
            get
            {
                if (m_arrNewVal.ContainsKey("fw_column_id"))
                    return m_arrNewVal["fw_column_id"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {        
                if (m_arrNewVal.ContainsKey("fw_column_id"))
                    m_arrNewVal["fw_column_id"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("fw_column_id", val);
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
        public CompareSign Sign
        {
            get
            {
                if (m_arrNewVal.ContainsKey("sign"))
                    return (CompareSign)m_arrNewVal["sign"].IntVal;
                else
                    return CompareSign.Equals;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("sign"))
                    m_arrNewVal["sign"].IntVal = (int)value;
                else
                {
                    CValue val = new CValue();
                    val.IntVal = (int)value;
                    m_arrNewVal.Add("sign", val);
                }
            }
        }
        public string Val
        {
            get
            {
                if (m_arrNewVal.ContainsKey("val"))
                    return m_arrNewVal["val"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("val"))
                    m_arrNewVal["val"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("val", val);
                }
            }
        }
        //用于存非字段的条件
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

        public  string GetSignName()
        {
            switch (Sign)
            {
                case CompareSign.Equals:
                    return "=";
                case CompareSign.Greater:
                    return ">";
                case CompareSign.Less:
                    return "<";
                case CompareSign.GreaterAndEquals:
                    return ">=";
                case CompareSign.LessAndEquals:
                    return "<=";
                case CompareSign.NotEquals:
                    return "!=";
                case CompareSign.Like:
                    return "like";
            }
            return "=";
        }

        #region 实现比较接口的CompareTo方法
        public int CompareTo(object obj)
        {
            int res = 0;
            try
            {
                CViewFilter sObj = (CViewFilter)obj;
                if (this.Idx > sObj.Idx)
                {
                    res = 1;
                }
                else if (this.Idx < sObj.Idx)
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
    }
}