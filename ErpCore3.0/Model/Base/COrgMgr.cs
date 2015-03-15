// File:    COrgMgr.cs
// Author:  甘孝俭
// email:   ganxiaojian@hotmail.com 
// QQ:      154986287
// http://www.8088net.com
// 协议声明：本软件为开源系统，遵循国际开源组织协议。任何单位或个人可以使用或修改本软件源码，
//          可以用于作为非商业或商业用途，但由于使用本源码所引起的一切后果与作者无关。
//          未经作者许可，禁止任何企业或个人直接出售本源码或者把本软件作为独立的功能进行销售活动，
//          作者将保留追究责任的权利。
// Created: 2011年7月9日 13:43:28
// Purpose: Definition of Class COrgMgr

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Base
{

    public class COrgMgr : CBaseObjectMgr
    {
        public COrgMgr()
        {
            TbCode = "B_Org";
            ClassName = "ErpCoreModel.Base.COrg";
        }

        public COrg FindByName(string sName)
        {
            List<CBaseObject> lstObj = GetList();
            var varObj = from obj in lstObj
                         where (obj as COrg).Name.Equals(sName, StringComparison.OrdinalIgnoreCase)
                         select obj;
            if (varObj.Count() > 0)
                return varObj.First() as COrg;
            else
                return null;

        }

        public List<COrg> FindByUser(Guid B_User_id)
        {
            List<COrg> ret = new List<COrg>();
            List<CBaseObject> lstObj = GetList();
            foreach (CBaseObject obj in lstObj)
            {
                COrg Org = (COrg)obj;
                if (Org.UserInOrgMgr.FindByUser(B_User_id) != null)
                    ret.Add(Org);
            }
            return ret;
        }
        //获取下级
        public List<CBaseObject> GetChildList(Guid Parent_id)
        {
            List<CBaseObject> lstObj =GetList();
            var varObj = from obj in lstObj where (obj as COrg).Parent_id == Parent_id select obj;
            return varObj.ToList();
        }
    }
}