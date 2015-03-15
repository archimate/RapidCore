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
using ErpCoreModel.SubSystem;

namespace ErpCore.Report
{
    public partial class ReportWizard : Form
    {
        public CReport m_Report = null;
        public Guid m_Catalog_id = Guid.Empty;
        public CCompany m_Company = null;

        bool m_bIsNew = false;

        public ReportWizard()
        {
            InitializeComponent();
        }

        private void ReportWizard_Load(object sender, EventArgs e)
        {
            LoadCatalog();
            LoadTable();
            LoadSign();

            LoadData();
        }
        void LoadCatalog()
        {
            cbCatalog.Items.Clear();
            cbCatalog.Items.Add("");
            int iDefaultIdx = 0;
            List<CBaseObject> lstObj = m_Company.ReportCatalogMgr.GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CReportCatalog catalog = (CReportCatalog)obj;
                DataItem item = new DataItem(catalog.Name, catalog);
                int idx = cbCatalog.Items.Add(item);
                if (catalog.Id ==m_Catalog_id)
                    iDefaultIdx = idx;
            }
            cbCatalog.SelectedIndex = iDefaultIdx;
        }
        void LoadData()
        {
            if (m_Report == null)
            {
                m_bIsNew = true;
                m_Report = new CReport();
                m_Report.Ctx = Program.Ctx;
                m_Report.RPT_ReportCatalog_id = m_Catalog_id;
                m_Report.B_Company_id = m_Company.Id;
            }

            txtName.Text = m_Report.Name;
            

            dataGridStatItem.Rows.Clear();
            foreach(CBaseObject obj in m_Report.StatItemMgr.GetList())
            {
                CStatItem StatItem = (CStatItem)obj;
                if (StatItem.ItemType == CStatItem.enumItemType.Field)
                {
                    CTable table = (CTable)Program.Ctx.TableMgr.Find(StatItem.FW_Table_id);
                    if (table == null)
                        continue;
                    CColumn column = (CColumn)table.ColumnMgr.Find(StatItem.FW_Column_id);
                    if (column == null)
                        continue;


                    dataGridStatItem.Rows.Add(1);
                    DataGridViewRow item = dataGridStatItem.Rows[dataGridStatItem.Rows.Count - 1];
                    item.Cells[0].Value = table.Name;
                    item.Cells[1].Value = column.Name;
                    //DataGridViewComboBoxCell cbCell = (DataGridViewComboBoxCell)item.Cells[2];
                    //if(cbCell!=null)
                    //    cbCell.Value = "val";
                    item.Cells[2].Value = StatItem.GetStatTypeName();
                    item.Cells[3].Value = StatItem.GetOrderName();
                    item.Tag = StatItem;
                }
                else
                {
                    dataGridStatItem.Rows.Add(1);
                    DataGridViewRow item = dataGridStatItem.Rows[dataGridStatItem.Rows.Count - 1];
                    item.Cells[0].Value = StatItem.Name;
                    item.Cells[1].Value = StatItem.Formula;
                    //DataGridViewComboBoxCell cbCell = (DataGridViewComboBoxCell)item.Cells[2];
                    //if(cbCell!=null)
                    //    cbCell.Value = "val";
                    item.Cells[2].Value = StatItem.GetStatTypeName();
                    item.Cells[3].Value = StatItem.GetOrderName();
                    item.Tag = StatItem;
                }

            }

            txtFilter.Text = m_Report.Filter;
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
        void LoadSign()
        {
            cbSign.Items.Clear();
            cbSign.Items.Add("=");
            cbSign.Items.Add(">");
            cbSign.Items.Add(">=");
            cbSign.Items.Add("<");
            cbSign.Items.Add("<=");
            cbSign.Items.Add("<>");
            cbSign.Items.Add("like");
        }

        private void cbTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTable.SelectedIndex == -1)
                return;
            cbColumn.Items.Clear();
            DataItem it = (DataItem)cbTable.SelectedItem;
            CTable table = (CTable)it.Data;
            List<CBaseObject> lstObj = table.ColumnMgr.GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CColumn column = (CColumn)obj;
                DataItem item = new DataItem();
                item.name = column.Name;
                item.Data = column;
                cbColumn.Items.Add(item);
            }
        }

        private void btNext_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }

        private void btPrev2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }

        private void btSet_Click(object sender, EventArgs e)
        {
            SelStatItem frm = new SelStatItem();
            frm.m_Report = m_Report;
            if (frm.ShowDialog() != DialogResult.OK)
                return;
            dataGridStatItem.Rows.Clear();
            foreach (CBaseObject obj in m_Report.StatItemMgr.GetList())
            {
                CStatItem StatItem = (CStatItem)obj;
                string sTableName = "", sColumnName = "";
                if (StatItem.ItemType == CStatItem.enumItemType.Field)
                {
                    CTable table = (CTable)Program.Ctx.TableMgr.Find(StatItem.FW_Table_id);
                    if (table != null)
                    {
                        sTableName = table.Name;
                        CColumn column = (CColumn)table.ColumnMgr.Find(StatItem.FW_Column_id);
                        if (column != null)
                            sColumnName = column.Name;
                    }
                }
                else
                {
                    sTableName = StatItem.Name;
                    sColumnName = StatItem.Formula;
                }

                dataGridStatItem.Rows.Add(1);
                DataGridViewRow item = dataGridStatItem.Rows[dataGridStatItem.Rows.Count-1];
                item.Cells[0].Value = sTableName;
                item.Cells[1].Value = sColumnName;
                //DataGridViewComboBoxCell cbCell = (DataGridViewComboBoxCell)item.Cells[2];
                //cbCell.Value = "取数";
                item.Cells[2].Value = "取数";
                item.Cells[3].Value = "默认";
                item.Tag = StatItem;

            }
        }

        private void btUp_Click(object sender, EventArgs e)
        {
            if (dataGridStatItem.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择一项！");
                return;
            }
            if (dataGridStatItem.SelectedRows[0] == dataGridStatItem.Rows[0])
                return;
            int idx = dataGridStatItem.SelectedRows[0].Index;
            DataGridViewRow item = dataGridStatItem.SelectedRows[0];
            dataGridStatItem.Rows.Remove(dataGridStatItem.SelectedRows[0]);
            dataGridStatItem.Rows.Insert(idx - 1, item);
        }

        private void btDown_Click(object sender, EventArgs e)
        {
            if (dataGridStatItem.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择一项！");
                return;
            }
            if (dataGridStatItem.SelectedRows[0] == dataGridStatItem.Rows[dataGridStatItem.Rows.Count - 1])
                return;
            int idx = dataGridStatItem.SelectedRows[0].Index;
            DataGridViewRow item = dataGridStatItem.SelectedRows[0];
            dataGridStatItem.Rows.Remove(dataGridStatItem.SelectedRows[0]);
            dataGridStatItem.Rows.Insert(idx + 1, item);

        }
        void UpdateStatItem()
        {
            foreach (DataGridViewRow item in dataGridStatItem.Rows)
            {
                CStatItem StatItem = (CStatItem)item.Tag;
                StatItem.Idx = item.Index;
                StatItem.SetStatTypeByName(item.Cells[2].Value.ToString());
                StatItem.SetOrderByName(item.Cells[3].Value.ToString());

                m_Report.StatItemMgr.Update(StatItem);
            }
        }
        private void btFinish_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim() == "")
            {
                MessageBox.Show("请输入报表名称！");
                return;
            }
            if (dataGridStatItem.Rows.Count==0)
            {
                MessageBox.Show("请选择统计指标！");
                return;
            }

            UpdateStatItem();

            m_Report.Name = txtName.Text.Trim();
            if (cbCatalog.SelectedIndex < 1)
                m_Report.RPT_ReportCatalog_id = Guid.Empty;
            else
            {
                DataItem item = (DataItem)cbCatalog.SelectedItem;
                CReportCatalog catalog = (CReportCatalog)item.Data;
                m_Report.RPT_ReportCatalog_id = catalog.Id;
            }
            
            m_Report.Filter = txtFilter.Text.Trim();


            if (m_bIsNew)
                m_Company.ReportMgr.AddNew(m_Report);
            else
                m_Company.ReportMgr.Update(m_Report);
            if (!m_Company.ReportMgr.Save(true))
            {
                MessageBox.Show("保存失败！");
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            if (cbTable.SelectedIndex == -1)
            {
                MessageBox.Show("请选择表！");
                return;
            }
            if (cbColumn.SelectedIndex == -1)
            {
                MessageBox.Show("请选择字段！");
                return;
            }
            if (cbSign.SelectedIndex == -1)
            {
                MessageBox.Show("请选择条件符号！");
                return;
            }
            if (txtVal.Text.Trim()=="")
            {
                MessageBox.Show("请输入值！");
                return;
            }
            DataItem itTb = (DataItem)cbTable.SelectedItem;
            CTable table = (CTable)itTb.Data;
            DataItem itCol = (DataItem)cbColumn.SelectedItem;
            CColumn column = (CColumn)itCol.Data;

            string sVal=txtVal.Text.Trim();
            if (column.ColType == ColumnType.guid_type
                || column.ColType == ColumnType.datetime_type
                || column.ColType == ColumnType.ref_type
                || column.ColType == ColumnType.string_type
                || column.ColType == ColumnType.text_type)
            {
                if(sVal[0]!='\''||sVal[sVal.Length-1]!='\'')
                    sVal = "'" + sVal + "'";
            }

            string sCond = string.Format("[{0}].[{1}]{2}{3}",
                table.Code,
                column.Code,
                cbSign.SelectedItem.ToString(),
                sVal);
            if (txtFilter.Text.Trim() == "")
            {
                txtFilter.Text = sCond;
            }
            else
            {
                if (rdAnd.Checked)
                    sCond = " and " + sCond;
                else
                    sCond = " or " + sCond;

                txtFilter.Text += sCond;
            }
        }

        private void dataGridStatItem_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            ComboBox comb = e.Control as ComboBox;
            if (comb != null)
            {
                comb.SelectedIndexChanged += new EventHandler(comb_SelectedIndexChanged);
            }
        }

        void comb_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comb = sender as ComboBox;
            if (dataGridStatItem.SelectedCells.Count == 0)
                return;
            int nColIdx = dataGridStatItem.SelectedCells[0].ColumnIndex;
            int nRowIdx = dataGridStatItem.SelectedCells[0].RowIndex;
            if (nColIdx == 2)
            {
                if(comb.SelectedIndex==0
                    ||comb.SelectedIndex==5)
                    return;

                CStatItem StatItem = (CStatItem)dataGridStatItem.Rows[nRowIdx].Tag;
                if (StatItem.ItemType == CStatItem.enumItemType.Field)
                {
                    CTable table = (CTable)Program.Ctx.TableMgr.Find(StatItem.FW_Table_id);
                    if (table == null)
                        return;
                    CColumn column = (CColumn)table.ColumnMgr.Find(StatItem.FW_Column_id);
                    if (column == null)
                        return;
                    if (column.ColType != ColumnType.int_type
                        && column.ColType != ColumnType.long_type
                        && column.ColType != ColumnType.numeric_type)
                    {
                        MessageBox.Show("非数值型指标！");
                        comb.SelectedIndex = 0;
                        return;
                    }
                }
            }
        }

    }
}
