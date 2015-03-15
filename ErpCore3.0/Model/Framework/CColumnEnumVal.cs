// File:    CColumnEnumVal.cs
// Created: 2012/7/8 8:16:12
// Purpose: Definition of Class CColumnEnumVal

using System;
using System.Text;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Framework
{
    
    public class CColumnEnumVal : CBaseObject
    {

        public CColumnEnumVal()
        {
            TbCode = "FW_ColumnEnumVal";
            ClassName = "ErpCoreModel.Framework.CColumnEnumVal";

        }

        
        public Guid FW_Column_id
        {
            get
            {
                if (m_arrNewVal.ContainsKey("fw_column_id"))
                    return m_arrNewVal["fw_column_id"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {        
                if (m_arrNewVal.ContainsKey("fw_column_id"))
                    m_arrNewVal["fw_column_id"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("fw_column_id", val);
                }
            }
        }
        public string Val
        {
            get
            {
                if (m_arrNewVal.ContainsKey("val"))
                    return m_arrNewVal["val"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("val"))
                    m_arrNewVal["val"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("val", val);
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