using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;

namespace ErpCore
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            string sUser = txtUser.Text.Trim();
            string sPwd = txtPwd.Text.Trim();
            if (sUser == "")
            {
                MessageBox.Show("请输入帐号！");
                return;
            }

            CUserMgr userMgr = Program.Ctx.UserMgr;
            
            CUser user = Program.Ctx.UserMgr.FindByName(sUser);
            if (user==null)
            {
                MessageBox.Show("帐号错误！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            if (user.Pwd != sPwd)
            {
                MessageBox.Show("帐号错误！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            Program.User = user;

            DialogResult = DialogResult.OK;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            //btOK_Click(null, null);

        }
    }
}
