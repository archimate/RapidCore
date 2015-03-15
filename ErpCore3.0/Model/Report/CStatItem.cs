// File:    CStatItem.cs
// Created: 2012/5/9 13:26:26
// Purpose: Definition of Class CStatItem

using System;
using System.Text;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Report
{
    //从IComparable继承可以方便List 的排序
    public class CStatItem : CBaseObject,IComparable
    {
        public enum enumItemType { Field, Formula }
        public enum enumStatType { Val, Sum,Avg,Max,Min,Count }
        public enum enumOrder { None, Asc, Desc }

        public CStatItem()
        {
            TbCode = "RPT_StatItem";
            ClassName = "ErpCoreModel.Report.CStatItem";

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
        public Guid RPT_Report_id
        {
            get
            {
                if (m_arrNewVal.ContainsKey("rpt_report_id"))
                    return m_arrNewVal["rpt_report_id"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {        
                if (m_arrNewVal.ContainsKey("rpt_report_id"))
                    m_arrNewVal["rpt_report_id"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("rpt_report_id", val);
                }
            }
        }
        public enumItemType ItemType
        {
            get
            {
                if (m_arrNewVal.ContainsKey("itemtype"))
                    return (enumItemType)m_arrNewVal["itemtype"].IntVal;
                else
                    return enumItemType.Field;
            }
            set
            {           
                if (m_arrNewVal.ContainsKey("itemtype"))
                    m_arrNewVal["itemtype"].IntVal = (int)value;
                else
                {
                    CValue val = new CValue();
                    val.IntVal = (int)value;
                    m_arrNewVal.Add("itemtype", val);
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
        public string Formula
        {
            get
            {
                if (m_arrNewVal.ContainsKey("formula"))
                    return m_arrNewVal["formula"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("formula"))
                    m_arrNewVal["formula"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("formula", val);
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
        public enumStatType StatType
        {
            get
            {
                if (m_arrNewVal.ContainsKey("stattype"))
                    return (enumStatType)m_arrNewVal["stattype"].IntVal;
                else
                    return enumStatType.Val;
            }
            set
            {           
                if (m_arrNewVal.ContainsKey("stattype"))
                    m_arrNewVal["stattype"].IntVal = (int)value;
                else
                {
                    CValue val = new CValue();
                    val.IntVal = (int)value;
                    m_arrNewVal.Add("stattype", val);
                }
            }
        }
        public enumOrder Order
        {
            get
            {
                if (m_arrNewVal.ContainsKey("order"))
                    return (enumOrder)m_arrNewVal["order"].IntVal;
                else
                    return enumOrder.None;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("order"))
                    m_arrNewVal["order"].IntVal = (int)value;
                else
                {
                    CValue val = new CValue();
                    val.IntVal = (int)value;
                    m_arrNewVal.Add("order", val);
                }
            }
        }

        public string GetStatTypeName()
        {
            if (StatType == enumStatType.Val)
                return "取数";
            else if (StatType == enumStatType.Sum)
                return "求和";
            else if (StatType == enumStatType.Avg)
                return "求平均";
            else if (StatType == enumStatType.Max)
                return "求最大值";
            else if (StatType == enumStatType.Min)
                return "求最小值";
            else
                return "计数";
        }
        public string GetStatTypeFunc()
        {
            if (StatType == enumStatType.Val)
                return "";
            else if (StatType == enumStatType.Sum)
                return "sum";
            else if (StatType == enumStatType.Avg)
                return "avg";
            else if (StatType == enumStatType.Max)
                return "max";
            else if (StatType == enumStatType.Min)
                return "min";
            else
                return "count";
        }
        public void SetStatTypeByName(string sName)
        {
            if (sName == "取数")
                StatType = enumStatType.Val;
            else if (sName == "求和")
                StatType = enumStatType.Sum;
            else if (sName == "求平均")
                StatType = enumStatType.Avg;
            else if (sName == "求最大值")
                StatType = enumStatType.Max;
            else if (sName == "求最小值")
                StatType = enumStatType.Min;
            else
                StatType = enumStatType.Count;
        }
        public string GetOrderName()
        {
            if (Order == enumOrder.None)
                return "默认";
            else if (Order == enumOrder.Asc)
                return "升序";
            else
                return "降序";
        }
        public void SetOrderByName(string sName)
        {
            if (sName == "默认")
                Order = enumOrder.None;
            else if (sName == "升序")
                Order = enumOrder.Asc;
            else
                Order = enumOrder.Desc;
        }

        #region 实现比较接口的CompareTo方法
        public int CompareTo(object obj)
        {
            int res = 0;
            try
            {
                CStatItem sObj = (CStatItem)obj;
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