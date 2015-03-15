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

namespace ErpCore.SubSystem
{
    public partial class SubSystemPanel : UserControl
    {
        public SubSystemPanel()
        {
            InitializeComponent();

            CBaseObjectMgr BaseObjectMgr = new CBaseObjectMgr();
            BaseObjectMgr.TbCode = "S_System";
            BaseObjectMgr.Ctx = Program.Ctx;

            tableCtrl.BaseObjectMgr = BaseObjectMgr;
        }

        private void SubSystemPanel_Load(object sender, EventArgs e)
        {
        }
    }
}
