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
using ErpCoreModel.Report;
using ErpCoreModel.SubSystem;
using ErpCoreModel.Workflow;

namespace ErpCore.Workflow
{
    public partial class ActivesDefInfo : Form
    {
        public CWorkflowDef m_WorkflowDef = null;
        public CActivesDef m_ActivesDef = null;

        bool m_bIsNew = false;

        public ActivesDefInfo()
        {
            InitializeComponent();
        }

        private void ActivesDefInfo_Load(object sender, EventArgs e)
        {
            if (m_ActivesDef == null)
            {
                m_bIsNew = true;
                m_ActivesDef = new CActivesDef();
                m_ActivesDef.Ctx = Program.Ctx;
                m_ActivesDef.WF_WorkflowDef_id = m_WorkflowDef.Id;
                m_ActivesDef.Idx = m_WorkflowDef.ActivesDefMgr.NewIdx();
                m_ActivesDef.WType = ActivesType.Middle;
                CLink link1 = new CLink();
                link1.Ctx = Program.Ctx;
                link1.WF_WorkflowDef_id = m_WorkflowDef.Id;
                link1.PreActives = m_ActivesDef.Id;
                link1.Result = enumApprovalResult.Accept;
                CActivesDef adSuccess = m_WorkflowDef.ActivesDefMgr.FindSuccess();
                if (adSuccess != null)
                    link1.NextActives = adSuccess.Id;
                else
                    link1.NextActives = Guid.Empty;
                m_WorkflowDef.LinkMgr.AddNew(link1);
                CLink link2 = new CLink();
                link2.Ctx = Program.Ctx;
                link2.WF_WorkflowDef_id = m_WorkflowDef.Id;
                link2.PreActives = m_ActivesDef.Id;
                link2.Result = enumApprovalResult.Reject;
                CActivesDef adFailure = m_WorkflowDef.ActivesDefMgr.FindFailure();
                if (adFailure != null)
                    link2.NextActives = adFailure.Id;
                else
                    link2.NextActives = Guid.Empty;
                m_WorkflowDef.LinkMgr.AddNew(link2);
            }
            LoadUser();
            LoadData();
        }
        void LoadUser()
        {
            cbUser.Items.Clear();
            List<CBaseObject> lstObj = Program.Ctx.UserMgr.GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CUser user = (CUser)obj;
                DataItem item = new DataItem();
                item.name = user.Name;
                item.Data = user;
                cbUser.Items.Add(item);
            }
        }
        void LoadData()
        {
            if (m_bIsNew)
                return;
            txtName.Text = m_ActivesDef.Name;
            for (int i = 0; i < cbUser.Items.Count; i++)
            {
                DataItem item = (DataItem)cbUser.Items[i];
                CUser user = (CUser)item.Data;
                if (m_ActivesDef.B_User_id == user.Id)
                {
                    cbUser.SelectedIndex = i;
                    break;
                }
            }
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            if(txtName.Text.Trim()=="")
            {
                MessageBox.Show("名称不能空！");
                return ;
            }
            if(cbUser.SelectedIndex==-1)
            {
                MessageBox.Show("请选择审批人！");
                return ;
            }
            DataItem item = (DataItem)cbUser.SelectedItem;
            CUser user = (CUser)item.Data;
            m_ActivesDef.Name = txtName.Text.Trim();
            m_ActivesDef.B_User_id = user.Id;

            if (m_bIsNew)
                m_WorkflowDef.ActivesDefMgr .AddNew(m_ActivesDef);
            else
                m_WorkflowDef.ActivesDefMgr.Update(m_ActivesDef);
            

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
