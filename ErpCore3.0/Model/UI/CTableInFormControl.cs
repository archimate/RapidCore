// File:    CTableInFormControl.cs
// Author:  甘孝俭
// email:   ganxiaojian@hotmail.com 
// QQ:      154986287
// http://www.8088net.com
// 协议声明：本软件为开源系统，遵循国际开源组织协议。任何单位或个人可以使用或修改本软件源码，
//          可以用于作为非商业或商业用途，但由于使用本源码所引起的一切后果与作者无关。
//          未经作者许可，禁止任何企业或个人直接出售本源码或者把本软件作为独立的功能进行销售活动，
//          作者将保留追究责任的权利。
// Created: 2011-10-7 14:13:18
// Purpose: Definition of Class CTableInFormControl

using System;
using System.Text;
using System.Collections.Generic;
using ErpCoreModel.Framework;

namespace ErpCoreModel.UI
{
    
    public class CTableInFormControl : CBaseObject
    {

        public CTableInFormControl()
        {
            TbCode = "UI_TableInFormControl";
            ClassName = "ErpCoreModel.UI.CTableInFormControl";

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
        public Guid UI_FormControl_id
        {
            get
            {
                if (m_arrNewVal.ContainsKey("ui_formcontrol_id"))
                    return m_arrNewVal["ui_formcontrol_id"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {        
                if (m_arrNewVal.ContainsKey("ui_formcontrol_id"))
                    m_arrNewVal["ui_formcontrol_id"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("ui_formcontrol_id", val);
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
        public string QueryFilter
        {
            get
            {
                if (m_arrNewVal.ContainsKey("queryfilter"))
                    return m_arrNewVal["queryfilter"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("queryfilter"))
                    m_arrNewVal["queryfilter"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("queryfilter", val);
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
        public string Text
        {
            get
            {
                if (m_arrNewVal.ContainsKey("text"))
                    return m_arrNewVal["text"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("text"))
                    m_arrNewVal["text"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("text", val);
                }
            }
        }
        public bool IsLoop
        {
            get
            {
                if (m_arrNewVal.ContainsKey("isloop"))
                    return m_arrNewVal["isloop"].BoolVal;
                else
                    return false;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("isloop"))
                    m_arrNewVal["isloop"].BoolVal = value;
                else
                {
                    CValue val = new CValue();
                    val.BoolVal = value;
                    m_arrNewVal.Add("isloop", val);
                }
            }
        }
        public Guid NodeIDCol
        {
            get
            {
                if (m_arrNewVal.ContainsKey("nodeidcol"))
                    return m_arrNewVal["nodeidcol"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {        
                if (m_arrNewVal.ContainsKey("nodeidcol"))
                    m_arrNewVal["nodeidcol"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("nodeidcol", val);
                }
            }
        }
        public Guid PNodeIDCol
        {
            get
            {
                if (m_arrNewVal.ContainsKey("pnodeidcol"))
                    return m_arrNewVal["pnodeidcol"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {        
                if (m_arrNewVal.ContainsKey("pnodeidcol"))
                    m_arrNewVal["pnodeidcol"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("pnodeidcol", val);
                }
            }
        }
        public string RootFilter
        {
            get
            {
                if (m_arrNewVal.ContainsKey("rootfilter"))
                    return m_arrNewVal["rootfilter"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("rootfilter"))
                    m_arrNewVal["rootfilter"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("rootfilter", val);
                }
            }
        }

        public CColumnInTableInFormControlMgr ColumnInTableInFormControlMgr
        {
            get
            {
                return (CColumnInTableInFormControlMgr)this.GetSubObjectMgr("UI_ColumnInTableInFormControl", typeof(CColumnInTableInFormControlMgr));
            }
            set
            {
                this.SetSubObjectMgr("UI_ColumnInTableInFormControl", value);
            }
        }
        public CTButtonInTableInFormControlMgr TButtonInTableInFormControlMgr
        {
            get
            {
                return (CTButtonInTableInFormControlMgr)this.GetSubObjectMgr("UI_TButtonInTableInFormControl", typeof(CTButtonInTableInFormControlMgr));
            }
            set
            {
                this.SetSubObjectMgr("UI_TButtonInTableInFormControl", value);
            }
        }

    }
}