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

namespace ErpCore.Database.Diagram
{
    public partial class DesignerForm : Form
    {
        public CDiagram m_Diagram = null;
        int m_iTableBoxWidth = 150;
        int m_iTableBoxHeight = 200;
        int m_iTableBoxCaptionSize = 30;
        int m_iTableBoxDis = 5;

        TableBox m_CurSelTableBox = null;

        public DesignerForm()
        {
            InitializeComponent();
        }

        private void DesignerForm_Resize(object sender, EventArgs e)
        {
            if (panelDiagram.Width < this.Width)
                panelDiagram.Width = this.Width;
            if (panelDiagram.Height < this.Height)
                panelDiagram.Height = this.Height;
        }

        private void DesignerForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        void LoadData()
        {
            panelDiagram.Controls.Clear();
            List<CBaseObject> lstTableInDiagram = m_Diagram.TableInDiagramMgr.GetList();
            foreach (CBaseObject obj in lstTableInDiagram)
            {
                CTableInDiagram TableInDiagram = (CTableInDiagram)obj;
                CTable table = (CTable)Program.Ctx.TableMgr.Find(TableInDiagram.FW_Table_id);
                if (table!=null)
                {
                    TableBox tableBox = new TableBox();
                    tableBox.Name = "TableBox";
                    tableBox.Table = table;
                    tableBox.Moveable = true;
                    tableBox.CaptionText = table.Name;
                    tableBox.CaptionSize = m_iTableBoxCaptionSize;
                    tableBox.Left = TableInDiagram.X;
                    tableBox.Top = TableInDiagram.Y;
                    tableBox.Width = TableInDiagram.Width;
                    tableBox.Height = TableInDiagram.Height;
                    tableBox.Move += new EventHandler(tableBox_Move);
                    tableBox.SizeChanged += new EventHandler(tableBox_Move);
                    tableBox.CaptionControl.MouseClick += new MouseEventHandler(CaptionControl_MouseClick);

                    panelDiagram.Controls.Add(tableBox);
                }
            }
        }


        void CaptionControl_MouseClick(object sender, MouseEventArgs e)
        {
            Stepi.UI.CaptionCtrl capctrl = (Stepi.UI.CaptionCtrl)sender;
            TableBox tableBox = (TableBox)capctrl.Parent;
            if (m_CurSelTableBox != null)
            {
                m_CurSelTableBox.CaptionColorTwo = Color.FromArgb(155, Color.Orange);
            }
            m_CurSelTableBox = tableBox;
            m_CurSelTableBox.CaptionColorTwo =  Color.Blue;
            m_CurSelTableBox.Focus();
        }

        private void tbtNewTable_Click(object sender, EventArgs e)
        {
            ErpCore.Database.Table.TableInfoForm frm = new ErpCore.Database.Table.TableInfoForm();
            if (frm.ShowDialog() != DialogResult.OK)
                return;
            AddTable(frm.m_Table);
        }

        private void tbtAddTable_Click(object sender, EventArgs e)
        {
            AddTableForm frm = new AddTableForm();
            frm.m_DesignerForm = this;
            frm.ShowDialog();
        }

        private void tbtRemoveTable_Click(object sender, EventArgs e)
        {
            if (m_CurSelTableBox == null)
                return;
            CTable table = m_CurSelTableBox.Table;
            List<CBaseObject> lstTableInDiagram = m_Diagram.TableInDiagramMgr.GetList();
            foreach (CBaseObject obj in lstTableInDiagram)
            {
                CTableInDiagram TableInDiagram = (CTableInDiagram)obj;
                if (TableInDiagram.FW_Table_id == table.Id)
                {
                    m_Diagram.TableInDiagramMgr.Delete(TableInDiagram);
                    break;
                }
            }
            panelDiagram.Controls.Remove(m_CurSelTableBox);
            panelDiagram.Invalidate();
        }

        public void AddTable(CTable table)
        {
            List<CBaseObject> lstTableInDiagram = m_Diagram.TableInDiagramMgr.GetList();
            foreach (CBaseObject obj in lstTableInDiagram)
            {
                CTableInDiagram tind = (CTableInDiagram)obj;
                if (tind.FW_Table_id == table.Id)
                {
                    return;
                }
            }
            CTableInDiagram TableInDiagram = new CTableInDiagram();
            TableInDiagram.FW_Table_id = table.Id;
            TableInDiagram.FW_Diagram_id = m_Diagram.Id;
            TableInDiagram.IsStandard = false;
            TableInDiagram.Width = m_iTableBoxWidth;
            TableInDiagram.Height = m_iTableBoxHeight;
            //计算坐标，使不与旧表重叠
            int x = m_iTableBoxDis, y = m_iTableBoxDis;//边空隙
            TableInDiagram.X = x;
            TableInDiagram.Y = y;
            while (true)
            {
                bool bInRect = false;
                foreach (PO po in lstTableInDiagram)
                {
                    CTableInDiagram tind = (CTableInDiagram)po;
                    if (CTableInDiagram.InRect(tind, TableInDiagram))
                    {
                        bInRect = true;
                        TableInDiagram.X = tind.X + tind.Width + m_iTableBoxDis;
                        break;
                    }
                }
                if (!bInRect)
                    break;
            }
            //

            if (!m_Diagram.TableInDiagramMgr.AddNew(TableInDiagram))
            {
                MessageBox.Show("添加表失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            TableBox tableBox = new TableBox();
            tableBox.Name = "TableBox";
            tableBox.Table = table;
            tableBox.Moveable = true;
            tableBox.CaptionText = table.Name;
            tableBox.CaptionSize = m_iTableBoxCaptionSize;
            tableBox.Left = TableInDiagram.X;
            tableBox.Top = TableInDiagram.Y;
            tableBox.Width = TableInDiagram.Width;
            tableBox.Height = TableInDiagram.Height;
            tableBox.Move += new EventHandler(tableBox_Move);
            tableBox.SizeChanged += new EventHandler(tableBox_Move);
            tableBox.CaptionControl.MouseClick += new MouseEventHandler(CaptionControl_MouseClick);

            panelDiagram.Controls.Add(tableBox);
        }

        void tableBox_Move(object sender, EventArgs e)
        {
            TableBox tableBox = (TableBox)sender;
            if (panelDiagram.Width < tableBox.Left + tableBox.Width + m_iTableBoxDis)
                panelDiagram.Width = tableBox.Left + tableBox.Width + m_iTableBoxDis;
            if (panelDiagram.Height < tableBox.Top + tableBox.Height + m_iTableBoxDis)
                panelDiagram.Height = tableBox.Top + tableBox.Height + m_iTableBoxDis;

            panelDiagram.Invalidate();
        }

        private void tbtSave_Click(object sender, EventArgs e)
        {
            foreach (Control ctrl in panelDiagram.Controls)
            {
                if (ctrl.Name.Equals("TableBox", StringComparison.OrdinalIgnoreCase))
                {
                    TableBox tableBox = (TableBox)ctrl;
                    CTable table = tableBox.Table;

                    List<CBaseObject> lstTableInDiagram = m_Diagram.TableInDiagramMgr.GetList();
                    foreach (CBaseObject obj in lstTableInDiagram)
                    {
                        CTableInDiagram tind = (CTableInDiagram)obj;
                        if (tind.FW_Table_id == table.Id)
                        {
                            tind.X = tableBox.Left;
                            tind.Y = tableBox.Top;
                            tind.Width = tableBox.Width;
                            tind.Height = tableBox.Height;
                            m_Diagram.TableInDiagramMgr.Update(tind);
                        }
                    }
                }
            }
            m_Diagram.m_CmdType = CmdType.Update;
            if (!m_Diagram.Save(true))
            {
                MessageBox.Show("保存失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
        }

        private void panelDiagram_Paint(object sender, PaintEventArgs e)
        {

            foreach (Control ctrl in panelDiagram.Controls)
            {
                if (ctrl.Name.Equals("TableBox", StringComparison.OrdinalIgnoreCase))
                {
                    TableBox tableBox = (TableBox)ctrl;
                    CTable table = tableBox.Table;
                    //找主表                    
                    List<CBaseObject> lstCol = table.ColumnMgr.GetList();
                    foreach (CBaseObject obj in lstCol)
                    {
                        CColumn col = (CColumn)obj;
                        if (col.ColType == ColumnType.ref_type)
                        {
                            CTable table2 = (CTable)Program.Ctx.TableMgr.Find(col.RefTable);

                            foreach (Control ctrl2 in panelDiagram.Controls)
                            {
                                if (ctrl2.Name.Equals("TableBox", StringComparison.OrdinalIgnoreCase))
                                {
                                    TableBox tableBox2 = (TableBox)ctrl2;
                                    if (table2 == tableBox2.Table)
                                    {
                                        //画关联线
                                        System.Drawing.Drawing2D.AdjustableArrowCap lineCap 
                                            = new System.Drawing.Drawing2D.AdjustableArrowCap(4, 4, true);
                                        Pen RedPen = new Pen(Color.Red, 1);
                                        RedPen.CustomEndCap = lineCap;

                                        int x1 = tableBox.Left ;
                                        int y1 = tableBox.Top + tableBox.Height / 2;
                                        int x2 = tableBox2.Left ;
                                        int y2 = tableBox2.Top + tableBox2.Height / 2;

                                        e.Graphics.DrawLine(RedPen, x1, y1, x2, y2);
                                    }
                                }
                            }
                        }
                    }

                }
            }
        }
    }
}
