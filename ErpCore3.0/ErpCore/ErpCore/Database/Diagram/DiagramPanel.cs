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

namespace ErpCore.Database.Diagram
{
    public partial class DiagramPanel : UserControl
    {
        public DiagramPanel()
        {
            InitializeComponent();
        }


        private void DiagramPanel_Load(object sender, EventArgs e)
        {
            LoadList();
        }
        void LoadList()
        {
            listDiagram.Items.Clear();
            List<CBaseObject> lstDiagram = Program.Ctx.DiagramMgr.GetList();
            foreach (CBaseObject obj in lstDiagram)
            {
                CDiagram Diagram = (CDiagram)obj;

                ListViewItem item= new ListViewItem();
                item.Text = Diagram.Name;
                item.ImageIndex = 0;
                item.Tag = Diagram;
                listDiagram.Items.Add(item);
            }
        }

        private void tbtNew_Click(object sender, EventArgs e)
        {
            DiagramInfoForm frm = new DiagramInfoForm();
            if (frm.ShowDialog() != DialogResult.OK)
                return;
            ListViewItem item = new ListViewItem();
            item.Text = frm.m_Diagram.Name;
            item.ImageIndex = 0;
            item.Tag = frm.m_Diagram;
            listDiagram.Items.Add(item);

            DesignerForm frm2 = new DesignerForm();
            frm2.m_Diagram = frm.m_Diagram;
            frm2.Show();
        }

        private void tbtEdit_Click(object sender, EventArgs e)
        {
            if (listDiagram.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选择一项！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            CDiagram Diagram = (CDiagram)listDiagram.SelectedItems[0].Tag;

            DiagramInfoForm frm = new DiagramInfoForm();
            frm.m_Diagram = Diagram;
            if (frm.ShowDialog() != DialogResult.OK)
                return;
            listDiagram.SelectedItems[0].Text = frm.m_Diagram.Name;
        }

        private void tbtDel_Click(object sender, EventArgs e)
        {
            if (listDiagram.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选择一项！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("是否确认删除？", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            CDiagram Diagram = (CDiagram)listDiagram.SelectedItems[0].Tag;

            if (!Program.Ctx.DiagramMgr.Delete(Diagram,true))
            {
                MessageBox.Show("删除失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            listDiagram.Items.Remove(listDiagram.SelectedItems[0]);
        }

        private void tbtOpen_Click(object sender, EventArgs e)
        {
            if (listDiagram.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选择一项！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            CDiagram Diagram = (CDiagram)listDiagram.SelectedItems[0].Tag;
            
            DesignerForm frm2 = new DesignerForm();
            frm2.m_Diagram = Diagram;
            frm2.Show();
        }

        private void listDiagram_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listDiagram.SelectedItems.Count == 0)
                return;
            tbtOpen_Click(null, null);
        }
    }
}
