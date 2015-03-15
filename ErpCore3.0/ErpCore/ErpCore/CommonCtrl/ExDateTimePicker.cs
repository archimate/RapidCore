using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

using System.Text;
using System.Windows.Forms;

namespace ErpCore.CommonCtrl
{
    public partial class ExDateTimePicker : UserControl, IColumnCtrl 
    {
        public ExDateTimePicker()
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
            return dateTimePicker.Value;
        }
        public void SetValue(object objVal)
        {
            if (objVal != null)
                dateTimePicker.Value = Convert.ToDateTime(objVal);
            else
                dateTimePicker.Value = DateTime.Now;
        }
    }
}
