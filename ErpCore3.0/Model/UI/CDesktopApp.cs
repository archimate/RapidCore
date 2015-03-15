// File:    CDesktopApp.cs
// Created: 2012-08-22 14:39:34
// Purpose: Definition of Class CDesktopApp

using System;
using System.Text;
using ErpCoreModel.Framework;

namespace ErpCoreModel.UI
{
    
    public class CDesktopApp : CBaseObject
    {

        public CDesktopApp()
        {
            TbCode = "UI_DesktopApp";
            ClassName = "ErpCoreModel.UI.CDesktopApp";

        }

        
        public Guid B_User_id
        {
            get
            {
                if (m_arrNewVal.ContainsKey("b_user_id"))
                    return m_arrNewVal["b_user_id"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {        
                if (m_arrNewVal.ContainsKey("b_user_id"))
                    m_arrNewVal["b_user_id"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("b_user_id", val);
                }
            }
        }
        public string Url
        {
            get
            {
                if (m_arrNewVal.ContainsKey("url"))
                    return m_arrNewVal["url"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("url"))
                    m_arrNewVal["url"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("url", val);
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
        public string IconUrl
        {
            get
            {
                if (m_arrNewVal.ContainsKey("iconurl"))
                    return m_arrNewVal["iconurl"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("iconurl"))
                    m_arrNewVal["iconurl"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("iconurl", val);
                }
            }
        }
        public Guid UI_DesktopGroup_id
        {
            get
            {
                if (m_arrNewVal.ContainsKey("ui_desktopgroup_id"))
                    return m_arrNewVal["ui_desktopgroup_id"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("ui_desktopgroup_id"))
                    m_arrNewVal["ui_desktopgroup_id"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("ui_desktopgroup_id", val);
                }
            }
        }
        public int OpenwinWidth
        {
            get
            {
                if (m_arrNewVal.ContainsKey("openwinwidth"))
                    return m_arrNewVal["openwinwidth"].IntVal;
                else
                    return 0;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("openwinwidth"))
                    m_arrNewVal["openwinwidth"].IntVal = value;
                else
                {
                    CValue val = new CValue();
                    val.IntVal = value;
                    m_arrNewVal.Add("openwinwidth", val);
                }
            }
        }
        public int OpenwinHeight
        {
            get
            {
                if (m_arrNewVal.ContainsKey("openwinheight"))
                    return m_arrNewVal["openwinheight"].IntVal;
                else
                    return 0;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("openwinheight"))
                    m_arrNewVal["openwinheight"].IntVal = value;
                else
                {
                    CValue val = new CValue();
                    val.IntVal = value;
                    m_arrNewVal.Add("openwinheight", val);
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
    }
}