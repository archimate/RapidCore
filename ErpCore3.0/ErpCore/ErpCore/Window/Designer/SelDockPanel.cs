using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace ErpCore.Window.Designer
{
    public partial class SelDockPanel : Form
    {
        public DockStyle m_dock = DockStyle.Fill;

        public SelDockPanel()
        {
            InitializeComponent();
        }

        private void btTop_Click(object sender, EventArgs e)
        {
            m_dock = DockStyle.Top;
            DialogResult = DialogResult.OK;
        }

        private void btLeft_Click(object sender, EventArgs e)
        {
            m_dock = DockStyle.Left;
            DialogResult = DialogResult.OK;
        }

        private void btFill_Click(object sender, EventArgs e)
        {
            m_dock = DockStyle.Fill;
            DialogResult = DialogResult.OK;
        }

        private void btRight_Click(object sender, EventArgs e)
        {
            m_dock = DockStyle.Right;
            DialogResult = DialogResult.OK;
        }

        private void btBottom_Click(object sender, EventArgs e)
        {
            m_dock = DockStyle.Bottom;
            DialogResult = DialogResult.OK;
        }
    }
}
