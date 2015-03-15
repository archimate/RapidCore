using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.UI;

namespace ErpCore.View
{
    public partial class ViewInfo : Form
    {
        public CView m_View = null;
        public Guid m_Catalog_id = Guid.Empty;
        //视图的主表
        Guid m_MainTb_id = Guid.Empty;

        bool m_bIsNew = false;

        public ViewInfo()
        {
            InitializeComponent();
        }

        private void ViewInfo_Load(object sender, EventArgs e)
        {
            LoadCatalog();
            LoadTable();
            LoadData();
        }
        void LoadCatalog()
        {
            cbCatalog.Items.Clear();
            cbCatalog.Items.Add("");
            int iDefaultIdx = 0;
            List<CBaseObject> lstObj = Program.Ctx.ViewCatalogMgr.GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CViewCatalog catalog = (CViewCatalog)obj;
                DataItem item = new DataItem(catalog.Name, catalog);
                int idx = cbCatalog.Items.Add(item);
                if (catalog.Id == m_Catalog_id)
                    iDefaultIdx = idx;
            }
            cbCatalog.SelectedIndex = iDefaultIdx;
        }
        void LoadTable()
        {
            cbTable.Items.Clear();
            List<CBaseObject> lstObj = Program.Ctx.TableMgr.GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CTable table = (CTable)obj;
                DataItem item = new DataItem();
                item.name = table.Name;
                item.Data = table;
                cbTable.Items.Add(item);
            }
        }
        void LoadData()
        {
            if (m_View == null) //新建 
            {
                m_bIsNew = true;
                m_View = new CView();
                m_View.Ctx = Program.Ctx;
            }


            txtName.Text = m_View.Name;
            m_MainTb_id = m_View.FW_Table_id;
            //默认表
            for (int i = 0; i < cbTable.Items.Count; i++)
            {
                DataItem item = (DataItem)cbTable.Items[i];
                CTable tb = (CTable)item.Data;
                if (tb.Id == m_MainTb_id)
                {
                    cbTable.SelectedIndex = i;
                    break;
                }
            }

            dataGridView.Rows.Clear();
            List<CBaseObject> lstCiv = m_View.ColumnInViewMgr.GetList();
            foreach (CBaseObject obj in lstCiv)
            {
                CColumnInView civ = (CColumnInView)obj;
                CTable table = (CTable)Program.Ctx.TableMgr.Find(civ.FW_Table_id);
                if (table == null)
                    continue;
                CColumn column = (CColumn)table.ColumnMgr.Find(civ.FW_Column_id);
                if (column == null)
                    continue;

                dataGridView.Rows.Add(1);
                DataGridViewRow rowNew = dataGridView.Rows[dataGridView.Rows.Count - 1];
                rowNew.Cells[0].Value = column.Name;
                rowNew.Cells[0].ReadOnly = true;
                rowNew.Cells[1].Value = civ.Caption;
                rowNew.Tag = civ;
            }

        }

        private void cbTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadColumn();
        }
        void LoadColumn()
        {
            if (cbTable.SelectedIndex == -1)
                return;
            listColumn.Items.Clear();
            DataItem it = (DataItem)cbTable.SelectedItem;
            CTable table = (CTable)it.Data;
            List<CBaseObject> lstObj = table.ColumnMgr.GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CColumn column = (CColumn)obj;
                ListViewItem item = new ListViewItem();
                item.Text = column.Name;
                item.Tag = column;
                listColumn.Items.Add(item);
            }
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            if (listColumn.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选择字段！");
                return;
            }
            for (int i = 0; i < listColumn.SelectedItems.Count; i++)
            {
                CColumn column = (CColumn)listColumn.SelectedItems[i].Tag;
                bool bHas = false;
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    CColumnInView civ2 = (CColumnInView)row.Tag;
                    if (civ2.FW_Column_id == column.Id)
                    {
                        bHas = true;
                        break;
                    }
                }
                if (bHas)
                    continue;

                CColumnInView civ = new CColumnInView();
                civ.Ctx = Program.Ctx;
                civ.UI_View_id = m_View.Id;
                civ.FW_Table_id = column.FW_Table_id;
                civ.FW_Column_id = column.Id;
                civ.Caption = column.Name;
                m_View.ColumnInViewMgr.AddNew(civ);
                if (m_MainTb_id == Guid.Empty)
                    m_MainTb_id = civ.FW_Table_id;

                dataGridView.Rows.Add(1);
                DataGridViewRow rowNew = dataGridView.Rows[dataGridView.Rows.Count-1];
                rowNew.Cells[0].Value = column.Name;
                rowNew.Cells[0].ReadOnly = true;
                rowNew.Cells[1].Value = column.Name;
                rowNew.Tag = civ;

            }
        }

        private void btDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow == null)
                return;
            CColumnInView civ = (CColumnInView)dataGridView.CurrentRow.Tag;
            m_View.ColumnInViewMgr.Delete(civ);

            dataGridView.Rows.Remove(dataGridView.CurrentRow);

            if (dataGridView.Rows.Count == 0)
                m_MainTb_id = Guid.Empty;
        }

        private void btUp_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow == null)
            {
                MessageBox.Show("请选择一行！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (dataGridView.CurrentRow.Index == 0)
                return;
            int idx = dataGridView.CurrentRow.Index;
            DataGridViewRow row = dataGridView.CurrentRow;
            DataGridViewRow row2 = dataGridView.Rows[idx - 1];
            dataGridView.Rows.Remove(row2);
            dataGridView.Rows.Insert(idx, row2);
        }

        private void btDown_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow == null)
            {
                MessageBox.Show("请选择一行！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (dataGridView.CurrentRow.Index == dataGridView.Rows.Count - 1)
                return;

            int idx = dataGridView.CurrentRow.Index;
            DataGridViewRow row = dataGridView.CurrentRow;
            DataGridViewRow row2 = dataGridView.Rows[idx + 1];
            dataGridView.Rows.Remove(row2);
            dataGridView.Rows.Insert(idx, row2);
        }
        
        private void listColumn_DoubleClick(object sender, EventArgs e)
        {
            btAdd_Click(null, null);
        }

        private void btAddAll_Click(object sender, EventArgs e)
        {
            if (listColumn.Items.Count == 0)
            {
                MessageBox.Show("请选择表！");
                return;
            }
            for (int i = 0; i < listColumn.Items.Count; i++)
            {
                CColumn column = (CColumn)listColumn.Items[i].Tag;
                bool bHas = false;
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    CColumnInView civ2 = (CColumnInView)row.Tag;
                    if (civ2.FW_Column_id == column.Id)
                    {
                        bHas = true;
                        break;
                    }
                }
                if (bHas)
                    continue;


                CColumnInView civ = new CColumnInView();
                civ.Ctx = Program.Ctx;
                civ.UI_View_id = m_View.Id;
                civ.FW_Table_id = column.FW_Table_id;
                civ.FW_Column_id = column.Id;
                civ.Caption = column.Name;
                m_View.ColumnInViewMgr.AddNew(civ);
                if (m_MainTb_id == Guid.Empty)
                    m_MainTb_id = civ.FW_Table_id;

                dataGridView.Rows.Add(1);
                DataGridViewRow rowNew = dataGridView.Rows[dataGridView.Rows.Count - 1];
                rowNew.Cells[0].Value = column.Name;
                rowNew.Cells[0].ReadOnly = true;
                rowNew.Cells[1].Value = column.Name;
                rowNew.Tag = civ;

            }
        }

        private void btDelAll_Click(object sender, EventArgs e)
        {
            m_View.ColumnInViewMgr.RemoveAll();
            m_MainTb_id = Guid.Empty;

            dataGridView.Rows.Clear();
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            string sName = txtName.Text.Trim();
            if (sName == "")
            {
                MessageBox.Show("名称不能空！");
                txtName.Focus();
                return;
            }
            if (!sName.Equals(m_View.Name))
            {
                CView view2 = Program.Ctx.ViewMgr.FindByName(sName);
                if (view2 != null)
                {
                    MessageBox.Show("名称重复！");
                    txtName.Focus();
                    return;
                }
            }
            if (listColumn.Items.Count == 0)
            {
                MessageBox.Show("请选择字段！");
                return;
            }

            m_View.Name = sName;
            if (cbCatalog.SelectedIndex < 1)
                m_View.UI_ViewCatalog_id = Guid.Empty;
            else
            {
                DataItem item = (DataItem)cbCatalog.SelectedItem;
                CViewCatalog catalog = (CViewCatalog)item.Data;
                m_View.UI_ViewCatalog_id = catalog.Id;
            }
            m_View.FW_Table_id = m_MainTb_id;

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                CColumnInView civ = (CColumnInView)row.Tag;
                civ.Caption = row.Cells[1].Value.ToString();
            }

            if(m_bIsNew)
                Program.Ctx.ViewMgr.AddNew(m_View);
            else
                Program.Ctx.ViewMgr.Update(m_View);  

            if (!Program.Ctx.ViewMgr.Save(true))
            {
                MessageBox.Show("保存失败！");
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
