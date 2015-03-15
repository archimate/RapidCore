namespace ErpCore.CommonCtrl
{
    partial class TableGrid
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
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.tbTitle = new System.Windows.Forms.ToolStrip();
            this.lbTitle = new System.Windows.Forms.ToolStripLabel();
            this.tbtNew = new System.Windows.Forms.ToolStripButton();
            this.tbtEdit = new System.Windows.Forms.ToolStripButton();
            this.tbtDel = new System.Windows.Forms.ToolStripButton();
            this.tbtWorkflow = new System.Windows.Forms.ToolStripSplitButton();
            this.MenuItem_StartWorkflow = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_ViewWorkflow = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.tbTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbtNew,
            this.tbtEdit,
            this.tbtDel,
            this.tbtWorkflow});
            this.toolStrip.Location = new System.Drawing.Point(0, 25);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(264, 40);
            this.toolStrip.TabIndex = 6;
            this.toolStrip.Text = "toolStrip1";
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(0, 65);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowTemplate.Height = 23;
            this.dataGridView.Size = new System.Drawing.Size(264, 162);
            this.dataGridView.TabIndex = 7;
            // 
            // tbTitle
            // 
            this.tbTitle.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbTitle});
            this.tbTitle.Location = new System.Drawing.Point(0, 0);
            this.tbTitle.Name = "tbTitle";
            this.tbTitle.Size = new System.Drawing.Size(264, 25);
            this.tbTitle.TabIndex = 9;
            this.tbTitle.Text = "toolStrip1";
            // 
            // lbTitle
            // 
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(32, 22);
            this.lbTitle.Text = "标题";
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
            // tbtWorkflow
            // 
            this.tbtWorkflow.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItem_StartWorkflow,
            this.MenuItem_ViewWorkflow});
            this.tbtWorkflow.Image = global::ErpCore.Properties.Resources.workflow;
            this.tbtWorkflow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtWorkflow.Name = "tbtWorkflow";
            this.tbtWorkflow.Size = new System.Drawing.Size(60, 37);
            this.tbtWorkflow.Text = "工作流";
            this.tbtWorkflow.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbtWorkflow.ButtonClick += new System.EventHandler(this.tbtWorkflow_ButtonClick);
            // 
            // MenuItem_StartWorkflow
            // 
            this.MenuItem_StartWorkflow.Name = "MenuItem_StartWorkflow";
            this.MenuItem_StartWorkflow.Size = new System.Drawing.Size(152, 22);
            this.MenuItem_StartWorkflow.Text = "启动";
            this.MenuItem_StartWorkflow.Click += new System.EventHandler(this.MenuItem_StartWorkflow_Click);
            // 
            // MenuItem_ViewWorkflow
            // 
            this.MenuItem_ViewWorkflow.Name = "MenuItem_ViewWorkflow";
            this.MenuItem_ViewWorkflow.Size = new System.Drawing.Size(152, 22);
            this.MenuItem_ViewWorkflow.Text = "查看";
            this.MenuItem_ViewWorkflow.Click += new System.EventHandler(this.MenuItem_ViewWorkflow_Click);
            // 
            // TableGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.tbTitle);
            this.Name = "TableGrid";
            this.Size = new System.Drawing.Size(264, 227);
            this.Load += new System.EventHandler(this.TableCtrl_Load);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.tbTitle.ResumeLayout(false);
            this.tbTitle.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripButton tbtEdit;
        private System.Windows.Forms.ToolStripButton tbtNew;
        private System.Windows.Forms.ToolStripButton tbtDel;
        public System.Windows.Forms.ToolStrip toolStrip;
        public System.Windows.Forms.DataGridView dataGridView;
        public System.Windows.Forms.ToolStrip tbTitle;
        private System.Windows.Forms.ToolStripLabel lbTitle;
        private System.Windows.Forms.ToolStripSplitButton tbtWorkflow;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_StartWorkflow;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_ViewWorkflow;
    }
}
