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
using ErpCoreModel.UI;
using ErpCoreModel.Report;
using ErpCore.Desktop;

namespace ErpCore.Menu
{
    public partial class MenuInfo : Form
    {
        public CMenu m_Menu = null;
        public CMenu m_PMenu = null;
        bool m_bIsNew = false;

        public MenuInfo()
        {
            InitializeComponent();
        }

        private void MenuInfo_Load(object sender, EventArgs e)
        {
            LoadView();
            LoadWindow();
            LoadReport();
            LoadData();
        }
        void LoadData()
        {
            if (m_Menu == null)
            {
                m_bIsNew = true;
                m_Menu = new CMenu();
                m_Menu.Ctx = Program.Ctx;
                if (m_PMenu != null)
                    txtParent.Text = m_PMenu.Name;
                return;
            }
            txtName.Text = m_Menu.Name;
            CMenu PMenu = (CMenu)m_Menu.m_ObjectMgr.Find(m_Menu.Parent_id);
            if (PMenu != null)
                txtParent.Text = PMenu.Name;
            if (m_Menu.MType == enumMenuType.CatalogMenu)
            {
                rdType1.Checked = true;
                rdType1_Click(null, null);
            }
            else if (m_Menu.MType == enumMenuType.ViewMenu)
            {
                rdType2.Checked = true;
                rdType2_Click(null, null);
            }
            else if (m_Menu.MType == enumMenuType.WindowMenu)
            {
                rdType3.Checked = true;
                rdType3_Click(null, null);
            }
            else if (m_Menu.MType == enumMenuType.UrlMenu)
            {
                rdType4.Checked = true;
                rdType4_Click(null, null);
            }
            else
            {
                rdType5.Checked = true;
                rdType5_Click(null, null);
            }

            if (m_Menu.MType == enumMenuType.ViewMenu && m_Menu.UI_View_id != Guid.Empty)
            {
                for (int i = 0; i < cbView.Items.Count; i++)
                {
                    DataItem item = (DataItem)cbView.Items[i];
                    CView view = (CView)item.Data;
                    if (view.Id == m_Menu.UI_View_id)
                    {
                        cbView.SelectedIndex = i;
                        break;
                    }
                }
            }
            if (m_Menu.MType == enumMenuType.WindowMenu && m_Menu.UI_Window_id != Guid.Empty)
            {
                for (int i = 0; i < cbWindow.Items.Count; i++)
                {
                    DataItem item = (DataItem)cbWindow.Items[i];
                    CWindow Window = (CWindow)item.Data;
                    if (Window.Id == m_Menu.UI_Window_id)
                    {
                        cbWindow.SelectedIndex = i;
                        break;
                    }
                }
            }
            txtUrl.Text = m_Menu.Url;
            if (m_Menu.MType == enumMenuType.ReportMenu && m_Menu.RPT_Report_id != Guid.Empty)
            {
                for (int i = 0; i < cbReport.Items.Count; i++)
                {
                    DataItem item = (DataItem)cbReport.Items[i];
                    CReport Report = (CReport)item.Data;
                    if (Report.Id == m_Menu.RPT_Report_id)
                    {
                        cbReport.SelectedIndex = i;
                        break;
                    }
                }
            }
            if (m_Menu.IconUrl != "")
            {
                string sPath = Application.StartupPath + "\\MenuIcon\\";
                picIcon.ImageLocation = sPath + m_Menu.IconUrl;
            }

        }
        void LoadView()
        {
            cbView.Items.Clear();
            List<CBaseObject> lstObj = Program.Ctx.ViewMgr.GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CView view = (CView)obj;
                DataItem item = new DataItem(view.Name, view);
                cbView.Items.Add(item);
            }
        }
        void LoadReport()
        {
            cbReport.Items.Clear();
            CCompany Company = (CCompany)Program.Ctx.CompanyMgr.Find(Program.User.B_Company_id);
            List<CBaseObject> lstObj = Company.ReportMgr.GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CReport Report = (CReport)obj;
                DataItem item = new DataItem(Report.Name, Report);
                cbReport.Items.Add(item);
            }
        }
        void LoadWindow()
        {
            cbWindow.Items.Clear();
            List<CBaseObject> lstObj = Program.Ctx.WindowMgr.GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CWindow Window = (CWindow)obj;
                DataItem item = new DataItem(Window.Name, Window);
                cbWindow.Items.Add(item);
            }
        }

        private void rdType1_Click(object sender, EventArgs e)
        {
            cbView.Enabled = false;
            cbWindow.Enabled = false;
            txtUrl.Enabled = false;
            cbReport.Enabled = false;
        }
        private void rdType2_Click(object sender, EventArgs e)
        {
            cbView.Enabled = true;
            cbWindow.Enabled = false;
            txtUrl.Enabled = false;
            cbReport.Enabled = false;
        }

        private void rdType3_Click(object sender, EventArgs e)
        {
            cbView.Enabled = false;
            cbWindow.Enabled = true;
            txtUrl.Enabled = false;
            cbReport.Enabled = false;
        }
        private void rdType4_Click(object sender, EventArgs e)
        {
            cbView.Enabled = false;
            cbWindow.Enabled = false;
            txtUrl.Enabled = true;
            cbReport.Enabled = false;
        }
        private void rdType5_Click(object sender, EventArgs e)
        {
            cbView.Enabled = false;
            cbWindow.Enabled = false;
            txtUrl.Enabled = false;
            cbReport.Enabled = true;
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            string sName = txtName.Text.Trim();
            if (sName == "")
            {
                MessageBox.Show("名称不能空！");
                txtName.Focus();
                return;
            }
            if (!sName.Equals(m_Menu.Name, StringComparison.OrdinalIgnoreCase))
            {
                List<CBaseObject> lstObj = Program.Ctx.MenuMgr.GetList();
                foreach(CBaseObject obj in lstObj)
                {
                    CMenu menu = (CMenu)obj;
                    if (m_PMenu == null && menu.Parent_id == Guid.Empty 
                        && sName.Equals(menu.Name, StringComparison.OrdinalIgnoreCase))
                    {
                        MessageBox.Show("名称重复！");
                        txtName.Focus();
                        return;
                    }
                    else if (m_PMenu != null && menu.Parent_id == m_PMenu.Id
                        && sName.Equals(menu.Name, StringComparison.OrdinalIgnoreCase))
                    {
                        MessageBox.Show("名称重复！");
                        txtName.Focus();
                        return;
                    }
                }
            }
            if (rdType2.Checked)
            {
                if (cbView.SelectedIndex == -1)
                {
                    MessageBox.Show("请选择视图！");
                    return;
                }
            }
            else if (rdType3.Checked)
            {
                if (cbWindow.SelectedIndex == -1)
                {
                    MessageBox.Show("请选择窗体！");
                    return;
                }
            }
            else if (rdType4.Checked)
            {
                if (txtUrl.Text.Trim()=="")
                {
                    MessageBox.Show("请填写url！");
                    txtUrl.Focus();
                    return;
                }
            }
            else if (rdType5.Checked)
            {
                if (cbReport.SelectedIndex == -1)
                {
                    MessageBox.Show("请选择报表！");
                    return;
                }
            }

            m_Menu.Name = sName;
            if (m_PMenu != null)
                m_Menu.Parent_id = m_PMenu.Id;
            if (rdType1.Checked)
                m_Menu.MType = enumMenuType.CatalogMenu;
            else if (rdType2.Checked)
            {
                m_Menu.MType = enumMenuType.ViewMenu;
                DataItem item = (DataItem)cbView.SelectedItem;
                CView view = (CView)item.Data;
                m_Menu.UI_View_id = view.Id;
            }
            else if (rdType3.Checked)
            {
                m_Menu.MType = enumMenuType.WindowMenu;
                DataItem item = (DataItem)cbWindow.SelectedItem;
                CWindow Window = (CWindow)item.Data;
                m_Menu.UI_Window_id = Window.Id;
            }
            else if(rdType4.Checked)
            {
                m_Menu.MType = enumMenuType.UrlMenu;
                m_Menu.Url = txtUrl.Text.Trim();
            }
            else if (rdType5.Checked)
            {
                m_Menu.MType = enumMenuType.ReportMenu;
                DataItem item = (DataItem)cbReport.SelectedItem;
                CReport Report = (CReport)item.Data;
                m_Menu.RPT_Report_id = Report.Id;
            }
            if (File.Exists(picIcon.ImageLocation))
            {
                FileInfo fi = new FileInfo(picIcon.ImageLocation);
                m_Menu.IconUrl = fi.Name;
            }
            else
                m_Menu.IconUrl = "";

            if (m_bIsNew)
                Program.Ctx.MenuMgr.AddNew(m_Menu);
            else
                Program.Ctx.MenuMgr.Update(m_Menu);

            if (!Program.Ctx.MenuMgr.Save(true))
            {
                MessageBox.Show("保存失败！");
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btBrowse_Click(object sender, EventArgs e)
        {
            SelIconForm frm = new SelIconForm();
            if (frm.ShowDialog() != DialogResult.OK)
                return;
            //string sPath = Application.StartupPath + "\\MenuIcon\\" + frm.m_sIconUrl;
            picIcon.ImageLocation = frm.m_sIconUrl;
        }



    }
}
