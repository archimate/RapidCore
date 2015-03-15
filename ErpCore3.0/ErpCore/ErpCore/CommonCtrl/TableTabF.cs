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
    public partial class TableTabF : UserControl, IWindowCtrl
    {
        string captionText = "";
        bool showTitleBar = false;

        CFormControl formControl = null;

        public TableTabF()
        {
            InitializeComponent();
        }


        public ControlType GetCtrlType()
        {
            return ControlType.TableTab;
        }
        public CFormControl FormControl
        {
            get { return formControl; }
            set
            {
                formControl = value;
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
            if (FormControl == null)
                return;
            tabControl.TabPages.Clear();
            List<CBaseObject> lstObj = FormControl.TableInFormControlMgr.GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CTableInFormControl tiwc = (CTableInFormControl)obj;
                CTable table = (CTable)Program.Ctx.TableMgr.Find(tiwc.FW_Table_id);
                if (table == null)
                    continue;
                TabPage page = new TabPage(table.Name);
                page.Tag = tiwc;
                tabControl.TabPages.Add(page);


                CBaseObjectMgr objMgr = new CBaseObjectMgr();
                objMgr.TbCode = table.Code;
                objMgr.Ctx = Program.Ctx;
                TableGridF te = new TableGridF();
                te.TableInFormControl = tiwc;
                te.BaseObjectMgr = objMgr;
                te.CaptionText = table.Name;
                te.ShowTitleBar = false;
                te.ShowToolBar = true;
                te.Tag = FormControl;
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
