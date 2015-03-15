// File:    CViewMgr.cs
// Created: 2012/7/1 6:24:56
// Purpose: Definition of Class CViewMgr

using System;
using System.Text;
using System.Collections.Generic;
using ErpCoreModel.Framework;

namespace ErpCoreModel.UI
{
    public class CViewMgr : CBaseObjectMgr
    {

        public CViewMgr()
        {
            TbCode = "UI_View";
            ClassName = "ErpCoreModel.UI.CView";
        }

        public CView FindByName(string sName)
        {
            List<CBaseObject> lstObj = GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CView view = (CView)obj;
                if (view.Name.Equals(sName, StringComparison.OrdinalIgnoreCase))
                    return view;
            }
            return null;
        }
        //根据表对象查找第一个单表视图
        public CView FindFirstByTable(Guid FW_Table_id)
        {
            List<CBaseObject> lstObj = GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CView view = (CView)obj;
                if (view.VType== enumViewType.Single && view.FW_Table_id == FW_Table_id) 
                    return view;
            }
            return null;
        }
    }
}