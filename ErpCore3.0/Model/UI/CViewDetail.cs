// File:    CViewDetail.cs
// Created: 2012-08-28 13:20:30
// Purpose: Definition of Class CViewDetail

using System;
using System.Text;
using System.Collections.Generic;
using ErpCoreModel.Framework;

namespace ErpCoreModel.UI
{

    //从IComparable继承可以方便List 的排序
    public class CViewDetail : CBaseObject, IComparable
    {

        public CViewDetail()
        {
            TbCode = "UI_ViewDetail";
            ClassName = "ErpCoreModel.UI.CViewDetail";

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
        public Guid PrimaryKey
        {
            get
            {
                if (m_arrNewVal.ContainsKey("primarykey"))
                    return m_arrNewVal["primarykey"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("primarykey"))
                    m_arrNewVal["primarykey"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("primarykey", val);
                }
            }
        }
        public Guid ForeignKey
        {
            get
            {
                if (m_arrNewVal.ContainsKey("foreignkey"))
                    return m_arrNewVal["foreignkey"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("foreignkey"))
                    m_arrNewVal["foreignkey"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("foreignkey", val);
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

        public CColumnInViewDetailMgr ColumnInViewDetailMgr
        {
            get
            {
                return (CColumnInViewDetailMgr)this.GetSubObjectMgr("UI_ColumnInViewDetail", typeof(CColumnInViewDetailMgr));
            }
            set
            {
                this.SetSubObjectMgr("UI_ColumnInViewDetail", value);
            }
        }
        public CColumnDefaultValInViewDetailMgr ColumnDefaultValInViewDetailMgr
        {
            get
            {
                return (CColumnDefaultValInViewDetailMgr)this.GetSubObjectMgr("UI_ColumnDefaultValInViewDetail", typeof(CColumnDefaultValInViewDetailMgr));
            }
            set
            {
                this.SetSubObjectMgr("UI_ColumnDefaultValInViewDetail", value);
            }
        }

        #region 实现比较接口的CompareTo方法
        public int CompareTo(object obj)
        {
            int res = 0;
            try
            {
                CViewDetail sObj = (CViewDetail)obj;
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