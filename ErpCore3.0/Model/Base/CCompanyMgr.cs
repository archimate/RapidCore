// File:    CCompanyMgr.cs
// Created: 2012/3/22 12:31:00
// Purpose: Definition of Class CCompanyMgr

using System;
using System.Text;
using System.Collections.Generic;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Base
{
    public class CCompanyMgr : CBaseObjectMgr
    {

        public CCompanyMgr()
        {
            TbCode = "B_Company";
            ClassName = "ErpCoreModel.Base.CCompany";
        }

        public CCompany FindByName(string sName)
        {
            List<CBaseObject> lstObj = GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CCompany company = (CCompany)obj;
                if (company.Name.Equals(sName, StringComparison.OrdinalIgnoreCase))
                    return company;
            }
            return null;
        }
        //¶¥¼¶µ¥Î»
        public CCompany FindTopCompany()
        {
            List<CBaseObject> lstObj = GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CCompany company = (CCompany)obj;
                if (company.Parent_id==Guid.Empty)
                    return company;
            }
            return null;
        }
    }
}