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
    public partial class RecordCtrl : UserControl
    {
        public bool m_bNeedSave = true; //是否需要保存
        CBaseObjectMgr baseObjectMgr = null;
        CBaseObject baseObject = null;
        //受限的字段：禁止或者只读权限
        SortedList<Guid, AccessType> m_sortRestrictColumnAccessType = new SortedList<Guid, AccessType>();

        bool m_bIsNew = false;

        public SortedList<string, object> m_sortNotShowCol = new SortedList<string, object>();
        public SortedList<string, object> m_sortDisableCol = new SortedList<string, object>();

        public CBaseObjectMgr BaseObjectMgr
        {
            get { return baseObjectMgr; }
            set
            {
                baseObjectMgr = value;
            }
        }
        public CBaseObject BaseObject
        {
            get { return baseObject; }
            set
            {
                baseObject = value;
            }
        }
        public RecordCtrl()
        {
            InitializeComponent();
        }

        private void RecordCtrl_Load(object sender, EventArgs e)
        {
            LoadUI();
        }
        void LoadUI()
        {
            if (BaseObjectMgr == null )
                return;
            m_sortRestrictColumnAccessType = Program.User.GetRestrictColumnAccessTypeList(BaseObjectMgr.Table);

            if (BaseObject == null) //新建
            {
                BaseObject = BaseObjectMgr.CreateBaseObject();
                BaseObject.Ctx = BaseObjectMgr.Ctx;
                BaseObject.TbCode = BaseObjectMgr.TbCode;
                m_bIsNew = true;
            }

            List<CBaseObject> lstCol = BaseObjectMgr.Table.ColumnMgr.GetList();
            int nTop = 5;
            foreach (CBaseObject obj in lstCol)
            {
                CColumn col = (CColumn)obj;
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

                //if (!col.IsVisible)
                //    continue;

                if (m_sortNotShowCol.ContainsKey(col.Code.ToLower()))
                    continue;

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

                    if (m_sortDisableCol.ContainsKey(col.Code.ToLower()))
                        ctrl.Enabled = false;
                }
                nTop = ctrl.Bottom + 5;
            }
            this.Height = nTop;
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
                        if (table != null)
                        {
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
                case ColumnType.path_type:
                    {
                        PathCtrl ctrl = new PathCtrl();
                        ctrl.SetCaption(col.Name + "：");
                        ctrl.SetUploadPath(col.UploadPath);
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
                {
                    if (m_bIsNew)
                        BaseObject.SetColValue(col, Program.User.Id);
                    continue;
                }
                else if (col.Code.Equals("Updated", StringComparison.OrdinalIgnoreCase))
                {
                    if (!m_bIsNew)
                        BaseObject.SetColValue(col, DateTime.Now);
                    continue;
                }
                else if (col.Code.Equals("Updator", StringComparison.OrdinalIgnoreCase))
                {
                    if (!m_bIsNew)
                        BaseObject.SetColValue(col, Program.User.Id);
                    continue;
                }

                //if (!col.IsVisible)
                //    continue;

                if (m_sortNotShowCol.ContainsKey(col.Code.ToLower()))
                {
                    BaseObject.SetColValue(col, m_sortNotShowCol[col.Code.ToLower()]);
                    continue;
                }
                if (m_sortDisableCol.ContainsKey(col.Code.ToLower()))
                {
                    BaseObject.SetColValue(col, m_sortDisableCol[col.Code.ToLower()]);
                    continue;
                }

                Control[] ctrls = this.Controls.Find(col.Code, false);
                if (ctrls.Length > 0)
                {
                    IColumnCtrl ctrl = (IColumnCtrl)ctrls[0];
                    BaseObject.SetColValue(col, ctrl.GetValue());
                    bHasVisible = true;
                }
            }
            if (!bHasVisible)
            {
                MessageBox.Show("没有可修改字段！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            if (m_bIsNew)
                BaseObjectMgr.AddNew(BaseObject);
            else
                BaseObjectMgr.Update(BaseObject);
            if (m_bNeedSave)
            {
                if (!BaseObjectMgr.Save())
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
            List<CBaseObject> lstCol = BaseObjectMgr.Table.ColumnMgr.GetList();
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
