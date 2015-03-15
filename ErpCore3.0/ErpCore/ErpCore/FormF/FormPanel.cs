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
using ErpCore.Window;
using ErpCore.FormF.Designer;

namespace ErpCore.FormF
{
    public partial class FormPanel : UserControl
    {
        CFormCatalog catalog = null;

        public FormPanel()
        {
            InitializeComponent();

        }
        public CFormCatalog Catalog
        {
            get { return catalog; }
            set { 
                catalog = value;
                LoadData();
            }
        }
        private void FormPanel_Load(object sender, EventArgs e)
        {
            LoadHeader();
            LoadData();

        }
        void LoadHeader()
        {
            if (dataGridView == null)
                return;
            dataGridView.Columns.Clear();

            List<CBaseObject> lstCol =Program.Ctx.FormMgr.Table.ColumnMgr.GetList();
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
            if (dataGridView == null)
                return;
            if (dataGridView.Columns.Count == 0)
                return;
            dataGridView.Rows.Clear();
            CBaseObjectMgr BaseObjectMgr = Program.Ctx.FormMgr;
            List<CBaseObject> lstObj = BaseObjectMgr.GetList();
            if (lstObj.Count == 0)
                return;
            foreach (CBaseObject obj in lstObj)
            {
                CForm form = (CForm)obj;
                if (catalog == null)
                {
                    if (form.UI_FormCatalog_id != Guid.Empty)
                        continue;
                }
                else
                {
                    if (form.UI_FormCatalog_id != catalog.Id)
                        continue;
                }

                dataGridView.Rows.Add(1);
                DataGridViewRow row = dataGridView.Rows[dataGridView.Rows.Count-1];
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
            RecordWindow frm = new RecordWindow(Program.Ctx.FormMgr, null);
            //frm.recordCtrl.m_sortNotShowCol.Add("fw_table_id", Program.Ctx.FormMgr.Table.Id);
            if (frm.ShowDialog() != DialogResult.OK)
                return;

            CBaseObjectMgr BaseObjectMgr = Program.Ctx.FormMgr;
            CForm obj = (CForm)frm.BaseObject;
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
            DesignForm(obj);
        }

        private void tbtEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow == null)
            {
                MessageBox.Show("请选择一项！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            CForm obj = (CForm)dataGridView.CurrentRow.Tag;

            RecordWindow frm = new RecordWindow(Program.Ctx.FormMgr, obj);
            //frm.recordCtrl.m_sortNotShowCol.Add("fw_table_id", Program.Ctx.FormMgr.Table.Id);
            if (frm.ShowDialog() != DialogResult.OK)
                return;

            CBaseObjectMgr BaseObjectMgr = Program.Ctx.FormMgr;
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
            CForm obj = (CForm)dataGridView.CurrentRow.Tag;
            if (!Program.Ctx.FormMgr.Delete(obj, true))
            {
                MessageBox.Show("删除失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            dataGridView.Rows.Remove(dataGridView.CurrentRow);
        }

        private void tbtDesign_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow == null)
            {
                MessageBox.Show("请选择一项！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            CForm obj = (CForm)dataGridView.CurrentRow.Tag;

            DesignForm(obj);
        }
        void DesignForm(CForm form)
        {
            LayoutFormDesigner frm = new LayoutFormDesigner();
            frm.Form = form;
            frm.Show();
        }
    }
}
