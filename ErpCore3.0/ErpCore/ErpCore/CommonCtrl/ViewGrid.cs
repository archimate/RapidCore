using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;

using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.Workflow;
using ErpCoreModel.UI;
using ErpCore.Window;
using ErpCore.Workflow;
using ErpCore.View;

namespace ErpCore.CommonCtrl
{
    public partial class ViewGrid : UserControl
    {
        string captionText = "";
        bool showToolBar = false;
        bool showTitleBar = false;

        CBaseObjectMgr baseObjectMgr = null;
        CView view = null;
        AccessType m_ViewAccessType = AccessType.forbide;
        AccessType m_TableAccessType = AccessType.forbide;
        //受限的字段：禁止或者只读权限
        SortedList<Guid, AccessType> m_sortRestrictColumnAccessType = new SortedList<Guid, AccessType>();

        public bool m_bShowWorkflow = true;//是否显示工作流

        CViewFilterMgr m_TempViewFilterMgr =new CViewFilterMgr();//临时过滤条件

        public ViewGrid()
        {
            InitializeComponent();
        }

        public CView View
        {
            get { return view; }
            set
            {
                view = value;
                //判断视图权限
                if(view!=null)
                    m_ViewAccessType = Program.User.GetViewAccess(view.Id);
                
                LoadHeader();
                LoadData();
            }
        }
        public CBaseObjectMgr BaseObjectMgr
        {
            get { return baseObjectMgr; }
            set { 
                baseObjectMgr = value;
                //判断表权限
                if (baseObjectMgr != null)
                {
                    m_TableAccessType = Program.User.GetTableAccess(baseObjectMgr.Table.Id);
                    m_sortRestrictColumnAccessType = Program.User.GetRestrictColumnAccessTypeList(baseObjectMgr.Table);
                }

                LoadHeader();
                LoadData();
            }
        }

        public string CaptionText
        {
            get { return captionText; }
            set
            {
                captionText = value;
                lbTitle.Text = captionText;
            }
        }
        public bool ShowToolBar
        {
            get { return showToolBar; }
            set
            {
                showToolBar = value;
                toolStrip.Visible = showToolBar;
            }
        }
        public bool ShowTitleBar
        {
            get { return showTitleBar; }
            set
            {
                showTitleBar = value;
                tbTitle.Visible = showTitleBar;
                if (showTitleBar)
                {
                    string sName = "";
                    if (BaseObjectMgr != null)
                    {
                        CTable table = Program.Ctx.TableMgr.FindByCode(BaseObjectMgr.TbCode);
                        if (table != null)
                            sName = table.Name;
                    }
                    lbTitle.Text = sName;
                }
            }
        }

        private void TableCtrl_Load(object sender, EventArgs e)
        {
            if (!m_bShowWorkflow)
                tbtWorkflow.Visible = false;

            LoadHeader();
            LoadData();
        }
        void LoadHeader()
        {
            if (BaseObjectMgr == null)
                return;
            if (dataGridView == null)
                return;
            dataGridView.Columns.Clear();

            bool bHas = false;
            if (View != null)
            {
                List<CBaseObject> lstCIV = View.ColumnInViewMgr.GetList();
                if (lstCIV.Count > 0)
                {
                    bHas = true;
                    foreach (CBaseObject obj in lstCIV)
                    {
                        CColumnInView civ = (CColumnInView)obj;
                        CColumn col = (CColumn)BaseObjectMgr.Table.ColumnMgr.Find(civ.FW_Column_id);
                        if (col == null) 
                            continue;
                        DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                        column.Name = col.Code;
                        column.HeaderText = col.Name;
                        column.Tag = col;
                        dataGridView.Columns.Add(column);
                    }
                }
            }
            if(!bHas)
            {
                List<CBaseObject> lstCol = baseObjectMgr.Table.ColumnMgr.GetList();
                foreach (CColumn col in lstCol)
                {
                    //if (!col.IsVisible)
                    //    continue;
                    DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                    column.Name = col.Code;
                    column.HeaderText = col.Name;
                    column.Tag = col;
                    dataGridView.Columns.Add(column);
                }
            }
            //工作流
            if (m_bShowWorkflow)
            {
                DataGridViewTextBoxColumn columnWF = new DataGridViewTextBoxColumn();
                columnWF.Name = "workflow";
                columnWF.HeaderText = "工作流";
                dataGridView.Columns.Add(columnWF);
            }
        }
        public void LoadData()
        {
            if (BaseObjectMgr == null)
                return;
            if (dataGridView.Columns.Count == 0)
                return;
            dataGridView.Rows.Clear();
            
            //检查权限
            if (!CheckAccess())
                return;

            m_TempViewFilterMgr.IsLoad = true;//避免临时数据从数据库装载
            List<CBaseObject> lstObj = BaseObjectMgr.FilterByView(View, m_TempViewFilterMgr);
            if (lstObj.Count == 0)
                return;
            dataGridView.Rows.Add(lstObj.Count);
            int nRowIdx = 0;
            foreach (CBaseObject obj in lstObj)
            {
                DataGridViewRow row= dataGridView.Rows[nRowIdx];
                row.Tag = obj;
                foreach (DataGridViewColumn column in dataGridView.Columns)
                {
                    //工作流
                    if (column.Name.Equals("workflow"))
                        continue;
                    //

                    CColumn col = (CColumn)column.Tag;
                    //判断禁止权限字段
                    if (m_sortRestrictColumnAccessType.ContainsKey(col.Id))
                    {
                        AccessType accessType = m_sortRestrictColumnAccessType[col.Id];
                        if (accessType == AccessType.forbide)
                            continue;
                    }
                    //
                    if (col.ColType == ColumnType.object_type)
                    {
                        if (obj.GetColValue(col) != null)
                            row.Cells[column.Name].Value = "long byte";
                    }
                    else if (col.ColType == ColumnType.ref_type)
                    {
                        CTable table = (CTable)Program.Ctx.TableMgr.Find(col.RefTable);
                        if (table == null) continue;
                        CColumn RefCol = (CColumn)table.ColumnMgr.Find(col.RefCol);
                        CColumn RefShowCol = (CColumn)table.ColumnMgr.Find(col.RefShowCol);
                        object objVal = obj.GetColValue(col);


                        Guid guidParentId = Guid.Empty;
                        if (BaseObjectMgr.m_Parent != null && BaseObjectMgr.m_Parent.Id == (Guid)objVal)
                        {
                            row.Cells[column.Name].Value = BaseObjectMgr.m_Parent.GetColValue(RefShowCol);
                        }
                        else
                        {
                            CBaseObjectMgr objMgr = Program.Ctx.FindBaseObjectMgrCache(table.Code, Guid.Empty);
                            if (objMgr != null)
                            {
                                CBaseObject objCache = objMgr.FindByValue(RefCol, objVal);
                                if (objCache != null)
                                    row.Cells[column.Name].Value = objCache.GetColValue(RefShowCol);
                            }
                            else
                            {
                                objMgr = new CBaseObjectMgr();
                                objMgr.TbCode = table.Code;
                                objMgr.Ctx = Program.Ctx;

                                string sWhere = string.Format(" {0}=?", RefCol.Code);
                                List<DbParameter> cmdParas = new List<DbParameter>();
                                cmdParas.Add(new DbParameter(RefCol.Code, obj.GetColValue(col)));
                                List<CBaseObject> lstObj2 = objMgr.GetList(sWhere, cmdParas);
                                if (lstObj2.Count > 0)
                                {
                                    CBaseObject obj2 = lstObj2[0];
                                    row.Cells[column.Name].Value = obj2.GetColValue(RefShowCol);
                                }
                            }
                        }
                    }
                    else
                        row.Cells[column.Name].Value = obj.GetColValue(col);
                    
                }
                UpdateRowWorkflow(row);

                nRowIdx++;
            }
        }
        //检查权限
        bool CheckAccess()
        {
            //判断视图权限
            if (m_ViewAccessType == AccessType.forbide)
            {
                lbNoAccess.Text = "没有视图权限！";
                lbNoAccess.Visible = true;
                tbtNew.Enabled = false;
                tbtEdit.Enabled = false;
                tbtDel.Enabled = false;
                return false;
            }
            //判断表权限
            if (m_TableAccessType == AccessType.forbide)
            {
                lbNoAccess.Text = "没有表权限！";
                lbNoAccess.Visible = true;
                tbtNew.Enabled = false;
                tbtEdit.Enabled = false;
                tbtDel.Enabled = false;
                return false;
            }
            else if (m_TableAccessType == AccessType.read)
            {
                tbtNew.Enabled = false;
                tbtEdit.Enabled = false;
                tbtDel.Enabled = false;
            }
            else
            {
                tbtNew.Enabled = true;
                tbtEdit.Enabled = true;
                tbtDel.Enabled = true;
            }
            lbNoAccess.Visible = false;

            return true;
        }
        //更新工作流状态
        void UpdateRowWorkflow(DataGridViewRow row)
        {
            if (!m_bShowWorkflow)
                return;
            CBaseObject obj = (CBaseObject)row.Tag;
            List<CWorkflow> lstWF = BaseObjectMgr.WorkflowMgr.FindLastByRowid(obj.Id);
            string sText = "";
            foreach (CWorkflow wf in lstWF)
            {
                if (wf.State == enumApprovalState.Running)
                    sText += "进行中;";
                else if (wf.State == enumApprovalState.Accept)
                    sText += "接受;";
                else if (wf.State == enumApprovalState.Reject)
                    sText += "拒绝;";
            }
            row.Cells["workflow"].Value = sText;
        }

        private void tbtNew_Click(object sender, EventArgs e)
        {
            CBaseObject obj = null;
            if (View.VType == enumViewType.Single)
            {
                SingleViewRecord frm = new SingleViewRecord(BaseObjectMgr, null);
                frm.View = View;
                if (frm.ShowDialog() != DialogResult.OK)
                    return;
                obj = frm.BaseObject;
            }
            else if (View.VType == enumViewType.MasterDetail)
            {
                MasterDetailViewRecord frm = new MasterDetailViewRecord(BaseObjectMgr, null);
                frm.View = View;
                if (frm.ShowDialog() != DialogResult.OK)
                    return;
                obj = frm.BaseObject;
            }
            else
            {
                MultMasterDetailViewRecord frm = new MultMasterDetailViewRecord(BaseObjectMgr, null);
                frm.View = View;
                if (frm.ShowDialog() != DialogResult.OK)
                    return;
                obj = frm.BaseObject;
            }

            dataGridView.Rows.Add(1);
            DataGridViewRow row = dataGridView.Rows[dataGridView.Rows.Count - 1];
            row.Tag = obj;
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                //工作流
                if (column.Name.Equals("workflow"))
                    continue;
                //
                CColumn col = (CColumn)column.Tag;
                if (col.ColType == ColumnType.object_type)
                {
                    if (obj.GetColValue(col) != null)
                        row.Cells[column.Name].Value = "long byte";
                }
                else if (col.ColType == ColumnType.ref_type)
                {
                    CTable table = (CTable)Program.Ctx.TableMgr.Find(col.RefTable);
                    if (table == null) continue;
                    CColumn RefCol = (CColumn)table.ColumnMgr.Find(col.RefCol);
                    CColumn RefShowCol = (CColumn)table.ColumnMgr.Find(col.RefShowCol);
                    object objVal = obj.GetColValue(col);


                    Guid guidParentId = Guid.Empty;
                    if (BaseObjectMgr.m_Parent != null && BaseObjectMgr.m_Parent.Id == (Guid)objVal)
                    {
                        row.Cells[column.Name].Value = BaseObjectMgr.m_Parent.GetColValue(RefShowCol);
                    }
                    else
                    {
                        CBaseObjectMgr objMgr = Program.Ctx.FindBaseObjectMgrCache(table.Code, Guid.Empty);
                        if (objMgr != null)
                        {
                            CBaseObject objCache = objMgr.FindByValue(RefCol, objVal);
                            if (objCache != null)
                                row.Cells[column.Name].Value = objCache.GetColValue(RefShowCol);
                        }
                        else
                        {
                            objMgr = new CBaseObjectMgr();
                            objMgr.TbCode = table.Code;
                            objMgr.Ctx = Program.Ctx;

                            string sWhere = string.Format(" {0}=?", RefCol.Code);
                            List<DbParameter> cmdParas = new List<DbParameter>();
                            cmdParas.Add(new DbParameter(RefCol.Code, obj.GetColValue(col)));
                            List<CBaseObject> lstObj2 = objMgr.GetList(sWhere, cmdParas);
                            if (lstObj2.Count > 0)
                            {
                                CBaseObject obj2 = lstObj2[0];
                                row.Cells[column.Name].Value = obj2.GetColValue(RefShowCol);
                            }
                        }
                    }
                }
                else
                    row.Cells[column.Name].Value = obj.GetColValue(col);
            }
        }

        private void tbtEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow == null)
            {
                MessageBox.Show("请选择一项！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            CBaseObject obj = (CBaseObject)dataGridView.CurrentRow.Tag;

            if (View.VType == enumViewType.Single)
            {
                SingleViewRecord frm = new SingleViewRecord(BaseObjectMgr, obj);
                frm.View = View;
                if (frm.ShowDialog() != DialogResult.OK)
                    return;
                obj = frm.BaseObject;
            }
            else if (View.VType == enumViewType.MasterDetail)
            {
                MasterDetailViewRecord frm = new MasterDetailViewRecord(BaseObjectMgr, obj);
                frm.View = View;
                if (frm.ShowDialog() != DialogResult.OK)
                    return;
                obj = frm.BaseObject;
            }
            else
            {
                MultMasterDetailViewRecord frm = new MultMasterDetailViewRecord(BaseObjectMgr, obj);
                frm.View = View;
                if (frm.ShowDialog() != DialogResult.OK)
                    return;
                obj = frm.BaseObject;
            }

            DataGridViewRow row = dataGridView.CurrentRow;
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                //工作流
                if (column.Name.Equals("workflow"))
                    continue;
                //
                CColumn col = (CColumn)column.Tag;
                if (col.ColType == ColumnType.object_type)
                {
                    if (obj.GetColValue(col) != null)
                        row.Cells[column.Name].Value = "long byte";
                }
                else if (col.ColType == ColumnType.ref_type)
                {
                    CTable table = (CTable)Program.Ctx.TableMgr.Find(col.RefTable);
                    if (table == null) continue;
                    CColumn RefCol = (CColumn)table.ColumnMgr.Find(col.RefCol);
                    CColumn RefShowCol = (CColumn)table.ColumnMgr.Find(col.RefShowCol);
                    object objVal = obj.GetColValue(col);


                    Guid guidParentId = Guid.Empty;
                    if (BaseObjectMgr.m_Parent != null && BaseObjectMgr.m_Parent.Id == (Guid)objVal)
                    {
                        row.Cells[column.Name].Value = BaseObjectMgr.m_Parent.GetColValue(RefShowCol);
                    }
                    else
                    {
                        CBaseObjectMgr objMgr = Program.Ctx.FindBaseObjectMgrCache(table.Code, Guid.Empty);
                        if (objMgr != null)
                        {
                            CBaseObject objCache = objMgr.FindByValue(RefCol, objVal);
                            if (objCache != null)
                                row.Cells[column.Name].Value = objCache.GetColValue(RefShowCol);
                        }
                        else
                        {
                            objMgr = new CBaseObjectMgr();
                            objMgr.TbCode = table.Code;
                            objMgr.Ctx = Program.Ctx;

                            string sWhere = string.Format(" {0}=?", RefCol.Code);
                            List<DbParameter> cmdParas = new List<DbParameter>();
                            cmdParas.Add(new DbParameter(RefCol.Code, obj.GetColValue(col)));
                            List<CBaseObject> lstObj2 = objMgr.GetList(sWhere, cmdParas);
                            if (lstObj2.Count > 0)
                            {
                                CBaseObject obj2 = lstObj2[0];
                                row.Cells[column.Name].Value = obj2.GetColValue(RefShowCol);
                            }
                        }
                    }
                }
                else
                    row.Cells[column.Name].Value = obj.GetColValue(col);
            }
        }

        private void tbtDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow == null)
            {
                MessageBox.Show("请选择一项！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("是否确认删除？", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            CBaseObject obj = (CBaseObject)dataGridView.CurrentRow.Tag;
            if (!BaseObjectMgr.Delete(obj,true))
            {
                MessageBox.Show("删除失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return ;
            }
            dataGridView.Rows.Remove(dataGridView.CurrentRow);
        }

        public void SetColumnVisible(CColumn col, bool bVisible)
        {
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                if (column.Name.Equals(col.Code, StringComparison.OrdinalIgnoreCase))
                {
                    column.Visible = bVisible;
                }
            }
        }
        public void SetToolBarVisible(bool bVisible)
        {
            toolStrip.Visible = bVisible;
        }
        public void SetToolBarButtonVisible(string sTitle, bool bVisible)
        {
            foreach (ToolStripItem tbutton in toolStrip.Items)
            {
                if (tbutton.Text.Equals(sTitle, StringComparison.OrdinalIgnoreCase))
                {
                    tbutton.Visible = bVisible;
                    break;
                }
            }
        }


        private void tbtWorkflow_ButtonClick(object sender, EventArgs e)
        {
            MenuItem_ViewWorkflow_Click(null, null);
        }

        private void MenuItem_StartWorkflow_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow == null)
            {
                MessageBox.Show("请选择一项！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            CBaseObject obj = (CBaseObject)dataGridView.CurrentRow.Tag;

            SelWorkflowDef frm = new SelWorkflowDef();
            frm.m_Table = BaseObjectMgr.Table;
            if (frm.ShowDialog() != DialogResult.OK)
                return;
            //只能存在一个运行的工作流实例
            List<CWorkflow> lstWF = BaseObjectMgr.WorkflowMgr.FindLastByRowid(obj.Id);
            foreach (CWorkflow wf in lstWF)
            {
                if (wf.WF_WorkflowDef_id == frm.m_SelWorkflowDef.Id
                    && wf.State == enumApprovalState.Running)
                {
                    MessageBox.Show("该工作流已经启动！");
                    return;
                }
            }
            //创建工作流实例并运行
            CWorkflow Workflow = new CWorkflow();
            Workflow.Ctx = Program.Ctx;
            Workflow.WF_WorkflowDef_id = frm.m_SelWorkflowDef.Id;
            Workflow.State = enumApprovalState.Init;
            Workflow.Row_id = obj.Id;
            Workflow.Creator = Program.User.Id;
            string sErr = "";
            if (!BaseObjectMgr.WorkflowMgr.StartWorkflow(Workflow, out sErr))
            {
                MessageBox.Show(string.Format("启动工作流失败：{0}",sErr));
                return;
            }
            BaseObjectMgr.WorkflowMgr.AddNew(Workflow);
            if (!BaseObjectMgr.WorkflowMgr.Save(true))
            {
                MessageBox.Show("创建工作流失败！");
                return;
            }

            UpdateRowWorkflow(dataGridView.CurrentRow);
            MessageBox.Show(string.Format("启动成功！"));
        }

        private void MenuItem_ViewWorkflow_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow == null)
            {
                MessageBox.Show("请选择一项！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            CBaseObject obj = (CBaseObject)dataGridView.CurrentRow.Tag;

            ViewWorkflow frm = new ViewWorkflow();
            frm.m_BaseObjectMgr = BaseObjectMgr;
            frm.m_BaseObject = obj;
            frm.ShowDialog();

            UpdateRowWorkflow(dataGridView.CurrentRow);
        }

        private void tbtFilter_Click(object sender, EventArgs e)
        {
            SetViewFilter frm = new SetViewFilter();
            frm.m_View = View;
            if (frm.ShowDialog() != DialogResult.OK)
                return;
            m_TempViewFilterMgr = frm.m_ViewFilterMgr;
            LoadData();
        }
    }
}
