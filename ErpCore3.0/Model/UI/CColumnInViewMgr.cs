// File:    CColumnInViewMgr.cs
// Created: 2012-08-28 13:20:30
// Purpose: Definition of Class CColumnInViewMgr

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using ErpCoreModel.Framework;

namespace ErpCoreModel.UI
{
    public class CColumnInViewMgr : CBaseObjectMgr
    {

        public CColumnInViewMgr()
        {
            TbCode = "UI_ColumnInView";
            ClassName = "ErpCoreModel.UI.CColumnInView";
        }

        public CColumnInView FindByColumn(Guid FW_Column_id)
        {
            List<CBaseObject> lstObj = GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CColumnInView civ = (CColumnInView)obj;
                if (civ.FW_Column_id == FW_Column_id)
                    return civ;
            }
            return null;
        }
        public int NewIdx()
        {
            int iNexIdx = -1;
            List<CBaseObject> lstObj = GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CColumnInView civ = (CColumnInView)obj;
                if (civ.Idx > iNexIdx)
                    iNexIdx = civ.Idx;
            }
            return iNexIdx+1;
        }

        public override List<CBaseObject> GetList()
        {
            List<CBaseObject> lstObj =base.GetList();
            var varObj = from obj in lstObj
                         orderby (obj as CColumnInView).Idx
                         select obj;
            return varObj.ToList();
        }
    }
}