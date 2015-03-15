// File:    CDesktopMgr.cs
// Created: 2012-08-22 14:39:34
// Purpose: Definition of Class CDesktopMgr

using System;
using System.Text;
using System.Collections.Generic;
using ErpCoreModel.Framework;

namespace ErpCoreModel.UI
{
    public class CDesktopMgr : CBaseObjectMgr
    {

        public CDesktopMgr()
        {
            TbCode = "UI_Desktop";
            ClassName = "ErpCoreModel.UI.CDesktop";
        }

    }
}