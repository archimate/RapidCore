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
    public partial class ApprovalActives : Form
    {
        public CWorkflow m_Workflow = null;
        public CActives m_Actives = null;

        public ApprovalActives()
        {
            InitializeComponent();
        }

        private void ApprovalActives_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        void LoadData()
        {
            CActivesDefMgr ActivesDefMgr = new CActivesDefMgr();
            ActivesDefMgr.Ctx = Program.Ctx;
            string sWhere = string.Format("id='{0}'", m_Actives.WF_ActivesDef_id);
            ActivesDefMgr.GetList(sWhere);
            CActivesDef ActivesDef = (CActivesDef)ActivesDefMgr.GetFirstObj();
            if(ActivesDef!=null)
                txtName.Text = ActivesDef.Name;
        }

        private void btAccept_Click(object sender, EventArgs e)
        {
            m_Actives.Result = enumApprovalResult.Accept;
            m_Actives.Comment = txtComment.Text.Trim();

            string sErr = "";
            CWorkflowMgr WorkflowMgr = (CWorkflowMgr)m_Workflow.m_ObjectMgr;
            if (!WorkflowMgr.Approval(m_Workflow, m_Actives, out sErr))
            {
                MessageBox.Show(sErr);
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btReject_Click(object sender, EventArgs e)
        {
            m_Actives.Result = enumApprovalResult.Reject;
            m_Actives.Comment = txtComment.Text.Trim();

            string sErr = "";
            CWorkflowMgr WorkflowMgr = (CWorkflowMgr)m_Workflow.m_ObjectMgr;
            if (!WorkflowMgr.Approval(m_Workflow,m_Actives,out sErr))
            {
                MessageBox.Show(sErr);
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
