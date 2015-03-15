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
    public partial class ExComboBoxEl : UserControl, IDesignEl 
    {
        public ExComboBoxEl()
        {
            InitializeComponent();
        }


        public ControlType GetCtrlType()
        {
            return ControlType.RefComboBox;
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
            if (this.comboBox.SelectedItem != null)
            {
                DataItem item = (DataItem)this.comboBox.SelectedItem;
                return item.Data;
            }
            else
                return null;
        }
        public void SetValue(object objVal)
        {
            for (int i = 0; i < comboBox.Items.Count; i++)
            {
                DataItem item = (DataItem)comboBox.Items[i];
                if (item.Data.ToString() == objVal.ToString())
                {
                    comboBox.SelectedIndex = i;
                    break;
                }
            }
        }
    }
}
