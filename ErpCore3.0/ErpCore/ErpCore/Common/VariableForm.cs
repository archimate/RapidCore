using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ErpCore
{
    public partial class VariableForm : Form
    {
        public VariableForm()
        {
            InitializeComponent();
        }

        private void VariableForm_Load(object sender, EventArgs e)
        {
            LoadGridView();
        }
        void LoadGridView()
        {
            dataGridView.Rows.Clear();
            CVariable Variable = new CVariable();
            foreach (KeyValuePair<string, string> kv in  CVariable.g_VarName)
            {
                dataGridView.Rows.Add(1);
                DataGridViewRow row = dataGridView.Rows[dataGridView.Rows.Count - 1];
                row.Cells[0].Value = kv.Key;
                row.Cells[1].Value = kv.Value;
            }
        }
    }
}
