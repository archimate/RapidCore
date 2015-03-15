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
    public partial class UILabelFEl : Label, IDesignEl
    {
        CFormControl formControl = null;

        public ControlType GetCtrlType()
        {
            return ControlType.Label;
        }
        public void OnEdit()
        {
        }
        public UILabelFEl()
        {
            InitializeComponent();
        }

        public UILabelFEl(IContainer container)
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
