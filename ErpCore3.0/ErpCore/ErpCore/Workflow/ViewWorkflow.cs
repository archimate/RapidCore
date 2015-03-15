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
using ErpCoreModel.Workflow;
using ErpCore.Workflow;

namespace ErpCore.Workflow
{
    public partial class ViewWorkflow : Form
    {
        public CBaseObjectMgr m_BaseObjectMgr = null;
        public CBaseObject m_BaseObject = null;

        public ViewWorkflow()
        {
            InitializeComponent();
        }

        private void ViewWorkflow_Load(object sender, EventArgs e)
        {
            LoadWorkflow();
            if (dataGridView.Rows.Count > 0)
            {
                dataGridView.Rows[0].Selected = true;
                CWorkflow wf = (CWorkflow)dataGridView.Rows[0].Tag;
                LoadActives(wf);
            }
        }
        void LoadWorkflow()
        {
            if (m_BaseObjectMgr == null)
                return;
            if (m_BaseObject == null)
                return;
            dataGridView.Rows.Clear();
            List<CWorkflow> lstWF = m_BaseObjectMgr.WorkflowMgr.FindByRowid(m_BaseObject.Id);
            foreach (CWorkflow wf in lstWF)
            {
                CWorkflowDef WorkflowDef = wf.GetWorkflowDef();
                if (WorkflowDef == null)
                    continue;
                dataGridView.Rows.Add(1);
                DataGridViewRow row = dataGridView.Rows[dataGridView.Rows.Count - 1];

                row.Cells[0].Value = WorkflowDef.Name;
                row.Cells[1].Value = wf.Created.ToString();
                row.Cells[2].Value = wf.GetStateString();
                row.Tag = wf;
            }
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dataGridView.Rows.Count)
                return;
            CWorkflow wf = (CWorkflow)dataGridView.Rows[e.RowIndex].Tag;
            LoadActives(wf);
        }
        void LoadActives(CWorkflow wf)
        {
            dataGridView2.Rows.Clear();
            CWorkflowDef WorkflowDef = wf.GetWorkflowDef();
            List<CBaseObject> lstObj= wf.ActivesMgr.GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CActives Actives = (CActives)obj;

                dataGridView2.Rows.Add(1);
                DataGridViewRow row = dataGridView2.Rows[dataGridView2.Rows.Count - 1];

                CActivesDef ActivesDef = (CActivesDef)WorkflowDef.ActivesDefMgr.Find(Actives.WF_ActivesDef_id);
                row.Cells[0].Value = ActivesDef.Name;
                row.Cells[1].Value = Actives.GetResultString();
                row.Cells[2].Value = Actives.Comment;
                CUser user = (CUser)Program.Ctx.UserMgr.Find(Actives.B_User_id);
                if (user != null)
                    row.Cells[3].Value = user.Name;
                row.Tag = Actives;
            }
        }

        private void tbtCancel_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow == null)
            {
                MessageBox.Show("请选择工作流！");
                return;
            }
            CWorkflow wf = (CWorkflow)dataGridView.CurrentRow.Tag;
            //只有启动者或管理员才能撤销
            if (wf.Creator != Program.User.Id
                && !Program.User.IsRole("管理员"))
            {
                MessageBox.Show("没有权限撤销！");
                return;
            }

            if (MessageBox.Show("是否确认撤销？", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;

            if (!m_BaseObjectMgr.WorkflowMgr.CancelWorkflow(wf))
            {
                MessageBox.Show("撤销失败！");
                return;
            }
            dataGridView.CurrentRow.Cells[2].Value = wf.GetStateString();
            MessageBox.Show("撤销成功！");
        }

        private void tbtApproval_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow == null)
            {
                MessageBox.Show("请选择工作流！");
                return;
            }
            CWorkflow wf = (CWorkflow)dataGridView.CurrentRow.Tag;
            if (wf.State != enumApprovalState.Running)
            {
                MessageBox.Show("只有进行中的工作流才能审批！");
                return;
            }
            CActives Actives = wf.ActivesMgr.FindNotApproval();
            if (Actives == null)
            {
                MessageBox.Show("没有审批的活动！");
                return;
            }
            if (Actives.B_User_id != Program.User.Id)
            {
                MessageBox.Show("没有权限审批！");
                return;
            }
            ApprovalActives frm = new ApprovalActives();
            frm.m_Workflow = wf;
            frm.m_Actives = Actives;
            if (frm.ShowDialog() != DialogResult.OK)
                return;

            dataGridView.CurrentRow.Cells[2].Value = wf.GetStateString();
            LoadActives(wf);
            MessageBox.Show("审批完成！");
        }
    }
}
