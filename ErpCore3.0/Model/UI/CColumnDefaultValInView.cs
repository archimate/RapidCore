// File:    CColumnDefaultValInView.cs
// Author:  甘孝俭
// email:   ganxiaojian@hotmail.com 
// QQ:      154986287
// http://www.8088net.com
// 协议声明：本软件为开源系统，遵循国际开源组织协议。任何单位或个人可以使用或修改本软件源码，
//          可以用于作为非商业或商业用途，但由于使用本源码所引起的一切后果与作者无关。
//          未经作者许可，禁止任何企业或个人直接出售本源码或者把本软件作为独立的功能进行销售活动，
//          作者将保留追究责任的权利。
// Created: 2013/3/5 20:47:30
// Purpose: Definition of Class CColumnDefaultValInView

using System;
using System.Text;
using ErpCoreModel.Framework;

namespace ErpCoreModel.UI
{
    
    public class CColumnDefaultValInView : CBaseObject
    {

        public CColumnDefaultValInView()
        {
            TbCode = "UI_ColumnDefaultValInView";
            ClassName = "ErpCoreModel.UI.CColumnDefaultValInView";

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
        public string DefaultVal
        {
            get
            {
                if (m_arrNewVal.ContainsKey("defaultval"))
                    return m_arrNewVal["defaultval"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("defaultval"))
                    m_arrNewVal["defaultval"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("defaultval", val);
                }
            }
        }
        public bool ReadOnly
        {
            get
            {
                if (m_arrNewVal.ContainsKey("readonly"))
                    return m_arrNewVal["readonly"].BoolVal;
                else
                    return false;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("readonly"))
                    m_arrNewVal["readonly"].BoolVal = value;
                else
                {
                    CValue val = new CValue();
                    val.BoolVal = value;
                    m_arrNewVal.Add("readonly", val);
                }
            }
        }
    }
}