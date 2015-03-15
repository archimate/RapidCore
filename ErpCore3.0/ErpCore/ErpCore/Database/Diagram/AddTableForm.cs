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

namespace ErpCore.Database.Diagram
{
    public partial class AddTableForm : Form
    {
        public DesignerForm m_DesignerForm = null;

        public AddTableForm()
        {
            InitializeComponent();
        }

        private void AddTableForm_Load(object sender, EventArgs e)
        {
            LoadList();
        }
        public void LoadList()
        {
            listTable.Items.Clear();
            List<CBaseObject> lstTable = Program.Ctx.TableMgr.GetList();
            foreach (CBaseObject obj in lstTable)
            {
                CTable tb = (CTable)obj;

                DataItem item = new DataItem();
                item.name = tb.Name;
                item.Data = tb;
                listTable.Items.Add(item);
            }
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            if(listTable.SelectedItems.Count==0)
            {
                MessageBox.Show("请选择一项！","提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            while (listTable.SelectedItems.Count > 0)
            {
                DataItem item = (DataItem)listTable.SelectedItems[0];
                CTable table = (CTable)item.Data;
                m_DesignerForm.AddTable(table);
                listTable.Items.Remove(listTable.SelectedItems[0]);
            }
        }
    }
}
