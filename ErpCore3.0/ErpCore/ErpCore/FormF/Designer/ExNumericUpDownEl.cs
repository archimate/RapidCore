using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

using System.Text;
using System.Windows.Forms;
using ErpCore.Window.Designer;
using ErpCoreModel.UI;

namespace ErpCore.FormF.Designer
{
    public partial class ExNumericUpDownEl : UserControl, IDesignEl 
    {
        public ExNumericUpDownEl()
        {
            InitializeComponent();
        }


        public ControlType GetCtrlType()
        {
            return ControlType.NumericUpDown;
        }
        public void OnEdit()
        {
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
            numericUpDown.Value = Convert.ToDecimal(objVal);
        }
    }
}
