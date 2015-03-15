// File:    CCity.cs
// Created: 2012/3/22 8:42:03
// Purpose: Definition of Class CCity

using System;
using System.Text;
using System.Collections.Generic;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Base
{
    
    public class CCity : CBaseObject
    {

        public CCity()
        {
            TbCode = "B_City";
            ClassName = "ErpCoreModel.Base.CCity";

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
        public Guid B_Province_id
        {
            get
            {
                if (m_arrNewVal.ContainsKey("b_province_id"))
                    return m_arrNewVal["b_province_id"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {        
                if (m_arrNewVal.ContainsKey("b_province_id"))
                    m_arrNewVal["b_province_id"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("b_province_id", val);
                }
            }
        }


        public CDistrictMgr DistrictMgr
        {
            get
            {
                return (CDistrictMgr)this.GetSubObjectMgr("B_District", typeof(CDistrictMgr));
            }
            set
            {
                this.SetSubObjectMgr("B_District", value);
            }
        }

    }
}