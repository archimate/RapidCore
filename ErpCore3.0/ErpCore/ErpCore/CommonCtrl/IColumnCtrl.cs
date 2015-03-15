using System;
using System.Collections.Generic;

using System.Text;

namespace ErpCore.CommonCtrl
{
    public interface IColumnCtrl
    {
        string GetCaption();
        void SetCaption(string sCaption);
        object GetValue();
        void SetValue(object objVal);
    }
}
