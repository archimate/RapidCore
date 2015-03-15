using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

using System.Text;
using System.Windows.Forms;

namespace ErpCore.CommonCtrl
{
    public partial class ExComboBox : UserControl, IColumnCtrl 
    {
        public ExComboBox()
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
            if (comboBox.DropDownStyle == ComboBoxStyle.DropDown)
            {
                return comboBox.Text;
            }
            else
            {
                if (this.comboBox.SelectedItem != null)
                {
                    DataItem item = (DataItem)this.comboBox.SelectedItem;
                    return item.Data;
                }
                else
                    return null;
            }
        }
        public void SetValue(object objVal)
        {
            if (objVal == null)
                return;
            if (comboBox.DropDownStyle == ComboBoxStyle.DropDown)
            {
                comboBox.Text = objVal.ToString();
            }
            else
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
}
