// File:    CDistrict.cs
// Created: 2012/3/22 8:42:03
// Purpose: Definition of Class CDistrict

using System;
using System.Text;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Base
{
    
    public class CDistrict : CBaseObject
    {

        public CDistrict()
        {
            TbCode = "B_District";
            ClassName = "ErpCoreModel.Base.CDistrict";

        }


        public string Zipcode
        {
            get
            {
                if (m_arrNewVal.ContainsKey("zipcode"))
                    return m_arrNewVal["zipcode"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("zipcode"))
                    m_arrNewVal["zipcode"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("zipcode", val);
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
        public Guid B_City_id
        {
            get
            {
                if (m_arrNewVal.ContainsKey("b_city_id"))
                    return m_arrNewVal["b_city_id"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {        
                if (m_arrNewVal.ContainsKey("b_city_id"))
                    m_arrNewVal["b_city_id"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("b_city_id", val);
                }
            }
        }
    }
}