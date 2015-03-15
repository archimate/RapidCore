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
    public partial class SelDiagramForm : Form
    {
        public CDiagram m_SelDiagram = null;

        public SelDiagramForm()
        {
            InitializeComponent();
        }

        private void SelDiagramForm_Load(object sender, EventArgs e)
        {
            LoadHead();
            LoadList();

        }
        void LoadHead()
        {
            listDiagram.Columns.Clear();
            listDiagram.Columns.Add("名称", 200);
        }
        public void LoadList()
        {
            listDiagram.Items.Clear();
            List<CBaseObject> lstDiagram = Program.Ctx.DiagramMgr.GetList();
            foreach (CBaseObject obj in lstDiagram)
            {
                CDiagram diagram = (CDiagram)obj;

                ListViewItem item = new ListViewItem();
                item.Text = diagram.Name;
                item.Tag = diagram;
                listDiagram.Items.Add(item);
            }
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            if (listDiagram.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选择一项！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            m_SelDiagram = (CDiagram)listDiagram.SelectedItems[0].Tag;

            DialogResult = DialogResult.OK;

        }
    }
}
