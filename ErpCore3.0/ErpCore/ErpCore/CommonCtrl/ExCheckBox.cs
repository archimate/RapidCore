using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

using System.Text;
using System.Windows.Forms;

namespace ErpCore.CommonCtrl
{
    public partial class ExCheckBox : UserControl, IColumnCtrl 
    {
        public ExCheckBox()
        {
            InitializeComponent();
        }

        public string GetCaption()
        {
            return lbCaption.Text;
        }
        public void SetCaption(string sCaption)
        {
            lbCaption.Text = sCaption;
        }
        public object GetValue()
        {
            return checkBox .Checked;
        }
        public void SetValue(object objVal)
        {
            if (objVal == null)
                return;
            checkBox.Checked = Convert.ToBoolean(objVal);
        }
    }
}
