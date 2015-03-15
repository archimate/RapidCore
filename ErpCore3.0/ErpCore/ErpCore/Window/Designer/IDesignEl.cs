using System;
using System.Collections.Generic;

using System.Text;
using ErpCoreModel.Framework;
using ErpCoreModel.UI;

namespace ErpCore.Window.Designer
{
    public interface IDesignEl
    {
        ControlType GetCtrlType();
        void OnEdit();
    }
}
