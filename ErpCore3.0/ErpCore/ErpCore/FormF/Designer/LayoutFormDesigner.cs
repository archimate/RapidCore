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

namespace ErpCore.FormF.Designer
{
    public partial class LayoutFormDesigner : Form
    {
        CForm form = null;

        public ControlToolWindow m_ControlToolWindow = new ControlToolWindow();
        public AttributeToolWindow m_AttributeToolWindow = new AttributeToolWindow();

        ChildenForm m_child = null;

        public LayoutFormDesigner()
        {
            InitializeComponent();
        }

        public CForm Form
        {
            get { return form; }
            set { form = value; }
        }

        public ChildenForm GetActiveChildenWindow()
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
                m_child = new ChildenForm();
            m_child.Form = Form;
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



    }
}
