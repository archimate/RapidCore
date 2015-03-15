using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
    public partial class AddDesktopApp : Form
    {
        public CDesktopApp m_App = null;
        public Guid UI_DesktopGroup_id = Guid.Empty;

        public AddDesktopApp()
        {
            InitializeComponent();
        }

        private void btBrowse_Click(object sender, EventArgs e)
        {
            SelIconForm frm = new SelIconForm();
            if (frm.ShowDialog() != DialogResult.OK)
                return;

            picIcon.ImageLocation = frm.m_sIconUrl;
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim() == "")
            {
                MessageBox.Show("名称不能空！");
                return;
            }
            if (txtUrl.Text.Trim() == "")
            {
                MessageBox.Show("Url不能空！");
                return;
            }

            CDesktopApp App = new CDesktopApp();
            App.Ctx = Program.Ctx;
            App.Name = txtName.Text.Trim();
            App.Url = txtUrl.Text.Trim();
            App.UI_DesktopGroup_id = UI_DesktopGroup_id;
            if (File.Exists(picIcon.ImageLocation))
            {
                FileInfo fi = new FileInfo(picIcon.ImageLocation);
                App.IconUrl = fi.Name;
            }
            else
                App.IconUrl = "";
            App.B_User_id = Program.User.Id;

            Program.User.DesktopAppMgr.AddNew(App);
            if (!Program.User.DesktopAppMgr.Save(true))
            {
                MessageBox.Show("保存失败！");
                return;
            }

            m_App = App;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
