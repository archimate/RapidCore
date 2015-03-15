// File:    CUserMenu.cs
// Created: 2012-08-13 23:16:13
// Purpose: Definition of Class CUserMenu

using System;
using System.Text;
using ErpCoreModel.Framework;

namespace ErpCoreModel.UI
{
    
    public class CUserMenu : CBaseObject
    {

        public CUserMenu()
        {
            TbCode = "UI_UserMenu";
            ClassName = "ErpCoreModel.UI.CUserMenu";

        }

        
        public Guid UI_Menu_id
        {
            get
            {
                if (m_arrNewVal.ContainsKey("ui_menu_id"))
                    return m_arrNewVal["ui_menu_id"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {        
                if (m_arrNewVal.ContainsKey("ui_menu_id"))
                    m_arrNewVal["ui_menu_id"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("ui_menu_id", val);
                }
            }
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