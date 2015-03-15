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
using ErpCoreModel.UI;


namespace ErpCore.View
{
    public partial class SetDefaultVal : Form
    {
        public CView m_View = null;
        CTable m_SelTable = null;

        public SetDefaultVal()
        {
            InitializeComponent();
        }

        private void SetDefaultVal_Load(object sender, EventArgs e)
        {
            LoadTable();
            if (cbTable.Items.Count > 0)
                cbTable.SelectedIndex = 0;
        }
        void LoadTable()
        {
            cbTable.Items.Clear();
            CTable table = (CTable)Program.Ctx.TableMgr.Find(m_View.FW_Table_id);
            DataItem item = new DataItem(table.Name,table);
            cbTable.Items.Add(item);
            List<CBaseObject> lstObj = m_View.ViewDetailMgr.GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CViewDetail ViewDetail = (CViewDetail)obj;
                CTable tableD = (CTable)Program.Ctx.TableMgr.Find(ViewDetail.FW_Table_id);
                DataItem item2 = new DataItem(tableD.Name, tableD);
                cbTable.Items.Add(item2);
            }
        }
        void LoadGridView()
        {
            if (cbTable.SelectedIndex == -1)
                return;
            DataItem item = (DataItem)cbTable.SelectedItem;
            CTable table = (CTable)item.Data;
            m_SelTable = table;
            List<CBaseObject> lstObj = table.ColumnMgr.GetList();

            dataGridView.Rows.Clear();
            foreach (CBaseObject obj in lstObj)
            {
                CColumn col = (CColumn)obj;
                if (col.Code.Equals("id", StringComparison.OrdinalIgnoreCase)
                    ||col.Code.Equals("Creator", StringComparison.OrdinalIgnoreCase)
                    || col.Code.Equals("Created", StringComparison.OrdinalIgnoreCase)
                    || col.Code.Equals("Updator", StringComparison.OrdinalIgnoreCase)
                    || col.Code.Equals("Updated", StringComparison.OrdinalIgnoreCase))
                    continue;
                if (table.Id == m_View.FW_Table_id) //主表
                {
                    CColumnDefaultValInView cdviv = m_View.ColumnDefaultValInViewMgr.FindByColumn(col.Id);

                    dataGridView.Rows.Add(1);
                    DataGridViewRow rowNew = dataGridView.Rows[dataGridView.Rows.Count - 1];
                    rowNew.Cells[0].Value = col.Name;
                    rowNew.Cells[0].ReadOnly = true;
                    if (cdviv != null)
                    {
                        rowNew.Cells[1].Value = cdviv.DefaultVal;
                        rowNew.Cells[2].Value = cdviv.ReadOnly;
                    }
                    rowNew.Tag = col;
                }
                else //从表
                {
                    CViewDetail ViewDetail= m_View.ViewDetailMgr.FindByTable(table.Id);
                    CColumnDefaultValInViewDetail cdvivd = ViewDetail.ColumnDefaultValInViewDetailMgr.FindByColumn(col.Id);

                    dataGridView.Rows.Add(1);
                    DataGridViewRow rowNew = dataGridView.Rows[dataGridView.Rows.Count - 1];
                    rowNew.Cells[0].Value = col.Name;
                    rowNew.Cells[0].ReadOnly = true;
                    if (cdvivd != null)
                    {
                        rowNew.Cells[1].Value = cdvivd.DefaultVal;
                        rowNew.Cells[2].Value = cdvivd.ReadOnly;
                    }
                    rowNew.Tag = col;
                }
            }
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            UpdateGridView();

            if (!m_View.ColumnDefaultValInViewMgr.Save(true))
            {
                MessageBox.Show("保存失败！");
                return;
            }

            MessageBox.Show("保存成功！");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void btCancel_Click(object sender, EventArgs e)
        {
            m_View.ColumnDefaultValInViewMgr.Cancel();
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        //在切换表前把旧表设置保存
        void UpdateGridView()
        {
            if (m_SelTable == null)
                return;
            if (m_SelTable.Id == m_View.FW_Table_id) //主表
            {
                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    DataGridViewRow row = dataGridView.Rows[i];
                    CColumn col = (CColumn)row.Tag;
                    if (row.Cells[1].Value == null || row.Cells[1].Value.ToString().Trim() == "")
                    {
                        m_View.ColumnDefaultValInViewMgr.RemoveByColumn(col.Id);
                    }
                    else
                    {
                        CColumnDefaultValInView cdviv = m_View.ColumnDefaultValInViewMgr.FindByColumn(col.Id);
                        if (cdviv == null)
                        {
                            cdviv = new CColumnDefaultValInView();
                            cdviv.Ctx = Program.Ctx;
                            cdviv.FW_Table_id = m_SelTable.Id;
                            cdviv.FW_Column_id = col.Id;
                            cdviv.DefaultVal = row.Cells[1].Value.ToString().Trim();
                            cdviv.ReadOnly =(row.Cells[2].Value!=null)? (bool)row.Cells[2].Value:false;
                            cdviv.UI_View_id = m_View.Id;
                            m_View.ColumnDefaultValInViewMgr.AddNew(cdviv);
                        }
                        else
                        {
                            cdviv.DefaultVal = row.Cells[1].Value.ToString().Trim();
                            cdviv.ReadOnly = (row.Cells[2].Value != null) ? (bool)row.Cells[2].Value : false;
                            m_View.ColumnDefaultValInViewMgr.Update(cdviv);
                        }
                    }
                }
            }
            else //从表
            {
                CViewDetail ViewDetail = m_View.ViewDetailMgr.FindByTable(m_SelTable.Id);
                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    DataGridViewRow row = dataGridView.Rows[i];
                    CColumn col = (CColumn)row.Tag;
                    if (row.Cells[1].Value == null || row.Cells[1].Value.ToString().Trim() == "")
                    {
                        ViewDetail.ColumnDefaultValInViewDetailMgr.RemoveByColumn(col.Id);
                    }
                    else
                    {
                        CColumnDefaultValInViewDetail cdvivd = ViewDetail.ColumnDefaultValInViewDetailMgr.FindByColumn(col.Id);
                        if (cdvivd == null)
                        {
                            cdvivd = new CColumnDefaultValInViewDetail();
                            cdvivd.Ctx = Program.Ctx;
                            cdvivd.FW_Table_id = m_SelTable.Id;
                            cdvivd.FW_Column_id = col.Id;
                            cdvivd.DefaultVal = row.Cells[1].Value.ToString().Trim();
                            cdvivd.ReadOnly = (row.Cells[2].Value != null) ? (bool)row.Cells[2].Value : false;
                            cdvivd.UI_ViewDetail_id = ViewDetail.Id;
                            ViewDetail.ColumnDefaultValInViewDetailMgr.AddNew(cdvivd);
                        }
                        else
                        {
                            cdvivd.DefaultVal = row.Cells[1].Value.ToString().Trim();
                            cdvivd.ReadOnly = (row.Cells[2].Value != null) ? (bool)row.Cells[2].Value : false;
                            ViewDetail.ColumnDefaultValInViewDetailMgr.Update(cdvivd);
                        }
                    }
                }
            }
        }

        private void cbTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            //在切换表前把旧表设置保存
            UpdateGridView();

            LoadGridView();
        }

        private void btVariable_Click(object sender, EventArgs e)
        {
            VariableForm frm = new VariableForm();
            frm.ShowDialog();
        }

    }
}
