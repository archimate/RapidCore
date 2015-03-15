namespace ErpCore.Window.Designer
{
    partial class NavigateBarEl
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuItemEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItemBringToFront = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemSendToBack = new System.Windows.Forms.ToolStripMenuItem();
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemEdit,
            this.MenuItemDelete,
            this.toolStripMenuItem1,
            this.MenuItemBringToFront,
            this.MenuItemSendToBack});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(119, 98);
            // 
            // MenuItemEdit
            // 
            this.MenuItemEdit.Name = "MenuItemEdit";
            this.MenuItemEdit.Size = new System.Drawing.Size(118, 22);
            this.MenuItemEdit.Text = "编辑按钮";
            this.MenuItemEdit.Click += new System.EventHandler(this.MenuItemEdit_Click);
            // 
            // MenuItemDelete
            // 
            this.MenuItemDelete.Name = "MenuItemDelete";
            this.MenuItemDelete.Size = new System.Drawing.Size(118, 22);
            this.MenuItemDelete.Text = "删除";
            this.MenuItemDelete.Click += new System.EventHandler(this.MenuItemDelete_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(115, 6);
            // 
            // MenuItemBringToFront
            // 
            this.MenuItemBringToFront.Name = "MenuItemBringToFront";
            this.MenuItemBringToFront.Size = new System.Drawing.Size(118, 22);
            this.MenuItemBringToFront.Text = "置于顶层";
            this.MenuItemBringToFront.Click += new System.EventHandler(this.MenuItemBringToFront_Click);
            // 
            // MenuItemSendToBack
            // 
            this.MenuItemSendToBack.Name = "MenuItemSendToBack";
            this.MenuItemSendToBack.Size = new System.Drawing.Size(118, 22);
            this.MenuItemSendToBack.Text = "置于底层";
            this.MenuItemSendToBack.Click += new System.EventHandler(this.MenuItemSendToBack_Click);
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.BackgroundImage = global::ErpCore.Properties.Resources.Toolbar_bk;
            this.flowLayoutPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.flowLayoutPanel.ContextMenuStrip = this.contextMenuStrip1;
            this.flowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(556, 94);
            this.flowLayoutPanel.TabIndex = 0;
            // 
            // NavigateBarEl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.flowLayoutPanel);
            this.Name = "NavigateBarEl";
            this.Size = new System.Drawing.Size(556, 94);
            this.Load += new System.EventHandler(this.NavigateBarEl_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MenuItemEdit;
        private System.Windows.Forms.ToolStripMenuItem MenuItemDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem MenuItemBringToFront;
        private System.Windows.Forms.ToolStripMenuItem MenuItemSendToBack;
        public System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
    }
}
