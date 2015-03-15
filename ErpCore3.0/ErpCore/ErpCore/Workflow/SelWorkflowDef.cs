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
using ErpCoreModel.Report;
using ErpCoreModel.SubSystem;
using ErpCoreModel.Workflow;

namespace ErpCore.Workflow
{
    public partial class SelWorkflowDef : Form
    {
        CCompany company = null;
        public CTable m_Table = null;
        public CWorkflowDef m_SelWorkflowDef = null;

        public SelWorkflowDef()
        {
            InitializeComponent();
        }

        public CCompany Company
        {
            get { return company; }
            set
            {
                company = value;
            }
        }
        private void SelWorkflowDef_Load(object sender, EventArgs e)
        {
            LoadHeader();
            LoadData();
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
                if (m_Table != null)
                {
                    if (WorkflowDef.FW_Table_id != m_Table.Id)
                        continue;
                }

                dataGridView.Rows.Add(1);
                int nRowIdx = dataGridView.Rows.Count - 1;
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

        private void btOk_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow == null)
            {
                MessageBox.Show("请选择工作流！");
                return;
            }
            m_SelWorkflowDef = (CWorkflowDef)dataGridView.CurrentRow.Tag;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
