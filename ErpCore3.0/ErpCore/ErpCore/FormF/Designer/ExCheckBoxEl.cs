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
    public partial class ExCheckBoxEl : UserControl, IDesignEl 
    {
        public ExCheckBoxEl()
        {
            InitializeComponent();
        }

        public ControlType GetCtrlType()
        {
            return ControlType.CheckBox;
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
            return checkBox .Checked;
        }
        public void SetValue(object objVal)
        {
            checkBox.Checked = Convert.ToBoolean(objVal);
        }
    }
}
