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

namespace ErpCore.Window.Designer
{
    public partial class UIToolbarButtonEl : ToolStripButton
    {
        byte[] m_byteValue = null;
        byte[] m_byteFileName = new byte[254];
        byte[] m_byteData = null;

        public UIToolbarButtonEl()
        {
            InitializeComponent();
        }

        public UIToolbarButtonEl(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
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
