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
    public partial class MultMasterDetailViewInfo : Form
    {
        public CView m_View = null;
        public Guid m_Catalog_id = Guid.Empty;

        bool m_bIsNew = false;
        bool m_bIsLoadingData = false;

        public MultMasterDetailViewInfo()
        {
            InitializeComponent();
        }

        private void MultMasterDetailViewInfo_Load(object sender, EventArgs e)
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
            cbMasterTable.Items.Clear();
            cbDetailTable.Items.Clear();
            List<CBaseObject> lstObj = Program.Ctx.TableMgr.GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CTable table = (CTable)obj;
                DataItem item = new DataItem();
                item.name = table.Name;
                item.Data = table;
                cbMasterTable.Items.Add(item);
                DataItem item2 = new DataItem();
                item2.name = table.Name;
                item2.Data = table;
                cbDetailTable.Items.Add(item2);
            }
        }
        void LoadData()
        {
            if (m_View == null) //新建 
            {
                m_bIsNew = true;
                m_View = new CView();
                m_View.Ctx = Program.Ctx;
                m_View.VType = enumViewType.MultMasterDetail;
                m_View.Creator = Program.User.Id;
                return;
            }
            m_bIsLoadingData = true;

            txtName.Text = m_View.Name;
            //默认表
            for (int i = 0; i < cbMasterTable.Items.Count; i++)
            {
                DataItem item = (DataItem)cbMasterTable.Items[i];
                CTable tb = (CTable)item.Data;
                if (tb.Id == m_View.FW_Table_id)
                {
                    cbMasterTable.SelectedIndex = i;
                    cbMasterTable_SelectedIndexChanged(null, null);
                    break;
                }
            }

            List<CBaseObject> lstObj = m_View.ViewDetailMgr.GetList();
            List<CViewDetail> sortObj = new List<CViewDetail>();
            foreach (CBaseObject obj in lstObj)
            {
                CViewDetail vd = (CViewDetail)obj;
                sortObj.Add(vd);
            }
            sortObj.Sort();

            listViewDetail.Items.Clear();
            foreach (CViewDetail ViewDetail in sortObj)
            {
                ListViewItem it = new ListViewItem();
                it.Text = ViewDetail.Name;
                it.Tag = ViewDetail;
                listViewDetail.Items.Add(it);

                CTable table = (CTable)Program.Ctx.TableMgr.Find(ViewDetail.FW_Table_id);
                DataItem itT = new DataItem(table.Name, table);
                cbDetailTable2.Items.Add(itT);
                DataItem itT2 = new DataItem(table.Name, table);
                cbDetailTable3.Items.Add(itT2);
            }


            LoadGridView();
            LoadGridView4();

            m_bIsLoadingData = false;
        }

        private void cbMasterTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!m_bIsLoadingData)
            {
                m_View.ColumnInViewMgr.RemoveAll();
                List<CBaseObject> lstObj= m_View.ViewDetailMgr.GetList();
                foreach (CBaseObject obj in lstObj)
                {
                    CViewDetail vd = (CViewDetail)obj;
                    vd.PrimaryKey = Guid.Empty;
                }
            }
            LoadPrimaryKey();
            LoadMasterColumn();
            LoadGridView();
            LoadGridView4();
        }
        void LoadPrimaryKey()
        {
            if (cbMasterTable.SelectedIndex == -1)
                return;
            DataItem item = (DataItem)cbMasterTable.SelectedItem;
            CTable table = (CTable)item.Data;
            cbPrimaryKey.Items.Clear();
            List<CBaseObject> lstObj = table.ColumnMgr.GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CColumn column = (CColumn)obj;
                DataItem it = new DataItem(column.Name, column);
                cbPrimaryKey.Items.Add(it);
            }
        }
        void LoadMasterColumn()
        {
            if (cbMasterTable.SelectedIndex == -1)
                return;
            DataItem item = (DataItem)cbMasterTable.SelectedItem;
            CTable tb = (CTable)item.Data;
            txtMasterTable.Text = tb.Name;
            listMasterColumn.Items.Clear();
            List<CBaseObject> lstObj = tb.ColumnMgr.GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CColumn column = (CColumn)obj;
                ListViewItem it = new ListViewItem();
                it.Text = column.Name;
                it.Tag = column;
                listMasterColumn.Items.Add(it);
                if (m_View.ColumnInViewMgr.FindByColumn(column.Id) != null)
                    it.Checked = true;
            }
        }
        void LoadGridView()
        {
            if (cbMasterTable.SelectedIndex == -1)
                return;
            DataItem item = (DataItem)cbMasterTable.SelectedItem;
            CTable tb = (CTable)item.Data;
            txtMasterTable2.Text = tb.Name;
            List<CBaseObject> lstObj = m_View.ColumnInViewMgr.GetList();


            dataGridView.Rows.Clear();
            foreach (CBaseObject obj in lstObj)
            {
                CColumnInView civ = (CColumnInView)obj;
                if (!civ.IsVirtual)
                {
                    CColumn column = (CColumn)tb.ColumnMgr.Find(civ.FW_Column_id);
                    if (column == null)
                        continue;

                    dataGridView.Rows.Add(1);
                    DataGridViewRow rowNew = dataGridView.Rows[dataGridView.Rows.Count - 1];
                    rowNew.Cells[0].Value = column.Name;
                    rowNew.Cells[0].ReadOnly = true;
                    rowNew.Cells[1].Value = civ.Caption;
                    rowNew.Tag = civ;
                }
                else
                {
                    dataGridView.Rows.Add(1);
                    DataGridViewRow rowNew = dataGridView.Rows[dataGridView.Rows.Count - 1];
                    rowNew.Cells[0].Value = civ.Caption;
                    rowNew.Cells[0].ReadOnly = true;
                    rowNew.Cells[1].Value = civ.Caption;
                    rowNew.Tag = civ;
                }
            }
        }

        

        private void btNext1_Click(object sender, EventArgs e)
        {
            if (!ValidatePage1())
                return;
            tabControl1.SelectedIndex=1;
        }
        bool ValidatePage1()
        {
            if (txtName.Text.Trim() == "")
            {
                MessageBox.Show("名称不能空！");
                return false;
            }
            if (cbMasterTable.SelectedIndex == -1)
            {
                MessageBox.Show("请选择主表！");
                return false;
            }
            DataItem itM = (DataItem)cbMasterTable.SelectedItem;
            CTable tbM = (CTable)itM.Data;
            if (listViewDetail.Items.Count == 0)
            {
                MessageBox.Show("请选择从表！");
                return false;
            }
            for (int i = 0; i < listViewDetail.Items.Count; i++)
            {
                CViewDetail vd = (CViewDetail)listViewDetail.Items[i].Tag;
                if (vd.FW_Table_id == tbM.Id)
                {
                    MessageBox.Show("主表不能与从表相同！");
                    return false;
                }
                if (vd.PrimaryKey == Guid.Empty || vd.ForeignKey == Guid.Empty)
                {
                    MessageBox.Show(string.Format("请设置 {0} 外键关联！",listViewDetail.Items[i].Text));
                    return false;
                }
            }
            
            return true;
        }

        private void btPrev2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex=0;
        }

        private void btNext2_Click(object sender, EventArgs e)
        {
            if (!ValidatePage2())
                return;
            tabControl1.SelectedIndex=2;
        }
        bool ValidatePage2()
        {
            if (listMasterColumn.CheckedItems.Count == 0)
            {
                MessageBox.Show("请选择主表字段！");
                return false;
            }
            List<CBaseObject> lstObj= m_View.ViewDetailMgr.GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CViewDetail vd = (CViewDetail)obj;
                if (vd.ColumnInViewDetailMgr.GetList().Count == 0)
                {
                    CTable table = (CTable)Program.Ctx.TableMgr.Find(vd.FW_Table_id);
                    MessageBox.Show(string.Format("请选择从表 {0} 字段！",table.Name));
                    return false;
                }
            }

            return true;
        }

        private void btPrev3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex=1;
        }

        private void btFinish_Click(object sender, EventArgs e)
        {
            if (!ValidatePage1())
                return;
            if (!ValidatePage2())
                return;
            if (!SavePage1())
                return;
            if (!SavePage3())
                return;
            if (!SavePage4())
                return;
            
            if (m_bIsNew)
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
        

        private void listMasterColumn_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            CColumn column = (CColumn)e.Item.Tag;
            if (e.Item.Checked)
            {
                CColumnInView civ = new CColumnInView();
                civ.Ctx = Program.Ctx;
                civ.FW_Column_id = column.Id;
                civ.FW_Table_id = column.FW_Table_id;
                civ.Caption = column.Name;
                civ.UI_View_id = m_View.Id;
                civ.Idx = m_View.ColumnInViewMgr.NewIdx();
                m_View.ColumnInViewMgr.AddNew(civ);

                dataGridView.Rows.Add(1);
                DataGridViewRow rowNew = dataGridView.Rows[dataGridView.Rows.Count - 1];
                rowNew.Cells[0].Value = column.Name;
                rowNew.Cells[0].ReadOnly = true;
                rowNew.Cells[1].Value = civ.Caption;
                rowNew.Tag = civ;
            }
            else
            {
                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    CColumnInView civ = (CColumnInView)dataGridView.Rows[i].Tag;
                    if (civ.FW_Column_id == column.Id)
                    {
                        m_View.ColumnInViewMgr.Delete(civ);
                        dataGridView.Rows.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        private void listDetailColumn_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (m_bIsLoadingData)
                return;
            CColumn column = (CColumn)e.Item.Tag;
            CViewDetail ViewDetail = (CViewDetail)m_View.ViewDetailMgr.FindByTable(column.FW_Table_id);

            if (e.Item.Checked)
            {
                CColumnInViewDetail civd = new CColumnInViewDetail();
                civd.Ctx = Program.Ctx;
                civd.FW_Column_id = column.Id;
                civd.FW_Table_id = column.FW_Table_id;
                civd.Caption = column.Name;
                civd.UI_ViewDetail_id = ViewDetail.Id;
                civd.Idx = ViewDetail.ColumnInViewDetailMgr.NewIdx();
                ViewDetail.ColumnInViewDetailMgr.AddNew(civd);

                if (cbDetailTable3.SelectedIndex != -1)
                {
                    DataItem item = (DataItem)cbDetailTable3.SelectedItem;
                    CTable table = (CTable)item.Data;
                    if (table.Id == column.FW_Table_id)
                    {
                        dataGridView2.Rows.Add(1);
                        DataGridViewRow rowNew = dataGridView2.Rows[dataGridView2.Rows.Count - 1];
                        rowNew.Cells[0].Value = column.Name;
                        rowNew.Cells[0].ReadOnly = true;
                        rowNew.Cells[1].Value = civd.Caption;
                        rowNew.Tag = civd;
                    }
                }
            }
            else
            {
                if (cbDetailTable3.SelectedIndex != -1)
                {
                    DataItem item = (DataItem)cbDetailTable3.SelectedItem;
                    CTable table = (CTable)item.Data;
                    if (table.Id == column.FW_Table_id)
                    {
                        for (int i = 0; i < dataGridView2.Rows.Count; i++)
                        {
                            CColumnInViewDetail civd = (CColumnInViewDetail)dataGridView2.Rows[i].Tag;
                            if (civd.FW_Column_id == column.Id)
                            {
                                dataGridView2.Rows.RemoveAt(i);
                                break;
                            }
                        }
                    }
                }
                CColumnInViewDetail obj = (CColumnInViewDetail)ViewDetail.ColumnInViewDetailMgr.FindByColumn(column.Id);
                if (obj != null)
                    ViewDetail.ColumnInViewDetailMgr.Delete(obj);
            }
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
            CColumnInView civ = (CColumnInView)row.Tag;
            civ.Idx--;
            CColumnInView civ2 = (CColumnInView)row2.Tag;
            civ2.Idx++;
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
            CColumnInView civ = (CColumnInView)row.Tag;
            civ.Idx++;
            CColumnInView civ2 = (CColumnInView)row2.Tag;
            civ2.Idx--;
        }

        private void btUp2_Click(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow == null)
            {
                MessageBox.Show("请选择一行！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (dataGridView2.CurrentRow.Index == 0)
                return;
            int idx = dataGridView2.CurrentRow.Index;
            DataGridViewRow row = dataGridView2.CurrentRow;
            DataGridViewRow row2 = dataGridView2.Rows[idx - 1];
            dataGridView2.Rows.Remove(row2);
            dataGridView2.Rows.Insert(idx, row2);
            CColumnInViewDetail civd = (CColumnInViewDetail)row.Tag;
            civd.Idx--;
            CColumnInViewDetail civd2 = (CColumnInViewDetail)row2.Tag;
            civd2.Idx++;
        }

        private void btDown2_Click(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow == null)
            {
                MessageBox.Show("请选择一行！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (dataGridView2.CurrentRow.Index == dataGridView2.Rows.Count - 1)
                return;

            int idx = dataGridView2.CurrentRow.Index;
            DataGridViewRow row = dataGridView2.CurrentRow;
            DataGridViewRow row2 = dataGridView2.Rows[idx + 1];
            dataGridView2.Rows.Remove(row2);
            dataGridView2.Rows.Insert(idx, row2);
            CColumnInViewDetail civd = (CColumnInViewDetail)row.Tag;
            civd.Idx++;
            CColumnInViewDetail civd2 = (CColumnInViewDetail)row2.Tag;
            civd2.Idx--;
        }

        bool SavePage1()
        {
            string sName = txtName.Text.Trim();
            if (!sName.Equals(m_View.Name))
            {
                CView view2 = Program.Ctx.ViewMgr.FindByName(sName);
                if (view2 != null)
                {
                    MessageBox.Show("名称重复！");
                    txtName.Focus();
                    return false;
                }
            }
            m_View.Name = sName;
            if (cbCatalog.SelectedIndex > 0)
            {
                DataItem item = (DataItem)cbCatalog.SelectedItem;
                CViewCatalog catalog = (CViewCatalog)item.Data;
                m_View.UI_ViewCatalog_id = catalog.Id;
            }
            else
                m_View.UI_ViewCatalog_id = Guid.Empty;
            DataItem itemP = (DataItem)cbMasterTable.SelectedItem;
            CTable tableP = (CTable)itemP.Data;
            m_View.FW_Table_id = tableP.Id;
            for (int i = 0; i < listViewDetail.Items.Count; i++)
            {
                CViewDetail vd = (CViewDetail)listViewDetail.Items[i].Tag;
                vd.Idx = i;
            }

            return true;
        }
        bool SavePage3()
        {
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                CColumnInView civ = (CColumnInView)dataGridView.Rows[i].Tag;
                civ.Idx = i;
                civ.Caption = dataGridView.Rows[i].Cells[1].Value.ToString();
                m_View.ColumnInViewMgr.Update(civ);
            }

            return true;
        }
        bool SavePage4()
        {
            for (int i = 0; i < dataGridView4.Rows.Count; i++)
            {
                CViewFilter vf = (CViewFilter)dataGridView4.Rows[i].Tag;
                vf.Idx = i;
                m_View.ViewFilterMgr.Update(vf);
            }
            return true;
        }

        private void btUp0_Click(object sender, EventArgs e)
        {
            if (listViewDetail.SelectedItems.Count==0)
            {
                MessageBox.Show("请选择一行！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (listViewDetail.SelectedItems[0].Index == 0)
                return;
            int idx = listViewDetail.SelectedItems[0].Index;
            ListViewItem row = listViewDetail.SelectedItems[0];
            ListViewItem row2 = listViewDetail.Items[idx - 1];
            listViewDetail.Items.Remove(row2);
            listViewDetail.Items.Insert(idx, row2);
            CViewDetail vd = (CViewDetail)row.Tag;
            vd.Idx--;
            CViewDetail vd2 = (CViewDetail)row2.Tag;
            vd2.Idx++;

            LoadDetailTable();
        }

        private void btDown0_Click(object sender, EventArgs e)
        {
            if (listViewDetail.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选择一行！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (listViewDetail.SelectedItems[0].Index == listViewDetail.Items.Count - 1)
                return;

            int idx = listViewDetail.SelectedItems[0].Index;
            ListViewItem row = listViewDetail.SelectedItems[0];
            ListViewItem row2 = listViewDetail.Items[idx + 1];
            listViewDetail.Items.Remove(row2);
            listViewDetail.Items.Insert(idx, row2);
            CViewDetail vd = (CViewDetail)row.Tag;
            vd.Idx++;
            CViewDetail vd2 = (CViewDetail)row2.Tag;
            vd2.Idx--;

            LoadDetailTable();
        }

        private void btDel0_Click(object sender, EventArgs e)
        {
            if (listViewDetail.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选择一行！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            CViewDetail ViewDetail = (CViewDetail)listViewDetail.SelectedItems[0].Tag;
            m_View.ViewDetailMgr.Delete(ViewDetail);
            listViewDetail.Items.Remove(listViewDetail.SelectedItems[0]);
            for (int i = 0; i < listViewDetail.Items.Count; i++)
            {
                CViewDetail vd = (CViewDetail)listViewDetail.Items[i].Tag;
                vd.Idx = i;
                vd.m_ObjectMgr.Update(vd);
            }

            LoadDetailTable();
        }

        private void btAddDetailTable_Click(object sender, EventArgs e)
        {
            if (cbDetailTable.SelectedIndex == -1)
            {
                MessageBox.Show("请选择从表！");
                return;
            }
            DataItem item = (DataItem)cbDetailTable.SelectedItem;
            CTable table = (CTable)item.Data;
            for (int i = 0; i < listViewDetail.Items.Count; i++)
            {
                CViewDetail vd = (CViewDetail)listViewDetail.Items[i].Tag;
                if (vd.FW_Table_id == table.Id)
                {
                    MessageBox.Show("从表已经存在！");
                    return;
                }
            }
            if (cbMasterTable.SelectedIndex != -1)
            {
                DataItem itM = (DataItem)cbMasterTable.SelectedItem;
                CTable tbM = (CTable)itM.Data;
                if (tbM.Id == table.Id)
                {
                    MessageBox.Show("从表不能与主表相同！");
                    return;
                }
            }
            CViewDetail ViewDetail = new CViewDetail();
            ViewDetail.Ctx = Program.Ctx;
            ViewDetail.FW_Table_id = table.Id;
            ViewDetail.Name = table.Name;
            ViewDetail.UI_View_id = m_View.Id;
            ViewDetail.Idx = listViewDetail.Items.Count;
            m_View.ViewDetailMgr.AddNew(ViewDetail);

            ListViewItem it = new ListViewItem();
            it.Text = ViewDetail.Name;
            it.Tag = ViewDetail;
            listViewDetail.Items.Add(it);

            LoadDetailTable();
        }
        void LoadDetailTable()
        {
            List<CBaseObject> lstObj = m_View.ViewDetailMgr.GetList();
            List< CViewDetail> sortObj = new List< CViewDetail>();
            foreach (CBaseObject obj in lstObj)
            {
                CViewDetail vd = (CViewDetail)obj;
                sortObj.Add(vd);
            }
            sortObj.Sort();
            cbDetailTable2.Items.Clear();
            cbDetailTable3.Items.Clear();
            foreach (CViewDetail ViewDetail in sortObj)
            {
                CTable table = (CTable)Program.Ctx.TableMgr.Find(ViewDetail.FW_Table_id);
                DataItem it = new DataItem(table.Name, table);
                cbDetailTable2.Items.Add(it);
                DataItem it2 = new DataItem(table.Name, table);
                cbDetailTable3.Items.Add(it2);
            }
            listDetailColumn.Items.Clear();
            dataGridView2.Rows.Clear();
        }

        private void listViewDetail_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewDetail.SelectedItems.Count == 0)
                return;
            CViewDetail ViewDetail = (CViewDetail)listViewDetail.SelectedItems[0].Tag;
            CTable table = (CTable)Program.Ctx.TableMgr.Find(ViewDetail.FW_Table_id);

            cbForeignKey.Items.Clear();
            List<CBaseObject> lstObj = table.ColumnMgr.GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CColumn column = (CColumn)obj;
                DataItem it = new DataItem(column.Name, column);
                cbForeignKey.Items.Add(it);
            }
            cbPrimaryKey.SelectedIndex = -1;
            for (int i = 0; i < cbPrimaryKey.Items.Count; i++)
            {
                DataItem it = (DataItem)cbPrimaryKey.Items[i];
                CColumn column = (CColumn)it.Data;
                if (column.Id == ViewDetail.PrimaryKey)
                {
                    cbPrimaryKey.SelectedIndex = i;
                    break;
                }
            }
            cbForeignKey.SelectedIndex = -1;
            for (int i = 0; i < cbForeignKey.Items.Count; i++)
            {
                DataItem it = (DataItem)cbForeignKey.Items[i];
                CColumn column = (CColumn)it.Data;
                if (column.Id == ViewDetail.ForeignKey)
                {
                    cbForeignKey.SelectedIndex = i;
                    break;
                }
            }
        }

        private void cbPrimaryKey_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewDetail.SelectedItems.Count == 0)
                return;
            if (cbPrimaryKey.Focused == false) //避免赋值触发事件
                return;
            CViewDetail ViewDetail = (CViewDetail)listViewDetail.SelectedItems[0].Tag;
            if (cbPrimaryKey.SelectedIndex == -1)
            {
                ViewDetail.PrimaryKey = Guid.Empty;
                return;
            }
            DataItem item = (DataItem)cbPrimaryKey.SelectedItem;
            CColumn column = (CColumn)item.Data;
            ViewDetail.PrimaryKey = column.Id;
        }

        private void cbForeignKey_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewDetail.SelectedItems.Count == 0)
                return;
            if (cbForeignKey.Focused == false) //避免赋值触发事件
                return;
            if (cbForeignKey.SelectedIndex == -1)
                return;
            CViewDetail ViewDetail = (CViewDetail)listViewDetail.SelectedItems[0].Tag;
            DataItem item = (DataItem)cbForeignKey.SelectedItem;
            CColumn column = (CColumn)item.Data;
            ViewDetail.ForeignKey = column.Id;
        }

        private void cbDetailTable2_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDetailColumn();
        }
        void LoadDetailColumn()
        {
            if (cbDetailTable2.SelectedIndex == -1)
                return;
            m_bIsLoadingData = true;//避免触发Item_Checked事件
            DataItem item = (DataItem)cbDetailTable2.SelectedItem;
            CTable table = (CTable)item.Data;
            CViewDetail ViewDetail = m_View.ViewDetailMgr.FindByTable(table.Id);

            listDetailColumn.Items.Clear();
            List<CBaseObject> lstObj = table.ColumnMgr.GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CColumn column = (CColumn)obj;
                ListViewItem it = new ListViewItem();
                it.Text = column.Name;
                it.Tag = column;
                listDetailColumn.Items.Add(it);
                if (ViewDetail.ColumnInViewDetailMgr.FindByColumn(column.Id) != null)
                    it.Checked = true;
            }
            m_bIsLoadingData = false;
        }

        private void cbDetailTable3_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataGridView2();
        }
        void LoadDataGridView2()
        {
            if (cbDetailTable3.SelectedIndex == -1)
                return;
            DataItem item = (DataItem)cbDetailTable3.SelectedItem;
            CTable table = (CTable)item.Data;
            CViewDetail ViewDetail = m_View.ViewDetailMgr.FindByTable(table.Id);
            dataGridView2.Rows.Clear();
            List<CBaseObject> lstObj = ViewDetail.ColumnInViewDetailMgr.GetList();
            
            foreach (CBaseObject obj in lstObj)
            {
                CColumnInViewDetail civd = (CColumnInViewDetail)obj;
                if (!civd.IsVirtual)
                {
                    CColumn column = (CColumn)table.ColumnMgr.Find(civd.FW_Column_id);

                    dataGridView2.Rows.Add(1);
                    DataGridViewRow rowNew = dataGridView2.Rows[dataGridView2.Rows.Count - 1];
                    rowNew.Cells[0].Value = column.Name;
                    rowNew.Cells[0].ReadOnly = true;
                    rowNew.Cells[1].Value = civd.Caption;
                    rowNew.Tag = civd;
                }
                else
                {
                    dataGridView2.Rows.Add(1);
                    DataGridViewRow rowNew = dataGridView2.Rows[dataGridView2.Rows.Count - 1];
                    rowNew.Cells[0].Value = civd.Caption;
                    rowNew.Cells[0].ReadOnly = true;
                    rowNew.Cells[1].Value = civd.Caption;
                    rowNew.Tag = civd;
                }
            }
        }

        private void btNext3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 3;
        }

        private void btPrev4_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 3;
        }

        void LoadGridView4()
        {
            if (cbMasterTable.SelectedIndex == -1)
                return;
            DataItem item = (DataItem)cbMasterTable.SelectedItem;
            CTable tb = (CTable)item.Data;
            txtMasterTable4.Text = tb.Name;
            List<CBaseObject> lstObj = m_View.ViewFilterMgr.GetList();
            List<CViewFilter> sortObj = new List<CViewFilter>();
            foreach (CBaseObject obj in lstObj)
            {
                CViewFilter vf = (CViewFilter)obj;
                sortObj.Add(vf);
            }
            sortObj.Sort();

            dataGridView4.Rows.Clear();
            foreach (CViewFilter vf in sortObj)
            {
                CColumn column = (CColumn)tb.ColumnMgr.Find(vf.FW_Column_id);
                if (column == null)
                    continue;

                dataGridView4.Rows.Add(1);
                DataGridViewRow rowNew = dataGridView4.Rows[dataGridView4.Rows.Count - 1];
                rowNew.Cells[0].Value = vf.AndOr;
                rowNew.Cells[1].Value = column.Name;
                rowNew.Cells[2].Value = vf.GetSignName();
                rowNew.Cells[3].Value = vf.Val;
                rowNew.Tag = vf;
            }
        }
        private void btDel4_Click(object sender, EventArgs e)
        {
            if (dataGridView4.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择行！");
                return;
            }
            CViewFilter vf = (CViewFilter)dataGridView4.SelectedRows[0].Tag;
            m_View.ViewFilterMgr.Delete(vf);
            dataGridView4.Rows.Remove(dataGridView4.SelectedRows[0]);
        }

        private void btAdd4_Click(object sender, EventArgs e)
        {
            if (cbMasterTable.SelectedIndex == -1)
                return;
            DataItem item = (DataItem)cbMasterTable.SelectedItem;
            CTable tb = (CTable)item.Data;

            if (cbMasterColumn4.SelectedIndex == -1)
            {
                MessageBox.Show("请选择字段！");
                return;
            }
            if (cbSign4.SelectedIndex == -1)
            {
                MessageBox.Show("请选择比较符号！");
                return;
            }
            if (txtVal4.Text.Trim() == "")
            {
                MessageBox.Show("请输入字段值！");
                return;
            }

            DataItem item2 = (DataItem)cbMasterColumn4.SelectedItem;
            CColumn col = (CColumn)item2.Data;

            CViewFilter vf = new CViewFilter();
            vf.Ctx = Program.Ctx;
            vf.UI_View_id = m_View.Id;
            vf.AndOr = (cbAndOr.SelectedIndex == 0) ? "and" : "or";
            vf.FW_Table_id = tb.Id;
            vf.FW_Column_id = col.Id;
            vf.Sign = (CompareSign)cbSign4.SelectedIndex;
            vf.Val = txtVal4.Text.Trim();
            m_View.ViewFilterMgr.AddNew(vf);

            dataGridView4.Rows.Add(1);
            DataGridViewRow rowNew = dataGridView4.Rows[dataGridView4.Rows.Count - 1];
            rowNew.Cells[0].Value = vf.AndOr;
            rowNew.Cells[1].Value = col.Name;
            rowNew.Cells[2].Value = vf.GetSignName();
            rowNew.Cells[3].Value = vf.Val;
            rowNew.Tag = vf;

            txtVal4.Text = "";
        }

        private void btAddVirCol_Click(object sender, EventArgs e)
        {
            if (txtVirCol.Text.Trim() == "")
            {
                MessageBox.Show("请输入虚列标题！");
                return;
            }
            foreach (DataGridViewRow r in dataGridView.Rows)
            {
                if (r.Cells[1].Value.ToString().Equals(txtVirCol.Text.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("标题已经存在！");
                    return;
                }
            }

            CColumnInView civ = new CColumnInView();
            civ.Ctx = m_View.Ctx;
            civ.UI_View_id = m_View.Id;
            civ.IsVirtual = true;
            civ.Caption = txtVirCol.Text.Trim();
            m_View.ColumnInViewMgr.AddNew(civ);

            dataGridView.Rows.Add();
            DataGridViewRow rowNew = dataGridView.Rows[dataGridView.Rows.Count - 1];
            rowNew.Cells[0].Value = civ.Caption;
            rowNew.Cells[0].ReadOnly = true;
            rowNew.Cells[1].Value = civ.Caption;
            rowNew.Tag = civ;

            txtVirCol.Text = "";
        }

        private void btAddVirCol2_Click(object sender, EventArgs e)
        {
            if (txtVirCol2.Text.Trim() == "")
            {
                MessageBox.Show("请输入虚列标题！");
                return;
            }
            foreach (DataGridViewRow r in dataGridView2.Rows)
            {
                if (r.Cells[1].Value.ToString().Equals(txtVirCol2.Text.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("标题已经存在！");
                    return;
                }
            }

            CViewDetail ViewDetail = (CViewDetail)m_View.ViewDetailMgr.GetFirstObj();

            CColumnInViewDetail civd = new CColumnInViewDetail();
            civd.Ctx = ViewDetail.Ctx;
            civd.UI_ViewDetail_id = ViewDetail.Id;
            civd.IsVirtual = true;
            civd.Caption = txtVirCol2.Text.Trim();
            ViewDetail.ColumnInViewDetailMgr.AddNew(civd);

            dataGridView2.Rows.Add();
            DataGridViewRow rowNew = dataGridView2.Rows[dataGridView2.Rows.Count - 1];
            rowNew.Cells[0].Value = civd.Caption;
            rowNew.Cells[0].ReadOnly = true;
            rowNew.Cells[1].Value = civd.Caption;
            rowNew.Tag = civd;

            txtVirCol2.Text = "";
        }
    }
}
