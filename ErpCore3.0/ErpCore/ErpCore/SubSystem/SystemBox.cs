using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;

using System.Text;
using System.Windows.Forms;
using System.IO;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.SubSystem;
using ErpCoreModel.UI;
using ErpCore.Window;

namespace ErpCore.SubSystem
{
    public partial class SystemBox : UserControl
    {
        byte[] m_byteValue = null;
        byte[] m_byteFileName = new byte[254];
        byte[] m_byteData = null;

        string title = "";
        public CSystem m_System = null;

        AdminForm m_frmAdmin = null;

        public SystemBox()
        {
            InitializeComponent();
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public object GetValue()
        {
            return this.m_byteValue;
        }
        public void SetValue(object objVal)
        {
            if (objVal != null)
            {
                this.m_byteValue = (byte[])objVal;
                Array.Copy(m_byteValue, m_byteFileName, 254);
                m_byteData = new byte[m_byteValue.Length - 254];
                Array.Copy(m_byteValue, 254, m_byteData, 0, m_byteData.Length);
            }
            else
            {
                this.m_byteValue = null;
                this.m_byteData = null;
                m_byteFileName = new byte[254];
            }


            ShowPic();
        }

        private void SystemBox_Load(object sender, EventArgs e)
        {
            lbTitle.Text = Title;
            ShowPic();
        }

        void ShowPic()
        {
            string sFileName = Encoding.Default.GetString(m_byteFileName);
            sFileName = sFileName.Trim("\0".ToCharArray());
            int idx = sFileName.LastIndexOf('.');
            if (idx < 0)
                return;
            string sExt = sFileName.Substring(idx + 1);
            if (sExt != "jpg"
                && sExt != "bmp"
                && sExt != "png"
                && sExt != "jpeg"
                && sExt != "gif"
                )
                return;
            MemoryStream ms = new MemoryStream(m_byteData);
            ms.Position = 0;
            Image img = Image.FromStream(ms);
            ms.Close();
            picIcon.Image = img;
        }

        private void picIcon_Click(object sender, EventArgs e)
        {
            if (m_System == null) //系统管理
            {
                if (m_frmAdmin == null || m_frmAdmin.IsDisposed)
                    m_frmAdmin = new AdminForm();
                m_frmAdmin.Show();
            }
            else
            {

                CWindow window = (CWindow)Program.Ctx.WindowMgr.Find(m_System.StartWindow);
                if (window != null)
                {
                    LayoutWindow frm = new LayoutWindow();
                    frm.Window = window;
                    frm.Show();
                }
            }
        }
    }
}
