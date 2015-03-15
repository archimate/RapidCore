// File:    CColumn.cs
// Author:  甘孝俭
// email:   ganxiaojian@hotmail.com 
// QQ:      154986287
// http://www.8088net.com
// 协议声明：本软件为开源系统，遵循国际开源组织协议。任何单位或个人可以使用或修改本软件源码，
//          可以用于作为非商业或商业用途，但由于使用本源码所引起的一切后果与作者无关。
//          未经作者许可，禁止任何企业或个人直接出售本源码或者把本软件作为独立的功能进行销售活动，
//          作者将保留追究责任的权利。
// Created: 2011年7月9日 12:40:36
// Purpose: Definition of Class CColumn

using System;
using System.Text;
using System.Collections.Generic;

namespace ErpCoreModel.Framework
{


    public class CColumn : CBaseObject
    {
        private string m_sUploadPath = "";//附件型类型字段的文件上传路径

        public CColumn()
        {
            TbCode = "FW_Column";
            ClassName = "ErpCoreModel.Framework.CColumn";

            FW_Table_id = Guid.Empty;
            Name = "";
            Code = "";
            IsSystem = false;
            ColType = ColumnType.string_type;
            ColLen = 0;
            ColDecimal = 0;
            RefTable = Guid.Empty;
            RefCol = Guid.Empty;
            RefShowCol = Guid.Empty;
            Formula = "";
            DefaultValue = "";
            AllowNull = true;
            UIControl = "";
            WebUIControl = "";
            Commit();
        }

        public string UploadPath
        {
            get
            {
                if (this.m_sUploadPath != "")
                    return this.m_sUploadPath;
                else
                    return Ctx.UploadPath;
            }
            set
            {
                this.m_sUploadPath = value;
            }
        }
        public Guid FW_Table_id
        {
            get
            {
                if (m_arrNewVal.ContainsKey("fw_table_id"))
                    return m_arrNewVal["fw_table_id"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("fw_table_id"))
                    m_arrNewVal["fw_table_id"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("fw_table_id", val);
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

        public string Code
        {
            get
            {
                if (m_arrNewVal.ContainsKey("code"))
                    return m_arrNewVal["code"].StrVal;
                else
                    return "";
            }
            set
            {              
                if (m_arrNewVal.ContainsKey("code"))
                    m_arrNewVal["code"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("code", val);
                }
            }
        }

        public bool IsSystem
        {
            get
            {
                if (m_arrNewVal.ContainsKey("issystem"))
                    return m_arrNewVal["issystem"].BoolVal;
                else
                    return false;
            }
            set
            {             
                if (m_arrNewVal.ContainsKey("issystem"))
                    m_arrNewVal["issystem"].BoolVal = value;
                else
                {
                    CValue val = new CValue();
                    val.BoolVal = value;
                    m_arrNewVal.Add("issystem", val);
                }
            }
        }

        public ColumnType ColType
        {
            get
            {
                if (m_arrNewVal.ContainsKey("coltype"))
                    return (ColumnType)m_arrNewVal["coltype"].IntVal;
                else
                    return ColumnType.string_type;
            }
            set
            {          
                if (m_arrNewVal.ContainsKey("coltype"))
                    m_arrNewVal["coltype"].IntVal = Convert.ToInt32(value);
                else
                {
                    CValue val = new CValue();
                    val.IntVal = Convert.ToInt32(value);
                    m_arrNewVal.Add("coltype", val);
                }
            }
        }

        public int ColLen
        {
            get
            {
                if (m_arrNewVal.ContainsKey("collen"))
                    return m_arrNewVal["collen"].IntVal;
                else
                    return 0;
            }
            set
            {           
                if (m_arrNewVal.ContainsKey("collen"))
                    m_arrNewVal["collen"].IntVal = value;
                else
                {
                    CValue val = new CValue();
                    val.IntVal = value;
                    m_arrNewVal.Add("collen", val);
                }
            }
        }

        public int ColDecimal
        {
            get
            {
                //return colDecimal;
                if (m_arrNewVal.ContainsKey("coldecimal"))
                    return m_arrNewVal["coldecimal"].IntVal;
                else
                    return 0;
            }
            set
            {
                //this.colDecimal = value;           
                if (m_arrNewVal.ContainsKey("coldecimal"))
                    m_arrNewVal["coldecimal"].IntVal = value;
                else
                {
                    CValue val = new CValue();
                    val.IntVal = value;
                    m_arrNewVal.Add("coldecimal", val);
                }
            }
        }

        public Guid RefTable
        {
            get
            {
                if (m_arrNewVal.ContainsKey("reftable"))
                    return m_arrNewVal["reftable"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {        
                if (m_arrNewVal.ContainsKey("reftable"))
                    m_arrNewVal["reftable"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("reftable", val);
                }
            }
        }

        public Guid RefCol
        {
            get
            {
                if (m_arrNewVal.ContainsKey("refcol"))
                    return m_arrNewVal["refcol"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {       
                if (m_arrNewVal.ContainsKey("refcol"))
                    m_arrNewVal["refcol"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("refcol", val);
                }
            }
        }

        public Guid RefShowCol
        {
            get
            {
                if (m_arrNewVal.ContainsKey("refshowcol"))
                    return m_arrNewVal["refshowcol"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {    
                if (m_arrNewVal.ContainsKey("refshowcol"))
                    m_arrNewVal["refshowcol"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("refshowcol", val);
                }
            }
        }

        public string Formula
        {
            get
            {
                if (m_arrNewVal.ContainsKey("formula"))
                    return m_arrNewVal["formula"].StrVal;
                else
                    return "";
            }
            set
            {    
                if (m_arrNewVal.ContainsKey("formula"))
                    m_arrNewVal["formula"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("formula", val);
                }
            }
        }

        public string DefaultValue
        {
            get
            {
                if (m_arrNewVal.ContainsKey("defaultvalue"))
                    return m_arrNewVal["defaultvalue"].StrVal;
                else
                    return "";
            }
            set
            {  
                if (m_arrNewVal.ContainsKey("defaultvalue"))
                    m_arrNewVal["defaultvalue"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("defaultvalue", val);
                }
            }
        }
        public bool AllowNull
        {
            get
            {
                if (m_arrNewVal.ContainsKey("allownull"))
                    return m_arrNewVal["allownull"].BoolVal;
                else
                    return false;
            }
            set
            {  
                if (m_arrNewVal.ContainsKey("allownull"))
                    m_arrNewVal["allownull"].BoolVal = value;
                else
                {
                    CValue val = new CValue();
                    val.BoolVal = value;
                    m_arrNewVal.Add("allownull", val);
                }
            }
        }

        public string UIControl
        {
            get
            {
                if (m_arrNewVal.ContainsKey("uicontrol"))
                    return m_arrNewVal["uicontrol"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("uicontrol"))
                    m_arrNewVal["uicontrol"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("uicontrol", val);
                }
            }
        }

        public string WebUIControl
        {
            get
            {
                if (m_arrNewVal.ContainsKey("webuicontrol"))
                    return m_arrNewVal["webuicontrol"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("webuicontrol"))
                    m_arrNewVal["webuicontrol"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("webuicontrol", val);
                }
            }
        }

        public bool IsVisible
        {
            get
            {
                if (m_arrNewVal.ContainsKey("isvisible"))
                    return m_arrNewVal["isvisible"].BoolVal;
                else
                    return false;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("isvisible"))
                    m_arrNewVal["isvisible"].BoolVal = value;
                else
                {
                    CValue val = new CValue();
                    val.BoolVal = value;
                    m_arrNewVal.Add("isvisible", val);
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

        public bool IsUnique
        {
            get
            {
                if (m_arrNewVal.ContainsKey("isunique"))
                    return m_arrNewVal["isunique"].BoolVal;
                else
                    return false;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("isunique"))
                    m_arrNewVal["isunique"].BoolVal = value;
                else
                {
                    CValue val = new CValue();
                    val.BoolVal = value;
                    m_arrNewVal.Add("isunique", val);
                }
            }
        }

        static public string ConvertColTypeToString(ColumnType ColType)
        {
            switch (ColType)
            {
                case ColumnType.string_type:
                    return "字符型";
                case ColumnType.int_type:
                    return "整型";
                case ColumnType.long_type:
                    return "长整型";
                case ColumnType.bool_type:
                    return "布尔型";
                case ColumnType.numeric_type:
                    return "数值型";
                case ColumnType.datetime_type:
                    return "日期型";
                case ColumnType.text_type:
                    return "备注型";
                case ColumnType.object_type:
                    return "二进制";
                case ColumnType.ref_type:
                    return "引用型";
                case ColumnType.guid_type:
                    return "GUID";
                case ColumnType.enum_type:
                    return "枚举型";
                case ColumnType.path_type:
                    return "附件型";
            }
            return "未知类型";
        }
        static public ColumnType ConvertStringToColType(string sType)
        {
            switch (sType)
            {
                case "字符型":
                    return ColumnType.string_type;
                case "整型":
                    return ColumnType.int_type;
                case "长整型":
                    return ColumnType.long_type;
                case "布尔型":
                    return ColumnType.bool_type;
                case "数值型":
                    return ColumnType.numeric_type;
                case "日期型":
                    return ColumnType.datetime_type;
                case "备注型":
                    return ColumnType.text_type;
                case "二进制":
                    return ColumnType.object_type;
                case "引用型":
                    return ColumnType.ref_type;
                case "GUID":
                    return ColumnType.guid_type;
                case "枚举型":
                    return ColumnType.enum_type;
                case "附件型":
                    return ColumnType.path_type;
            }
            return ColumnType.string_type;
        }



        public CColumnEnumValMgr ColumnEnumValMgr
        {
            get
            {
                return (CColumnEnumValMgr)this.GetSubObjectMgr("FW_ColumnEnumVal", typeof(CColumnEnumValMgr));
            }
            set
            {
                this.SetSubObjectMgr("FW_ColumnEnumVal", value);
            }
        }

    }
}