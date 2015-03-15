using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using System.Collections;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.UI;
using ErpCore.Window;
using ErpCore.Database.Table;

namespace ErpCore.Window.Designer
{
    public partial class ChildenWindow : Form
    {
        CWindow window = null;

        public ControlToolWindow m_ControlToolWindow = null;
        public AttributeToolWindow m_AttributeToolWindow = null;

        public Control m_CurSelDesignEl = null;


        private UISizeDot[] _UISizeDot;
        private const int DOT_WIDTH = 7;   //UISizeDot宽度 
        private const int DOT_HEIGHT = 7;  //UISizeDot高度 
        private const int DOT_COUNT = 8;   //要显示的UISizeDot数 

        public ChildenWindow()
        {
            InitializeComponent();


            this._UISizeDot = new UISizeDot[DOT_COUNT];
            for (int i = 0; i < DOT_COUNT; i++)
            {
                this._UISizeDot[i] = new UISizeDot();
                this._UISizeDot[i].Width = DOT_WIDTH;
                this._UISizeDot[i].Height = DOT_HEIGHT;
                this._UISizeDot[i].Visible = false;
                this.Controls.Add(this._UISizeDot[i]);
            }
        }

        public CWindow Window
        {
            get { return window; }
            set { window = value; }
        }

        public void OnDragDrop(DockStyle dock, ListViewItem item)
        {
            Panel panel = panelFill;
            if (dock == DockStyle.Top)
                panel = panelTop;
            else if (dock == DockStyle.Bottom)
                panel = panelBottom;
            else if (dock == DockStyle.Left)
                panel = panelLeft;
            else if (dock == DockStyle.Right)
                panel = panelRight;

            if (item.Text.Equals("ToolBar", StringComparison.OrdinalIgnoreCase))
            {
                string sCtrlName = GetDistinctName("导航栏", Window.WindowControlMgr);

                CWindowControl WindowControl = new CWindowControl();
                WindowControl.CtrlType = ControlType.NavBar;
                WindowControl.Ctx = Program.Ctx;
                WindowControl.Name = sCtrlName;
                WindowControl.Dock = (int)dock;
                WindowControl.UI_Window_id = Window.Id;
                Window.WindowControlMgr.AddNew(WindowControl);

                NavigateBarEl nb = new NavigateBarEl();
                nb.WindowControl = WindowControl;
                nb.Tag = WindowControl;
                panel.Controls.Add(nb);
                nb.Dock = DockStyle.Fill;
                nb.BringToFront();

                nb.flowLayoutPanel.MouseClick += new MouseEventHandler(childCtrl_MouseClick);

                SelectElement(nb);
            }
            else if (item.Text.Equals("DataGrid", StringComparison.OrdinalIgnoreCase))
            {
                SelTableForm frm = new SelTableForm();
                if (frm.ShowDialog() != DialogResult.OK)
                    return;

                string sCtrlName = GetDistinctName(frm.m_SelTable.Name, Window.WindowControlMgr);

                CWindowControl WindowControl = new CWindowControl();
                WindowControl.CtrlType = ControlType.TableGrid;
                WindowControl.Ctx = Program.Ctx;
                WindowControl.Name = sCtrlName;
                WindowControl.Dock = (int)dock;
                WindowControl.UI_Window_id = Window.Id;
                Window.WindowControlMgr.AddNew(WindowControl) ;

                CTableInWindowControl tiwc = new CTableInWindowControl();
                tiwc.Ctx = Program.Ctx;
                tiwc.FW_Table_id = frm.m_SelTable.Id;
                tiwc.UI_WindowControl_id = WindowControl.Id;
                tiwc.Text = frm.m_SelTable.Name;
                WindowControl.TableInWindowControlMgr.AddNew(tiwc);

                TableGridEl te = new TableGridEl();
                te.WindowControl = WindowControl;
                te.CaptionText = sCtrlName;
                te.ShowTitleBar = true;
                te.ShowToolBar = true;
                te.Tag = WindowControl;
                panel.Controls.Add(te);
                te.Dock = DockStyle.Fill;
                te.BringToFront();


                //联动表
                ListViewItem itemLT = new ListViewItem();
                itemLT.Text = te.CaptionText;
                itemLT.Tag = WindowControl;
                m_AttributeToolWindow.listLinkageWindowControl.Items.Add(itemLT);
                //

                //List<CBaseObject> lstColumn = table.ColumnMgr.GetList();
                //foreach (CBaseObject obj in lstColumn)
                //{
                //    CColumn col = (CColumn)obj;
                //    CColumnInWindowControl ciw = new CColumnInWindowControl();
                //    ciw.FW_Column_id = col.Id;
                //    ciw.UI_TableInWindow_id = TableInWindow.Id;
                //    if (!TableInWindow.ColumnInWindowMgr.AddNew(ciw))
                //    {
                //        MessageBox.Show("设置显示字段失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                //        //return false;
                //    }
                //    te.m_lstShowColumn.Add(ciw.FW_Column_id);
                //}
                //foreach (ToolStripItem tbutton in te.tableCtrlEl.toolStrip.Items)
                //{
                //    CToolbarButtonInWindowControl tbiw = new CToolbarButtonInWindowControl();
                //    tbiw.Title = tbutton.Text;
                //    tbiw.UI_TableInWindow_id = TableInWindow.Id;
                //    if (!TableInWindow.ToolbarButtonInWindowMgr.AddNew(tbiw))
                //    {
                //        MessageBox.Show("设置工具栏按钮失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                //        //return false;
                //    }
                //    te.m_lstShowToolBarButton.Add(tbiw.Title);
                //}

                te.tbTitle.MouseClick += new MouseEventHandler(childCtrl_MouseClick);
                
                SelectElement(te);
            }
            else if (item.Text.Equals("TreeCtrl", StringComparison.OrdinalIgnoreCase))
            {
                TableTreeSet frm = new TableTreeSet();
                if (frm.ShowDialog() != DialogResult.OK)
                    return;

                string sCtrlName = GetDistinctName(frm.m_WindowControl.Name, Window.WindowControlMgr);

                CWindowControl WindowControl = frm.m_WindowControl;
                WindowControl.Name = sCtrlName;
                WindowControl.CtrlType = ControlType.TableTree;
                WindowControl.Ctx = Program.Ctx;
                WindowControl.Dock = (int)dock;
                WindowControl.UI_Window_id = Window.Id;
                Window.WindowControlMgr.AddNew(WindowControl);
                

                TableTreeEl tt = new TableTreeEl();
                tt.CaptionText = sCtrlName;
                tt.ShowTitleBar = true;
                tt.Tag = WindowControl;
                panel.Controls.Add(tt);
                tt.Dock = DockStyle.Fill;
                tt.BringToFront();

                tt.tbTitle.MouseClick += new MouseEventHandler(childCtrl_MouseClick);

                SelectElement(tt);
            }
            else if (item.Text.Equals("TabCtrl", StringComparison.OrdinalIgnoreCase))
            {
                SelMultTableForm frm = new SelMultTableForm();
                if (frm.ShowDialog() != DialogResult.OK)
                    return;

                string sCtrlName = GetDistinctName("标签", Window.WindowControlMgr);

                CWindowControl WindowControl = new CWindowControl();
                WindowControl.CtrlType = ControlType.TableTab;
                WindowControl.Ctx = Program.Ctx;
                WindowControl.Name = sCtrlName;
                WindowControl.Dock = (int)dock;
                WindowControl.UI_Window_id = Window.Id;
                Window.WindowControlMgr.AddNew(WindowControl);

                foreach (CTable table in frm.m_lstSelTable)
                {
                    CTableInWindowControl tiwc = new CTableInWindowControl();
                    tiwc.Ctx = Program.Ctx;
                    tiwc.FW_Table_id = table.Id;
                    tiwc.UI_WindowControl_id = WindowControl.Id;
                    tiwc.Text = table.Name;
                    WindowControl.TableInWindowControlMgr.AddNew(tiwc);
                }

                TableTabEl tab = new TableTabEl();
                tab.WindowControl = WindowControl;
                tab.CaptionText = sCtrlName;
                tab.ShowTitleBar = false;
                tab.Tag = WindowControl;
                panel.Controls.Add(tab);
                tab.Dock = DockStyle.Fill;
                tab.BringToFront();

                tab.tabControl.MouseClick += new MouseEventHandler(childCtrl_MouseClick);
                SelectElement(tab);    
            }
            else if (item.Text.Equals("ComboBox", StringComparison.OrdinalIgnoreCase))
            {
                SelTableForm frm = new SelTableForm();
                if (frm.ShowDialog() != DialogResult.OK)
                    return;
                SelColumnForm frm2 = new SelColumnForm();
                frm2.m_Table = frm.m_SelTable;
                if (frm2.ShowDialog() != DialogResult.OK)
                    return;

                string sCtrlName = GetDistinctName(frm.m_SelTable.Name, Window.WindowControlMgr);

                CWindowControl WindowControl = new CWindowControl();
                WindowControl.CtrlType = ControlType.ComboBox;
                WindowControl.Ctx = Program.Ctx;
                WindowControl.Name = sCtrlName;
                WindowControl.Dock = (int)dock;
                WindowControl.UI_Window_id = Window.Id;
                Window.WindowControlMgr.AddNew(WindowControl);


                string sText = string.Format("[{0}]", frm2.m_SelColumn.Code);
                
                CTableInWindowControl tiwc = new CTableInWindowControl();
                tiwc.Ctx = Program.Ctx;
                tiwc.FW_Table_id = frm.m_SelTable.Id;
                tiwc.UI_WindowControl_id = WindowControl.Id;
                tiwc.Text = sText;
                WindowControl.TableInWindowControlMgr.AddNew(tiwc);

                UIComboBoxEl cb = new UIComboBoxEl();
                cb.CaptionText = frm.m_SelTable.Name+"：";
                cb.WindowControl = WindowControl;
                cb.Tag = WindowControl;
                panel.Controls.Add(cb);
                cb.Dock = DockStyle.Top;
                cb.BringToFront();

                cb.lbTitle.MouseClick += new MouseEventHandler(childCtrl_MouseClick);
                cb.comboBox.MouseClick += new MouseEventHandler(childCtrl_MouseClick);

                SelectElement(cb);
            }
            else if (item.Text.Equals("ListBox", StringComparison.OrdinalIgnoreCase))
            {

                SelTableForm frm = new SelTableForm();
                if (frm.ShowDialog() != DialogResult.OK)
                    return;
                SelColumnForm frm2 = new SelColumnForm();
                frm2.m_Table = frm.m_SelTable;
                if (frm2.ShowDialog() != DialogResult.OK)
                    return;

                string sCtrlName = GetDistinctName(frm.m_SelTable.Name, Window.WindowControlMgr);

                CWindowControl WindowControl = new CWindowControl();
                WindowControl.CtrlType = ControlType.ListBox;
                WindowControl.Ctx = Program.Ctx;
                WindowControl.Name = sCtrlName;
                WindowControl.Dock = (int)dock;
                WindowControl.UI_Window_id = Window.Id;
                Window.WindowControlMgr.AddNew(WindowControl);


                string sText = string.Format("[{0}]",frm2.m_SelColumn.Code);
                //string sText = "";
                //List<CBaseObject> lstObj = frm.m_SelTable.ColumnMgr.GetList();
                //bool bHasName = false, bHasCode = false;
                //foreach (CBaseObject obj in lstObj)
                //{
                //    CColumn column = (CColumn)obj;
                //    if (column.Code.Equals("name", StringComparison.OrdinalIgnoreCase))
                //    {
                //        bHasName = true;
                //    }
                //    if (column.Code.Equals("code", StringComparison.OrdinalIgnoreCase))
                //    {
                //        bHasCode = true;
                //    }
                //}
                //if (bHasName)
                //    sText = "[Name]";
                //else if (bHasCode)
                //    sText = "[Code]";

                CTableInWindowControl tiwc = new CTableInWindowControl();
                tiwc.Ctx = Program.Ctx;
                tiwc.FW_Table_id = frm.m_SelTable.Id;
                tiwc.UI_WindowControl_id = WindowControl.Id;
                tiwc.Text = sText;
                WindowControl.TableInWindowControlMgr.AddNew(tiwc);

                UIListBoxEl listBox = new UIListBoxEl();
                listBox.CaptionText = sCtrlName;
                listBox.WindowControl = WindowControl;
                listBox.Tag = WindowControl;
                panel.Controls.Add(listBox);
                listBox.Dock = DockStyle.Fill;
                listBox.BringToFront();

                listBox.tbTitle.MouseClick += new MouseEventHandler(childCtrl_MouseClick);

                SelectElement(listBox);
            }
            
        }
        public static string GetDistinctName(string sName, CWindowControlMgr WindowControlMgr)
        {
            int idx = 2;
            string sRetName = sName;
            List<CBaseObject> lstObj = WindowControlMgr.GetList();
            while (true)
            {
                bool bHas = false;
                foreach (CBaseObject obj in lstObj)
                {
                    CWindowControl WindowControl = (CWindowControl)obj;
                    if (WindowControl.Name.Equals(sRetName, StringComparison.OrdinalIgnoreCase))
                    {
                        bHas = true;
                        break;
                    }
                }
                if (!bHas)
                    return sRetName;
                else
                    sRetName = string.Format("{0}{1}", sName,idx);
                idx++;
            }
        }

        private void ChildenWindow_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ListViewItem)))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }
        void SelectElement(Control ctrlEl)
        {
            m_CurSelDesignEl = ctrlEl;
            IDesignEl designEl =(IDesignEl)m_CurSelDesignEl;

            ShowFocusDot(ctrlEl);

            if (m_CurSelDesignEl == null)
                return;

            CWindowControl WindowControl = (CWindowControl)m_CurSelDesignEl.Tag;
            if (WindowControl.m_objTempData == null)
            {
                TableGridProp Setting = new TableGridProp();
                Setting.宽度 = m_CurSelDesignEl.Width;
                Setting.高度 = m_CurSelDesignEl.Height;
                Setting.停靠 = m_CurSelDesignEl.Dock;
                if (designEl.GetCtrlType() == ControlType.TableGrid)
                {
                    TableGridEl te = (TableGridEl)designEl;

                    Setting.工具栏显示 = te.TableInWindowControl.ShowToolBar;
                    Setting.标题栏显示 = te.TableInWindowControl.ShowTitleBar;
                }
                else if (designEl.GetCtrlType() == ControlType.TableTree)
                {
                }
                WindowControl.m_objTempData = Setting;
            }
            m_AttributeToolWindow.ControlEl = m_CurSelDesignEl;
        }
        public void ShowFocusDot(Control ctrlEl)
        {
            if (ctrlEl == null)
            {
                for (int i = 0; i < this._UISizeDot.Length; i++)
                {
                    this._UISizeDot[i].Visible = false;
                }
                return;
            }
            
            Rectangle rc = new Rectangle(ctrlEl.Left, ctrlEl.Top, ctrlEl.Width, ctrlEl.Height);
            rc = this.RectangleToClient(ctrlEl.RectangleToScreen(rc));

            _UISizeDot[0].Left = rc.Left;
            _UISizeDot[0].Top = rc.Top;
            _UISizeDot[0].Width = DOT_WIDTH;
            _UISizeDot[0].Height = DOT_HEIGHT;
            _UISizeDot[0].Visible = true;
            _UISizeDot[0].BringToFront();

            _UISizeDot[1].Left = rc.Left + rc.Width / 2;
            _UISizeDot[1].Top = rc.Top;
            _UISizeDot[1].Width = DOT_WIDTH;
            _UISizeDot[1].Height = DOT_HEIGHT;
            _UISizeDot[1].Visible = true;
            _UISizeDot[1].BringToFront();

            _UISizeDot[2].Left = rc.Right - DOT_WIDTH;
            _UISizeDot[2].Top = rc.Top;
            _UISizeDot[2].Width = DOT_WIDTH;
            _UISizeDot[2].Height = DOT_HEIGHT;
            _UISizeDot[2].Visible = true;
            _UISizeDot[2].BringToFront();

            _UISizeDot[3].Left = rc.Right - DOT_WIDTH;
            _UISizeDot[3].Top = rc.Top + rc.Height / 2;
            _UISizeDot[3].Width = DOT_WIDTH;
            _UISizeDot[3].Height = DOT_HEIGHT;
            _UISizeDot[3].Visible = true;
            _UISizeDot[3].BringToFront();

            _UISizeDot[4].Left = rc.Right - DOT_WIDTH;
            _UISizeDot[4].Top = rc.Bottom - DOT_HEIGHT;
            _UISizeDot[4].Width = DOT_WIDTH;
            _UISizeDot[4].Height = DOT_HEIGHT;
            _UISizeDot[4].Visible = true;
            _UISizeDot[4].BringToFront();

            _UISizeDot[5].Left = rc.Left + rc.Width / 2;
            _UISizeDot[5].Top = rc.Bottom - DOT_HEIGHT;
            _UISizeDot[5].Width = DOT_WIDTH;
            _UISizeDot[5].Height = DOT_HEIGHT;
            _UISizeDot[5].Visible = true;
            _UISizeDot[5].BringToFront();

            _UISizeDot[6].Left = rc.Left;
            _UISizeDot[6].Top = rc.Bottom - DOT_HEIGHT;
            _UISizeDot[6].Width = DOT_WIDTH;
            _UISizeDot[6].Height = DOT_HEIGHT;
            _UISizeDot[6].Visible = true;
            _UISizeDot[6].BringToFront();

            _UISizeDot[7].Left = rc.Left;
            _UISizeDot[7].Top = rc.Top + rc.Height / 2;
            _UISizeDot[7].Width = DOT_WIDTH;
            _UISizeDot[7].Height = DOT_HEIGHT;
            _UISizeDot[7].Visible = true;
            _UISizeDot[7].BringToFront();
        }
        private void ChildenWindow_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        void LoadData()
        {
            //this.Controls.Clear();
            m_AttributeToolWindow.listLinkageWindowControl.Items.Clear();
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


                //联动控件
                ListViewItem itemLT = new ListViewItem();
                itemLT.Text = WindowControl.Name;
                itemLT.Tag = WindowControl;
                m_AttributeToolWindow.listLinkageWindowControl.Items.Add(itemLT);
                //

                if (WindowControl.CtrlType == ControlType.NavBar)
                {
                    UIToolbarEl toolbar = new UIToolbarEl();
                    toolbar.WindowControl = WindowControl;
                    toolbar.Tag = WindowControl;
                    panel.Controls.Add(toolbar);
                    toolbar.Dock = DockStyle.Fill;

                    toolbar.MouseClick += new MouseEventHandler(ctrl_MouseClick);
                }
                else if (WindowControl.CtrlType == ControlType.TableGrid)
                {
                    if (WindowControl.TableInWindowControlMgr.GetList().Count == 0)
                        continue;
                    CTableInWindowControl TableInWindowControl = (CTableInWindowControl)WindowControl.TableInWindowControlMgr.GetFirstObj();
                    CTable table = (CTable)Program.Ctx.TableMgr.Find(TableInWindowControl.FW_Table_id);
                    if (table == null)
                        continue;

                    TableGridEl te = new TableGridEl();
                    te.WindowControl = WindowControl;
                    te.Name = table.Code;
                    te.TableInWindowControl = TableInWindowControl;
                    te.ShowToolBar = TableInWindowControl.ShowToolBar;
                    te.ShowTitleBar = TableInWindowControl.ShowTitleBar;
                    te.CaptionText = WindowControl.Name;
                    te.Tag = WindowControl;
                    panel.Controls.Add(te);
                    te.Dock = (DockStyle)WindowControl.Dock;
                    te.BringToFront();
                    


                    te.tbTitle.MouseClick += new MouseEventHandler(childCtrl_MouseClick);
                    te.toolStrip.MouseClick += new MouseEventHandler(childCtrl_MouseClick);
                    te.dataGridView.GotFocus += new EventHandler(ctrl_GotFocus);
                }
                else if (WindowControl.CtrlType == ControlType.TableTree)
                {

                    TableTreeEl tt = new TableTreeEl();
                    tt.CaptionText = WindowControl.Name;
                    tt.ShowTitleBar = WindowControl.ShowTitleBar;
                    tt.WindowControl = WindowControl;
                    tt.Tag = WindowControl;
                    panel.Controls.Add(tt);
                    tt.Dock = DockStyle.Fill;
                    tt.BringToFront();

                    tt.tbTitle.MouseClick += new MouseEventHandler(childCtrl_MouseClick);
                    tt.treeView.GotFocus += new EventHandler(ctrl_GotFocus);
                    tt.treeView.ContextMenuStrip = contextMenuStrip1;
                }
                else if (WindowControl.CtrlType == ControlType.TableTab)
                {
                    TableTabEl tab = new TableTabEl();
                    tab.WindowControl = WindowControl;
                    tab.CaptionText = "TabCtrl";
                    tab.ShowTitleBar = WindowControl.ShowTitleBar;
                    tab.Tag = WindowControl;
                    panel.Controls.Add(tab);
                    tab.Dock = DockStyle.Fill;
                    tab.BringToFront();

                    tab.tabControl.MouseClick += new MouseEventHandler(childCtrl_MouseClick);
                }
                else if (WindowControl.CtrlType == ControlType.ComboBox)
                {
                    UIComboBoxEl cb = new UIComboBoxEl();
                    cb.CaptionText = WindowControl.Name + "：";
                    cb.WindowControl = WindowControl;
                    cb.ShowTitleBar = WindowControl.ShowTitleBar;
                    cb.Tag = WindowControl;
                    panel.Controls.Add(cb);
                    cb.Dock = DockStyle.Top;
                    cb.BringToFront();

                    cb.lbTitle.MouseClick += new MouseEventHandler(childCtrl_MouseClick);
                    cb.comboBox.MouseClick += new MouseEventHandler(childCtrl_MouseClick);
                }
                else if (WindowControl.CtrlType == ControlType.ListBox)
                {

                    UIListBoxEl listBox = new UIListBoxEl();
                    listBox.CaptionText = WindowControl.Name;
                    listBox.WindowControl = WindowControl;
                    listBox.ShowTitleBar = WindowControl.ShowTitleBar;
                    listBox.Tag = WindowControl;
                    panel.Controls.Add(listBox);
                    listBox.Dock = DockStyle.Fill;
                    listBox.BringToFront();

                    listBox.tbTitle.MouseClick += new MouseEventHandler(childCtrl_MouseClick);
                    listBox.listBox.MouseClick += new MouseEventHandler(childCtrl_MouseClick);
                    listBox.ContextMenuStrip = contextMenuStrip1;
                }
            }


        }


        void ctrl_GotFocus(object sender, EventArgs e)
        {
            Control ctrl = (Control)sender;
            SelectElement(ctrl.Parent);
        }


        void ctrl_MouseClick(object sender, MouseEventArgs e)
        {
            Control ctrl = (Control)sender;
            SelectElement(ctrl);

            if (e.Button == MouseButtons.Right)
            {
                Point pt = this.PointToClient(ctrl.PointToScreen(e.Location));
                contextMenuStrip1.Show(pt);
            }
        }

        void childCtrl_MouseClick(object sender, MouseEventArgs e)
        {
            Control ctrl = (Control)sender;
            SelectElement(ctrl.Parent);

            if (e.Button == MouseButtons.Right)
            {
                Point pt = this.PointToClient(ctrl.PointToScreen(e.Location));
                contextMenuStrip1.Show(pt);
            }
        }


        public bool Save()
        {
            Window.TopPanelHeight = panelTop.Height;
            Window.BottomPanelHeight = panelBottom.Height;
            Window.LeftPanelWidth = panelLeft.Width;
            Window.RightPanelWidth = panelRight.Width;
            Window.TopPanelVisible = panelTop.Visible;
            Window.BottomPanelVisible = panelBottom.Visible;
            Window.LeftPanelVisible = panelLeft.Visible;
            Window.RightPanelVisible = panelRight.Visible;
            Window.Width = this.Width;
            Window.Height = this.Height;
            Window.m_CmdType = CmdType.Update;
            if (!Window.Save(true))
            {
                MessageBox.Show("保存失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }

            return true;
        }

        public void OnEdit()
        {
            if (m_CurSelDesignEl == null)
            {
                MessageBox.Show("请选择一项！");
                return;
            }
            MenuItemEdit_Click(null, null);
        }
        public void OnDelete()
        {
            if (m_CurSelDesignEl == null)
            {
                MessageBox.Show("请选择一项！");
                return;
            }
            CWindowControl WindowControl = (CWindowControl)m_CurSelDesignEl.Tag;
            WindowControl.m_ObjectMgr.Delete(WindowControl);
            m_CurSelDesignEl.Parent.Controls.Remove(m_CurSelDesignEl);
            m_CurSelDesignEl = null;
            SelectElement(null);
        }

        private void panelTop_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ListViewItem)))
            {
                ListViewItem item = (ListViewItem)e.Data.GetData(typeof(ListViewItem));

                OnDragDrop(DockStyle.Top, item);
            }
        }

        private void panelTop_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ListViewItem)))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;

        }

        private void panelLeft_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ListViewItem)))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;

        }

        private void panelLeft_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ListViewItem)))
            {
                ListViewItem item = (ListViewItem)e.Data.GetData(typeof(ListViewItem));

                OnDragDrop(DockStyle.Left, item);
            }
        }

        private void panelFill_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ListViewItem)))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;

        }

        private void panelFill_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ListViewItem)))
            {
                ListViewItem item = (ListViewItem)e.Data.GetData(typeof(ListViewItem));

                OnDragDrop(DockStyle.Fill, item);
            }

        }

        private void panelRight_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ListViewItem)))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;

        }

        private void panelRight_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ListViewItem)))
            {
                ListViewItem item = (ListViewItem)e.Data.GetData(typeof(ListViewItem));

                OnDragDrop(DockStyle.Right, item);
            }

        }

        private void panelBottom_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ListViewItem)))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;

        }

        private void panelBottom_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ListViewItem)))
            {
                ListViewItem item = (ListViewItem)e.Data.GetData(typeof(ListViewItem));

                OnDragDrop(DockStyle.Bottom, item);
            }
        }

        public void ShowPanel(DockStyle dock, bool bShow)
        {
            Panel panel = null;
            if (dock == DockStyle.Top)
                panel = panelTop;
            else if (dock == DockStyle.Bottom)
                panel = panelBottom;
            else if (dock == DockStyle.Left)
                panel = panelLeft;
            else if (dock == DockStyle.Right)
                panel = panelRight;

            if (panel != null)
                panel.Visible = bShow;
        }

        private void ChildenWindow_Activated(object sender, EventArgs e)
        {
            LayoutWindowDesigner frm = (LayoutWindowDesigner)this.MdiParent;
            frm.MenuItemTopPanel.Checked = panelTop.Visible;
            frm.MenuItemBottomPanel.Checked = panelBottom.Visible;
            frm.MenuItemLeftPanel.Checked = panelLeft.Visible;
            frm.MenuItemRightPanel.Checked = panelRight.Visible;
        }

        private void panelTop_Resize(object sender, EventArgs e)
        {
            ShowFocusDot(m_CurSelDesignEl);
        }

        private void panelLeft_Resize(object sender, EventArgs e)
        {
            ShowFocusDot(m_CurSelDesignEl);
        }

        private void panelFill_Resize(object sender, EventArgs e)
        {
            ShowFocusDot(m_CurSelDesignEl);
        }

        private void panelRight_Resize(object sender, EventArgs e)
        {
            ShowFocusDot(m_CurSelDesignEl);

        }

        private void panelBottom_Resize(object sender, EventArgs e)
        {
            ShowFocusDot(m_CurSelDesignEl);
        }

        private void MenuItemEdit_Click(object sender, EventArgs e)
        {
            if (m_CurSelDesignEl != null)
            {
                IDesignEl designEl = (IDesignEl)m_CurSelDesignEl;
                designEl.OnEdit();
                m_AttributeToolWindow.ControlEl = m_CurSelDesignEl;
            }
        }

        private void MenuItemDelete_Click(object sender, EventArgs e)
        {
            CWindowControl WindowControl = (CWindowControl)m_CurSelDesignEl.Tag;
            WindowControl.m_ObjectMgr.Delete(WindowControl);
            m_CurSelDesignEl.Parent.Controls.Remove(m_CurSelDesignEl);
            m_CurSelDesignEl = null;
            SelectElement(m_CurSelDesignEl);
        }

        private void MenuItemBringToFront_Click(object sender, EventArgs e)
        {
            if (m_CurSelDesignEl != null)
                m_CurSelDesignEl.BringToFront();
        }

        private void MenuItemSendToBack_Click(object sender, EventArgs e)
        {
            if (m_CurSelDesignEl != null)
                m_CurSelDesignEl.SendToBack();
        }

        private void ChildenWindow_Click(object sender, EventArgs e)
        {
            m_CurSelDesignEl = null;
            ShowFocusDot(null);
            m_AttributeToolWindow.WindowEl = this;
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            switch (m.Msg)
            {
                case 0xA1://WM_NCLBUTTONDOWN
                    {
                        ChildenWindow_Click(null,null);
                        break;
                    }
            }
        }
    }
}
