using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCore.Window;

namespace ErpCore.Database.Table
{
    public partial class TableInfoForm : Form
    {
        public CTable m_Table = null;

        SortedList m_arrColProp = new SortedList();

        public TableInfoForm()
        {
            InitializeComponent();
        }

        private void tbtSave_Click(object sender, EventArgs e)
        {
            string sName = txtTableName.Text.Trim();
            string sCode = txtTableCode.Text.Trim() ;
            if ( sName== "")
            {
                MessageBox.Show("名称不能空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTableName.Focus();
                return;
            }
            if (sCode== "")
            {
                MessageBox.Show("编码不能空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTableCode.Focus();
                return;
            }
            if (!ValidateColName(sCode))
            {
                MessageBox.Show("编码只能由字母、数字、下划线组成，且数字不能在前面！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                txtTableCode.Focus();
                txtTableCode.SelectAll();
                return;
            }
            if (m_Table.m_CmdType== CmdType.AddNew)
            {
                if (Program.Ctx.TableMgr.FindByCode(sCode) != null)
                {
                    MessageBox.Show("相同编码的表已经存在！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    txtTableCode.Focus();
                    txtTableCode.SelectAll();
                    return;
                }
            }
            else if(!sCode.Equals(m_Table.Code, StringComparison.OrdinalIgnoreCase))
            {
                CTable table = Program.Ctx.TableMgr.FindByCode(sCode);
                if (table != null && table != m_Table)
                {
                    MessageBox.Show("相同编码的表已经存在！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    txtTableCode.Focus();
                    txtTableCode.SelectAll();
                    return;
                }
            }
            //检查列的合法性
            SortedList arrColName = new SortedList();
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.Cells[0].Value == null)
                    continue;
                string sColName = row.Cells[0].Value.ToString().Trim();
                if (sColName == "")
                {
                    MessageBox.Show(string.Format("列名不能空！"), "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
                if (!ValidateColName(sColName))
                {
                    MessageBox.Show(string.Format("列名只能由字母、数字、下划线组成，且数字不能在前面：{0}",sColName), "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
                if (arrColName.Contains(sColName))
                {
                    MessageBox.Show(string.Format("列名重复：{0}", sColName), "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
                arrColName.Add(sColName, sColName);

                //
                CColumn col = (CColumn)row.Tag;
                DataGridViewComboBoxCell cbCell = (DataGridViewComboBoxCell)row.Cells[1];
                ColumnType colType = CColumn.ConvertStringToColType(cbCell.Value.ToString());
                if ( colType== ColumnType.ref_type)
                {
                    ColPropertySetting prop = (ColPropertySetting)col.m_objTempData;
                    if (prop != null)
                    {
                        if (string.IsNullOrEmpty(prop.引用表))
                        {
                            MessageBox.Show(string.Format("{0}列引用表不能空！", sColName), "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            return;
                        }
                        if (string.IsNullOrEmpty(prop.引用字段))
                        {
                            MessageBox.Show(string.Format("{0}列引用字段不能空！", sColName), "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            return;
                        }
                        if (string.IsNullOrEmpty(prop.引用显示字段))
                        {
                            MessageBox.Show(string.Format("{0}列引用显示字段不能空！", sColName), "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            return;
                        }
                    }
                }
                else if(colType== ColumnType.enum_type)
                {
                    ColPropertySetting prop = (ColPropertySetting)col.m_objTempData;
                    if (prop != null)
                    {
                        if (string.IsNullOrEmpty(prop.引用显示字段)
                            && string.IsNullOrEmpty(prop.枚举值))
                        {
                            MessageBox.Show(string.Format("{0}列至少有引用显示字段或枚举值！", sColName), "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            return;
                        }
                        if (!string.IsNullOrEmpty(prop.引用显示字段))
                        {
                            if (string.IsNullOrEmpty(prop.引用表))
                            {
                                MessageBox.Show(string.Format("{0}列引用表不能空！", sColName), "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                return;
                            }
                        }
                    }
                }
            }


            m_Table.Name = sName;
            m_Table.Code = sCode;
            m_Table.IsSystem = ckIsSystem.Checked;
            m_Table.IsFinish = true;
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.Cells[0].Value == null)
                    continue;
                string sColName = row.Cells[0].Value.ToString().Trim();
                DataGridViewComboBoxCell cbCell = (DataGridViewComboBoxCell)row.Cells[1];
                string sColLen = row.Cells[2].Value.ToString().Trim();
                DataGridViewCheckBoxCell ckCell = (DataGridViewCheckBoxCell)row.Cells[3];
                DataGridViewCheckBoxCell ckCell2 = (DataGridViewCheckBoxCell)row.Cells[4];
                DataGridViewCheckBoxCell ckCell3 = (DataGridViewCheckBoxCell)row.Cells[5];

                CColumn col = (CColumn)row.Tag;
                col.FW_Table_id = m_Table.Id;
                col.Code = sColName;
                col.ColType = CColumn.ConvertStringToColType(cbCell.Value.ToString());
                col.ColLen = Convert.ToInt32(sColLen);
                col.AllowNull = Convert.ToBoolean(ckCell.Value);
                col.IsSystem = Convert.ToBoolean(ckCell2.Value);
                col.IsUnique = Convert.ToBoolean(ckCell3.Value);
                col.Idx = row.Index;
                ColPropertySetting prop = (ColPropertySetting)col.m_objTempData;
                if (prop != null)
                {
                    col.Name = prop.名称;
                    col.DefaultValue = prop.默认值;
                    col.ColDecimal = prop.小数位数;
                    col.Formula = prop.公式;
                    if (col.ColType == ColumnType.ref_type)
                    {
                        CTable table = Program.Ctx.TableMgr.FindByCode(prop.引用表);
                        col.RefTable = table.Id;
                        CColumn column = table.ColumnMgr.FindByCode(prop.引用字段);
                        col.RefCol = column.Id;
                        column = table.ColumnMgr.FindByCode(prop.引用显示字段);
                        col.RefShowCol = column.Id;
                    }
                    else if(col.ColType== ColumnType.enum_type)
                    {
                        //引用显示字段优先
                        if (!string.IsNullOrEmpty(prop.引用显示字段))
                        {
                            CTable table = Program.Ctx.TableMgr.FindByCode(prop.引用表);
                            col.RefTable = table.Id;
                            CColumn column = table.ColumnMgr.FindByCode(prop.引用显示字段);
                            col.RefShowCol = column.Id;
                        }
                        else
                        {
                            col.ColumnEnumValMgr.RemoveAll();
                            string[] arrItem = prop.枚举值.Split("\\".ToCharArray());
                            for (int i = 0; i < arrItem.Length; i++)
                            {
                                string sVal = arrItem[i];
                                CColumnEnumVal cev = new CColumnEnumVal();
                                cev.Ctx = Program.Ctx;
                                cev.FW_Column_id = col.Id;
                                cev.Val = sVal;
                                cev.Idx = i;

                                col.ColumnEnumValMgr.AddNew(cev);
                            }
                        }
                    }
                    col.UIControl = prop.界面控件;
                    col.WebUIControl = prop.web界面控件;
                }
                m_Table.ColumnMgr.Update(col);
            }
            
            if (!CTable.CreateDataTable(m_Table))
            {
                MessageBox.Show(Program.Ctx.LastError, "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            //if (!m_Table.Save(true))
            if (!Program.Ctx.TableMgr.Save(true))
            {
                MessageBox.Show(Program.Ctx.LastError, "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            //if (m_Table.m_CmdType== CmdType.AddNew) //新建
            //{
            //    //m_Table = new CTable();
            //    m_Table.Name = sName;
            //    m_Table.Code = sCode;
            //    m_Table.IsSystem = ckIsSystem.Checked;
            //    if (Program.User != null)
            //    {
            //        m_Table.Creator = Program.User.Id;
            //        m_Table.Updator = Program.User.Id;
            //    }
            //    m_Table.Ctx = Program.Ctx;
            //    m_Table.IsFinish = true;
            //    if (!Program.Ctx.TableMgr.AddNew(m_Table))
            //    {
            //        MessageBox.Show(Program.Ctx.LastError, "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            //        return;
            //    }
            //    foreach (DataGridViewRow row in dataGridView.Rows)
            //    {
            //        if (row.Cells[0].Value == null)
            //            continue;
            //        string sColName = row.Cells[0].Value.ToString().Trim();
            //        DataGridViewComboBoxCell cbCell = (DataGridViewComboBoxCell)row.Cells[1];
            //        string sColLen = row.Cells[2].Value.ToString().Trim();
            //        DataGridViewCheckBoxCell ckCell = (DataGridViewCheckBoxCell)row.Cells[3];
            //        DataGridViewCheckBoxCell ckCell2 = (DataGridViewCheckBoxCell)row.Cells[4];

            //        CColumn col = (CColumn)row.Tag;
            //        col.FW_Table_id = m_Table.Id;
            //        col.Code = sColName;
            //        col.ColType = CColumn.ConvertStringToColType(cbCell.Value.ToString());
            //        col.ColLen = Convert.ToInt32(sColLen);
            //        col.AllowNull = Convert.ToBoolean(ckCell.Value);
            //        col.IsSystem = Convert.ToBoolean(ckCell2.Value);
            //        col.Idx = row.Index;
            //        ColPropertySetting prop = (ColPropertySetting)col.m_objTempData;
            //        if (prop != null)
            //        {
            //            col.Name = prop.名称;
            //            col.DefaultValue = prop.默认值;
            //            col.ColDecimal = prop.小数位数;
            //            col.Formula = prop.公式;
            //            if (col.ColType == ColumnType.ref_type)
            //            {
            //                CTable table = Program.Ctx.TableMgr.FindByCode(prop.引用表);
            //                col.RefTable = table.Id;
            //                CColumn column = table.ColumnMgr.FindByCode(prop.引用字段);
            //                col.RefCol = column.Id;
            //                column = table.ColumnMgr.FindByCode(prop.引用显示字段);
            //                col.RefShowCol = column.Id;
            //            }
            //            col.UIControl = prop.界面控件;
            //            col.WebUIControl = prop.web界面控件;
            //        }
            //        col.Ctx = m_Table.Ctx;

            //        if (!m_Table.ColumnMgr.AddNew(col))
            //        {
            //            MessageBox.Show(Program.Ctx.LastError, "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            //            return;
            //        }
            //    }

            //    if (!m_Table.Save(true))
            //    {
            //        MessageBox.Show(Program.Ctx.LastError, "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            //        return;
            //    }
            //}
            //else //保存
            //{
            //    m_Table.Name = sName;
            //    m_Table.Code = sCode;
            //    m_Table.IsFinish = true;
            //    m_Table.IsSystem = ckIsSystem.Checked;
            //    if (!Program.Ctx.TableMgr.Update(m_Table))
            //    {
            //        MessageBox.Show(Program.Ctx.LastError, "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            //        return;
            //    }
            //    List<CBaseObject> lstCol = m_Table.ColumnMgr.GetList();
            //    List<CColumn> lstDel = new List<CColumn>();
            //    foreach (CBaseObject obj in lstCol)
            //    {
            //        CColumn col = (CColumn)obj;
            //        bool bHas = false;
            //        foreach (DataGridViewRow row in dataGridView.Rows)
            //        {
            //            if (row.Cells[0].Value == null)
            //                continue;
            //            CColumn col2 = (CColumn)row.Tag;
            //            if (col == col2)
            //            {
            //                bHas = true;
            //                break;
            //            }
            //        }
            //        if (!bHas)
            //            lstDel.Add(col);
            //    }
            //    foreach (CColumn col in lstDel)
            //    {
            //        m_Table.ColumnMgr.Delete(col);
            //    }

            //    foreach (DataGridViewRow row in dataGridView.Rows)
            //    {
            //        if (row.Cells[0].Value == null)
            //            continue;
            //        string sColName = row.Cells[0].Value.ToString().Trim();
            //        DataGridViewComboBoxCell cbCell = (DataGridViewComboBoxCell)row.Cells[1];
            //        string sColLen = row.Cells[2].Value.ToString().Trim();
            //        DataGridViewCheckBoxCell ckCell = (DataGridViewCheckBoxCell)row.Cells[3];
            //        DataGridViewCheckBoxCell ckCell2 = (DataGridViewCheckBoxCell)row.Cells[4];

            //        CColumn col = (CColumn)row.Tag;
            //        col.FW_Table_id = m_Table.Id;
            //        col.Code = sColName;
            //        col.ColType = CColumn.ConvertStringToColType(cbCell.Value.ToString());
            //        col.ColLen = Convert.ToInt32(sColLen);
            //        col.AllowNull = Convert.ToBoolean(ckCell.Value);
            //        col.IsSystem = Convert.ToBoolean(ckCell2.Value);
            //        col.Idx = row.Index;
            //        ColPropertySetting prop = (ColPropertySetting)col.m_objTempData;
            //        if (prop != null)
            //        {
            //            col.Name = prop.名称;
            //            col.DefaultValue = prop.默认值;
            //            col.ColDecimal = prop.小数位数;
            //            col.Formula = prop.公式;
            //            if (col.ColType == ColumnType.ref_type)
            //            {
            //                CTable table = Program.Ctx.TableMgr.FindByCode(prop.引用表);
            //                col.RefTable = table.Id;
            //                CColumn column = table.ColumnMgr.FindByCode(prop.引用字段);
            //                col.RefCol = column.Id;
            //                column = table.ColumnMgr.FindByCode(prop.引用显示字段);
            //                col.RefShowCol = column.Id;
            //            }
            //            col.UIControl = prop.界面控件;
            //            col.WebUIControl = prop.web界面控件;
            //        }
            //        col.Ctx = m_Table.Ctx;

            //        if (col.m_CmdType== CmdType.AddNew)  
            //        {
            //            if (!m_Table.ColumnMgr.AddNew(col))
            //            {
            //                MessageBox.Show(Program.Ctx.LastError, "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            //                return;
            //            }
            //        }
            //        else if (col.m_CmdType == CmdType.Update)  
            //        {
            //            if (!m_Table.ColumnMgr.Update(col))
            //            {
            //                MessageBox.Show(Program.Ctx.LastError, "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            //                return;
            //            }
            //        }
            //    }
            //    if (!m_Table.Save(true))
            //    {
            //        MessageBox.Show(Program.Ctx.LastError, "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            //        return;
            //    }
            //    //排列索引变化，重新加载
            //    m_Table.ColumnMgr.Load(m_Table.ColumnMgr.m_sWhere, true);
            //}
            //if (!CTable.CreateDataTable(m_Table))
            //{
            //    MessageBox.Show(Program.Ctx.LastError, "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            //    return;
            //}


            DialogResult = DialogResult.OK;
        }

        private void tbtCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView.Rows[e.RowIndex];
            CColumn col = (CColumn)row.Tag;
            if (col != null)
            {
                //if (col.m_CmdType != CmdType.AddNew)
                //    col.m_CmdType = CmdType.Update;
                m_Table.ColumnMgr.Update(col);
            }

            if (e.ColumnIndex == 0)
            {
                if (row.Cells[e.ColumnIndex].Value == null)
                    return;
                string sColName = row.Cells[e.ColumnIndex].Value.ToString();
                if (!ValidateColName(sColName))
                {
                    MessageBox.Show("列名只能由字母、数字、下划线组成，且数字不能在前面！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
                DataGridViewComboBoxCell cbCell = (DataGridViewComboBoxCell)row.Cells[1];
                if (cbCell.Value == null)
                {
                    cbCell.Value = "字符型";
                    row.Cells[2].Value = "50";
                    DataGridViewCheckBoxCell ckCell = (DataGridViewCheckBoxCell)row.Cells[3];
                    ckCell.Value = true;
                    DataGridViewCheckBoxCell ckCell2 = (DataGridViewCheckBoxCell)row.Cells[4];
                    ckCell2.Value = false;
                }

                if (col == null)
                {
                    col = new CColumn();
                    col.Ctx = Program.Ctx;
                    col.Name = sColName;
                    col.AllowNull = true;
                    col.IsSystem = false;
                    col.IsUnique = false;
                    col.IsVisible = true;
                    //col.m_CmdType = CmdType.AddNew;
                    m_Table.ColumnMgr.AddNew(col);
                    row.Tag = col;

                    ColPropertySetting prop = (ColPropertySetting)col.m_objTempData;
                    if (prop == null)
                    {
                        prop = new ColPropertySetting();
                        prop.名称 = col.Name;
                        prop.默认值 = col.DefaultValue;
                        prop.公式 = col.Formula;
                        prop.小数位数 = col.ColDecimal;
                        List<CBaseObject> lstObj= col.ColumnEnumValMgr.GetList();
                        string sEnumVal = "";
                        foreach (CBaseObject obj in lstObj)
                        {
                            CColumnEnumVal cev = (CColumnEnumVal)obj;
                            sEnumVal += cev.Val + "\\";
                        }
                        sEnumVal = sEnumVal.TrimEnd("\\".ToCharArray());
                        prop.枚举值 = sEnumVal;

                        col.m_objTempData = prop;
                    }
                    propertyGrid.SelectedObject = prop;
                    propertyGrid.Refresh();
                    UpdateRefTable(prop.引用表);

                    if (col.IsSystem)
                        propertyGrid.Enabled = false;
                    else
                        propertyGrid.Enabled = true;
                }
            }
        }

        //判断列名合法性：列名只能由字母、数字、下划线组成，且数字不能在前面
        bool ValidateColName(string sColName)
        {
            if (sColName.Length == 0)
                return false;
            sColName = sColName.ToUpper();
            if (sColName[0] < 'A' || sColName[0] > 'Z')
                return false;
            for (int i = 1; i < sColName.Length; i++)
            {
                bool bFlag = false;
                if (sColName[i] >= 'A' && sColName[i] <= 'Z')
                    bFlag = true;
                else if (sColName[i] >= '0' && sColName[i] <= '9')
                    bFlag = true;
                else if (sColName[i] =='_')
                    bFlag = true;

                if (!bFlag)
                    return false;
            }

            return true;
        }


        private void dataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            ComboBox comb = e.Control as ComboBox;
            if (comb != null)
            {
                comb.SelectedIndexChanged += new EventHandler(comb_SelectedIndexChanged);
            }


        }

        void comb_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comb = sender as ComboBox; 
            if (dataGridView.SelectedCells.Count == 0)
                return;
            int nColIdx = dataGridView.SelectedCells[0].ColumnIndex;
            int nRowIdx = dataGridView.SelectedCells[0].RowIndex;
            if (nColIdx == 1)
            {
                string sColType = comb.SelectedItem.ToString();
                if (sColType == "字符型")
                {
                    dataGridView.Rows[nRowIdx].Cells[2].Value = "50";
                }
                else if (sColType == "整型")
                {
                    dataGridView.Rows[nRowIdx].Cells[2].Value = "4";
                }
                else if (sColType == "长整型")
                {
                    dataGridView.Rows[nRowIdx].Cells[2].Value = "8";
                }
                else if (sColType == "布尔型")
                {
                    dataGridView.Rows[nRowIdx].Cells[2].Value = "1";
                }
                else if (sColType == "数值型")
                {
                    dataGridView.Rows[nRowIdx].Cells[2].Value = "18";
                }
                else if (sColType == "日期型")
                {
                    dataGridView.Rows[nRowIdx].Cells[2].Value = "8";
                }
                else if (sColType == "备注型")
                {
                    dataGridView.Rows[nRowIdx].Cells[2].Value = "16";
                }
                else if (sColType == "二进制")
                {
                    dataGridView.Rows[nRowIdx].Cells[2].Value = "16";
                }
                else if (sColType == "引用型")
                {
                    dataGridView.Rows[nRowIdx].Cells[2].Value = "16";
                }
                else if (sColType == "GUID")
                {
                    dataGridView.Rows[nRowIdx].Cells[2].Value = "16";
                }
                else if (sColType == "枚举型")
                {
                    dataGridView.Rows[nRowIdx].Cells[2].Value = "50";
                }
                else if (sColType == "附件型")
                {
                    dataGridView.Rows[nRowIdx].Cells[2].Value = "250";
                }
            }
        }

        private void TableInfoForm_Load(object sender, EventArgs e)
        {
            LoadList();
        }

        void LoadList()
        {
            dataGridView.Rows.Clear();

            if (m_Table == null)  //新建
            {
                m_Table = new CTable();
                m_Table.Ctx = Program.Ctx; 
                if (Program.User != null)
                {
                    m_Table.Creator = Program.User.Id;
                    m_Table.Updator = Program.User.Id;
                }
                //m_Table.m_CmdType = CmdType.AddNew;
                Program.Ctx.TableMgr.AddNew(m_Table);
                //系统字段
                dataGridView.Rows.Add(5);
                DataGridViewRow row = dataGridView.Rows[0];
                row.Cells[0].Value = "id";
                DataGridViewComboBoxCell cbCell = (DataGridViewComboBoxCell)row.Cells[1];
                cbCell.Value = "GUID";
                row.Cells[2].Value = "16";
                DataGridViewCheckBoxCell ckCell = (DataGridViewCheckBoxCell)row.Cells[3];
                ckCell.Value = false;
                DataGridViewCheckBoxCell ckCell2 = (DataGridViewCheckBoxCell)row.Cells[4];
                ckCell2.Value = true;
                DataGridViewCheckBoxCell ckCell3 = (DataGridViewCheckBoxCell)row.Cells[5];
                ckCell3.Value = true;
                row.ReadOnly = true;
                CColumn col = new CColumn();
                col.Name = "id";
                col.Code = "id";
                col.ColType = ColumnType.guid_type;
                col.ColLen=16;
                col.AllowNull = false;
                col.IsSystem = true;
                col.IsUnique = true;
                col.IsVisible = false;
                col.Idx = 0;
                m_Table.ColumnMgr.AddNew(col);
                row.Tag = col;

                row = dataGridView.Rows[1];
                row.Cells[0].Value = "Created";
                cbCell = (DataGridViewComboBoxCell)row.Cells[1];
                cbCell.Value = "日期型";
                row.Cells[2].Value = "8";
                ckCell = (DataGridViewCheckBoxCell)row.Cells[3];
                ckCell.Value = true;
                ckCell2 = (DataGridViewCheckBoxCell)row.Cells[4];
                ckCell2.Value = true;
                ckCell3 = (DataGridViewCheckBoxCell)row.Cells[5];
                ckCell3.Value = false;
                row.ReadOnly = true;
                col = new CColumn();
                col.Name = "创建时间";
                col.Code = "Created";
                col.ColType = ColumnType.datetime_type;
                col.ColLen = 8;
                col.DefaultValue = "getdate()";
                col.AllowNull = true;
                col.IsSystem = true;
                col.IsUnique = false;
                col.IsVisible = false;
                col.Idx = 1;
                m_Table.ColumnMgr.AddNew(col);
                row.Tag = col;

                row = dataGridView.Rows[2];
                row.Cells[0].Value = "Creator";
                cbCell = (DataGridViewComboBoxCell)row.Cells[1];
                cbCell.Value = "引用型";
                row.Cells[2].Value = "16";
                ckCell = (DataGridViewCheckBoxCell)row.Cells[3];
                ckCell.Value = true;
                ckCell2 = (DataGridViewCheckBoxCell)row.Cells[4];
                ckCell2.Value = true;
                ckCell3 = (DataGridViewCheckBoxCell)row.Cells[5];
                ckCell3.Value = false;
                row.ReadOnly = true;
                col = new CColumn();
                col.Name = "创建者";
                col.Code = "Creator";
                col.ColType = ColumnType.ref_type;
                CTable tableUser = (CTable)Program.Ctx.TableMgr.FindByCode("B_User");
                Guid guidUid = Guid.Empty;
                Guid guidUname = Guid.Empty;
                if (tableUser != null)
                {
                    col.RefTable = tableUser.Id;
                    CColumn colUid = tableUser.ColumnMgr.FindByCode("id");
                    col.RefCol = colUid.Id;
                    guidUid = col.RefCol;
                    CColumn colUname = tableUser.ColumnMgr.FindByCode("name");
                    col.RefShowCol = colUname.Id;
                    guidUname = col.RefShowCol;
                }
                col.ColLen = 16;
                col.DefaultValue = "0";
                col.AllowNull = true;
                col.IsSystem = true;
                col.IsUnique = false;
                col.IsVisible = false;
                col.Idx = 2;
                m_Table.ColumnMgr.AddNew(col);
                row.Tag = col;

                row = dataGridView.Rows[3];
                row.Cells[0].Value = "Updated";
                cbCell = (DataGridViewComboBoxCell)row.Cells[1];
                cbCell.Value = "日期型";
                row.Cells[2].Value = "8";
                ckCell = (DataGridViewCheckBoxCell)row.Cells[3];
                ckCell.Value = true;
                ckCell2 = (DataGridViewCheckBoxCell)row.Cells[4];
                ckCell2.Value = true;
                ckCell3 = (DataGridViewCheckBoxCell)row.Cells[5];
                ckCell3.Value = false;
                row.ReadOnly = true;
                col = new CColumn();
                col.Name = "修改时间";
                col.Code = "Updated";
                col.ColType = ColumnType.datetime_type;
                col.ColLen = 8;
                col.DefaultValue = "getdate()";
                col.AllowNull = true;
                col.IsSystem = true;
                col.IsUnique = false;
                col.IsVisible = false;
                col.Idx = 3;
                m_Table.ColumnMgr.AddNew(col);
                row.Tag = col;

                row = dataGridView.Rows[4];
                row.Cells[0].Value = "Updator";
                cbCell = (DataGridViewComboBoxCell)row.Cells[1];
                cbCell.Value = "引用型";
                row.Cells[2].Value = "16";
                ckCell = (DataGridViewCheckBoxCell)row.Cells[3];
                ckCell.Value = true;
                ckCell2 = (DataGridViewCheckBoxCell)row.Cells[4];
                ckCell2.Value = true;
                ckCell3 = (DataGridViewCheckBoxCell)row.Cells[5];
                ckCell3.Value = false;
                row.ReadOnly = true;
                col = new CColumn();
                col.Name = "修改者";
                col.Code = "Updator";
                col.ColType = ColumnType.ref_type;
                if (tableUser != null)
                {
                    col.RefTable = tableUser.Id;
                    col.RefCol = guidUid;
                    col.RefShowCol = guidUname;
                }
                col.ColLen = 16;
                col.DefaultValue = "0";
                col.AllowNull = true;
                col.IsSystem = true;
                col.IsUnique = false;
                col.IsVisible = false;
                col.Idx = 4;
                m_Table.ColumnMgr.AddNew(col);
                row.Tag = col;
            }
            else
            {
                //m_Table.m_CmdType = CmdType.Update;
                Program.Ctx.TableMgr.Update(m_Table);
                txtTableName.Text = m_Table.Name;
                txtTableCode.Text = m_Table.Code;
                ckIsSystem.Checked = m_Table.IsSystem;
                List<CBaseObject> lstCol = m_Table.ColumnMgr.GetList();

                dataGridView.Rows.Add(lstCol.Count);
                int iRowIdx = 0;
                foreach (CBaseObject obj in lstCol)
                {
                    CColumn col = (CColumn)obj;

                    DataGridViewRow row = dataGridView.Rows[iRowIdx];
                    row.Cells[0].Value = col.Code;
                    DataGridViewComboBoxCell cbCell = (DataGridViewComboBoxCell)row.Cells[1];
                    cbCell.Value =CColumn.ConvertColTypeToString(col.ColType);
                    row.Cells[2].Value = col.ColLen.ToString();
                    DataGridViewCheckBoxCell ckCell = (DataGridViewCheckBoxCell)row.Cells[3];
                    ckCell.Value = col.AllowNull;
                    DataGridViewCheckBoxCell ckCell2 = (DataGridViewCheckBoxCell)row.Cells[4];
                    ckCell2.Value = col.IsSystem;
                    DataGridViewCheckBoxCell ckCell3 = (DataGridViewCheckBoxCell)row.Cells[5];
                    ckCell3.Value = col.IsUnique;

                    if (col.IsSystem)
                    {
                        row.ReadOnly = true;
                    }
                    else
                    {
                        //text image 类型字段不允许修改
                        //if (col.ColType == ColumnType.text_type
                        //    || col.ColType == ColumnType.object_type
                        //    || col.ColType == ColumnType.guid_type)
                        //    row.ReadOnly = true;
                        //else
                            row.ReadOnly = false;
                    }
                    ckCell2.ReadOnly = false;

                    iRowIdx++;
                    row.Tag = col;
                }
            }
            
        }


        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            CColumn col = (CColumn)dataGridView.Rows[e.RowIndex].Tag;
            if (col != null)
            {
                ColPropertySetting prop = (ColPropertySetting)col.m_objTempData;
                if (prop == null)
                {
                    prop = new ColPropertySetting();
                    prop.名称 = col.Name;
                    prop.默认值 = col.DefaultValue;
                    prop.公式 = col.Formula;
                    prop.小数位数 = col.ColDecimal;
                    if (col.ColType == ColumnType.ref_type)
                    {
                        CTable table = (CTable)Program.Ctx.TableMgr.Find(col.RefTable);
                        if (table != null)
                        {
                            prop.引用表 = table.Code;
                            CColumn column = (CColumn)table.ColumnMgr.Find(col.RefCol);
                            prop.引用字段 = column.Code;
                            CColumn column2 = (CColumn)table.ColumnMgr.Find(col.RefShowCol);
                            prop.引用显示字段 = column2.Code;
                        }
                    }
                    else if (col.ColType == ColumnType.enum_type)
                    {
                        CTable table = (CTable)Program.Ctx.TableMgr.Find(col.RefTable);
                        if (table != null)
                        {
                            prop.引用表 = table.Code;
                            CColumn column2 = (CColumn)table.ColumnMgr.Find(col.RefShowCol);
                            prop.引用显示字段 = column2.Code;
                        }
                        //
                        List<CBaseObject> lstObj = col.ColumnEnumValMgr.GetList();
                        string sEnumVal = "";
                        foreach (CBaseObject obj in lstObj)
                        {
                            CColumnEnumVal cev = (CColumnEnumVal)obj;
                            sEnumVal += cev.Val + "\\";
                        }
                        sEnumVal = sEnumVal.TrimEnd("\\".ToCharArray());
                        prop.枚举值 = sEnumVal;
                    }
                    prop.界面控件 = col.UIControl;
                    prop.web界面控件 = col.WebUIControl;

                    col.m_objTempData = prop;
                }
                propertyGrid.SelectedObject = prop;
                propertyGrid.Refresh();
                UpdateRefTable(prop.引用表);

                //if (col.IsSystem))
                //    propertyGrid.Enabled = false;
                //else
                //    propertyGrid.Enabled = true;
            }
        }

        private void tbtDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow == null)
                return;
            CColumn col = (CColumn)dataGridView.CurrentRow.Tag;
            if (col == null)
                return;
            if (col.IsSystem)
            {
                MessageBox.Show("系统字段不能删除！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            m_Table.ColumnMgr.Delete(col);
            dataGridView.Rows.Remove(dataGridView.CurrentRow);
        }

        private void propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (e.ChangedItem.Label == "引用表")
            {
                string sCode = e.ChangedItem.Value.ToString();
                UpdateRefTable(sCode);
            }
            if (dataGridView.CurrentRow == null)
                return;
            CColumn col = (CColumn)dataGridView.CurrentRow.Tag;
            if (col == null)
                return;
            //if (col.m_CmdType != CmdType.AddNew)
            //    col.m_CmdType = CmdType.Update;
            m_Table.ColumnMgr.Update(col);
        }
        void UpdateRefTable(string sCode)
        {
            CTable table = Program.Ctx.TableMgr.FindByCode(sCode);
            if (table != null)
            {
                PropertyGridComboBoxItem._hash.Clear();
                List<CBaseObject> lstCol = table.ColumnMgr.GetList();
                foreach (CBaseObject obj in lstCol)
                {
                    CColumn col = (CColumn)obj;
                    PropertyGridComboBoxItem._hash.Add(col.Code, col.Code);
                }
            }
            else
                PropertyGridComboBoxItem._hash.Clear();

            propertyGrid.Refresh();
        }

        private void tbtUp_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow == null)
            {
                MessageBox.Show("请选择一行！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (dataGridView.CurrentRow.Index == 0)
                return;
            int idx = dataGridView.CurrentRow.Index;
            DataGridViewRow row = dataGridView.CurrentRow;
            DataGridViewRow row2 = dataGridView.Rows[idx-1];
            dataGridView.Rows.Remove(row2);
            dataGridView.Rows.Insert(idx , row2);
        }

        private void tbtDown_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow == null)
            {
                MessageBox.Show("请选择一行！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (dataGridView.CurrentRow.Index == dataGridView.Rows.Count-1)
                return;

            int idx = dataGridView.CurrentRow.Index;
            DataGridViewRow row = dataGridView.CurrentRow;
            DataGridViewRow row2 = dataGridView.Rows[idx + 1];
            dataGridView.Rows.Remove(row2);
            dataGridView.Rows.Insert(idx, row2);
        }

        private void btSetDataServer_Click(object sender, EventArgs e)
        {
            DataServerListForm frm = new DataServerListForm();
            frm.m_Table = m_Table;
            frm.ShowDialog();
        }

    }
}
