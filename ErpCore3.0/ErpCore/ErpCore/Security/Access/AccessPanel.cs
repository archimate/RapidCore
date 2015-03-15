using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.SubSystem;
using ErpCoreModel.UI;
using ErpCoreModel.Report;
using ErpCore.Window;

namespace ErpCore.Security.Access
{
    public partial class AccessPanel : UserControl
    {
        CCompany company = null;

        public AccessPanel()
        {
            InitializeComponent();
        }
        public CCompany Company
        {
            get { return company; }
            set
            {
                company = value;
                
                LoadType();
                LoadDesktopGroupGrid();
                LoadViewGrid();
                LoadTableGrid();
                //LoadColumnGrid();
                LoadDesktopGroup();
                LoadTree();
                LoadReportGrid();
            }
        }

        private void AccessPanel_Load(object sender, EventArgs e)
        {
        }
        void LoadDesktopGroup()
        {
            tcbDesktopGroup.Items.Clear();
            tcbDesktopGroup.Items.Add(new DataItem("主桌面",null));
            List<CBaseObject> lstObj = Program.Ctx.DesktopGroupMgr.GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CDesktopGroup group = (CDesktopGroup)obj;
                DataItem item = new DataItem(group.Name, group);
                tcbDesktopGroup.Items.Add(item);
            }
        }
        void LoadType()
        {
            tcbType1.SelectedIndex = 0;
            tcbType2.SelectedIndex = 0;
            tcbType3.SelectedIndex = 0;
            tcbType4.SelectedIndex = 0;
            tcbType5.SelectedIndex = 0;
            tcbType6.SelectedIndex = 0;
        }
        public void LoadTree()
        {
            if (treeView == null)
                return;
            treeView.Nodes.Clear();

            TreeNode node = new TreeNode("菜单");
            treeView.Nodes.Add(node);

            LoopLoadTree(node, null);

            treeView.ExpandAll();
        }
        void LoopLoadTree(TreeNode pNode, CMenu pMenu)
        {
            List<CBaseObject> lstObj = Program.Ctx.MenuMgr.GetList();
            if (lstObj.Count == 0)
                return;

            foreach (CBaseObject obj in lstObj)
            {
                CMenu menu = (CMenu)obj;
                if (pMenu == null)
                {
                    if (menu.Parent_id != Guid.Empty)
                        continue;
                }
                else
                {
                    if (menu.Parent_id != pMenu.Id)
                        continue;
                }
                TreeNode node = new TreeNode(menu.Name);
                node.Tag = menu;
                if (pNode == null)
                    treeView.Nodes.Add(node);
                else
                    pNode.Nodes.Add(node);

                LoopLoadTree(node, menu);
            }
        }


        private void tcbType1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadUser1(tcbType1.SelectedIndex);
        }
        void LoadUser1(int iType)
        {
            tcbUser1.Items.Clear();
            if (Company == null)
                return;
            if (iType == 0)
            {
                List<CBaseObject> lstObj= Program.Ctx.UserMgr.GetList();
                foreach (CBaseObject obj in lstObj)
                {
                    CUser user = (CUser)obj;
                    if (user.B_Company_id != Company.Id)
                        continue;
                    DataItem item = new DataItem();
                    item.name = user.Name;
                    item.Data = user;
                    tcbUser1.Items.Add(item);
                }
            }
            else
            {
                List<CBaseObject> lstObj = Company.RoleMgr.GetList();
                foreach (CBaseObject obj in lstObj)
                {
                    CRole role = (CRole)obj;
                    DataItem item = new DataItem();
                    item.name = role.Name;
                    item.Data = role;
                    tcbUser1.Items.Add(item);
                }
            }
        }

        private void tcbType2_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadUser2(tcbType2.SelectedIndex);
        }
        void LoadUser2(int iType)
        {
            tcbUser2.Items.Clear();
            if (Company == null)
                return;
            if (iType == 0)
            {
                List<CBaseObject> lstObj = Program.Ctx.UserMgr.GetList();
                foreach (CBaseObject obj in lstObj)
                {
                    CUser user = (CUser)obj;
                    if (user.B_Company_id != Company.Id)
                        continue;
                    DataItem item = new DataItem();
                    item.name = user.Name;
                    item.Data = user;
                    tcbUser2.Items.Add(item);
                }
            }
            else
            {
                List<CBaseObject> lstObj = Company.RoleMgr.GetList();
                foreach (CBaseObject obj in lstObj)
                {
                    CRole role = (CRole)obj;
                    DataItem item = new DataItem();
                    item.name = role.Name;
                    item.Data = role;
                    tcbUser2.Items.Add(item);
                }
            }
        }


        private void tcbType3_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadUser3(tcbType3.SelectedIndex);
        }
        void LoadUser3(int iType)
        {
            tcbUser3.Items.Clear();
            if (Company == null)
                return;
            if (iType == 0)
            {
                List<CBaseObject> lstObj = Program.Ctx.UserMgr.GetList();
                foreach (CBaseObject obj in lstObj)
                {
                    CUser user = (CUser)obj;
                    if (user.B_Company_id != Company.Id)
                        continue;
                    DataItem item = new DataItem();
                    item.name = user.Name;
                    item.Data = user;
                    tcbUser3.Items.Add(item);
                }
            }
            else
            {
                List<CBaseObject> lstObj = Company.RoleMgr.GetList();
                foreach (CBaseObject obj in lstObj)
                {
                    CRole role = (CRole)obj;
                    DataItem item = new DataItem();
                    item.name = role.Name;
                    item.Data = role;
                    tcbUser3.Items.Add(item);
                }
            }
        }


        private void tcbType4_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadUser4(tcbType4.SelectedIndex);
        }
        void LoadUser4(int iType)
        {
            tcbUser4.Items.Clear();
            if (Company == null)
                return;
            if (iType == 0)
            {
                List<CBaseObject> lstObj = Program.Ctx.UserMgr.GetList();
                foreach (CBaseObject obj in lstObj)
                {
                    CUser user = (CUser)obj;
                    if (user.B_Company_id != Company.Id)
                        continue;
                    DataItem item = new DataItem();
                    item.name = user.Name;
                    item.Data = user;
                    tcbUser4.Items.Add(item);
                }
            }
            else
            {
                List<CBaseObject> lstObj = Company.RoleMgr.GetList();
                foreach (CBaseObject obj in lstObj)
                {
                    CRole role = (CRole)obj;
                    DataItem item = new DataItem();
                    item.name = role.Name;
                    item.Data = role;
                    tcbUser4.Items.Add(item);
                }
            }
        }


        private void tcbType6_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadUser6(tcbType6.SelectedIndex);
        }
        void LoadUser6(int iType)
        {
            tcbUser6.Items.Clear();
            if (Company == null)
                return;
            if (iType == 0)
            {
                List<CBaseObject> lstObj = Program.Ctx.UserMgr.GetList();
                foreach (CBaseObject obj in lstObj)
                {
                    CUser user = (CUser)obj;
                    if (user.B_Company_id != Company.Id)
                        continue;
                    DataItem item = new DataItem();
                    item.name = user.Name;
                    item.Data = user;
                    tcbUser6.Items.Add(item);
                }
            }
            else
            {
                List<CBaseObject> lstObj = Company.RoleMgr.GetList();
                foreach (CBaseObject obj in lstObj)
                {
                    CRole role = (CRole)obj;
                    DataItem item = new DataItem();
                    item.name = role.Name;
                    item.Data = role;
                    tcbUser6.Items.Add(item);
                }
            }
        }

        void LoadDesktopGroupGrid()
        {
            List<CBaseObject> lstObj= Program.Ctx.DesktopGroupMgr.GetList();
            dataGridView1.Rows.Clear();
            foreach (CBaseObject obj in lstObj)
            {
                CDesktopGroup group = (CDesktopGroup)obj;
                dataGridView1.Rows.Add(1);
                DataGridViewRow row= dataGridView1.Rows[dataGridView1.Rows.Count - 1];
                row.Tag = group;
                row.Cells[0].Value = group.Name;
                row.Cells[0].ReadOnly = true;
            }
        }
        void LoadViewGrid()
        {
            List<CBaseObject> lstObj = Program.Ctx.ViewMgr.GetList();
            dataGridView2.Rows.Clear();
            foreach (CBaseObject obj in lstObj)
            {
                CView view = (CView)obj;
                dataGridView2.Rows.Add(1);
                DataGridViewRow row = dataGridView2.Rows[dataGridView2.Rows.Count - 1];
                row.Tag = view;
                row.Cells[0].Value = view.Name;
                row.Cells[0].ReadOnly = true;
            }
        }
        void LoadTableGrid()
        {
            List<CBaseObject> lstObj = Program.Ctx.TableMgr.GetList();
            dataGridView3.Rows.Clear();
            tcbTable.Items.Clear();
            foreach (CBaseObject obj in lstObj)
            {
                CTable table = (CTable)obj;
                dataGridView3.Rows.Add(1);
                DataGridViewRow row = dataGridView3.Rows[dataGridView3.Rows.Count - 1];
                row.Tag = table;
                row.Cells[0].Value = table.Name;
                row.Cells[0].ReadOnly = true;

                DataItem item = new DataItem();
                item.name = table.Name;
                item.Data = table;
                tcbTable.Items.Add(item);
            }
        }
        void LoadColumnGrid()
        {
            if (tcbTable.SelectedIndex == -1)
                return;
            DataItem item = (DataItem)tcbTable.SelectedItem;
            CTable table = (CTable)item.Data;
            dataGridView4.Rows.Clear();
            List<CBaseObject> lstObj = table.ColumnMgr.GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CColumn column = (CColumn)obj;
                dataGridView4.Rows.Add(1);
                DataGridViewRow row = dataGridView4.Rows[dataGridView4.Rows.Count - 1];
                row.Tag = column;
                row.Cells[0].Value = column.Name;

            }
        }
        void LoadReportGrid()
        {
            CCompany Company = (CCompany)Program.Ctx.CompanyMgr.Find(Program.User.B_Company_id);
            List<CBaseObject> lstObj = Company.ReportMgr.GetList();
            dataGridView6.Rows.Clear();
            foreach (CBaseObject obj in lstObj)
            {
                CReport Report = (CReport)obj;
                dataGridView6.Rows.Add(1);
                DataGridViewRow row = dataGridView6.Rows[dataGridView6.Rows.Count - 1];
                row.Tag = Report;
                row.Cells[0].Value = Report.Name;
                row.Cells[0].ReadOnly = true;
            }
        }

        private void tcbTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadColumnGrid();
            LoadColumnAccess();
        }

        private void tcbUser1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDesktopGroupAccess();
        }
        void LoadDesktopGroupAccess()
        {
            if (tcbUser1.SelectedIndex == -1)
                return;
            if (tcbType1.SelectedIndex == 0)
            {
                DataItem item = (DataItem)tcbUser1.SelectedItem;
                CUser user = (CUser)item.Data;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    //管理员有所有权限
                    if (user.IsRole("管理员"))
                    {
                        row.Cells[1].Value = true;
                        row.Cells[2].Value = true;
                        continue;
                    }
                    //
                    CDesktopGroup group = (CDesktopGroup)row.Tag;

                    CDesktopGroupAccessInUser dgaiu = user.DesktopGroupAccessInUserMgr.FindByDesktopGroup(group.Id);
                    if (dgaiu != null)
                    {
                        if (dgaiu.Access == AccessType.forbide)
                        {
                            row.Cells[1].Value = false;
                            row.Cells[2].Value = false;
                        }
                        else if (dgaiu.Access == AccessType.read)
                        {
                            row.Cells[1].Value = true;
                            row.Cells[2].Value = false;
                        }
                        else
                        {
                            row.Cells[1].Value = true;
                            row.Cells[2].Value = true;
                        }
                    }
                    else
                    {
                        row.Cells[1].Value = false;
                        row.Cells[2].Value = false;
                    }
                }
            }
            else
            {
                DataItem item = (DataItem)tcbUser1.SelectedItem;
                CRole role = (CRole)item.Data;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    //管理员有所有权限
                    if (role.Name == "管理员")
                    {
                        row.Cells[1].Value = true;
                        row.Cells[2].Value = true;
                        continue;
                    }
                    //
                    CDesktopGroup group = (CDesktopGroup)row.Tag;

                    CDesktopGroupAccessInRole dgair = role.DesktopGroupAccessInRoleMgr.FindByDesktopGroup(group.Id);
                    if (dgair != null)
                    {
                        if (dgair.Access == AccessType.forbide)
                        {
                            row.Cells[1].Value = false;
                            row.Cells[2].Value = false;
                        }
                        else if (dgair.Access == AccessType.read)
                        {
                            row.Cells[1].Value = true;
                            row.Cells[2].Value = false;
                        }
                        else
                        {
                            row.Cells[1].Value = true;
                            row.Cells[2].Value = true;
                        }
                    }
                    else
                    {
                        row.Cells[1].Value = false;
                        row.Cells[2].Value = false;
                    }
                }
            }
        }

        private void tcbUser2_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadViewAccess();
        }
        void LoadViewAccess()
        {
            if (tcbUser2.SelectedIndex == -1)
                return;
            if (tcbType2.SelectedIndex == 0)
            {
                DataItem item = (DataItem)tcbUser2.SelectedItem;
                CUser user = (CUser)item.Data;
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    //管理员有所有权限
                    if (user.IsRole("管理员"))
                    {
                        row.Cells[1].Value = true;
                        row.Cells[2].Value = true;
                        continue;
                    }
                    //
                    CView view = (CView)row.Tag;

                    CViewAccessInUser vaiu = user.ViewAccessInUserMgr.FindByView(view.Id);
                    if (vaiu != null)
                    {
                        if (vaiu.Access == AccessType.forbide)
                        {
                            row.Cells[1].Value = false;
                            row.Cells[2].Value = false;
                        }
                        if (vaiu.Access == AccessType.read)
                        {
                            row.Cells[1].Value = true;
                            row.Cells[2].Value = false;
                        }
                        else
                        {
                            row.Cells[1].Value = true;
                            row.Cells[2].Value = true;
                        }
                    }
                    else
                    {
                        row.Cells[1].Value = false;
                        row.Cells[2].Value = false;
                    }

                }
            }
            else
            {
                DataItem item = (DataItem)tcbUser2.SelectedItem;
                CRole role = (CRole)item.Data;
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    //管理员有所有权限
                    if (role.Name == "管理员")
                    {
                        row.Cells[1].Value = true;
                        row.Cells[2].Value = true;
                        continue;
                    }
                    //
                    CView view = (CView)row.Tag;

                    CViewAccessInRole vair = role.ViewAccessInRoleMgr.FindByView(view.Id);
                    if (vair != null)
                    {
                        if (vair.Access == AccessType.forbide)
                        {
                            row.Cells[1].Value = false;
                            row.Cells[2].Value = false;
                        }
                        if (vair.Access == AccessType.read)
                        {
                            row.Cells[1].Value = true;
                            row.Cells[2].Value = false;
                        }
                        else
                        {
                            row.Cells[1].Value = true;
                            row.Cells[2].Value = true;
                        }

                    }
                    else
                    {
                        row.Cells[1].Value = false;
                        row.Cells[2].Value = false;
                    }
                }
            }
        }

        private void tcbUser3_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTableAccess();
        }
        void LoadTableAccess()
        {
            if (tcbUser3.SelectedIndex == -1)
                return;
            if (tcbType3.SelectedIndex == 0)
            {
                DataItem item = (DataItem)tcbUser3.SelectedItem;
                CUser user = (CUser)item.Data;
                foreach (DataGridViewRow row in dataGridView3.Rows)
                {
                    //管理员有所有权限
                    if (user.IsRole("管理员"))
                    {
                        row.Cells[1].Value = true;
                        row.Cells[2].Value = true;
                        continue;
                    }
                    //
                    CTable table = (CTable)row.Tag;

                    CTableAccessInUser taiu = user.TableAccessInUserMgr.FindByTable(table.Id);
                    if (taiu != null)
                    {
                        if (taiu.Access == AccessType.forbide)
                        {
                            row.Cells[1].Value = false;
                            row.Cells[2].Value = false;
                        }
                        if (taiu.Access == AccessType.read)
                        {
                            row.Cells[1].Value = true;
                            row.Cells[2].Value = false;
                        }
                        else
                        {
                            row.Cells[1].Value = true;
                            row.Cells[2].Value = true;
                        }
                    }
                    else
                    {
                        row.Cells[1].Value = false;
                        row.Cells[2].Value = false;
                    }
                }
            }
            else
            {
                DataItem item = (DataItem)tcbUser3.SelectedItem;
                CRole role = (CRole)item.Data;
                foreach (DataGridViewRow row in dataGridView3.Rows)
                {
                    //管理员有所有权限
                    if (role.Name == "管理员")
                    {
                        row.Cells[1].Value = true;
                        row.Cells[2].Value = true;
                        continue;
                    }
                    //
                    CTable table = (CTable)row.Tag;

                    CTableAccessInRole tair = role.TableAccessInRoleMgr.FindByTable(table.Id);
                    if (tair.FW_Table_id == table.Id)
                    {
                        if (tair.Access == AccessType.forbide)
                        {
                            row.Cells[1].Value = false;
                            row.Cells[2].Value = false;
                        }
                        if (tair.Access == AccessType.read)
                        {
                            row.Cells[1].Value = true;
                            row.Cells[2].Value = false;
                        }
                        else
                        {
                            row.Cells[1].Value = true;
                            row.Cells[2].Value = true;
                        }
                    }
                    else
                    {
                        row.Cells[1].Value = false;
                        row.Cells[2].Value = false;
                    }
                }
            }
        }

        private void tcbUser4_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadColumnAccess();
        }
        void LoadColumnAccess()
        {
            if (tcbUser4.SelectedIndex == -1)
                return;
            if (tcbType4.SelectedIndex == 0)
            {
                DataItem item = (DataItem)tcbUser4.SelectedItem;
                CUser user = (CUser)item.Data;
                foreach (DataGridViewRow row in dataGridView4.Rows)
                {
                    //管理员有所有权限
                    if (user.IsRole("管理员"))
                    {
                        row.Cells[1].Value = true;
                        row.Cells[2].Value = true;
                        continue;
                    }
                    //
                    CColumn column = (CColumn)row.Tag;

                    CColumnAccessInUser caiu = user.ColumnAccessInUserMgr.FindByColumn(column.Id);
                    if (caiu != null)
                    {
                        if (caiu.Access == AccessType.forbide)
                        {
                            row.Cells[1].Value = false;
                            row.Cells[2].Value = false;
                        }
                        if (caiu.Access == AccessType.read)
                        {
                            row.Cells[1].Value = true;
                            row.Cells[2].Value = false;
                        }
                        else
                        {
                            row.Cells[1].Value = true;
                            row.Cells[2].Value = true;
                        }

                    }
                    else
                    {
                        row.Cells[1].Value = false;
                        row.Cells[2].Value = false;
                    }
                }
            }
            else
            {
                DataItem item = (DataItem)tcbUser4.SelectedItem;
                CRole role = (CRole)item.Data;
                foreach (DataGridViewRow row in dataGridView4.Rows)
                {
                    //管理员有所有权限
                    if (role.Name == "管理员")
                    {
                        row.Cells[1].Value = true;
                        row.Cells[2].Value = true;
                        continue;
                    }
                    //
                    CColumn column = (CColumn)row.Tag;

                    CColumnAccessInRole cair = role.ColumnAccessInRoleMgr.FindByColumn(column.Id);
                    if (cair != null)
                    {
                        if (cair.Access == AccessType.forbide)
                        {
                            row.Cells[1].Value = false;
                            row.Cells[2].Value = false;
                        }
                        if (cair.Access == AccessType.read)
                        {
                            row.Cells[1].Value = true;
                            row.Cells[2].Value = false;
                        }
                        else
                        {
                            row.Cells[1].Value = true;
                            row.Cells[2].Value = true;
                        }

                    }
                    else
                    {
                        row.Cells[1].Value = false;
                        row.Cells[2].Value = false;
                    }
                }
            }
        }


        private void tcbUser6_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadReportAccess();
        }
        void LoadReportAccess()
        {
            if (tcbUser6.SelectedIndex == -1)
                return;
            if (tcbType6.SelectedIndex == 0)
            {
                DataItem item = (DataItem)tcbUser6.SelectedItem;
                CUser user = (CUser)item.Data;
                foreach (DataGridViewRow row in dataGridView6.Rows)
                {
                    //管理员有所有权限
                    if (user.IsRole("管理员"))
                    {
                        row.Cells[1].Value = true;
                        row.Cells[2].Value = true;
                        continue;
                    }
                    //
                    CReport Report = (CReport)row.Tag;

                    CReportAccessInUser raiu = user.ReportAccessInUserMgr.FindByReport(Report.Id);
                    if (raiu != null)
                    {
                        if (raiu.Access == AccessType.forbide)
                        {
                            row.Cells[1].Value = false;
                            row.Cells[2].Value = false;
                        }
                        else if (raiu.Access == AccessType.read)
                        {
                            row.Cells[1].Value = true;
                            row.Cells[2].Value = false;
                        }
                        else
                        {
                            row.Cells[1].Value = true;
                            row.Cells[2].Value = true;
                        }
                    }
                    else
                    {
                        row.Cells[1].Value = false;
                        row.Cells[2].Value = false;
                    }
                }
            }
            else
            {
                DataItem item = (DataItem)tcbUser6.SelectedItem;
                CRole role = (CRole)item.Data;
                foreach (DataGridViewRow row in dataGridView6.Rows)
                {
                    //管理员有所有权限
                    if (role.Name == "管理员")
                    {
                        row.Cells[1].Value = true;
                        row.Cells[2].Value = true;
                        continue;
                    }
                    //
                    CReport Report = (CReport)row.Tag;

                    CReportAccessInRole rair = role.ReportAccessInRoleMgr.FindByReport(Report.Id);
                    if (rair != null)
                    {
                        if (rair.Access == AccessType.forbide)
                        {
                            row.Cells[1].Value = false;
                            row.Cells[2].Value = false;
                        }
                        else if (rair.Access == AccessType.read)
                        {
                            row.Cells[1].Value = true;
                            row.Cells[2].Value = false;
                        }
                        else
                        {
                            row.Cells[1].Value = true;
                            row.Cells[2].Value = true;
                        }
                    }
                    else
                    {
                        row.Cells[1].Value = false;
                        row.Cells[2].Value = false;
                    }
                }
            }
        }

        private void btSave1_Click(object sender, EventArgs e)
        {
            if (tcbUser1.SelectedIndex == -1)
            {
                MessageBox.Show("请选择用户！");
                return;
            }
            if (tcbType1.SelectedIndex == 0)
            {
                DataItem item = (DataItem)tcbUser1.SelectedItem;
                CUser user = (CUser)item.Data;
                //管理员有所有权限，不能修改！
                if (user.IsRole("管理员"))
                {
                    MessageBox.Show("管理员有所有权限，不能修改！");
                    return;
                }
                //
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    CDesktopGroup group = (CDesktopGroup)row.Tag;
                    CDesktopGroupAccessInUser dgaiu = user.DesktopGroupAccessInUserMgr.FindByDesktopGroup(group.Id);
                    if (dgaiu == null)
                    {
                        dgaiu = new CDesktopGroupAccessInUser();
                        dgaiu.UI_DesktopGroup_id = group.Id;
                        dgaiu.B_User_id = user.Id;
                        if (Convert.ToBoolean(row.Cells[2].Value))
                            dgaiu.Access = AccessType.write;
                        else if (Convert.ToBoolean(row.Cells[1].Value))
                            dgaiu.Access = AccessType.read;
                        else
                            dgaiu.Access = AccessType.forbide;
                        user.DesktopGroupAccessInUserMgr.AddNew(dgaiu);
                    }
                    else
                    {
                        if (Convert.ToBoolean(row.Cells[2].Value))
                            dgaiu.Access = AccessType.write;
                        else if (Convert.ToBoolean(row.Cells[1].Value))
                            dgaiu.Access = AccessType.read;
                        else
                            dgaiu.Access = AccessType.forbide;
                        user.DesktopGroupAccessInUserMgr.Update(dgaiu);
                    }
                }

                if (!user.Save(true))
                {
                    MessageBox.Show("保存失败！");
                }
                MessageBox.Show("保存成功！");
            }
            else
            {
                DataItem item = (DataItem)tcbUser1.SelectedItem;
                CRole role = (CRole)item.Data;
                //管理员有所有权限，不能修改！
                if (role.Name == "管理员")
                {
                    MessageBox.Show("管理员有所有权限，不能修改！");
                    return;
                }
                //
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    CDesktopGroup group = (CDesktopGroup)row.Tag;
                    CDesktopGroupAccessInRole dgair = role.DesktopGroupAccessInRoleMgr.FindByDesktopGroup(group.Id);
                    if (dgair == null)
                    {
                        dgair = new CDesktopGroupAccessInRole();
                        dgair.UI_DesktopGroup_id = group.Id;
                        dgair.B_Role_id = role.Id;
                        if (Convert.ToBoolean(row.Cells[2].Value))
                            dgair.Access = AccessType.write;
                        else if (Convert.ToBoolean(row.Cells[1].Value))
                            dgair.Access = AccessType.read;
                        else
                            dgair.Access = AccessType.forbide;
                        role.DesktopGroupAccessInRoleMgr.AddNew(dgair);
                    }
                    else
                    {
                        if (Convert.ToBoolean(row.Cells[2].Value))
                            dgair.Access = AccessType.write;
                        else if (Convert.ToBoolean(row.Cells[1].Value))
                            dgair.Access = AccessType.read;
                        else
                            dgair.Access = AccessType.forbide;
                        role.DesktopGroupAccessInRoleMgr.Update(dgair);
                    }
                }

                if (!role.Save(true))
                {
                    MessageBox.Show("保存失败！");
                }
                MessageBox.Show("保存成功！");
            }
        }

        private void btSave2_Click(object sender, EventArgs e)
        {
            if (tcbUser2.SelectedIndex == -1)
            {
                MessageBox.Show("请选择用户！");
                return;
            }
            if (tcbType2.SelectedIndex == 0)
            {
                DataItem item = (DataItem)tcbUser2.SelectedItem;
                CUser user = (CUser)item.Data;
                //管理员有所有权限，不能修改！
                if (user.IsRole("管理员"))
                {
                    MessageBox.Show("管理员有所有权限，不能修改！");
                    return;
                }
                //
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    CView view = (CView)row.Tag;
                    CViewAccessInUser vaiu = user.ViewAccessInUserMgr.FindByView(view.Id);
                    if (vaiu == null)
                    {
                        vaiu = new CViewAccessInUser();
                        vaiu.UI_View_id = view.Id;
                        vaiu.B_User_id = user.Id;
                        if (Convert.ToBoolean(row.Cells[2].Value))
                            vaiu.Access = AccessType.write;
                        else if (Convert.ToBoolean(row.Cells[1].Value))
                            vaiu.Access = AccessType.read;
                        else
                            vaiu.Access = AccessType.forbide;
                        user.ViewAccessInUserMgr.AddNew(vaiu);
                    }
                    else
                    {
                        if (Convert.ToBoolean(row.Cells[2].Value))
                            vaiu.Access = AccessType.write;
                        else if (Convert.ToBoolean(row.Cells[1].Value))
                            vaiu.Access = AccessType.read;
                        else
                            vaiu.Access = AccessType.forbide;
                        user.ViewAccessInUserMgr.Update(vaiu);
                    }
                }

                if (!user.Save(true))
                {
                    MessageBox.Show("保存失败！");
                }
                MessageBox.Show("保存成功！");
            }
            else
            {
                DataItem item = (DataItem)tcbUser2.SelectedItem;
                CRole role = (CRole)item.Data;
                //管理员有所有权限，不能修改！
                if (role.Name == "管理员")
                {
                    MessageBox.Show("管理员有所有权限，不能修改！");
                    return;
                }
                //
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    CView view = (CView)row.Tag;
                    CViewAccessInRole vair = role.ViewAccessInRoleMgr.FindByView(view.Id);
                    if (vair == null)
                    {
                        vair = new CViewAccessInRole();
                        vair.UI_View_id = view.Id;
                        vair.B_Role_id = role.Id;
                        if (Convert.ToBoolean(row.Cells[2].Value))
                            vair.Access = AccessType.write;
                        else if (Convert.ToBoolean(row.Cells[1].Value))
                            vair.Access = AccessType.read;
                        else
                            vair.Access = AccessType.forbide;
                        role.ViewAccessInRoleMgr.AddNew(vair);
                    }
                    else
                    {
                        if (Convert.ToBoolean(row.Cells[2].Value))
                            vair.Access = AccessType.write;
                        else if (Convert.ToBoolean(row.Cells[1].Value))
                            vair.Access = AccessType.read;
                        else
                            vair.Access = AccessType.forbide;
                        role.ViewAccessInRoleMgr.Update(vair);
                    }
                }

                if (!role.Save(true))
                {
                    MessageBox.Show("保存失败！");
                }
                MessageBox.Show("保存成功！");
            }
        }

        private void btSave3_Click(object sender, EventArgs e)
        {
            if (tcbUser3.SelectedIndex == -1)
            {
                MessageBox.Show("请选择用户！");
                return;
            }
            if (tcbType3.SelectedIndex == 0)
            {
                DataItem item = (DataItem)tcbUser3.SelectedItem;
                CUser user = (CUser)item.Data;
                //管理员有所有权限，不能修改！
                if (user.IsRole("管理员"))
                {
                    MessageBox.Show("管理员有所有权限，不能修改！");
                    return;
                }
                //
                foreach (DataGridViewRow row in dataGridView3.Rows)
                {
                    CTable table = (CTable)row.Tag;
                    CTableAccessInUser taiu = user.TableAccessInUserMgr.FindByTable(table.Id);
                    if (taiu == null)
                    {
                        taiu = new CTableAccessInUser();
                        taiu.FW_Table_id = table.Id;
                        taiu.B_User_id = user.Id;
                        if (Convert.ToBoolean(row.Cells[2].Value))
                            taiu.Access = AccessType.write;
                        else if (Convert.ToBoolean(row.Cells[1].Value))
                            taiu.Access = AccessType.read;
                        else
                            taiu.Access = AccessType.forbide;
                        user.TableAccessInUserMgr.AddNew(taiu);
                    }
                    else
                    {
                        if (Convert.ToBoolean(row.Cells[2].Value))
                            taiu.Access = AccessType.write;
                        else if (Convert.ToBoolean(row.Cells[1].Value))
                            taiu.Access = AccessType.read;
                        else
                            taiu.Access = AccessType.forbide;
                        user.TableAccessInUserMgr.Update(taiu);
                    }
                }

                if (!user.Save(true))
                {
                    MessageBox.Show("保存失败！");
                }
                MessageBox.Show("保存成功！");
            }
            else
            {
                DataItem item = (DataItem)tcbUser3.SelectedItem;
                CRole role = (CRole)item.Data;
                //管理员有所有权限，不能修改！
                if (role.Name == "管理员")
                {
                    MessageBox.Show("管理员有所有权限，不能修改！");
                    return;
                }
                //
                foreach (DataGridViewRow row in dataGridView3.Rows)
                {
                    CTable table = (CTable)row.Tag;
                    CTableAccessInRole tair = role.TableAccessInRoleMgr.FindByTable(table.Id);
                    if (tair == null)
                    {
                        tair = new CTableAccessInRole();
                        tair.FW_Table_id = table.Id;
                        tair.B_Role_id = role.Id;
                        if (Convert.ToBoolean(row.Cells[2].Value))
                            tair.Access = AccessType.write;
                        else if (Convert.ToBoolean(row.Cells[1].Value))
                            tair.Access = AccessType.read;
                        else
                            tair.Access = AccessType.forbide;
                        role.TableAccessInRoleMgr.AddNew(tair);
                    }
                    else
                    {
                        if (Convert.ToBoolean(row.Cells[2].Value))
                            tair.Access = AccessType.write;
                        else if (Convert.ToBoolean(row.Cells[1].Value))
                            tair.Access = AccessType.read;
                        else
                            tair.Access = AccessType.forbide;
                        role.TableAccessInRoleMgr.Update(tair);
                    }
                }

                if (!role.Save(true))
                {
                    MessageBox.Show("保存失败！");
                }
                MessageBox.Show("保存成功！");
            }
        }

        private void btSave4_Click(object sender, EventArgs e)
        {
            if (tcbUser4.SelectedIndex == -1)
            {
                MessageBox.Show("请选择用户！");
                return;
            }
            if (tcbType4.SelectedIndex == 0)
            {
                DataItem item = (DataItem)tcbUser4.SelectedItem;
                CUser user = (CUser)item.Data;
                //管理员有所有权限，不能修改！
                if (user.IsRole("管理员"))
                {
                    MessageBox.Show("管理员有所有权限，不能修改！");
                    return;
                }
                //
                foreach (DataGridViewRow row in dataGridView4.Rows)
                {
                    CColumn column = (CColumn)row.Tag;
                    CColumnAccessInUser caiu = user.ColumnAccessInUserMgr.FindByColumn(column.Id);
                    if (caiu == null)
                    {
                        caiu = new CColumnAccessInUser();
                        caiu.FW_Column_id = column.Id;
                        caiu.B_User_id = user.Id;
                        if (Convert.ToBoolean(row.Cells[2].Value))
                            caiu.Access = AccessType.write;
                        else if (Convert.ToBoolean(row.Cells[1].Value))
                            caiu.Access = AccessType.read;
                        else
                            caiu.Access = AccessType.forbide;
                        user.ColumnAccessInUserMgr.AddNew(caiu);
                    }
                    else
                    {
                        if (Convert.ToBoolean(row.Cells[2].Value))
                            caiu.Access = AccessType.write;
                        else if (Convert.ToBoolean(row.Cells[1].Value))
                            caiu.Access = AccessType.read;
                        else
                            caiu.Access = AccessType.forbide;
                        user.ColumnAccessInUserMgr.Update(caiu);
                    }
                }

                if (!user.Save(true))
                {
                    MessageBox.Show("保存失败！");
                }
                MessageBox.Show("保存成功！");
            }
            else
            {
                DataItem item = (DataItem)tcbUser4.SelectedItem;
                CRole role = (CRole)item.Data;
                //管理员有所有权限，不能修改！
                if (role.Name == "管理员")
                {
                    MessageBox.Show("管理员有所有权限，不能修改！");
                    return;
                }
                //
                foreach (DataGridViewRow row in dataGridView4.Rows)
                {
                    CColumn column = (CColumn)row.Tag;
                    CColumnAccessInRole cair = role.ColumnAccessInRoleMgr.FindByColumn(column.Id);
                    if (cair == null)
                    {
                        cair = new CColumnAccessInRole();
                        cair.FW_Column_id = column.Id;
                        cair.B_Role_id = role.Id;
                        if (Convert.ToBoolean(row.Cells[2].Value))
                            cair.Access = AccessType.write;
                        else if (Convert.ToBoolean(row.Cells[1].Value))
                            cair.Access = AccessType.read;
                        else
                            cair.Access = AccessType.forbide;
                        role.ColumnAccessInRoleMgr.AddNew(cair);
                    }
                    else
                    {
                        if (Convert.ToBoolean(row.Cells[2].Value))
                            cair.Access = AccessType.write;
                        else if (Convert.ToBoolean(row.Cells[1].Value))
                            cair.Access = AccessType.read;
                        else
                            cair.Access = AccessType.forbide;
                        role.ColumnAccessInRoleMgr.Update(cair);
                    }
                }

                if (!role.Save(true))
                {
                    MessageBox.Show("保存失败！");
                }
                MessageBox.Show("保存成功！");
            }
        }

        private void btSave5_Click(object sender, EventArgs e)
        {
            if (tcbUser5.SelectedIndex == -1)
            {
                MessageBox.Show("请选择用户！");
                return;
            }
            if (tcbDesktopGroup.SelectedIndex == -1)
            {
                MessageBox.Show("请选择桌面组！");
                return;
            }
            if (tcbType5.SelectedIndex == 0)
            {
                DataItem item = (DataItem)tcbUser5.SelectedItem;
                CUser user = (CUser)item.Data;

                DataItem item2 = (DataItem)tcbDesktopGroup.SelectedItem;
                CDesktopGroup group = (CDesktopGroup)item2.Data;
                user.UserMenuMgr.RemoveByDesktopGroupId(group!=null? group.Id:Guid.Empty);
                LoopTreeAddUserMenu(null, user, group);

                if (!user.Save(true))
                {
                    MessageBox.Show("保存失败！");
                }
                MessageBox.Show("保存成功！");
            }
            else
            {
                DataItem item = (DataItem)tcbUser4.SelectedItem;
                CRole role = (CRole)item.Data;

                DataItem item2 = (DataItem)tcbDesktopGroup.SelectedItem;
                CDesktopGroup group = (CDesktopGroup)item2.Data;
                role.RoleMenuMgr.RemoveByDesktopGroupId(group != null ? group.Id : Guid.Empty);
                LoopTreeAddRoleMenu(null, role, group);

                if (!role.Save(true))
                {
                    MessageBox.Show("保存失败！");
                }
                MessageBox.Show("保存成功！");
            }
        }
        void LoopTreeAddUserMenu(TreeNode pNode, CUser user, CDesktopGroup group)
        {
            if (pNode == null)
            {
                foreach (TreeNode node in treeView.Nodes)
                {
                    CMenu menu = (CMenu)node.Tag;
                    if (menu != null)
                    {
                        if (node.Checked)
                        {
                            CUserMenu UserMenu = new CUserMenu();
                            UserMenu.Ctx = Program.Ctx;
                            UserMenu.UI_Menu_id = menu.Id;
                            UserMenu.B_User_id = user.Id;
                            UserMenu.UI_DesktopGroup_id = group != null ? group.Id : Guid.Empty;
                            user.UserMenuMgr.AddNew(UserMenu);
                        }
                    }
                    LoopTreeAddUserMenu(node, user, group);
                }
            }
            else
            {
                foreach (TreeNode node in pNode.Nodes)
                {
                    CMenu menu = (CMenu)node.Tag;
                    if (menu != null)
                    {
                        if (node.Checked)
                        {
                            CUserMenu UserMenu = new CUserMenu();
                            UserMenu.Ctx = Program.Ctx;
                            UserMenu.UI_Menu_id = menu.Id;
                            UserMenu.B_User_id = user.Id;
                            UserMenu.UI_DesktopGroup_id = group != null ? group.Id : Guid.Empty;
                            user.UserMenuMgr.AddNew(UserMenu);
                        }
                    }
                    LoopTreeAddUserMenu(node, user, group);
                }
            }
        }
        void LoopTreeAddRoleMenu(TreeNode pNode, CRole role, CDesktopGroup group)
        {
            if (pNode == null)
            {
                foreach (TreeNode node in treeView.Nodes)
                {
                    CMenu menu = (CMenu)node.Tag;
                    if (menu != null)
                    {
                        if (node.Checked)
                        {
                            CRoleMenu RoleMenu = new CRoleMenu();
                            RoleMenu.Ctx = Program.Ctx;
                            RoleMenu.UI_Menu_id = menu.Id;
                            RoleMenu.B_Role_id = role.Id;
                            RoleMenu.UI_DesktopGroup_id = group != null ? group.Id : Guid.Empty;
                            role.RoleMenuMgr.AddNew(RoleMenu);
                        }
                    }
                    LoopTreeAddRoleMenu(node, role, group);
                }
            }
            else
            {
                foreach (TreeNode node in pNode.Nodes)
                {
                    CMenu menu = (CMenu)node.Tag;
                    if (menu != null)
                    {
                        if (node.Checked)
                        {
                            CRoleMenu RoleMenu = new CRoleMenu();
                            RoleMenu.Ctx = Program.Ctx;
                            RoleMenu.UI_Menu_id = menu.Id;
                            RoleMenu.B_Role_id = role.Id;
                            RoleMenu.UI_DesktopGroup_id = group != null ? group.Id : Guid.Empty;
                            role.RoleMenuMgr.AddNew(RoleMenu);
                        }
                    }
                    LoopTreeAddRoleMenu(node, role, group);
                }
            }
        }

        private void tcbType5_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadUser5(tcbType5.SelectedIndex);
        }
        void LoadUser5(int iType)
        {
            tcbUser5.Items.Clear();
            if (Company == null)
                return;
            if (iType == 0)
            {
                List<CBaseObject> lstObj = Program.Ctx.UserMgr.GetList();
                foreach (CBaseObject obj in lstObj)
                {
                    CUser user = (CUser)obj;
                    if (user.B_Company_id != Company.Id)
                        continue;
                    DataItem item = new DataItem();
                    item.name = user.Name;
                    item.Data = user;
                    tcbUser5.Items.Add(item);
                }
            }
            else
            {
                List<CBaseObject> lstObj = Company.RoleMgr.GetList();
                foreach (CBaseObject obj in lstObj)
                {
                    CRole role = (CRole)obj;
                    DataItem item = new DataItem();
                    item.name = role.Name;
                    item.Data = role;
                    tcbUser5.Items.Add(item);
                }
            }
        }

        private void tcbUser5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcbUser5.SelectedIndex == -1)
                return;
            if (tcbDesktopGroup.SelectedIndex == -1)
                return;
            if (tcbType5.SelectedIndex == 0)
            {
                DataItem item = (DataItem)tcbUser5.SelectedItem;
                CUser user = (CUser)item.Data;
                DataItem item2 = (DataItem)tcbDesktopGroup.SelectedItem;
                CDesktopGroup group = (CDesktopGroup)item2.Data;
                SetUserMenuCheckBox(null, user, group);
            }
            else
            {
                DataItem item = (DataItem)tcbUser5.SelectedItem;
                CRole role = (CRole)item.Data;
                DataItem item2 = (DataItem)tcbDesktopGroup.SelectedItem;
                CDesktopGroup group = (CDesktopGroup)item2.Data;
                SetRoleMenuCheckBox(null, role, group);
            }
        }
        void SetUserMenuCheckBox(TreeNode pNode, CUser user, CDesktopGroup group)
        {
            if (pNode == null)
            {
                foreach (TreeNode node in treeView.Nodes)
                {
                    CMenu menu = (CMenu)node.Tag;
                    if (menu != null)
                    {
                        if (user.UserMenuMgr.FindByMenu(menu.Id, group != null ? group.Id : Guid.Empty) != null)
                            node.Checked = true;
                        else
                            node.Checked = false;
                    }
                    SetUserMenuCheckBox(node, user, group);
                }
            }
            else
            {
                foreach (TreeNode node in pNode.Nodes)
                {
                    CMenu menu = (CMenu)node.Tag;
                    if (menu != null)
                    {
                        if (user.UserMenuMgr.FindByMenu(menu.Id, group != null ? group.Id : Guid.Empty) != null)
                            node.Checked = true;
                        else
                            node.Checked = false;
                    }
                    SetUserMenuCheckBox(node, user, group);
                }
            }
        }
        void SetRoleMenuCheckBox(TreeNode pNode, CRole role, CDesktopGroup group)
        {
            if (pNode == null)
            {
                foreach (TreeNode node in treeView.Nodes)
                {
                    CMenu menu = (CMenu)node.Tag;
                    if (menu != null)
                    {
                        if (role.RoleMenuMgr.FindByMenu(menu.Id, group != null ? group.Id : Guid.Empty) != null)
                            node.Checked = true;
                        else
                            node.Checked = false;
                    }
                    SetRoleMenuCheckBox(node, role, group);
                }
            }
            else
            {
                foreach (TreeNode node in pNode.Nodes)
                {
                    CMenu menu = (CMenu)node.Tag;
                    if (menu != null)
                    {
                        if (role.RoleMenuMgr.FindByMenu(menu.Id, group != null ? group.Id : Guid.Empty) != null)
                            node.Checked = true;
                        else
                            node.Checked = false;
                    }
                    SetRoleMenuCheckBox(node, role, group);
                }
            }
        }

        private void tcbDesktopGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcbUser5.SelectedIndex == -1)
                return;
            if (tcbDesktopGroup.SelectedIndex == -1)
                return;
            if (tcbType5.SelectedIndex == 0)
            {
                DataItem item = (DataItem)tcbUser5.SelectedItem;
                CUser user = (CUser)item.Data;
                DataItem item2 = (DataItem)tcbDesktopGroup.SelectedItem;
                CDesktopGroup group = (CDesktopGroup)item2.Data;
                SetUserMenuCheckBox(null, user, group);
            }
            else
            {
                DataItem item = (DataItem)tcbUser5.SelectedItem;
                CRole role = (CRole)item.Data;
                DataItem item2 = (DataItem)tcbDesktopGroup.SelectedItem;
                CDesktopGroup group = (CDesktopGroup)item2.Data;
                SetRoleMenuCheckBox(null, role, group);
            }
        }


        private void btSave6_Click(object sender, EventArgs e)
        {
            if (tcbUser6.SelectedIndex == -1)
            {
                MessageBox.Show("请选择用户！");
                return;
            }
            if (tcbType6.SelectedIndex == 0)
            {
                DataItem item = (DataItem)tcbUser6.SelectedItem;
                CUser user = (CUser)item.Data;
                //管理员有所有权限，不能修改！
                if (user.IsRole("管理员"))
                {
                    MessageBox.Show("管理员有所有权限，不能修改！");
                    return;
                }
                //
                foreach (DataGridViewRow row in dataGridView6.Rows)
                {
                    CReport Report = (CReport)row.Tag;
                    CReportAccessInUser raiu = user.ReportAccessInUserMgr.FindByReport(Report.Id);
                    if (raiu == null)
                    {
                        raiu = new CReportAccessInUser();
                        raiu.RPT_Report_id = Report.Id;
                        raiu.B_User_id = user.Id;
                        if (Convert.ToBoolean(row.Cells[2].Value))
                            raiu.Access = AccessType.write;
                        else if (Convert.ToBoolean(row.Cells[1].Value))
                            raiu.Access = AccessType.read;
                        else
                            raiu.Access = AccessType.forbide;
                        user.ReportAccessInUserMgr.AddNew(raiu);
                    }
                    else
                    {
                        if (Convert.ToBoolean(row.Cells[2].Value))
                            raiu.Access = AccessType.write;
                        else if (Convert.ToBoolean(row.Cells[1].Value))
                            raiu.Access = AccessType.read;
                        else
                            raiu.Access = AccessType.forbide;
                        user.ReportAccessInUserMgr.Update(raiu);
                    }
                }

                if (!user.Save(true))
                {
                    MessageBox.Show("保存失败！");
                }
                MessageBox.Show("保存成功！");
            }
            else
            {
                DataItem item = (DataItem)tcbUser6.SelectedItem;
                CRole role = (CRole)item.Data;
                //管理员有所有权限，不能修改！
                if (role.Name == "管理员")
                {
                    MessageBox.Show("管理员有所有权限，不能修改！");
                    return;
                }
                //
                foreach (DataGridViewRow row in dataGridView6.Rows)
                {
                    CReport Report = (CReport)row.Tag;
                    CReportAccessInRole rair = role.ReportAccessInRoleMgr.FindByReport(Report.Id);
                    if (rair == null)
                    {
                        rair = new CReportAccessInRole();
                        rair.RPT_Report_id = Report.Id;
                        rair.B_Role_id = role.Id;
                        if (Convert.ToBoolean(row.Cells[2].Value))
                            rair.Access = AccessType.write;
                        else if (Convert.ToBoolean(row.Cells[1].Value))
                            rair.Access = AccessType.read;
                        else
                            rair.Access = AccessType.forbide;
                        role.ReportAccessInRoleMgr.AddNew(rair);
                    }
                    else
                    {
                        if (Convert.ToBoolean(row.Cells[2].Value))
                            rair.Access = AccessType.write;
                        else if (Convert.ToBoolean(row.Cells[1].Value))
                            rair.Access = AccessType.read;
                        else
                            rair.Access = AccessType.forbide;
                        role.ReportAccessInRoleMgr.Update(rair);
                    }
                }

                if (!role.Save(true))
                {
                    MessageBox.Show("保存失败！");
                }
                MessageBox.Show("保存成功！");
            }
        }
    }
}
