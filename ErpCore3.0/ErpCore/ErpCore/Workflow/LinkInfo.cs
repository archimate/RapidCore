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
    public partial class LinkInfo : Form
    {
        public CWorkflowDef m_WorkflowDef = null;
        public CActivesDef m_PreActives = null;
        public CLink m_Link = null;

        bool m_bIsNew = false;

        public LinkInfo()
        {
            InitializeComponent();
        }

        private void LinkInfo_Load(object sender, EventArgs e)
        {
            if (m_Link == null)
            {
                m_bIsNew = true;
                m_Link = new CLink();
                m_Link.Ctx = Program.Ctx;
                m_Link.WF_WorkflowDef_id = m_WorkflowDef.Id;
                m_Link.PreActives = m_PreActives.Id;
                m_Link.Result = enumApprovalResult.Accept;
            }
            LoadResult();
            LoadAndOr();
            LoadColumn();
            LoadSign();
            LoadNextActives();
            LoadData();
        }
        void LoadResult()
        {
            cbResult.Items.Clear();
            cbResult.Items.Add("接受");
            cbResult.Items.Add("拒绝");
            cbResult.SelectedIndex = 0;
        }
        void LoadAndOr()
        {
            cbAndOr.Items.Clear();
            cbAndOr.Items.Add("与");
            cbAndOr.Items.Add("或");
            cbAndOr.SelectedIndex = 0;
        }
        void LoadColumn()
        {
            cbColumn.Items.Clear();
            CTable Table = (CTable)Program.Ctx.TableMgr.Find(m_WorkflowDef.FW_Table_id);
            if (Table == null)
                return;
            List<CBaseObject> lstObj = Table.ColumnMgr.GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CColumn Column=(CColumn)obj;
                DataItem item = new DataItem(Column.Name, Column);
                cbColumn.Items.Add(item);
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
            cbSign.SelectedIndex = 0;
        }
        void LoadNextActives()
        {
            cbNextActives.Items.Clear();
            List<CBaseObject> lstObj = m_WorkflowDef.ActivesDefMgr.GetList();
            foreach(CBaseObject obj in lstObj)
            {
                CActivesDef ActivesDef = (CActivesDef)obj;
                if (ActivesDef == m_PreActives)
                    continue;
                if (ActivesDef.WType == ActivesType.Start)
                    continue;
                DataItem item = new DataItem();
                item.name = ActivesDef.Name;
                item.Data = ActivesDef;
                cbNextActives.Items.Add(item);
            }
        }
        void LoadData()
        {
            txtPreActives.Text = m_PreActives.Name;
            if (m_bIsNew)
                return;
            if (m_Link.Result == enumApprovalResult.Accept)
                cbResult.SelectedIndex = 0;
            else
                cbResult.SelectedIndex = 1;
            txtCondiction.Text = m_Link.Condiction;
            for (int i = 0; i < cbNextActives.Items.Count; i++)
            {
                DataItem item = (DataItem)cbNextActives.Items[i];
                CActivesDef ActivesDef = (CActivesDef)item.Data;
                if (ActivesDef.Id == m_Link.NextActives)
                {
                    cbNextActives.SelectedIndex = i;
                    break;
                }
            }
            //启动活动
            if (m_PreActives.WType == ActivesType.Start)
            {
                cbResult.Enabled = false;
                btAdd.Enabled = false;
            }
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            if (cbColumn.SelectedIndex == -1)
            {
                MessageBox.Show("请选择字段！");
                return;
            }
            string sExp = "";
            if (txtCondiction.Text.Trim() != "")
            {
                if (cbAndOr.SelectedIndex == 0)
                    sExp += " and ";
                else
                    sExp += " or ";
            }
            DataItem item = (DataItem)cbColumn.SelectedItem;
            CColumn Column = (CColumn)item.Data;
            sExp += string.Format("[{0}]", cbColumn.Text);
            sExp += cbSign.Text;
            string sVal = txtVal.Text.Trim();
            if (Column.ColType == ColumnType.int_type
                || Column.ColType == ColumnType.long_type
                || Column.ColType == ColumnType.numeric_type
                || Column.ColType == ColumnType.bool_type)
            {
                if (sVal == "")
                    sVal = "0";
                else
                {
                    try { Convert.ToDouble(sVal); }
                    catch
                    {
                        MessageBox.Show("请输入数值型！");
                        txtVal.Focus();
                        return;
                    }
                }
            }
            else
            {
                if (sVal == "")
                    sVal = "''";
                else
                {
                    if (sVal[0] != '\'')
                        sVal = "\'" + sVal;
                    if (sVal[sVal.Length - 1] != '\'')
                        sVal += "\'";
                }
            }
            sExp += sVal;

            txtCondiction.Text += sExp;
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            if (cbNextActives.SelectedIndex == -1)
            {
                MessageBox.Show("请选择后置活动！");
                return;
            }
            DataItem item = (DataItem)cbNextActives.SelectedItem;
            CActivesDef NextActivesDef = (CActivesDef)item.Data;

            m_Link.Result = (cbResult.SelectedIndex == 0) ? enumApprovalResult.Accept : enumApprovalResult.Reject;
            m_Link.Condiction = txtCondiction.Text.Trim();
            m_Link.NextActives = NextActivesDef.Id;


            if (m_bIsNew)
                m_WorkflowDef.LinkMgr.AddNew(m_Link);
            else
                m_WorkflowDef.LinkMgr.Update(m_Link);


            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
