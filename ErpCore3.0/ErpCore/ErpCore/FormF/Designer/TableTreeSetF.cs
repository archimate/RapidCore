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
using ErpCore.Window;
using ErpCore.Database.Table;

namespace ErpCore.FormF.Designer
{
    public partial class TableTreeSetF : Form
    {
        public CFormControl m_FormControl = null;

        public TableTreeNodeSetF m_SelTableTreeNodeSet = null;

        public TableTreeSetF()
        {
            InitializeComponent();
        }

        private void TableTreeSet_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        void LoadData()
        {
            if (m_FormControl == null)
            {
                TableTreeNodeSetF ctrl = new TableTreeNodeSetF();
                ctrl.Idx = 0;
                ctrl.toolStrip.Click += new EventHandler(toolStrip_Click);
                flowLayoutPanel.Controls.Add(ctrl);

                CTableInFormControl TableInFormControl = new CTableInFormControl();
                TableInFormControl.Ctx = Program.Ctx;
                ctrl.Tag = TableInFormControl;
            }
            else
            {
                txtName.Text = m_FormControl.Name;
                List<CBaseObject> lstObj = m_FormControl.TableInFormControlMgr.GetList();
                foreach (CBaseObject obj in lstObj)
                {
                    CTableInFormControl TableInFormControl = (CTableInFormControl)obj;

                    TableTreeNodeSetF ctrl = new TableTreeNodeSetF();
                    ctrl.Idx = flowLayoutPanel.Controls.Count;
                    ctrl.Tag = TableInFormControl;
                    ctrl.toolStrip.Click += new EventHandler(toolStrip_Click);
                    flowLayoutPanel.Controls.Add(ctrl);
                }
            }
        }

        void toolStrip_Click(object sender, EventArgs e)
        {
            foreach (Control ctrl in flowLayoutPanel.Controls)
            {
                TableTreeNodeSetF ttns = (TableTreeNodeSetF)ctrl;
                ttns.Selected = false;
            }
            ToolStrip toolStrip = (ToolStrip)sender;
            TableTreeNodeSetF NodeSet = (TableTreeNodeSetF)toolStrip.Parent;
            NodeSet.Selected = true;

            m_SelTableTreeNodeSet = NodeSet;
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            foreach (Control ctrl in flowLayoutPanel.Controls)
            {
                TableTreeNodeSetF ttns = (TableTreeNodeSetF)ctrl;
                ttns.Selected = false;
            }
            TableTreeNodeSetF NodeSet = new TableTreeNodeSetF();
            NodeSet.Idx = flowLayoutPanel.Controls.Count;
            CTableInFormControl TableInFormControl = new CTableInFormControl();
            TableInFormControl.m_CmdType = CmdType.AddNew;
            TableInFormControl.Ctx = Program.Ctx;
            NodeSet.Tag = TableInFormControl;

            NodeSet.toolStrip.Click += new EventHandler(toolStrip_Click);
            flowLayoutPanel.Controls.Add(NodeSet);
            NodeSet.Selected = true;

            m_SelTableTreeNodeSet = NodeSet;
        }

        private void btDel_Click(object sender, EventArgs e)
        {
            if (m_SelTableTreeNodeSet == null)
            {
                MessageBox.Show("请选择一项！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            flowLayoutPanel.Controls.Remove(m_SelTableTreeNodeSet);

            if (flowLayoutPanel.Controls.Count == 0)
                m_SelTableTreeNodeSet = null;
            else
            {
                m_SelTableTreeNodeSet = (TableTreeNodeSetF)flowLayoutPanel.Controls[0];
                m_SelTableTreeNodeSet.Selected = true;

                int idx = 0;
                foreach (Control ctrl in flowLayoutPanel.Controls)
                {
                    TableTreeNodeSetF NodeSet = (TableTreeNodeSetF)ctrl;
                    NodeSet.Idx = idx;
                    idx++;
                }
            }
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            if (flowLayoutPanel.Controls.Count == 0)
            {
                MessageBox.Show("请添加表！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            for (int i = 0; i < flowLayoutPanel.Controls.Count; i++)
            {
                TableTreeNodeSetF NodeSet = (TableTreeNodeSetF)flowLayoutPanel.Controls[i];
                if (NodeSet.m_Table == null)
                {
                    MessageBox.Show("请设置表！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (NodeSet.txtText.Text.Trim()=="")
                {
                    MessageBox.Show("请设置显示文本！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (NodeSet.ckIsLoop.Checked)
                {
                    if (NodeSet.cbNodeIDCol.SelectedItem == null)
                    {
                        MessageBox.Show("请设置本节点字段！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (NodeSet.cbPNodeIDCol.SelectedItem == null)
                    {
                        MessageBox.Show("请设置父节点字段！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }

            if (m_FormControl == null)
            {
                m_FormControl = new CFormControl();
                m_FormControl.Ctx = Program.Ctx;
                m_FormControl.m_CmdType = CmdType.AddNew;
                m_FormControl.Name = txtName.Text.Trim();
                for (int i = 0; i < flowLayoutPanel.Controls.Count; i++)
                {
                    TableTreeNodeSetF NodeSet = (TableTreeNodeSetF)flowLayoutPanel.Controls[i];
                    CTableInFormControl TableInFormControl = (CTableInFormControl)NodeSet.Tag;
                    TableInFormControl.Idx = i;
                    TableInFormControl.FW_Table_id = NodeSet.m_Table.Id;
                    TableInFormControl.IsLoop = NodeSet.ckIsLoop.Checked;
                    TableInFormControl.QueryFilter = NodeSet.txtQueryFilter.Text.Trim();
                    TableInFormControl.RootFilter = NodeSet.txtRootFilter.Text.Trim();
                    TableInFormControl.Text = NodeSet.txtText.Text.Trim();
                    if (NodeSet.ckIsLoop.Checked)
                    {
                        DataItem item = (DataItem)NodeSet.cbNodeIDCol.SelectedItem;
                        CColumn column = (CColumn)item.Data;
                        TableInFormControl.NodeIDCol = column.Id;

                        DataItem item2 = (DataItem)NodeSet.cbPNodeIDCol.SelectedItem;
                        CColumn column2 = (CColumn)item2.Data;
                        TableInFormControl.PNodeIDCol = column2.Id;
                    }
                    TableInFormControl.UI_FormControl_id = m_FormControl.Id;
                    m_FormControl.TableInFormControlMgr.AddNew(TableInFormControl);
                }
            }
            else
            {
                //删除
                List<CTableInFormControl> lstDel = new List<CTableInFormControl>();
                List<CBaseObject> lstObj = m_FormControl.TableInFormControlMgr.GetList();
                foreach (CBaseObject obj in lstObj)
                {
                    CTableInFormControl TableInFormControl = (CTableInFormControl)obj;
                    bool bHas=false;
                    for (int i = 0; i < flowLayoutPanel.Controls.Count; i++)
                    {
                        TableTreeNodeSetF NodeSet = (TableTreeNodeSetF)flowLayoutPanel.Controls[i];
                        CTableInFormControl tiwc = (CTableInFormControl)NodeSet.Tag;
                        if (TableInFormControl == tiwc)
                        {
                            bHas = true;
                            break;
                        }
                    }
                    if (!bHas)
                    {
                        lstDel.Add(TableInFormControl);
                    }
                }
                foreach (CTableInFormControl tiwc in lstDel)
                {
                    m_FormControl.TableInFormControlMgr.Delete(tiwc);
                }
                //添加、修改
                for (int i = 0; i < flowLayoutPanel.Controls.Count; i++)
                {
                    TableTreeNodeSetF NodeSet = (TableTreeNodeSetF)flowLayoutPanel.Controls[i];
                    CTableInFormControl TableInFormControl = (CTableInFormControl)NodeSet.Tag;
                    TableInFormControl.Idx = i;
                    TableInFormControl.FW_Table_id = NodeSet.m_Table.Id;
                    TableInFormControl.IsLoop = NodeSet.ckIsLoop.Checked;
                    TableInFormControl.QueryFilter = NodeSet.txtQueryFilter.Text.Trim();
                    TableInFormControl.RootFilter = NodeSet.txtRootFilter.Text.Trim();
                    TableInFormControl.Text = NodeSet.txtText.Text.Trim();
                    if (NodeSet.ckIsLoop.Checked)
                    {
                        DataItem item = (DataItem)NodeSet.cbNodeIDCol.SelectedItem;
                        CColumn column = (CColumn)item.Data;
                        TableInFormControl.NodeIDCol = column.Id;

                        DataItem item2 = (DataItem)NodeSet.cbPNodeIDCol.SelectedItem;
                        CColumn column2 = (CColumn)item2.Data;
                        TableInFormControl.PNodeIDCol = column2.Id;
                    }
                    TableInFormControl.UI_FormControl_id = m_FormControl.Id;
                    if (TableInFormControl.m_CmdType == CmdType.AddNew)
                        m_FormControl.TableInFormControlMgr.AddNew(TableInFormControl);
                    else
                        m_FormControl.TableInFormControlMgr.Update(TableInFormControl);
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
