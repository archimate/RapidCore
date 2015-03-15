// File:    CProvinceMgr.cs
// Created: 2012/3/22 8:42:03
// Purpose: Definition of Class CProvinceMgr

using System;
using System.Text;
using System.Collections.Generic;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Base
{
    public class CProvinceMgr : CBaseObjectMgr
    {

        public CProvinceMgr()
        {
            TbCode = "B_Province";
            ClassName = "ErpCoreModel.Base.CProvince";
        }

    }
}