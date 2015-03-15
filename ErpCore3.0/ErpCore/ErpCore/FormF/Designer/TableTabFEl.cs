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
using ErpCore.Window.Designer;
using ErpCore.Database.Table;

namespace ErpCore.FormF.Designer
{
    public partial class TableTabFEl : UserControl, IDesignEl
    {
        string captionText = "";
        bool showTitleBar = false;

        CFormControl formControl = null;

        public TableTabFEl()
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

            FormControl.TableInFormControlMgr.RemoveAll();

            foreach (CTable table in frm.m_lstSelTable)
            {
                CTableInFormControl tiwc = new CTableInFormControl();
                tiwc.Ctx = Program.Ctx;
                tiwc.FW_Table_id = table.Id;
                tiwc.UI_FormControl_id = FormControl.Id;
                tiwc.Text = table.Name;
                FormControl.TableInFormControlMgr.AddNew(tiwc);
            }

            LoadTab();
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

                TableGridFEl te = new TableGridFEl();
                te.FormControl = FormControl;
                te.TableInFormControl = tiwc;
                te.CaptionText = table.Name;
                te.ShowTitleBar = tiwc.ShowTitleBar;
                te.ShowToolBar = tiwc.ShowToolBar;
                te.Tag = FormControl;
                page.Controls.Add(te);
                te.Dock = DockStyle.Fill;
                te.BringToFront();
            }
        }
        public TableGridFEl GetCurTableGridEl()
        {
            if (tabControl.SelectedTab == null)
                return null;
            return (TableGridFEl)tabControl.SelectedTab.Controls[0];
        }
    }
}
