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

namespace ErpCore.Window.Designer
{
    public partial class LayoutWindowDesigner : Form
    {
        CWindow window = null;

        public ControlToolWindow m_ControlToolWindow = new ControlToolWindow();
        public AttributeToolWindow m_AttributeToolWindow = new AttributeToolWindow();

        ChildenWindow m_child = null;

        public LayoutWindowDesigner()
        {
            InitializeComponent();
        }

        public CWindow Window
        {
            get { return window; }
            set { window = value; }
        }

        public ChildenWindow GetActiveChildenWindow()
        {
            return m_child;
        }

        private void LayoutWindowDesigner_Load(object sender, EventArgs e)
        {
            LayoutUI();
        }
        void LayoutUI()
        {
            m_ControlToolWindow.MdiParent = this;
            m_ControlToolWindow.Show();
            m_AttributeToolWindow.MdiParent = this;
            m_AttributeToolWindow.Show();
            m_ControlToolWindow.Left = this.Width - m_ControlToolWindow.Width - 10;
            m_ControlToolWindow.Top = 0;
            m_AttributeToolWindow.Left = this.Width - m_AttributeToolWindow.Width - 10;
            m_AttributeToolWindow.Top = m_ControlToolWindow.Bottom;

            tbtNew_Click(null, null);
        }

        private void LayoutWindowDesigner_SizeChanged(object sender, EventArgs e)
        {
            m_ControlToolWindow.Left = this.Width - m_ControlToolWindow.Width - 10;
            m_ControlToolWindow.Top = 0;
            m_AttributeToolWindow.Left = this.Width - m_AttributeToolWindow.Width-10;
            m_AttributeToolWindow.Top = m_ControlToolWindow.Bottom;
        }

        private void tbtNew_Click(object sender, EventArgs e)
        {
            if(m_child==null || m_child.IsDisposed)
                m_child = new ChildenWindow();
            m_child.Window = Window;
            m_child.m_AttributeToolWindow = m_AttributeToolWindow;
            m_child.m_ControlToolWindow = m_ControlToolWindow;
            m_child.MdiParent = this;
            m_child.Show();
            m_child.Left = 0;
            m_child.Top = 0;
            m_child.Focus();
        }

        private void tbtEdit_Click(object sender, EventArgs e)
        {
            m_child.OnEdit();
        }
        private void tbtSave_Click(object sender, EventArgs e)
        {
            if (m_child.Save())
            {                
                this.Close();
            }
        }

        private void tbtDel_Click(object sender, EventArgs e)
        {
            m_child.OnDelete();
        }

        private void MenuItemTopPanel_Click(object sender, EventArgs e)
        {
            if (m_child.panelTop.Controls.Count > 0 && !MenuItemTopPanel.Checked)
            {
                MenuItemTopPanel.Checked = true;
                MessageBox.Show("面板有控件，不能隐藏！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            m_child.ShowPanel(DockStyle.Top, MenuItemTopPanel.Checked);
        }

        private void MenuItemBottomPanel_Click(object sender, EventArgs e)
        {
            if (m_child.panelBottom.Controls.Count > 0 && !MenuItemBottomPanel.Checked)
            {
                MenuItemBottomPanel.Checked = true;
                MessageBox.Show("面板有控件，不能隐藏！","提示",  MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            m_child.ShowPanel(DockStyle.Bottom, MenuItemBottomPanel.Checked);

        }

        private void MenuItemLeftPanel_Click(object sender, EventArgs e)
        {
            if (m_child.panelLeft.Controls.Count > 0 && !MenuItemLeftPanel.Checked)
            {
                MenuItemLeftPanel.Checked = true;
                MessageBox.Show("面板有控件，不能隐藏！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            m_child.ShowPanel(DockStyle.Left, MenuItemLeftPanel.Checked);

        }

        private void MenuItemRightPanel_Click(object sender, EventArgs e)
        {
            if (m_child.panelRight.Controls.Count > 0 && !MenuItemRightPanel.Checked)
            {
                MenuItemRightPanel.Checked = true;
                MessageBox.Show("面板有控件，不能隐藏！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            m_child.ShowPanel(DockStyle.Right, MenuItemRightPanel.Checked);

        }

    }
}
