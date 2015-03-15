using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using System.Reflection;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.UI;
using ErpCore.CommonCtrl;

namespace ErpCore.View
{
    public partial class SingleViewRecord : Form
    {

        public SingleViewRecord(CBaseObjectMgr BaseObjectMgr, CBaseObject BaseObject)
        {
            InitializeComponent();

            recordCtrl.BaseObjectMgr = BaseObjectMgr;
            recordCtrl.BaseObject = BaseObject;
        }
        public CView View
        {
            get
            {
                return recordCtrl.View;
            }
            set
            {
                recordCtrl.View = value;
            }
        }
        public CBaseObjectMgr BaseObjectMgr
        {
            get
            {
                return recordCtrl.BaseObjectMgr;
            }
            set
            {
                recordCtrl.BaseObjectMgr = value;
            }
        }
        public CBaseObject BaseObject
        {
            get
            {
                return recordCtrl.BaseObject;
            }
            set
            {
                recordCtrl.BaseObject = value;
            }
        }

        private void RecordWindow_Load(object sender, EventArgs e)
        {
            if (BaseObject == null)
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
