using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace ErpCore.Window.Designer
{
    public partial class SelUIFormula : Form
    {
        public string m_sSelFormula = "";

        public SelUIFormula()
        {
            InitializeComponent();
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            if (listFormula.SelectedIndex < 0)
            {
                MessageBox.Show("请选择一项！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            m_sSelFormula = listFormula.SelectedItem.ToString();
            this.DialogResult = DialogResult.OK;

        }

        private void listFormula_DoubleClick(object sender, EventArgs e)
        {
            btOk_Click(null, null);
        }

        private void SelUIFormula_Load(object sender, EventArgs e)
        {

        }
    }
}
