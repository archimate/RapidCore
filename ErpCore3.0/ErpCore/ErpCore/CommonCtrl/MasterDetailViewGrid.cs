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
    public partial class MasterDetailViewGrid : UserControl
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
                if (view != null)
                {
                    CViewDetail vd = (CViewDetail)view.ViewDetailMgr.GetFirstObj();
                    if (vd != null)
                        viewDetailGrid.ViewDetail = vd;
                }
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
        public MasterDetailViewGrid()
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
            viewDetailGrid.ViewDetail = vd;
            CTable table = (CTable)Program.Ctx.TableMgr.Find(vd.FW_Table_id);
            if (table == null)
                return;
            CBaseObject obj = (CBaseObject)viewGrid.dataGridView.CurrentRow.Tag;
            CBaseObjectMgr objMgr = Program.Ctx.FindBaseObjectMgrCache(table.Code, obj.Id);
            if(objMgr==null)
                objMgr = obj.GetSubObjectMgr(table.Code,typeof(CBaseObjectMgr));
            if (objMgr == null)
            {
                CColumn colP = (CColumn)obj.Table.ColumnMgr.Find(vd.PrimaryKey);
                if (colP == null)
                    return;
                CColumn colF = (CColumn)table.ColumnMgr.Find(vd.ForeignKey);
                if (colF == null)
                    return;
                object objVal = obj.GetColValue(colP);
                string sVal = objVal.ToString();
                bool bIsStr = false;
                if (colP.ColType == ColumnType.string_type
                    || colP.ColType == ColumnType.text_type
                    || colP.ColType == ColumnType.ref_type
                    || colP.ColType == ColumnType.guid_type
                    || colP.ColType == ColumnType.datetime_type)
                {
                    sVal = string.Format("'{0}'", sVal);
                    bIsStr = true;
                }

                objMgr = new CBaseObjectMgr();
                objMgr.Ctx = Program.Ctx;
                objMgr.TbCode = table.Code;
                objMgr.m_Parent = obj;
                string sWhere = string.Format(" {0}={1}", colF.Code, bIsStr ? sVal : objVal);
                objMgr.Load(sWhere, false);
            }
            viewDetailGrid.BaseObjectMgr = objMgr;
        }
    }
}
