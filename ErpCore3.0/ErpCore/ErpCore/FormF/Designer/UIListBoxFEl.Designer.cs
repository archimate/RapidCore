namespace ErpCore.FormF.Designer
{
    partial class UIListBoxFEl
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
            this.lbTitle = new System.Windows.Forms.ToolStripLabel();
            this.tbTitle = new System.Windows.Forms.ToolStrip();
            this.MenuItemBringToFront = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemSendToBack = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.listBox = new System.Windows.Forms.ListBox();
            this.tbTitle.SuspendLayout();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbTitle
            // 
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(29, 22);
            this.lbTitle.Text = "标题";
            // 
            // tbTitle
            // 
            this.tbTitle.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbTitle});
            this.tbTitle.Location = new System.Drawing.Point(0, 0);
            this.tbTitle.Name = "tbTitle";
            this.tbTitle.Size = new System.Drawing.Size(299, 25);
            this.tbTitle.TabIndex = 11;
            this.tbTitle.Text = "toolStrip1";
            // 
            // MenuItemBringToFront
            // 
            this.MenuItemBringToFront.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.MenuItemBringToFront.Name = "MenuItemBringToFront";
            this.MenuItemBringToFront.Size = new System.Drawing.Size(118, 22);
            this.MenuItemBringToFront.Text = "置于顶层";
            // 
            // MenuItemSendToBack
            // 
            this.MenuItemSendToBack.Name = "MenuItemSendToBack";
            this.MenuItemSendToBack.Size = new System.Drawing.Size(118, 22);
            this.MenuItemSendToBack.Text = "置于底层";
            // 
            // MenuItemDelete
            // 
            this.MenuItemDelete.Name = "MenuItemDelete";
            this.MenuItemDelete.Size = new System.Drawing.Size(118, 22);
            this.MenuItemDelete.Text = "删除";
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemBringToFront,
            this.MenuItemSendToBack,
            this.MenuItemDelete});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(119, 70);
            // 
            // listBox
            // 
            this.listBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox.FormattingEnabled = true;
            this.listBox.ItemHeight = 12;
            this.listBox.Location = new System.Drawing.Point(0, 25);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(299, 256);
            this.listBox.TabIndex = 12;
            // 
            // UIListBoxEl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listBox);
            this.Controls.Add(this.tbTitle);
            this.Name = "UIListBoxEl";
            this.Size = new System.Drawing.Size(299, 286);
            this.tbTitle.ResumeLayout(false);
            this.tbTitle.PerformLayout();
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripLabel lbTitle;
        public System.Windows.Forms.ToolStrip tbTitle;
        private System.Windows.Forms.ToolStripMenuItem MenuItemBringToFront;
        private System.Windows.Forms.ToolStripMenuItem MenuItemSendToBack;
        private System.Windows.Forms.ToolStripMenuItem MenuItemDelete;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        public System.Windows.Forms.ListBox listBox;
    }
}
