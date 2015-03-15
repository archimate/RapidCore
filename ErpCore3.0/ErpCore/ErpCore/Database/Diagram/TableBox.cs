using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

using System.Text;
using System.Windows.Forms;
using System.Drawing;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;

namespace ErpCore.Database.Diagram
{
    public partial class TableBox : Stepi.UI.ExtendedPanel
    {
        private CTable table = null;
        public DataGridView dataGridView = null;
        private bool standard = false;

        public TableBox()
        {
            InitializeComponent();

            AddDataGridView();
            this.CaptionControl.MouseClick += new MouseEventHandler(CaptionControl_MouseClick);
            this.CaptionTextColor = Color.Black;
            
            this.CaptionFont = new Font("黑体", 10, FontStyle.Bold);
        }


        public TableBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();

            AddDataGridView();
            this.CaptionControl.MouseClick += new MouseEventHandler(CaptionControl_MouseClick);
            this.CaptionTextColor = Color.Black;
            this.CaptionFont = new Font("黑体", 10, FontStyle.Bold);
        }

        public CTable Table
        {
            get { return table; }
            set 
            { 
                table = value;
                LoadTable();
            }
        }
        public bool Standard
        {
            get { return standard; }
            set { 
                standard = value;
                if (standard)
                {
                    this.Width = 400;
                    dataGridView.Columns[1].Visible = true;
                    dataGridView.Columns[2].Visible = true;
                    dataGridView.Columns[3].Visible = true;
                    dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.Single;
                }
                else
                {
                    this.Width = 150;
                    dataGridView.Columns[1].Visible = false;
                    dataGridView.Columns[2].Visible = false;
                    dataGridView.Columns[3].Visible = false;
                    dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.None;
                }
            }
        }

        void AddDataGridView()
        {
            dataGridView = new DataGridView();
            dataGridView.Columns.Add("colName", "列名");
            DataGridViewComboBoxColumn dvCol2= new DataGridViewComboBoxColumn();
            dvCol2.HeaderText = "类型";
            dvCol2.Items.Add("字符型");
            dvCol2.Items.Add("整型");
            dvCol2.Items.Add("长整型");
            dvCol2.Items.Add("布尔型");
            dvCol2.Items.Add("数值型");
            dvCol2.Items.Add("日期型");
            dvCol2.Items.Add("备注型");
            dvCol2.Items.Add("二进制");
            dvCol2.Items.Add("引用型");
            dataGridView.Columns.Add(dvCol2);
            dataGridView.Columns.Add("colLen", "长度");
            DataGridViewCheckBoxColumn dvCol4 = new DataGridViewCheckBoxColumn();
            dvCol4.HeaderText = "允许空";
            dataGridView.Columns.Add(dvCol4);

            dataGridView.AllowUserToAddRows = false;
            dataGridView.RowHeadersWidth = 20;

            dataGridView.BackgroundColor = Color.White;
            dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dataGridView.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            

            dataGridView.Columns[1].Visible = false;
            dataGridView.Columns[2].Visible = false;
            dataGridView.Columns[3].Visible = false;
            dataGridView.ClearSelection();
            this.Controls.Add(dataGridView);
        }
        void LoadTable()
        {
            if (Table == null)
                return;
            dataGridView.Rows.Clear();
            List<CBaseObject> lstCol = Table.ColumnMgr.GetList();

            dataGridView.Rows.Add(lstCol.Count);
            int iRowIdx = 0;
            //系统字段
            foreach (CBaseObject obj in lstCol)
            {
                CColumn col = (CColumn)obj;
                if (!col.IsSystem)
                    continue;

                DataGridViewRow row = dataGridView.Rows[iRowIdx];
                row.Cells[0].Value = col.Code;
                DataGridViewComboBoxCell cbCell = (DataGridViewComboBoxCell)row.Cells[1];
                cbCell.Value = CColumn.ConvertColTypeToString(col.ColType);
                row.Cells[2].Value = col.ColLen.ToString();
                DataGridViewCheckBoxCell ckCell2 = (DataGridViewCheckBoxCell)row.Cells[3];
                ckCell2.Value = col.AllowNull;
                row.ReadOnly = true;

                row.Tag = col;
                iRowIdx++;
            }
            //自定义字段
            foreach (PO po in lstCol)
            {
                CColumn col = (CColumn)po;
                if (col.IsSystem)
                    continue;

                DataGridViewRow row = dataGridView.Rows[iRowIdx];
                row.Cells[0].Value = col.Code;
                DataGridViewComboBoxCell cbCell = (DataGridViewComboBoxCell)row.Cells[1];
                cbCell.Value = CColumn.ConvertColTypeToString(col.ColType);
                row.Cells[2].Value = col.ColLen.ToString();
                DataGridViewCheckBoxCell ckCell2 = (DataGridViewCheckBoxCell)row.Cells[3];
                ckCell2.Value = col.AllowNull;

                //text image 类型字段不允许修改
                if (col.ColType == ColumnType.text_type
                    || col.ColType == ColumnType.object_type)
                    row.ReadOnly = true;
                else
                    row.ReadOnly = false;

                row.Tag = col;
                iRowIdx++;
            }
            dataGridView.ClearSelection();
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            dataGridView.Width = this.Width;
            dataGridView.Height = this.Height-this.CaptionSize;
        }

        void CaptionControl_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point pt = this.PointToScreen(e.Location);
                contextMenu.Show(pt.X, pt.Y);
            }
        }

        void MenuItemStandard_Click(object sender, System.EventArgs e)
        {
            if (MenuItemStandard.Text == "标准")
            {
                MenuItemStandard.Text = "列名";
                Standard = true;
            }
            else
            {
                MenuItemStandard.Text = "标准";
                Standard = false;
            }
        }

        void MenuItemEditTable_Click(object sender, System.EventArgs e)
        {
            ErpCore.Database.Table.TableInfoForm frm = new ErpCore.Database.Table.TableInfoForm();
            frm.m_Table = this.Table;
            if (frm.ShowDialog() != DialogResult.OK)
                return;
            Table = frm.m_Table;
        }
    }
}
