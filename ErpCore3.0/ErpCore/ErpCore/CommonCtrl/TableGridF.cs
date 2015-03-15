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
using ErpCore.Window;
using ErpCoreModel.UI;

namespace ErpCore.CommonCtrl
{
    public partial class TableGridF : UserControl, IWindowCtrl
    {
        string captionText = "";
        bool showToolBar = false;
        bool showTitleBar = false;

        CBaseObjectMgr baseObjectMgr = null;
        CTableInFormControl tableInFormControl = null;

        public TableGridF()
        {
            InitializeComponent();
        }

        public ControlType GetCtrlType()
        {
            return ControlType.TableGrid;
        }
        public CTableInFormControl TableInFormControl
        {
            get { return tableInFormControl; }
            set
            {
                tableInFormControl = value;
                LoadHeader();
                LoadData();
            }
        }
        public CBaseObjectMgr BaseObjectMgr
        {
            get { return baseObjectMgr; }
            set { 
                baseObjectMgr = value;
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
            if (TableInFormControl != null)
            {
                List<CBaseObject> lstCIWC = TableInFormControl.ColumnInTableInFormControlMgr.GetList();
                if (lstCIWC.Count > 0)
                {
                    bHas = true;
                    foreach (CBaseObject obj in lstCIWC)
                    {
                        CColumnInTableInFormControl ciwc = (CColumnInTableInFormControl)obj;
                        CColumn col = (CColumn)BaseObjectMgr.Table.ColumnMgr.Find(ciwc.FW_Column_id);
                        if (col == null) continue;
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

        }
        public void LoadData()
        {
            if (BaseObjectMgr == null)
                return;
            if (dataGridView.Columns.Count == 0)
                return;
            dataGridView.Rows.Clear();
            List<CBaseObject> lstObj = BaseObjectMgr.GetList();
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
                nRowIdx++;
            }
        }

        private void tbtNew_Click(object sender, EventArgs e)
        {
            RecordWindow frm = new RecordWindow(BaseObjectMgr, null);
            if (frm.ShowDialog() != DialogResult.OK)
                return;

            CBaseObject obj = frm.BaseObject;
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
        }

        private void tbtEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow == null)
            {
                MessageBox.Show("请选择一项！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            CBaseObject obj = (CBaseObject)dataGridView.CurrentRow.Tag;

            RecordWindow frm = new RecordWindow(BaseObjectMgr, obj);
            if (frm.ShowDialog() != DialogResult.OK)
                return;

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

        //界面公式取值
        public object GetSelectValue(string sColCode)
        {
            if (dataGridView.CurrentRow == null)
                return null;
            CColumn col = BaseObjectMgr.Table.ColumnMgr.FindByCode(sColCode);
            if (col == null)
                return null;
            CBaseObject obj = (CBaseObject)dataGridView.CurrentRow.Tag;
            return obj.GetColValue(col);
        }
    }
}
