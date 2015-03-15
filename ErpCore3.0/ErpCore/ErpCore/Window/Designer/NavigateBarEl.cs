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
using ErpCore.Window;
using ErpCoreModel.UI;

namespace ErpCore.Window.Designer
{
    public partial class NavigateBarEl : UserControl, IDesignEl
    {
        CWindowControl windowControl = null;


        public NavigateBarEl()
        {
            InitializeComponent();
        }


        public ControlType GetCtrlType()
        {
            return ControlType.NavBar;
        }
        public void OnEdit()
        {
            OnEditButton();
        }

        public CWindowControl WindowControl
        {
            get { return windowControl; }
            set
            {
                windowControl = value;
                LoadData();
            }
        }

        private void NavigateBarEl_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        public void LoadData()
        {
            if (WindowControl == null)
                return;
            flowLayoutPanel.Controls.Clear();
            List<CBaseObject> lstButton = WindowControl.NavigateBarButtonMgr.GetList();
            foreach (CBaseObject obj in lstButton)
            {
                CNavigateBarButton navbt = (CNavigateBarButton)obj;
                NavigateBarButtonEl navbtEl = new NavigateBarButtonEl();
                navbtEl.Title = navbt.Name;
                navbtEl.SetIcon(navbt.Icon);

                Add(navbtEl);
            }
        }

        public void Add(NavigateBarButtonEl navbt)
        {
            flowLayoutPanel.Controls.Add(navbt);
        }

        private void MenuItemEdit_Click(object sender, EventArgs e)
        {
            OnEditButton();
        }
        public void OnEditButton()
        {
            //CBaseObjectMgr BaseObjectMgr = new CBaseObjectMgr();
            //BaseObjectMgr.TbCode = "UI_NavigateBarButton";
            //BaseObjectMgr.Ctx = Program.Ctx;

            TableWindow frm = new TableWindow(WindowControl.NavigateBarButtonMgr);
            frm.ShowDialog();

            LoadData();
        }

        public void MenuItemDelete_Click(object sender, EventArgs e)
        {
            WindowControl.m_ObjectMgr.Delete(WindowControl);
            this.Parent.Controls.Remove(this);
        }

        private void MenuItemBringToFront_Click(object sender, EventArgs e)
        {
            this.BringToFront();
        }

        private void MenuItemSendToBack_Click(object sender, EventArgs e)
        {
            this.SendToBack();
        }

    }
}
