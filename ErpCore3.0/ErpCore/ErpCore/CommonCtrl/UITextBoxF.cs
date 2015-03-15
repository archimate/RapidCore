using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.UI;
using ErpCore.Window;

namespace ErpCore.CommonCtrl
{
    public partial class UITextBoxF : TextBox, IWindowCtrl
    {
        CFormControl formControl = null;

        public ControlType GetCtrlType()
        {
            return ControlType.TextBox;
        }
        public void OnEdit()
        {
        }
        public UITextBoxF()
        {
            InitializeComponent();
        }

        public UITextBoxF(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public CFormControl FormControl
        {
            get { return formControl; }
            set
            {
                formControl = value;
            }
        }
        
        //界面公式取值
        public object GetSelectValue(string sColCode)
        {
            return this.Text;
        }
    }
}
