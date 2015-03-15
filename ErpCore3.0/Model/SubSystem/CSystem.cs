// File:    CSystem.cs
// Author:  甘孝俭
// email:   ganxiaojian@hotmail.com 
// QQ:      154986287
// http://www.8088net.com
// 协议声明：本软件为开源系统，遵循国际开源组织协议。任何单位或个人可以使用或修改本软件源码，
//          可以用于作为非商业或商业用途，但由于使用本源码所引起的一切后果与作者无关。
//          未经作者许可，禁止任何企业或个人直接出售本源码或者把本软件作为独立的功能进行销售活动，
//          作者将保留追究责任的权利。
// Created: 2011年7月9日 12:40:36
// Purpose: Definition of Class CSystem

using System;
using System.Text;
using ErpCoreModel.Framework;

namespace ErpCoreModel.SubSystem
{

    public class CSystem : CBaseObject
    {
        public CSystem()
        {
            TbCode = "S_System";
            ClassName = "ErpCoreModel.SubSystem.CSystem";

            Name = "";
            FW_Diagram_id = Guid.Empty;
            Icon = null;
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
        public Guid FW_Diagram_id
        {
            get
            {
                if (m_arrNewVal.ContainsKey("fw_diagram_id"))
                    return m_arrNewVal["fw_diagram_id"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("fw_diagram_id"))
                    m_arrNewVal["fw_diagram_id"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("fw_diagram_id", val);
                }
            }
        }

        public object Icon
        {
            get
            {
                if (m_arrNewVal.ContainsKey("icon"))
                    return m_arrNewVal["icon"].ObjectVal;
                else
                    return null;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("icon"))
                    m_arrNewVal["icon"].ObjectVal = value;
                else
                {
                    CValue val = new CValue();
                    val.ObjectVal = value;
                    m_arrNewVal.Add("icon", val);
                }
            }
        }
        public Guid StartWindow
        {
            get
            {
                if (m_arrNewVal.ContainsKey("startwindow"))
                    return m_arrNewVal["startwindow"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("startwindow"))
                    m_arrNewVal["startwindow"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("startwindow", val);
                }
            }
        }
    }
}