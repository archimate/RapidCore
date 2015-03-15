using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCore.Window;

namespace ErpCore.FormF.Designer
{
    public partial class ControlToolWindow : Form
    {
        public ControlToolWindow()
        {
            InitializeComponent();
        }

        private void TableToolWindow_Load(object sender, EventArgs e)
        {
        }


        private void listControl_ItemDrag(object sender, ItemDragEventArgs e)
        {
            this.DoDragDrop(e.Item, DragDropEffects.All);
        }

        private void listControl_DoubleClick(object sender, EventArgs e)
        {
            if (listDataControl.SelectedItems.Count == 0)
                return;

            LayoutFormDesigner mdiParent = (LayoutFormDesigner)this.MdiParent;
            mdiParent.GetActiveChildenWindow().OnDragDrop( listDataControl.SelectedItems[0]);
        }

        private void listFormControl_ItemDrag(object sender, ItemDragEventArgs e)
        {
            this.DoDragDrop(e.Item, DragDropEffects.All);
        }

    }
}
