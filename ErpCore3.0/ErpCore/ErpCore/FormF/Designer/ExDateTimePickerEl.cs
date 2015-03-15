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
    public partial class ExDateTimePickerEl : UserControl, IDesignEl 
    {
        public ExDateTimePickerEl()
        {
            InitializeComponent();
        }


        public ControlType GetCtrlType()
        {
            return ControlType.DateTimePicker;
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
            return dateTimePicker.Value;
        }
        public void SetValue(object objVal)
        {
            dateTimePicker.Value = (DateTime)objVal;
        }
    }
}
