// File:    CViewAccessInUser.cs
// Created: 2012/7/3 23:07:26
// Purpose: Definition of Class CViewAccessInUser

using System;
using System.Text;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Base
{
    
    public class CViewAccessInUser : CBaseObject
    {

        public CViewAccessInUser()
        {
            TbCode = "B_ViewAccessInUser";
            ClassName = "ErpCoreModel.Base.CViewAccessInUser";

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