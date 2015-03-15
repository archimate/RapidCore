using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace ErpCore.Database.Table
{
    public partial class AddPrefixForm : Form
    {
        public AddPrefixForm()
        {
            InitializeComponent();
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            if (txtPrefix.Text.Trim() == "")
            {
                MessageBox.Show("前缀不能空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
