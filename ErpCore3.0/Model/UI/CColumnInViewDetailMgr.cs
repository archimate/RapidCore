// File:    CColumnInViewDetailMgr.cs
// Created: 2012-08-28 13:20:30
// Purpose: Definition of Class CColumnInViewDetailMgr

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using ErpCoreModel.Framework;

namespace ErpCoreModel.UI
{
    public class CColumnInViewDetailMgr : CBaseObjectMgr
    {

        public CColumnInViewDetailMgr()
        {
            TbCode = "UI_ColumnInViewDetail";
            ClassName = "ErpCoreModel.UI.CColumnInViewDetail";
        }

        public CColumnInViewDetail FindByColumn(Guid FW_Column_id)
        {
            List<CBaseObject> lstObj = GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CColumnInViewDetail civd = (CColumnInViewDetail)obj;
                if (civd.FW_Column_id == FW_Column_id)
                    return civd;
            }
            return null;
        }
        public int NewIdx()
        {
            int iNexIdx = -1;
            List<CBaseObject> lstObj = GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CColumnInViewDetail civd = (CColumnInViewDetail)obj;
                if (civd.Idx > iNexIdx)
                    iNexIdx = civd.Idx;
            }
            return iNexIdx+1;
        }

        public override List<CBaseObject> GetList()
        {
            List<CBaseObject> lstObj = base.GetList();
            var varObj = from obj in lstObj
                         orderby (obj as CColumnInViewDetail).Idx
                         select obj;
            return varObj.ToList();
        }
    }
}