// File:    CSystemAccessInRole.cs
// Created: 2012/7/3 23:07:26
// Purpose: Definition of Class CSystemAccessInRole

using System;
using System.Text;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Base
{
    
    public class CSystemAccessInRole : CBaseObject
    {

        public CSystemAccessInRole()
        {
            TbCode = "B_SystemAccessInRole";
            ClassName = "ErpCoreModel.Base.CSystemAccessInRole";

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
        public Guid S_System_id
        {
            get
            {
                if (m_arrNewVal.ContainsKey("s_system_id"))
                    return m_arrNewVal["s_system_id"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {        
                if (m_arrNewVal.ContainsKey("s_system_id"))
                    m_arrNewVal["s_system_id"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("s_system_id", val);
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