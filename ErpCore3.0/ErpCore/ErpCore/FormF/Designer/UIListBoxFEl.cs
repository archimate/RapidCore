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
    public partial class UIListBoxFEl : UserControl, IDesignEl
    {

        string captionText = "";
        bool showTitleBar = false;

        CFormControl formControl = null;

        public UIListBoxFEl()
        {
            InitializeComponent();
        }

        public ControlType GetCtrlType()
        {
            return ControlType.ListBox;
        }
        public void OnEdit()
        {
            SelTableForm frm = new SelTableForm();
            if (frm.ShowDialog() != DialogResult.OK)
                return;
            SelColumnForm frm2 = new SelColumnForm();
            frm2.m_Table = frm.m_SelTable;
            if (frm2.ShowDialog() != DialogResult.OK)
                return;

            string sCtrlName = ChildenForm.GetDistinctName(frm.m_SelTable.Name, (CFormControlMgr)FormControl.m_ObjectMgr);


            FormControl.TableInFormControlMgr.RemoveAll();
            string sText = string.Format("[{0}]", frm2.m_SelColumn.Code);
            CTableInFormControl tiwc = new CTableInFormControl();
            tiwc.Ctx = Program.Ctx;
            tiwc.FW_Table_id = frm.m_SelTable.Id;
            tiwc.UI_FormControl_id = FormControl.Id;
            tiwc.Text = sText;
            FormControl.TableInFormControlMgr.AddNew(tiwc);

            CaptionText = sCtrlName;
            FormControl = FormControl;
        }


        public CFormControl FormControl
        {
            get { return formControl; }
            set
            {
                formControl = value;
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
                tbTitle.Visible = showTitleBar;
            }
        }
    }
}
