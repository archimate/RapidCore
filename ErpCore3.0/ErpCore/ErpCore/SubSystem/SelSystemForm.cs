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
using ErpCore.Database.Diagram;


namespace ErpCore.SubSystem
{
    public partial class SelSystemForm : Form
    {
        public SelSystemForm()
        {
            InitializeComponent();
        }

        private void SelSystemForm_Load(object sender, EventArgs e)
        {
            string sPath = Application.StartupPath + "\\config.cfg";
            if (!File.Exists(sPath))
            {
                DbSetup frmDb = new DbSetup();
                if (frmDb.ShowDialog() != DialogResult.OK)
                {
                    Application.Exit();
                    return;
                }
            }
            string sConnectionString =ErpCore.Util.DESEncrypt.DesDecrypt( File.ReadAllText(sPath));
            Program.Ctx.ConnectionString = sConnectionString;

            LoginForm frm = new LoginForm();
            if (frm.ShowDialog() != DialogResult.OK)
            {
                Application.Exit();
                return;
            }
            LoadList();
        }
        void LoadList()
        {
            this.flowLayoutPanel.Controls.Clear();

            if (Program.User.Name.Equals("admin"))
            {
                SystemBox adminBox = new SystemBox();
                adminBox.Title = "系统管理";
                this.flowLayoutPanel.Controls.Add(adminBox);
            }

            List<CBaseObject> lstSystem = Program.Ctx.SystemMgr.GetList();
            foreach (CBaseObject obj in lstSystem)
            {
                CSystem system = (CSystem)obj;

                SystemBox sysBox = new SystemBox();
                sysBox.Title = system.Name;
                sysBox.SetValue(system.Icon);
                sysBox.m_System = system;
                this.flowLayoutPanel.Controls.Add(sysBox);
            }
        }
    }
}
