using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text.RegularExpressions;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ErpCore.CommonCtrl
{
    public partial class PathCtrl : UserControl, IColumnCtrl
    {
        private string m_sUploadPath = "";
        private object m_objVal = null;

        public PathCtrl()
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
        public void SetUploadPath(string sPath)
        {
            m_sUploadPath = sPath;
            if (m_sUploadPath[m_sUploadPath.Length - 1] != '\\')
                m_sUploadPath += "\\";
        }
        public object GetValue()
        {
            return m_objVal;
        }
        public void SetValue(object objVal)
        {
            m_objVal = objVal;
            if (objVal != null)
            {
                lbFileName.Visible = true;
                linkDel.Visible = true;
                string sVal = objVal.ToString();
                string[] arrName = sVal.Split("|".ToCharArray());
                if(arrName.Length>1)
                    lbFileName.Text = arrName[1];
            }
            else
            {
                lbFileName.Visible = false;
                linkDel.Visible = false;
                lbFileName.Text = "";
            }


            ShowPic(objVal);
        }

        private void BinaryCtrl_Load(object sender, EventArgs e)
        {

        }

        private void btBrowse_Click(object sender, EventArgs e)
        {
            if (m_sUploadPath == "")
            {
                MessageBox.Show("请先设置上传路径！");
                return;
            }
            if (!Directory.Exists(m_sUploadPath))
                Directory.CreateDirectory(m_sUploadPath);
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            txtUrl.Text = openFileDialog1.FileName;

            FileInfo fi = new FileInfo(openFileDialog1.FileName);
            Guid guid = Guid.NewGuid();
            string sDestFile = string.Format("{0}{1}", guid.ToString().Replace("-",""),fi.Extension);
            File.Copy(openFileDialog1.FileName, m_sUploadPath + sDestFile);

            string sVal = string.Format("{0}|{1}", sDestFile, fi.Name);
            SetValue(sVal);
        }

        private void linkDel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SetValue(null);
        }

        void ShowPic(object objVal)
        {
            if (objVal==null ||objVal.ToString()=="")
            {
                picBox.Image = null;
                return;
            }
            string sVal = objVal.ToString();
            string[] arrName = sVal.Split("|".ToCharArray());
            if (arrName.Length != 2)
            {
                picBox.Image = null;
                return;
            }
            string sFileName = arrName[0];
            int idx = sFileName.LastIndexOf('.');
            if (idx < 0)
            {
                picBox.Image = null;
                return;
            }
            string sExt = sFileName.Substring(idx+1);
            if (sExt != "jpg"
                &&sExt != "bmp"
                && sExt != "png"
                && sExt != "jpeg"
                && sExt != "gif"
                )
            {
                picBox.Image = null;
                return;
            }
            picBox.ImageLocation = m_sUploadPath + sFileName;
        }
    }
}
