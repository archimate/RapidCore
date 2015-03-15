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
    public partial class SetViewFilter : Form
    {
        public CView m_View=null;
        //返回过滤条件，只做临时查询条件，不保存设置
        public CViewFilterMgr m_ViewFilterMgr = new CViewFilterMgr();

        public SetViewFilter()
        {
            InitializeComponent();
        }

        private void SetViewFilter_Load(object sender, EventArgs e)
        {
            cbAndOr.SelectedIndex = 0;
            cbSign4.SelectedIndex = 0;

            LoadMasterColumn();
        }

        void LoadMasterColumn()
        {
            if (m_View == null)
                return;

            CTable tb = (CTable)Program.Ctx.TableMgr.Find(m_View.FW_Table_id);
            txtMasterTable4.Text = tb.Name;

            cbMasterColumn4.Items.Clear();
            List<CBaseObject> lstObj = tb.ColumnMgr.GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CColumn column = (CColumn)obj;

                DataItem it = new DataItem(column.Name, column);
                cbMasterColumn4.Items.Add(it);
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
            m_ViewFilterMgr.Delete(vf);
            dataGridView4.Rows.Remove(dataGridView4.SelectedRows[0]);
        }

        private void btAdd4_Click(object sender, EventArgs e)
        {
            CTable tb = (CTable)Program.Ctx.TableMgr.Find(m_View.FW_Table_id);

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
            m_ViewFilterMgr.AddNew(vf);

            dataGridView4.Rows.Add(1);
            DataGridViewRow rowNew = dataGridView4.Rows[dataGridView4.Rows.Count - 1];
            rowNew.Cells[0].Value = vf.AndOr;
            rowNew.Cells[1].Value = col.Name;
            rowNew.Cells[2].Value = vf.GetSignName();
            rowNew.Cells[3].Value = vf.Val;
            rowNew.Tag = vf;

            txtVal4.Text = "";
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView4.Rows.Count; i++)
            {
                CViewFilter vf = (CViewFilter)dataGridView4.Rows[i].Tag;
                vf.Idx = i;
                m_ViewFilterMgr.Update(vf);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
