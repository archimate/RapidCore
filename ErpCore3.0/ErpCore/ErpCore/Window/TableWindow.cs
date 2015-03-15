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

namespace ErpCore.Window
{
    public partial class TableWindow : Form
    {
        public TableWindow(CBaseObjectMgr BaseObjectMgr)
        {
            InitializeComponent();

            tableCtrl.BaseObjectMgr = BaseObjectMgr;
        }

        private void TableWindow_Load(object sender, EventArgs e)
        {
            this.Text = tableCtrl.BaseObjectMgr.Table.Name;
        }
    }
}
