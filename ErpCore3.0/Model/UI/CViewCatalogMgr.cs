// File:    CViewCatalogMgr.cs
// Created: 2012/7/1 6:24:56
// Purpose: Definition of Class CViewCatalogMgr

using System;
using System.Text;
using System.Collections.Generic;
using ErpCoreModel.Framework;

namespace ErpCoreModel.UI
{
    public class CViewCatalogMgr : CBaseObjectMgr
    {

        public CViewCatalogMgr()
        {
            TbCode = "UI_ViewCatalog";
            ClassName = "ErpCoreModel.UI.CViewCatalog";
        }

        public CViewCatalog FindByName(string sName)
        {
            List<CBaseObject> lstObj = GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CViewCatalog Catalog = (CViewCatalog)obj;
                if (Catalog.Name.Equals(sName, StringComparison.OrdinalIgnoreCase))
                    return Catalog;
            }
            return null;
        }
    }
}