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

namespace ErpCore.Window.Designer
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
            if (listControl.SelectedItems.Count == 0)
                return;
            SelDockPanel frm = new SelDockPanel();
            if (frm.ShowDialog() != DialogResult.OK)
                return;

            LayoutWindowDesigner mdiParent = (LayoutWindowDesigner)this.MdiParent;
            mdiParent.GetActiveChildenWindow().OnDragDrop(frm.m_dock, listControl.SelectedItems[0]);
        }
    }
}
