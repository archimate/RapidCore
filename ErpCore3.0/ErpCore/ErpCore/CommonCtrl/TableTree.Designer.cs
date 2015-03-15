namespace ErpCore.CommonCtrl
{
    partial class TableTree
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
            this.tbTitle = new System.Windows.Forms.ToolStrip();
            this.lbTitle = new System.Windows.Forms.ToolStripLabel();
            this.treeView = new System.Windows.Forms.TreeView();
            this.tbTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbTitle
            // 
            this.tbTitle.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbTitle});
            this.tbTitle.Location = new System.Drawing.Point(0, 0);
            this.tbTitle.Name = "tbTitle";
            this.tbTitle.Size = new System.Drawing.Size(264, 25);
            this.tbTitle.TabIndex = 8;
            this.tbTitle.Text = "toolStrip1";
            // 
            // lbTitle
            // 
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(29, 22);
            this.lbTitle.Text = "标题";
            // 
            // treeView
            // 
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.Location = new System.Drawing.Point(0, 25);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(264, 202);
            this.treeView.TabIndex = 9;
            // 
            // TableTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeView);
            this.Controls.Add(this.tbTitle);
            this.Name = "TableTree";
            this.Size = new System.Drawing.Size(264, 227);
            this.Load += new System.EventHandler(this.TableCtrlEl_Load);
            this.tbTitle.ResumeLayout(false);
            this.tbTitle.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripLabel lbTitle;
        public System.Windows.Forms.ToolStrip tbTitle;
        public System.Windows.Forms.TreeView treeView;

    }
}
