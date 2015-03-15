namespace ErpCore.Window.Designer
{
    partial class LayoutWindowDesigner
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.tbtSave = new System.Windows.Forms.ToolStripButton();
            this.tbtDel = new System.Windows.Forms.ToolStripButton();
            this.tddPanel = new System.Windows.Forms.ToolStripDropDownButton();
            this.MenuItemTopPanel = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemBottomPanel = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemLeftPanel = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemRightPanel = new System.Windows.Forms.ToolStripMenuItem();
            this.tbtEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbtEdit,
            this.tbtSave,
            this.tbtDel,
            this.tddPanel});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(600, 35);
            this.toolStrip.TabIndex = 2;
            this.toolStrip.Text = "toolStrip1";
            // 
            // tbtSave
            // 
            this.tbtSave.Image = global::ErpCore.Properties.Resources.SAVE;
            this.tbtSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbtSave.ImageTransparentColor = System.Drawing.Color.Silver;
            this.tbtSave.Name = "tbtSave";
            this.tbtSave.Size = new System.Drawing.Size(33, 32);
            this.tbtSave.Text = "保存";
            this.tbtSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbtSave.Click += new System.EventHandler(this.tbtSave_Click);
            // 
            // tbtDel
            // 
            this.tbtDel.Image = global::ErpCore.Properties.Resources.DELETE;
            this.tbtDel.ImageTransparentColor = System.Drawing.Color.Silver;
            this.tbtDel.Name = "tbtDel";
            this.tbtDel.Size = new System.Drawing.Size(33, 32);
            this.tbtDel.Text = "删除";
            this.tbtDel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbtDel.Click += new System.EventHandler(this.tbtDel_Click);
            // 
            // tddPanel
            // 
            this.tddPanel.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemTopPanel,
            this.MenuItemBottomPanel,
            this.MenuItemLeftPanel,
            this.MenuItemRightPanel});
            this.tddPanel.Image = global::ErpCore.Properties.Resources.panellayout;
            this.tddPanel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tddPanel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tddPanel.Name = "tddPanel";
            this.tddPanel.Size = new System.Drawing.Size(42, 32);
            this.tddPanel.Text = "面板";
            this.tddPanel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // MenuItemTopPanel
            // 
            this.MenuItemTopPanel.Checked = true;
            this.MenuItemTopPanel.CheckOnClick = true;
            this.MenuItemTopPanel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MenuItemTopPanel.Name = "MenuItemTopPanel";
            this.MenuItemTopPanel.Size = new System.Drawing.Size(118, 22);
            this.MenuItemTopPanel.Text = "顶部面板";
            this.MenuItemTopPanel.Click += new System.EventHandler(this.MenuItemTopPanel_Click);
            // 
            // MenuItemBottomPanel
            // 
            this.MenuItemBottomPanel.Checked = true;
            this.MenuItemBottomPanel.CheckOnClick = true;
            this.MenuItemBottomPanel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MenuItemBottomPanel.Name = "MenuItemBottomPanel";
            this.MenuItemBottomPanel.Size = new System.Drawing.Size(118, 22);
            this.MenuItemBottomPanel.Text = "底部面板";
            this.MenuItemBottomPanel.Click += new System.EventHandler(this.MenuItemBottomPanel_Click);
            // 
            // MenuItemLeftPanel
            // 
            this.MenuItemLeftPanel.Checked = true;
            this.MenuItemLeftPanel.CheckOnClick = true;
            this.MenuItemLeftPanel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MenuItemLeftPanel.Name = "MenuItemLeftPanel";
            this.MenuItemLeftPanel.Size = new System.Drawing.Size(118, 22);
            this.MenuItemLeftPanel.Text = "左边面板";
            this.MenuItemLeftPanel.Click += new System.EventHandler(this.MenuItemLeftPanel_Click);
            // 
            // MenuItemRightPanel
            // 
            this.MenuItemRightPanel.Checked = true;
            this.MenuItemRightPanel.CheckOnClick = true;
            this.MenuItemRightPanel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MenuItemRightPanel.Name = "MenuItemRightPanel";
            this.MenuItemRightPanel.Size = new System.Drawing.Size(118, 22);
            this.MenuItemRightPanel.Text = "右边面板";
            this.MenuItemRightPanel.Click += new System.EventHandler(this.MenuItemRightPanel_Click);
            // 
            // tbtEdit
            // 
            this.tbtEdit.Image = global::ErpCore.Properties.Resources.edit;
            this.tbtEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbtEdit.ImageTransparentColor = System.Drawing.Color.Silver;
            this.tbtEdit.Name = "tbtEdit";
            this.tbtEdit.Size = new System.Drawing.Size(33, 32);
            this.tbtEdit.Text = "编辑";
            this.tbtEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbtEdit.Click += new System.EventHandler(this.tbtEdit_Click);
            // 
            // LayoutWindowDesigner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 469);
            this.Controls.Add(this.toolStrip);
            this.IsMdiContainer = true;
            this.Name = "LayoutWindowDesigner";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "窗体布局设计器";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.LayoutWindowDesigner_Load);
            this.SizeChanged += new System.EventHandler(this.LayoutWindowDesigner_SizeChanged);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton tbtSave;
        private System.Windows.Forms.ToolStripButton tbtDel;
        private System.Windows.Forms.ToolStripDropDownButton tddPanel;
        public System.Windows.Forms.ToolStripMenuItem MenuItemTopPanel;
        public System.Windows.Forms.ToolStripMenuItem MenuItemBottomPanel;
        public System.Windows.Forms.ToolStripMenuItem MenuItemLeftPanel;
        public System.Windows.Forms.ToolStripMenuItem MenuItemRightPanel;
        private System.Windows.Forms.ToolStripButton tbtEdit;
    }
}