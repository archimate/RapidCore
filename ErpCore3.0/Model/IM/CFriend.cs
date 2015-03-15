// File:    CFriend.cs
// Created: 2012/10/9 22:08:37
// Purpose: Definition of Class CFriend

using System;
using System.Text;
using ErpCoreModel.Framework;

namespace ErpCoreModel.IM
{
    
    public class CFriend : CBaseObject
    {

        public CFriend()
        {
            TbCode = "IM_Friend";
            ClassName = "ErpCoreModel.IM.CFriend";

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
        public Guid Friend_id
        {
            get
            {
                if (m_arrNewVal.ContainsKey("friend_id"))
                    return m_arrNewVal["friend_id"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {        
                if (m_arrNewVal.ContainsKey("friend_id"))
                    m_arrNewVal["friend_id"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("friend_id", val);
                }
            }
        }
        public bool IsStrange
        {
            get
            {
                if (m_arrNewVal.ContainsKey("isstrange"))
                    return m_arrNewVal["isstrange"].BoolVal;
                else
                    return false;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("isstrange"))
                    m_arrNewVal["isstrange"].BoolVal = value;
                else
                {
                    CValue val = new CValue();
                    val.BoolVal = value;
                    m_arrNewVal.Add("isstrange", val);
                }
            }
        }
    }
}