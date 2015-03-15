using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.OleDb;

using System.Text;
using System.Windows.Forms;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.UI;
using ErpCore.Window.Designer;

namespace ErpCore.Menu
{
    public partial class MenuPanel : UserControl
    {

        public MenuPanel()
        {
            InitializeComponent();

        }
        private void MenuPanel_Load(object sender, EventArgs e)
        {
            LoadTree();
            treeView.ExpandAll();
        }
        public void LoadTree()
        {
            if (treeView == null)
                return;
            treeView.Nodes.Clear();

            TreeNode node = new TreeNode("菜单");
            treeView.Nodes.Add(node);

            LoopLoadTree(node, null);
        }
        void LoopLoadTree(TreeNode pNode,CMenu pMenu)
        {
            List<CBaseObject> lstObj = Program.Ctx.MenuMgr.GetList();
            if (lstObj.Count == 0)
                return;

            foreach (CBaseObject obj in lstObj)
            {
                CMenu menu = (CMenu)obj;
                if (pMenu == null)
                {
                    if (menu.Parent_id != Guid.Empty)
                        continue;
                }
                else
                {
                    if (menu.Parent_id != pMenu.Id)
                        continue;
                }
                TreeNode node = new TreeNode(menu.Name);
                node.Tag = menu;
                if (pNode == null)
                    treeView.Nodes.Add(node);
                else
                    pNode.Nodes.Add(node);

                LoopLoadTree(node, menu);
            }
        }

        private void tbtNew_Click(object sender, EventArgs e)
        {
            TreeNode node = treeView.SelectedNode;
            if (node == null)
                node = treeView.Nodes[0];
            CMenu menu = (CMenu)node.Tag;
            if (menu!=null && menu.MType != enumMenuType.CatalogMenu)
            {
                MessageBox.Show("请选择分级菜单！");
                return;
            }
            MenuInfo frm = new MenuInfo();
            frm.m_PMenu = menu;
            if (frm.ShowDialog() != DialogResult.OK)
                return;
            TreeNode subnode = new TreeNode(frm.m_Menu.Name);
            subnode.Tag = frm.m_Menu;
            node.Nodes.Add(subnode);
            node.Expand();
        }

        private void tbtEdit_Click(object sender, EventArgs e)
        {
            if (treeView.SelectedNode == null
                || treeView.SelectedNode.Tag == null)
            {
                MessageBox.Show("请选择菜单！");
                return;
            }
            MenuInfo frm = new MenuInfo();
            frm.m_Menu = (CMenu)treeView.SelectedNode.Tag;
            if (frm.ShowDialog() != DialogResult.OK)
                return;
            treeView.SelectedNode.Text = frm.m_Menu.Name;
        }

        private void tbtDel_Click(object sender, EventArgs e)
        {
            if (treeView.SelectedNode == null
                || treeView.SelectedNode.Tag == null)
            {
                MessageBox.Show("请选择菜单！");
                return;
            }
            if (MessageBox.Show("是否确认删除？", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;

            CMenu menu = (CMenu)treeView.SelectedNode.Tag;

            Program.Ctx.MenuMgr.Delete(menu);
            if (!Program.Ctx.MenuMgr.Save(true))
            {
                MessageBox.Show("删除失败！");
                return;
            }
            treeView.SelectedNode.Parent.Nodes.Remove(treeView.SelectedNode);
        }

    }
}
