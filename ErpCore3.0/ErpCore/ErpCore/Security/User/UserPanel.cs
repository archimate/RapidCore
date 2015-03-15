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

namespace ErpCore.Security.User
{
    public partial class UserPanel : UserControl
    {
        CCompany company = null;

        public UserPanel()
        {
            InitializeComponent();

        }
        public CCompany Company
        {
            get { return company; }
            set
            {
                company = value;

                CBaseObjectMgr BaseObjectMgr = new CBaseObjectMgr();
                BaseObjectMgr.TbCode = "B_User";
                BaseObjectMgr.Ctx = Program.Ctx;
                string sWhere = string.Format("B_Company_id='{0}'", company.Id);
                BaseObjectMgr.Load(sWhere);

                tableCtrl.BaseObjectMgr = BaseObjectMgr;
            }
        }

        private void UserPanel_Load(object sender, EventArgs e)
        {
        }
    }
}
