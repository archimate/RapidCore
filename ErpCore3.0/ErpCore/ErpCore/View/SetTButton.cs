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
    public partial class SetTButton : Form
    {
        public CView m_View = null;

        public SetTButton()
        {
            InitializeComponent();
        }

        private void SetTButton_Load(object sender, EventArgs e)
        {
            LoadGridView();
        }
        void LoadGridView()
        {
            List<CTButtonInView> lstObj = m_View.TButtonInViewMgr.GetListOrderByIdx();

            dataGridView.Rows.Clear();
            foreach (CTButtonInView tiv in lstObj)
            {
                dataGridView.Rows.Add(1);
                DataGridViewRow rowNew = dataGridView.Rows[dataGridView.Rows.Count - 1];
                rowNew.Cells[0].Value = tiv.Caption;
                rowNew.Cells[1].Value = tiv.Url;
                rowNew.Tag = tiv;
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

        private void btDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择行！");
                return;
            }
            CTButtonInView tiv = (CTButtonInView)dataGridView.SelectedRows[0].Tag;
            m_View.TButtonInViewMgr.Delete(tiv);
            dataGridView.Rows.Remove(dataGridView.SelectedRows[0]);
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            if (txtCaption.Text.Trim() == "")
            {
                MessageBox.Show("请输入标题！");
                return;
            }
            if (txtUrl.Text.Trim() == "")
            {
                MessageBox.Show("请输入Url！");
                return;
            }

            CTButtonInView tiv = new CTButtonInView();
            tiv.Ctx = Program.Ctx;
            tiv.UI_View_id = m_View.Id;
            tiv.Caption = txtCaption.Text.Trim();
            tiv.Url = txtUrl.Text.Trim();
            tiv.Creator = Program.User.Id;
            m_View.TButtonInViewMgr.AddNew(tiv);

            dataGridView.Rows.Add(1);
            DataGridViewRow rowNew = dataGridView.Rows[dataGridView.Rows.Count - 1];
            rowNew.Cells[0].Value = tiv.Caption;
            rowNew.Cells[1].Value = tiv.Url;
            rowNew.Tag = tiv;

            txtCaption.Text = "";
            txtUrl.Text = "";
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            m_View.Cancel();
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                CTButtonInView tiv = (CTButtonInView)dataGridView.Rows[i].Tag;
                tiv.Idx = i;
                m_View.ViewFilterMgr.Update(tiv);
            }

            if (!m_View.Save(true))
            {
                MessageBox.Show("保存失败！","错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
