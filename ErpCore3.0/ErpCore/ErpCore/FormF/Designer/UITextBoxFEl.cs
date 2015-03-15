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
using ErpCore.Window.Designer;
using ErpCore.Database.Table;

namespace ErpCore.FormF.Designer
{
    public partial class UITextBoxFEl : TextBox, IDesignEl
    {
        CFormControl formControl = null;

        public ControlType GetCtrlType()
        {
            return ControlType.TextBox;
        }
        public void OnEdit()
        {
        }
        public UITextBoxFEl()
        {
            InitializeComponent();
        }

        public UITextBoxFEl(IContainer container)
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
    }
}
