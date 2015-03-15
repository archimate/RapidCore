using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.Report;

namespace ErpCore.Report
{
    public partial class SelStatItem : Form
    {
        public CReport m_Report = null;

        public SelStatItem()
        {
            InitializeComponent();
        }

        private void SelStatItem_Load(object sender, EventArgs e)
        {
            LoadTable();
            LoadData();
        }
        void LoadData()
        {
            listSelColumn.Items.Clear();
            foreach (CBaseObject obj in m_Report.StatItemMgr.GetList())
            {
                CStatItem StatItem = (CStatItem)obj;

                ListViewItem item = new ListViewItem();
                if (StatItem.ItemType == CStatItem.enumItemType.Field)
                {
                    CTable table = (CTable)Program.Ctx.TableMgr.Find(StatItem.FW_Table_id);
                    if (table == null)
                        continue;
                    CColumn column = (CColumn)table.ColumnMgr.Find(StatItem.FW_Column_id);
                    item.Text = table.Name;
                    item.SubItems.Add(column.Name);
                }
                else
                {
                    item.Text = StatItem.Name;
                    item.SubItems.Add(StatItem.Formula);
                }
                item.Tag = StatItem;
                listSelColumn.Items.Add(item);
            }
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

        private void cbTable_SelectedIndexChanged(object sender, EventArgs e)
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
                for (int j = 0; j < listSelColumn.Items.Count; j++)
                {
                    CStatItem SItem = (CStatItem)listSelColumn.Items[j].Tag;
                    if (SItem.ItemType == CStatItem.enumItemType.Field
                        && SItem.FW_Column_id == column.Id)
                    {
                        bHas = true;
                        break;
                    }
                }
                if (bHas)
                    continue;

                CStatItem StatItem = new CStatItem();
                StatItem.Ctx = Program.Ctx;
                StatItem.RPT_Report_id = m_Report.Id;
                StatItem.Name = column.Name;
                StatItem.FW_Table_id = column.FW_Table_id;
                StatItem.FW_Column_id = column.Id;
                m_Report.StatItemMgr.AddNew(StatItem);

                ListViewItem item = new ListViewItem();
                item.Text = cbTable.SelectedItem.ToString();
                item.SubItems.Add(column.Name);
                item.Tag = StatItem;
                listSelColumn.Items.Add(item);
            }

        }

        private void btDel_Click(object sender, EventArgs e)
        {
            if (listSelColumn.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选择要删除的字段！");
                return;
            }
            foreach (ListViewItem item in listSelColumn.SelectedItems)
            {
                CStatItem StatItem = (CStatItem)item.Tag;
                m_Report.StatItemMgr.Delete(StatItem);
                listSelColumn.Items.Remove(item);
            }
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            if (listSelColumn.Items.Count == 0)
            {
                MessageBox.Show("请选择字段！");
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void listColumn_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btAdd_Click(null, null);
        }

        private void listSelColumn_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btDel_Click(null, null);
        }

        private void btAddFormula_Click(object sender, EventArgs e)
        {
            if (txtAsName.Text.Trim() == "")
            {
                MessageBox.Show("请输入别名！");
                return;
            }
            if (txtFormula.Text.Trim() == "")
            {
                MessageBox.Show("请输入公式！");
                return;
            }

            CStatItem StatItem = new CStatItem();
            StatItem.Ctx = Program.Ctx;
            StatItem.ItemType = CStatItem.enumItemType.Formula;
            StatItem.RPT_Report_id = m_Report.Id;
            StatItem.Name = txtAsName.Text.Trim();
            StatItem.Formula = txtFormula.Text.Trim();
            m_Report.StatItemMgr.AddNew(StatItem);


            ListViewItem item = new ListViewItem();
            item.Text = txtAsName.Text.Trim();
            item.SubItems.Add(txtFormula.Text.Trim());
            item.Tag = StatItem;
            listSelColumn.Items.Add(item);
        }
    }
}
