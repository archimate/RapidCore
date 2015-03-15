// File:    CColumnInWindowControl.cs
// Author:  甘孝俭
// email:   ganxiaojian@hotmail.com 
// QQ:      154986287
// http://www.8088net.com
// 协议声明：本软件为开源系统，遵循国际开源组织协议。任何单位或个人可以使用或修改本软件源码，
//          可以用于作为非商业或商业用途，但由于使用本源码所引起的一切后果与作者无关。
//          未经作者许可，禁止任何企业或个人直接出售本源码或者把本软件作为独立的功能进行销售活动，
//          作者将保留追究责任的权利。
// Created: 2011年7月9日 12:40:36
// Purpose: Definition of Class CColumnInWindowControl
using System;
using System.Collections.Generic;
using System.Text;
using ErpCoreModel.Framework;

namespace ErpCoreModel.UI
{
    public class CColumnInTableInWindowControl : CBaseObject
    {
        public CColumnInTableInWindowControl()
        {
            TbCode = "UI_ColumnInTableInWindowControl";
            ClassName = "ErpCoreModel.UI.CColumnInTableInWindowControl";

            UI_TableInWindowControl_id = Guid.Empty;
            FW_Column_id = Guid.Empty;
        }
        public Guid UI_TableInWindowControl_id
        {
            get
            {
                if (m_arrNewVal.ContainsKey("ui_tableinwindowcontrol_id"))
                    return m_arrNewVal["ui_tableinwindowcontrol_id"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("ui_tableinwindowcontrol_id"))
                    m_arrNewVal["ui_tableinwindowcontrol_id"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("ui_tableinwindowcontrol_id", val);
                }
            }
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
    }
}
