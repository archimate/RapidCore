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
using ErpCore.Database.Diagram;
using ErpCore.Window;
using ErpCore.View;
using ErpCore.Report;


namespace ErpCore.Desktop
{
    public partial class DesktopPanel : Form
    {
        AdminForm m_frmAdmin = null;
        Guid m_guidDesktopGroupId = Guid.Empty;//当前桌面组

        public DesktopPanel()
        {
            UpdateForm frmUp = new UpdateForm();
            frmUp.ShowDialog();

            InitializeComponent();
        }

        private void DesktopPanel_Load(object sender, EventArgs e)
        {
            string sBackImg = Application.StartupPath + "\\DesktopImg\\default.jpg";
            if (File.Exists(sBackImg))
                this.BackgroundImage = Image.FromFile(sBackImg);
        }
        public void LoadDesktopGroup()
        {
            for (int i=this.flowLayoutPanelBottom.Controls.Count-1;i>=0;i--)
            {
                Control ctrl=this.flowLayoutPanelBottom.Controls[i];
                if (!ctrl.Name.Equals("rdDefault", StringComparison.OrdinalIgnoreCase))
                    this.flowLayoutPanelBottom.Controls.Remove(ctrl);
            }
            List<CBaseObject> lstObj = Program.Ctx.DesktopGroupMgr.GetList();
            this.flowLayoutPanelBottom.Width = (lstObj.Count + 1) * 20;
            foreach (CBaseObject obj in lstObj)
            {
                CDesktopGroup group = (CDesktopGroup)obj;
                //判断权限
                AccessType accesstype = Program.User.GetDesktopGroupAccess(group.Id);
                if (accesstype== AccessType.forbide)
                    continue;
                RadioButton rdButton = new RadioButton();
                rdButton.Name ="rd_"+ group.Id.ToString();
                rdButton.Text = "";
                rdButton.Tag = group;
                rdButton.Width = 14;
                rdButton.Height = 13;
                rdButton.Click += new EventHandler(DesktopGroup_rdButton_Click);
                toolTip_Group.SetToolTip(rdButton, group.Name);
                this.flowLayoutPanelBottom.Controls.Add(rdButton);
            }
        }

        void DesktopGroup_rdButton_Click(object sender, EventArgs e)
        {
            RadioButton rdButton = (RadioButton)sender;
            if (rdButton.Name.Equals("rdDefault", StringComparison.OrdinalIgnoreCase))
            {
                m_guidDesktopGroupId = Guid.Empty;
            }
            else
            {
                int idx = rdButton.Name.IndexOf('_');
                string id = rdButton.Name.Substring(idx + 1);
                m_guidDesktopGroupId = new Guid(id);
            }
            LoadList();
        }
        void LoadDesktop()
        {
            string sBackImg = Application.StartupPath + "\\DesktopImg\\default.jpg";
            CDesktop desktop =(CDesktop) Program.User.DesktopMgr.GetFirstObj();
            if (desktop != null && desktop.BackImg!="")
                sBackImg = Application.StartupPath + "\\DesktopImg\\"+desktop.BackImg;
            if (File.Exists(sBackImg))
                this.BackgroundImage = Image.FromFile(sBackImg);
        }
        void LoadList()
        {
            if (string.IsNullOrEmpty(Program.User.Name))
                return;

            this.flowLayoutPanel.Controls.Clear();

            if (Program.User.IsRole("管理员") && m_guidDesktopGroupId==Guid.Empty)
            {
                DesktopItem adminBox = new DesktopItem();
                adminBox.Title = "系统管理";
                adminBox.Icon = "admin.png";
                adminBox.ItemType = enumDesktopItemType.Admin;
                adminBox.ClickItem += new ClickEventHandler(adminBox_ClickItem);
                this.flowLayoutPanel.Controls.Add(adminBox);
            }
            //用户菜单
            List<CBaseObject> lstObj = Program.User.UserMenuMgr.GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CUserMenu UserMenu = (CUserMenu)obj;
                if (UserMenu.UI_DesktopGroup_id != m_guidDesktopGroupId)
                    continue;
                CMenu menu = (CMenu)Program.Ctx.MenuMgr.Find(UserMenu.UI_Menu_id);
                if (menu == null)
                    continue;

                DesktopItem Item = new DesktopItem();
                Item.Title = menu.Name;
                Item.Icon = menu.IconUrl;
                Item.ItemType = (enumDesktopItemType)Convert.ToInt32(menu.MType);
                Item.m_BaseObject = menu;
                Item.ClickItem += new ClickEventHandler(menu_ClickItem);
                this.flowLayoutPanel.Controls.Add(Item);
            }
            //角色菜单
            CCompany Company = (CCompany)Program.Ctx.CompanyMgr.Find(Program.User.B_Company_id);
            if (Company != null)
            {
                List<CBaseObject> lstObjR = Company.RoleMgr.GetList();
                foreach (CBaseObject objR in lstObjR)
                {
                    CRole Role = (CRole)objR;
                    if (!Program.User.IsRole(Role.Name))
                        continue;
                    List<CBaseObject> lstObjRM = Role.RoleMenuMgr.GetList();
                    foreach (CBaseObject objRM in lstObjRM)
                    {
                        CRoleMenu RoleMenu = (CRoleMenu)objRM;
                        if (RoleMenu.UI_DesktopGroup_id != m_guidDesktopGroupId)
                            continue;
                        CMenu menu = (CMenu)Program.Ctx.MenuMgr.Find(RoleMenu.UI_Menu_id);
                        if (menu == null)
                            continue;

                        DesktopItem Item = new DesktopItem();
                        Item.Title = menu.Name;
                        Item.Icon = menu.IconUrl;
                        Item.ItemType = (enumDesktopItemType)Convert.ToInt32(menu.MType);
                        Item.m_BaseObject = menu;
                        Item.ClickItem += new ClickEventHandler(menu_ClickItem);
                        this.flowLayoutPanel.Controls.Add(Item);
                    }
                }
            }
            //应用
            lstObj = Program.User.DesktopAppMgr.GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CDesktopApp App = (CDesktopApp)obj;
                if (App.UI_DesktopGroup_id != m_guidDesktopGroupId)
                    continue;

                DesktopItem Item = new DesktopItem();
                Item.Title = App.Name;
                Item.Icon = App.IconUrl;
                Item.ItemType = enumDesktopItemType.DesktopApp;
                Item.m_BaseObject = App;
                Item.ClickItem += new ClickEventHandler(App_ClickItem);
                Item.ClickClose += new ClickEventHandler(App_ClickClose);
                this.flowLayoutPanel.Controls.Add(Item);
            }
            //添加
            DesktopItem addBox = new DesktopItem();
            addBox.Title = "添加应用";
            addBox.Icon = "add.png";
            addBox.ItemType = enumDesktopItemType.AddDesktopApp;
            addBox.ClickItem += new ClickEventHandler(addBox_ClickItem);
            this.flowLayoutPanel.Controls.Add(addBox);
        }

        void App_ClickClose(object sender, EventArgs e)
        {
            DesktopItem item = (DesktopItem)sender;
            CDesktopApp App = (CDesktopApp)item.m_BaseObject;
            if (MessageBox.Show(string.Format("确认删除{0}？",App.Name), "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                        != DialogResult.OK)
                return;
            if (!Program.User.DesktopAppMgr.Delete(App, true))
            {
                MessageBox.Show("删除失败！");
                return;
            }
            this.flowLayoutPanel.Controls.Remove(item);
        }

        void addBox_ClickItem(object sender, EventArgs e)
        {
            AddDesktopApp frm = new AddDesktopApp();
            frm.UI_DesktopGroup_id = m_guidDesktopGroupId;
            if (frm.ShowDialog() != DialogResult.OK)
                return;

            DesktopItem Item = new DesktopItem();
            Item.Title = frm.m_App.Name;
            Item.Icon = frm.m_App.IconUrl;
            Item.ItemType = enumDesktopItemType.DesktopApp;
            Item.m_BaseObject = frm.m_App;
            Item.ClickItem += new ClickEventHandler(App_ClickItem);
            Item.ClickClose += new ClickEventHandler(App_ClickClose);
            this.flowLayoutPanel.Controls.Add(Item);
        }

        void App_ClickItem(object sender, EventArgs e)
        {
            DesktopItem item = (DesktopItem)sender;
            CDesktopApp App = (CDesktopApp)item.m_BaseObject;
            bool bIsWebUrl = App.Url.Length > 4 && App.Url.Substring(0, 4).Equals("http", StringComparison.OrdinalIgnoreCase);
            if (!bIsWebUrl && !File.Exists(App.Url))
            {
                MessageBox.Show(string.Format("{0} 不存在！",App.Url));
                return;
            }
            System.Diagnostics.Process.Start(App.Url);
        }

        void menu_ClickItem(object sender, EventArgs e)
        {
            DesktopItem item = (DesktopItem)sender;
            CMenu menu = (CMenu)item.m_BaseObject;
            if (menu.MType == enumMenuType.CatalogMenu)
            {
                ContextMenu popMenu = new ContextMenu();
                LoopLoadPopMenu(popMenu, menu);
                Point pt = this.PointToClient(MousePosition);
                popMenu.Show(this, pt, LeftRightAlignment.Right);
            }
            else
            {
                item_Click(menu);
            }
        }

        void LoopLoadPopMenu(ContextMenu popMenu, CMenu pmenu)
        {
            List<CBaseObject> lstObj = Program.Ctx.MenuMgr.GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CMenu menu = (CMenu)obj;
                if (menu.Parent_id == pmenu.Id)
                {
                    if (menu.MType == enumMenuType.CatalogMenu)
                    {
                        LoopLoadPopMenu(popMenu, menu);
                    }
                    else
                    {
                        MenuItem item = popMenu.MenuItems.Add(menu.Name);
                        item.Tag = menu;
                        item.Click += new EventHandler(MenuItem_Click);
                    }
                }
            }
        }

        void MenuItem_Click(object sender, EventArgs e)
        {

            MenuItem item = (MenuItem)sender;
            CMenu menu = (CMenu)item.Tag;
            item_Click(menu);
        }
        void item_Click(CMenu menu)
        {
            if (menu.MType == enumMenuType.ViewMenu)
            {
                CView view = (CView)Program.Ctx.ViewMgr.Find(menu.UI_View_id);
                if (view != null)
                {
                    CTable table = (CTable)Program.Ctx.TableMgr.Find(view.FW_Table_id);
                    if (table == null)
                        return;
                    CBaseObjectMgr objMgr = Program.Ctx.FindBaseObjectMgrCache(table.Code, Guid.Empty);
                    if (objMgr == null)
                    {
                        objMgr = new CBaseObjectMgr();
                        objMgr.TbCode = table.Code;
                        objMgr.Ctx = Program.Ctx;
                    }

                    if (view.VType == enumViewType.Single)
                    {
                        SingleView frm = new SingleView();
                        frm.View = view;
                        frm.BaseObjectMgr = objMgr;
                        frm.Show();
                    }
                    else if (view.VType == enumViewType.MasterDetail)
                    {
                        MasterDetailView frm = new MasterDetailView();
                        frm.View = view;
                        frm.BaseObjectMgr = objMgr;
                        frm.Show();
                    }
                    else
                    {
                        MultMasterDetailView frm = new MultMasterDetailView();
                        frm.View = view;
                        frm.BaseObjectMgr = objMgr;
                        frm.Show();
                    }
                }
            }
            else if (menu.MType == enumMenuType.WindowMenu)
            {
                CWindow window = (CWindow)Program.Ctx.WindowMgr.Find(menu.UI_Window_id);
                if (window != null)
                {
                    LayoutWindow frm = new LayoutWindow();
                    frm.Window = window;
                    frm.Show();
                }
            }
            else if (menu.MType == enumMenuType.UrlMenu)
            {
                bool bIsWebUrl = menu.Url.Length > 4 && menu.Url.Substring(0, 4).Equals("http", StringComparison.OrdinalIgnoreCase);
                if (!bIsWebUrl && !File.Exists(menu.Url))
                {
                    MessageBox.Show(string.Format("{0} 不存在！", menu.Url));
                    return;
                }
                System.Diagnostics.Process.Start(menu.Url);
            }
            else if (menu.MType == enumMenuType.ReportMenu)
            {
                CCompany Company = (CCompany)Program.Ctx.CompanyMgr.Find(Program.User.B_Company_id);
                CReport Report = (CReport)Company.ReportMgr.Find(menu.RPT_Report_id);
                RunReport frm = new RunReport();
                frm.m_Report = Report;
                frm.Show();
            }
        }

        void adminBox_ClickItem(object sender, EventArgs e)
        {
            if (m_frmAdmin == null || m_frmAdmin.IsDisposed)
                m_frmAdmin = new AdminForm();
            m_frmAdmin.m_frmDesktopPanel = this;
            m_frmAdmin.Show();
        }

        private void MenuItemBackground_Click(object sender, EventArgs e)
        {
            SelBgForm frm = new SelBgForm();
            if (frm.ShowDialog() != DialogResult.OK)
                return;
            CDesktop desktop =(CDesktop) Program.User.DesktopMgr.GetFirstObj();
            if (desktop == null)
            {
                desktop = new CDesktop();
                desktop.Ctx = Program.Ctx;
                desktop.B_User_id = Program.User.Id;
                Program.User.DesktopMgr.AddNew(desktop);
            }
            FileInfo fi=new FileInfo(frm.m_sBgUrl);
            desktop.BackImg = fi.Name;
            if (!Program.User.DesktopMgr.Save(true))
            {
                MessageBox.Show("保存失败！");
                return;
            }
            this.flowLayoutPanel.BackgroundImage = Image.FromFile(frm.m_sBgUrl);
        }

        private void DesktopPanel_Shown(object sender, EventArgs e)
        {
            string sPath = Application.StartupPath + "\\config.cfg";
            if (!File.Exists(sPath))
            {
                DbSetup frmDb = new DbSetup();
                if (frmDb.ShowDialog() != DialogResult.OK)
                {
                    Application.Exit();
                    return;
                }
            }
            string sConnectionString = ErpCore.Util.DESEncrypt.DesDecrypt(File.ReadAllText(sPath));
            Program.Ctx.ConnectionString = sConnectionString;

            LoginForm frm = new LoginForm();
            if (frm.ShowDialog() != DialogResult.OK)
            {
                Application.Exit();
                return;
            }
            LoadDesktop();
            LoadDesktopGroup();
            //LoadList();
            rdDefault.Checked = true;
            DesktopGroup_rdButton_Click(rdDefault, null);
        }
    }
}
