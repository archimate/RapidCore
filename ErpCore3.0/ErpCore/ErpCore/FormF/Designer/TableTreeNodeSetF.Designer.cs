namespace ErpCore.FormF.Designer
{
    partial class TableTreeNodeSetF
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
            this.lbCaption = new System.Windows.Forms.ToolStripLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTable = new System.Windows.Forms.TextBox();
            this.btSelTable = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ckIsLoop = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbNodeIDCol = new System.Windows.Forms.ComboBox();
            this.cbPNodeIDCol = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtRootFilter = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btSelColumn = new System.Windows.Forms.Button();
            this.txtQueryFilter = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbCaption});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(334, 25);
            this.toolStrip.TabIndex = 0;
            this.toolStrip.Text = "toolStrip1";
            // 
            // lbCaption
            // 
            this.lbCaption.Name = "lbCaption";
            this.lbCaption.Size = new System.Drawing.Size(35, 22);
            this.lbCaption.Text = "第1级";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "数据表：";
            // 
            // txtTable
            // 
            this.txtTable.Location = new System.Drawing.Point(109, 40);
            this.txtTable.Name = "txtTable";
            this.txtTable.ReadOnly = true;
            this.txtTable.Size = new System.Drawing.Size(151, 21);
            this.txtTable.TabIndex = 2;
            // 
            // btSelTable
            // 
            this.btSelTable.Location = new System.Drawing.Point(266, 39);
            this.btSelTable.Name = "btSelTable";
            this.btSelTable.Size = new System.Drawing.Size(44, 23);
            this.btSelTable.TabIndex = 3;
            this.btSelTable.Text = "...";
            this.btSelTable.UseVisualStyleBackColor = true;
            this.btSelTable.Click += new System.EventHandler(this.btSelTable_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "显示文本：";
            // 
            // txtText
            // 
            this.txtText.Location = new System.Drawing.Point(109, 95);
            this.txtText.Name = "txtText";
            this.txtText.Size = new System.Drawing.Size(151, 21);
            this.txtText.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "递归循环：";
            // 
            // ckIsLoop
            // 
            this.ckIsLoop.AutoSize = true;
            this.ckIsLoop.Checked = true;
            this.ckIsLoop.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckIsLoop.Location = new System.Drawing.Point(109, 119);
            this.ckIsLoop.Name = "ckIsLoop";
            this.ckIsLoop.Size = new System.Drawing.Size(15, 14);
            this.ckIsLoop.TabIndex = 7;
            this.ckIsLoop.UseVisualStyleBackColor = true;
            this.ckIsLoop.CheckedChanged += new System.EventHandler(this.ckIsLoop_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 141);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "本节点字段：";
            // 
            // cbNodeIDCol
            // 
            this.cbNodeIDCol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNodeIDCol.FormattingEnabled = true;
            this.cbNodeIDCol.Location = new System.Drawing.Point(109, 136);
            this.cbNodeIDCol.Name = "cbNodeIDCol";
            this.cbNodeIDCol.Size = new System.Drawing.Size(151, 20);
            this.cbNodeIDCol.TabIndex = 9;
            // 
            // cbPNodeIDCol
            // 
            this.cbPNodeIDCol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPNodeIDCol.FormattingEnabled = true;
            this.cbPNodeIDCol.Location = new System.Drawing.Point(110, 162);
            this.cbPNodeIDCol.Name = "cbPNodeIDCol";
            this.cbPNodeIDCol.Size = new System.Drawing.Size(151, 20);
            this.cbPNodeIDCol.TabIndex = 11;
            this.cbPNodeIDCol.SelectedIndexChanged += new System.EventHandler(this.cbPNodeIDCol_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 167);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "父节点字段：";
            // 
            // txtRootFilter
            // 
            this.txtRootFilter.AcceptsReturn = true;
            this.txtRootFilter.Location = new System.Drawing.Point(109, 188);
            this.txtRootFilter.Name = "txtRootFilter";
            this.txtRootFilter.Size = new System.Drawing.Size(151, 21);
            this.txtRootFilter.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(28, 193);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "根节点条件：";
            // 
            // btSelColumn
            // 
            this.btSelColumn.Location = new System.Drawing.Point(266, 94);
            this.btSelColumn.Name = "btSelColumn";
            this.btSelColumn.Size = new System.Drawing.Size(44, 23);
            this.btSelColumn.TabIndex = 14;
            this.btSelColumn.Text = "...";
            this.btSelColumn.UseVisualStyleBackColor = true;
            this.btSelColumn.Click += new System.EventHandler(this.btSelColumn_Click);
            // 
            // txtQueryFilter
            // 
            this.txtQueryFilter.AcceptsReturn = true;
            this.txtQueryFilter.Location = new System.Drawing.Point(109, 68);
            this.txtQueryFilter.Name = "txtQueryFilter";
            this.txtQueryFilter.Size = new System.Drawing.Size(151, 21);
            this.txtQueryFilter.TabIndex = 16;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(37, 71);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 15;
            this.label7.Text = "过滤条件：";
            // 
            // TableTreeNodeSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.txtQueryFilter);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btSelColumn);
            this.Controls.Add(this.txtRootFilter);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbPNodeIDCol);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbNodeIDCol);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ckIsLoop);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btSelTable);
            this.Controls.Add(this.txtTable);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.toolStrip);
            this.Name = "TableTreeNodeSet";
            this.Size = new System.Drawing.Size(334, 226);
            this.Load += new System.EventHandler(this.TableTreeNodeSet_Load);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripLabel lbCaption;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTable;
        private System.Windows.Forms.Button btSelTable;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btSelColumn;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.ToolStrip toolStrip;
        public System.Windows.Forms.TextBox txtText;
        public System.Windows.Forms.CheckBox ckIsLoop;
        public System.Windows.Forms.ComboBox cbNodeIDCol;
        public System.Windows.Forms.ComboBox cbPNodeIDCol;
        public System.Windows.Forms.TextBox txtRootFilter;
        public System.Windows.Forms.TextBox txtQueryFilter;
    }
}
