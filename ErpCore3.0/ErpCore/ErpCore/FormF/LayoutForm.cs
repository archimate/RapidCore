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

namespace ErpCore.FormF
{
    public partial class LayoutForm : Form
    {
        CForm formF = null;

        public LayoutForm()
        {
            InitializeComponent();
        }

        public CForm FormF
        {
            get { return formF; }
            set { formF = value; }
        }

        private void LayoutForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        void LoadData()
        {
            if (FormF == null)
                return;

            this.Width = FormF.Width;
            this.Height = FormF.Height;

            List<CBaseObject> lstFormControl = FormF.FormControlMgr.GetList();
            foreach (CBaseObject obj in lstFormControl)
            {
                CFormControl FormControl = (CFormControl)obj;


                if (FormControl.CtrlType == (int)ControlType.TableGrid)
                {
                    if (FormControl.TableInFormControlMgr.GetList().Count == 0)
                        continue;
                    CTableInFormControl TableInFormControl = (CTableInFormControl)FormControl.TableInFormControlMgr.GetFirstObj();
                    CTable table = (CTable)Program.Ctx.TableMgr.Find(TableInFormControl.FW_Table_id);
                    if (table == null)
                        continue;

                    CBaseObjectMgr objMgr = new CBaseObjectMgr();
                    objMgr.TbCode = table.Code;
                    objMgr.Ctx = Program.Ctx;
                    TableGridF te = new TableGridF();
                    te.TableInFormControl = TableInFormControl;
                    te.BaseObjectMgr = objMgr;
                    te.Name = FormControl.Name;
                    te.ShowToolBar = TableInFormControl.ShowToolBar;
                    te.ShowTitleBar = TableInFormControl.ShowTitleBar;
                    te.CaptionText = FormControl.Name;
                    te.Tag = FormControl;
                    flowPanel.Controls.Add(te);

                    te.dataGridView.CellClick += new DataGridViewCellEventHandler(dataGridView_CellClick);
                    
                }
                else if (FormControl.CtrlType == (int)ControlType.TableTree)
                {

                    TableTreeF tt = new TableTreeF();
                    tt.CaptionText = FormControl.Name;
                    tt.ShowTitleBar = FormControl.ShowTitleBar;
                    tt.FormControl = FormControl;
                    tt.Name = FormControl.Name;
                    tt.Tag = FormControl;
                    flowPanel.Controls.Add(tt);
                    tt.Dock = DockStyle.Fill;
                    tt.BringToFront();

                    tt.treeView.NodeMouseClick += new TreeNodeMouseClickEventHandler(treeView_NodeMouseClick);
                }
                else if (FormControl.CtrlType == (int)ControlType.TableTab)
                {
                    TableTabF tab = new TableTabF();
                    tab.FormControl = FormControl;
                    tab.CaptionText = FormControl.Name;
                    tab.Name = FormControl.Name;
                    tab.ShowTitleBar = FormControl.ShowTitleBar;
                    tab.Tag = FormControl;
                    flowPanel.Controls.Add(tab);
                    tab.Dock = DockStyle.Fill;
                    tab.BringToFront();

                }
                else if (FormControl.CtrlType == (int)ControlType.ComboBox)
                {
                    if (FormControl.TableInFormControlMgr.GetList().Count == 0)
                        continue;
                    CTableInFormControl TableInFormControl = (CTableInFormControl)FormControl.TableInFormControlMgr.GetFirstObj();
                    CTable table = (CTable)Program.Ctx.TableMgr.Find(TableInFormControl.FW_Table_id);
                    if (table == null)
                        continue;

                    CBaseObjectMgr objMgr = new CBaseObjectMgr();
                    objMgr.TbCode = table.Code;
                    objMgr.Ctx = Program.Ctx;

                    UIComboBoxF cb = new UIComboBoxF();
                    cb.CaptionText = FormControl.Name + "：";
                    cb.TableInFormControl = TableInFormControl;
                    cb.BaseObjectMgr = objMgr;
                    cb.Name = FormControl.Name;
                    cb.Tag = FormControl;
                    flowPanel .Controls.Add(cb);
                    cb.Dock = DockStyle.Top;
                    cb.BringToFront();

                    cb.comboBox.SelectedIndexChanged += new EventHandler(comboBox_SelectedIndexChanged);
                }
                else if (FormControl.CtrlType == (int)ControlType.ListBox)
                {
                    if (FormControl.TableInFormControlMgr.GetList().Count == 0)
                        continue;
                    CTableInFormControl TableInFormControl = (CTableInFormControl)FormControl.TableInFormControlMgr.GetFirstObj();
                    CTable table = (CTable)Program.Ctx.TableMgr.Find(TableInFormControl.FW_Table_id);
                    if (table == null)
                        continue;

                    CBaseObjectMgr objMgr = new CBaseObjectMgr();
                    objMgr.TbCode = table.Code;
                    objMgr.Ctx = Program.Ctx;

                    UIListBoxF listBox = new UIListBoxF();
                    listBox.CaptionText = FormControl.Name;
                    listBox.TableInFormControl = TableInFormControl;
                    listBox.BaseObjectMgr = objMgr;
                    listBox.Name = FormControl.Name;
                    listBox.Tag = FormControl;
                    flowPanel.Controls.Add(listBox);
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
            CFormControl FormControl = (CFormControl)cb.Tag;

            //RefreshLinkageWindowControl(FormControl);
        }

        void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox list = (ListBox)sender;
            UIListBoxF lb = (UIListBoxF)list.Parent;
            CFormControl FormControl = (CFormControl)lb.Tag;

           // RefreshLinkageWindowControl(FormControl);
        }

        void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeView tree = (TreeView)sender;
            tree.SelectedNode = e.Node;

            TableTreeF tt = (TableTreeF)tree.Parent;
            CFormControl FormControl = (CFormControl)tt.Tag;

            //RefreshLinkageWindowControl(FormControl);
        }

        //void RefreshLinkageWindowControl(CWindowControl WindowControl)
        //{
        //    List<Control> lstCtrls = new List<Control>();
        //    foreach (Control ctrl in panelTop.Controls)
        //        lstCtrls.Add(ctrl);
        //    foreach (Control ctrl in panelBottom.Controls)
        //        lstCtrls.Add(ctrl);
        //    foreach (Control ctrl in panelLeft.Controls)
        //        lstCtrls.Add(ctrl);
        //    foreach (Control ctrl in panelRight.Controls)
        //        lstCtrls.Add(ctrl);
        //    foreach (Control ctrl in panelFill.Controls)
        //        lstCtrls.Add(ctrl);

        //    List<CBaseObject> lstLT = WindowControl.LinkageWindowControlMgr.GetList();
        //    foreach (CBaseObject obj in lstLT)
        //    {
        //        CLinkageWindowControl LinkageWindowControl = (CLinkageWindowControl)obj;
        //        CWindowControl swc = (CWindowControl)WindowControl.m_ObjectMgr.Find(LinkageWindowControl.SlaveID);
        //        if (swc != null)
        //        {
        //            foreach (Control ctrl in lstCtrls)
        //            {
        //                if ((CWindowControl)(ctrl.Tag) == swc)
        //                {
        //                    IWindowCtrl winCtrl = (IWindowCtrl)ctrl;
        //                    if (winCtrl.GetCtrlType() == ControlType.TableGrid)
        //                    {
        //                        TableGrid teLT = (TableGrid)ctrl;

        //                        CTableInWindowControl tiw = teLT.TableInWindowControl;
        //                        string sQueryFilter = tiw.QueryFilter;
        //                        if (CalcUIFormula(ref sQueryFilter))
        //                        {
        //                            teLT.BaseObjectMgr.GetList(sQueryFilter);
        //                            teLT.LoadData();
        //                        }
        //                    }
        //                    else if (winCtrl.GetCtrlType() == ControlType.TableTab)
        //                    {
        //                        TableTab tabLT = (TableTab)ctrl;
        //                        foreach (TabPage page in tabLT.tabControl.TabPages)
        //                        {
        //                            TableGrid teLT = (TableGrid)page.Controls[0];
        //                            CTableInWindowControl tiw = teLT.TableInWindowControl;
        //                            string sQueryFilter = tiw.QueryFilter;
        //                            if (CalcUIFormula(ref sQueryFilter))
        //                            {
        //                                teLT.BaseObjectMgr.GetList(sQueryFilter);
        //                                teLT.LoadData();
        //                            }
        //                        }
        //                    }
        //                    else if (winCtrl.GetCtrlType() == ControlType.ListBox)
        //                    {
        //                        UIListBox listBoxLT = (UIListBox)ctrl;

        //                        CTableInWindowControl tiw = listBoxLT.TableInWindowControl;
        //                        string sQueryFilter = tiw.QueryFilter;
        //                        if (CalcUIFormula(ref sQueryFilter))
        //                        {
        //                            listBoxLT.BaseObjectMgr.GetList(sQueryFilter);
        //                            listBoxLT.LoadData();
        //                        }
        //                    }
        //                    else if (winCtrl.GetCtrlType() == ControlType.ComboBox)
        //                    {
        //                        UIComboBox combBoxLT = (UIComboBox)ctrl;

        //                        CTableInWindowControl tiw = combBoxLT.TableInWindowControl;
        //                        string sQueryFilter = tiw.QueryFilter;
        //                        if (CalcUIFormula(ref sQueryFilter))
        //                        {
        //                            combBoxLT.BaseObjectMgr.GetList(sQueryFilter);
        //                            combBoxLT.LoadData();
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            DataGridView dv = (DataGridView)sender;
            TableGridF te = (TableGridF)dv.Parent;
            CFormControl FormControl = (CFormControl)te.Tag;

            //RefreshLinkageWindowControl(WindowControl);
        }

        //计算界面公式: UIValue('控件名','字段名')
        bool CalcUIFormula(ref string sQueryFilter)
        {
            SortedList<string, Control> sortCtrls = new SortedList<string, Control>();
            foreach (Control ctrl in flowPanel.Controls)
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
