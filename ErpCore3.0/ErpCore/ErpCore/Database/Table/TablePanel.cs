using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;

using System.Text;
using System.Windows.Forms;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCore.Window;


namespace ErpCore.Database.Table
{
    public partial class TablePanel : UserControl
    {
        public TablePanel()
        {
            InitializeComponent();
        }

        private void TablePanel_Load(object sender, EventArgs e)
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

        private void tbtDel_Click(object sender, EventArgs e)
        {
            if (listTable.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选择删除项！","提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("是否确认删除？","确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            for (int i = listTable.SelectedItems.Count - 1; i >= 0;i-- )
            {
                ListViewItem item = listTable.SelectedItems[i];
                CTable tb = (CTable)item.Tag;
                if (tb.IsSystem)
                {
                    string sMsg = string.Format("系统表 {0} 不能删除！", tb.Name);
                    MessageBox.Show(sMsg, "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    continue;
                }
                if (Program.Ctx.TableMgr.Delete(tb,true))
                    CTable.DeleteDataTable(tb);
                listTable.Items.Remove(item);
            }
        }

        private void tbtNew_Click(object sender, EventArgs e)
        {
            TableInfoForm frm = new TableInfoForm();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                ListViewItem item = new ListViewItem();
                item.Text = frm.m_Table.Name;
                item.SubItems.Add(frm.m_Table.Code);
                item.SubItems.Add(frm.m_Table.IsSystem.ToString());
                item.SubItems.Add(frm.m_Table.IsFinish.ToString());
                item.SubItems.Add(frm.m_Table.Created.ToShortDateString());
                item.Tag = frm.m_Table;
                listTable.Items.Add(item);                
            }
        }

        private void tbtEdit_Click(object sender, EventArgs e)
        {
            if (listTable.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选择一项！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            CTable table = (CTable)listTable.SelectedItems[0].Tag;

            TableInfoForm frm = new TableInfoForm();
            frm.m_Table = table;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                ListViewItem item = listTable.SelectedItems[0];
                item.Text = frm.m_Table.Name;
                item.SubItems[1].Text= frm.m_Table.Code;
                item.SubItems[2].Text =frm.m_Table.IsSystem.ToString();
                item.SubItems[3].Text = frm.m_Table.IsFinish.ToString();
                item.SubItems[4].Text=frm.m_Table.Created.ToShortDateString();
            }
        }

        private void tbtData_Click(object sender, EventArgs e)
        {
            if (listTable.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选择一项！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            CTable table = (CTable)listTable.SelectedItems[0].Tag;

            CBaseObjectMgr BaseObjectMgr = new CBaseObjectMgr();
            BaseObjectMgr.TbCode = table.Code;
            BaseObjectMgr.Ctx = Program.Ctx;

            TableWindow frm = new TableWindow(BaseObjectMgr);
            frm.ShowDialog();
        }

        private void tbtMakeModel_Click(object sender, EventArgs e)
        {
            if (listTable.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选择一项！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            List<CTable> lstTable = new List<CTable>();
            foreach (ListViewItem item in listTable.SelectedItems)
            {
                lstTable.Add((CTable)item.Tag);
            }

            MakeModelForm frm = new MakeModelForm();
            frm.m_lstTable = lstTable;
            frm.ShowDialog();
        }

    }
}
