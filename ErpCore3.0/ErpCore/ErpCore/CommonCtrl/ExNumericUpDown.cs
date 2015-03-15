using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

using System.Text;
using System.Windows.Forms;

namespace ErpCore.CommonCtrl
{
    public partial class ExNumericUpDown : UserControl, IColumnCtrl 
    {
        public ExNumericUpDown()
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
            return numericUpDown.Value;
        }
        public void SetValue(object objVal)
        {
            if (objVal == null)
                return;
            numericUpDown.Value = Convert.ToDecimal(objVal);
        }
    }
}
