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
using ErpCoreModel.UI;
using ErpCore.Window;

namespace ErpCore.CommonCtrl
{
    public partial class ViewTab : UserControl
    {
        string captionText = "";
        bool showTitleBar = false;
        bool showToolBar = false;

        CView view = null;
        CBaseObject parentObject = null;

        public ViewTab()
        {
            InitializeComponent();
        }

        public CView View
        {
            get { return view; }
            set
            {
                view = value;
                LoadTab();
            }
        }
        public CBaseObject ParentObject
        {
            get { return parentObject; }
            set
            {
                parentObject = value;
                LoadTab();
            }
        }
        public string CaptionText
        {
            get { return captionText; }
            set
            {
                captionText = value;
                lbTitle.Text = captionText;
            }
        }

        public bool ShowTitleBar
        {
            get { return showTitleBar; }
            set
            {
                showTitleBar = value;
                tbTitle.Visible = showTitleBar;
            }
        }
        public bool ShowToolBar
        {
            get { return showToolBar; }
            set
            {
                showToolBar = value;
                LoadTab();
            }
        }

        void LoadTab()
        {
            tabControl.TabPages.Clear();
            if (View == null)
                return;
            List<CBaseObject> lstObj = View.ViewDetailMgr.GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CViewDetail vd = (CViewDetail)obj;
                CTable table = (CTable)Program.Ctx.TableMgr.Find(vd.FW_Table_id);
                if (table == null)
                    continue;

                CBaseObjectMgr objMgr = null;
                if (ParentObject != null)
                {
                    objMgr = Program.Ctx.FindBaseObjectMgrCache(table.Code, ParentObject.Id);
                    if(objMgr==null)
                        objMgr = ParentObject.GetSubObjectMgr(table.Code, typeof(CBaseObjectMgr));
                    if (objMgr == null)
                    {
                        CColumn colP = (CColumn)ParentObject.Table.ColumnMgr.Find(vd.PrimaryKey);
                        if (colP == null)
                            continue;
                        CColumn colF = (CColumn)table.ColumnMgr.Find(vd.ForeignKey);
                        if (colF == null)
                            continue;
                        object objVal = ParentObject.GetColValue(colP);
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
                        objMgr.m_Parent = ParentObject;
                        string sWhere = string.Format(" {0}={1}", colF.Code, bIsStr ? sVal : objVal);
                        objMgr.Load(sWhere, false);
                    }
                }

                TabPage page = new TabPage(table.Name);
                page.Tag = vd;
                tabControl.TabPages.Add(page);
                
                ViewDetailGrid vdg = new ViewDetailGrid();
                vdg.ViewDetail = vd;
                vdg.BaseObjectMgr = objMgr;
                vdg.CaptionText = table.Name;
                vdg.ShowTitleBar = ShowTitleBar;
                vdg.ShowToolBar = ShowToolBar;
                vdg.m_bShowWorkflow = false;
                vdg.Tag = View;
                page.Controls.Add(vdg);
                vdg.Dock = DockStyle.Fill;
                vdg.BringToFront();
            }
        }

    }
}
