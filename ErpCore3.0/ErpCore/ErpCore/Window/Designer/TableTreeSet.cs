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

namespace ErpCore.Window.Designer
{
    public partial class TableTreeSet : Form
    {
        public CWindowControl m_WindowControl = null;

        public TableTreeNodeSet m_SelTableTreeNodeSet = null;

        public TableTreeSet()
        {
            InitializeComponent();
        }

        private void TableTreeSet_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        void LoadData()
        {
            if (m_WindowControl == null)
            {
                TableTreeNodeSet ctrl = new TableTreeNodeSet();
                ctrl.Idx = 0;
                ctrl.toolStrip.Click += new EventHandler(toolStrip_Click);
                flowLayoutPanel.Controls.Add(ctrl);

                CTableInWindowControl TableInWindowControl = new CTableInWindowControl();
                TableInWindowControl.Ctx = Program.Ctx;
                ctrl.Tag = TableInWindowControl;
            }
            else
            {
                txtName.Text = m_WindowControl.Name;
                List<CBaseObject> lstObj = m_WindowControl.TableInWindowControlMgr.GetList();
                foreach (CBaseObject obj in lstObj)
                {
                    CTableInWindowControl TableInWindowControl = (CTableInWindowControl)obj;

                    TableTreeNodeSet ctrl = new TableTreeNodeSet();
                    ctrl.Idx = flowLayoutPanel.Controls.Count;
                    ctrl.Tag = TableInWindowControl;
                    ctrl.toolStrip.Click += new EventHandler(toolStrip_Click);
                    flowLayoutPanel.Controls.Add(ctrl);
                }
            }
        }

        void toolStrip_Click(object sender, EventArgs e)
        {
            foreach (Control ctrl in flowLayoutPanel.Controls)
            {
                TableTreeNodeSet ttns = (TableTreeNodeSet)ctrl;
                ttns.Selected = false;
            }
            ToolStrip toolStrip = (ToolStrip)sender;
            TableTreeNodeSet NodeSet = (TableTreeNodeSet)toolStrip.Parent;
            NodeSet.Selected = true;

            m_SelTableTreeNodeSet = NodeSet;
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            foreach (Control ctrl in flowLayoutPanel.Controls)
            {
                TableTreeNodeSet ttns = (TableTreeNodeSet)ctrl;
                ttns.Selected = false;
            }
            TableTreeNodeSet NodeSet = new TableTreeNodeSet();
            NodeSet.Idx = flowLayoutPanel.Controls.Count;
            CTableInWindowControl TableInWindowControl = new CTableInWindowControl();
            TableInWindowControl.m_CmdType = CmdType.AddNew;
            TableInWindowControl.Ctx = Program.Ctx;
            NodeSet.Tag = TableInWindowControl;

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
                m_SelTableTreeNodeSet = (TableTreeNodeSet)flowLayoutPanel.Controls[0];
                m_SelTableTreeNodeSet.Selected = true;

                int idx = 0;
                foreach (Control ctrl in flowLayoutPanel.Controls)
                {
                    TableTreeNodeSet NodeSet = (TableTreeNodeSet)ctrl;
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
                TableTreeNodeSet NodeSet = (TableTreeNodeSet)flowLayoutPanel.Controls[i];
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

            if (m_WindowControl == null)
            {
                m_WindowControl = new CWindowControl();
                m_WindowControl.Ctx = Program.Ctx;
                m_WindowControl.m_CmdType = CmdType.AddNew;
                m_WindowControl.Name = txtName.Text.Trim();
                for (int i = 0; i < flowLayoutPanel.Controls.Count; i++)
                {
                    TableTreeNodeSet NodeSet = (TableTreeNodeSet)flowLayoutPanel.Controls[i];
                    CTableInWindowControl TableInWindowControl = (CTableInWindowControl)NodeSet.Tag;
                    TableInWindowControl.Idx = i;
                    TableInWindowControl.FW_Table_id = NodeSet.m_Table.Id;
                    TableInWindowControl.IsLoop = NodeSet.ckIsLoop.Checked;
                    TableInWindowControl.QueryFilter = NodeSet.txtQueryFilter.Text.Trim();
                    TableInWindowControl.RootFilter = NodeSet.txtRootFilter.Text.Trim();
                    TableInWindowControl.Text = NodeSet.txtText.Text.Trim();
                    if (NodeSet.ckIsLoop.Checked)
                    {
                        DataItem item = (DataItem)NodeSet.cbNodeIDCol.SelectedItem;
                        CColumn column = (CColumn)item.Data;
                        TableInWindowControl.NodeIDCol = column.Id;

                        DataItem item2 = (DataItem)NodeSet.cbPNodeIDCol.SelectedItem;
                        CColumn column2 = (CColumn)item2.Data;
                        TableInWindowControl.PNodeIDCol = column2.Id;
                    }
                    TableInWindowControl.UI_WindowControl_id = m_WindowControl.Id;
                    m_WindowControl.TableInWindowControlMgr.AddNew(TableInWindowControl);
                }
            }
            else
            {
                //删除
                List<CTableInWindowControl> lstDel = new List<CTableInWindowControl>();
                List<CBaseObject> lstObj = m_WindowControl.TableInWindowControlMgr.GetList();
                foreach (CBaseObject obj in lstObj)
                {
                    CTableInWindowControl TableInWindowControl = (CTableInWindowControl)obj;
                    bool bHas=false;
                    for (int i = 0; i < flowLayoutPanel.Controls.Count; i++)
                    {
                        TableTreeNodeSet NodeSet = (TableTreeNodeSet)flowLayoutPanel.Controls[i];
                        CTableInWindowControl tiwc = (CTableInWindowControl)NodeSet.Tag;
                        if (TableInWindowControl == tiwc)
                        {
                            bHas = true;
                            break;
                        }
                    }
                    if (!bHas)
                    {
                        lstDel.Add(TableInWindowControl);
                    }
                }
                foreach (CTableInWindowControl tiwc in lstDel)
                {
                    m_WindowControl.TableInWindowControlMgr.Delete(tiwc);
                }
                //添加、修改
                for (int i = 0; i < flowLayoutPanel.Controls.Count; i++)
                {
                    TableTreeNodeSet NodeSet = (TableTreeNodeSet)flowLayoutPanel.Controls[i];
                    CTableInWindowControl TableInWindowControl = (CTableInWindowControl)NodeSet.Tag;
                    TableInWindowControl.Idx = i;
                    TableInWindowControl.FW_Table_id = NodeSet.m_Table.Id;
                    TableInWindowControl.IsLoop = NodeSet.ckIsLoop.Checked;
                    TableInWindowControl.QueryFilter = NodeSet.txtQueryFilter.Text.Trim();
                    TableInWindowControl.RootFilter = NodeSet.txtRootFilter.Text.Trim();
                    TableInWindowControl.Text = NodeSet.txtText.Text.Trim();
                    if (NodeSet.ckIsLoop.Checked)
                    {
                        DataItem item = (DataItem)NodeSet.cbNodeIDCol.SelectedItem;
                        CColumn column = (CColumn)item.Data;
                        TableInWindowControl.NodeIDCol = column.Id;

                        DataItem item2 = (DataItem)NodeSet.cbPNodeIDCol.SelectedItem;
                        CColumn column2 = (CColumn)item2.Data;
                        TableInWindowControl.PNodeIDCol = column2.Id;
                    }
                    TableInWindowControl.UI_WindowControl_id = m_WindowControl.Id;
                    if (TableInWindowControl.m_CmdType == CmdType.AddNew)
                        m_WindowControl.TableInWindowControlMgr.AddNew(TableInWindowControl);
                    else
                        m_WindowControl.TableInWindowControlMgr.Update(TableInWindowControl);
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
