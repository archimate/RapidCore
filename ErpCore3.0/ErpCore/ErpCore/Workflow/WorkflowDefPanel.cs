using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.OleDb;

using System.Text;
using System.Windows.Forms;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.UI;
using ErpCoreModel.Report;
using ErpCoreModel.SubSystem;
using ErpCoreModel.Workflow;

namespace ErpCore.Workflow
{
    public partial class WorkflowDefPanel : UserControl
    {
        CCompany company = null;
        CWorkflowCatalog catalog = null;

        public WorkflowDefPanel()
        {
            InitializeComponent();

        }
        public CCompany Company
        {
            get { return company; }
            set
            {
                company = value;

                LoadHeader();
                LoadData();
            }
        }
        public CWorkflowCatalog Catalog
        {
            get { return catalog; }
            set { 
                catalog = value;

                LoadData();
            }
        }
        private void ReportPanel_Load(object sender, EventArgs e)
        {

        }
        void LoadHeader()
        {
            if (Company == null)
                return;
            if (dataGridView == null)
                return;
            dataGridView.Columns.Clear();

            List<CBaseObject> lstCol = Company.WorkflowDefMgr.Table.ColumnMgr.GetList();
            foreach (CBaseObject obj in lstCol)
            {
                CColumn col = (CColumn)obj;
                //if (!col.IsVisible)
                //    continue;
                DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                column.Name = col.Code;
                column.HeaderText = col.Name;
                column.Tag = col;
                dataGridView.Columns.Add(column);
            }
        }
        public void LoadData()
        {
            if (Company == null)
                return;
            if (dataGridView == null)
                return;
            if (dataGridView.Columns.Count == 0)
                return;
            dataGridView.Rows.Clear();
            CBaseObjectMgr BaseObjectMgr = Company.WorkflowDefMgr;
            List<CBaseObject> lstObj = BaseObjectMgr.GetList();
                        
            foreach (CBaseObject obj in lstObj)
            {
                CWorkflowDef WorkflowDef = (CWorkflowDef)obj;
                Guid WF_WorkflowCatalog_id = (catalog == null) ? Guid.Empty : catalog.Id;
                if (WorkflowDef.WF_WorkflowCatalog_id != WF_WorkflowCatalog_id)
                    continue;

                dataGridView.Rows.Add(1);
                int nRowIdx = dataGridView.Rows.Count-1;
                DataGridViewRow row = dataGridView.Rows[nRowIdx];
                row.Tag = obj;
                foreach (DataGridViewColumn column in dataGridView.Columns)
                {
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
        }

        private void tbtNew_Click(object sender, EventArgs e)
        {
            WorkflowDefInfo frm = new WorkflowDefInfo();
            Guid Catalog_id = (catalog == null) ? Guid.Empty : catalog.Id;
            frm.m_Catalog_id = Catalog_id;
            frm.m_Company = Company;
            if (frm.ShowDialog() != DialogResult.OK)
                return;

            CBaseObjectMgr BaseObjectMgr = Company.WorkflowDefMgr;

            CWorkflowDef obj = (CWorkflowDef)frm.m_WorkflowDef;
            dataGridView.Rows.Add(1);
            DataGridViewRow row = dataGridView.Rows[dataGridView.Rows.Count - 1];
            row.Tag = obj;
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
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
            dataGridView.ClearSelection();
            row.Selected = true;
            row.Cells[1].Selected = true;
        }

        private void tbtEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow == null)
            {
                MessageBox.Show("请选择一项！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            CWorkflowDef obj = (CWorkflowDef)dataGridView.CurrentRow.Tag;

            WorkflowDefInfo frm = new WorkflowDefInfo();
            Guid Catalog_id = (catalog == null) ? Guid.Empty : catalog.Id;
            frm.m_Catalog_id = Catalog_id;
            frm.m_Company = Company;
            frm.m_WorkflowDef = obj;
            if (frm.ShowDialog() != DialogResult.OK)
                return;

            CBaseObjectMgr BaseObjectMgr = Company.WorkflowDefMgr;

            DataGridViewRow row = dataGridView.CurrentRow;
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
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
            for (int i = dataGridView.SelectedRows.Count - 1; i >= 0; i--)
            {
                CWorkflowDef obj = (CWorkflowDef)dataGridView.SelectedRows[i].Tag;
                obj.m_ObjectMgr.Delete(obj);

                if (!obj.m_ObjectMgr.Save(true))
                {
                    MessageBox.Show("删除失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
                dataGridView.Rows.Remove(dataGridView.SelectedRows[i]);
            }
        }

    }
}
