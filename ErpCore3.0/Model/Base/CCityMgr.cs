// File:    CCityMgr.cs
// Created: 2012/3/22 8:42:03
// Purpose: Definition of Class CCityMgr

using System;
using System.Text;
using System.Collections.Generic;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Base
{
    public class CCityMgr : CBaseObjectMgr
    {

        public CCityMgr()
        {
            TbCode = "B_City";
            ClassName = "ErpCoreModel.Base.CCity";
        }
    }
}