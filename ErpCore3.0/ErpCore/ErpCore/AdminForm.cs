using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.SubSystem;
using ErpCoreModel.UI;
using ErpCore.Database.Diagram;
using ErpCore.Window.Designer;
using ErpCore.Window;
using ErpCoreModel.Report;
using ErpCoreModel.Workflow;
using ErpCore.Common;

namespace ErpCore
{
    public partial class AdminForm : Form
    {
        ErpCore.Window.WindowPanel windowPanel = new ErpCore.Window.WindowPanel();
        ErpCore.View.ViewPanel viewPanel = new ErpCore.View.ViewPanel();
        ErpCore.FormF.FormPanel formPanel = new ErpCore.FormF.FormPanel();
        ErpCore.Security.Org.OrgPanel orgPanel = new ErpCore.Security.Org.OrgPanel();
        ErpCore.Security.Role.RolePanel rolePanel = new ErpCore.Security.Role.RolePanel();
        ErpCore.Security.Access.AccessPanel accessPanel = new ErpCore.Security.Access.AccessPanel();        
        ErpCore.Security.User.UserPanel userPanel = new ErpCore.Security.User.UserPanel();
        ErpCore.Database.Diagram.DiagramPanel diagramPanel = new ErpCore.Database.Diagram.DiagramPanel();
        ErpCore.Database.Table.TablePanel tablePanel = new ErpCore.Database.Table.TablePanel();
        ErpCore.SubSystem.SubSystemPanel subSystemPanel = new ErpCore.SubSystem.SubSystemPanel();
        ErpCore.Report.ReportPanel reportPanel = new ErpCore.Report.ReportPanel();
        ErpCore.Workflow.WorkflowDefPanel workflowDefPanel = new ErpCore.Workflow.WorkflowDefPanel();
        ErpCore.Menu.MenuPanel menuPanel = new ErpCore.Menu.MenuPanel();

        public Desktop.DesktopPanel m_frmDesktopPanel = null;//桌面
        public AdminForm()
        {
            InitializeComponent();
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            LoadPanel();
            LoadTree();

            treeLeft.ExpandAll();
        }
        void LoadPanel()
        {
            this.windowPanel.Visible = false;
            this.viewPanel.Visible = false;
            this.formPanel.Visible = false;
            this.orgPanel.Visible = false;
            this.rolePanel.Visible = false;
            this.accessPanel.Visible = false;
            this.userPanel.Visible = false;
            this.diagramPanel.Visible = false;
            this.tablePanel.Visible = false;
            this.subSystemPanel.Visible = false;
            this.reportPanel.Visible = false;
            this.workflowDefPanel.Visible = false;
            this.menuPanel.Visible = false;
            this.splitContainer1.Panel2.Controls.Add(this.windowPanel);
            this.splitContainer1.Panel2.Controls.Add(this.viewPanel);
            this.splitContainer1.Panel2.Controls.Add(this.formPanel);
            this.splitContainer1.Panel2.Controls.Add(this.orgPanel);
            this.splitContainer1.Panel2.Controls.Add(this.rolePanel);
            this.splitContainer1.Panel2.Controls.Add(this.accessPanel);
            this.splitContainer1.Panel2.Controls.Add(this.userPanel);
            this.splitContainer1.Panel2.Controls.Add(this.diagramPanel);
            this.splitContainer1.Panel2.Controls.Add(this.tablePanel);
            this.splitContainer1.Panel2.Controls.Add(this.subSystemPanel);
            this.splitContainer1.Panel2.Controls.Add(this.reportPanel);
            this.splitContainer1.Panel2.Controls.Add(this.workflowDefPanel);
            this.splitContainer1.Panel2.Controls.Add(this.menuPanel);
        }
        void LoadTree()
        {
            treeLeft.Nodes.Clear();
            TreeNode node = new TreeNode();
            node.Name = "nodeDatabase";
            node.Text = "数据库";
            TreeNodeTag tag = new TreeNodeTag();
            tag.NodeType = TreeNodeType.DatabaseRoot;
            node.ImageIndex = 1;
            node.SelectedImageIndex = 1;
            node.Tag = tag;
            treeLeft.Nodes.Add(node);

            TreeNode nodeSub = new TreeNode();
            nodeSub.Name = "nodeTable";
            nodeSub.Text = "表";
            TreeNodeTag tagSub = new TreeNodeTag();
            tagSub.NodeType = TreeNodeType.TableRoot;
            nodeSub.ImageIndex = 4;
            nodeSub.SelectedImageIndex = 4;
            nodeSub.Tag = tagSub;
            node.Nodes.Add(nodeSub);

            nodeSub = new TreeNode();
            nodeSub.Name = "nodeDiagram";
            nodeSub.Text = "关系图";
            tagSub = new TreeNodeTag();
            tagSub.NodeType = TreeNodeType.DiagramRoot;
            nodeSub.ImageIndex = 7;
            nodeSub.SelectedImageIndex = 7;
            nodeSub.Tag = tagSub;
            node.Nodes.Add(nodeSub);

            node = new TreeNode();
            node.Name = "nodeWindow";
            node.Text = "窗体";
            tag = new TreeNodeTag();
            tag.NodeType = TreeNodeType.WindowCatalogRoot;
            node.ImageIndex = 9;
            node.SelectedImageIndex = 9;
            node.Tag = tag;
            treeLeft.Nodes.Add(node);
            LoadWindowCatalog(node);

            node = new TreeNode();
            node.Name = "nodeForm";
            node.Text = "表单";
            tag = new TreeNodeTag();
            tag.NodeType = TreeNodeType.FormCatalogRoot;
            node.ImageIndex = 9;
            node.SelectedImageIndex = 9;
            node.Tag = tag;
            treeLeft.Nodes.Add(node);
            LoadFormCatalog(node);


            node = new TreeNode();
            node.Name = "nodeView";
            node.Text = "视图";
            tag = new TreeNodeTag();
            tag.NodeType = TreeNodeType.ViewCatalogRoot;
            node.ImageIndex = 9;
            node.SelectedImageIndex = 9;
            node.Tag = tag;
            treeLeft.Nodes.Add(node);
            LoadViewCatalog(node);


            node = new TreeNode();
            node.Name = "nodeDesktopGroup";
            node.Text = "桌面组";
            tag = new TreeNodeTag();
            tag.NodeType = TreeNodeType.DesktopGroupRoot;
            node.ImageIndex = 9;
            node.SelectedImageIndex = 9;
            node.Tag = tag;
            treeLeft.Nodes.Add(node);
            LoadDesktopGroup(node);

            node = new TreeNode();
            node.Name = "nodeSecurity";
            node.Text = "安全性";
            tag = new TreeNodeTag();
            tag.NodeType = TreeNodeType.SecurityRoot;
            node.ImageIndex = 0;
            node.SelectedImageIndex = 0;
            node.Tag = tag;
            treeLeft.Nodes.Add(node);
            LoadSecurityRoot(node);

            node = new TreeNode();
            node.Name = "nodeSubSystem";
            node.Text = "子系统";
            tag = new TreeNodeTag();
            tag.NodeType = TreeNodeType.SubSystemRoot;
            node.ImageIndex = 3;
            node.SelectedImageIndex = 3;
            node.Tag = tag;
            treeLeft.Nodes.Add(node);
            LoadSubSystem(node);

            node = new TreeNode();
            node.Name = "nodeReport";
            node.Text = "报表";
            tag = new TreeNodeTag();
            tag.NodeType = TreeNodeType.ReportCatalogRoot;
            node.ImageIndex = 12;
            node.SelectedImageIndex = 12;
            node.Tag = tag;
            treeLeft.Nodes.Add(node);
            LoadReportRoot(node);

            node = new TreeNode();
            node.Name = "nodeWorkflowDef";
            node.Text = "工作流";
            tag = new TreeNodeTag();
            tag.NodeType = TreeNodeType.WorkflowCatalogRoot;
            node.ImageIndex = 12;
            node.SelectedImageIndex = 12;
            node.Tag = tag;
            treeLeft.Nodes.Add(node);
            LoadWorkflowRoot(node);

            node = new TreeNode();
            node.Name = "nodeMenu";
            node.Text = "菜单";
            tag = new TreeNodeTag();
            tag.NodeType = TreeNodeType.MenuRoot;
            node.ImageIndex = 12;
            node.SelectedImageIndex = 12;
            node.Tag = tag;
            treeLeft.Nodes.Add(node);
        }

        void LoadSecurityRoot(TreeNode pnode)
        {
            List<CBaseObject> lstObj = Program.Ctx.CompanyMgr.GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CCompany Company = (CCompany)obj;

                TreeNode node = new TreeNode();
                node.Name = "nodeSecurityCompany";
                node.Text = Company.Name;
                TreeNodeTag tag = new TreeNodeTag();
                tag.NodeType = TreeNodeType.SecurityCompany;
                tag.Data = Company;
                node.ImageIndex = 12;
                node.SelectedImageIndex = 12;
                node.Tag = tag;
                pnode.Nodes.Add(node);

                TreeNode nodeSub = new TreeNode();
                nodeSub.Name = "nodeUser";
                nodeSub.Text = "用户";
                TreeNodeTag tagSub = new TreeNodeTag();
                tagSub.NodeType = TreeNodeType.UserRoot;
                nodeSub.ImageIndex = 5;
                nodeSub.SelectedImageIndex = 5;
                nodeSub.Tag = tagSub;
                node.Nodes.Add(nodeSub);

                nodeSub = new TreeNode();
                nodeSub.Name = "nodeOrg";
                nodeSub.Text = "组织";
                tagSub = new TreeNodeTag();
                tagSub.NodeType = TreeNodeType.OrgRoot;
                nodeSub.ImageIndex = 2;
                nodeSub.SelectedImageIndex = 2;
                nodeSub.Tag = tagSub;
                node.Nodes.Add(nodeSub);

                nodeSub = new TreeNode();
                nodeSub.Name = "nodeRole";
                nodeSub.Text = "角色";
                tagSub = new TreeNodeTag();
                tagSub.NodeType = TreeNodeType.RoleRoot;
                nodeSub.ImageIndex = 0;
                nodeSub.SelectedImageIndex = 0;
                nodeSub.Tag = tagSub;
                node.Nodes.Add(nodeSub);

                nodeSub = new TreeNode();
                nodeSub.Name = "nodeAccess";
                nodeSub.Text = "权限";
                tagSub = new TreeNodeTag();
                tagSub.NodeType = TreeNodeType.AccessRoot;
                nodeSub.ImageIndex = 0;
                nodeSub.SelectedImageIndex = 0;
                nodeSub.Tag = tagSub;
                node.Nodes.Add(nodeSub);

            }
        }
        void LoadWorkflowRoot(TreeNode pnode)
        {
            List<CBaseObject> lstObj = Program.Ctx.CompanyMgr.GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CCompany Company = (CCompany)obj;

                TreeNode node = new TreeNode();
                node.Name = "nodeWorkflowCompany";
                node.Text = Company.Name;
                TreeNodeTag tag = new TreeNodeTag();
                tag.NodeType = TreeNodeType.WorkflowCompany;
                tag.Data = Company;
                node.ImageIndex = 12;
                node.SelectedImageIndex = 12;
                node.Tag = tag;
                pnode.Nodes.Add(node);
                LoadWorkflowCatalog(Company, node);
            }
        }
        void LoadWorkflowCatalog(CCompany Company, TreeNode pNode)
        {
            LoopLoadWorkflowCatalog( Company,Guid.Empty, pNode);
        }
        void LoopLoadWorkflowCatalog(CCompany Company, Guid Parent_id, TreeNode pNode)
        {
            List<CBaseObject> lstWorkflowCatalog = Company.WorkflowCatalogMgr.GetList();
            foreach (CBaseObject obj in lstWorkflowCatalog)
            {
                CWorkflowCatalog catalog = (CWorkflowCatalog)obj;
                if (catalog.Parent_id == Parent_id)
                {
                    TreeNode node = new TreeNode();
                    node.Text = catalog.Name;
                    node.ImageIndex = 10;
                    node.SelectedImageIndex = 10;
                    TreeNodeTag tag = new TreeNodeTag();
                    tag.NodeType = TreeNodeType.WorkflowCatalog;
                    tag.Data = catalog;
                    node.Tag = tag;

                    pNode.Nodes.Add(node);

                    LoopLoadWorkflowCatalog(Company,catalog.Id, node);
                }
            }
        }
        void LoadReportRoot(TreeNode pnode)
        {
            List<CBaseObject> lstObj = Program.Ctx.CompanyMgr.GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CCompany Company = (CCompany)obj;

                TreeNode node = new TreeNode();
                node.Name = "nodeReportCompany";
                node.Text = Company.Name;
                TreeNodeTag tag = new TreeNodeTag();
                tag.NodeType = TreeNodeType.ReportCompany;
                tag.Data = Company;
                node.ImageIndex = 12;
                node.SelectedImageIndex = 12;
                node.Tag = tag;
                pnode.Nodes.Add(node);
                LoadReportCatalog(Company,node);
            }
        }
        void LoadReportCatalog(CCompany Company,TreeNode pNode)
        {
            LoopLoadReportCatalog(Company,Guid.Empty, pNode);
        }
        void LoopLoadReportCatalog(CCompany Company,Guid Parent_id, TreeNode pNode)
        {
            List<CBaseObject> lstReportCatalog = Company.ReportCatalogMgr.GetList();
            foreach (CBaseObject obj in lstReportCatalog)
            {
                CReportCatalog catalog = (CReportCatalog)obj;
                if (catalog.Parent_id == Parent_id )
                {
                    TreeNode node = new TreeNode();
                    node.Text = catalog.Name;
                    node.ImageIndex = 10;
                    node.SelectedImageIndex = 10;
                    TreeNodeTag tag = new TreeNodeTag();
                    tag.NodeType = TreeNodeType.ReportCatalog;
                    tag.Data = catalog;
                    node.Tag = tag;

                    pNode.Nodes.Add(node);

                    LoopLoadReportCatalog(Company,catalog.Id, node);
                }
            }
        }
        void LoadWindowCatalog(TreeNode pNode)
        {
            pNode.Nodes.Clear();

            LoopLoadWindowCatalog(Guid.Empty, pNode);
        }
        void LoopLoadWindowCatalog(Guid Parent_id, TreeNode pNode)
        {
            List<CBaseObject> lstWindowCatalog = Program.Ctx.WindowCatalogMgr.GetList();
            foreach (CBaseObject obj in lstWindowCatalog)
            {
                CWindowCatalog catalog = (CWindowCatalog)obj;
                if (catalog.Parent_id == Parent_id)
                {
                    TreeNode node = new TreeNode();
                    node.Text = catalog.Name;
                    node.ImageIndex = 10;
                    node.SelectedImageIndex = 10;
                    TreeNodeTag tag = new TreeNodeTag();
                    tag.NodeType = TreeNodeType.WindowCatalog;
                    tag.Data = catalog;
                    node.Tag = tag;

                    pNode.Nodes.Add(node);

                    LoopLoadWindowCatalog(catalog.Id, node);
                }
            }
        }

        void LoadFormCatalog(TreeNode pNode)
        {
            pNode.Nodes.Clear();

            LoopLoadFormCatalog(Guid.Empty, pNode);
        }
        void LoopLoadFormCatalog(Guid Parent_id, TreeNode pNode)
        {
            List<CBaseObject> lstFormCatalog = Program.Ctx.FormCatalogMgr.GetList();
            foreach (CBaseObject obj in lstFormCatalog)
            {
                CFormCatalog catalog = (CFormCatalog)obj;
                if (catalog.Parent_id == Parent_id)
                {
                    TreeNode node = new TreeNode();
                    node.Text = catalog.Name;
                    node.ImageIndex = 10;
                    node.SelectedImageIndex = 10;
                    TreeNodeTag tag = new TreeNodeTag();
                    tag.NodeType = TreeNodeType.FormCatalog;
                    tag.Data = catalog;
                    node.Tag = tag;

                    pNode.Nodes.Add(node);

                    LoopLoadFormCatalog(catalog.Id, node);
                }
            }
        }

        void LoadViewCatalog(TreeNode pNode)
        {
            pNode.Nodes.Clear();

            LoopLoadViewCatalog(Guid.Empty, pNode);
        }
        void LoopLoadViewCatalog(Guid Parent_id, TreeNode pNode)
        {
            List<CBaseObject> lstViewCatalog = Program.Ctx.ViewCatalogMgr.GetList();
            foreach (CBaseObject obj in lstViewCatalog)
            {
                CViewCatalog catalog = (CViewCatalog)obj;
                if (catalog.Parent_id == Parent_id)
                {
                    TreeNode node = new TreeNode();
                    node.Text = catalog.Name;
                    node.ImageIndex = 10;
                    node.SelectedImageIndex = 10;
                    TreeNodeTag tag = new TreeNodeTag();
                    tag.NodeType = TreeNodeType.ViewCatalog;
                    tag.Data = catalog;
                    node.Tag = tag;

                    pNode.Nodes.Add(node);

                    LoopLoadViewCatalog(catalog.Id, node);
                }
            }
        }
        void LoadDesktopGroup(TreeNode pNode)
        {
            pNode.Nodes.Clear();

            List<CBaseObject> lstDesktopGroup = Program.Ctx.DesktopGroupMgr.GetList();
            foreach (CBaseObject obj in lstDesktopGroup)
            {
                CDesktopGroup group = (CDesktopGroup)obj;

                TreeNode node = new TreeNode();
                node.Text = group.Name;
                node.ImageIndex = 10;
                node.SelectedImageIndex = 10;
                TreeNodeTag tag = new TreeNodeTag();
                tag.NodeType = TreeNodeType.DesktopGroup;
                tag.Data = group;
                node.Tag = tag;

                pNode.Nodes.Add(node);

            }
        }

        void LoadSubSystem(TreeNode pNode)
        {
            pNode.Nodes.Clear();
            List<CBaseObject> lstObj= Program.Ctx.SystemMgr.GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CSystem system = (CSystem)obj;
                TreeNode node = new TreeNode();
                node.Text = system.Name;
                node.ImageIndex = 6;
                node.SelectedImageIndex = 6;
                TreeNodeTag tag = new TreeNodeTag();
                tag.NodeType = TreeNodeType.SubSystem;
                tag.Data = system;
                node.Tag = tag;

                TreeNode nodeSub = new TreeNode();
                nodeSub.Text = "子关系图";
                nodeSub.ImageIndex = 7;
                nodeSub.SelectedImageIndex = 7;
                TreeNodeTag tagSub = new TreeNodeTag();
                tagSub.NodeType = TreeNodeType.SubDiagram;
                nodeSub.Tag = tagSub;
                node.Nodes.Add(nodeSub);

                pNode.Nodes.Add(node);
            }
        }

        private void treeLeft_AfterSelect(object sender, TreeViewEventArgs e)
        {
            
            TreeNodeTag tag = (TreeNodeTag)e.Node.Tag;
            if (tag == null)
                return;

            if (tag.NodeType == TreeNodeType.TableRoot)
            {
                tablePanel.Dock = DockStyle.Fill;
                tablePanel.Visible = true;
                tablePanel.BringToFront();
            }
            else if (tag.NodeType == TreeNodeType.DiagramRoot)
            {
                diagramPanel.Dock = DockStyle.Fill;
                diagramPanel.Visible = true;
                diagramPanel.BringToFront();
            }
            else if (tag.NodeType == TreeNodeType.UserRoot)
            {
                TreeNodeTag ptag = (TreeNodeTag)e.Node.Parent.Tag;
                CCompany Company = (CCompany)ptag.Data;
                userPanel.Company = Company;
                userPanel.Dock = DockStyle.Fill;
                userPanel.Visible = true;
                userPanel.BringToFront();
            }
            else if (tag.NodeType == TreeNodeType.OrgRoot)
            {
                TreeNodeTag ptag = (TreeNodeTag)e.Node.Parent.Tag;
                CCompany Company = (CCompany)ptag.Data;
                orgPanel.Company = Company;
                orgPanel.Dock = DockStyle.Fill;
                orgPanel.Visible = true;
                orgPanel.BringToFront();
            }
            else if (tag.NodeType == TreeNodeType.RoleRoot)
            {
                TreeNodeTag ptag = (TreeNodeTag)e.Node.Parent.Tag;
                CCompany Company = (CCompany)ptag.Data;
                rolePanel.Company = Company;
                rolePanel.Dock = DockStyle.Fill;
                rolePanel.Visible = true;
                rolePanel.BringToFront();
            }
            else if (tag.NodeType == TreeNodeType.AccessRoot)
            {
                TreeNodeTag ptag = (TreeNodeTag)e.Node.Parent.Tag;
                CCompany Company = (CCompany)ptag.Data;
                accessPanel.Company = Company;
                accessPanel.Dock = DockStyle.Fill;
                accessPanel.Visible = true;
                accessPanel.BringToFront();
            }
            else if (tag.NodeType == TreeNodeType.WindowCatalogRoot)
            {
                windowPanel.Catalog = null;
                windowPanel.Dock = DockStyle.Fill;
                windowPanel.Visible = true;
                windowPanel.BringToFront();
            }
            else if (tag.NodeType == TreeNodeType.WindowCatalog)
            {
                CWindowCatalog catalog = (CWindowCatalog)tag.Data;
                windowPanel.Catalog = catalog;
                windowPanel.Dock = DockStyle.Fill;
                windowPanel.Visible = true;
                windowPanel.BringToFront();
            }
            else if (tag.NodeType == TreeNodeType.ViewCatalogRoot)
            {
                viewPanel.Catalog = null;
                viewPanel.Dock = DockStyle.Fill;
                viewPanel.Visible = true;
                viewPanel.BringToFront();
            }
            else if (tag.NodeType == TreeNodeType.ViewCatalog)
            {
                CViewCatalog catalog = (CViewCatalog)tag.Data;
                viewPanel.Catalog = catalog;
                viewPanel.Dock = DockStyle.Fill;
                viewPanel.Visible = true;
                viewPanel.BringToFront();
            }
            else if (tag.NodeType == TreeNodeType.FormCatalogRoot)
            {
                formPanel.Catalog = null;
                formPanel.Dock = DockStyle.Fill;
                formPanel.Visible = true;
                formPanel.BringToFront();
            }
            else if (tag.NodeType == TreeNodeType.FormCatalog)
            {
                CFormCatalog catalog = (CFormCatalog)tag.Data;
                formPanel.Catalog = catalog;
                formPanel.Dock = DockStyle.Fill;
                formPanel.Visible = true;
                formPanel.BringToFront();
            }
            else if (tag.NodeType == TreeNodeType.SubSystemRoot)
            {
                subSystemPanel.Dock = DockStyle.Fill;
                subSystemPanel.Visible = true;
                subSystemPanel.BringToFront();
                
            }
            else if (tag.NodeType == TreeNodeType.SubDiagram)
            {
                TreeNode pNode = e.Node.Parent;
                TreeNodeTag tagP = (TreeNodeTag)pNode.Tag;
                CSystem system = (CSystem)tagP.Data;
                if (system.FW_Diagram_id == Guid.Empty)
                {
                    SelDiagramForm frm = new SelDiagramForm();
                    if (frm.ShowDialog() != DialogResult.OK)
                        return;
                    system.FW_Diagram_id = frm.m_SelDiagram.Id;
                    if (!Program.Ctx.SystemMgr.Update(system))
                    {
                        MessageBox.Show("修改失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                    }
                    DesignerForm frm2 = new DesignerForm();
                    frm2.m_Diagram = frm.m_SelDiagram;
                    frm2.Show(this);
                }
                else
                {
                    CDiagram diagram = (CDiagram)Program.Ctx.DiagramMgr.Find(system.FW_Diagram_id);
                    if (diagram == null)
                    {
                        SelDiagramForm frm = new SelDiagramForm();
                        if (frm.ShowDialog() != DialogResult.OK)
                            return;
                        system.FW_Diagram_id = frm.m_SelDiagram.Id;
                        if (!Program.Ctx.SystemMgr.Update(system))
                        {
                            MessageBox.Show("修改失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                        }
                        DesignerForm frm2 = new DesignerForm();
                        frm2.m_Diagram = frm.m_SelDiagram;
                        frm2.Show(this);
                    }
                    else
                    {
                        DesignerForm frm2 = new DesignerForm();
                        frm2.m_Diagram = diagram;
                        frm2.Show(this);
                    }
                }
            }
            else if (tag.NodeType == TreeNodeType.WindowCatalog)
            {
            }
            else if (tag.NodeType == TreeNodeType.ReportCompany)
            {
                TreeNodeTag ptag = (TreeNodeTag)e.Node.Tag;
                CCompany Company = (CCompany)ptag.Data;
                reportPanel.Company = Company;
                reportPanel.Catalog = null;
                reportPanel.Dock = DockStyle.Fill;
                reportPanel.Visible = true;
                reportPanel.BringToFront();
            }
            else if (tag.NodeType == TreeNodeType.ReportCatalog)
            {
                CCompany Company = null;
                TreeNode p = e.Node.Parent;
                while (p != null)
                {
                    TreeNodeTag ptag = (TreeNodeTag)p.Tag;
                    if (ptag.NodeType == TreeNodeType.ReportCompany)
                    {
                        Company = (CCompany)ptag.Data;
                        break;
                    }
                    p = p.Parent;
                }
                reportPanel.Company = Company;
                CReportCatalog catalog = (CReportCatalog)tag.Data;
                reportPanel.Catalog = catalog;
                reportPanel.Dock = DockStyle.Fill;
                reportPanel.Visible = true;
                reportPanel.BringToFront();
            }
            else if (tag.NodeType == TreeNodeType.WorkflowCompany)
            {
                TreeNodeTag ptag = (TreeNodeTag)e.Node.Tag;
                CCompany Company = (CCompany)ptag.Data;
                workflowDefPanel.Company = Company;
                workflowDefPanel.Catalog = null;
                workflowDefPanel.Dock = DockStyle.Fill;
                workflowDefPanel.Visible = true;
                workflowDefPanel.BringToFront();
            }
            else if (tag.NodeType == TreeNodeType.WorkflowCatalog)
            {
                CCompany Company = null;
                TreeNode p = e.Node.Parent;
                while (p != null)
                {
                    TreeNodeTag ptag = (TreeNodeTag)p.Tag;
                    if (ptag.NodeType == TreeNodeType.WorkflowCompany)
                    {
                        Company = (CCompany)ptag.Data;
                        break;
                    }
                    p = p.Parent;
                }
                workflowDefPanel.Company = Company;
                CWorkflowCatalog catalog = (CWorkflowCatalog)tag.Data;
                workflowDefPanel.Catalog = catalog;
                workflowDefPanel.Dock = DockStyle.Fill;
                workflowDefPanel.Visible = true;
                workflowDefPanel.BringToFront();
            }
            else if (tag.NodeType == TreeNodeType.MenuRoot)
            {
                menuPanel.Dock = DockStyle.Fill;
                menuPanel.Visible = true;
                menuPanel.BringToFront();
            }
        }

        private void MenuItemSetSystemDiagram_Click(object sender, EventArgs e)
        {
            if (treeLeft.SelectedNode == null)
                return;
            TreeNode pNode = treeLeft.SelectedNode.Parent;
            CSystem system = (CSystem)pNode.Tag;
            SelDiagramForm frm = new SelDiagramForm();
            if (frm.ShowDialog() != DialogResult.OK)
                return;
            system.FW_Diagram_id = frm.m_SelDiagram.Id;
            if (!Program.Ctx.SystemMgr.Update(system))
            {
                MessageBox.Show("修改失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            }
        }

        private void treeLeft_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.Node != null)
                    treeLeft.SelectedNode = e.Node;
                TreeNodeTag tag = (TreeNodeTag)e.Node.Tag;

                if (tag.NodeType== TreeNodeType.SubDiagram)
                {
                    Point pt = treeLeft.PointToScreen( e.Location);
                    contextMenuSystemDiagram.Show(pt);
                }
                else if (tag.NodeType== TreeNodeType.ViewCatalogRoot)
                {
                    Point pt = treeLeft.PointToScreen(e.Location);
                    MenuItemNewViewCatalog.Visible = true;
                    MenuItemEditViewCatalog.Visible = false;
                    MenuItemDelViewCatalog.Visible = false;
                    contextMenuView.Show(pt);
                }
                else if (tag.NodeType == TreeNodeType.ViewCatalog)
                {
                    Point pt = treeLeft.PointToScreen(e.Location);
                    MenuItemNewViewCatalog.Visible = false;
                    MenuItemEditViewCatalog.Visible = true;
                    MenuItemDelViewCatalog.Visible = true;
                    contextMenuView.Show(pt);
                }
                else if (tag.NodeType== TreeNodeType.DesktopGroupRoot)
                {
                    Point pt = treeLeft.PointToScreen(e.Location);
                    MenuItemNewDesktopGroup.Visible = true;
                    MenuItemDelDesktopGroup.Visible = false;
                    contextMenuDesktopGroup.Show(pt);
                }
                else if (tag.NodeType == TreeNodeType.DesktopGroup)
                {
                    Point pt = treeLeft.PointToScreen(e.Location);
                    MenuItemNewDesktopGroup.Visible = false;
                    MenuItemDelDesktopGroup.Visible = true;
                    contextMenuDesktopGroup.Show(pt);
                }
                
            }
        }

        #region 视图菜单
        //新建视图菜单
        private void MenuItemNewViewCatalog_Click(object sender, EventArgs e)
        {
            if (treeLeft.SelectedNode == null)
                return;

            string sVal = "";
        _ReTry:
            InputForm frm = new InputForm();
            frm.lbTitle.Text = "请输入目录名称：";
            frm.txtVal.Text = sVal;
            if (frm.ShowDialog() != DialogResult.OK)
                return;
            sVal = frm.txtVal.Text.Trim();
            if (sVal == "")
            {
                MessageBox.Show("名称不能空！");
                goto _ReTry;
            }
            if (Program.Ctx.ViewCatalogMgr.FindByName(sVal) != null)
            {
                MessageBox.Show("目录已经存在！");
                goto _ReTry;
            }
            CViewCatalog catalog = new CViewCatalog();
            catalog.Ctx = Program.Ctx;
            catalog.Name = sVal;
            Program.Ctx.ViewCatalogMgr.AddNew(catalog);
            if (!Program.Ctx.ViewCatalogMgr.Save(true))
            {
                MessageBox.Show("添加目录失败！");
                return;
            }

            TreeNode node = new TreeNode();
            node.Text = catalog.Name;
            node.ImageIndex = 10;
            node.SelectedImageIndex = 10;
            TreeNodeTag tag = new TreeNodeTag();
            tag.NodeType = TreeNodeType.ViewCatalog;
            tag.Data = catalog;
            node.Tag = tag;

            treeLeft.SelectedNode.Nodes.Add(node);
        }
        private void MenuItemEditViewCatalog_Click(object sender, EventArgs e)
        {
            if (treeLeft.SelectedNode == null)
                return;
            TreeNodeTag tag = (TreeNodeTag)treeLeft.SelectedNode.Tag;
            if (tag.NodeType != TreeNodeType.ViewCatalog)
                return;
            CViewCatalog catalog = (CViewCatalog)tag.Data;

            string sVal = catalog.Name;
        _ReTry:
            InputForm frm = new InputForm();
            frm.lbTitle.Text = "请输入目录名称：";
            frm.txtVal.Text = sVal;
            if (frm.ShowDialog() != DialogResult.OK)
                return;
            sVal = frm.txtVal.Text.Trim();
            if (sVal == catalog.Name)
                return;
            if (sVal == "")
            {
                MessageBox.Show("名称不能空！");
                goto _ReTry;
            }
            if (Program.Ctx.ViewCatalogMgr.FindByName(sVal) != null)
            {
                MessageBox.Show("目录已经存在！");
                goto _ReTry;
            }

            catalog.Name = sVal;
            Program.Ctx.ViewCatalogMgr.Update(catalog);
            if (!Program.Ctx.ViewCatalogMgr.Save(true))
            {
                MessageBox.Show("修改目录失败！");
                return;
            }


            treeLeft.SelectedNode.Text = sVal;
        }

        private void MenuItemDelViewCatalog_Click(object sender, EventArgs e)
        {
            if (treeLeft.SelectedNode == null)
                return;
            string sMsg = string.Format("确认删除 {0} ?", treeLeft.SelectedNode.Text);
            if (MessageBox.Show(sMsg, "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.OK)
                return;
            TreeNodeTag tag = (TreeNodeTag)treeLeft.SelectedNode.Tag;
            if (tag.NodeType != TreeNodeType.ViewCatalog)
                return;
            CViewCatalog catalog = (CViewCatalog)tag.Data;

            catalog.m_ObjectMgr.Delete(catalog);
            if (!catalog.m_ObjectMgr.Save(true))
            {
                MessageBox.Show("删除目录失败！");
                return;
            }

            treeLeft.SelectedNode.Parent.Nodes.Remove(treeLeft.SelectedNode);
        }
        #endregion 视图菜单

        #region 桌面组菜单
        //新建桌面组菜单
        private void MenuItemNewDesktopGroup_Click(object sender, EventArgs e)
        {
            if (treeLeft.SelectedNode == null)
                return;

            string sVal = "";
        _ReTry:
            InputForm frm = new InputForm();
            frm.lbTitle.Text = "请输入桌面组名称：";
            frm.txtVal.Text = sVal;
            if (frm.ShowDialog() != DialogResult.OK)
                return;
            sVal = frm.txtVal.Text.Trim();
            if (sVal == "")
            {
                MessageBox.Show("名称不能空！");
                goto _ReTry;
            }
            if (Program.Ctx.DesktopGroupMgr.FindByName(sVal) != null)
            {
                MessageBox.Show("桌面组已经存在！");
                goto _ReTry;
            }
            CDesktopGroup group = new CDesktopGroup();
            group.Ctx = Program.Ctx;
            group.Name = sVal;
            Program.Ctx.DesktopGroupMgr.AddNew(group);
            if (!Program.Ctx.DesktopGroupMgr.Save(true))
            {
                MessageBox.Show("添加桌面组失败！");
                return;
            }

            TreeNode node = new TreeNode();
            node.Text = group.Name;
            node.ImageIndex = 10;
            node.SelectedImageIndex = 10;
            TreeNodeTag tag = new TreeNodeTag();
            tag.NodeType = TreeNodeType.DesktopGroup;
            tag.Data = group;
            node.Tag = tag;

            treeLeft.SelectedNode.Nodes.Add(node);
            //刷新桌面组
            if (m_frmDesktopPanel != null)
            {
                m_frmDesktopPanel.LoadDesktopGroup();
            }
        }
        private void MenuItemEditDesktopGroup_Click(object sender, EventArgs e)
        {
            if (treeLeft.SelectedNode == null)
                return;
            TreeNodeTag tag = (TreeNodeTag)treeLeft.SelectedNode.Tag;
            if (tag.NodeType != TreeNodeType.DesktopGroup)
                return;
            CDesktopGroup group = (CDesktopGroup)tag.Data;

            string sVal = group.Name;
        _ReTry:
            InputForm frm = new InputForm();
            frm.lbTitle.Text = "请输入桌面组名称：";
            frm.txtVal.Text = sVal;
            if (frm.ShowDialog() != DialogResult.OK)
                return;
            sVal = frm.txtVal.Text.Trim();
            if (sVal == group.Name)
                return;
            if (sVal == "")
            {
                MessageBox.Show("名称不能空！");
                goto _ReTry;
            }
            if (Program.Ctx.DesktopGroupMgr.FindByName(sVal) != null)
            {
                MessageBox.Show("桌面组已经存在！");
                goto _ReTry;
            }

            group.Name = sVal;
            Program.Ctx.DesktopGroupMgr.Update(group);
            if (!Program.Ctx.DesktopGroupMgr.Save(true))
            {
                MessageBox.Show("修改桌面组失败！");
                return;
            }
            treeLeft.SelectedNode.Text = sVal;
            //刷新桌面组
            if (m_frmDesktopPanel != null)
            {
                m_frmDesktopPanel.LoadDesktopGroup();
            }
        }

        private void MenuItemDelDesktopGroup_Click(object sender, EventArgs e)
        {
            if (treeLeft.SelectedNode == null)
                return;
            string sMsg = string.Format("确认删除 {0} ?",treeLeft.SelectedNode.Text);
            if (MessageBox.Show(sMsg, "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.OK)
                return;
            TreeNodeTag tag = (TreeNodeTag)treeLeft.SelectedNode.Tag;
            CDesktopGroup group = (CDesktopGroup)tag.Data;
            Program.Ctx.DesktopGroupMgr.Delete(group);
            if (!Program.Ctx.DesktopGroupMgr.Save(true))
            {
                MessageBox.Show("删除桌面组失败！");
                return;
            }
            treeLeft.SelectedNode.Parent.Nodes.Remove(treeLeft.SelectedNode);
            //刷新桌面组
            if (m_frmDesktopPanel != null)
            {
                m_frmDesktopPanel.LoadDesktopGroup();
            }
        }
        #endregion 桌面组菜单


    }
}
