// File:    CView.cs
// Created: 2012-08-28 13:20:29
// Purpose: Definition of Class CView

using System;
using System.Text;
using System.Collections.Generic;
using ErpCoreModel.Framework;

namespace ErpCoreModel.UI
{
    public enum enumViewType {Single,MasterDetail,MultMasterDetail }
    public class CView : CBaseObject
    {

        public CView()
        {
            TbCode = "UI_View";
            ClassName = "ErpCoreModel.UI.CView";

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
        public Guid UI_ViewCatalog_id
        {
            get
            {
                if (m_arrNewVal.ContainsKey("ui_viewcatalog_id"))
                    return m_arrNewVal["ui_viewcatalog_id"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {        
                if (m_arrNewVal.ContainsKey("ui_viewcatalog_id"))
                    m_arrNewVal["ui_viewcatalog_id"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("ui_viewcatalog_id", val);
                }
            }
        }
        public enumViewType VType
        {
            get
            {
                if (m_arrNewVal.ContainsKey("vtype"))
                    return (enumViewType)m_arrNewVal["vtype"].IntVal;
                else
                    return enumViewType.Single;
            }
            set
            {           
                if (m_arrNewVal.ContainsKey("vtype"))
                    m_arrNewVal["vtype"].IntVal = (int)value;
                else
                {
                    CValue val = new CValue();
                    val.IntVal = (int)value;
                    m_arrNewVal.Add("vtype", val);
                }
            }
        }

        public CColumnInViewMgr ColumnInViewMgr
        {
            get
            {
                return (CColumnInViewMgr)this.GetSubObjectMgr("UI_ColumnInView", typeof(CColumnInViewMgr));
            }
            set
            {
                this.SetSubObjectMgr("UI_ColumnInView", value);
            }
        }
        public CColumnDefaultValInViewMgr ColumnDefaultValInViewMgr
        {
            get
            {
                return (CColumnDefaultValInViewMgr)this.GetSubObjectMgr("UI_ColumnDefaultValInView", typeof(CColumnDefaultValInViewMgr));
            }
            set
            {
                this.SetSubObjectMgr("UI_ColumnDefaultValInView", value);
            }
        }
        public CViewDetailMgr ViewDetailMgr
        {
            get
            {
                return (CViewDetailMgr)this.GetSubObjectMgr("UI_ViewDetail", typeof(CViewDetailMgr));
            }
            set
            {
                this.SetSubObjectMgr("UI_ViewDetail", value);
            }
        }

        public CViewFilterMgr ViewFilterMgr
        {
            get
            {
                return (CViewFilterMgr)this.GetSubObjectMgr("UI_ViewFilter", typeof(CViewFilterMgr));
            }
            set
            {
                this.SetSubObjectMgr("UI_ViewFilter", value);
            }
        }

        public CTButtonInViewMgr TButtonInViewMgr
        {
            get
            {
                return (CTButtonInViewMgr)this.GetSubObjectMgr("UI_TButtonInView", typeof(CTButtonInViewMgr));
            }
            set
            {
                this.SetSubObjectMgr("UI_TButtonInView", value);
            }
        }

    }
}