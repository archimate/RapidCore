namespace ErpCore.Menu
{
    partial class MenuPanel
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
            this.tbtEdit = new System.Windows.Forms.ToolStripButton();
            this.tbtNew = new System.Windows.Forms.ToolStripButton();
            this.tbtDel = new System.Windows.Forms.ToolStripButton();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.treeView = new System.Windows.Forms.TreeView();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbtEdit
            // 
            this.tbtEdit.Image = global::ErpCore.Properties.Resources.edit;
            this.tbtEdit.ImageTransparentColor = System.Drawing.Color.Silver;
            this.tbtEdit.Name = "tbtEdit";
            this.tbtEdit.Size = new System.Drawing.Size(36, 37);
            this.tbtEdit.Text = "修改";
            this.tbtEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbtEdit.Click += new System.EventHandler(this.tbtEdit_Click);
            // 
            // tbtNew
            // 
            this.tbtNew.Image = global::ErpCore.Properties.Resources.NEW;
            this.tbtNew.ImageTransparentColor = System.Drawing.Color.Silver;
            this.tbtNew.Name = "tbtNew";
            this.tbtNew.Size = new System.Drawing.Size(36, 37);
            this.tbtNew.Text = "新建";
            this.tbtNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbtNew.Click += new System.EventHandler(this.tbtNew_Click);
            // 
            // tbtDel
            // 
            this.tbtDel.Image = global::ErpCore.Properties.Resources.DELETE;
            this.tbtDel.ImageTransparentColor = System.Drawing.Color.Silver;
            this.tbtDel.Name = "tbtDel";
            this.tbtDel.Size = new System.Drawing.Size(36, 37);
            this.tbtDel.Text = "删除";
            this.tbtDel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbtDel.Click += new System.EventHandler(this.tbtDel_Click);
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbtNew,
            this.tbtEdit,
            this.tbtDel});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(293, 40);
            this.toolStrip.TabIndex = 8;
            this.toolStrip.Text = "toolStrip1";
            // 
            // treeView
            // 
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.Location = new System.Drawing.Point(0, 40);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(293, 246);
            this.treeView.TabIndex = 9;
            // 
            // MenuPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeView);
            this.Controls.Add(this.toolStrip);
            this.Name = "MenuPanel";
            this.Size = new System.Drawing.Size(293, 286);
            this.Load += new System.EventHandler(this.MenuPanel_Load);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripButton tbtEdit;
        private System.Windows.Forms.ToolStripButton tbtNew;
        private System.Windows.Forms.ToolStripButton tbtDel;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.TreeView treeView;



    }
}
