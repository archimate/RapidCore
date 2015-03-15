// File:    CRoleMenu.cs
// Created: 2012-08-15 08:58:33
// Purpose: Definition of Class CRoleMenu

using System;
using System.Text;
using ErpCoreModel.Framework;

namespace ErpCoreModel.UI
{
    
    public class CRoleMenu : CBaseObject
    {

        public CRoleMenu()
        {
            TbCode = "UI_RoleMenu";
            ClassName = "ErpCoreModel.UI.CRoleMenu";

        }

        
        public Guid B_Role_id
        {
            get
            {
                if (m_arrNewVal.ContainsKey("b_role_id"))
                    return m_arrNewVal["b_role_id"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {        
                if (m_arrNewVal.ContainsKey("b_role_id"))
                    m_arrNewVal["b_role_id"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("b_role_id", val);
                }
            }
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