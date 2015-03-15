using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

using System.Text;
using System.Windows.Forms;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCore.Window;
using ErpCoreModel.UI;

namespace ErpCore.CommonCtrl
{
    public partial class UIToolbar : ToolStrip, IWindowCtrl
    {
        CWindowControl windowControl = null;

        public CWindowControl WindowControl
        {
            get { return windowControl; }
            set
            {
                windowControl = value;
                LoadData();
            }
        }


        public ControlType GetCtrlType()
        {
            return ControlType.NavBar;
        }
        public UIToolbar()
        {
            InitializeComponent();
        }

        public UIToolbar(IContainer container)
        {
            container.Add(this);

            InitializeComponent();

        }

        public void LoadData()
        {
            if (WindowControl == null)
                return;
            this.Items.Clear();
            List<CBaseObject> lstButton = WindowControl.NavigateBarButtonMgr.GetList();
            foreach (CBaseObject obj in lstButton)
            {
                CNavigateBarButton navbt = (CNavigateBarButton)obj;
                UIToolbarButton tbutton = new UIToolbarButton();
                tbutton.NavigateBarButton = navbt;
                tbutton.Text = navbt.Name;
                tbutton.SetIcon(navbt.Icon);
                tbutton.TextImageRelation = TextImageRelation.ImageAboveText;
                
                Add(tbutton);
            }
        }

        public void Add(UIToolbarButton navbt)
        {
            this.Items.Add(navbt);
        }

        //界面公式取值
        public object GetSelectValue(string sColCode)
        {
            return null;
        }
    }
}
