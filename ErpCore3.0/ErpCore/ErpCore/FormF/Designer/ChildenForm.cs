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
using ErpCore.Window.Designer;
using ErpCore.Database.Table;
using ErpCore.CommonCtrl;

namespace ErpCore.FormF.Designer
{
    public partial class ChildenForm : Form
    {
        CForm form = null;
        CTable m_Table = null;

        public ControlToolWindow m_ControlToolWindow = null;
        public AttributeToolWindow m_AttributeToolWindow = null;

        public Control m_CurSelDesignEl = null;


        private UISizeDot[] _UISizeDot;
        private const int DOT_WIDTH = 7;   //UISizeDot宽度 
        private const int DOT_HEIGHT = 7;  //UISizeDot高度 
        private const int DOT_COUNT = 8;   //要显示的UISizeDot数 

        public ChildenForm()
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
            //this._UISizeDot[0].Cursor = Cursors.SizeNWSE;
            //this._UISizeDot[1].Cursor = Cursors.SizeNS;
            //this._UISizeDot[2].Cursor = Cursors.SizeNESW;
            //this._UISizeDot[3].Cursor = Cursors.SizeWE;
            //this._UISizeDot[4].Cursor = Cursors.SizeNWSE;
            //this._UISizeDot[5].Cursor = Cursors.SizeNS;
            //this._UISizeDot[6].Cursor = Cursors.SizeNESW;
            //this._UISizeDot[7].Cursor = Cursors.SizeWE;
            //this._UISizeDot[0].Move += new EventHandler(UISizeDot_Move);
            //this._UISizeDot[1].Move += new EventHandler(UISizeDot_Move);
            //this._UISizeDot[2].Move += new EventHandler(UISizeDot_Move);
            //this._UISizeDot[3].Move += new EventHandler(UISizeDot_Move);
            //this._UISizeDot[4].Move += new EventHandler(UISizeDot_Move);
            //this._UISizeDot[5].Move += new EventHandler(UISizeDot_Move);
            //this._UISizeDot[6].Move += new EventHandler(UISizeDot_Move);
            //this._UISizeDot[7].Move += new EventHandler(UISizeDot_Move);
        }

        void UISizeDot_Move(object sender, EventArgs e)
        {
            if (m_CurSelDesignEl == null)
                return;
            UISizeDot dot = (UISizeDot)sender;
            if (!dot.m_bIsDragging)
                return;

            if (dot == this._UISizeDot[0])
            {
            }
            else if (dot == this._UISizeDot[1])
            {
                m_CurSelDesignEl.Height = this._UISizeDot[5].Bottom - dot.Top;
            }
            else if (dot == this._UISizeDot[2])
            {
                m_CurSelDesignEl.Width = dot.Right - this._UISizeDot[0].Left;
                m_CurSelDesignEl.Height = this._UISizeDot[4].Bottom - dot.Top;
            }
            else if (dot == this._UISizeDot[3])
            {
                m_CurSelDesignEl.Width = dot.Right - this._UISizeDot[7].Left;
            }
            else if (dot == this._UISizeDot[4])
            {
                m_CurSelDesignEl.Width = dot.Right - this._UISizeDot[0].Left;
                m_CurSelDesignEl.Height = dot.Bottom - this._UISizeDot[0].Top;
            }
            else if (dot == this._UISizeDot[5])
            {
                m_CurSelDesignEl.Height = dot.Bottom - this._UISizeDot[0].Top;
            }
            else if (dot == this._UISizeDot[6])
            {
                m_CurSelDesignEl.Height = dot.Bottom - this._UISizeDot[0].Top;
            }
            else if (dot == this._UISizeDot[7])
            {
            }
            //this.ShowFocusDot(m_CurSelDesignEl);
        }

        public CForm Form
        {
            get { return form; }
            set { 
                form = value;
                m_Table = (CTable)Program.Ctx.TableMgr.Find(form.FW_Table_id);
                if (m_Table == null)
                {
                    m_Table = new CTable();
                    m_Table.Ctx = Program.Ctx;
                    m_Table.Code = form.Name + "_data";
                    m_Table.Name = form.Name + "_表单数据";
                    m_Table.m_CmdType = CmdType.AddNew;
                    CColumn col = new CColumn();
                    col.Name = "id";
                    col.Code = "id";
                    col.ColType = ColumnType.guid_type;
                    col.ColLen = 16;
                    col.AllowNull = false;
                    col.IsSystem = true;
                    col.IsVisible = false;
                    col.Idx = 0;
                    m_Table.ColumnMgr.AddNew(col);
                    col = new CColumn();
                    col.Name = "创建时间";
                    col.Code = "Created";
                    col.ColType = ColumnType.datetime_type;
                    col.ColLen = 8;
                    col.DefaultValue = "getdate()";
                    col.AllowNull = true;
                    col.IsSystem = true;
                    col.IsVisible = false;
                    col.Idx = 1;
                    m_Table.ColumnMgr.AddNew(col);
                    col = new CColumn();
                    col.Name = "创建者";
                    col.Code = "Creator";
                    col.ColType = ColumnType.ref_type;
                    CTable tableUser = (CTable)Program.Ctx.TableMgr.FindByCode("B_User");
                    Guid guidUid = Guid.Empty;
                    Guid guidUname = Guid.Empty;
                    if (tableUser != null)
                    {
                        col.RefTable = tableUser.Id;
                        CColumn colUid = tableUser.ColumnMgr.FindByCode("id");
                        col.RefCol = colUid.Id;
                        guidUid = col.RefCol;
                        CColumn colUname = tableUser.ColumnMgr.FindByCode("name");
                        col.RefShowCol = colUname.Id;
                        guidUname = col.RefShowCol;
                    }
                    col.ColLen = 16;
                    col.DefaultValue = "0";
                    col.AllowNull = true;
                    col.IsSystem = true;
                    col.IsVisible = false;
                    col.Idx = 2;
                    m_Table.ColumnMgr.AddNew(col);
                    col = new CColumn();
                    col.Name = "修改时间";
                    col.Code = "Updated";
                    col.ColType = ColumnType.datetime_type;
                    col.ColLen = 8;
                    col.DefaultValue = "getdate()";
                    col.AllowNull = true;
                    col.IsSystem = true;
                    col.IsVisible = false;
                    col.Idx = 3;
                    m_Table.ColumnMgr.AddNew(col);
                    col = new CColumn();
                    col.Name = "修改者";
                    col.Code = "Updator";
                    col.ColType = ColumnType.ref_type;
                    if (tableUser != null)
                    {
                        col.RefTable = tableUser.Id;
                        col.RefCol = guidUid;
                        col.RefShowCol = guidUname;
                    }
                    col.ColLen = 16;
                    col.DefaultValue = "0";
                    col.AllowNull = true;
                    col.IsSystem = true;
                    col.IsVisible = false;
                    col.Idx = 4;
                    m_Table.ColumnMgr.AddNew(col);

                    form.FW_Table_id = m_Table.Id;
                    form.m_ObjectMgr.Update(form);

                    Program.Ctx.TableMgr.AddNew(m_Table);

                }
                else
                    Program.Ctx.TableMgr.Update(m_Table);

                if(m_AttributeToolWindow!=null)
                    m_AttributeToolWindow.m_Table = m_Table;
            }
        }

        public void OnDragDrop( ListViewItem item)
        {
            
            if (item.Text.Equals("DataGrid", StringComparison.OrdinalIgnoreCase))
            {
                SelTableForm frm = new SelTableForm();
                if (frm.ShowDialog() != DialogResult.OK)
                    return;

                string sCtrlName = GetDistinctName(frm.m_SelTable.Name, Form.FormControlMgr);

                CFormControl FormControl = new CFormControl();
                FormControl.CtrlType = (int)ControlType.TableGrid;
                FormControl.DomainType = DomainControlType.Data;
                FormControl.Ctx = Program.Ctx;
                FormControl.Name = sCtrlName;
                FormControl.UI_Form_id = Form.Id;
                Form.FormControlMgr.AddNew(FormControl);

                CTableInFormControl tiwc = new CTableInFormControl();
                tiwc.Ctx = Program.Ctx;
                tiwc.FW_Table_id = frm.m_SelTable.Id;
                tiwc.UI_FormControl_id = FormControl.Id;
                tiwc.Text = frm.m_SelTable.Name;
                FormControl.TableInFormControlMgr.AddNew(tiwc);

                TableGridFEl te = new TableGridFEl();
                te.FormControl = FormControl;
                te.TableInFormControl = tiwc;
                te.CaptionText = sCtrlName;
                te.ShowTitleBar = true;
                te.ShowToolBar = true;
                te.Tag = FormControl;
                flowPanel.Controls.Add(te);

                FormControl.Width = te.Width;
                FormControl.Height = te.Height;
                FormControl.Idx = flowPanel.Controls.Count-1;


                te.tbTitle.MouseClick += new MouseEventHandler(childCtrl_MouseClick);
                
                SelectElement(te);
            }
            else if (item.Text.Equals("TreeCtrl", StringComparison.OrdinalIgnoreCase))
            {
                TableTreeSetF frm = new TableTreeSetF();
                if (frm.ShowDialog() != DialogResult.OK)
                    return;

                string sCtrlName = GetDistinctName(frm.m_FormControl.Name, Form.FormControlMgr);

                CFormControl FormControl = frm.m_FormControl;
                FormControl.Name = sCtrlName;
                FormControl.CtrlType = (int)ControlType.TableTree;
                FormControl.DomainType = DomainControlType.Data;
                FormControl.Ctx = Program.Ctx;
                FormControl.UI_Form_id = Form.Id;
                Form.FormControlMgr.AddNew(FormControl);
                

                TableTreeFEl tt = new TableTreeFEl();
                tt.CaptionText = sCtrlName;
                tt.ShowTitleBar = true;
                tt.Tag = FormControl;
                flowPanel.Controls.Add(tt);

                FormControl.Width = tt.Width;
                FormControl.Height = tt.Height;
                FormControl.Idx = flowPanel.Controls.Count - 1;

                tt.tbTitle.MouseClick += new MouseEventHandler(childCtrl_MouseClick);

                SelectElement(tt);
            }
            else if (item.Text.Equals("TabCtrl", StringComparison.OrdinalIgnoreCase))
            {
                SelMultTableForm frm = new SelMultTableForm();
                if (frm.ShowDialog() != DialogResult.OK)
                    return;

                string sCtrlName = GetDistinctName("标签", Form.FormControlMgr);

                CFormControl FormControl = new CFormControl();
                FormControl.CtrlType = (int)ControlType.TableTab;
                FormControl.DomainType = DomainControlType.Data;
                FormControl.Ctx = Program.Ctx;
                FormControl.Name = sCtrlName;
                FormControl.UI_Form_id = Form.Id;
                Form.FormControlMgr.AddNew(FormControl);

                foreach (CTable table in frm.m_lstSelTable)
                {
                    CTableInFormControl tiwc = new CTableInFormControl();
                    tiwc.Ctx = Program.Ctx;
                    tiwc.FW_Table_id = table.Id;
                    tiwc.UI_FormControl_id = FormControl.Id;
                    tiwc.Text = table.Name;
                    FormControl.TableInFormControlMgr.AddNew(tiwc);
                }

                TableTabFEl tab = new TableTabFEl();
                tab.FormControl = FormControl;
                tab.CaptionText = sCtrlName;
                tab.ShowTitleBar = false;
                tab.Tag = FormControl;
                flowPanel.Controls.Add(tab);

                FormControl.Width = tab.Width;
                FormControl.Height = tab.Height;
                FormControl.Idx = flowPanel.Controls.Count - 1;

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

                string sCtrlName = GetDistinctName(frm.m_SelTable.Name, Form.FormControlMgr);

                CFormControl FormControl = new CFormControl();
                FormControl.CtrlType = (int)ControlType.ComboBox;
                FormControl.DomainType = DomainControlType.Data;
                FormControl.Ctx = Program.Ctx;
                FormControl.Name = sCtrlName;
                FormControl.UI_Form_id = Form.Id;
                Form.FormControlMgr.AddNew(FormControl);
                
                string sText = string.Format("[{0}]", frm2.m_SelColumn.Code);

                CTableInFormControl tiwc = new CTableInFormControl();
                tiwc.Ctx = Program.Ctx;
                tiwc.FW_Table_id = frm.m_SelTable.Id;
                tiwc.UI_FormControl_id = FormControl.Id;
                tiwc.Text = sText;
                FormControl.TableInFormControlMgr.AddNew(tiwc);

                UIComboBoxFEl cb = new UIComboBoxFEl();
                cb.CaptionText = frm.m_SelTable.Name+"：";
                cb.FormControl = FormControl;
                cb.Tag = FormControl;
                flowPanel.Controls.Add(cb);

                FormControl.Width = cb.Width;
                FormControl.Height = cb.Height;
                FormControl.Idx = flowPanel.Controls.Count - 1;

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

                string sCtrlName = GetDistinctName(frm.m_SelTable.Name, Form.FormControlMgr);

                CFormControl FormControl = new CFormControl();
                FormControl.CtrlType = (int)ControlType.ListBox;
                FormControl.DomainType = DomainControlType.Data;
                FormControl.Ctx = Program.Ctx;
                FormControl.Name = sCtrlName;
                FormControl.UI_Form_id = Form.Id;
                Form.FormControlMgr.AddNew(FormControl);


                string sText = string.Format("[{0}]",frm2.m_SelColumn.Code);
               
                CTableInFormControl tiwc = new CTableInFormControl();
                tiwc.Ctx = Program.Ctx;
                tiwc.FW_Table_id = frm.m_SelTable.Id;
                tiwc.UI_FormControl_id = FormControl.Id;
                tiwc.Text = sText;
                FormControl.TableInFormControlMgr.AddNew(tiwc);

                UIListBoxFEl listBox = new UIListBoxFEl();
                listBox.CaptionText = sCtrlName;
                listBox.FormControl = FormControl;
                listBox.Tag = FormControl;
                flowPanel.Controls.Add(listBox);

                FormControl.Width = listBox.Width;
                FormControl.Height = listBox.Height;
                FormControl.Idx = flowPanel.Controls.Count - 1;

                listBox.tbTitle.MouseClick += new MouseEventHandler(childCtrl_MouseClick);

                SelectElement(listBox);
            }
            //else if (item.Text.Equals("Label", StringComparison.OrdinalIgnoreCase))
            //{
            //    string sCtrlName = GetDistinctName("Label", Form.FormControlMgr);

            //    CFormControl FormControl = new CFormControl();
            //    FormControl.CtrlType = ControlType.Label;
            //    FormControl.Ctx = Program.Ctx;
            //    FormControl.Name = sCtrlName;
            //    FormControl.Caption = sCtrlName;
            //    FormControl.UI_Form_id = Form.Id;
            //    Form.FormControlMgr.AddNew(FormControl);

            //    UILabelFEl label = new UILabelFEl();
            //    label.FormControl = FormControl;
            //    label.Text = sCtrlName;
            //    label.Tag = FormControl;
            //    flowPanel.Controls.Add(label);

            //    FormControl.Width = label.Width;
            //    FormControl.Height = label.Height;
            //    FormControl.Idx = flowPanel.Controls.Count - 1;

            //    label.MouseClick += new MouseEventHandler(ctrl_MouseClick);

            //    SelectElement(label);
            //}
            //表单域
            else if (item.Text.Equals("文本型", StringComparison.OrdinalIgnoreCase))
            {
                string sCtrlName = GetDistinctName("StringBox", Form.FormControlMgr);

                CFormControl FormControl = new CFormControl();
                FormControl.CtrlType = (int)ColumnType.string_type;
                FormControl.DomainType = DomainControlType.Form;
                FormControl.Ctx = Program.Ctx;
                FormControl.Name = sCtrlName;
                FormControl.UI_Form_id = Form.Id;
                Form.FormControlMgr.AddNew(FormControl);

                ExTextBoxEl ctrl = new ExTextBoxEl();
                ctrl.textBox.MaxLength = 50;
                ctrl.Tag = FormControl;
                flowPanel.Controls.Add(ctrl);
                flowPanel.Refresh();

                FormControl.Caption = ctrl.lbCaption.Text;
                FormControl.Width = ctrl.Width;
                FormControl.Height = ctrl.Height;
                FormControl.Idx = flowPanel.Controls.Count - 1;

                CColumn col = new CColumn();
                col.Ctx = Program.Ctx;
                col.FW_Table_id = m_Table.Id;
                col.Name = sCtrlName;
                col.Code = sCtrlName;
                col.ColType = ColumnType.string_type;
                col.ColLen = ctrl.textBox.MaxLength;
                col.AllowNull = true;
                col.IsSystem = false;
                col.IsVisible = true;
                col.Idx = m_Table.ColumnMgr.GetList().Count;
                m_Table.ColumnMgr.AddNew(col);
                FormControl.FW_Column_id = col.Id;

                ctrl.lbCaption.MouseClick += new MouseEventHandler(childCtrl_MouseClick);
                ctrl.textBox.MouseClick += new MouseEventHandler(childCtrl_MouseClick);

                SelectElement(ctrl);
            }
            else if (item.Text.Equals("备注型", StringComparison.OrdinalIgnoreCase))
            {
                string sCtrlName = GetDistinctName("TextBox", Form.FormControlMgr);

                CFormControl FormControl = new CFormControl();
                FormControl.CtrlType = (int)ColumnType.text_type;
                FormControl.DomainType = DomainControlType.Form;
                FormControl.Ctx = Program.Ctx;
                FormControl.Name = sCtrlName;
                FormControl.UI_Form_id = Form.Id;
                Form.FormControlMgr.AddNew(FormControl);

                ExTextBoxEl ctrl = new ExTextBoxEl();
                ctrl.textBox.Multiline = true;
                ctrl.textBox.ScrollBars = ScrollBars.Both;
                ctrl.Height = 100;
                ctrl.Tag = FormControl;
                flowPanel.Controls.Add(ctrl);

                FormControl.Caption = ctrl.lbCaption.Text;
                FormControl.Width = ctrl.Width;
                FormControl.Height = ctrl.Height;
                FormControl.Idx = flowPanel.Controls.Count - 1;


                CColumn col = new CColumn();
                col.Ctx = Program.Ctx;
                col.FW_Table_id = m_Table.Id;
                col.Name = sCtrlName;
                col.Code = sCtrlName;
                col.ColType = ColumnType.text_type;
                col.ColLen = 16;
                col.AllowNull = true;
                col.IsSystem = false;
                col.IsVisible = true;
                col.Idx = m_Table.ColumnMgr.GetList().Count;
                m_Table.ColumnMgr.AddNew(col);
                FormControl.FW_Column_id = col.Id;

                ctrl.lbCaption.MouseClick += new MouseEventHandler(childCtrl_MouseClick);
                ctrl.textBox.MouseClick += new MouseEventHandler(childCtrl_MouseClick);

                SelectElement(ctrl);
            }
            else if (item.Text.Equals("整型", StringComparison.OrdinalIgnoreCase))
            {
                string sCtrlName = GetDistinctName("IntBox", Form.FormControlMgr);

                CFormControl FormControl = new CFormControl();
                FormControl.CtrlType = (int)ColumnType.int_type;
                FormControl.DomainType = DomainControlType.Form;
                FormControl.Ctx = Program.Ctx;
                FormControl.Name = sCtrlName;
                FormControl.UI_Form_id = Form.Id;
                Form.FormControlMgr.AddNew(FormControl);

                ExTextBoxEl ctrl = new ExTextBoxEl();
                ctrl.Tag = FormControl;
                flowPanel.Controls.Add(ctrl);

                FormControl.Caption = ctrl.lbCaption.Text;
                FormControl.Width = ctrl.Width;
                FormControl.Height = ctrl.Height;
                FormControl.Idx = flowPanel.Controls.Count - 1;

                CColumn col = new CColumn();
                col.Ctx = Program.Ctx;
                col.FW_Table_id = m_Table.Id;
                col.Name = sCtrlName;
                col.Code = sCtrlName;
                col.ColType = ColumnType.int_type;
                col.ColLen = 4;
                col.AllowNull = true;
                col.IsSystem = false;
                col.IsVisible = true;
                col.Idx = m_Table.ColumnMgr.GetList().Count;
                m_Table.ColumnMgr.AddNew(col);
                FormControl.FW_Column_id = col.Id;

                ctrl.lbCaption.MouseClick += new MouseEventHandler(childCtrl_MouseClick);
                ctrl.textBox.MouseClick += new MouseEventHandler(childCtrl_MouseClick);

                SelectElement(ctrl);
            }
            else if (item.Text.Equals("长整型", StringComparison.OrdinalIgnoreCase))
            {
                string sCtrlName = GetDistinctName("LongBox", Form.FormControlMgr);

                CFormControl FormControl = new CFormControl();
                FormControl.CtrlType = (int)ColumnType.long_type;
                FormControl.DomainType = DomainControlType.Form;
                FormControl.Ctx = Program.Ctx;
                FormControl.Name = sCtrlName;
                FormControl.UI_Form_id = Form.Id;
                Form.FormControlMgr.AddNew(FormControl);

                ExTextBoxEl ctrl = new ExTextBoxEl();
                ctrl.Tag = FormControl;
                flowPanel.Controls.Add(ctrl);

                FormControl.Caption = ctrl.lbCaption.Text;
                FormControl.Width = ctrl.Width;
                FormControl.Height = ctrl.Height;
                FormControl.Idx = flowPanel.Controls.Count - 1;

                CColumn col = new CColumn();
                col.Ctx = Program.Ctx;
                col.FW_Table_id = m_Table.Id;
                col.Name = sCtrlName;
                col.Code = sCtrlName;
                col.ColType = ColumnType.long_type;
                col.ColLen = 8;
                col.AllowNull = true;
                col.IsSystem = false;
                col.IsVisible = true;
                col.Idx = m_Table.ColumnMgr.GetList().Count;
                m_Table.ColumnMgr.AddNew(col);
                FormControl.FW_Column_id = col.Id;

                ctrl.lbCaption.MouseClick += new MouseEventHandler(childCtrl_MouseClick);
                ctrl.textBox.MouseClick += new MouseEventHandler(childCtrl_MouseClick);

                SelectElement(ctrl);
            }
            else if (item.Text.Equals("布尔型", StringComparison.OrdinalIgnoreCase))
            {
                string sCtrlName = GetDistinctName("CheckBox", Form.FormControlMgr);

                CFormControl FormControl = new CFormControl();
                FormControl.CtrlType = (int)ColumnType.bool_type;
                FormControl.DomainType = DomainControlType.Form;
                FormControl.Ctx = Program.Ctx;
                FormControl.Name = sCtrlName;
                FormControl.UI_Form_id = Form.Id;
                Form.FormControlMgr.AddNew(FormControl);

                ExCheckBoxEl ctrl = new ExCheckBoxEl();
                ctrl.Tag = FormControl;
                flowPanel.Controls.Add(ctrl);

                FormControl.Caption = ctrl.lbCaption.Text;
                FormControl.Width = ctrl.Width;
                FormControl.Height = ctrl.Height;
                FormControl.Idx = flowPanel.Controls.Count - 1;

                CColumn col = new CColumn();
                col.Ctx = Program.Ctx;
                col.FW_Table_id = m_Table.Id;
                col.Name = sCtrlName;
                col.Code = sCtrlName;
                col.ColType = ColumnType.bool_type;
                col.ColLen = 1;
                col.AllowNull = true;
                col.IsSystem = false;
                col.IsVisible = true;
                col.Idx = m_Table.ColumnMgr.GetList().Count;
                m_Table.ColumnMgr.AddNew(col);
                FormControl.FW_Column_id = col.Id;

                ctrl.lbCaption.MouseClick += new MouseEventHandler(childCtrl_MouseClick);
                ctrl.checkBox.MouseClick += new MouseEventHandler(childCtrl_MouseClick);

                SelectElement(ctrl);
            }
            else if (item.Text.Equals("数值型", StringComparison.OrdinalIgnoreCase))
            {
                string sCtrlName = GetDistinctName("NumberBox", Form.FormControlMgr);

                CFormControl FormControl = new CFormControl();
                FormControl.CtrlType = (int)ColumnType.numeric_type;
                FormControl.DomainType = DomainControlType.Form;
                FormControl.Ctx = Program.Ctx;
                FormControl.Name = sCtrlName;
                FormControl.UI_Form_id = Form.Id;
                Form.FormControlMgr.AddNew(FormControl);

                ExTextBoxEl ctrl = new ExTextBoxEl();
                ctrl.Tag = FormControl;
                flowPanel.Controls.Add(ctrl);

                FormControl.Caption = ctrl.lbCaption.Text;
                FormControl.Width = ctrl.Width;
                FormControl.Height = ctrl.Height;
                FormControl.Idx = flowPanel.Controls.Count - 1;

                CColumn col = new CColumn();
                col.Ctx = Program.Ctx;
                col.FW_Table_id = m_Table.Id;
                col.Name = sCtrlName;
                col.Code = sCtrlName;
                col.ColType = ColumnType.numeric_type;
                col.ColLen = 16;
                col.AllowNull = true;
                col.IsSystem = false;
                col.IsVisible = true;
                col.Idx = m_Table.ColumnMgr.GetList().Count;
                m_Table.ColumnMgr.AddNew(col);
                FormControl.FW_Column_id = col.Id;

                ctrl.lbCaption.MouseClick += new MouseEventHandler(childCtrl_MouseClick);
                ctrl.textBox.MouseClick += new MouseEventHandler(childCtrl_MouseClick);

                SelectElement(ctrl);
            }
            else if (item.Text.Equals("引用型", StringComparison.OrdinalIgnoreCase))
            {

                SelTableForm frm = new SelTableForm();
                if (frm.ShowDialog() != DialogResult.OK)
                    return;
                SelColumnForm frm2 = new SelColumnForm();
                frm2.m_Table = frm.m_SelTable;
                if (frm2.ShowDialog() != DialogResult.OK)
                    return;

                string sCtrlName = GetDistinctName("RefBox", Form.FormControlMgr);

                CFormControl FormControl = new CFormControl();
                FormControl.CtrlType = (int)ColumnType.ref_type;
                FormControl.DomainType = DomainControlType.Form;
                FormControl.Ctx = Program.Ctx;
                FormControl.Name = sCtrlName;
                FormControl.UI_Form_id = Form.Id;
                Form.FormControlMgr.AddNew(FormControl);

                ExComboBoxEl ctrl = new ExComboBoxEl();
                ctrl.Tag = FormControl;
                flowPanel.Controls.Add(ctrl);

                FormControl.Caption = ctrl.lbCaption.Text;
                FormControl.Width = ctrl.Width;
                FormControl.Height = ctrl.Height;
                FormControl.Idx = flowPanel.Controls.Count - 1;

                CColumn col = new CColumn();
                col.Ctx = Program.Ctx;
                col.FW_Table_id = m_Table.Id;
                col.Name = sCtrlName;
                col.Code = sCtrlName;
                col.ColType = ColumnType.ref_type;
                col.ColLen = 16;
                col.RefTable = frm.m_SelTable.Id;
                col.RefCol = frm.m_SelTable.ColumnMgr.FindByCode("id").Id;
                col.RefShowCol = frm2.m_SelColumn.Id;
                col.AllowNull = true;
                col.IsSystem = false;
                col.IsVisible = true;
                col.Idx = m_Table.ColumnMgr.GetList().Count;
                m_Table.ColumnMgr.AddNew(col);
                FormControl.FW_Column_id = col.Id;

                ctrl.lbCaption.MouseClick += new MouseEventHandler(childCtrl_MouseClick);
                ctrl.comboBox.MouseClick += new MouseEventHandler(childCtrl_MouseClick);

                SelectElement(ctrl);
            }
            else if (item.Text.Equals("日期型", StringComparison.OrdinalIgnoreCase))
            {
                string sCtrlName = GetDistinctName("DateTimeBox", Form.FormControlMgr);

                CFormControl FormControl = new CFormControl();
                FormControl.CtrlType = (int)ColumnType.datetime_type;
                FormControl.DomainType = DomainControlType.Form;
                FormControl.Ctx = Program.Ctx;
                FormControl.Name = sCtrlName;
                FormControl.UI_Form_id = Form.Id;
                Form.FormControlMgr.AddNew(FormControl);

                ExDateTimePickerEl ctrl = new ExDateTimePickerEl();
                ctrl.Tag = FormControl;
                flowPanel.Controls.Add(ctrl);

                FormControl.Caption = ctrl.lbCaption.Text;
                FormControl.Width = ctrl.Width;
                FormControl.Height = ctrl.Height;
                FormControl.Idx = flowPanel.Controls.Count - 1;

                CColumn col = new CColumn();
                col.Ctx = Program.Ctx;
                col.FW_Table_id = m_Table.Id;
                col.Name = sCtrlName;
                col.Code = sCtrlName;
                col.ColType = ColumnType.datetime_type;
                col.ColLen = 16;
                col.AllowNull = true;
                col.IsSystem = false;
                col.IsVisible = true;
                col.Idx = m_Table.ColumnMgr.GetList().Count;
                m_Table.ColumnMgr.AddNew(col);
                FormControl.FW_Column_id = col.Id;

                ctrl.lbCaption.MouseClick += new MouseEventHandler(childCtrl_MouseClick);
                ctrl.dateTimePicker.MouseClick += new MouseEventHandler(childCtrl_MouseClick);

                SelectElement(ctrl);
            }
            
        }
        public static string GetDistinctName(string sName, CFormControlMgr FormControlMgr)
        {
            int idx = 2;
            string sRetName = sName;
            List<CBaseObject> lstObj = FormControlMgr.GetList();
            while (true)
            {
                bool bHas = false;
                foreach (CBaseObject obj in lstObj)
                {
                    CFormControl FormControl = (CFormControl)obj;
                    if (FormControl.Name.Equals(sRetName, StringComparison.OrdinalIgnoreCase))
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

            ShowFocusDot(ctrlEl);

            if (m_CurSelDesignEl == null)
                return;

            //IDesignEl designEl =(IDesignEl)m_CurSelDesignEl;
            //CFormControl FormControl = (CFormControl)m_CurSelDesignEl.Tag;
            //if (FormControl.m_objTempData == null)
            //{
            //    TableGridFProp Setting = new TableGridFProp();
            //    Setting.宽度 = m_CurSelDesignEl.Width;
            //    Setting.高度 = m_CurSelDesignEl.Height;
            //    if (designEl.GetCtrlType() == ControlType.TableGrid)
            //    {
            //        TableGridFEl te = (TableGridFEl)designEl;

            //        Setting.工具栏显示 = te.TableInFormControl.ShowToolBar;
            //        Setting.标题栏显示 = te.TableInFormControl.ShowTitleBar;
            //    }
            //    else if (designEl.GetCtrlType() == ControlType.TableTree)
            //    {
            //    }
            //    FormControl.m_objTempData = Setting;
            //}
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
            //rc = this.RectangleToClient(ctrlEl.RectangleToScreen(rc));

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

            if (m_AttributeToolWindow != null)
                m_AttributeToolWindow.m_Table = m_Table;
        }
        void LoadData()
        {
            flowPanel.Controls.Clear();
            if (Form == null)
                return;
            
            this.Width = Form.Width;
            this.Height = Form.Height;

            List<CBaseObject> lstFormControl = Form.FormControlMgr.GetList("","idx");
            foreach (CBaseObject obj in lstFormControl)
            {
                CFormControl FormControl = (CFormControl)obj;

                if (FormControl.DomainType == DomainControlType.Data)
                {
                    if (FormControl.CtrlType == (int)ControlType.TableGrid)
                    {
                        if (FormControl.TableInFormControlMgr.GetList().Count == 0)
                            continue;
                        CTableInFormControl TableInFormControl = (CTableInFormControl)FormControl.TableInFormControlMgr.GetFirstObj();
                        CTable table = (CTable)Program.Ctx.TableMgr.Find(TableInFormControl.FW_Table_id);
                        if (table == null)
                            continue;

                        TableGridFEl te = new TableGridFEl();
                        te.FormControl = FormControl;
                        te.Name = table.Code;
                        te.TableInFormControl = TableInFormControl;
                        te.ShowToolBar = TableInFormControl.ShowToolBar;
                        te.ShowTitleBar = TableInFormControl.ShowTitleBar;
                        te.CaptionText = FormControl.Name;
                        te.Tag = FormControl;
                        te.Width = FormControl.Width;
                        te.Height = FormControl.Height;
                        flowPanel.Controls.Add(te);


                        te.tbTitle.MouseClick += new MouseEventHandler(childCtrl_MouseClick);
                        te.toolStrip.MouseClick += new MouseEventHandler(childCtrl_MouseClick);
                        te.dataGridView.GotFocus += new EventHandler(ctrl_GotFocus);
                    }
                    else if (FormControl.CtrlType == (int)ControlType.TableTree)
                    {
                        TableTreeFEl tt = new TableTreeFEl();
                        tt.CaptionText = FormControl.Name;
                        tt.ShowTitleBar = FormControl.ShowTitleBar;
                        tt.FormControl = FormControl;
                        tt.Tag = FormControl;
                        tt.Width = FormControl.Width;
                        tt.Height = FormControl.Height;
                        flowPanel.Controls.Add(tt);

                        tt.tbTitle.MouseClick += new MouseEventHandler(childCtrl_MouseClick);
                        tt.treeView.GotFocus += new EventHandler(ctrl_GotFocus);
                        tt.treeView.ContextMenuStrip = contextMenuStrip1;
                    }
                    else if (FormControl.CtrlType == (int)ControlType.TableTab)
                    {
                        TableTabFEl tab = new TableTabFEl();
                        tab.FormControl = FormControl;
                        tab.CaptionText = "TabCtrl";
                        tab.ShowTitleBar = FormControl.ShowTitleBar;
                        tab.Tag = FormControl;
                        tab.Width = FormControl.Width;
                        tab.Height = FormControl.Height;
                        flowPanel.Controls.Add(tab);

                        tab.tabControl.MouseClick += new MouseEventHandler(childCtrl_MouseClick);
                    }
                    else if (FormControl.CtrlType == (int)ControlType.ComboBox)
                    {
                        UIComboBoxFEl cb = new UIComboBoxFEl();
                        cb.CaptionText = FormControl.Name + "：";
                        cb.FormControl = FormControl;
                        cb.ShowTitleBar = FormControl.ShowTitleBar;
                        cb.Tag = FormControl;
                        cb.Width = FormControl.Width;
                        cb.Height = FormControl.Height;
                        flowPanel.Controls.Add(cb);

                        cb.lbTitle.MouseClick += new MouseEventHandler(childCtrl_MouseClick);
                        cb.comboBox.MouseClick += new MouseEventHandler(childCtrl_MouseClick);
                    }
                    else if (FormControl.CtrlType == (int)ControlType.ListBox)
                    {

                        UIListBoxFEl listBox = new UIListBoxFEl();
                        listBox.CaptionText = FormControl.Name;
                        listBox.FormControl = FormControl;
                        listBox.ShowTitleBar = FormControl.ShowTitleBar;
                        listBox.Tag = FormControl;
                        listBox.Width = FormControl.Width;
                        listBox.Height = FormControl.Height;
                        flowPanel.Controls.Add(listBox);

                        listBox.tbTitle.MouseClick += new MouseEventHandler(childCtrl_MouseClick);
                        listBox.listBox.MouseClick += new MouseEventHandler(childCtrl_MouseClick);
                        listBox.ContextMenuStrip = contextMenuStrip1;
                    }
                    //else if (FormControl.CtrlType == ControlType.Label)
                    //{

                    //    UILabelFEl label = new UILabelFEl();
                    //    label.Text = FormControl.Caption;
                    //    label.FormControl = FormControl;
                    //    label.Tag = FormControl;
                    //    label.Width = FormControl.Width;
                    //    label.Height = FormControl.Height;
                    //    flowPanel.Controls.Add(label);

                    //    label.MouseClick += new MouseEventHandler(ctrl_MouseClick);
                    //}
                    //else if (FormControl.CtrlType == ControlType.TextBox)
                    //{

                    //    UITextBoxFEl textBox = new UITextBoxFEl();
                    //    textBox.Text = FormControl.Caption;
                    //    textBox.FormControl = FormControl;
                    //    textBox.Tag = FormControl;
                    //    textBox.Width = FormControl.Width;
                    //    textBox.Height = FormControl.Height;
                    //    flowPanel.Controls.Add(textBox);

                    //    textBox.MouseClick += new MouseEventHandler(ctrl_MouseClick);
                    //}
                }
                else //if(FormControl.DomainType== DomainControlType.Form)
                {
                    if (FormControl.CtrlType == (int)ColumnType.string_type)
                    {
                        ExTextBoxEl ctrl = new ExTextBoxEl();
                        CColumn col = (CColumn)m_Table.ColumnMgr.Find(FormControl.FW_Column_id);
                        if(col!=null)
                            ctrl.textBox.MaxLength = col.ColLen;
                        ctrl.Width = FormControl.Width;
                        ctrl.Height = FormControl.Height;
                        ctrl.lbCaption.Text = FormControl.Caption;
                        ctrl.Tag = FormControl;
                        flowPanel.Controls.Add(ctrl);
                        flowPanel.Refresh();
                                                
                        ctrl.lbCaption.MouseClick += new MouseEventHandler(childCtrl_MouseClick);
                        ctrl.textBox.MouseClick += new MouseEventHandler(childCtrl_MouseClick);
                    }
                    else if (FormControl.CtrlType == (int)ColumnType.text_type)
                    {
                        ExTextBoxEl ctrl = new ExTextBoxEl();
                        ctrl.Width = FormControl.Width;
                        ctrl.Height = FormControl.Height;
                        ctrl.lbCaption.Text = FormControl.Caption;
                        ctrl.Tag = FormControl;
                        flowPanel.Controls.Add(ctrl);
                        flowPanel.Refresh();
                                                
                        ctrl.lbCaption.MouseClick += new MouseEventHandler(childCtrl_MouseClick);
                        ctrl.textBox.MouseClick += new MouseEventHandler(childCtrl_MouseClick);
                    }
                    else if (FormControl.CtrlType == (int)ColumnType.int_type)
                    {
                        ExTextBoxEl ctrl = new ExTextBoxEl();
                        ctrl.Width = FormControl.Width;
                        ctrl.Height = FormControl.Height;
                        ctrl.lbCaption.Text = FormControl.Caption;
                        ctrl.Tag = FormControl;
                        flowPanel.Controls.Add(ctrl);
                        flowPanel.Refresh();

                        ctrl.lbCaption.MouseClick += new MouseEventHandler(childCtrl_MouseClick);
                        ctrl.textBox.MouseClick += new MouseEventHandler(childCtrl_MouseClick);
                    }
                    else if (FormControl.CtrlType == (int)ColumnType.long_type)
                    {
                        ExTextBoxEl ctrl = new ExTextBoxEl();
                        ctrl.Width = FormControl.Width;
                        ctrl.Height = FormControl.Height;
                        ctrl.lbCaption.Text = FormControl.Caption;
                        ctrl.Tag = FormControl;
                        flowPanel.Controls.Add(ctrl);
                        flowPanel.Refresh();

                        ctrl.lbCaption.MouseClick += new MouseEventHandler(childCtrl_MouseClick);
                        ctrl.textBox.MouseClick += new MouseEventHandler(childCtrl_MouseClick);
                    }
                    else if (FormControl.CtrlType == (int)ColumnType.numeric_type)
                    {
                        ExTextBoxEl ctrl = new ExTextBoxEl();
                        ctrl.Width = FormControl.Width;
                        ctrl.Height = FormControl.Height;
                        ctrl.lbCaption.Text = FormControl.Caption;
                        ctrl.Tag = FormControl;
                        flowPanel.Controls.Add(ctrl);
                        flowPanel.Refresh();

                        ctrl.lbCaption.MouseClick += new MouseEventHandler(childCtrl_MouseClick);
                        ctrl.textBox.MouseClick += new MouseEventHandler(childCtrl_MouseClick);
                    }
                    else if (FormControl.CtrlType == (int)ColumnType.bool_type)
                    {
                        ExCheckBoxEl ctrl = new ExCheckBoxEl();
                        ctrl.Width = FormControl.Width;
                        ctrl.Height = FormControl.Height;
                        ctrl.lbCaption.Text = FormControl.Caption;
                        ctrl.Tag = FormControl;
                        flowPanel.Controls.Add(ctrl);
                        flowPanel.Refresh();

                        ctrl.lbCaption.MouseClick += new MouseEventHandler(childCtrl_MouseClick);
                        ctrl.checkBox.MouseClick += new MouseEventHandler(childCtrl_MouseClick);
                    }
                    else if (FormControl.CtrlType == (int)ColumnType.datetime_type)
                    {
                        ExDateTimePickerEl ctrl = new ExDateTimePickerEl();
                        ctrl.Width = FormControl.Width;
                        ctrl.Height = FormControl.Height;
                        ctrl.lbCaption.Text = FormControl.Caption;
                        ctrl.Tag = FormControl;
                        flowPanel.Controls.Add(ctrl);
                        flowPanel.Refresh();

                        ctrl.lbCaption.MouseClick += new MouseEventHandler(childCtrl_MouseClick);
                        ctrl.dateTimePicker.MouseClick += new MouseEventHandler(childCtrl_MouseClick);
                    }
                    else if (FormControl.CtrlType == (int)ColumnType.ref_type)
                    {
                        ExComboBoxEl ctrl = new ExComboBoxEl();
                        ctrl.Width = FormControl.Width;
                        ctrl.Height = FormControl.Height;
                        ctrl.lbCaption.Text = FormControl.Caption;
                        ctrl.Tag = FormControl;
                        flowPanel.Controls.Add(ctrl);
                        flowPanel.Refresh();

                        ctrl.lbCaption.MouseClick += new MouseEventHandler(childCtrl_MouseClick);
                        ctrl.comboBox.MouseClick += new MouseEventHandler(childCtrl_MouseClick);
                    }
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
            if (!CTable.CreateDataTable(m_Table))
            {
                MessageBox.Show(Program.Ctx.LastError, "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            //if (!m_Table.Save(true))
            Program.Ctx.TableMgr.Update(m_Table);
            if (!Program.Ctx.TableMgr.Save(true))
            {
                MessageBox.Show("保存失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            Form.m_CmdType = CmdType.Update;
            if (!Form.Save(true))
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
            CFormControl FormControl = (CFormControl)m_CurSelDesignEl.Tag;
            FormControl.m_ObjectMgr.Delete(FormControl);
            m_CurSelDesignEl.Parent.Controls.Remove(m_CurSelDesignEl);
            m_CurSelDesignEl = null;
            SelectElement(null);
        }


        private void flowPanel_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ListViewItem)))
            {
                ListViewItem item = (ListViewItem)e.Data.GetData(typeof(ListViewItem));

                OnDragDrop( item);
            }

        }

        private void flowPanel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ListViewItem)))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;

        }

        private void flowPanel_Resize(object sender, EventArgs e)
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
            m_AttributeToolWindow.FormEl = this;
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

        private void flowPanel_Layout(object sender, LayoutEventArgs e)
        {
            SelectElement(m_CurSelDesignEl);
        }


    }
}
