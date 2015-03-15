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
    public partial class ExTextBoxEl : UserControl, IDesignEl 
    {
        public ExTextBoxEl()
        {
            InitializeComponent();
        }

        
        public ControlType GetCtrlType()
        {
            return ControlType.TextBox;
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
            return textBox.Text;
        }
        public void SetValue(object objVal)
        {
            textBox .Text = objVal.ToString();
        }


    }
}
