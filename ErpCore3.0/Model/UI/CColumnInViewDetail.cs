// File:    CColumnInViewDetail.cs
// Created: 2012-08-28 13:20:30
// Purpose: Definition of Class CColumnInViewDetail

using System;
using System.Text;
using ErpCoreModel.Framework;

namespace ErpCoreModel.UI
{

    //从IComparable继承可以方便List 的排序
    public class CColumnInViewDetail : CBaseObject, IComparable
    {

        public CColumnInViewDetail()
        {
            TbCode = "UI_ColumnInViewDetail";
            ClassName = "ErpCoreModel.UI.CColumnInViewDetail";

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
        public string Caption
        {
            get
            {
                if (m_arrNewVal.ContainsKey("caption"))
                    return m_arrNewVal["caption"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("caption"))
                    m_arrNewVal["caption"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("caption", val);
                }
            }
        }

        public bool IsVirtual
        {
            get
            {
                if (m_arrNewVal.ContainsKey("isvirtual"))
                    return m_arrNewVal["isvirtual"].BoolVal;
                else
                    return false;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("isvirtual"))
                    m_arrNewVal["isvirtual"].BoolVal = value;
                else
                {
                    CValue val = new CValue();
                    val.BoolVal = value;
                    m_arrNewVal.Add("isvirtual", val);
                }
            }
        }

        public Guid UI_ViewDetail_id
        {
            get
            {
                if (m_arrNewVal.ContainsKey("ui_viewdetail_id"))
                    return m_arrNewVal["ui_viewdetail_id"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {        
                if (m_arrNewVal.ContainsKey("ui_viewdetail_id"))
                    m_arrNewVal["ui_viewdetail_id"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("ui_viewdetail_id", val);
                }
            }
        }

        #region 实现比较接口的CompareTo方法
        public int CompareTo(object obj)
        {
            int res = 0;
            try
            {
                CColumnInViewDetail sObj = (CColumnInViewDetail)obj;
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