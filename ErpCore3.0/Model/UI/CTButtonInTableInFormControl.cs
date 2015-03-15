// File:    CTButtonInTableInFormControl.cs
// Author:  甘孝俭
// email:   ganxiaojian@hotmail.com 
// QQ:      154986287
// http://www.8088net.com
// 协议声明：本软件为开源系统，遵循国际开源组织协议。任何单位或个人可以使用或修改本软件源码，
//          可以用于作为非商业或商业用途，但由于使用本源码所引起的一切后果与作者无关。
//          未经作者许可，禁止任何企业或个人直接出售本源码或者把本软件作为独立的功能进行销售活动，
//          作者将保留追究责任的权利。
// Created: 2011-10-7 14:13:18
// Purpose: Definition of Class CTButtonInTableInFormControl

using System;
using System.Text;
using ErpCoreModel.Framework;

namespace ErpCoreModel.UI
{
    
    public class CTButtonInTableInFormControl : CBaseObject
    {

        public CTButtonInTableInFormControl()
        {
            TbCode = "UI_TButtonInTableInFormControl";
            ClassName = "ErpCoreModel.UI.CTButtonInTableInFormControl";

        }

        
        public Guid UI_TableInFormControl_id
        {
            get
            {
                if (m_arrNewVal.ContainsKey("ui_tableinformcontrol_id"))
                    return m_arrNewVal["ui_tableinformcontrol_id"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {        
                if (m_arrNewVal.ContainsKey("ui_tableinformcontrol_id"))
                    m_arrNewVal["ui_tableinformcontrol_id"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("ui_tableinformcontrol_id", val);
                }
            }
        }
        public string Title
        {
            get
            {
                if (m_arrNewVal.ContainsKey("title"))
                    return m_arrNewVal["title"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("title"))
                    m_arrNewVal["title"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("title", val);
                }
            }
        }
    }
}