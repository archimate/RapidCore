using System;
using System.Collections.Generic;

using System.Text;
using ErpCoreModel.UI;

namespace ErpCore.CommonCtrl
{
    public interface IWindowCtrl
    {
        ControlType GetCtrlType();
        object GetSelectValue(string sColCode);
    }
}
