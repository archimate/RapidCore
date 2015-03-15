using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.UI;

namespace ErpCore.View
{
    public partial class SelViewType : Form
    {
        public enumViewType m_enumViewType = enumViewType.Single;

        public SelViewType()
        {
            InitializeComponent();
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            if (rdSingle.Checked)
                m_enumViewType = enumViewType.Single;
            else if (rdMasterDetail.Checked)
                m_enumViewType = enumViewType.MasterDetail;
            else
                m_enumViewType = enumViewType.MultMasterDetail;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
