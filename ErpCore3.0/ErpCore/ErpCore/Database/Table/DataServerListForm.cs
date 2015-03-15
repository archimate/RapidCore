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

namespace ErpCore.Database.Table
{
    public partial class DataServerListForm : Form
    {
        public CTable m_Table = null;

        public DataServerListForm()
        {
            InitializeComponent();
        }

        private void DataServerListForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        public void LoadData()
        {
            if (m_Table == null)
                return;
            dataGridView.Rows.Clear();
            List<CBaseObject> lstObj = m_Table.DataServerMgr.GetList();
            if (lstObj.Count == 0)
            {
                string connStr = Program.Ctx.ConnectionString.ToLower();
                int idx1 = connStr.IndexOf("password=") + "password=".Length;
                int idx2 = connStr.IndexOf(";",idx1);
                string sPwd = (idx2 == -1) ? connStr.Substring(idx1) : connStr.Substring(idx1, idx2 - idx1);
                idx1 = connStr.IndexOf("user id=") + "user id=".Length;
                idx2 = connStr.IndexOf(";", idx1);
                string sUid = (idx2 == -1) ? connStr.Substring(idx1) : connStr.Substring(idx1, idx2 - idx1);
                idx1 = connStr.IndexOf("initial catalog=") + "initial catalog=".Length;
                idx2 = connStr.IndexOf(";", idx1);
                string sDbName = (idx2 == -1) ? connStr.Substring(idx1) : connStr.Substring(idx1, idx2 - idx1);
                idx1 = connStr.IndexOf("data source=") + "data source=".Length;
                idx2 = connStr.IndexOf(";", idx1);
                string sServer = (idx2 == -1) ? connStr.Substring(idx1) : connStr.Substring(idx1, idx2 - idx1);

                CDataServer DataServer = new CDataServer();
                DataServer.Server = sServer;
                DataServer.DBName = sDbName;
                DataServer.UserID = sUid;
                DataServer.Pwd = sPwd;
                DataServer.FW_Table_id = m_Table.Id;
                DataServer.IsWrite = true;

                m_Table.DataServerMgr.AddNew(DataServer);
                lstObj = m_Table.DataServerMgr.GetList();
            }

            dataGridView.Rows.Add(lstObj.Count);
            int nRowIdx = 0;
            foreach (CBaseObject obj in lstObj)
            {
                CDataServer DataServer = (CDataServer)obj;

                DataGridViewRow row = dataGridView.Rows[nRowIdx];
                row.Tag = obj;
                row.Cells[0].Value = DataServer.Server;
                row.Cells[1].Value = DataServer.DBName;
                row.Cells[2].Value = DataServer.UserID;
                row.Cells[3].Value = DataServer.Pwd;
                DataGridViewCheckBoxCell ckCell = (DataGridViewCheckBoxCell)row.Cells[4];
                ckCell.Value = DataServer.IsWrite;

                nRowIdx++;
            }
        }

        private void tbtNew_Click(object sender, EventArgs e)
        {
            DataServerInfoForm frm = new DataServerInfoForm();
            frm.m_DataServerMgr = m_Table.DataServerMgr;
            if (frm.ShowDialog() != DialogResult.OK)
            {
                m_Table.DataServerMgr.Cancel();
                return;
            }
            LoadData();
        }

        private void tbtEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow == null)
            {
                MessageBox.Show("请选择一项！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            CDataServer DataServer = (CDataServer)dataGridView.CurrentRow.Tag;

            DataServerInfoForm frm = new DataServerInfoForm();
            frm.m_DataServer = DataServer;
            frm.m_DataServerMgr = m_Table.DataServerMgr;
            if (frm.ShowDialog() != DialogResult.OK)
            {
                m_Table.DataServerMgr.Cancel();
                return;
            }
            LoadData();
        }

        private void tbtDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow == null)
            {
                MessageBox.Show("请选择一项！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            CDataServer DataServer = (CDataServer)dataGridView.CurrentRow.Tag;
            m_Table.DataServerMgr.Delete(DataServer,true);

            dataGridView.Rows.Remove(dataGridView.CurrentRow);
        }
    }
}
