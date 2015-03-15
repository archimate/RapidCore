using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;

using System.Text;
using System.Windows.Forms;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.UI;
using ErpCore.Window;
using ErpCore.Database.Table;

namespace ErpCore.Window.Designer
{
    public partial class TableTabEl : UserControl, IDesignEl
    {
        string captionText = "";
        bool showTitleBar = false;

        CWindowControl windowControl = null;

        public TableTabEl()
        {
            InitializeComponent();
        }
        public ControlType GetCtrlType()
        {
            return ControlType.TableTab;
        }
        public void OnEdit()
        {
            SelMultTableForm frm = new SelMultTableForm();
            if (frm.ShowDialog() != DialogResult.OK)
                return;

            WindowControl.TableInWindowControlMgr.RemoveAll();

            foreach (CTable table in frm.m_lstSelTable)
            {
                CTableInWindowControl tiwc = new CTableInWindowControl();
                tiwc.Ctx = Program.Ctx;
                tiwc.FW_Table_id = table.Id;
                tiwc.UI_WindowControl_id = WindowControl.Id;
                tiwc.Text = table.Name;
                WindowControl.TableInWindowControlMgr.AddNew(tiwc);
            }

            LoadTab();
        }


        public CWindowControl WindowControl
        {
            get { return windowControl; }
            set
            {
                windowControl = value;
                LoadTab();
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

        public bool ShowTitleBar
        {
            get { return showTitleBar; }
            set
            {
                showTitleBar = value;
                tbTitle .Visible = showTitleBar;
            }
        }

        void LoadTab()
        {
            if (WindowControl == null)
                return;
            tabControl.TabPages.Clear();
            List<CBaseObject> lstObj =  WindowControl.TableInWindowControlMgr.GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CTableInWindowControl tiwc = (CTableInWindowControl)obj;
                CTable table = (CTable)Program.Ctx.TableMgr.Find(tiwc.FW_Table_id);
                if (table == null)
                    continue;
                TabPage page = new TabPage(table.Name);
                page.Tag = tiwc;
                tabControl.TabPages.Add(page);

                TableGridEl te = new TableGridEl();
                te.WindowControl = WindowControl;
                te.TableInWindowControl = tiwc;
                te.CaptionText = table.Name;
                te.ShowTitleBar = tiwc.ShowTitleBar;
                te.ShowToolBar = tiwc.ShowToolBar;
                te.Tag = WindowControl;
                page.Controls.Add(te);
                te.Dock = DockStyle.Fill;
                te.BringToFront();
            }
        }
        public TableGridEl GetCurTableGridEl()
        {
            if (tabControl.SelectedTab == null)
                return null;
            return (TableGridEl)tabControl.SelectedTab.Controls[0];
        }
    }
}
