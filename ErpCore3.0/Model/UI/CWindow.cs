// File:    CWindow.cs
// Author:  甘孝俭
// email:   ganxiaojian@hotmail.com 
// QQ:      154986287
// http://www.8088net.com
// 协议声明：本软件为开源系统，遵循国际开源组织协议。任何单位或个人可以使用或修改本软件源码，
//          可以用于作为非商业或商业用途，但由于使用本源码所引起的一切后果与作者无关。
//          未经作者许可，禁止任何企业或个人直接出售本源码或者把本软件作为独立的功能进行销售活动，
//          作者将保留追究责任的权利。
// Created: 2011年7月9日 12:40:36
// Purpose: Definition of Class CWindow
using System;
using System.Collections.Generic;
using System.Text;
using ErpCoreModel.Framework;

namespace ErpCoreModel.UI
{
    public class CWindow : CBaseObject
    {

        public CWindow()
        {
            TbCode = "UI_Window";
            ClassName = "ErpCoreModel.UI.CWindow";

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
        public Guid UI_WindowCatalog_id
        {
            get
            {
                if (m_arrNewVal.ContainsKey("ui_windowcatalog_id"))
                    return m_arrNewVal["ui_windowcatalog_id"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("ui_windowcatalog_id"))
                    m_arrNewVal["ui_windowcatalog_id"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("ui_windowcatalog_id", val);
                }
            }
        }
        public int TopPanelHeight
        {
            get
            {
                if (m_arrNewVal.ContainsKey("toppanelheight"))
                    return m_arrNewVal["toppanelheight"].IntVal;
                else
                    return 0;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("toppanelheight"))
                    m_arrNewVal["toppanelheight"].IntVal = value;
                else
                {
                    CValue val = new CValue();
                    val.IntVal = value;
                    m_arrNewVal.Add("toppanelheight", val);
                }
            }
        }
        public bool TopPanelVisible
        {
            get
            {
                if (m_arrNewVal.ContainsKey("toppanelvisible"))
                    return m_arrNewVal["toppanelvisible"].BoolVal;
                else
                    return false;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("toppanelvisible"))
                    m_arrNewVal["toppanelvisible"].BoolVal = value;
                else
                {
                    CValue val = new CValue();
                    val.BoolVal = value;
                    m_arrNewVal.Add("toppanelvisible", val);
                }
            }
        }
        public int BottomPanelHeight
        {
            get
            {
                if (m_arrNewVal.ContainsKey("bottompanelheight"))
                    return m_arrNewVal["bottompanelheight"].IntVal;
                else
                    return 0;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("bottompanelheight"))
                    m_arrNewVal["bottompanelheight"].IntVal = value;
                else
                {
                    CValue val = new CValue();
                    val.IntVal = value;
                    m_arrNewVal.Add("bottompanelheight", val);
                }
            }
        }
        public bool BottomPanelVisible
        {
            get
            {
                if (m_arrNewVal.ContainsKey("bottompanelvisible"))
                    return m_arrNewVal["bottompanelvisible"].BoolVal;
                else
                    return false;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("bottompanelvisible"))
                    m_arrNewVal["bottompanelvisible"].BoolVal = value;
                else
                {
                    CValue val = new CValue();
                    val.BoolVal = value;
                    m_arrNewVal.Add("bottompanelvisible", val);
                }
            }
        }
        public int LeftPanelWidth
        {
            get
            {
                if (m_arrNewVal.ContainsKey("leftpanelwidth"))
                    return m_arrNewVal["leftpanelwidth"].IntVal;
                else
                    return 0;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("leftpanelwidth"))
                    m_arrNewVal["leftpanelwidth"].IntVal = value;
                else
                {
                    CValue val = new CValue();
                    val.IntVal = value;
                    m_arrNewVal.Add("leftpanelwidth", val);
                }
            }
        }
        public bool LeftPanelVisible
        {
            get
            {
                if (m_arrNewVal.ContainsKey("leftpanelvisible"))
                    return m_arrNewVal["leftpanelvisible"].BoolVal;
                else
                    return false;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("leftpanelvisible"))
                    m_arrNewVal["leftpanelvisible"].BoolVal = value;
                else
                {
                    CValue val = new CValue();
                    val.BoolVal = value;
                    m_arrNewVal.Add("leftpanelvisible", val);
                }
            }
        }
        public int RightPanelWidth
        {
            get
            {
                if (m_arrNewVal.ContainsKey("rightpanelwidth"))
                    return m_arrNewVal["rightpanelwidth"].IntVal;
                else
                    return 0;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("rightpanelwidth"))
                    m_arrNewVal["rightpanelwidth"].IntVal = value;
                else
                {
                    CValue val = new CValue();
                    val.IntVal = value;
                    m_arrNewVal.Add("rightpanelwidth", val);
                }
            }
        }
        public bool RightPanelVisible
        {
            get
            {
                if (m_arrNewVal.ContainsKey("rightpanelvisible"))
                    return m_arrNewVal["rightpanelvisible"].BoolVal;
                else
                    return false;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("rightpanelvisible"))
                    m_arrNewVal["rightpanelvisible"].BoolVal = value;
                else
                {
                    CValue val = new CValue();
                    val.BoolVal = value;
                    m_arrNewVal.Add("rightpanelvisible", val);
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

        public CWindowControlMgr WindowControlMgr
        {
            get
            {
                return (CWindowControlMgr)this.GetSubObjectMgr("UI_WindowControl", typeof(CWindowControlMgr));
            }
            set
            {
                this.SetSubObjectMgr("UI_WindowControl", value);
            }
        }

    }
}
