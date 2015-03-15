using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;

namespace ErpCore.Database.Table
{
    public partial class DataServerInfoForm : Form
    {
        public CDataServer m_DataServer = null;
        public CDataServerMgr m_DataServerMgr = null;

        public DataServerInfoForm()
        {
            InitializeComponent();
        }


        private void DataServerInfoForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        void LoadData()
        {
            if (m_DataServer == null)
                return;
            txtServer.Text = m_DataServer.Server;
            txtDBName.Text = m_DataServer.DBName;
            txtUserID.Text = m_DataServer.UserID;
            txtPwd.Text = m_DataServer.Pwd;
            ckIsWrite.Checked = m_DataServer.IsWrite;
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            if (txtServer.Text.Trim() == "")
            {
                MessageBox.Show("服务器不能空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtDBName.Text.Trim() == "")
            {
                MessageBox.Show("数据库名不能空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtUserID.Text.Trim() == "")
            {
                MessageBox.Show("登录帐号不能空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtPwd.Text.Trim() == "")
            {
                MessageBox.Show("登录密码不能空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!TestConnect())
                return;

            if (m_DataServer == null)
            {
                List<CBaseObject> lstObj = m_DataServerMgr.GetList();
                foreach (CBaseObject obj in lstObj)
                {
                    CDataServer dserver = (CDataServer)obj;
                    if (dserver.Server.Equals(txtServer.Text.Trim(), StringComparison.OrdinalIgnoreCase)
                        && dserver.DBName.Equals(txtDBName.Text.Trim(), StringComparison.OrdinalIgnoreCase))
                    {
                        MessageBox.Show("服务器已经存在！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        return ; 
                    }
                }
                m_DataServer = new CDataServer();
                m_DataServer.Server = txtServer.Text.Trim();
                m_DataServer.DBName = txtDBName.Text.Trim();
                m_DataServer.UserID = txtUserID.Text.Trim();
                m_DataServer.Pwd = txtPwd.Text.Trim();
                m_DataServer.IsWrite = ckIsWrite.Checked;
                m_DataServer.FW_Table_id = m_DataServerMgr.m_Parent.Id;


                m_DataServerMgr.AddNew(m_DataServer);
            }
            else
            {
                List<CBaseObject> lstObj = m_DataServerMgr.GetList();
                foreach (CBaseObject obj in lstObj)
                {
                    CDataServer dserver = (CDataServer)obj;
                    if (dserver.Server.Equals(txtServer.Text.Trim(), StringComparison.OrdinalIgnoreCase)
                        && dserver.DBName.Equals(txtDBName.Text.Trim(), StringComparison.OrdinalIgnoreCase)
                        && m_DataServer != dserver)
                    {
                        MessageBox.Show("服务器已经存在！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        return;
                    }
                }
                m_DataServer.Server = txtServer.Text.Trim();
                m_DataServer.DBName = txtDBName.Text.Trim();
                m_DataServer.UserID = txtUserID.Text.Trim();
                m_DataServer.Pwd = txtPwd.Text.Trim();
                m_DataServer.IsWrite = ckIsWrite.Checked;

                m_DataServerMgr.Update(m_DataServer);
            }
            this.DialogResult = DialogResult.OK;
        }

        bool TestConnect()
        {
            string connectionString = string.Format("Provider=SQLOLEDB.1;Password={0};Persist Security Info=True;User ID={1};Initial Catalog={2};Data Source={3}",
                   txtPwd.Text.Trim(), txtUserID.Text.Trim(), txtDBName.Text.Trim(), txtServer.Text.Trim());
            OleDbConnection connection = new OleDbConnection(connectionString);
            try { 
                connection.Open();
                connection.Close();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false; 
            }
            return true;
        }
    }
}
