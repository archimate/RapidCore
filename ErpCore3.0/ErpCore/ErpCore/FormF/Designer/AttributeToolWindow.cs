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
using ErpCore.Window.Designer;
using ErpCoreModel.UI;
using ErpCore.Database.Table;

namespace ErpCore.FormF.Designer
{
    enum AttrType { FormControl = 0, Form };
    public partial class AttributeToolWindow : Form
    {
        AttrType m_AttrType = AttrType.FormControl;
        Control controlEl = null;
        ChildenForm formEl = null;

        public CTable m_Table = null;

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
                m_AttrType = AttrType.FormControl;
                controlEl = value;
                LoadData();
            }
        }

        public ChildenForm FormEl
        {
            get { return formEl; }
            set
            {
                m_AttrType = AttrType.Form;
                formEl = value;
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
            if (m_AttrType == AttrType.FormControl)
            {
                if (ControlEl == null || ControlEl.Tag == null)
                    return;
            }
            else
            {
                if (FormEl == null || FormEl.Form == null)
                    return;
            }

            m_bIsLoading = true;

            UpdateTabCtrl();

            UpdatePropertyGrid();

            if (m_AttrType == AttrType.FormControl)
            {
                CFormControl FormControl = (CFormControl)ControlEl.Tag;
                if (FormControl.DomainType == DomainControlType.Data)
                {
                    IDesignEl designEl = (IDesignEl)ControlEl;
                    if (designEl.GetCtrlType() == ControlType.TableGrid)
                    {
                        TableGridFEl tableGridEl = (TableGridFEl)ControlEl;
                        CTableInFormControl TableInFormControl = (CTableInFormControl)FormControl.TableInFormControlMgr.GetFirstObj();
                        CTable table = (CTable)Program.Ctx.TableMgr.Find(TableInFormControl.FW_Table_id);
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

                                List<CBaseObject> lstCIWC = TableInFormControl.ColumnInTableInFormControlMgr.GetList();
                                bool bHas = false;
                                foreach (CBaseObject obj2 in lstCIWC)
                                {
                                    CColumnInTableInFormControl ciwc = (CColumnInTableInFormControl)obj2;
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

                        List<CBaseObject> lstTButton = TableInFormControl.TButtonInTableInFormControlMgr.GetList();
                        listToolBarButton.Items.Clear();
                        foreach (ToolStripItem tbutton in tableGridEl.toolStrip.Items)
                        {
                            ListViewItem item = new ListViewItem();
                            item.Text = tbutton.Text;
                            listToolBarButton.Items.Add(item);
                            bool bHas = false;
                            foreach (CBaseObject obj in lstTButton)
                            {
                                CTButtonInTableInFormControl tbiwc = (CTButtonInTableInFormControl)obj;
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

                        richTextFilter.Text = TableInFormControl.QueryFilter;

                    }
                    else if (designEl.GetCtrlType() == ControlType.TableTree)
                    {
                        TableTreeFEl treeEl = (TableTreeFEl)ControlEl;

                    }
                    else if (designEl.GetCtrlType() == ControlType.ListBox)
                    {
                        CTableInFormControl TableInFormControl = (CTableInFormControl)FormControl.TableInFormControlMgr.GetFirstObj();

                        richTextFilter.Text = TableInFormControl.QueryFilter;


                    }
                    else if (designEl.GetCtrlType() == ControlType.ComboBox)
                    {
                        CTableInFormControl TableInFormControl = (CTableInFormControl)FormControl.TableInFormControlMgr.GetFirstObj();

                        richTextFilter.Text = TableInFormControl.QueryFilter;


                    }
                    else if (designEl.GetCtrlType() == ControlType.TableTab)
                    {
                        TableTabFEl tab = (TableTabFEl)ControlEl;
                        TableGridFEl tableGridEl = tab.GetCurTableGridEl();
                        CTableInFormControl TableInFormControl = tableGridEl.TableInFormControl;
                        CTable table = (CTable)Program.Ctx.TableMgr.Find(TableInFormControl.FW_Table_id);
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
                                List<CBaseObject> lstCIWC = TableInFormControl.ColumnInTableInFormControlMgr.GetList();
                                foreach (CBaseObject obj2 in lstCIWC)
                                {
                                    CColumnInTableInFormControl ciwc = (CColumnInTableInFormControl)obj2;
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

                        List<CBaseObject> lstTButton = TableInFormControl.TButtonInTableInFormControlMgr.GetList();
                        listToolBarButton.Items.Clear();
                        foreach (ToolStripItem tbutton in tableGridEl.toolStrip.Items)
                        {
                            ListViewItem item = new ListViewItem();
                            item.Text = tbutton.Text;
                            listToolBarButton.Items.Add(item);
                            bool bHas = false;
                            foreach (CBaseObject obj in lstTButton)
                            {
                                CTButtonInTableInFormControl tbiwc = (CTButtonInTableInFormControl)obj;
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

                        richTextFilter.Text = TableInFormControl.QueryFilter;


                    }
                }
                else //if (FormControl.DomainType == DomainControlType.Form)
                {
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

            if (m_AttrType == AttrType.FormControl)
            {
                CFormControl FormControl = (CFormControl)ControlEl.Tag;
                if (FormControl.DomainType == DomainControlType.Data)
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
                    }
                    else if (designEl.GetCtrlType() == ControlType.TableTree)
                    {
                        tabControl.TabPages.Add(m_lstPage[0]);
                    }
                    else if (designEl.GetCtrlType() == ControlType.ListBox)
                    {
                        tabControl.TabPages.Add(m_lstPage[0]);
                        tabControl.TabPages.Add(m_lstPage[3]);
                    }
                    else if (designEl.GetCtrlType() == ControlType.ComboBox)
                    {
                        tabControl.TabPages.Add(m_lstPage[0]);
                        tabControl.TabPages.Add(m_lstPage[3]);
                    }
                    else if (designEl.GetCtrlType() == ControlType.TableTab)
                    {
                        tabControl.TabPages.Add(m_lstPage[0]);
                        tabControl.TabPages.Add(m_lstPage[1]);
                        tabControl.TabPages.Add(m_lstPage[2]);
                        tabControl.TabPages.Add(m_lstPage[3]);
                    }
                    else if (designEl.GetCtrlType() == ControlType.Label)
                    {
                        tabControl.TabPages.Add(m_lstPage[0]);
                    }
                    else if (designEl.GetCtrlType() == ControlType.TextBox)
                    {
                        tabControl.TabPages.Add(m_lstPage[0]);
                    }
                }
                else //if (FormControl.DomainType == DomainControlType.Form)
                {
                    tabControl.TabPages.Add(m_lstPage[0]);
                }
            }
            else
            {
                tabControl.TabPages.Add(m_lstPage[0]);
            }
        }
        public void UpdatePropertyGrid()
        {
            if (m_AttrType == AttrType.FormControl)
            {
                if (ControlEl == null || ControlEl.Tag == null)
                    return;
                CFormControl FormControl = (CFormControl)ControlEl.Tag;
                if (FormControl.DomainType == DomainControlType.Data)
                {
                    IDesignEl designEl = (IDesignEl)ControlEl;
                    if (designEl.GetCtrlType() == ControlType.TableGrid)
                    {
                        TableGridFEl te = (TableGridFEl)ControlEl;
                        TableGridFProp Setting = new TableGridFProp();
                        Setting.名称 = te.FormControl.Name;
                        Setting.宽度 = te.Width;
                        Setting.高度 = te.Height;
                        Setting.工具栏显示 = te.ShowToolBar;
                        Setting.标题栏显示 = te.ShowTitleBar;
                        propertyGrid.SelectedObject = Setting;
                        propertyGrid.Refresh();
                    }
                    else if (designEl.GetCtrlType() == ControlType.TableTree)
                    {
                        TableTreeFEl tree = (TableTreeFEl)ControlEl;
                        TableTreeFProp Setting = new TableTreeFProp();
                        Setting.名称 = tree.FormControl.Name;
                        Setting.宽度 = tree.Width;
                        Setting.高度 = tree.Height;
                        Setting.标题栏显示 = tree.ShowTitleBar;
                        propertyGrid.SelectedObject = Setting;
                        propertyGrid.Refresh();
                    }
                    else if (designEl.GetCtrlType() == ControlType.ListBox)
                    {
                        UIListBoxFEl list = (UIListBoxFEl)ControlEl;
                        UIListBoxFProp Setting = new UIListBoxFProp();
                        Setting.名称 = list.FormControl.Name;
                        Setting.宽度 = list.Width;
                        Setting.高度 = list.Height;
                        Setting.标题栏显示 = list.ShowTitleBar;
                        propertyGrid.SelectedObject = Setting;
                        propertyGrid.Refresh();
                    }
                    else if (designEl.GetCtrlType() == ControlType.ComboBox)
                    {
                        UIComboBoxFEl combo = (UIComboBoxFEl)ControlEl;
                        UIComboBoxFProp Setting = new UIComboBoxFProp();
                        Setting.名称 = combo.FormControl.Name;
                        Setting.宽度 = combo.Width;
                        Setting.高度 = combo.Height;
                        Setting.标题栏显示 = combo.ShowTitleBar;
                        propertyGrid.SelectedObject = Setting;
                        propertyGrid.Refresh();
                    }
                    else if (designEl.GetCtrlType() == ControlType.TableTab)
                    {
                        TableTabFEl tab = (TableTabFEl)ControlEl;
                        TableGridFEl grid = tab.GetCurTableGridEl();
                        TableTabFProp Setting = new TableTabFProp();
                        Setting.名称 = tab.FormControl.Name;
                        Setting.宽度 = tab.Width;
                        Setting.高度 = tab.Height;
                        Setting.工具栏显示 = grid.ShowToolBar;
                        Setting.标题栏显示 = tab.ShowTitleBar;
                        propertyGrid.SelectedObject = Setting;
                        propertyGrid.Refresh();
                    }
                    //else if (designEl.GetCtrlType() == ControlType.Label)
                    //{
                    //    UILabelFEl label = (UILabelFEl)ControlEl;
                    //    UILabelFProp Setting = new UILabelFProp();
                    //    Setting.名称 = label.FormControl.Name;
                    //    Setting.宽度 = label.Width;
                    //    Setting.高度 = label.Height;
                    //    Setting.文本 = label.Text;
                    //    propertyGrid.SelectedObject = Setting;
                    //    propertyGrid.Refresh();
                    //}
                    //else if (designEl.GetCtrlType() == ControlType.TextBox)
                    //{
                    //    UITextBoxFEl textBox = (UITextBoxFEl)ControlEl;
                    //    UITextBoxFProp Setting = new UITextBoxFProp();
                    //    Setting.名称 = textBox.FormControl.Name;
                    //    Setting.宽度 = textBox.Width;
                    //    Setting.高度 = textBox.Height;
                    //    Setting.文本 = textBox.Text;
                    //    propertyGrid.SelectedObject = Setting;
                    //    propertyGrid.Refresh();
                    //}
                }
                else //if(FormControl.DomainType== DomainControlType.Form)
                {
                    ColumnType coltype = (ColumnType)FormControl.CtrlType;
                    if (coltype == ColumnType.string_type)
                    {
                        ExTextBoxEl ctrl=(ExTextBoxEl)ControlEl;
                        ExStringBoxProp Setting = new ExStringBoxProp();
                        Setting.名称 = FormControl.Name;
                        Setting.标题 = FormControl.Caption;
                        Setting.宽度 = FormControl.Width;
                        Setting.高度 = FormControl.Height;
                        Setting.最大长度 = ctrl.textBox.MaxLength;
                        propertyGrid.SelectedObject = Setting;
                        propertyGrid.Refresh();
                    }
                    else if (coltype == ColumnType.text_type)
                    {
                        ExTextBoxProp Setting = new ExTextBoxProp();
                        Setting.名称 = FormControl.Name;
                        Setting.标题 = FormControl.Caption;
                        Setting.宽度 = FormControl.Width;
                        Setting.高度 = FormControl.Height;
                        propertyGrid.SelectedObject = Setting;
                        propertyGrid.Refresh();
                    }
                    else if (coltype == ColumnType.int_type)
                    {
                        ExTextBoxProp Setting = new ExTextBoxProp();
                        Setting.名称 = FormControl.Name;
                        Setting.标题 = FormControl.Caption;
                        Setting.宽度 = FormControl.Width;
                        Setting.高度 = FormControl.Height;
                        propertyGrid.SelectedObject = Setting;
                        propertyGrid.Refresh();
                    }
                    else if (coltype == ColumnType.long_type)
                    {
                        ExTextBoxProp Setting = new ExTextBoxProp();
                        Setting.名称 = FormControl.Name;
                        Setting.标题 = FormControl.Caption;
                        Setting.宽度 = FormControl.Width;
                        Setting.高度 = FormControl.Height;
                        propertyGrid.SelectedObject = Setting;
                        propertyGrid.Refresh();
                    }
                    else if (coltype == ColumnType.numeric_type)
                    {
                        ExNumericBoxProp Setting = new ExNumericBoxProp();
                        Setting.名称 = FormControl.Name;
                        Setting.标题 = FormControl.Caption;
                        Setting.宽度 = FormControl.Width;
                        Setting.高度 = FormControl.Height;
                        CColumn col = (CColumn)m_Table.ColumnMgr.Find(FormControl.FW_Column_id);
                        if (col != null)
                        {
                            Setting.长度 = col.ColLen;
                            Setting.小数位数 = col.ColDecimal;
                        }
                        propertyGrid.SelectedObject = Setting;
                        propertyGrid.Refresh();
                    }
                    else if (coltype == ColumnType.bool_type)
                    {
                        ExCheckBoxProp Setting = new ExCheckBoxProp();
                        Setting.名称 = FormControl.Name;
                        Setting.标题 = FormControl.Caption;
                        Setting.宽度 = FormControl.Width;
                        Setting.高度 = FormControl.Height;
                        propertyGrid.SelectedObject = Setting;
                        propertyGrid.Refresh();
                    }
                    else if (coltype == ColumnType.ref_type)
                    {
                        ExComboBoxProp Setting = new ExComboBoxProp();
                        Setting.名称 = FormControl.Name;
                        Setting.标题 = FormControl.Caption;
                        Setting.宽度 = FormControl.Width;
                        Setting.高度 = FormControl.Height;
                        CColumn col = (CColumn)m_Table.ColumnMgr.Find(FormControl.FW_Column_id);
                        if (col != null)
                        {
                            CTable table = (CTable)Program.Ctx.TableMgr.Find(col.RefTable);
                            if (table != null)
                            {
                                Setting.引用表 = table.Code;
                                UpdateRefTable(table.Code);
                                CColumn column = (CColumn)table.ColumnMgr.Find(col.RefCol);
                                Setting.引用字段 = column.Code;
                                CColumn column2 = (CColumn)table.ColumnMgr.Find(col.RefShowCol);
                                Setting.引用显示字段 = column2.Code;
                            }
                        }
                        propertyGrid.SelectedObject = Setting;
                        propertyGrid.Refresh();
                    }
                    else if (coltype == ColumnType.datetime_type)
                    {
                        ExDateTimePickerProp Setting = new ExDateTimePickerProp();
                        Setting.名称 = FormControl.Name;
                        Setting.标题 = FormControl.Caption;
                        Setting.宽度 = FormControl.Width;
                        Setting.高度 = FormControl.Height;
                        propertyGrid.SelectedObject = Setting;
                        propertyGrid.Refresh();
                    }

                }
            }
            else
            {
                if (FormEl == null || FormEl.Form == null)
                    return;

                ChildenFormProp Setting = new ChildenFormProp();
                Setting.名称 = FormEl.Form.Name;
                Setting.宽度 = FormEl.Width;
                Setting.高度 = FormEl.Height;
                propertyGrid.SelectedObject = Setting;
                propertyGrid.Refresh();
            }
        }

        private void propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (m_AttrType == AttrType.FormControl)
            {
                CFormControl FormControl = (CFormControl)ControlEl.Tag;
                IDesignEl designEl = (IDesignEl)ControlEl;
                
                if (designEl.GetCtrlType() == ControlType.TableGrid)
                {
                    TableGridFEl te = (TableGridFEl)ControlEl;

                    if (e.ChangedItem.Label == "名称")
                    {
                        FormControl.Name = e.ChangedItem.Value.ToString();
                    }
                    else if (e.ChangedItem.Label == "工具栏显示")
                    {
                        te.ShowToolBar = (bool)e.ChangedItem.Value;
                        te.TableInFormControl.ShowToolBar = te.ShowToolBar;
                        te.TableInFormControl.m_CmdType = CmdType.Update;
                    }
                    else if (e.ChangedItem.Label == "标题栏显示")
                    {
                        te.ShowTitleBar = (bool)e.ChangedItem.Value;
                        te.TableInFormControl.ShowTitleBar = te.ShowTitleBar;
                        te.TableInFormControl.m_CmdType = CmdType.Update;
                    }
                    else if (e.ChangedItem.Label == "宽度")
                    {
                        te.Width = (int)e.ChangedItem.Value;
                        FormControl.Width = te.Width;
                    }
                    else if (e.ChangedItem.Label == "高度")
                    {
                        te.Height = (int)e.ChangedItem.Value;
                        FormControl.Height = te.Height;
                    }
                }
                else if (designEl.GetCtrlType() == ControlType.TableTree)
                {
                    TableTreeFEl tree = (TableTreeFEl)ControlEl;
                    if (e.ChangedItem.Label == "名称")
                    {
                        FormControl.Name = e.ChangedItem.Value.ToString();
                    }
                    else if (e.ChangedItem.Label == "标题栏显示")
                    {
                        tree.ShowTitleBar = (bool)e.ChangedItem.Value;
                        FormControl.ShowTitleBar = tree.ShowTitleBar;
                    }
                    else if (e.ChangedItem.Label == "宽度")
                    {
                        tree.Width = (int)e.ChangedItem.Value;
                        FormControl.Width = tree.Width;
                    }
                    else if (e.ChangedItem.Label == "高度")
                    {
                        tree.Height = (int)e.ChangedItem.Value;
                        FormControl.Height = tree.Height;
                    }
                }
                else if (designEl.GetCtrlType() == ControlType.ListBox)
                {
                    UIListBoxFEl list = (UIListBoxFEl)ControlEl;
                    if (e.ChangedItem.Label == "名称")
                    {
                        FormControl.Name = e.ChangedItem.Value.ToString();
                    }
                    else if (e.ChangedItem.Label == "标题栏显示")
                    {
                        list.ShowTitleBar = (bool)e.ChangedItem.Value;
                        FormControl.ShowTitleBar = list.ShowTitleBar;
                    }
                    else if (e.ChangedItem.Label == "宽度")
                    {
                        list.Width = (int)e.ChangedItem.Value;
                        FormControl.Width = list.Width;
                    }
                    else if (e.ChangedItem.Label == "高度")
                    {
                        list.Height = (int)e.ChangedItem.Value;
                        FormControl.Height = list.Height;
                    }
                }
                else if (designEl.GetCtrlType() == ControlType.ComboBox)
                {
                    UIComboBoxFEl comb = (UIComboBoxFEl)ControlEl;
                    if (e.ChangedItem.Label == "名称")
                    {
                        FormControl.Name = e.ChangedItem.Value.ToString();
                    }
                    else if (e.ChangedItem.Label == "标题栏显示")
                    {
                        comb.ShowTitleBar = (bool)e.ChangedItem.Value;
                        FormControl.ShowTitleBar = comb.ShowTitleBar;
                    }
                    else if (e.ChangedItem.Label == "宽度")
                    {
                        comb.Width = (int)e.ChangedItem.Value;
                        FormControl.Width = comb.Width;
                    }
                    else if (e.ChangedItem.Label == "高度")
                    {
                        comb.Height = (int)e.ChangedItem.Value;
                        FormControl.Height = comb.Height;
                    }
                }
                else if (designEl.GetCtrlType() == ControlType.TableTab)
                {
                    TableTabFEl tab = (TableTabFEl)ControlEl;

                    if (e.ChangedItem.Label == "名称")
                    {
                        FormControl.Name = e.ChangedItem.Value.ToString();
                    }
                    else if (e.ChangedItem.Label == "工具栏显示")
                    {
                        TableGridFEl grid = tab.GetCurTableGridEl();
                        if (grid != null)
                        {
                            grid.ShowToolBar = (bool)e.ChangedItem.Value;
                            grid.TableInFormControl.ShowToolBar = grid.ShowToolBar;
                            grid.TableInFormControl.m_CmdType = CmdType.Update;
                        }
                    }
                    else if (e.ChangedItem.Label == "标题栏显示")
                    {
                        tab.ShowTitleBar = (bool)e.ChangedItem.Value;
                        FormControl.ShowTitleBar = tab.ShowTitleBar;
                    }
                    else if (e.ChangedItem.Label == "宽度")
                    {
                        tab.Width = (int)e.ChangedItem.Value;
                        FormControl.Width = tab.Width;
                    }
                    else if (e.ChangedItem.Label == "高度")
                    {
                        tab.Height = (int)e.ChangedItem.Value;
                        FormControl.Height = tab.Height;
                    }
                }
                else if (designEl.GetCtrlType() == ControlType.TextBox)
                {
                    ExTextBoxEl ctrl = (ExTextBoxEl)ControlEl;
                    if (e.ChangedItem.Label == "名称")
                    {
                        FormControl.Name = e.ChangedItem.Value.ToString();
                    }
                    else if (e.ChangedItem.Label == "标题")
                    {
                        ctrl.lbCaption.Text = e.ChangedItem.Value.ToString();
                        FormControl.Caption = ctrl.lbCaption.Text;
                    }
                    else if (e.ChangedItem.Label == "最大长度")
                    {
                        ctrl.textBox.MaxLength =Convert.ToInt32( e.ChangedItem.Value);
                        CColumn col = (CColumn)m_Table.ColumnMgr.Find(FormControl.FW_Column_id);
                        if (col != null)
                        {
                            col.ColLen = ctrl.textBox.MaxLength;
                            m_Table.ColumnMgr.Update(col);
                        }
                    }
                    else if (e.ChangedItem.Label == "长度")
                    {
                        CColumn col = (CColumn)m_Table.ColumnMgr.Find(FormControl.FW_Column_id);
                        if (col != null)
                        {
                            col.ColLen = Convert.ToInt32(e.ChangedItem.Value);
                            m_Table.ColumnMgr.Update(col);
                        }
                    }
                    else if (e.ChangedItem.Label == "小数位数")
                    {
                        CColumn col = (CColumn)m_Table.ColumnMgr.Find(FormControl.FW_Column_id);
                        if (col != null)
                        {
                            col.ColDecimal = Convert.ToInt32(e.ChangedItem.Value);
                            m_Table.ColumnMgr.Update(col);
                        }
                    }
                    else if (e.ChangedItem.Label == "宽度")
                    {
                        ctrl.Width = (int)e.ChangedItem.Value;
                        FormControl.Width = ctrl.Width;
                    }
                    else if (e.ChangedItem.Label == "高度")
                    {
                        ctrl.Height = (int)e.ChangedItem.Value;
                        FormControl.Height = ctrl.Height;
                    }
                }
                else if (designEl.GetCtrlType() == ControlType.CheckBox)
                {
                    ExCheckBoxEl ctrl = (ExCheckBoxEl)ControlEl;
                    if (e.ChangedItem.Label == "名称")
                    {
                        FormControl.Name = e.ChangedItem.Value.ToString();
                    }
                    else if (e.ChangedItem.Label == "标题")
                    {
                        ctrl.lbCaption.Text = e.ChangedItem.Value.ToString();
                        FormControl.Caption = ctrl.lbCaption.Text;
                    }
                    else if (e.ChangedItem.Label == "宽度")
                    {
                        ctrl.Width = (int)e.ChangedItem.Value;
                        FormControl.Width = ctrl.Width;
                    }
                    else if (e.ChangedItem.Label == "高度")
                    {
                        ctrl.Height = (int)e.ChangedItem.Value;
                        FormControl.Height = ctrl.Height;
                    }
                }
                else if (designEl.GetCtrlType() == ControlType.DateTimePicker)
                {
                    ExCheckBoxEl ctrl = (ExCheckBoxEl)ControlEl;
                    if (e.ChangedItem.Label == "名称")
                    {
                        FormControl.Name = e.ChangedItem.Value.ToString();
                    }
                    else if (e.ChangedItem.Label == "标题")
                    {
                        ctrl.lbCaption.Text = e.ChangedItem.Value.ToString();
                        FormControl.Caption = ctrl.lbCaption.Text;
                    }
                    else if (e.ChangedItem.Label == "宽度")
                    {
                        ctrl.Width = (int)e.ChangedItem.Value;
                        FormControl.Width = ctrl.Width;
                    }
                    else if (e.ChangedItem.Label == "高度")
                    {
                        ctrl.Height = (int)e.ChangedItem.Value;
                        FormControl.Height = ctrl.Height;
                    }
                }
                else if (designEl.GetCtrlType() == ControlType.RefComboBox)
                {
                    ExComboBoxEl ctrl = (ExComboBoxEl)ControlEl;
                    if (e.ChangedItem.Label == "名称")
                    {
                        FormControl.Name = e.ChangedItem.Value.ToString();
                    }
                    else if (e.ChangedItem.Label == "标题")
                    {
                        ctrl.lbCaption.Text = e.ChangedItem.Value.ToString();
                        FormControl.Caption = ctrl.lbCaption.Text;
                    }
                    else if (e.ChangedItem.Label == "宽度")
                    {
                        ctrl.Width = (int)e.ChangedItem.Value;
                        FormControl.Width = ctrl.Width;
                    }
                    else if (e.ChangedItem.Label == "高度")
                    {
                        ctrl.Height = (int)e.ChangedItem.Value;
                        FormControl.Height = ctrl.Height;
                    }
                    else if (e.ChangedItem.Label == "引用表")
                    {
                        UpdateRefTable(e.ChangedItem.Value.ToString());
                        CColumn col=(CColumn) m_Table.ColumnMgr.Find(FormControl.FW_Column_id);
                        m_Table.ColumnMgr.Update(col);
                    }
                    else if (e.ChangedItem.Label == "引用字段")
                    {
                        CColumn col = (CColumn)m_Table.ColumnMgr.Find(FormControl.FW_Column_id);
                        m_Table.ColumnMgr.Update(col);
                    }
                    else if (e.ChangedItem.Label == "引用显示字段")
                    {
                        CColumn col = (CColumn)m_Table.ColumnMgr.Find(FormControl.FW_Column_id);
                        m_Table.ColumnMgr.Update(col);
                    }
                }
                FormControl.m_ObjectMgr.Update(FormControl);
            }
            else
            {
                CForm form = (CForm)FormEl.Form;
                if (e.ChangedItem.Label == "名称")
                {
                    FormEl.Name = e.ChangedItem.Value.ToString();
                    form.Name = FormEl.Name;
                }
                else if (e.ChangedItem.Label == "宽度")
                {
                    FormEl.Width = (int)e.ChangedItem.Value;
                    form.Width = FormEl.Width;
                }
                else if (e.ChangedItem.Label == "高度")
                {
                    FormEl.Height = (int)e.ChangedItem.Value;
                    form.Height = FormEl.Height;
                }
                form.m_ObjectMgr.Update(form);
            }

        }


        private void richTextFilter_TextChanged(object sender, EventArgs e)
        {
            CFormControl FormControl = (CFormControl)ControlEl.Tag;
            IDesignEl designEl = (IDesignEl)ControlEl;
            if (designEl.GetCtrlType() == ControlType.TableGrid)
            {
                CTableInFormControl TableInFormControl = (CTableInFormControl)FormControl.TableInFormControlMgr.GetFirstObj();
                TableInFormControl.QueryFilter = richTextFilter.Text.Trim();
                TableInFormControl.m_CmdType = CmdType.Update;
            }
            else if (designEl.GetCtrlType() == ControlType.ListBox)
            {
                CTableInFormControl TableInFormControl = (CTableInFormControl)FormControl.TableInFormControlMgr.GetFirstObj();
                TableInFormControl.QueryFilter = richTextFilter.Text.Trim();
                TableInFormControl.m_CmdType = CmdType.Update;
            }
            else if (designEl.GetCtrlType() == ControlType.ComboBox)
            {
                CTableInFormControl TableInFormControl = (CTableInFormControl)FormControl.TableInFormControlMgr.GetFirstObj();
                TableInFormControl.QueryFilter = richTextFilter.Text.Trim();
                TableInFormControl.m_CmdType = CmdType.Update;
            }
        }


        private void listColumn_ItemCheck(object sender, ItemCheckEventArgs e)
        {

            if (e.CurrentValue == e.NewValue)
                return;
            ListViewItem item = listColumn.Items[e.Index];

            CFormControl FormControl = (CFormControl)ControlEl.Tag;
            IDesignEl designEl = (IDesignEl)ControlEl;
            CColumn col = (CColumn)item.Tag;

            if (designEl.GetCtrlType() == ControlType.TableGrid)
            {
                TableGridFEl te = (TableGridFEl)ControlEl;
                CTableInFormControl tiwc = (CTableInFormControl)FormControl.TableInFormControlMgr.GetFirstObj();
                List<CBaseObject> lstCIWC = tiwc.ColumnInTableInFormControlMgr.GetList();
                if (e.NewValue== CheckState.Checked)
                {                    
                    bool bHas = false;
                    foreach (CBaseObject obj in lstCIWC)
                    {
                        CColumnInTableInFormControl ciwc = (CColumnInTableInFormControl)obj;
                        if (ciwc.FW_Column_id == col.Id)
                        {
                            bHas = true;
                            break;
                        }
                    }
                    if (!bHas)
                    {
                        CColumnInTableInFormControl ciwc = new CColumnInTableInFormControl();
                        ciwc.FW_Column_id = col.Id;
                        ciwc.UI_TableInFormControl_id = tiwc.Id;
                        ciwc.Ctx = Program.Ctx;
                        tiwc.ColumnInTableInFormControlMgr.AddNew(ciwc);

                        if (te.dataGridView.Columns[col.Code] == null)
                            te.dataGridView.Columns.Add(col.Code, col.Name);
                    }
                }
                else
                {
                    foreach (CBaseObject obj in lstCIWC)
                    {
                        CColumnInTableInFormControl ciwc = (CColumnInTableInFormControl)obj;
                        if (ciwc.FW_Column_id == col.Id)
                        {
                            tiwc.ColumnInTableInFormControlMgr.Delete(ciwc);
                            if (te.dataGridView.Columns[col.Code] != null)
                                te.dataGridView.Columns.Remove(col.Code);
                            break;
                        }
                    }
                }
            }
            else if (designEl.GetCtrlType() == ControlType.TableTab)
            {
                TableTabFEl tab = (TableTabFEl)ControlEl;
                TableGridFEl te = tab.GetCurTableGridEl();
                CTableInFormControl tiwc = te.TableInFormControl;
                List<CBaseObject> lstCIWC = tiwc.ColumnInTableInFormControlMgr.GetList();
                if (e.NewValue == CheckState.Checked)
                {
                    bool bHas = false;
                    foreach (CBaseObject obj in lstCIWC)
                    {
                        CColumnInTableInFormControl ciwc = (CColumnInTableInFormControl)obj;
                        if (ciwc.FW_Column_id == col.Id)
                        {
                            bHas = true;
                            break;
                        }
                    }
                    if (!bHas)
                    {
                        CColumnInTableInFormControl ciwc = new CColumnInTableInFormControl();
                        ciwc.FW_Column_id = col.Id;
                        ciwc.UI_TableInFormControl_id = tiwc.Id;
                        ciwc.Ctx = Program.Ctx;
                        tiwc.ColumnInTableInFormControlMgr.AddNew(ciwc);

                        if (te.dataGridView.Columns[col.Code] == null)
                            te.dataGridView.Columns.Add(col.Code, col.Name);
                    }
                }
                else
                {
                    foreach (CBaseObject obj in lstCIWC)
                    {
                        CColumnInTableInFormControl ciwc = (CColumnInTableInFormControl)obj;
                        if (ciwc.FW_Column_id == col.Id)
                        {
                            tiwc.ColumnInTableInFormControlMgr.Delete(ciwc);
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

            CFormControl FormControl = (CFormControl)ControlEl.Tag;
            IDesignEl designEl = (IDesignEl)ControlEl;
            if (designEl.GetCtrlType() == ControlType.TableGrid)
            {
                TableGridFEl te = (TableGridFEl)ControlEl;
                CTableInFormControl tiwc = (CTableInFormControl)FormControl.TableInFormControlMgr.GetFirstObj();
                List<CBaseObject> lstTBIWC = tiwc.TButtonInTableInFormControlMgr.GetList();
                if (e.NewValue== CheckState.Checked)
                {
                    bool bHas = false;
                    foreach (CBaseObject obj in lstTBIWC)
                    {
                        CTButtonInTableInFormControl tbiwc = (CTButtonInTableInFormControl)obj;
                        if (tbiwc.Title.Equals(listToolBarButton.Items[e.Index].Text, StringComparison.OrdinalIgnoreCase))
                        {
                            bHas = true;
                            break;
                        }
                    }
                    if (!bHas)
                    {
                        CTButtonInTableInFormControl tbiwc = new CTButtonInTableInFormControl();
                        tbiwc.Title = listToolBarButton.Items[e.Index].Text;
                        tbiwc.UI_TableInFormControl_id = tiwc.Id;
                        tbiwc.Ctx = Program.Ctx;
                        tiwc.TButtonInTableInFormControlMgr.AddNew(tbiwc);
                        te.SetToolBarButtonVisible(listToolBarButton.Items[e.Index].Text, true);
                    }
                }
                else
                {
                    foreach (CBaseObject obj in lstTBIWC)
                    {
                        CTButtonInTableInFormControl tbiwc = (CTButtonInTableInFormControl)obj;
                        if (tbiwc.Title.Equals(listToolBarButton.Items[e.Index].Text, StringComparison.OrdinalIgnoreCase))
                        {
                            tiwc.TButtonInTableInFormControlMgr.Delete(tbiwc);
                            te.SetToolBarButtonVisible(listToolBarButton.Items[e.Index].Text, false);
                            break;
                        }
                    }
                }
            }
            else if (designEl.GetCtrlType() == ControlType.TableTree)
            {
                TableTreeFEl treeEl = (TableTreeFEl)ControlEl;
            }
            else if (designEl.GetCtrlType() == ControlType.TableTab)
            {
                TableTabFEl tab = (TableTabFEl)ControlEl;
                TableGridFEl te = tab.GetCurTableGridEl();
                CTableInFormControl tiwc = te.TableInFormControl;
                List<CBaseObject> lstTBIWC = tiwc.TButtonInTableInFormControlMgr.GetList();
                if (e.NewValue == CheckState.Checked)
                {
                    bool bHas = false;
                    foreach (CBaseObject obj in lstTBIWC)
                    {
                        CTButtonInTableInFormControl tbiwc = (CTButtonInTableInFormControl)obj;
                        if (tbiwc.Title.Equals(listToolBarButton.Items[e.Index].Text, StringComparison.OrdinalIgnoreCase))
                        {
                            bHas = true;
                            break;
                        }
                    }
                    if (!bHas)
                    {
                        CTButtonInTableInFormControl tbiwc = new CTButtonInTableInFormControl();
                        tbiwc.Title = listToolBarButton.Items[e.Index].Text;
                        tbiwc.UI_TableInFormControl_id = tiwc.Id;
                        tbiwc.Ctx = Program.Ctx;
                        tiwc.TButtonInTableInFormControlMgr.AddNew(tbiwc);
                        te.SetToolBarButtonVisible(listToolBarButton.Items[e.Index].Text, true);
                    }
                }
                else
                {
                    foreach (CBaseObject obj in lstTBIWC)
                    {
                        CTButtonInTableInFormControl tbiwc = (CTButtonInTableInFormControl)obj;
                        if (tbiwc.Title.Equals(listToolBarButton.Items[e.Index].Text, StringComparison.OrdinalIgnoreCase))
                        {
                            tiwc.TButtonInTableInFormControlMgr.Delete(tbiwc);
                            te.SetToolBarButtonVisible(listToolBarButton.Items[e.Index].Text, false);
                            break;
                        }
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


        void UpdateRefTable(string sCode)
        {
            CTable table = Program.Ctx.TableMgr.FindByCode(sCode);
            if (table != null)
            {
                PropertyGridComboBoxItem._hash.Clear();
                List<CBaseObject> lstCol = table.ColumnMgr.GetList();
                foreach (CBaseObject obj in lstCol)
                {
                    CColumn col = (CColumn)obj;
                    PropertyGridComboBoxItem._hash.Add(col.Code, col.Code);
                }
            }
            else
                PropertyGridComboBoxItem._hash.Clear();

            propertyGrid.Refresh();
        }

    }
}
