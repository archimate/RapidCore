// File:    CDataServer.cs
// Author:  甘孝俭
// email:   ganxiaojian@hotmail.com 
// QQ:      154986287
// http://www.8088net.com
// 协议声明：本软件为开源系统，遵循国际开源组织协议。任何单位或个人可以使用或修改本软件源码，
//          可以用于作为非商业或商业用途，但由于使用本源码所引起的一切后果与作者无关。
//          未经作者许可，禁止任何企业或个人直接出售本源码或者把本软件作为独立的功能进行销售活动，
//          作者将保留追究责任的权利。
// Created: 2011年7月9日 12:40:36
// Purpose: Definition of Class CTable

using System;
using System.Text;

namespace ErpCoreModel.Framework
{


    public class CDataServer : CBaseObject
    {

        public CDataServer()
        {
            TbCode = "FW_DataServer";
            ClassName = "ErpCoreModel.Framework.CDataServer";

            FW_Table_id = Guid.Empty;
            Server = "";
            DBName = "";
            UserID = "";
            Pwd = "";
            IsWrite = false;
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
        public string Server
        {
            get
            {
                //return name;
                if (m_arrNewVal.ContainsKey("server"))
                    return m_arrNewVal["server"].StrVal;
                else
                    return null;
            }
            set
            {
                //this.name = value;                
                if (m_arrNewVal.ContainsKey("server"))
                    m_arrNewVal["server"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("server", val);
                }
            }
        }

        public string DBName
        {
            get
            {
                //return name;
                if (m_arrNewVal.ContainsKey("dbname"))
                    return m_arrNewVal["dbname"].StrVal;
                else
                    return null;
            }
            set
            {
                //this.name = value;                
                if (m_arrNewVal.ContainsKey("dbname"))
                    m_arrNewVal["dbname"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("dbname", val);
                }
            }
        }

        public string UserID
        {
            get
            {
                //return name;
                if (m_arrNewVal.ContainsKey("userid"))
                    return m_arrNewVal["userid"].StrVal;
                else
                    return null;
            }
            set
            {
                //this.name = value;                
                if (m_arrNewVal.ContainsKey("userid"))
                    m_arrNewVal["userid"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("userid", val);
                }
            }
        }

        public string Pwd
        {
            get
            {
                //return name;
                if (m_arrNewVal.ContainsKey("pwd"))
                    return m_arrNewVal["pwd"].StrVal;
                else
                    return null;
            }
            set
            {
                //this.name = value;                
                if (m_arrNewVal.ContainsKey("pwd"))
                    m_arrNewVal["pwd"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("pwd", val);
                }
            }
        }


        public bool IsWrite
        {
            get
            {
                //return name;
                if (m_arrNewVal.ContainsKey("iswrite"))
                    return m_arrNewVal["iswrite"].BoolVal;
                else
                    return false;
            }
            set
            {
                //this.name = value;                
                if (m_arrNewVal.ContainsKey("iswrite"))
                    m_arrNewVal["iswrite"].BoolVal = value;
                else
                {
                    CValue val = new CValue();
                    val.BoolVal = value;
                    m_arrNewVal.Add("iswrite", val);
                }
            }
        }


    }
}