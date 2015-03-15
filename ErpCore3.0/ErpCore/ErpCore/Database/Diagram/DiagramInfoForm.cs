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
    public partial class DiagramInfoForm : Form
    {
        public CDiagram m_Diagram = null;

        public DiagramInfoForm()
        {
            InitializeComponent();
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            string sName = txtName.Text.Trim();
            if (sName == "")
            {
                MessageBox.Show("名称不能空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (m_Diagram == null)
            {
                if (Program.Ctx.DiagramMgr.FindByName(sName) != null)
                {
                    MessageBox.Show("关系图已经存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                m_Diagram = new CDiagram();
                m_Diagram.Name = sName;
                m_Diagram.DType = DiagramType.Normal;
                m_Diagram.Ctx = Program.Ctx;
                if (!Program.Ctx.DiagramMgr.AddNew(m_Diagram))
                {
                    MessageBox.Show("新建失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
            }
            else
            {
                if (Program.Ctx.DiagramMgr.FindByName(sName) != m_Diagram)
                {
                    MessageBox.Show("关系图已经存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                m_Diagram.Name = sName;
                if (!Program.Ctx.DiagramMgr.Update(m_Diagram))
                {
                    MessageBox.Show("修改失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
            }

            DialogResult = DialogResult.OK;
        }

        private void DiagramInfoForm_Load(object sender, EventArgs e)
        {
            if (m_Diagram != null)
            {
                txtName.Text = m_Diagram.Name;
            }
        }
    }
}
