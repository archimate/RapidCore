// File:    CForm.cs
// Author:  甘孝俭
// email:   ganxiaojian@hotmail.com 
// QQ:      154986287
// http://www.8088net.com
// 协议声明：本软件为开源系统，遵循国际开源组织协议。任何单位或个人可以使用或修改本软件源码，
//          可以用于作为非商业或商业用途，但由于使用本源码所引起的一切后果与作者无关。
//          未经作者许可，禁止任何企业或个人直接出售本源码或者把本软件作为独立的功能进行销售活动，
//          作者将保留追究责任的权利。
// Created: 2011-10-7 14:13:18
// Purpose: Definition of Class CForm

using System;
using System.Text;
using System.Collections.Generic;
using ErpCoreModel.Framework;

namespace ErpCoreModel.UI
{
    
    public class CForm : CBaseObject
    {

        public CForm()
        {
            TbCode = "UI_Form";
            ClassName = "ErpCoreModel.UI.CForm";

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
        public Guid UI_FormCatalog_id
        {
            get
            {
                if (m_arrNewVal.ContainsKey("ui_formcatalog_id"))
                    return m_arrNewVal["ui_formcatalog_id"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {        
                if (m_arrNewVal.ContainsKey("ui_formcatalog_id"))
                    m_arrNewVal["ui_formcatalog_id"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("ui_formcatalog_id", val);
                }
            }
        }
        public int Width
        {
            get
            {
                if (m_arrNewVal.ContainsKey("width"))
                    return m_arrNewVal["width"].IntVal;
                else
                    return 0;
            }
            set
            {           
                if (m_arrNewVal.ContainsKey("width"))
                    m_arrNewVal["width"].IntVal = value;
                else
                {
                    CValue val = new CValue();
                    val.IntVal = value;
                    m_arrNewVal.Add("width", val);
                }
            }
        }
        public int Height
        {
            get
            {
                if (m_arrNewVal.ContainsKey("height"))
                    return m_arrNewVal["height"].IntVal;
                else
                    return 0;
            }
            set
            {           
                if (m_arrNewVal.ContainsKey("height"))
                    m_arrNewVal["height"].IntVal = value;
                else
                {
                    CValue val = new CValue();
                    val.IntVal = value;
                    m_arrNewVal.Add("height", val);
                }
            }
        }


        public CFormControlMgr FormControlMgr
        {
            get
            {
                return (CFormControlMgr)this.GetSubObjectMgr("UI_FormControl", typeof(CFormControlMgr));
            }
            set
            {
                this.SetSubObjectMgr("UI_FormControl", value);
            }
        }

    }
}