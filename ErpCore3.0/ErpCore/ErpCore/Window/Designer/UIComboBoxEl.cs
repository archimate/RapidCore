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
    public partial class UIComboBoxEl : UserControl, IDesignEl
    {

        string captionText = "";
        bool showTitleBar = false;

        CWindowControl windowControl = null;

        public UIComboBoxEl()
        {
            InitializeComponent();
        }
        public ControlType GetCtrlType()
        {
            return ControlType.ComboBox;
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

            string sCtrlName = ChildenWindow.GetDistinctName(frm.m_SelTable.Name, (CWindowControlMgr)WindowControl.m_ObjectMgr);


            WindowControl.TableInWindowControlMgr.RemoveAll();
            string sText = string.Format("[{0}]", frm2.m_SelColumn.Code);
            CTableInWindowControl tiwc = new CTableInWindowControl();
            tiwc.Ctx = Program.Ctx;
            tiwc.FW_Table_id = frm.m_SelTable.Id;
            tiwc.UI_WindowControl_id = WindowControl.Id;
            tiwc.Text = sText;
            WindowControl.TableInWindowControlMgr.AddNew(tiwc);

            CaptionText = sCtrlName;

        }


        public CWindowControl WindowControl
        {
            get { return windowControl; }
            set
            {
                windowControl = value;
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
                lbTitle.Visible = showTitleBar;
            }
        }
    }
}
