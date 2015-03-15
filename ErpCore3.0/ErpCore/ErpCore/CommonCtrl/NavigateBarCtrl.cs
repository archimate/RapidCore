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

namespace ErpCore.CommonCtrl
{
    public partial class NavigateBarCtrl : UserControl
    {
        CWindowControl windowControl = null;

        public NavigateBarCtrl()
        {
            InitializeComponent();
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

        private void NavigateBarCtrl_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        public void LoadData()
        {
            if (windowControl == null)
                return;
            flowLayoutPanel.Controls.Clear();
            List<CBaseObject> lstButton = windowControl.NavigateBarButtonMgr.GetList();
            foreach (CBaseObject obj in lstButton)
            {
                CNavigateBarButton navbt = (CNavigateBarButton)obj;
                NavigateBarButtonCtrl navbtEl = new NavigateBarButtonCtrl();
                navbtEl.NavigateBarButton = navbt;
                navbtEl.Title = navbt.Name;
                navbtEl.SetIcon(navbt.Icon);

                Add(navbtEl);
            }
        }

        public void Add(NavigateBarButtonCtrl navbt)
        {
            flowLayoutPanel.Controls.Add(navbt);
        }

    }
}
