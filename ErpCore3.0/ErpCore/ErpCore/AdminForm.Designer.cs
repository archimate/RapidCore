namespace ErpCore
{
    partial class AdminForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("表", 4, 4);
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("关系图", 7, 7);
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("子系统", 3, 3);
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("数据库", 1, 1, new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("窗体", 9, 9);
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("表单", 9, 9);
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("用户", 5, 5);
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("组织", 2, 2);
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("角色");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("安全性", new System.Windows.Forms.TreeNode[] {
            treeNode7,
            treeNode8,
            treeNode9});
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("报表", 12, 12);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminForm));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeLeft = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.contextMenuSystemDiagram = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuItemSetSystemDiagram = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuItemNewViewCatalog = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuDesktopGroup = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuItemNewDesktopGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemDelDesktopGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemEditViewCatalog = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemDelViewCatalog = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemEditDesktopGroup = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuSystemDiagram.SuspendLayout();
            this.contextMenuView.SuspendLayout();
            this.contextMenuDesktopGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(792, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // toolStrip
            // 
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(792, 25);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip1";
            // 
            // statusStrip
            // 
            this.statusStrip.Location = new System.Drawing.Point(0, 544);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(792, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 49);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeLeft);
            this.splitContainer1.Size = new System.Drawing.Size(792, 495);
            this.splitContainer1.SplitterDistance = 222;
            this.splitContainer1.TabIndex = 3;
            // 
            // treeLeft
            // 
            this.treeLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeLeft.ImageIndex = 0;
            this.treeLeft.ImageList = this.imageList1;
            this.treeLeft.Location = new System.Drawing.Point(0, 0);
            this.treeLeft.Name = "treeLeft";
            treeNode1.ImageIndex = 4;
            treeNode1.Name = "nodeTable";
            treeNode1.SelectedImageIndex = 4;
            treeNode1.Text = "表";
            treeNode2.ImageIndex = 7;
            treeNode2.Name = "nodeDiagram";
            treeNode2.SelectedImageIndex = 7;
            treeNode2.Text = "关系图";
            treeNode3.ImageIndex = 3;
            treeNode3.Name = "nodeSubSystem";
            treeNode3.SelectedImageIndex = 3;
            treeNode3.Text = "子系统";
            treeNode4.ImageIndex = 1;
            treeNode4.Name = "nodeDatabase";
            treeNode4.SelectedImageIndex = 1;
            treeNode4.Text = "数据库";
            treeNode5.ImageIndex = 9;
            treeNode5.Name = "nodeWindow";
            treeNode5.SelectedImageIndex = 9;
            treeNode5.Text = "窗体";
            treeNode6.ImageIndex = 9;
            treeNode6.Name = "nodeForm";
            treeNode6.SelectedImageIndex = 9;
            treeNode6.Text = "表单";
            treeNode7.ImageIndex = 5;
            treeNode7.Name = "nodeUser";
            treeNode7.SelectedImageIndex = 5;
            treeNode7.Text = "用户";
            treeNode8.ImageIndex = 2;
            treeNode8.Name = "nodeOrg";
            treeNode8.SelectedImageIndex = 2;
            treeNode8.Text = "组织";
            treeNode9.Name = "nodeRole";
            treeNode9.Text = "角色";
            treeNode10.Name = "nodeSecurity";
            treeNode10.Text = "安全性";
            treeNode11.ImageIndex = 12;
            treeNode11.Name = "nodeReport";
            treeNode11.SelectedImageIndex = 12;
            treeNode11.Text = "报表";
            this.treeLeft.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode5,
            treeNode6,
            treeNode10,
            treeNode11});
            this.treeLeft.SelectedImageIndex = 0;
            this.treeLeft.Size = new System.Drawing.Size(218, 491);
            this.treeLeft.TabIndex = 0;
            this.treeLeft.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeLeft_AfterSelect);
            this.treeLeft.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeLeft_NodeMouseClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "access.png");
            this.imageList1.Images.SetKeyName(1, "database.png");
            this.imageList1.Images.SetKeyName(2, "role.png");
            this.imageList1.Images.SetKeyName(3, "system.png");
            this.imageList1.Images.SetKeyName(4, "table.png");
            this.imageList1.Images.SetKeyName(5, "user.png");
            this.imageList1.Images.SetKeyName(6, "app.png");
            this.imageList1.Images.SetKeyName(7, "association.png");
            this.imageList1.Images.SetKeyName(8, "associat.png");
            this.imageList1.Images.SetKeyName(9, "window.png");
            this.imageList1.Images.SetKeyName(10, "catalog2.png");
            this.imageList1.Images.SetKeyName(11, "catalog.png");
            this.imageList1.Images.SetKeyName(12, "report.png");
            // 
            // contextMenuSystemDiagram
            // 
            this.contextMenuSystemDiagram.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemSetSystemDiagram});
            this.contextMenuSystemDiagram.Name = "contextMenuSystemDiagram";
            this.contextMenuSystemDiagram.Size = new System.Drawing.Size(135, 26);
            // 
            // MenuItemSetSystemDiagram
            // 
            this.MenuItemSetSystemDiagram.Name = "MenuItemSetSystemDiagram";
            this.MenuItemSetSystemDiagram.Size = new System.Drawing.Size(134, 22);
            this.MenuItemSetSystemDiagram.Text = "设置关系图";
            this.MenuItemSetSystemDiagram.Click += new System.EventHandler(this.MenuItemSetSystemDiagram_Click);
            // 
            // contextMenuView
            // 
            this.contextMenuView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemNewViewCatalog,
            this.MenuItemEditViewCatalog,
            this.MenuItemDelViewCatalog});
            this.contextMenuView.Name = "contextMenuView";
            this.contextMenuView.Size = new System.Drawing.Size(99, 70);
            // 
            // MenuItemNewViewCatalog
            // 
            this.MenuItemNewViewCatalog.Name = "MenuItemNewViewCatalog";
            this.MenuItemNewViewCatalog.Size = new System.Drawing.Size(98, 22);
            this.MenuItemNewViewCatalog.Text = "新建";
            this.MenuItemNewViewCatalog.Click += new System.EventHandler(this.MenuItemNewViewCatalog_Click);
            // 
            // contextMenuDesktopGroup
            // 
            this.contextMenuDesktopGroup.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemNewDesktopGroup,
            this.MenuItemEditDesktopGroup,
            this.MenuItemDelDesktopGroup});
            this.contextMenuDesktopGroup.Name = "contextMenuView";
            this.contextMenuDesktopGroup.Size = new System.Drawing.Size(153, 92);
            // 
            // MenuItemNewDesktopGroup
            // 
            this.MenuItemNewDesktopGroup.Name = "MenuItemNewDesktopGroup";
            this.MenuItemNewDesktopGroup.Size = new System.Drawing.Size(152, 22);
            this.MenuItemNewDesktopGroup.Text = "新建";
            this.MenuItemNewDesktopGroup.Click += new System.EventHandler(this.MenuItemNewDesktopGroup_Click);
            // 
            // MenuItemDelDesktopGroup
            // 
            this.MenuItemDelDesktopGroup.Name = "MenuItemDelDesktopGroup";
            this.MenuItemDelDesktopGroup.Size = new System.Drawing.Size(152, 22);
            this.MenuItemDelDesktopGroup.Text = "删除";
            this.MenuItemDelDesktopGroup.Click += new System.EventHandler(this.MenuItemDelDesktopGroup_Click);
            // 
            // MenuItemEditViewCatalog
            // 
            this.MenuItemEditViewCatalog.Name = "MenuItemEditViewCatalog";
            this.MenuItemEditViewCatalog.Size = new System.Drawing.Size(98, 22);
            this.MenuItemEditViewCatalog.Text = "修改";
            this.MenuItemEditViewCatalog.Click += new System.EventHandler(this.MenuItemEditViewCatalog_Click);
            // 
            // MenuItemDelViewCatalog
            // 
            this.MenuItemDelViewCatalog.Name = "MenuItemDelViewCatalog";
            this.MenuItemDelViewCatalog.Size = new System.Drawing.Size(98, 22);
            this.MenuItemDelViewCatalog.Text = "删除";
            this.MenuItemDelViewCatalog.Click += new System.EventHandler(this.MenuItemDelViewCatalog_Click);
            // 
            // MenuItemEditDesktopGroup
            // 
            this.MenuItemEditDesktopGroup.Name = "MenuItemEditDesktopGroup";
            this.MenuItemEditDesktopGroup.Size = new System.Drawing.Size(152, 22);
            this.MenuItemEditDesktopGroup.Text = "修改";
            this.MenuItemEditDesktopGroup.Click += new System.EventHandler(this.MenuItemEditDesktopGroup_Click);
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 566);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "AdminForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "系统工厂";
            this.Load += new System.EventHandler(this.AdminForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuSystemDiagram.ResumeLayout(false);
            this.contextMenuView.ResumeLayout(false);
            this.contextMenuDesktopGroup.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeLeft;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ContextMenuStrip contextMenuSystemDiagram;
        private System.Windows.Forms.ToolStripMenuItem MenuItemSetSystemDiagram;
        private System.Windows.Forms.ContextMenuStrip contextMenuView;
        private System.Windows.Forms.ToolStripMenuItem MenuItemNewViewCatalog;
        private System.Windows.Forms.ContextMenuStrip contextMenuDesktopGroup;
        private System.Windows.Forms.ToolStripMenuItem MenuItemNewDesktopGroup;
        private System.Windows.Forms.ToolStripMenuItem MenuItemDelDesktopGroup;
        private System.Windows.Forms.ToolStripMenuItem MenuItemEditViewCatalog;
        private System.Windows.Forms.ToolStripMenuItem MenuItemDelViewCatalog;
        private System.Windows.Forms.ToolStripMenuItem MenuItemEditDesktopGroup;
    }
}

