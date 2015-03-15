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
using ErpCore.Window;
using ErpCoreModel.UI;
using ErpCore.CommonCtrl;

namespace ErpCore.Window
{
    public partial class LayoutWindow : Form
    {
        CWindow window = null;

        public LayoutWindow()
        {
            InitializeComponent();
        }

        public CWindow Window
        {
            get { return window; }
            set { window = value; }
        }

        private void LayoutWindow_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        void LoadData()
        {
            if (Window == null)
                return;

            this.Width = Window.Width;
            this.Height = Window.Height;
            panelTop.Height = Window.TopPanelHeight;
            panelBottom.Height = Window.BottomPanelHeight;
            panelLeft.Width = Window.LeftPanelWidth;
            panelRight.Width = Window.RightPanelWidth;
            panelTop.Visible = Window.TopPanelVisible;
            panelBottom.Visible = Window.BottomPanelVisible;
            panelLeft.Visible = Window.LeftPanelVisible;
            panelRight.Visible = Window.RightPanelVisible;

            List<CBaseObject> lstWindowControl = Window.WindowControlMgr.GetList();
            foreach (CBaseObject obj in lstWindowControl)
            {
                CWindowControl WindowControl = (CWindowControl)obj;
                Panel panel = null;
                if (WindowControl.Dock == (int)DockStyle.Top)
                    panel = panelTop;
                else if (WindowControl.Dock == (int)DockStyle.Bottom)
                    panel = panelBottom;
                else if (WindowControl.Dock == (int)DockStyle.Left)
                    panel = panelLeft;
                else if (WindowControl.Dock == (int)DockStyle.Right)
                    panel = panelRight;
                else
                    panel = panelFill;

                if (WindowControl.CtrlType == ControlType.NavBar)
                {
                    UIToolbar toolbar = new UIToolbar();
                    toolbar.WindowControl = WindowControl;
                    toolbar.Name = WindowControl.Name;
                    panel.Controls.Add(toolbar);
                    toolbar.Dock = DockStyle.Fill;
                    toolbar.SendToBack();
                }
                else if (WindowControl.CtrlType == ControlType.TableGrid)
                {
                    if (WindowControl.TableInWindowControlMgr.GetList().Count == 0)
                        continue;
                    CTableInWindowControl TableInWindowControl = (CTableInWindowControl)WindowControl.TableInWindowControlMgr.GetFirstObj();
                    CTable table = (CTable)Program.Ctx.TableMgr.Find(TableInWindowControl.FW_Table_id);
                    if (table == null)
                        continue;

                    CBaseObjectMgr objMgr = new CBaseObjectMgr();
                    objMgr.TbCode = table.Code;
                    objMgr.Ctx = Program.Ctx;
                    TableGrid te = new TableGrid();
                    te.TableInWindowControl = TableInWindowControl;
                    te.BaseObjectMgr = objMgr;
                    te.Name = WindowControl.Name;
                    te.ShowToolBar = TableInWindowControl.ShowToolBar;
                    te.ShowTitleBar = TableInWindowControl.ShowTitleBar;
                    te.CaptionText = WindowControl.Name;
                    te.Tag = WindowControl;
                    panel.Controls.Add(te);
                    te.Dock = (DockStyle)WindowControl.Dock;
                    te.BringToFront();
                    te.dataGridView.CellClick += new DataGridViewCellEventHandler(dataGridView_CellClick);
                    
                }
                else if (WindowControl.CtrlType == ControlType.TableTree)
                {

                    TableTree tt = new TableTree();
                    tt.CaptionText = WindowControl.Name;
                    tt.ShowTitleBar = WindowControl.ShowTitleBar;
                    tt.WindowControl = WindowControl;
                    tt.Name = WindowControl.Name;
                    tt.Tag = WindowControl;
                    panel.Controls.Add(tt);
                    tt.Dock = DockStyle.Fill;
                    tt.BringToFront();

                    tt.treeView.NodeMouseClick += new TreeNodeMouseClickEventHandler(treeView_NodeMouseClick);
                }
                else if (WindowControl.CtrlType == ControlType.TableTab)
                {
                    TableTab tab = new TableTab();
                    tab.WindowControl = WindowControl;
                    tab.CaptionText = WindowControl.Name;
                    tab.Name = WindowControl.Name;
                    tab.ShowTitleBar = WindowControl.ShowTitleBar;
                    tab.Tag = WindowControl;
                    panel.Controls.Add(tab);
                    tab.Dock = DockStyle.Fill;
                    tab.BringToFront();

                }
                else if (WindowControl.CtrlType == ControlType.ComboBox)
                {
                    if (WindowControl.TableInWindowControlMgr.GetList().Count == 0)
                        continue;
                    CTableInWindowControl TableInWindowControl = (CTableInWindowControl)WindowControl.TableInWindowControlMgr.GetFirstObj();
                    CTable table = (CTable)Program.Ctx.TableMgr.Find(TableInWindowControl.FW_Table_id);
                    if (table == null)
                        continue;

                    CBaseObjectMgr objMgr = new CBaseObjectMgr();
                    objMgr.TbCode = table.Code;
                    objMgr.Ctx = Program.Ctx;

                    UIComboBox cb = new UIComboBox();
                    cb.CaptionText = WindowControl.Name + "：";
                    cb.TableInWindowControl = TableInWindowControl;
                    cb.BaseObjectMgr = objMgr;
                    cb.Name = WindowControl.Name;
                    cb.Tag = WindowControl;
                    panel.Controls.Add(cb);
                    cb.Dock = DockStyle.Top;
                    cb.BringToFront();

                    cb.comboBox.SelectedIndexChanged += new EventHandler(comboBox_SelectedIndexChanged);
                }
                else if (WindowControl.CtrlType == ControlType.ListBox)
                {
                    if (WindowControl.TableInWindowControlMgr.GetList().Count == 0)
                        continue;
                    CTableInWindowControl TableInWindowControl = (CTableInWindowControl)WindowControl.TableInWindowControlMgr.GetFirstObj();
                    CTable table = (CTable)Program.Ctx.TableMgr.Find(TableInWindowControl.FW_Table_id);
                    if (table == null)
                        continue;

                    CBaseObjectMgr objMgr = new CBaseObjectMgr();
                    objMgr.TbCode = table.Code;
                    objMgr.Ctx = Program.Ctx;

                    UIListBox listBox = new UIListBox();
                    listBox.CaptionText = WindowControl.Name;
                    listBox.TableInWindowControl = TableInWindowControl;
                    listBox.BaseObjectMgr = objMgr;
                    listBox.Name = WindowControl.Name;
                    listBox.Tag = WindowControl;
                    panel.Controls.Add(listBox);
                    listBox.Dock = DockStyle.Fill;
                    listBox.BringToFront();

                    listBox.listBox.SelectedIndexChanged += new EventHandler(listBox_SelectedIndexChanged);
                }
            }
        }

        void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            UIComboBox cb = (UIComboBox)comboBox.Parent;
            CWindowControl WindowControl = (CWindowControl)cb.Tag;

            RefreshLinkageWindowControl(WindowControl);
        }

        void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox list = (ListBox)sender;
            UIListBox lb = (UIListBox)list.Parent;
            CWindowControl WindowControl = (CWindowControl)lb.Tag;

            RefreshLinkageWindowControl(WindowControl);
        }

        void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeView tree = (TreeView)sender;
            tree.SelectedNode = e.Node;

            TableTree tt = (TableTree)tree.Parent;
            CWindowControl WindowControl = (CWindowControl)tt.Tag;

            RefreshLinkageWindowControl(WindowControl);
        }

        void RefreshLinkageWindowControl(CWindowControl WindowControl)
        {
            List<Control> lstCtrls = new List<Control>();
            foreach (Control ctrl in panelTop.Controls)
                lstCtrls.Add(ctrl);
            foreach (Control ctrl in panelBottom.Controls)
                lstCtrls.Add(ctrl);
            foreach (Control ctrl in panelLeft.Controls)
                lstCtrls.Add(ctrl);
            foreach (Control ctrl in panelRight.Controls)
                lstCtrls.Add(ctrl);
            foreach (Control ctrl in panelFill.Controls)
                lstCtrls.Add(ctrl);

            List<CBaseObject> lstLT = WindowControl.LinkageWindowControlMgr.GetList();
            foreach (CBaseObject obj in lstLT)
            {
                CLinkageWindowControl LinkageWindowControl = (CLinkageWindowControl)obj;
                CWindowControl swc = (CWindowControl)WindowControl.m_ObjectMgr.Find(LinkageWindowControl.SlaveID);
                if (swc != null)
                {
                    foreach (Control ctrl in lstCtrls)
                    {
                        if ((CWindowControl)(ctrl.Tag) == swc)
                        {
                            IWindowCtrl winCtrl = (IWindowCtrl)ctrl;
                            if (winCtrl.GetCtrlType() == ControlType.TableGrid)
                            {
                                TableGrid teLT = (TableGrid)ctrl;

                                CTableInWindowControl tiw = teLT.TableInWindowControl;
                                string sQueryFilter = tiw.QueryFilter;
                                if (CalcUIFormula(ref sQueryFilter))
                                {
                                    teLT.BaseObjectMgr.GetList(sQueryFilter);
                                    teLT.LoadData();
                                }
                            }
                            else if (winCtrl.GetCtrlType() == ControlType.TableTab)
                            {
                                TableTab tabLT = (TableTab)ctrl;
                                foreach (TabPage page in tabLT.tabControl.TabPages)
                                {
                                    TableGrid teLT = (TableGrid)page.Controls[0];
                                    CTableInWindowControl tiw = teLT.TableInWindowControl;
                                    string sQueryFilter = tiw.QueryFilter;
                                    if (CalcUIFormula(ref sQueryFilter))
                                    {
                                        teLT.BaseObjectMgr.GetList(sQueryFilter);
                                        teLT.LoadData();
                                    }
                                }
                            }
                            else if (winCtrl.GetCtrlType() == ControlType.ListBox)
                            {
                                UIListBox listBoxLT = (UIListBox)ctrl;

                                CTableInWindowControl tiw = listBoxLT.TableInWindowControl;
                                string sQueryFilter = tiw.QueryFilter;
                                if (CalcUIFormula(ref sQueryFilter))
                                {
                                    listBoxLT.BaseObjectMgr.GetList(sQueryFilter);
                                    listBoxLT.LoadData();
                                }
                            }
                            else if (winCtrl.GetCtrlType() == ControlType.ComboBox)
                            {
                                UIComboBox combBoxLT = (UIComboBox)ctrl;

                                CTableInWindowControl tiw = combBoxLT.TableInWindowControl;
                                string sQueryFilter = tiw.QueryFilter;
                                if (CalcUIFormula(ref sQueryFilter))
                                {
                                    combBoxLT.BaseObjectMgr.GetList(sQueryFilter);
                                    combBoxLT.LoadData();
                                }
                            }
                        }
                    }
                }
            }
        }

        void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            DataGridView dv = (DataGridView)sender;
            TableGrid te = (TableGrid)dv.Parent;
            CWindowControl WindowControl = (CWindowControl)te.Tag;

            RefreshLinkageWindowControl(WindowControl);
        }

        //计算界面公式: UIValue('控件名','字段名')
        bool CalcUIFormula(ref string sQueryFilter)
        {
            SortedList<string, Control> sortCtrls = new SortedList<string, Control>();
            foreach (Control ctrl in panelTop.Controls)
                sortCtrls.Add(ctrl.Name, ctrl);
            foreach (Control ctrl in panelBottom.Controls)
                sortCtrls.Add(ctrl.Name, ctrl);
            foreach (Control ctrl in panelLeft.Controls)
                sortCtrls.Add(ctrl.Name, ctrl);
            foreach (Control ctrl in panelRight.Controls)
                sortCtrls.Add(ctrl.Name, ctrl);
            foreach (Control ctrl in panelFill.Controls)
                sortCtrls.Add(ctrl.Name, ctrl);


            sQueryFilter = sQueryFilter.ToLower();
            int idx = sQueryFilter.IndexOf("uivalue");
            while (idx > -1)
            {
                int idx2 = idx + "uivalue".Length;
                while (sQueryFilter.Length > idx2+1 && sQueryFilter[idx2 + 1] == ' ')
                    idx2++;
                if (sQueryFilter[idx2 ] == '(')
                {
                    idx2++;
                    int idx3 = sQueryFilter.IndexOf(')', idx2);
                    if (idx3 > idx2)
                    {
                        string sPara = sQueryFilter.Substring(idx2, idx3 - idx2);
                        string[] arr = sPara.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        if (arr.Length == 2)
                        {
                            string sCtrlName = arr[0].Trim("'".ToCharArray());
                            string sColCode = arr[1].Trim("'".ToCharArray());
                            if (!sortCtrls.ContainsKey(sCtrlName))
                            {
                                string sMsg = string.Format("过滤表达式错误：控件 {0} 不存在！", sCtrlName);
                                MessageBox.Show(sMsg, "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                return false;
                            }
                            Control ctrl = sortCtrls[sCtrlName];
                            IWindowCtrl winCtrl = (IWindowCtrl)ctrl;


                            object objVal = winCtrl.GetSelectValue(sColCode);
                            string sVal = (objVal != null) ? objVal.ToString() : "";
                            sQueryFilter = sQueryFilter.Substring(0, idx) + sVal + sQueryFilter.Substring(idx3 + 1);
                            
                        }
                    }
                }
                idx = sQueryFilter.IndexOf("uivalue", idx + 1);
            }
            return true;
        }
    }
}
