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

namespace ErpCore.Window
{
    public partial class RecordWindow : Form
    {
        bool m_bIsNew = false;
        //是否需要保存
        public bool NeedSave
        {
            get { return recordCtrl.m_bNeedSave; }
            set
            {
                recordCtrl.m_bNeedSave = value;
            }
        }
        public CBaseObjectMgr BaseObjectMgr
        {
            get { return recordCtrl.BaseObjectMgr; }
            set
            {
                recordCtrl.BaseObjectMgr = value;
            }
        }
        public CBaseObject BaseObject
        {
            get { return recordCtrl.BaseObject; }
            set
            {
                recordCtrl.BaseObject = value;
            }
        }
        public RecordWindow(CBaseObjectMgr objMgr, CBaseObject obj)
        {
            InitializeComponent();

            BaseObjectMgr = objMgr;
            BaseObject = obj;
            if (obj == null)
                m_bIsNew = true;
        }

        private void RecordWindow_Load(object sender, EventArgs e)
        {
            if (m_bIsNew)
            {
                this.Text = string.Format("添加{0}",BaseObjectMgr.Table.Name);
            }
            else
            {
                this.Text = string.Format("修改{0}", BaseObjectMgr.Table.Name);
            }
        }

        private void btOk_Click(object sender, EventArgs e)
        {

            if (!recordCtrl.Save())
                return;

            DialogResult = DialogResult.OK;
        }
    }
}
