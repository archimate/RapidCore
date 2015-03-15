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
using ErpCoreModel.UI;
using ErpCore.Window;
using ErpCore.Database.Table;

namespace ErpCore.Window.Designer
{
    public partial class TableGridEl : UserControl, IDesignEl
    {
        private CTableInWindowControl tableInWindowControl = null;

        string captionText = "";
        bool showToolBar = false;
        bool showTitleBar = false;

        CWindowControl windowControl = null;


        public TableGridEl()
        {
            InitializeComponent();
        }

        public ControlType GetCtrlType()
        {
            return ControlType.TableGrid;
        }
        public void OnEdit()
        {
            SelTableForm frm = new SelTableForm();
            if (frm.ShowDialog() != DialogResult.OK)
                return;

            WindowControl.TableInWindowControlMgr.RemoveAll();

            CTableInWindowControl tiwc = new CTableInWindowControl();
            tiwc.Ctx = Program.Ctx;
            tiwc.FW_Table_id = frm.m_SelTable.Id;
            tiwc.UI_WindowControl_id = WindowControl.Id;
            tiwc.Text = frm.m_SelTable.Name;
            WindowControl.TableInWindowControlMgr.AddNew(tiwc);

            foreach (CColumn col in frm.m_SelTable.ColumnMgr.GetList())
            {
                //if (!col.IsVisible)
                //    continue;
                CColumnInTableInWindowControl ciwc = new CColumnInTableInWindowControl();
                ciwc.Ctx = Program.Ctx;
                ciwc.FW_Column_id = col.Id;
                ciwc.UI_TableInWindowControl_id = tiwc.Id;
                tiwc.ColumnInTableInWindowControlMgr.AddNew(ciwc);
            }
            foreach (ToolStripItem tbutton in toolStrip.Items)
            {
                CTButtonInTableInWindowControl tbiwc = new CTButtonInTableInWindowControl();
                tbiwc.Ctx = Program.Ctx;
                tbiwc.Title = tbutton.Text;
                tbiwc.UI_TableInWindowControl_id = tiwc.Id;
                tiwc.TButtonInTableInWindowControlMgr.AddNew(tbiwc);
            }

            TableInWindowControl = tiwc;
        }


        public CWindowControl WindowControl
        {
            get { return windowControl; }
            set
            {
                windowControl = value;
                LoadTable();
            }
        }
        public CTableInWindowControl TableInWindowControl
        {
            get { return tableInWindowControl; }
            set
            {
                tableInWindowControl = value;
                LoadTable();
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
            }
        }
        private void TableCtrlEl_Load(object sender, EventArgs e)
        {
            LoadTable();
        }
        void LoadTable()
        {
            dataGridView.Columns.Clear();
            if (WindowControl == null)
                return;
            CTableInWindowControl tiwc = null;

            if (TableInWindowControl == null)
            {
                tiwc = (CTableInWindowControl)WindowControl.TableInWindowControlMgr.GetFirstObj();
                if (tiwc == null)
                    return;
            }
            else
                tiwc = TableInWindowControl;

            CTable table = (CTable)Program.Ctx.TableMgr.Find(tiwc.FW_Table_id);
            if (table == null)
                return;

            {
                bool bHas = false;
                List<CBaseObject> lstCIWC = tiwc.ColumnInTableInWindowControlMgr.GetList();
                if (lstCIWC.Count > 0)
                {
                    bHas = true;
                    foreach (CBaseObject obj in lstCIWC)
                    {
                        CColumnInTableInWindowControl ciwc = (CColumnInTableInWindowControl)obj;
                        CColumn col = (CColumn)table.ColumnMgr.Find(ciwc.FW_Column_id);
                        dataGridView.Columns.Add(col.Code, col.Name);
                    }
                }

                if (!bHas)
                {
                    List<CBaseObject> lstCol = table.ColumnMgr.GetList();
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

            List<CBaseObject> lstTButton = tiwc.TButtonInTableInWindowControlMgr.GetList();
            foreach (ToolStripItem tbutton in toolStrip.Items)
            {
                bool bHas = false;
                foreach (CBaseObject obj in lstTButton)
                {
                    CTButtonInTableInWindowControl tbiwc = (CTButtonInTableInWindowControl)obj;
                    if (tbiwc.Title.Equals(tbutton.Text, StringComparison.OrdinalIgnoreCase))
                    {
                        bHas = true;
                        break;
                    }
                }
                if (!bHas)
                    tbutton.Visible = false;
            }
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
        public void SetToolBarButtonVisible(string sTitle, bool bVisible)
        {
            foreach(ToolStripItem tbutton in toolStrip.Items)
            {
                if (tbutton.Text.Equals(sTitle, StringComparison.OrdinalIgnoreCase))
                {
                    tbutton.Visible = bVisible;
                    break;
                }
            }
        }


    }
}
