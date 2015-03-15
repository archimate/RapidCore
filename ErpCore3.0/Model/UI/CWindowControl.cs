// File:    CWindowControl.cs
// Author:  甘孝俭
// email:   ganxiaojian@hotmail.com 
// QQ:      154986287
// http://www.8088net.com
// 协议声明：本软件为开源系统，遵循国际开源组织协议。任何单位或个人可以使用或修改本软件源码，
//          可以用于作为非商业或商业用途，但由于使用本源码所引起的一切后果与作者无关。
//          未经作者许可，禁止任何企业或个人直接出售本源码或者把本软件作为独立的功能进行销售活动，
//          作者将保留追究责任的权利。
// Created: 2011年8月31日 12:40:36
// Purpose: Definition of Class CWindowControl
using System;
using System.Collections.Generic;
using System.Text;
using ErpCoreModel.Framework;

namespace ErpCoreModel.UI
{
    public class CWindowControl : CBaseObject
    {
        public CWindowControl()
        {
            TbCode = "UI_WindowControl";
            ClassName = "ErpCoreModel.UI.CWindowControl";

            Name = "";
            UI_Window_id = Guid.Empty;
            CtrlType = ControlType.TableGrid;
            Dock = 0;
            Idx = 0;
            ShowToolBar = true;
            ShowTitleBar = true;
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
        public ControlType CtrlType
        {
            get
            {
                if (m_arrNewVal.ContainsKey("ctrltype"))
                    return (ControlType)m_arrNewVal["ctrltype"].IntVal;
                else
                    return ControlType.TableGrid;
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
        public Guid UI_Window_id
        {
            get
            {
                if (m_arrNewVal.ContainsKey("ui_window_id"))
                    return m_arrNewVal["ui_window_id"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("ui_window_id"))
                    m_arrNewVal["ui_window_id"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("ui_window_id", val);
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
        public int Dock
        {
            get
            {
                if (m_arrNewVal.ContainsKey("dock"))
                    return m_arrNewVal["dock"].IntVal;
                else
                    return 0;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("dock"))
                    m_arrNewVal["dock"].IntVal = value;
                else
                {
                    CValue val = new CValue();
                    val.IntVal = value;
                    m_arrNewVal.Add("dock", val);
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


        public CLinkageWindowControlMgr LinkageWindowControlMgr
        {
            get
            {
                return (CLinkageWindowControlMgr)this.GetSubObjectMgr("UI_LinkageWindowControl", typeof(CLinkageWindowControlMgr));
            }
            set
            {
                this.SetSubObjectMgr("UI_LinkageWindowControl", value);
            }
        }
        public CNavigateBarButtonMgr NavigateBarButtonMgr
        {
            get
            {
                return (CNavigateBarButtonMgr)this.GetSubObjectMgr("UI_NavigateBarButton", typeof(CNavigateBarButtonMgr));
            }
            set
            {
                this.SetSubObjectMgr("UI_NavigateBarButton", value);
            }
        }
        public CTableInWindowControlMgr TableInWindowControlMgr
        {
            get
            {
                return (CTableInWindowControlMgr)this.GetSubObjectMgr("UI_TableInWindowControl", typeof(CTableInWindowControlMgr));
            }
            set
            {
                this.SetSubObjectMgr("UI_TableInWindowControl", value);
            }
        }

        public CTButtonInWindowControlMgr TButtonInWindowControlMgr
        {
            get
            {
                return (CTButtonInWindowControlMgr)this.GetSubObjectMgr("UI_TButtonInWindowControl", typeof(CTButtonInWindowControlMgr));
            }
            set
            {
                this.SetSubObjectMgr("UI_TButtonInWindowControl", value);
            }
        }


    }
}
