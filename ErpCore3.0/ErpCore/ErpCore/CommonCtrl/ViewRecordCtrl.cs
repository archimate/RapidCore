using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;

using System.Text;
using System.Windows.Forms;
using System.Reflection;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.UI;
using ErpCore.Util;

namespace ErpCore.CommonCtrl
{
    public partial class ViewRecordCtrl : UserControl
    {
        CBaseObjectMgr baseObjectMgr = null;
        CBaseObject baseObject = null;
        CView view = null;
        //受限的字段：禁止或者只读权限
        SortedList<Guid, AccessType> m_sortRestrictColumnAccessType = new SortedList<Guid, AccessType>();

        public ViewRecordCtrl()
        {
            InitializeComponent();
        }
        public CView View
        {
            get
            {
                return view;
            }
            set
            {
                view = value;
                LoadUI();
            }
        }
        public CBaseObjectMgr BaseObjectMgr
        {
            get
            {
                return baseObjectMgr;
            }
            set
            {
                baseObjectMgr = value;
                LoadUI();
            }
        }
        public CBaseObject BaseObject
        {
            get
            {
                return baseObject;
            }
            set
            {
                baseObject = value;
                LoadUI();
            }
        }

        private void RecordCtrl_Load(object sender, EventArgs e)
        {
            LoadUI();
        }
        void LoadUI()
        {
            if (View == null)
                return;
            if (BaseObjectMgr == null )
                return;
            m_sortRestrictColumnAccessType = Program.User.GetRestrictColumnAccessTypeList(BaseObjectMgr.Table);

            this.Controls.Clear();
            List<CBaseObject> lstCIV = View.ColumnInViewMgr.GetList();
            int nTop = 5;
            foreach (CBaseObject obj in lstCIV)
            {
                CColumnInView civ = (CColumnInView)obj;
                CColumn col = (CColumn)BaseObjectMgr.Table.ColumnMgr.Find(civ.FW_Column_id);
                if (col == null) continue;
                //判断禁止权限字段
                bool bReadOnly = false;
                if (m_sortRestrictColumnAccessType.ContainsKey(col.Id))
                {
                    AccessType accessType = m_sortRestrictColumnAccessType[col.Id];
                    if (accessType == AccessType.forbide)
                        continue;
                    else if (accessType == AccessType.read)
                        bReadOnly = true;
                }
                //

                Control ctrl = ColumnMapControl(col);
                if (ctrl != null)
                {
                    ctrl.Name = col.Code;
                    ctrl.Left = 10;
                    ctrl.Top = nTop;
                    //ctrl.Width = lbCaption.Width;
                    //ctrl.Height = lbCaption.Height;
                    this.Controls.Add(ctrl);
                    //只读权限
                    if (bReadOnly)
                        ctrl.Enabled = false;
                }
                nTop = ctrl.Bottom + 5;
            }
            //具有默认值的字段            
            List<CBaseObject> lstObj = View.ColumnDefaultValInViewMgr.GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CColumnDefaultValInView cdviv = (CColumnDefaultValInView)obj;
                CColumn col = (CColumn)baseObjectMgr.Table.ColumnMgr.Find(cdviv.FW_Column_id);
                if (col == null) continue;
                if (View.ColumnInViewMgr.FindByColumn(col.Id) != null) //在视图列中
                {
                    Control[] ctrls = this.Controls.Find(col.Code, true);
                    if (ctrls.Length > 0)
                    {
                        if (baseObject == null) //新建
                            ((IColumnCtrl)ctrls[0]).SetValue(GetColumnDefaultVal(cdviv));
                        if (cdviv.ReadOnly)
                            ctrls[0].Enabled = false;
                    }
                }
                else  //不在视图列中
                {
                    Control ctrl = ColumnMapControl(col);
                    if (ctrl != null)
                    {
                        ctrl.Name = col.Code;
                        if (baseObject == null) //新建
                            ((IColumnCtrl)ctrl).SetValue(GetColumnDefaultVal(cdviv));
                        ctrl.Visible = false; //作为隐藏字段
                        this.Controls.Add(ctrl);
                    }
                }
            }
            //
            this.Height = nTop;
        }

        object GetColumnDefaultVal(CColumnDefaultValInView cdviv)
        {
            if (cdviv.DefaultVal.Trim() == "")
                return "";
            //变量
            if (cdviv.DefaultVal.Length > 2 && cdviv.DefaultVal[0] == '[' && cdviv.DefaultVal[cdviv.DefaultVal.Length - 1] == ']')
            {
                CVariable Variable = new CVariable();
                return Variable.GetVarValue(cdviv.DefaultVal);
            }
            //sql语句
            else if (cdviv.DefaultVal.Length > 4 && cdviv.DefaultVal.Substring(0, 4).Equals("sql:", StringComparison.OrdinalIgnoreCase))
            {
                string sSql = cdviv.DefaultVal.Substring(4);
                return Program.Ctx.MainDB.GetSingle(sSql);
            }
            //常量
            else
                return cdviv.DefaultVal;
        }


        Control ColumnMapControl(CColumn col)
        {
            //自扩展类优先
            if (!string.IsNullOrEmpty( col.UIControl))
            {
                try
                {
                    object obj = Activator.CreateInstance(Type.GetType(col.UIControl));
                    IColumnCtrl ctrl = (IColumnCtrl)obj;
                    ctrl.SetCaption(col.Name + "：");
                    if (BaseObject != null)
                        ctrl.SetValue(BaseObject.GetColValue(col));
                    return (Control)ctrl;
                }
                catch 
                {
                }
            }
            switch (col.ColType)
            {
                case ColumnType.string_type:
                    {
                        ExTextBox ctrl = new ExTextBox();
                        ctrl.Width = 300;
                        ctrl.SetCaption(col.Name + "：");
                        if (BaseObject != null)
                            ctrl.SetValue(BaseObject.GetColValue(col));
                        return ctrl;
                        break;
                    }
                case ColumnType.text_type:
                    {
                        ExTextBox ctrl = new ExTextBox();
                        ctrl.textBox.Multiline = true;
                        ctrl.textBox.ScrollBars = ScrollBars.Both;
                        ctrl.Width = 300;
                        ctrl.Height = 100;
                        ctrl.SetCaption(col.Name + "：");
                        if (BaseObject != null)
                            ctrl.SetValue(BaseObject.GetColValue(col));
                        return ctrl;
                        break;
                    }
                case ColumnType.int_type:
                    {
                        ExTextBox ctrl = new ExTextBox();
                        ctrl.Width = 300;
                        ctrl.SetCaption(col.Name + "：");
                        if (BaseObject != null)
                            ctrl.SetValue(BaseObject.GetColValue(col));
                        return ctrl;
                        break;
                    }
                case ColumnType.ref_type:
                    {
                        ExComboBox ctrl = new ExComboBox();
                        ctrl.Width = 300;
                        CTable table = (CTable)Program.Ctx.TableMgr.Find(col.RefTable);
                        CBaseObjectMgr BaseObjectMgr = new CBaseObjectMgr();
                        BaseObjectMgr.TbCode = table.Code;
                        BaseObjectMgr.Ctx = Program.Ctx;

                        CColumn RefCol = (CColumn)table.ColumnMgr.Find(col.RefCol);
                        CColumn RefShowCol = (CColumn)table.ColumnMgr.Find(col.RefShowCol);
                        List<CBaseObject> lstObj = BaseObjectMgr.GetList();
                        foreach (CBaseObject obj in lstObj)
                        {
                            DataItem item = new DataItem();
                            item.name = obj.GetColValue(RefShowCol).ToString();
                            item.Data = obj.GetColValue(RefCol);

                            ctrl.comboBox.Items.Add(item);
                        }
                        ctrl.comboBox.DropDownStyle = ComboBoxStyle.DropDownList;


                        ctrl.SetCaption(col.Name+"：");
                        if (BaseObject != null)
                            ctrl.SetValue(BaseObject.GetColValue(col));
                        return ctrl;
                        break;
                    }
                case ColumnType.enum_type:
                    {
                        ExComboBox ctrl = new ExComboBox();
                        ctrl.Width = 300;
                        //引用显示字段优先
                        if (col.RefShowCol != Guid.Empty)
                        {
                            CTable table = (CTable)Program.Ctx.TableMgr.Find(col.RefTable);
                            CBaseObjectMgr BaseObjectMgr = new CBaseObjectMgr();
                            BaseObjectMgr.TbCode = table.Code;
                            BaseObjectMgr.Ctx = Program.Ctx;

                            CColumn RefShowCol = (CColumn)table.ColumnMgr.Find(col.RefShowCol);
                            List<CBaseObject> lstObj = BaseObjectMgr.GetList();
                            foreach (CBaseObject obj in lstObj)
                            {
                                DataItem item = new DataItem();
                                item.name = obj.GetColValue(RefShowCol).ToString();

                                ctrl.comboBox.Items.Add(item);
                            }
                        }
                        else
                        {
                            List<CBaseObject> lstObj = col.ColumnEnumValMgr.GetList();
                            foreach (CBaseObject obj in lstObj)
                            {
                                CColumnEnumVal cev = (CColumnEnumVal)obj;
                                DataItem item = new DataItem();
                                item.name = cev.Val;
                                item.Data = cev.Val;

                                ctrl.comboBox.Items.Add(item);
                            }
                        }
                        ctrl.comboBox.DropDownStyle = ComboBoxStyle.DropDown;
                        
                        ctrl.SetCaption(col.Name + "：");
                        if (BaseObject != null)
                            ctrl.SetValue(BaseObject.GetColValue(col));
                        return ctrl;
                        break;
                    }
                case ColumnType.long_type:
                    {
                        ExTextBox ctrl = new ExTextBox();
                        ctrl.Width = 300;
                        ctrl.SetCaption(col.Name + "：");
                        if (BaseObject != null)
                            ctrl.SetValue(BaseObject.GetColValue(col));
                        return ctrl;
                    }
                case ColumnType.bool_type:
                    {
                        ExCheckBox ctrl = new ExCheckBox();
                        ctrl.Width = 300;
                        ctrl.SetCaption(col.Name + "：");
                        if (BaseObject != null)
                            ctrl.SetValue(BaseObject.GetColValue(col));
                        return ctrl;
                        break;
                    }
                case ColumnType.numeric_type:
                    {
                        ExTextBox ctrl = new ExTextBox();
                        ctrl.Width = 300;
                        ctrl.SetCaption(col.Name + "：");
                        if (BaseObject != null)
                            ctrl.SetValue(BaseObject.GetColValue(col));
                        return ctrl;
                        break;
                    }
                case ColumnType.datetime_type:
                    {
                        ExDateTimePicker ctrl = new ExDateTimePicker();
                        ctrl.Width = 300;
                        ctrl.SetCaption(col.Name + "：");
                        if (BaseObject != null)
                            ctrl.SetValue(BaseObject.GetColValue(col));
                        return ctrl;
                        break;
                    }
                case ColumnType.object_type:
                    {
                        BinaryCtrl ctrl = new BinaryCtrl();
                        ctrl.SetCaption(col.Name + "：");
                        if (BaseObject != null)
                            ctrl.SetValue(BaseObject.GetColValue(col));
                        return ctrl;
                        break;
                    }
            }
            return new ExTextBox() ;
        }

        public bool Save()
        {
            if (!Validate())
                return false;

            if (baseObject == null) //新建
            {
                baseObject = BaseObjectMgr.CreateBaseObject();
                baseObject.Ctx = BaseObjectMgr.Ctx;
                baseObject.TbCode = BaseObjectMgr.TbCode;
                bool bHasVisible = false;
                foreach (CBaseObject obj in View.ColumnInViewMgr.GetList())
                {
                    CColumnInView civ = (CColumnInView)obj;
                    CColumn col = (CColumn)BaseObjectMgr.Table.ColumnMgr.Find(civ.FW_Column_id);
                    if (col == null) continue;
                    //判断禁止和只读权限字段
                    if (m_sortRestrictColumnAccessType.ContainsKey(col.Id))
                    {
                        AccessType accessType = m_sortRestrictColumnAccessType[col.Id];
                        if (accessType == AccessType.forbide)
                            continue;
                        else if (accessType == AccessType.read)
                            continue;
                    }
                    //

                    if (col.Code.Equals("id", StringComparison.OrdinalIgnoreCase))
                        continue;
                    else if (col.Code.Equals("Created", StringComparison.OrdinalIgnoreCase))
                        continue;
                    else if (col.Code.Equals("Creator", StringComparison.OrdinalIgnoreCase))
                    {
                        baseObject.SetColValue(col, Program.User.Id);
                        continue;
                    }
                    else if (col.Code.Equals("Updated", StringComparison.OrdinalIgnoreCase))
                        continue;
                    else if (col.Code.Equals("Updator", StringComparison.OrdinalIgnoreCase))
                    {
                        baseObject.SetColValue(col, Program.User.Id);
                        continue;
                    }


                    Control[] ctrls= this.Controls.Find(col.Code, false);
                    if (ctrls.Length > 0)
                    {
                        IColumnCtrl ctrl = (IColumnCtrl)ctrls[0];
                        baseObject.SetColValue(col, ctrl.GetValue());
                        bHasVisible = true;
                    }
                }
                if (!bHasVisible)
                {
                    MessageBox.Show("没有可修改字段！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return false;
                }
                if (!BaseObjectMgr.AddNew(baseObject, true))
                {
                    MessageBox.Show("添加失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return false;
                }
            }
            else //修改
            {
                List<CBaseObject> lstCol = BaseObjectMgr.Table.ColumnMgr.GetList();
                bool bHasVisible = false;
                foreach (CBaseObject obj in lstCol)
                {
                    CColumn col = (CColumn)obj;
                    //判断禁止和只读权限字段
                    if (m_sortRestrictColumnAccessType.ContainsKey(col.Id))
                    {
                        AccessType accessType = m_sortRestrictColumnAccessType[col.Id];
                        if (accessType == AccessType.forbide)
                            continue;
                        else if (accessType == AccessType.read)
                            continue;
                    }
                    //

                    if (col.Code.Equals("id", StringComparison.OrdinalIgnoreCase))
                        continue;
                    else if (col.Code.Equals("Created", StringComparison.OrdinalIgnoreCase))
                        continue;
                    else if (col.Code.Equals("Creator", StringComparison.OrdinalIgnoreCase))
                        continue;
                    else if (col.Code.Equals("Updated", StringComparison.OrdinalIgnoreCase))
                    {
                        baseObject.SetColValue(col, DateTime.Now);
                        continue;
                    }
                    else if (col.Code.Equals("Updator", StringComparison.OrdinalIgnoreCase))
                    {
                        baseObject.SetColValue(col, Program.User.Id);
                        continue;
                    }


                    Control[] ctrls = this.Controls.Find(col.Code, false);
                    if (ctrls.Length > 0)
                    {
                        IColumnCtrl ctrl = (IColumnCtrl)ctrls[0];
                        baseObject.SetColValue(col, ctrl.GetValue());
                        bHasVisible = true;
                    }
                }
                if (!bHasVisible)
                {
                    MessageBox.Show("没有可修改字段！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return false;
                }
                if (!BaseObjectMgr.Update(baseObject, true))
                {
                    MessageBox.Show("修改失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return false;
                }
            }
            return true;
        }
        //验证数据
        bool Validate()
        {
            foreach (CBaseObject objCIV in View.ColumnInViewMgr.GetList())
            {
                CColumnInView civ = (CColumnInView)objCIV;

                CColumn col = (CColumn)BaseObjectMgr.Table.ColumnMgr.Find(civ.FW_Column_id);
                if (col == null)
                    continue;
                //判断禁止和只读权限字段
                if (m_sortRestrictColumnAccessType.ContainsKey(col.Id))
                {
                    AccessType accessType = m_sortRestrictColumnAccessType[col.Id];
                    if (accessType == AccessType.forbide)
                        continue;
                    else if (accessType == AccessType.read)
                        continue;
                }
                //

                if (col.Code.Equals("id", StringComparison.OrdinalIgnoreCase))
                    continue;
                else if (col.Code.Equals("Created", StringComparison.OrdinalIgnoreCase))
                    continue;
                else if (col.Code.Equals("Creator", StringComparison.OrdinalIgnoreCase))
                    continue;
                else if (col.Code.Equals("Updated", StringComparison.OrdinalIgnoreCase))
                    continue;
                else if (col.Code.Equals("Updator", StringComparison.OrdinalIgnoreCase))
                    continue;

                Control[] ctrls = this.Controls.Find(col.Code, false);
                if (ctrls.Length == 0)
                    continue;

                IColumnCtrl ctrl = (IColumnCtrl)ctrls[0];
                object val = ctrl.GetValue();

                if (!col.AllowNull && (val == null || val.ToString() == ""))
                {
                    MessageBox.Show(string.Format("{0}不允许空！", col.Name), "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return false;
                }
                if (col.ColType == ColumnType.string_type)
                {
                    if (val.ToString().Length > col.ColLen)
                    {
                        MessageBox.Show(string.Format("{0}长度不能超过{1}！", col.Name, col.ColLen), "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        return false;
                    }
                }
                else if (col.ColType == ColumnType.datetime_type)
                {
                    if (!(val == null || val.ToString() == ""))
                    {
                        try { Convert.ToDateTime(val); }
                        catch
                        {
                            MessageBox.Show(string.Format("{0}日期格式错误！", col.Name), "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            return false;
                        }
                    }
                }
                else if (col.ColType == ColumnType.int_type
                    || col.ColType == ColumnType.long_type)
                {
                    if (!CUtil.IsInt(val.ToString()))
                    {
                        MessageBox.Show(string.Format("{0}为整型数字！", col.Name), "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        return false;
                    }
                }
                else if (col.ColType == ColumnType.numeric_type)
                {
                    if (!CUtil.IsNum(val.ToString()))
                    {
                        MessageBox.Show(string.Format("{0}为数字！", col.Name), "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        return false;
                    }
                }
                else if (col.ColType == ColumnType.guid_type
                || col.ColType == ColumnType.ref_type)
                {
                    if (!(val == null || val.ToString() == ""))
                    {
                        try { Guid guid = new Guid(val.ToString()); }
                        catch
                        {
                            MessageBox.Show(string.Format("{0}为GUID格式！", col.Name), "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            return false;
                        }
                    }
                }

            }
            return true;
        }
    }
}
