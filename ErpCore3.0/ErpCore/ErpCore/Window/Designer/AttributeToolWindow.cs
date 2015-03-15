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

namespace ErpCore.Window.Designer
{
    enum AttrType { WindowControl = 0, Window };
    public partial class AttributeToolWindow : Form
    {
        AttrType m_AttrType = AttrType.WindowControl;
        Control controlEl = null;
        ChildenWindow windowEl = null;

        bool m_bIsLoading = false;

        List<TabPage> m_lstPage = new List<TabPage>();

        public AttributeToolWindow()
        {
            InitializeComponent();
        }

        public Control ControlEl
        {
            get { return controlEl; }
            set
            {
                m_AttrType = AttrType.WindowControl;
                controlEl = value;
                LoadData();
            }
        }

        public ChildenWindow WindowEl
        {
            get { return windowEl; }
            set
            {
                m_AttrType = AttrType.Window;
                windowEl = value;
                LoadData();
            }
        }

        private void AttributeToolWindow_Load(object sender, EventArgs e)
        {
            foreach (TabPage page in tabControl.TabPages)
            {
                m_lstPage.Add(page);
            }
        }

        public void LoadData()
        {
            if (m_AttrType == AttrType.WindowControl)
            {
                if (ControlEl == null || ControlEl.Tag == null)
                    return;
            }
            else
            {
                if (WindowEl == null || WindowEl.Window == null)
                    return;
            }

            m_bIsLoading = true;

            UpdateTabCtrl();

            UpdatePropertyGrid();

            if (m_AttrType == AttrType.WindowControl)
            {
                IDesignEl designEl = (IDesignEl)ControlEl;
                CWindowControl WindowControl = (CWindowControl)ControlEl.Tag;
                if (designEl.GetCtrlType() == ControlType.NavBar)
                {
                }
                else if (designEl.GetCtrlType() == ControlType.TableGrid)
                {
                    TableGridEl tableGridEl = (TableGridEl)ControlEl;
                    CTableInWindowControl TableInWindowControl = (CTableInWindowControl)WindowControl.TableInWindowControlMgr.GetFirstObj();
                    CTable table = (CTable)Program.Ctx.TableMgr.Find(TableInWindowControl.FW_Table_id);
                    if (table != null)
                    {
                        listColumn.Items.Clear();
                        List<CBaseObject> lstColumn = table.ColumnMgr.GetList();
                        foreach (CBaseObject obj in lstColumn)
                        {
                            CColumn col = (CColumn)obj;
                            ListViewItem item = new ListViewItem();
                            item.Text = col.Name;
                            item.Tag = col;
                            listColumn.Items.Add(item);

                            List<CBaseObject> lstCIWC = TableInWindowControl.ColumnInTableInWindowControlMgr.GetList();
                            bool bHas = false;
                            foreach (CBaseObject obj2 in lstCIWC)
                            {
                                CColumnInTableInWindowControl ciwc = (CColumnInTableInWindowControl)obj2;
                                if (ciwc.FW_Column_id == col.Id)
                                {
                                    item.Checked = true;
                                    if (tableGridEl.dataGridView.Columns[col.Code] == null)
                                        tableGridEl.dataGridView.Columns.Add(col.Code, col.Name);
                                    bHas = true;
                                    break;
                                }
                            }
                            if (!bHas)
                            {
                                if (tableGridEl.dataGridView.Columns[col.Code] != null)
                                    tableGridEl.dataGridView.Columns.Remove(col.Code);
                            }
                        }
                    }

                    List<CBaseObject> lstTButton = TableInWindowControl.TButtonInTableInWindowControlMgr.GetList();
                    listToolBarButton.Items.Clear();
                    foreach (ToolStripItem tbutton in tableGridEl.toolStrip.Items)
                    {
                        ListViewItem item = new ListViewItem();
                        item.Text = tbutton.Text;
                        listToolBarButton.Items.Add(item);
                        bool bHas = false;
                        foreach (CBaseObject obj in lstTButton)
                        {
                            CTButtonInTableInWindowControl tbiwc = (CTButtonInTableInWindowControl)obj;
                            if (tbiwc.Title.Equals(tbutton.Text, StringComparison.OrdinalIgnoreCase))
                            {
                                item.Checked = true;
                                bHas = true;
                                break;
                            }
                        }
                        if (!bHas)
                            tbutton.Visible = false;
                    }

                    richTextFilter.Text = TableInWindowControl.QueryFilter;


                    List<CBaseObject> lstLWC = WindowControl.LinkageWindowControlMgr.GetList();
                    for (int i = 0; i < listLinkageWindowControl.Items.Count; i++)
                        listLinkageWindowControl.Items[i].Checked = false;
                    foreach (ListViewItem item in listLinkageWindowControl.Items)
                    {
                        CWindowControl wc = (CWindowControl)item.Tag;
                        foreach (CBaseObject obj in lstLWC)
                        {
                            CLinkageWindowControl lwc = (CLinkageWindowControl)obj;
                            if (lwc.SlaveID == wc.Id)
                            {
                                item.Checked = true;
                                break;
                            }
                        }
                    }
                }
                else if (designEl.GetCtrlType() == ControlType.TableTree)
                {
                    TableTreeEl treeEl = (TableTreeEl)ControlEl;

                    List<CBaseObject> lstLWC = WindowControl.LinkageWindowControlMgr.GetList();
                    for (int i = 0; i < listLinkageWindowControl.Items.Count; i++)
                        listLinkageWindowControl.Items[i].Checked = false;
                    foreach (ListViewItem item in listLinkageWindowControl.Items)
                    {
                        CWindowControl wc = (CWindowControl)item.Tag;
                        foreach (CBaseObject obj in lstLWC)
                        {
                            CLinkageWindowControl lwc = (CLinkageWindowControl)obj;
                            if (lwc.SlaveID == wc.Id)
                            {
                                item.Checked = true;
                                break;
                            }
                        }
                    }
                }
                else if (designEl.GetCtrlType() == ControlType.ListBox)
                {
                    CTableInWindowControl TableInWindowControl = (CTableInWindowControl)WindowControl.TableInWindowControlMgr.GetFirstObj();

                    richTextFilter.Text = TableInWindowControl.QueryFilter;


                    List<CBaseObject> lstLWC = WindowControl.LinkageWindowControlMgr.GetList();
                    for (int i = 0; i < listLinkageWindowControl.Items.Count; i++)
                        listLinkageWindowControl.Items[i].Checked = false;
                    foreach (ListViewItem item in listLinkageWindowControl.Items)
                    {
                        CWindowControl wc = (CWindowControl)item.Tag;
                        foreach (CBaseObject obj in lstLWC)
                        {
                            CLinkageWindowControl lwc = (CLinkageWindowControl)obj;
                            if (lwc.SlaveID == wc.Id)
                            {
                                item.Checked = true;
                                break;
                            }
                        }
                    }
                }
                else if (designEl.GetCtrlType() == ControlType.ComboBox)
                {
                    CTableInWindowControl TableInWindowControl = (CTableInWindowControl)WindowControl.TableInWindowControlMgr.GetFirstObj();

                    richTextFilter.Text = TableInWindowControl.QueryFilter;


                    List<CBaseObject> lstLWC = WindowControl.LinkageWindowControlMgr.GetList();
                    for (int i = 0; i < listLinkageWindowControl.Items.Count; i++)
                        listLinkageWindowControl.Items[i].Checked = false;
                    foreach (ListViewItem item in listLinkageWindowControl.Items)
                    {
                        CWindowControl wc = (CWindowControl)item.Tag;
                        foreach (CBaseObject obj in lstLWC)
                        {
                            CLinkageWindowControl lwc = (CLinkageWindowControl)obj;
                            if (lwc.SlaveID == wc.Id)
                            {
                                item.Checked = true;
                                break;
                            }
                        }
                    }
                }
                else if (designEl.GetCtrlType() == ControlType.TableTab)
                {
                    TableTabEl tab = (TableTabEl)ControlEl;
                    TableGridEl tableGridEl = tab.GetCurTableGridEl();
                    CTableInWindowControl TableInWindowControl = tableGridEl.TableInWindowControl;
                    CTable table = (CTable)Program.Ctx.TableMgr.Find(TableInWindowControl.FW_Table_id);
                    if (table != null)
                    {
                        listColumn.Items.Clear();
                        List<CBaseObject> lstColumn = table.ColumnMgr.GetList();
                        foreach (CBaseObject obj in lstColumn)
                        {
                            CColumn col = (CColumn)obj;
                            ListViewItem item = new ListViewItem();
                            item.Text = col.Name;
                            item.Tag = col;
                            listColumn.Items.Add(item);

                            bool bHas = false;
                            List<CBaseObject> lstCIWC = TableInWindowControl.ColumnInTableInWindowControlMgr.GetList();
                            foreach (CBaseObject obj2 in lstCIWC)
                            {
                                CColumnInTableInWindowControl ciwc = (CColumnInTableInWindowControl)obj2;
                                if (ciwc.FW_Column_id == col.Id)
                                {
                                    item.Checked = true;
                                    bHas = true;
                                    if (tableGridEl.dataGridView.Columns[col.Code] == null)
                                        tableGridEl.dataGridView.Columns.Add(col.Code, col.Name);
                                    break;
                                }
                            }
                            if (!bHas)
                            {
                                if (tableGridEl.dataGridView.Columns[col.Code] != null)
                                    tableGridEl.dataGridView.Columns.Remove(col.Code);
                            }
                        }
                    }

                    List<CBaseObject> lstTButton = TableInWindowControl.TButtonInTableInWindowControlMgr.GetList();
                    listToolBarButton.Items.Clear();
                    foreach (ToolStripItem tbutton in tableGridEl.toolStrip.Items)
                    {
                        ListViewItem item = new ListViewItem();
                        item.Text = tbutton.Text;
                        listToolBarButton.Items.Add(item);
                        bool bHas = false;
                        foreach (CBaseObject obj in lstTButton)
                        {
                            CTButtonInTableInWindowControl tbiwc = (CTButtonInTableInWindowControl)obj;
                            if (tbiwc.Title.Equals(tbutton.Text, StringComparison.OrdinalIgnoreCase))
                            {
                                item.Checked = true;
                                bHas = true;
                                break;
                            }
                        }
                        if (!bHas)
                            tbutton.Visible = false;
                    }

                    richTextFilter.Text = TableInWindowControl.QueryFilter;


                    List<CBaseObject> lstLWC = WindowControl.LinkageWindowControlMgr.GetList();
                    for (int i = 0; i < listLinkageWindowControl.Items.Count; i++)
                        listLinkageWindowControl.Items[i].Checked = false;
                    foreach (ListViewItem item in listLinkageWindowControl.Items)
                    {
                        CWindowControl wc = (CWindowControl)item.Tag;
                        foreach (CBaseObject obj in lstLWC)
                        {
                            CLinkageWindowControl lwc = (CLinkageWindowControl)obj;
                            if (lwc.SlaveID == wc.Id)
                            {
                                item.Checked = true;
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
            }

            m_bIsLoading = false;
        }
        void UpdateTabCtrl()
        {
            tabControl.TabPages.Clear();

            if (m_AttrType == AttrType.WindowControl)
            {
                IDesignEl designEl = (IDesignEl)ControlEl;
                if (designEl.GetCtrlType() == ControlType.NavBar)
                {
                    tabControl.TabPages.Add(m_lstPage[0]);
                }
                else if (designEl.GetCtrlType() == ControlType.TableGrid)
                {
                    tabControl.TabPages.Add(m_lstPage[0]);
                    tabControl.TabPages.Add(m_lstPage[1]);
                    tabControl.TabPages.Add(m_lstPage[2]);
                    tabControl.TabPages.Add(m_lstPage[3]);
                    tabControl.TabPages.Add(m_lstPage[4]);
                }
                else if (designEl.GetCtrlType() == ControlType.TableTree)
                {
                    tabControl.TabPages.Add(m_lstPage[0]);
                    tabControl.TabPages.Add(m_lstPage[4]);
                }
                else if (designEl.GetCtrlType() == ControlType.ListBox)
                {
                    tabControl.TabPages.Add(m_lstPage[0]);
                    tabControl.TabPages.Add(m_lstPage[3]);
                    tabControl.TabPages.Add(m_lstPage[4]);
                }
                else if (designEl.GetCtrlType() == ControlType.ComboBox)
                {
                    tabControl.TabPages.Add(m_lstPage[0]);
                    tabControl.TabPages.Add(m_lstPage[3]);
                    tabControl.TabPages.Add(m_lstPage[4]);
                }
                else if (designEl.GetCtrlType() == ControlType.TableTab)
                {
                    tabControl.TabPages.Add(m_lstPage[0]);
                    tabControl.TabPages.Add(m_lstPage[1]);
                    tabControl.TabPages.Add(m_lstPage[2]);
                    tabControl.TabPages.Add(m_lstPage[3]);
                    tabControl.TabPages.Add(m_lstPage[4]);
                }
            }
            else
            {
                tabControl.TabPages.Add(m_lstPage[0]);
            }
        }
        public void UpdatePropertyGrid()
        {
            if (m_AttrType == AttrType.WindowControl)
            {
                if (ControlEl == null || ControlEl.Tag == null)
                    return;
                IDesignEl designEl = (IDesignEl)ControlEl;
                if (designEl.GetCtrlType() == ControlType.NavBar)
                {
                    UIToolbarEl toolbar = (UIToolbarEl)ControlEl;
                    UIToolbarProp Setting = new UIToolbarProp();

                    Setting.名称 = toolbar.WindowControl.Name;
                    Setting.宽度 = toolbar.Width;
                    Setting.高度 = toolbar.Height;
                    Setting.停靠 = toolbar.Dock;
                    propertyGrid.SelectedObject = Setting;
                    propertyGrid.Refresh();
                }
                else if (designEl.GetCtrlType() == ControlType.TableGrid)
                {
                    TableGridEl te = (TableGridEl)ControlEl;
                    TableGridProp Setting = new TableGridProp();
                    Setting.名称 = te.WindowControl.Name;
                    Setting.宽度 = te.Width;
                    Setting.高度 = te.Height;
                    Setting.停靠 = te.Dock;
                    Setting.工具栏显示 = te.ShowToolBar;
                    Setting.标题栏显示 = te.ShowTitleBar;
                    propertyGrid.SelectedObject = Setting;
                    propertyGrid.Refresh();
                }
                else if (designEl.GetCtrlType() == ControlType.TableTree)
                {
                    TableTreeEl tree = (TableTreeEl)ControlEl;
                    TableTreeProp Setting = new TableTreeProp();
                    Setting.名称 = tree.WindowControl.Name;
                    Setting.宽度 = tree.Width;
                    Setting.高度 = tree.Height;
                    Setting.停靠 = tree.Dock;
                    Setting.标题栏显示 = tree.ShowTitleBar;
                    propertyGrid.SelectedObject = Setting;
                    propertyGrid.Refresh();
                }
                else if (designEl.GetCtrlType() == ControlType.ListBox)
                {
                    UIListBoxEl list = (UIListBoxEl)ControlEl;
                    UIListBoxProp Setting = new UIListBoxProp();
                    Setting.名称 = list.WindowControl.Name;
                    Setting.宽度 = list.Width;
                    Setting.高度 = list.Height;
                    Setting.停靠 = list.Dock;
                    Setting.标题栏显示 = list.ShowTitleBar;
                    propertyGrid.SelectedObject = Setting;
                    propertyGrid.Refresh();
                }
                else if (designEl.GetCtrlType() == ControlType.ComboBox)
                {
                    UIComboBoxEl combo = (UIComboBoxEl)ControlEl;
                    UIComboBoxProp Setting = new UIComboBoxProp();
                    Setting.名称 = combo.WindowControl.Name;
                    Setting.宽度 = combo.Width;
                    Setting.高度 = combo.Height;
                    Setting.停靠 = combo.Dock;
                    Setting.标题栏显示 = combo.ShowTitleBar;
                    propertyGrid.SelectedObject = Setting;
                    propertyGrid.Refresh();
                }
                else if (designEl.GetCtrlType() == ControlType.TableTab)
                {
                    TableTabEl tab = (TableTabEl)ControlEl;
                    TableGridEl grid = tab.GetCurTableGridEl();
                    TableTabProp Setting = new TableTabProp();
                    Setting.名称 = tab.WindowControl.Name;
                    Setting.宽度 = tab.Width;
                    Setting.高度 = tab.Height;
                    Setting.停靠 = tab.Dock;
                    Setting.工具栏显示 = grid.ShowToolBar;
                    Setting.标题栏显示 = tab.ShowTitleBar;
                    propertyGrid.SelectedObject = Setting;
                    propertyGrid.Refresh();
                }
            }
            else
            {
                if (WindowEl == null || WindowEl.Window == null)
                    return;

                ChildenWindowProp Setting = new ChildenWindowProp();
                Setting.名称 = WindowEl.Window.Name;
                Setting.宽度 = WindowEl.Width;
                Setting.高度 = WindowEl.Height;
                propertyGrid.SelectedObject = Setting;
                propertyGrid.Refresh();
            }
        }

        private void propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (m_AttrType == AttrType.WindowControl)
            {
                CWindowControl WindowControl = (CWindowControl)ControlEl.Tag;
                IDesignEl designEl = (IDesignEl)ControlEl;


                if (designEl.GetCtrlType() == ControlType.NavBar)
                {
                    if (e.ChangedItem.Label == "名称")
                    {
                        WindowControl.Name = e.ChangedItem.Value.ToString();
                    }
                    else if (e.ChangedItem.Label == "停靠")
                    {
                        ControlEl.Dock = (DockStyle)e.ChangedItem.Value;
                        WindowControl.Dock = (int)ControlEl.Dock;
                    }
                }
                else if (designEl.GetCtrlType() == ControlType.TableGrid)
                {
                    TableGridEl te = (TableGridEl)ControlEl;

                    if (e.ChangedItem.Label == "名称")
                    {
                        WindowControl.Name = e.ChangedItem.Value.ToString();
                    }
                    else if (e.ChangedItem.Label == "停靠")
                    {
                        te.Dock = (DockStyle)e.ChangedItem.Value;
                        WindowControl.Dock = (int)te.Dock;
                    }
                    else if (e.ChangedItem.Label == "工具栏显示")
                    {
                        te.ShowToolBar = (bool)e.ChangedItem.Value;
                        te.TableInWindowControl.ShowToolBar = te.ShowToolBar;
                        te.TableInWindowControl.m_CmdType = CmdType.Update;
                    }
                    else if (e.ChangedItem.Label == "标题栏显示")
                    {
                        te.ShowTitleBar = (bool)e.ChangedItem.Value;
                        te.TableInWindowControl.ShowTitleBar = te.ShowTitleBar;
                        te.TableInWindowControl.m_CmdType = CmdType.Update;
                    }
                }
                else if (designEl.GetCtrlType() == ControlType.TableTree)
                {
                    TableTreeEl tree = (TableTreeEl)ControlEl;
                    if (e.ChangedItem.Label == "名称")
                    {
                        WindowControl.Name = e.ChangedItem.Value.ToString();
                    }
                    else if (e.ChangedItem.Label == "停靠")
                    {
                        tree.Dock = (DockStyle)e.ChangedItem.Value;
                        WindowControl.Dock = (int)tree.Dock;
                    }
                    else if (e.ChangedItem.Label == "标题栏显示")
                    {
                        tree.ShowTitleBar = (bool)e.ChangedItem.Value;
                        WindowControl.ShowTitleBar = tree.ShowTitleBar;
                    }
                }
                else if (designEl.GetCtrlType() == ControlType.ListBox)
                {
                    UIListBoxEl list = (UIListBoxEl)ControlEl;
                    if (e.ChangedItem.Label == "名称")
                    {
                        WindowControl.Name = e.ChangedItem.Value.ToString();
                    }
                    else if (e.ChangedItem.Label == "停靠")
                    {
                        list.Dock = (DockStyle)e.ChangedItem.Value;
                        WindowControl.Dock = (int)list.Dock;
                    }
                    else if (e.ChangedItem.Label == "标题栏显示")
                    {
                        list.ShowTitleBar = (bool)e.ChangedItem.Value;
                        WindowControl.ShowTitleBar = list.ShowTitleBar;
                    }
                }
                else if (designEl.GetCtrlType() == ControlType.ComboBox)
                {
                    UIComboBoxEl comb = (UIComboBoxEl)ControlEl;
                    if (e.ChangedItem.Label == "名称")
                    {
                        ControlEl.Name = e.ChangedItem.Value.ToString();
                        WindowControl.Name = e.ChangedItem.Value.ToString();
                    }
                    else if (e.ChangedItem.Label == "停靠")
                    {
                        comb.Dock = (DockStyle)e.ChangedItem.Value;
                        WindowControl.Dock = (int)comb.Dock;
                    }
                    else if (e.ChangedItem.Label == "标题栏显示")
                    {
                        comb.ShowTitleBar = (bool)e.ChangedItem.Value;
                        WindowControl.ShowTitleBar = comb.ShowTitleBar;
                    }
                }
                else if (designEl.GetCtrlType() == ControlType.TableTab)
                {
                    TableTabEl tab = (TableTabEl)ControlEl;

                    if (e.ChangedItem.Label == "名称")
                    {
                        WindowControl.Name = e.ChangedItem.Value.ToString();
                    }
                    else if (e.ChangedItem.Label == "停靠")
                    {
                        tab.Dock = (DockStyle)e.ChangedItem.Value;
                        WindowControl.Dock = (int)tab.Dock;
                    }
                    else if (e.ChangedItem.Label == "工具栏显示")
                    {
                        TableGridEl grid = tab.GetCurTableGridEl();
                        if (grid != null)
                        {
                            grid.ShowToolBar = (bool)e.ChangedItem.Value;
                            grid.TableInWindowControl.ShowToolBar = grid.ShowToolBar;
                            grid.TableInWindowControl.m_CmdType = CmdType.Update;
                        }
                    }
                    else if (e.ChangedItem.Label == "标题栏显示")
                    {
                        tab.ShowTitleBar = (bool)e.ChangedItem.Value;
                        WindowControl.ShowTitleBar = tab.ShowTitleBar;
                    }
                }
                WindowControl.m_ObjectMgr.Update(WindowControl);
            }
            else
            {
                CWindow window = (CWindow)WindowEl.Window;
                if (e.ChangedItem.Label == "名称")
                {
                    window.Name = e.ChangedItem.Value.ToString();
                }
                else if (e.ChangedItem.Label == "宽度")
                {
                    WindowEl.Width = (int)e.ChangedItem.Value;
                    window.Width = WindowEl.Width;
                }
                else if (e.ChangedItem.Label == "高度")
                {
                    WindowEl.Height = (int)e.ChangedItem.Value;
                    window.Height = WindowEl.Height;
                }
                window.m_ObjectMgr.Update(window);
            }

        }


        private void richTextFilter_TextChanged(object sender, EventArgs e)
        {
            CWindowControl WindowControl = (CWindowControl)ControlEl.Tag;
            IDesignEl designEl = (IDesignEl)ControlEl;
            if (designEl.GetCtrlType() == ControlType.TableGrid)
            {
                CTableInWindowControl TableInWindowControl = (CTableInWindowControl)WindowControl.TableInWindowControlMgr.GetFirstObj();
                TableInWindowControl.QueryFilter = richTextFilter.Text.Trim();
                TableInWindowControl.m_CmdType = CmdType.Update;
            }
            else if (designEl.GetCtrlType() == ControlType.ListBox)
            {
                CTableInWindowControl TableInWindowControl = (CTableInWindowControl)WindowControl.TableInWindowControlMgr.GetFirstObj();
                TableInWindowControl.QueryFilter = richTextFilter.Text.Trim();
                TableInWindowControl.m_CmdType = CmdType.Update;
            }
            else if (designEl.GetCtrlType() == ControlType.ComboBox)
            {
                CTableInWindowControl TableInWindowControl = (CTableInWindowControl)WindowControl.TableInWindowControlMgr.GetFirstObj();
                TableInWindowControl.QueryFilter = richTextFilter.Text.Trim();
                TableInWindowControl.m_CmdType = CmdType.Update;
            }
        }


        private void listColumn_ItemCheck(object sender, ItemCheckEventArgs e)
        {

            if (e.CurrentValue == e.NewValue)
                return;
            ListViewItem item = listColumn.Items[e.Index];
            
            CWindowControl WindowControl = (CWindowControl)ControlEl.Tag;
            IDesignEl designEl = (IDesignEl)ControlEl;
            CColumn col = (CColumn)item.Tag;

            if (designEl.GetCtrlType() == ControlType.TableGrid)
            {
                TableGridEl te = (TableGridEl)ControlEl;
                CTableInWindowControl tiwc = (CTableInWindowControl)WindowControl.TableInWindowControlMgr.GetFirstObj();
                List<CBaseObject> lstCIWC= tiwc.ColumnInTableInWindowControlMgr.GetList();
                if (e.NewValue== CheckState.Checked)
                {                    
                    bool bHas = false;
                    foreach (CBaseObject obj in lstCIWC)
                    {
                        CColumnInTableInWindowControl ciwc = (CColumnInTableInWindowControl)obj;
                        if (ciwc.FW_Column_id == col.Id)
                        {
                            bHas = true;
                            break;
                        }
                    }
                    if (!bHas)
                    {
                        CColumnInTableInWindowControl ciwc = new CColumnInTableInWindowControl();
                        ciwc.FW_Column_id = col.Id;
                        ciwc.UI_TableInWindowControl_id = tiwc.Id;
                        ciwc.Ctx = Program.Ctx;
                        tiwc.ColumnInTableInWindowControlMgr.AddNew(ciwc);

                        if (te.dataGridView.Columns[col.Code] == null)
                            te.dataGridView.Columns.Add(col.Code, col.Name);
                    }
                }
                else
                {
                    foreach (CBaseObject obj in lstCIWC)
                    {
                        CColumnInTableInWindowControl ciwc = (CColumnInTableInWindowControl)obj;
                        if (ciwc.FW_Column_id == col.Id)
                        {
                            tiwc.ColumnInTableInWindowControlMgr.Delete(ciwc);
                            if (te.dataGridView.Columns[col.Code] != null)
                                te.dataGridView.Columns.Remove(col.Code);
                            break;
                        }
                    }
                }
            }
            else if (designEl.GetCtrlType() == ControlType.TableTab)
            {
                TableTabEl tab = (TableTabEl)ControlEl;
                TableGridEl te = tab.GetCurTableGridEl();
                CTableInWindowControl tiwc = te.TableInWindowControl;
                List<CBaseObject> lstCIWC = tiwc.ColumnInTableInWindowControlMgr.GetList();
                if (e.NewValue == CheckState.Checked)
                {
                    bool bHas = false;
                    foreach (CBaseObject obj in lstCIWC)
                    {
                        CColumnInTableInWindowControl ciwc = (CColumnInTableInWindowControl)obj;
                        if (ciwc.FW_Column_id == col.Id)
                        {
                            bHas = true;
                            break;
                        }
                    }
                    if (!bHas)
                    {
                        CColumnInTableInWindowControl ciwc = new CColumnInTableInWindowControl();
                        ciwc.FW_Column_id = col.Id;
                        ciwc.UI_TableInWindowControl_id = tiwc.Id;
                        ciwc.Ctx = Program.Ctx;
                        tiwc.ColumnInTableInWindowControlMgr.AddNew(ciwc);

                        if (te.dataGridView.Columns[col.Code] == null)
                            te.dataGridView.Columns.Add(col.Code, col.Name);
                    }
                }
                else
                {
                    foreach (CBaseObject obj in lstCIWC)
                    {
                        CColumnInTableInWindowControl ciwc = (CColumnInTableInWindowControl)obj;
                        if (ciwc.FW_Column_id == col.Id)
                        {
                            tiwc.ColumnInTableInWindowControlMgr.Delete(ciwc);
                            if (te.dataGridView.Columns[col.Code] != null)
                                te.dataGridView.Columns.Remove(col.Code);
                            break;
                        }
                    }
                }
            }
        }

        private void listToolBarButton_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.CurrentValue == e.NewValue)
                return;

            CWindowControl WindowControl = (CWindowControl)ControlEl.Tag;
            IDesignEl designEl = (IDesignEl)ControlEl;
            if (designEl.GetCtrlType() == ControlType.TableGrid)
            {
                TableGridEl te = (TableGridEl)ControlEl;
                CTableInWindowControl tiwc = (CTableInWindowControl)WindowControl.TableInWindowControlMgr.GetFirstObj();
                List<CBaseObject> lstTBIWC = tiwc.TButtonInTableInWindowControlMgr.GetList();
                if (e.NewValue== CheckState.Checked)
                {
                    bool bHas = false;
                    foreach (CBaseObject obj in lstTBIWC)
                    {
                        CTButtonInTableInWindowControl tbiwc = (CTButtonInTableInWindowControl)obj;
                        if (tbiwc.Title.Equals(listToolBarButton.Items[e.Index].Text, StringComparison.OrdinalIgnoreCase))
                        {
                            bHas = true;
                            break;
                        }
                    }
                    if (!bHas)
                    {
                        CTButtonInTableInWindowControl tbiwc =new CTButtonInTableInWindowControl();
                        tbiwc.Title = listToolBarButton.Items[e.Index].Text;
                        tbiwc.UI_TableInWindowControl_id=tiwc.Id;
                        tbiwc.Ctx = Program.Ctx;
                        tiwc.TButtonInTableInWindowControlMgr.AddNew(tbiwc);
                        te.SetToolBarButtonVisible(listToolBarButton.Items[e.Index].Text, true);
                    }
                }
                else
                {
                    foreach (CBaseObject obj in lstTBIWC)
                    {
                        CTButtonInTableInWindowControl tbiwc = (CTButtonInTableInWindowControl)obj;
                        if (tbiwc.Title.Equals(listToolBarButton.Items[e.Index].Text, StringComparison.OrdinalIgnoreCase))
                        {
                            tiwc.TButtonInTableInWindowControlMgr.Delete(tbiwc);
                            te.SetToolBarButtonVisible(listToolBarButton.Items[e.Index].Text, false);
                            break;
                        }
                    }
                }
            }
            else if (designEl.GetCtrlType() == ControlType.TableTree)
            {
                TableTreeEl treeEl = (TableTreeEl)ControlEl;
            }
            else if (designEl.GetCtrlType() == ControlType.TableTab)
            {
                TableTabEl tab = (TableTabEl)ControlEl;
                TableGridEl te = tab.GetCurTableGridEl();
                CTableInWindowControl tiwc = te.TableInWindowControl;
                List<CBaseObject> lstTBIWC = tiwc.TButtonInTableInWindowControlMgr.GetList();
                if (e.NewValue == CheckState.Checked)
                {
                    bool bHas = false;
                    foreach (CBaseObject obj in lstTBIWC)
                    {
                        CTButtonInTableInWindowControl tbiwc = (CTButtonInTableInWindowControl)obj;
                        if (tbiwc.Title.Equals(listToolBarButton.Items[e.Index].Text, StringComparison.OrdinalIgnoreCase))
                        {
                            bHas = true;
                            break;
                        }
                    }
                    if (!bHas)
                    {
                        CTButtonInTableInWindowControl tbiwc = new CTButtonInTableInWindowControl();
                        tbiwc.Title = listToolBarButton.Items[e.Index].Text;
                        tbiwc.UI_TableInWindowControl_id = tiwc.Id;
                        tbiwc.Ctx = Program.Ctx;
                        tiwc.TButtonInTableInWindowControlMgr.AddNew(tbiwc);
                        te.SetToolBarButtonVisible(listToolBarButton.Items[e.Index].Text, true);
                    }
                }
                else
                {
                    foreach (CBaseObject obj in lstTBIWC)
                    {
                        CTButtonInTableInWindowControl tbiwc = (CTButtonInTableInWindowControl)obj;
                        if (tbiwc.Title.Equals(listToolBarButton.Items[e.Index].Text, StringComparison.OrdinalIgnoreCase))
                        {
                            tiwc.TButtonInTableInWindowControlMgr.Delete(tbiwc);
                            te.SetToolBarButtonVisible(listToolBarButton.Items[e.Index].Text, false);
                            break;
                        }
                    }
                }
            }
        }

        private void listLinkageWindowControl_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (m_bIsLoading)
                return;

            CWindowControl WindowControl = (CWindowControl)ControlEl.Tag;

            CWindowControl wc = (CWindowControl)e.Item.Tag;
            if (e.Item.Checked)
            {
                List<CBaseObject> lstLWC = WindowControl.LinkageWindowControlMgr.GetList();
                bool bHas = false;
                foreach (CBaseObject obj in lstLWC)
                {
                    CLinkageWindowControl lwc = (CLinkageWindowControl)obj;
                    if (lwc.SlaveID == wc.Id)
                    {
                        bHas = true;
                        break;
                    }
                }
                if (!bHas)
                {
                    CLinkageWindowControl lwc = new CLinkageWindowControl();
                    lwc.MasterID = WindowControl.Id;
                    lwc.SlaveID = wc.Id;
                    lwc.Ctx = Program.Ctx;
                    WindowControl.LinkageWindowControlMgr.AddNew(lwc);
                }
            }
            else
            {
                List<CBaseObject> lstLWC = WindowControl.LinkageWindowControlMgr.GetList();
                foreach (CBaseObject obj in lstLWC)
                {
                    CLinkageWindowControl lwc = (CLinkageWindowControl)obj;
                    if (lwc.SlaveID == wc.Id)
                    {
                        WindowControl.LinkageWindowControlMgr.Delete(lwc);
                        break;
                    }
                }
            }


        }

        private void btUIFormula_Click(object sender, EventArgs e)
        {
            SelUIFormula frm = new SelUIFormula();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                int pos = richTextFilter.SelectionStart;
                richTextFilter.Text=richTextFilter.Text.Remove(pos, richTextFilter.SelectionLength);
                richTextFilter.Text=richTextFilter.Text.Insert(pos, frm.m_sSelFormula);
            }
        }



    }
}
