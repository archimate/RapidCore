namespace ErpCore.CommonCtrl
{
    partial class TableGridF
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
            this.tbtNew = new System.Windows.Forms.ToolStripButton();
            this.tbtEdit = new System.Windows.Forms.ToolStripButton();
            this.tbtDel = new System.Windows.Forms.ToolStripButton();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.tbTitle = new System.Windows.Forms.ToolStrip();
            this.lbTitle = new System.Windows.Forms.ToolStripLabel();
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
            this.tbtDel});
            this.toolStrip.Location = new System.Drawing.Point(0, 25);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(264, 35);
            this.toolStrip.TabIndex = 6;
            this.toolStrip.Text = "toolStrip1";
            // 
            // tbtNew
            // 
            this.tbtNew.Image = global::ErpCore.Properties.Resources.NEW;
            this.tbtNew.ImageTransparentColor = System.Drawing.Color.Silver;
            this.tbtNew.Name = "tbtNew";
            this.tbtNew.Size = new System.Drawing.Size(33, 32);
            this.tbtNew.Text = "新建";
            this.tbtNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbtNew.Click += new System.EventHandler(this.tbtNew_Click);
            // 
            // tbtEdit
            // 
            this.tbtEdit.Image = global::ErpCore.Properties.Resources.edit;
            this.tbtEdit.ImageTransparentColor = System.Drawing.Color.Silver;
            this.tbtEdit.Name = "tbtEdit";
            this.tbtEdit.Size = new System.Drawing.Size(33, 32);
            this.tbtEdit.Text = "修改";
            this.tbtEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbtEdit.Click += new System.EventHandler(this.tbtEdit_Click);
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
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(0, 60);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowTemplate.Height = 23;
            this.dataGridView.Size = new System.Drawing.Size(264, 167);
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
            this.lbTitle.Size = new System.Drawing.Size(29, 22);
            this.lbTitle.Text = "标题";
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
    }
}
