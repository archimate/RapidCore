// File:    CProvince.cs
// Created: 2012/3/22 8:42:03
// Purpose: Definition of Class CProvince

using System;
using System.Text;
using System.Collections.Generic;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Base
{
    
    public class CProvince : CBaseObject
    {

        public CProvince()
        {
            TbCode = "B_Province";
            ClassName = "ErpCoreModel.Base.CProvince";

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


        public CCityMgr CityMgr
        {
            get
            {
                return (CCityMgr)this.GetSubObjectMgr("B_City", typeof(CCityMgr));
            }
            set
            {
                this.SetSubObjectMgr("B_City", value);
            }
        }

    }
}