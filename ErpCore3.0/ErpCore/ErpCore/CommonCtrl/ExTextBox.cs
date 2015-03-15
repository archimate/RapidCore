using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

using System.Text;
using System.Windows.Forms;

namespace ErpCore.CommonCtrl
{
    public partial class ExTextBox : UserControl, IColumnCtrl 
    {
        public ExTextBox()
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
            return textBox.Text;
        }
        public void SetValue(object objVal)
        {
            if (objVal == null)
            {
                textBox.Text = "";
                return;
            }
            textBox .Text = objVal.ToString();
        }


    }
}
