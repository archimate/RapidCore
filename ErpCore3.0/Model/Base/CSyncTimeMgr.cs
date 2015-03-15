// File:    CSyncTimeMgr.cs
// Author:  甘孝俭
// email:   ganxiaojian@hotmail.com 
// QQ:      154986287
// http://www.8088net.com
// 协议声明：本软件为开源系统，遵循国际开源组织协议。任何单位或个人可以使用或修改本软件源码，
//          可以用于作为非商业或商业用途，但由于使用本源码所引起的一切后果与作者无关。
//          未经作者许可，禁止任何企业或个人直接出售本源码或者把本软件作为独立的功能进行销售活动，
//          作者将保留追究责任的权利。
// Created: 2014/5/27 11:34:35
// Purpose: Definition of Class CSyncTimeMgr

using System;
using System.Text;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Base
{
    public class CSyncTimeMgr : CBaseObjectMgr
    {

        public CSyncTimeMgr()
        {
            TbCode = "B_SyncTime";
            ClassName = "ErpCoreModel.Base.CSyncTime";
            Assembly assembly = Assembly.LoadFrom(Assembly.GetExecutingAssembly().Location);
            ObjType = assembly.GetType(ClassName);
        }

        public CSyncTime FindByTbCode(string SystemName, string TableCode)
        {
            List<CBaseObject> lstObj = GetList();
            var varObj = from obj in lstObj
                         where (obj as CSyncTime).SystemName.Equals(SystemName, StringComparison.OrdinalIgnoreCase)
                         && (obj as CSyncTime).TableCode.Equals(TableCode, StringComparison.OrdinalIgnoreCase)
                         select obj;
            if (varObj.Count() == 0)
                return null;

            return varObj.First() as CSyncTime;
        }
    }
}