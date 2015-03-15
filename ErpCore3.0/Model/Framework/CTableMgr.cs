// File:    CTableMgr.cs
// Author:  甘孝俭
// email:   ganxiaojian@hotmail.com 
// QQ:      154986287
// http://www.8088net.com
// 协议声明：本软件为开源系统，遵循国际开源组织协议。任何单位或个人可以使用或修改本软件源码，
//          可以用于作为非商业或商业用途，但由于使用本源码所引起的一切后果与作者无关。
//          未经作者许可，禁止任何企业或个人直接出售本源码或者把本软件作为独立的功能进行销售活动，
//          作者将保留追究责任的权利。
// Created: 2011年7月9日 13:37:16
// Purpose: Definition of Class CTableMgr

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace ErpCoreModel.Framework
{
    public class CTableMgr : CBaseObjectMgr
    {
        public CTableMgr()
        {
            TbCode = "FW_Table";
            ClassName = "ErpCoreModel.Framework.CTable";
        }

        public CTable FindByName(string sName)
        {
            List<CBaseObject> lstTable = this.GetList();
            foreach (CBaseObject obj in lstTable)
            {
                CTable table = (CTable)obj;
                if (table.Name.Equals(sName, StringComparison.OrdinalIgnoreCase))
                    return table;
            }
            return null;
        }
        public CTable FindByCode(string sCode)
        {
            List<CBaseObject> lstTable = this.GetList();
            foreach (CBaseObject obj in lstTable)
            {
                CTable table = (CTable)obj;
                if (table.Code.Equals(sCode, StringComparison.OrdinalIgnoreCase))
                    return table;
            }
            return null;
        }

        //默认按编码排序
        public override List<CBaseObject> GetList()
        {
            List<CBaseObject> lstObj = base.GetList();
            var varObj = from obj in lstObj orderby (obj as CTable).Code select obj;
            return varObj.ToList();
        }
    }
}