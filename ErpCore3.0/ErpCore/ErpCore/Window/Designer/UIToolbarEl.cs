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

namespace ErpCore.Window.Designer
{
    public partial class UIToolbarEl : ToolStrip, IDesignEl
    {
        CWindowControl windowControl = null;

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


        public UIToolbarEl()
        {
            InitializeComponent();

        }

        public UIToolbarEl(IContainer container)
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
                UIToolbarButtonEl tbutton = new UIToolbarButtonEl();
                tbutton.Text = navbt.Name;
                tbutton.SetIcon(navbt.Icon);
                tbutton.TextImageRelation = TextImageRelation.ImageAboveText;
                
                Add(tbutton);
            }
        }

        public void Add(UIToolbarButtonEl navbt)
        {
            this.Items.Add(navbt);
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


    }
}
