using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.Workflow;
using ErpCoreModel.UI;
using ErpCore.Window;
using ErpCore.Workflow;

namespace ErpCore.View
{
    public partial class MultMasterDetailView : Form
    {
        CView view = null;
        CBaseObjectMgr baseObjectMgr = null;

        public CView View
        {
            get { return view; }
            set
            {
                view = value;
                viewGrid.View = view;
            }
        }
        public CBaseObjectMgr BaseObjectMgr
        {
            get { return baseObjectMgr; }
            set
            {
                baseObjectMgr = value;
                viewGrid.BaseObjectMgr = baseObjectMgr;
            }
        }
        public MultMasterDetailView()
        {
            InitializeComponent();
        }

        private void MultMasterDetailView_Load(object sender, EventArgs e)
        {
            viewGrid.View = View;
            viewGrid.BaseObjectMgr = BaseObjectMgr;
            this.Text = View.Name;
        }
    }
}

