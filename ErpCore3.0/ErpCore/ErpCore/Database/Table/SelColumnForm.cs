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

namespace ErpCore.Database.Table
{
    public partial class SelColumnForm : Form
    {
        public CTable m_Table = null;
        public CColumn m_SelColumn = null;

        public SelColumnForm()
        {
            InitializeComponent();
        }

        private void SelTableForm_Load(object sender, EventArgs e)
        {
            LoadHead();
            LoadList();
        }
        void LoadHead()
        {
            listColumn.Columns.Clear();
            listColumn.Columns.Add("名称", 100);
            listColumn.Columns.Add("编码", 100);
        }
        public void LoadList()
        {
            if (m_Table == null)
                return;
            listColumn.Items.Clear();
            List<CBaseObject> lstColumn = m_Table.ColumnMgr.GetList();
            foreach (CBaseObject obj in lstColumn)
            {
                CColumn col = (CColumn)obj;

                ListViewItem item = new ListViewItem();
                item.Text = col.Name;
                item.SubItems.Add(col.Code);
                item.Tag = col;
                listColumn.Items.Add(item);
            }
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            if (listColumn.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选择一项！","提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            m_SelColumn = (CColumn)listColumn.SelectedItems[0].Tag;
            DialogResult = DialogResult.OK;
        }
    }
}
