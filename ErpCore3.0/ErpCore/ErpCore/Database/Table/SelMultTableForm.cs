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
    public partial class SelMultTableForm : Form
    {
        public List<CTable> m_lstSelTable = new List<CTable>();

        public SelMultTableForm()
        {
            InitializeComponent();
        }

        private void SelTableForm_Load(object sender, EventArgs e)
        {
            LoadHead();
            LoadList();
        }
        void LoadHead()
        {
            listTable.Columns.Clear();
            listTable.Columns.Add("名称", 100);
            listTable.Columns.Add("编码", 100);
            listTable.Columns.Add("系统表", 100);
            listTable.Columns.Add("是否完成", 100);
            listTable.Columns.Add("创建时间", 100);
        }
        public void LoadList()
        {
            listTable.Items.Clear();
            List<CBaseObject> lstTable = Program.Ctx.TableMgr.GetList();
            foreach (CBaseObject obj in lstTable)
            {
                CTable tb = (CTable)obj;

                ListViewItem item = new ListViewItem();
                item.Text = tb.Name;
                item.SubItems.Add(tb.Code);
                item.SubItems.Add(tb.IsSystem.ToString());
                item.SubItems.Add(tb.IsFinish.ToString());
                item.SubItems.Add(tb.Created.ToShortDateString());
                item.Tag = tb;
                listTable.Items.Add(item);
            }
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            m_lstSelTable.Clear();
            foreach (ListViewItem item in listTable.Items)
            {
                if (item.Checked)
                {
                    CTable table = (CTable)item.Tag;
                    m_lstSelTable.Add(table);
                }
            }
            if (m_lstSelTable.Count == 0)
            {
                MessageBox.Show("请选择一项或多项！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult = DialogResult.OK;
        }
    }
}
