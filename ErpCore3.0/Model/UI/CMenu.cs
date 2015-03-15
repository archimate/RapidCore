// File:    CMenu.cs
// Created: 2012-08-13 23:16:13
// Purpose: Definition of Class CMenu

using System;
using System.Text;
using ErpCoreModel.Framework;

namespace ErpCoreModel.UI
{
    //0-分级菜单（导航用）；1-视图菜单（链接视图）;2-窗体视图；3-url菜单；报表菜单
    public enum enumMenuType { CatalogMenu,ViewMenu,WindowMenu,UrlMenu,ReportMenu}
    public class CMenu : CBaseObject
    {

        public CMenu()
        {
            TbCode = "UI_Menu";
            ClassName = "ErpCoreModel.UI.CMenu";

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
        public enumMenuType MType
        {
            get
            {
                if (m_arrNewVal.ContainsKey("mtype"))
                    return (enumMenuType)m_arrNewVal["mtype"].IntVal;
                else
                    return enumMenuType.CatalogMenu;
            }
            set
            {           
                if (m_arrNewVal.ContainsKey("mtype"))
                    m_arrNewVal["mtype"].IntVal = (int)value;
                else
                {
                    CValue val = new CValue();
                    val.IntVal = (int)value;
                    m_arrNewVal.Add("mtype", val);
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
        public Guid RPT_Report_id
        {
            get
            {
                if (m_arrNewVal.ContainsKey("rpt_report_id"))
                    return m_arrNewVal["rpt_report_id"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("rpt_report_id"))
                    m_arrNewVal["rpt_report_id"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("rpt_report_id", val);
                }
            }
        }
        public Guid Parent_id
        {
            get
            {
                if (m_arrNewVal.ContainsKey("parent_id"))
                    return m_arrNewVal["parent_id"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {        
                if (m_arrNewVal.ContainsKey("parent_id"))
                    m_arrNewVal["parent_id"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("parent_id", val);
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
    }
}