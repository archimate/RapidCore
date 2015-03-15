// File:    CDesktop.cs
// Created: 2012-08-22 14:39:34
// Purpose: Definition of Class CDesktop

using System;
using System.Text;
using ErpCoreModel.Framework;

namespace ErpCoreModel.UI
{
    
    public class CDesktop : CBaseObject
    {

        public CDesktop()
        {
            TbCode = "UI_Desktop";
            ClassName = "ErpCoreModel.UI.CDesktop";

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
        public string BackImg
        {
            get
            {
                if (m_arrNewVal.ContainsKey("backimg"))
                    return m_arrNewVal["backimg"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("backimg"))
                    m_arrNewVal["backimg"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("backimg", val);
                }
            }
        }
    }
}