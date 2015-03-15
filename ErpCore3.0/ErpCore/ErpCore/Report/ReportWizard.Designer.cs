namespace ErpCore.Report
{
    partial class ReportWizard
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.cbCatalog = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridStatItem = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Order = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btSet = new System.Windows.Forms.Button();
            this.btDown = new System.Windows.Forms.Button();
            this.btUp = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btCancel = new System.Windows.Forms.Button();
            this.btNext = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btFinish = new System.Windows.Forms.Button();
            this.btPrev2 = new System.Windows.Forms.Button();
            this.btCancel2 = new System.Windows.Forms.Button();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.btAdd = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.rdOr = new System.Windows.Forms.RadioButton();
            this.rdAnd = new System.Windows.Forms.RadioButton();
            this.txtVal = new System.Windows.Forms.TextBox();
            this.cbSign = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbColumn = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbTable = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridStatItem)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(771, 504);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.cbCatalog);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.dataGridStatItem);
            this.tabPage1.Controls.Add(this.txtName);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.btSet);
            this.tabPage1.Controls.Add(this.btDown);
            this.tabPage1.Controls.Add(this.btUp);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.btCancel);
            this.tabPage1.Controls.Add(this.btNext);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(763, 478);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "报表定义";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // cbCatalog
            // 
            this.cbCatalog.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCatalog.FormattingEnabled = true;
            this.cbCatalog.Location = new System.Drawing.Point(328, 30);
            this.cbCatalog.Name = "cbCatalog";
            this.cbCatalog.Size = new System.Drawing.Size(169, 20);
            this.cbCatalog.TabIndex = 31;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(280, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 30;
            this.label2.Text = "目录：";
            // 
            // dataGridStatItem
            // 
            this.dataGridStatItem.AllowUserToAddRows = false;
            this.dataGridStatItem.AllowUserToDeleteRows = false;
            this.dataGridStatItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridStatItem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Order});
            this.dataGridStatItem.Location = new System.Drawing.Point(19, 101);
            this.dataGridStatItem.Name = "dataGridStatItem";
            this.dataGridStatItem.RowTemplate.Height = 23;
            this.dataGridStatItem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridStatItem.Size = new System.Drawing.Size(655, 310);
            this.dataGridStatItem.TabIndex = 23;
            this.dataGridStatItem.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridStatItem_EditingControlShowing);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "表";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "字段";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "统计类型";
            this.Column3.Items.AddRange(new object[] {
            "取数",
            "求和",
            "求平均",
            "求最大值",
            "求最小值",
            "计数"});
            this.Column3.Name = "Column3";
            // 
            // Order
            // 
            this.Order.HeaderText = "排序";
            this.Order.Items.AddRange(new object[] {
            "默认",
            "升序",
            "降序"});
            this.Order.Name = "Order";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(79, 30);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(175, 21);
            this.txtName.TabIndex = 22;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(26, 33);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 12);
            this.label11.TabIndex = 21;
            this.label11.Text = "名称：";
            // 
            // btSet
            // 
            this.btSet.Location = new System.Drawing.Point(680, 336);
            this.btSet.Name = "btSet";
            this.btSet.Size = new System.Drawing.Size(65, 23);
            this.btSet.TabIndex = 12;
            this.btSet.Text = "设置";
            this.btSet.UseVisualStyleBackColor = true;
            this.btSet.Click += new System.EventHandler(this.btSet_Click);
            // 
            // btDown
            // 
            this.btDown.Location = new System.Drawing.Point(680, 275);
            this.btDown.Name = "btDown";
            this.btDown.Size = new System.Drawing.Size(65, 23);
            this.btDown.TabIndex = 11;
            this.btDown.Text = "向下";
            this.btDown.UseVisualStyleBackColor = true;
            this.btDown.Click += new System.EventHandler(this.btDown_Click);
            // 
            // btUp
            // 
            this.btUp.Location = new System.Drawing.Point(680, 237);
            this.btUp.Name = "btUp";
            this.btUp.Size = new System.Drawing.Size(65, 23);
            this.btUp.TabIndex = 10;
            this.btUp.Text = "向上";
            this.btUp.UseVisualStyleBackColor = true;
            this.btUp.Click += new System.EventHandler(this.btUp_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "表体统计指标：";
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(631, 433);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 7;
            this.btCancel.Text = "取消";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // btNext
            // 
            this.btNext.Location = new System.Drawing.Point(535, 433);
            this.btNext.Name = "btNext";
            this.btNext.Size = new System.Drawing.Size(77, 23);
            this.btNext.TabIndex = 6;
            this.btNext.Text = "下一步 >>";
            this.btNext.UseVisualStyleBackColor = true;
            this.btNext.Click += new System.EventHandler(this.btNext_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btFinish);
            this.tabPage2.Controls.Add(this.btPrev2);
            this.tabPage2.Controls.Add(this.btCancel2);
            this.tabPage2.Controls.Add(this.txtFilter);
            this.tabPage2.Controls.Add(this.btAdd);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.rdOr);
            this.tabPage2.Controls.Add(this.rdAnd);
            this.tabPage2.Controls.Add(this.txtVal);
            this.tabPage2.Controls.Add(this.cbSign);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.cbColumn);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.cbTable);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(763, 478);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "过滤条件";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btFinish
            // 
            this.btFinish.Location = new System.Drawing.Point(538, 433);
            this.btFinish.Name = "btFinish";
            this.btFinish.Size = new System.Drawing.Size(77, 23);
            this.btFinish.TabIndex = 16;
            this.btFinish.Text = "完成";
            this.btFinish.UseVisualStyleBackColor = true;
            this.btFinish.Click += new System.EventHandler(this.btFinish_Click);
            // 
            // btPrev2
            // 
            this.btPrev2.Location = new System.Drawing.Point(439, 433);
            this.btPrev2.Name = "btPrev2";
            this.btPrev2.Size = new System.Drawing.Size(77, 23);
            this.btPrev2.TabIndex = 14;
            this.btPrev2.Text = "<< 上一步";
            this.btPrev2.UseVisualStyleBackColor = true;
            this.btPrev2.Click += new System.EventHandler(this.btPrev2_Click);
            // 
            // btCancel2
            // 
            this.btCancel2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel2.Location = new System.Drawing.Point(631, 433);
            this.btCancel2.Name = "btCancel2";
            this.btCancel2.Size = new System.Drawing.Size(75, 23);
            this.btCancel2.TabIndex = 13;
            this.btCancel2.Text = "取消";
            this.btCancel2.UseVisualStyleBackColor = true;
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(139, 179);
            this.txtFilter.Multiline = true;
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtFilter.Size = new System.Drawing.Size(456, 211);
            this.txtFilter.TabIndex = 11;
            // 
            // btAdd
            // 
            this.btAdd.Location = new System.Drawing.Point(520, 131);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(75, 23);
            this.btAdd.TabIndex = 10;
            this.btAdd.Text = "添加";
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(137, 48);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 9;
            this.label9.Text = "组合关系：";
            // 
            // rdOr
            // 
            this.rdOr.AutoSize = true;
            this.rdOr.Location = new System.Drawing.Point(256, 46);
            this.rdOr.Name = "rdOr";
            this.rdOr.Size = new System.Drawing.Size(35, 16);
            this.rdOr.TabIndex = 8;
            this.rdOr.Text = "或";
            this.rdOr.UseVisualStyleBackColor = true;
            // 
            // rdAnd
            // 
            this.rdAnd.AutoSize = true;
            this.rdAnd.Checked = true;
            this.rdAnd.Location = new System.Drawing.Point(208, 46);
            this.rdAnd.Name = "rdAnd";
            this.rdAnd.Size = new System.Drawing.Size(35, 16);
            this.rdAnd.TabIndex = 7;
            this.rdAnd.TabStop = true;
            this.rdAnd.Text = "与";
            this.rdAnd.UseVisualStyleBackColor = true;
            // 
            // txtVal
            // 
            this.txtVal.Location = new System.Drawing.Point(298, 131);
            this.txtVal.Name = "txtVal";
            this.txtVal.Size = new System.Drawing.Size(205, 21);
            this.txtVal.TabIndex = 6;
            // 
            // cbSign
            // 
            this.cbSign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSign.FormattingEnabled = true;
            this.cbSign.Location = new System.Drawing.Point(208, 131);
            this.cbSign.Name = "cbSign";
            this.cbSign.Size = new System.Drawing.Size(83, 20);
            this.cbSign.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(163, 134);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 4;
            this.label8.Text = "条件：";
            // 
            // cbColumn
            // 
            this.cbColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbColumn.FormattingEnabled = true;
            this.cbColumn.Location = new System.Drawing.Point(208, 100);
            this.cbColumn.Name = "cbColumn";
            this.cbColumn.Size = new System.Drawing.Size(295, 20);
            this.cbColumn.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(161, 103);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 2;
            this.label7.Text = "字段：";
            // 
            // cbTable
            // 
            this.cbTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTable.FormattingEnabled = true;
            this.cbTable.Location = new System.Drawing.Point(208, 70);
            this.cbTable.Name = "cbTable";
            this.cbTable.Size = new System.Drawing.Size(295, 20);
            this.cbTable.TabIndex = 1;
            this.cbTable.SelectedIndexChanged += new System.EventHandler(this.cbTable_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(173, 73);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "表：";
            // 
            // ReportWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(771, 504);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReportWizard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "报表向导";
            this.Load += new System.EventHandler(this.ReportWizard_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridStatItem)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btNext;
        private System.Windows.Forms.Button btSet;
        private System.Windows.Forms.Button btDown;
        private System.Windows.Forms.Button btUp;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbSign;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbColumn;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbTable;
        private System.Windows.Forms.Button btCancel2;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RadioButton rdOr;
        private System.Windows.Forms.RadioButton rdAnd;
        private System.Windows.Forms.TextBox txtVal;
        private System.Windows.Forms.Button btPrev2;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridView dataGridStatItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column3;
        private System.Windows.Forms.DataGridViewComboBoxColumn Order;
        private System.Windows.Forms.Button btFinish;
        private System.Windows.Forms.ComboBox cbCatalog;
        private System.Windows.Forms.Label label2;
    }
}