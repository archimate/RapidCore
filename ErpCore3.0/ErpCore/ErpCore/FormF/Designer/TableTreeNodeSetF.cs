using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;

using System.Text;
using System.Windows.Forms;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.UI;
using ErpCore.Window;
using ErpCore.Database.Table;

namespace ErpCore.FormF.Designer
{
    public partial class TableTreeNodeSetF : UserControl
    {
        public CTable m_Table = null;

        private int idx = 0;
        private bool selected = false;

        public TableTreeNodeSetF()
        {
            InitializeComponent();
        }

        public int Idx
        {
            get { return idx; }
            set { 
                idx = value;
                lbCaption.Text = string.Format("第 {0} 级",idx);
            }
        }
        public bool Selected
        {
            get { return selected; }
            set { 
                selected = value;
                if (selected)
                    toolStrip.BackColor = Color.DeepSkyBlue;
                else
                    toolStrip.BackColor = Color.Empty;
            }
        }

        private void TableTreeNodeSet_Load(object sender, EventArgs e)
        {

            lbCaption.Text = string.Format("第 {0} 级", idx);
        }

        private void btSelTable_Click(object sender, EventArgs e)
        {
            SelTableForm frm = new SelTableForm();
            if (frm.ShowDialog() != DialogResult.OK)
                return;
            m_Table = frm.m_SelTable;
            txtTable.Text = m_Table.Name;

            List<CBaseObject> lstObj= m_Table.ColumnMgr.GetList();
            bool bHasName=false,bHasCode=false;
            foreach (CBaseObject obj in lstObj)
            {
                CColumn column = (CColumn)obj;
                if (column.Code.Equals("name", StringComparison.OrdinalIgnoreCase))
                {
                    bHasName = true;
                }
                if (column.Code.Equals("code", StringComparison.OrdinalIgnoreCase))
                {
                    bHasCode = true;
                }
            }
            if (bHasName)
                txtText.Text = "[Name]";
            else if (bHasCode)
                txtText.Text = "[Code]";

            LoadColumn();
        }

        void LoadColumn()
        {
            if (m_Table == null)
                return;
            cbNodeIDCol.Items.Clear();
            cbPNodeIDCol.Items.Clear();
            List<CBaseObject> lstObj = m_Table.ColumnMgr.GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CColumn column = (CColumn)obj;
                DataItem item = new DataItem(column.Code, column);
                cbNodeIDCol.Items.Add(item);
                DataItem item2 = new DataItem(column.Code, column);
                cbPNodeIDCol.Items.Add(item2);
            }
            if(cbNodeIDCol.Items.Count>0)
                cbNodeIDCol.SelectedIndex = 0;
        }

        private void btSelColumn_Click(object sender, EventArgs e)
        {
            if (m_Table == null)
            {
                MessageBox.Show("请选择表！");
                return;
            }
            SelColumnForm frm = new SelColumnForm();
            frm.m_Table = m_Table;
            if (frm.ShowDialog() != DialogResult.OK)
                return;
            txtText.Text =string.Format( "[{0}]",frm.m_SelColumn.Code);
        }

        private void ckIsLoop_CheckedChanged(object sender, EventArgs e)
        {
            if (ckIsLoop.Checked)
            {
                cbNodeIDCol.Enabled = true;
                cbPNodeIDCol.Enabled = true;
                txtRootFilter.Enabled = true;
            }
            else
            {
                cbNodeIDCol.Enabled = false;
                cbPNodeIDCol.Enabled = false;
                txtRootFilter.Enabled = false;
            }
        }

        private void cbPNodeIDCol_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPNodeIDCol.SelectedItem == null)
                return;
            DataItem item = (DataItem)cbPNodeIDCol.SelectedItem;
            CColumn column = (CColumn)item.Data;
            switch (column.ColType)
            {
                case ColumnType.int_type:
                case ColumnType.long_type:
                    {
                        txtRootFilter.Text = string.Format("{0}=0",column.Code);
                        break;
                    }
                case ColumnType.string_type:
                    {
                        txtRootFilter.Text = string.Format("({0}='' or {0} is null)", column.Code);
                        break;
                    }
                case ColumnType.guid_type:
                case ColumnType.ref_type:
                    {
                        txtRootFilter.Text = string.Format("{0}='{{00000000-0000-0000-0000-000000000000}}'", column.Code);
                        break;
                    }
            }
        }

    }
}
