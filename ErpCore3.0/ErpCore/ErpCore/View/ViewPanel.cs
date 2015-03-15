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
using ErpCore.Window.Designer;

namespace ErpCore.View
{
    public partial class ViewPanel : UserControl
    {
        CViewCatalog catalog = null;

        public ViewPanel()
        {
            InitializeComponent();

        }
        public CViewCatalog Catalog
        {
            get { return catalog; }
            set { 
                catalog = value;

                LoadData();
            }
        }
        private void ViewPanel_Load(object sender, EventArgs e)
        {
            LoadHeader();
            LoadData();

        }
        void LoadHeader()
        {
            if (dataGridView == null)
                return;
            dataGridView.Columns.Clear();

            List<CBaseObject> lstCol = Program.Ctx.ViewMgr.Table.ColumnMgr.GetList();
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
            CBaseObjectMgr BaseObjectMgr = Program.Ctx.ViewMgr;
            List<CBaseObject> lstObj = BaseObjectMgr.GetList();
            if (lstObj.Count == 0)
                return;

            foreach (CBaseObject obj in lstObj)
            {
                CView view = (CView)obj;
                if (catalog == null)
                {
                    if (view.UI_ViewCatalog_id != Guid.Empty)
                        continue;
                }
                else
                {
                    if (view.UI_ViewCatalog_id != catalog.Id)
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
            SelViewType frmVT = new SelViewType();
            if (frmVT.ShowDialog() != DialogResult.OK)
                return;
            CView obj = null;
            if (frmVT.m_enumViewType == enumViewType.Single)
            {
                SingleViewInfo frm = new SingleViewInfo();
                if (frm.ShowDialog() != DialogResult.OK)
                    return;
                obj =  frm.m_View;
            }
            else if (frmVT.m_enumViewType == enumViewType.MasterDetail)
            {
                MasterDetailViewInfo frm = new MasterDetailViewInfo();
                if (frm.ShowDialog() != DialogResult.OK)
                    return;
                obj = frm.m_View;
            }
            else
            {
                MultMasterDetailViewInfo frm = new MultMasterDetailViewInfo();
                if (frm.ShowDialog() != DialogResult.OK)
                    return;
                obj = frm.m_View;
            }

            CBaseObjectMgr BaseObjectMgr = Program.Ctx.ViewMgr;

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
            CView obj = (CView)dataGridView.CurrentRow.Tag;

            if (obj.VType == enumViewType.Single)
            {
                SingleViewInfo frm = new SingleViewInfo();
                frm.m_View = obj;
                frm.m_Catalog_id = obj.UI_ViewCatalog_id;
                if (frm.ShowDialog() != DialogResult.OK)
                    return;
            }
            else if (obj.VType == enumViewType.MasterDetail)
            {
                MasterDetailViewInfo frm = new MasterDetailViewInfo();
                frm.m_View = obj;
                frm.m_Catalog_id = obj.UI_ViewCatalog_id;
                if (frm.ShowDialog() != DialogResult.OK)
                    return;
            }
            else
            {
                MultMasterDetailViewInfo frm = new MultMasterDetailViewInfo();
                frm.m_View = obj;
                frm.m_Catalog_id = obj.UI_ViewCatalog_id;
                if (frm.ShowDialog() != DialogResult.OK)
                    return;
            }

            CBaseObjectMgr BaseObjectMgr = Program.Ctx.ViewMgr;

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
                CView obj = (CView)dataGridView.SelectedRows[i].Tag;
                if (!Program.Ctx.ViewMgr.Delete(obj, true))
                {
                    MessageBox.Show("删除失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
                dataGridView.Rows.Remove(dataGridView.SelectedRows[i]);
            }
        }

        private void tbtButton_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow == null)
            {
                MessageBox.Show("请选择一项！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            CView obj = (CView)dataGridView.CurrentRow.Tag;
            SetTButton frm = new SetTButton();
            frm.m_View = obj;
            frm.ShowDialog();
        }

        private void tbtSetDefaultVal_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow == null)
            {
                MessageBox.Show("请选择一项！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            CView obj = (CView)dataGridView.CurrentRow.Tag;
            SetDefaultVal frm = new SetDefaultVal();
            frm.m_View = obj;
            frm.ShowDialog();
        }
    }
}
