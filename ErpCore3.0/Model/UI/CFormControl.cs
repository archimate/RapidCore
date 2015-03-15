// File:    CFormControl.cs
// Author:  甘孝俭
// email:   ganxiaojian@hotmail.com 
// QQ:      154986287
// http://www.8088net.com
// 协议声明：本软件为开源系统，遵循国际开源组织协议。任何单位或个人可以使用或修改本软件源码，
//          可以用于作为非商业或商业用途，但由于使用本源码所引起的一切后果与作者无关。
//          未经作者许可，禁止任何企业或个人直接出售本源码或者把本软件作为独立的功能进行销售活动，
//          作者将保留追究责任的权利。
// Created: 2011-10-7 14:13:18
// Purpose: Definition of Class CFormControl

using System;
using System.Text;
using System.Collections.Generic;
using ErpCoreModel.Framework;

namespace ErpCoreModel.UI
{
    public enum DomainControlType {Data,Form };

    public class CFormControl : CBaseObject
    {

        public CFormControl()
        {
            TbCode = "UI_FormControl";
            ClassName = "ErpCoreModel.UI.CFormControl";

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
        public Guid UI_Form_id
        {
            get
            {
                if (m_arrNewVal.ContainsKey("ui_form_id"))
                    return m_arrNewVal["ui_form_id"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {        
                if (m_arrNewVal.ContainsKey("ui_form_id"))
                    m_arrNewVal["ui_form_id"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("ui_form_id", val);
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
        public int CtrlType
        {
            get
            {
                if (m_arrNewVal.ContainsKey("ctrltype"))
                    return m_arrNewVal["ctrltype"].IntVal;
                else
                    return 0;
            }
            set
            {           
                if (m_arrNewVal.ContainsKey("ctrltype"))
                    m_arrNewVal["ctrltype"].IntVal = (int)value;
                else
                {
                    CValue val = new CValue();
                    val.IntVal = (int)value;
                    m_arrNewVal.Add("ctrltype", val);
                }
            }
        }
        public DomainControlType DomainType
        {
            get
            {
                if (m_arrNewVal.ContainsKey("domaintype"))
                    return (DomainControlType)m_arrNewVal["domaintype"].IntVal;
                else
                    return DomainControlType.Form;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("domaintype"))
                    m_arrNewVal["domaintype"].IntVal = (int)value;
                else
                {
                    CValue val = new CValue();
                    val.IntVal = (int)value;
                    m_arrNewVal.Add("domaintype", val);
                }
            }
        }
        public bool ShowToolBar
        {
            get
            {
                if (m_arrNewVal.ContainsKey("showtoolbar"))
                    return m_arrNewVal["showtoolbar"].BoolVal;
                else
                    return false;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("showtoolbar"))
                    m_arrNewVal["showtoolbar"].BoolVal = value;
                else
                {
                    CValue val = new CValue();
                    val.BoolVal = value;
                    m_arrNewVal.Add("showtoolbar", val);
                }
            }
        }
        public bool ShowTitleBar
        {
            get
            {
                if (m_arrNewVal.ContainsKey("showtitlebar"))
                    return m_arrNewVal["showtitlebar"].BoolVal;
                else
                    return false;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("showtitlebar"))
                    m_arrNewVal["showtitlebar"].BoolVal = value;
                else
                {
                    CValue val = new CValue();
                    val.BoolVal = value;
                    m_arrNewVal.Add("showtitlebar", val);
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

        public CTableInFormControlMgr TableInFormControlMgr
        {
            get
            {
                return (CTableInFormControlMgr)this.GetSubObjectMgr("UI_TableInFormControl", typeof(CTableInFormControlMgr));
            }
            set
            {
                this.SetSubObjectMgr("UI_TableInFormControl", value);
            }
        }

        public CTButtonInFormControlMgr TButtonInFormControlMgr
        {
            get
            {
                return (CTButtonInFormControlMgr)this.GetSubObjectMgr("UI_TButtonInFormControl", typeof(CTButtonInFormControlMgr));
            }
            set
            {
                this.SetSubObjectMgr("UI_TButtonInFormControl", value);
            }
        }


    }
}