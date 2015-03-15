// File:    CTButtonInView.cs
// Author:  甘孝俭
// email:   ganxiaojian@hotmail.com 
// QQ:      154986287
// http://www.8088net.com
// 协议声明：本软件为开源系统，遵循国际开源组织协议。任何单位或个人可以使用或修改本软件源码，
//          可以用于作为非商业或商业用途，但由于使用本源码所引起的一切后果与作者无关。
//          未经作者许可，禁止任何企业或个人直接出售本源码或者把本软件作为独立的功能进行销售活动，
//          作者将保留追究责任的权利。
// Created: 2013/2/28 20:52:51
// Purpose: Definition of Class CTButtonInView

using System;
using System.Text;
using ErpCoreModel.Framework;

namespace ErpCoreModel.UI
{

    //从IComparable继承可以方便List 的排序
    public class CTButtonInView : CBaseObject, IComparable
    {

        public CTButtonInView()
        {
            TbCode = "UI_TButtonInView";
            ClassName = "ErpCoreModel.UI.CTButtonInView";

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
        public string Caption
        {
            get
            {
                if (m_arrNewVal.ContainsKey("caption"))
                    return m_arrNewVal["caption"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("caption"))
                    m_arrNewVal["caption"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("caption", val);
                }
            }
        }
        public string Url
        {
            get
            {
                if (m_arrNewVal.ContainsKey("url"))
                    return m_arrNewVal["url"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("url"))
                    m_arrNewVal["url"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("url", val);
                }
            }
        }
        public Guid UI_View_id
        {
            get
            {
                if (m_arrNewVal.ContainsKey("ui_view_id"))
                    return m_arrNewVal["ui_view_id"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {        
                if (m_arrNewVal.ContainsKey("ui_view_id"))
                    m_arrNewVal["ui_view_id"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("ui_view_id", val);
                }
            }
        }
        
        #region 实现比较接口的CompareTo方法
        public int CompareTo(object obj)
        {
            int res = 0;
            try
            {
                CTButtonInView sObj = (CTButtonInView)obj;
                if (this.Idx > sObj.Idx)
                {
                    res = 1;
                }
                else if (this.Idx < sObj.Idx)
                {
                    res = -1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("比较异常", ex.InnerException);
            }
            return res;
        }
        #endregion
    }
}