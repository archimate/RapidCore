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
using ErpCore.Database.Table;

namespace ErpCore.Workflow
{
    public partial class WorkflowDefInfo : Form
    {
        public CWorkflowDef m_WorkflowDef = null;
        public Guid m_Catalog_id = Guid.Empty;
        public CCompany m_Company = null;

        bool m_bIsNew = false;

        public WorkflowDefInfo()
        {
            InitializeComponent();
        }

        private void WorkflowDefInfo_Load(object sender, EventArgs e)
        {
            
            LoadCatalog();
            LoadData();
        }
        void LoadCatalog()
        {
            cbCatalog.Items.Clear();
            cbCatalog.Items.Add("");
            int iDefaultIdx = 0;
            List<CBaseObject> lstObj = m_Company.WorkflowCatalogMgr.GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CWorkflowCatalog catalog = (CWorkflowCatalog)obj;
                DataItem item = new DataItem(catalog.Name, catalog);
                int idx = cbCatalog.Items.Add(item);
                if (catalog.Id == m_Catalog_id)
                    iDefaultIdx = idx;
            }
            cbCatalog.SelectedIndex = iDefaultIdx;
        }
        void LoadData()
        {
            if (m_WorkflowDef == null)
            {
                m_bIsNew = true;
                m_WorkflowDef = new CWorkflowDef();
                m_WorkflowDef.Ctx = Program.Ctx;
                m_WorkflowDef.WF_WorkflowCatalog_id = m_Catalog_id;
                CActivesDef startActivesDef = new CActivesDef();
                startActivesDef.Ctx = Program.Ctx;
                startActivesDef.WF_WorkflowDef_id = m_WorkflowDef.Id;
                startActivesDef.WType = ActivesType.Start;
                startActivesDef.Name = "启动";
                startActivesDef.Idx = 0;
                m_WorkflowDef.ActivesDefMgr.AddNew(startActivesDef);

                CActivesDef SuccessActivesDef = new CActivesDef();
                SuccessActivesDef.Ctx = Program.Ctx;
                SuccessActivesDef.WF_WorkflowDef_id = m_WorkflowDef.Id;
                SuccessActivesDef.WType = ActivesType.Success;
                SuccessActivesDef.Name = "成功结束";
                SuccessActivesDef.Idx = -1;
                m_WorkflowDef.ActivesDefMgr.AddNew(SuccessActivesDef);

                CActivesDef FailureActivesDef = new CActivesDef();
                FailureActivesDef.Ctx = Program.Ctx;
                FailureActivesDef.WF_WorkflowDef_id = m_WorkflowDef.Id;
                FailureActivesDef.WType = ActivesType.Failure;
                FailureActivesDef.Name = "失败结束";
                FailureActivesDef.Idx = -2;
                m_WorkflowDef.ActivesDefMgr.AddNew(FailureActivesDef);

                CLink Link = new CLink();
                Link.Ctx = Program.Ctx;
                Link.WF_WorkflowDef_id = m_WorkflowDef.Id;
                Link.Result = enumApprovalResult.Accept;
                Link.PreActives = startActivesDef.Id;
                Link.NextActives = SuccessActivesDef.Id;
                m_WorkflowDef.LinkMgr.AddNew(Link);
            }

            txtName.Text = m_WorkflowDef.Name;
            

            CTable Table = (CTable)Program.Ctx.TableMgr.Find(m_WorkflowDef.FW_Table_id);
            if (Table != null)
                txtTable.Text = Table.Name;
            LoadActives();
            if (dataGridView.Rows.Count > 0)
            {
                CActivesDef ActivesDef = (CActivesDef)dataGridView.Rows[0].Tag;
                LoadLink(ActivesDef);
            }
        }
        void LoadActives()
        {
            dataGridView.Rows.Clear();
            List<CBaseObject> lstObj= m_WorkflowDef.ActivesDefMgr.GetList();
            //按序号排序
            SortedList<int, CActivesDef> sortObj = new SortedList<int, CActivesDef>();
            foreach (CBaseObject obj in lstObj)
            {
                CActivesDef ActivesDef=(CActivesDef)obj;
                sortObj.Add(ActivesDef.Idx,ActivesDef);
            }
            CActivesDef SuccessActivesDef = null;
            CActivesDef FailureActivesDef = null;
            foreach(KeyValuePair<int,CActivesDef> pair in sortObj)
            {
                CActivesDef ActivesDef = pair.Value;
                if (ActivesDef.WType == ActivesType.Success)
                {
                    SuccessActivesDef = ActivesDef;
                    continue;
                }
                if (ActivesDef.WType == ActivesType.Failure)
                {
                    FailureActivesDef = ActivesDef;
                    continue;
                }
                dataGridView.Rows.Add(1);
                DataGridViewRow item = dataGridView.Rows[dataGridView.Rows.Count - 1];
                item.Cells[0].Value = ActivesDef.Idx;
                item.Cells[1].Value = ActivesDef.Name;
                CUser user = (CUser)Program.Ctx.UserMgr.Find(ActivesDef.B_User_id);
                if(user!=null)
                    item.Cells[2].Value = user.Name;
                item.Tag = ActivesDef;
            }
            //成功/失败结束活动放最后
            if (SuccessActivesDef != null)
            {
                dataGridView.Rows.Add(1);
                DataGridViewRow item = dataGridView.Rows[dataGridView.Rows.Count - 1];
                item.Cells[0].Value = SuccessActivesDef.Idx;
                item.Cells[1].Value = SuccessActivesDef.Name;
                CUser user = (CUser)Program.Ctx.UserMgr.Find(SuccessActivesDef.B_User_id);
                if (user != null)
                    item.Cells[2].Value = user.Name;
                item.Tag = SuccessActivesDef;
            }
            if (FailureActivesDef != null)
            {
                dataGridView.Rows.Add(1);
                DataGridViewRow item = dataGridView.Rows[dataGridView.Rows.Count - 1];
                item.Cells[0].Value = FailureActivesDef.Idx;
                item.Cells[1].Value = FailureActivesDef.Name;
                CUser user = (CUser)Program.Ctx.UserMgr.Find(FailureActivesDef.B_User_id);
                if (user != null)
                    item.Cells[2].Value = user.Name;
                item.Tag = FailureActivesDef;
            }
        }
        private void tbtNew_Click(object sender, EventArgs e)
        {
            ActivesDefInfo frm = new ActivesDefInfo();
            frm.m_WorkflowDef = m_WorkflowDef;
            if (frm.ShowDialog() != DialogResult.OK)
                return;
            LoadActives();
        }

        private void tbtEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow == null)
            {
                MessageBox.Show("请先选择活动！");
                return;
            }
            CActivesDef ActivesDef = (CActivesDef)dataGridView.CurrentRow.Tag;
            if (ActivesDef.WType == ActivesType.Start)
            {
                MessageBox.Show("启动活动不能修改！");
                return;
            }
            if (ActivesDef.WType == ActivesType.Success)
            {
                MessageBox.Show("成功结束活动不能修改！");
                return;
            }
            if (ActivesDef.WType == ActivesType.Failure)
            {
                MessageBox.Show("失败结束活动不能修改！");
                return;
            }

            ActivesDefInfo frm = new ActivesDefInfo();
            frm.m_WorkflowDef = m_WorkflowDef;
            frm.m_ActivesDef = ActivesDef;
            if (frm.ShowDialog() != DialogResult.OK)
                return;
            LoadActives();
        }

        private void tbtDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow == null)
            {
                MessageBox.Show("请先选择活动！");
                return;
            } 
            CActivesDef ActivesDef = (CActivesDef)dataGridView.CurrentRow.Tag;
            if (ActivesDef.WType == ActivesType.Start)
            {
                MessageBox.Show("启动活动不能删除！");
                return;
            }
            if (ActivesDef.WType == ActivesType.Success)
            {
                MessageBox.Show("成功结束活动不能删除！");
                return;
            }
            if (ActivesDef.WType == ActivesType.Failure)
            {
                MessageBox.Show("失败结束活动不能删除！");
                return;
            }

            m_WorkflowDef.ActivesDefMgr.Delete(ActivesDef);
            //删除相关的连接
            List<CLink> lstLink = m_WorkflowDef.LinkMgr.FindByPreActives(ActivesDef.Id);
            foreach (CLink Link in lstLink)
            {
                m_WorkflowDef.LinkMgr.Delete(Link);
            }

            dataGridView.Rows.Remove(dataGridView.CurrentRow);
            dataGridView2.Rows.Clear();
        }

        private void tbtNew2_Click(object sender, EventArgs e)
        {
            if (m_WorkflowDef.FW_Table_id == Guid.Empty)
            {
                MessageBox.Show("请先选择表对象！");
                return;
            }
            if (dataGridView.CurrentRow == null)
            {
                MessageBox.Show("请先选择活动！");
                return;
            }
            CActivesDef ActivesDef = (CActivesDef)dataGridView.CurrentRow.Tag;
            if (ActivesDef.WType == ActivesType.Start)
            {
                MessageBox.Show("启动活动有且仅有一个连接！");
                return;
            }
            else if (ActivesDef.WType == ActivesType.Success
                || ActivesDef.WType == ActivesType.Failure)
            {
                MessageBox.Show("结束活动不能有连接！");
                return;
            }

            LinkInfo frm = new LinkInfo();
            frm.m_WorkflowDef = m_WorkflowDef;
            frm.m_PreActives = ActivesDef;
            if (frm.ShowDialog() != DialogResult.OK)
                return;
            LoadLink(ActivesDef);

        }

        private void tbtEdit2_Click(object sender, EventArgs e)
        {
            if (m_WorkflowDef.FW_Table_id == Guid.Empty)
            {
                MessageBox.Show("请先选择表对象！");
                return;
            }
            if (dataGridView.CurrentRow == null)
            {
                MessageBox.Show("请先选择活动！");
                return;
            }
            CActivesDef ActivesDef = (CActivesDef)dataGridView.CurrentRow.Tag;

            if (dataGridView2.CurrentRow == null)
            {
                MessageBox.Show("请先选择连接！");
                return;
            }
            CLink Link = (CLink)dataGridView2.CurrentRow.Tag;

            LinkInfo frm = new LinkInfo();
            frm.m_WorkflowDef = m_WorkflowDef;
            frm.m_PreActives = ActivesDef;
            frm.m_Link = Link;
            if (frm.ShowDialog() != DialogResult.OK)
                return;
            LoadLink(ActivesDef);

        }

        private void tbtDel2_Click(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow == null)
            {
                MessageBox.Show("请先选择连接！");
                return;
            }

            CActivesDef ActivesDef = (CActivesDef)dataGridView.CurrentRow.Tag;
            if (ActivesDef.WType == ActivesType.Start)
            {
                MessageBox.Show("启动活动有且仅有一个连接！");
                return;
            }

            CLink Link = (CLink)dataGridView2.CurrentRow.Tag;
            m_WorkflowDef.LinkMgr.Delete(Link);

            dataGridView2.Rows.Remove(dataGridView2.CurrentRow);
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim() == "")
            {
                MessageBox.Show("名称不能空！");
                return;
            }
            if (txtTable.Text.Trim()=="")
            {
                MessageBox.Show("请选择对象表！");
                return;
            }
            m_WorkflowDef.Name = txtName.Text.Trim();
            if (cbCatalog.SelectedIndex < 1)
                m_WorkflowDef.WF_WorkflowCatalog_id = Guid.Empty;
            else
            {
                DataItem item = (DataItem)cbCatalog.SelectedItem;
                CWorkflowCatalog catalog = (CWorkflowCatalog)item.Data;
                m_WorkflowDef.WF_WorkflowCatalog_id = catalog.Id;
            }
            //保证启动活动有后置活动，中间活动有前置活动与后置活动
            List<CBaseObject> lstObj = m_WorkflowDef.ActivesDefMgr.GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CActivesDef ActivesDef = (CActivesDef)obj;
                if (ActivesDef.WType == ActivesType.Start)
                {
                    bool bHas = false;
                    List<CLink> lstLink= m_WorkflowDef.LinkMgr.FindByPreActives(ActivesDef.Id);
                    foreach (CLink link in lstLink)
                    {
                        CActivesDef Next = (CActivesDef)m_WorkflowDef.ActivesDefMgr.Find(link.NextActives);
                        if (Next.WType == ActivesType.Middle)
                        {
                            bHas = true;
                            break;
                        }
                    }
                    if (!bHas)
                    {
                        MessageBox.Show("启动活动没有实际后置活动！");
                        return;
                    }
                }
                else if (ActivesDef.WType == ActivesType.Middle)
                {
                    List<CLink> lstLink = m_WorkflowDef.LinkMgr.FindByPreActives(ActivesDef.Id);
                    if (lstLink.Count == 0)
                    {
                        MessageBox.Show(string.Format("{0} 没有后置活动！",ActivesDef.Name));
                        return;
                    }
                    List<CLink> lstLink2 = m_WorkflowDef.LinkMgr.FindByPreActives(ActivesDef.Id);
                    if (lstLink2.Count == 0)
                    {
                        MessageBox.Show(string.Format("{0} 没有前置活动！", ActivesDef.Name));
                        return;
                    }
                }
            }


            if (m_bIsNew)
                m_Company.WorkflowDefMgr.AddNew(m_WorkflowDef);
            else
                m_Company.WorkflowDefMgr.Update(m_WorkflowDef);

            if (!m_Company.WorkflowDefMgr.Save(true))
            {
                MessageBox.Show("保存失败！");
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dataGridView.Rows.Count)
                return;
            CActivesDef ActivesDef = (CActivesDef)dataGridView.Rows[e.RowIndex].Tag;
            LoadLink(ActivesDef);
        }
        void LoadLink(CActivesDef ActivesDef)
        {
            dataGridView2.Rows.Clear();
            List<CBaseObject> lstObj = m_WorkflowDef.LinkMgr.GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CLink link = (CLink)obj;
                if (link.PreActives == ActivesDef.Id)
                {
                    dataGridView2.Rows.Add(1);
                    DataGridViewRow item = dataGridView2.Rows[dataGridView2.Rows.Count - 1];
                    item.Cells[0].Value = link.Result;
                    item.Cells[1].Value = link.Condiction;
                    CActivesDef NextActivesDef = (CActivesDef)m_WorkflowDef.ActivesDefMgr.Find(link.NextActives);
                    if (NextActivesDef != null)
                        item.Cells[2].Value = NextActivesDef.Name;
                    item.Tag = link;
                }
            }
        }

        private void btSelTable_Click(object sender, EventArgs e)
        {
            SelTableForm frm = new SelTableForm();
            if (frm.ShowDialog() != DialogResult.OK)
                return;
            txtTable.Text = frm.m_SelTable.Name;
            m_WorkflowDef.FW_Table_id = frm.m_SelTable.Id;
        }
    }
}
