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
    public partial class IconItem : UserControl
    {
        string title = "";
        string icon = "";
        public event ClickEventHandler ClickItem;
        
        public IconItem()
        {
            InitializeComponent();
        }

        public string Icon
        {
            get { return icon; }
            set
            {
                icon = value;
                string sPath = Application.StartupPath + "\\MenuIcon\\default.png";
                if (icon != "")
                    sPath = icon;

                if (File.Exists(sPath))
                    picIcon.ImageLocation = sPath;
            }
        }
        

        private void DesktopItem_Load(object sender, EventArgs e)
        {
        }

        private void picIcon_Click(object sender, EventArgs e)
        {
            ClickItem(this, null);
        }


    }
}
