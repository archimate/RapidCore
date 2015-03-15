// File:    CTypeInCategory.cs
// Author:  甘孝俭
// email:   ganxiaojian@hotmail.com 
// QQ:      154986287
// http://www.8088net.com
// 协议声明：本软件为开源系统，遵循国际开源组织协议。任何单位或个人可以使用或修改本软件源码，
//          可以用于作为非商业或商业用途，但由于使用本源码所引起的一切后果与作者无关。
//          未经作者许可，禁止任何企业或个人直接出售本源码或者把本软件作为独立的功能进行销售活动，
//          作者将保留追究责任的权利。
// Created: 2012/12/1 10:31:54
// Purpose: Definition of Class CTypeInCategory

using System;
using System.Text;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Store
{
    
    public class CTypeInCategory : CBaseObject
    {

        public CTypeInCategory()
        {
            TbCode = "SP_TypeInCategory";
            ClassName = "ErpCoreModel.Store.CTypeInCategory";

        }

        
        public Guid SP_ProductCategory_id
        {
            get
            {
                if (m_arrNewVal.ContainsKey("sp_productcategory_id"))
                    return m_arrNewVal["sp_productcategory_id"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {        
                if (m_arrNewVal.ContainsKey("sp_productcategory_id"))
                    m_arrNewVal["sp_productcategory_id"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("sp_productcategory_id", val);
                }
            }
        }
        public Guid SP_ProductType_id
        {
            get
            {
                if (m_arrNewVal.ContainsKey("sp_producttype_id"))
                    return m_arrNewVal["sp_producttype_id"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {        
                if (m_arrNewVal.ContainsKey("sp_producttype_id"))
                    m_arrNewVal["sp_producttype_id"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("sp_producttype_id", val);
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