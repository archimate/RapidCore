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

namespace ErpCore.CommonCtrl
{
    public partial class TableTab : UserControl, IWindowCtrl
    {
        string captionText = "";
        bool showTitleBar = false;

        CWindowControl windowControl = null;

        public TableTab()
        {
            InitializeComponent();
        }


        public ControlType GetCtrlType()
        {
            return ControlType.TableTab;
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


                CBaseObjectMgr objMgr = new CBaseObjectMgr();
                objMgr.TbCode = table.Code;
                objMgr.Ctx = Program.Ctx;
                TableGrid te = new TableGrid();
                te.TableInWindowControl = tiwc;
                te.BaseObjectMgr = objMgr;
                te.CaptionText = table.Name;
                te.ShowTitleBar = false;
                te.ShowToolBar = true;
                te.Tag = WindowControl;
                page.Controls.Add(te);
                te.Dock = DockStyle.Fill;
                te.BringToFront();
            }
        }


        //界面公式取值
        public object GetSelectValue(string sColCode)
        {
            if (tabControl.SelectedTab == null)
                return null;
            TableGrid te = (TableGrid)tabControl.SelectedTab.Controls[0];
            return te.GetSelectValue(sColCode);
        }
    }
}
