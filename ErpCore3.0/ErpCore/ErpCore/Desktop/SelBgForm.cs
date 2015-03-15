using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using System.IO;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.SubSystem;
using ErpCore.Database.Diagram;


namespace ErpCore.Desktop
{
    public partial class SelBgForm : Form
    {
        public string m_sBgUrl = "";

        public SelBgForm()
        {
            InitializeComponent();
        }

        private void SelIconForm_Load(object sender, EventArgs e)
        {
            LoadList();
        }
        void LoadList()
        {
            this.flowLayoutPanel.Controls.Clear();

            string sPath = Application.StartupPath + "\\DesktopImg\\";
            if(!Directory.Exists(sPath))
                Directory.CreateDirectory(sPath);
            DirectoryInfo di = new DirectoryInfo(sPath);
            FileInfo[] fs= di.GetFiles();
            foreach (FileInfo fi in fs)
            {
                IconItem item = new IconItem();
                item.Icon = fi.FullName;
                item.ClickItem += new ClickEventHandler(item_ClickItem);
                flowLayoutPanel.Controls.Add(item);
            }
        }

        void item_ClickItem(object sender, EventArgs e)
        {
            IconItem item=(IconItem)sender;
            m_sBgUrl = item.Icon;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btUpload_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "*.jpg|*.jpg|*.png|*.png|*.gif|*.gif";
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Multiselect = true;
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            string sPath = Application.StartupPath + "\\DesktopImg\\";
            foreach (string sFile in openFileDialog1.FileNames)
            {
                FileInfo fi = new FileInfo(sFile);
                string sDest = sPath + Guid.NewGuid().ToString() + fi.Extension;

                File.Copy(sFile, sDest);
            }
            LoadList();
        }
    }
}
