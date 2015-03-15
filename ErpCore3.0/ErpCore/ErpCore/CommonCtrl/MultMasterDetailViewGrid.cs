using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
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

namespace ErpCore.CommonCtrl
{
    public partial class MultMasterDetailViewGrid : UserControl
    {
        string captionText = "";
        bool showToolBar = false;
        bool showTitleBar = false;

        CView view = null;
        CBaseObjectMgr baseObjectMgr = null;

        public CView View
        {
            get { return view; }
            set
            {
                view = value;
                viewGrid.View = view;
                viewTab.View = view;
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

        public string CaptionText
        {
            get { return captionText; }
            set
            {
                captionText = value;
                viewGrid.CaptionText = captionText;
            }
        }
        public bool ShowToolBar
        {
            get { return showToolBar; }
            set
            {
                showToolBar = value;
                viewGrid.ShowToolBar = showToolBar;
            }
        }
        public bool ShowTitleBar
        {
            get { return showTitleBar; }
            set
            {
                showTitleBar = value;
                viewGrid.ShowTitleBar = showTitleBar;
            }
        }
        public MultMasterDetailViewGrid()
        {
            InitializeComponent();

            viewGrid.dataGridView.CellClick += new DataGridViewCellEventHandler(viewGrid_CellClick);
        }

        void viewGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(View==null)
                return;
            if (viewGrid.dataGridView.CurrentRow == null)
                return;
            CViewDetail vd = (CViewDetail)View.ViewDetailMgr.GetFirstObj();
            CTable table = (CTable)Program.Ctx.TableMgr.Find(vd.FW_Table_id);
            CBaseObject obj = (CBaseObject)viewGrid.dataGridView.CurrentRow.Tag;
            viewTab.View = View;
            viewTab.ParentObject = obj;
        }
    }
}
