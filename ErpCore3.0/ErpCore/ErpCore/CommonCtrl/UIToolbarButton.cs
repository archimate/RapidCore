using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

using System.Text;
using System.Windows.Forms;
using System.IO;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.UI;
using ErpCore.Window;

namespace ErpCore.CommonCtrl
{
    public partial class UIToolbarButton : ToolStripButton
    {
        byte[] m_byteValue = null;
        byte[] m_byteFileName = new byte[254];
        byte[] m_byteData = null;

        CNavigateBarButton navigateBarButton = null;

        public CNavigateBarButton NavigateBarButton
        {
            get { return navigateBarButton; }
            set
            {
                navigateBarButton = value;
            }
        }

        public UIToolbarButton()
        {
            InitializeComponent();

            this.Click += new EventHandler(UIToolbarButton_Click);
        }

        void UIToolbarButton_Click(object sender, EventArgs e)
        {
            CWindow window = (CWindow)Program.Ctx.WindowMgr.Find(NavigateBarButton.UI_Window_id);
            if (window != null)
            {
                LayoutWindow frm = new LayoutWindow();
                frm.Window = window;
                frm.Show();
            }
        }

        public UIToolbarButton(IContainer container)
        {
            container.Add(this);

            InitializeComponent();

            this.Click += new EventHandler(UIToolbarButton_Click);
        }

        public object GetIcon()
        {
            return this.m_byteValue;
        }
        public void SetIcon(object objVal)
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
            System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
            ms.Close();
            this.Image = img;
        }
    }
}
