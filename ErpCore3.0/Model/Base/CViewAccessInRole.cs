// File:    CViewAccessInRole.cs
// Created: 2012/7/3 23:07:26
// Purpose: Definition of Class CViewAccessInRole

using System;
using System.Text;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Base
{
    
    public class CViewAccessInRole : CBaseObject
    {

        public CViewAccessInRole()
        {
            TbCode = "B_ViewAccessInRole";
            ClassName = "ErpCoreModel.Base.CViewAccessInRole";

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
        public AccessType Access
        {
            get
            {
                if (m_arrNewVal.ContainsKey("access"))
                    return (AccessType)m_arrNewVal["access"].IntVal;
                else
                    return AccessType.forbide;
            }
            set
            {           
                if (m_arrNewVal.ContainsKey("access"))
                    m_arrNewVal["access"].IntVal = (int)value;
                else
                {
                    CValue val = new CValue();
                    val.IntVal = (int)value;
                    m_arrNewVal.Add("access", val);
                }
            }
        }
    }
}