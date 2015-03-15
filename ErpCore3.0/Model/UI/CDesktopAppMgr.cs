// File:    CDesktopAppMgr.cs
// Created: 2012-08-22 14:39:34
// Purpose: Definition of Class CDesktopAppMgr

using System;
using System.Text;
using System.Collections.Generic;
using ErpCoreModel.Framework;

namespace ErpCoreModel.UI
{
    public class CDesktopAppMgr : CBaseObjectMgr
    {

        public CDesktopAppMgr()
        {
            TbCode = "UI_DesktopApp";
            ClassName = "ErpCoreModel.UI.CDesktopApp";
        }

    }
}