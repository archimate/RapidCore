// File:    CDistrictMgr.cs
// Created: 2012/3/22 8:42:03
// Purpose: Definition of Class CDistrictMgr

using System;
using System.Text;
using System.Collections.Generic;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Base
{
    public class CDistrictMgr : CBaseObjectMgr
    {

        public CDistrictMgr()
        {
            TbCode = "B_District";
            ClassName = "ErpCoreModel.Base.CDistrict";
        }

    }
}