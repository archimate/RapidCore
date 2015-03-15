using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;

using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ErpCore.CommonCtrl
{
    public partial class BinaryCtrl : UserControl, IColumnCtrl
    {
        byte[] m_byteValue = null;
        byte[] m_byteFileName = new byte[254];
        byte[] m_byteData = null;

        public BinaryCtrl()
        {
            InitializeComponent();
        }

        public string GetCaption()
        {
            return lbCaption.Text;
        }
        public void SetCaption(string sCaption)
        {
            lbCaption.Text = sCaption;
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

                lbFileName.Visible = true;
                linkDel.Visible = true;
                lbFileName.Text =Encoding.Default.GetString( m_byteFileName);
            }
            else
            {
                this.m_byteValue = null;
                this.m_byteData = null;
                m_byteFileName = new byte[254];

                lbFileName.Visible = false;
                linkDel.Visible = false;
                lbFileName.Text = "";
            }


            ShowPic();
        }

        private void BinaryCtrl_Load(object sender, EventArgs e)
        {

        }

        private void btBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            txtUrl.Text = openFileDialog1.FileName;

            FileInfo fi = new FileInfo(openFileDialog1.FileName);
            m_byteFileName = Encoding.Default.GetBytes(fi.Name);
            BinaryReader reader = new BinaryReader(fi.OpenRead());
            m_byteData = new byte[fi.Length];
            reader.Read(m_byteData, 0, (int)fi.Length);
            reader.Close();

            m_byteValue = new byte[254 + fi.Length];
            Array.Copy(m_byteFileName, m_byteValue, m_byteFileName.Length);
            Array.Copy(m_byteData, 0, m_byteValue, 254, m_byteData.Length);

            ShowPic();
        }

        private void linkDel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            m_byteValue = null;
            lbFileName.Visible = false;
            linkDel.Visible = false;
            lbFileName.Text = "";
        }

        void ShowPic()
        {
            string sFileName = Encoding.Default.GetString(m_byteFileName);
            sFileName = sFileName.Trim("\0".ToCharArray());
            int idx = sFileName.LastIndexOf('.');
            if (idx < 0)
                return;
            string sExt = sFileName.Substring(idx+1);
            if (sExt != "jpg"
                &&sExt != "bmp"
                && sExt != "png"
                && sExt != "jpeg"
                && sExt != "gif"
                )
                return;
            MemoryStream ms = new MemoryStream(m_byteData);
            ms.Position = 0;
            Image img = Image.FromStream(ms);
            ms.Close();
            picBox.Image = img;
        }
    }
}
