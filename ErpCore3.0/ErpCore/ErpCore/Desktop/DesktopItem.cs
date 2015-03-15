using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;

using System.Text;
using System.Windows.Forms;
using System.IO;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.SubSystem;
using ErpCoreModel.UI;
using ErpCore.Window;

namespace ErpCore.Desktop
{
    public enum enumDesktopItemType { CatalogMenu, ViewMenu, WindowMenu, UrlMenu,ReportMenu, DesktopApp, Admin, AddDesktopApp }
    public delegate void ClickEventHandler(object sender, EventArgs e);
    public partial class DesktopItem : UserControl
    {
        string title = "";
        string icon = "";

        public enumDesktopItemType ItemType = enumDesktopItemType.CatalogMenu;
        public CBaseObject m_BaseObject = null;

        public event ClickEventHandler ClickClose;
        public event ClickEventHandler ClickItem;

        public DesktopItem()
        {
            InitializeComponent();
        }

        public string Title
        {
            get { return title; }
            set { 
                title = value;
                lbTitle.Text = title;
            }
        }
        public string Icon
        {
            get { return icon; }
            set { 
                icon = value; 
                string sPath = Application.StartupPath + "\\MenuIcon\\default.png";
                if (icon != "")
                    sPath = Application.StartupPath + "\\MenuIcon\\" + icon;

                if(File.Exists(sPath))
                    picIcon.ImageLocation = sPath;
            }
        }


        private void DesktopItem_Load(object sender, EventArgs e)
        {

        }


        private void picIcon_Click(object sender, EventArgs e)
        {
            if (picClose.Visible == true)
            {
                Point ptClick = picClose.PointToClient(MousePosition);
                if (ptClick.X > 0 && ptClick.X < picClose.Width
                    && ptClick.Y > 0 && ptClick.Y < picClose.Height)
                {
                    ClickClose(this, null);
                    return;
                }
            }

            ClickItem(this, null);
            
        }

        private void lbTitle_Click(object sender, EventArgs e)
        {
            ClickItem(this, null);
        }

        private void picIcon_MouseEnter(object sender, EventArgs e)
        {
            if(ItemType== enumDesktopItemType.DesktopApp)
                picClose.Visible = true;
            this.BorderStyle = BorderStyle.FixedSingle;
        }

        private void picIcon_MouseLeave(object sender, EventArgs e)
        {
            picClose.Visible = false;
            this.BorderStyle = BorderStyle.None;
        }


    }
}
