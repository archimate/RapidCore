// File:    CPurchaseNoteMgr.cs
// Author:  甘孝俭
// email:   ganxiaojian@hotmail.com 
// QQ:      154986287
// http://www.8088net.com
// 协议声明：本软件为开源系统，遵循国际开源组织协议。任何单位或个人可以使用或修改本软件源码，
//          可以用于作为非商业或商业用途，但由于使用本源码所引起的一切后果与作者无关。
//          未经作者许可，禁止任何企业或个人直接出售本源码或者把本软件作为独立的功能进行销售活动，
//          作者将保留追究责任的权利。
// Created: 2013/3/1 10:56:13
// Purpose: Definition of Class CPurchaseNoteMgr

using System;
using System.Text;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Invoicing
{
    public class CPurchaseNoteMgr : CBaseObjectMgr
    {

        public CPurchaseNoteMgr()
        {
            TbCode = "CG_PurchaseNote";
            ClassName = "ErpCoreModel.Invoicing.CPurchaseNote";
            Assembly assembly = Assembly.LoadFrom(Assembly.GetExecutingAssembly().Location);
            ObjType = assembly.GetType(ClassName);
        }

        public CPurchaseNote FindByCode(string sCode)
        {
            List<CBaseObject> lstObj = GetList();
            var varObj = from obj in lstObj where (obj as CPurchaseNote).Code == sCode select obj;
            if (varObj.Count() > 0)
                return varObj.First() as CPurchaseNote;
            else
                return null;
        }
    }
}