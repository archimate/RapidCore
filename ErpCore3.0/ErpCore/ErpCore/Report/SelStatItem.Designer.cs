namespace ErpCore.Report
{
    partial class SelStatItem
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
            this.cbTable = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.listColumn = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.label1 = new System.Windows.Forms.Label();
            this.listSelColumn = new System.Windows.Forms.ListView();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.btAdd = new System.Windows.Forms.Button();
            this.btDel = new System.Windows.Forms.Button();
            this.btOk = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFormula = new System.Windows.Forms.TextBox();
            this.btAddFormula = new System.Windows.Forms.Button();
            this.txtAsName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbTable
            // 
            this.cbTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTable.FormattingEnabled = true;
            this.cbTable.Location = new System.Drawing.Point(61, 24);
            this.cbTable.Name = "cbTable";
            this.cbTable.Size = new System.Drawing.Size(217, 20);
            this.cbTable.TabIndex = 3;
            this.cbTable.SelectedIndexChanged += new System.EventHandler(this.cbTable_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 2;
            this.label6.Text = "表：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 60);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 4;
            this.label7.Text = "字段：";
            // 
            // listColumn
            // 
            this.listColumn.AccessibleDescription = " hv ";
            this.listColumn.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listColumn.FullRowSelect = true;
            this.listColumn.HideSelection = false;
            this.listColumn.Location = new System.Drawing.Point(63, 62);
            this.listColumn.Name = "listColumn";
            this.listColumn.Size = new System.Drawing.Size(215, 354);
            this.listColumn.TabIndex = 5;
            this.listColumn.UseCompatibleStateImageBehavior = false;
            this.listColumn.View = System.Windows.Forms.View.Details;
            this.listColumn.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listColumn_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "名称";
            this.columnHeader1.Width = 206;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(332, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "已选择指标：";
            // 
            // listSelColumn
            // 
            this.listSelColumn.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader3});
            this.listSelColumn.FullRowSelect = true;
            this.listSelColumn.HideSelection = false;
            this.listSelColumn.Location = new System.Drawing.Point(334, 62);
            this.listSelColumn.Name = "listSelColumn";
            this.listSelColumn.Size = new System.Drawing.Size(304, 300);
            this.listSelColumn.TabIndex = 7;
            this.listSelColumn.UseCompatibleStateImageBehavior = false;
            this.listSelColumn.View = System.Windows.Forms.View.Details;
            this.listSelColumn.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listSelColumn_MouseDoubleClick);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "表";
            this.columnHeader2.Width = 139;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "字段";
            this.columnHeader3.Width = 155;
            // 
            // btAdd
            // 
            this.btAdd.Location = new System.Drawing.Point(284, 160);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(43, 23);
            this.btAdd.TabIndex = 8;
            this.btAdd.Text = ">";
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // btDel
            // 
            this.btDel.Location = new System.Drawing.Point(284, 229);
            this.btDel.Name = "btDel";
            this.btDel.Size = new System.Drawing.Size(43, 23);
            this.btDel.TabIndex = 9;
            this.btDel.Text = "<";
            this.btDel.UseVisualStyleBackColor = true;
            this.btDel.Click += new System.EventHandler(this.btDel_Click);
            // 
            // btOk
            // 
            this.btOk.Location = new System.Drawing.Point(320, 439);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(75, 23);
            this.btOk.TabIndex = 10;
            this.btOk.Text = "确定";
            this.btOk.UseVisualStyleBackColor = true;
            this.btOk.Click += new System.EventHandler(this.btOk_Click);
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(490, 439);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 11;
            this.btCancel.Text = "取消";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(336, 398);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 12;
            this.label2.Text = "公式：";
            // 
            // txtFormula
            // 
            this.txtFormula.Location = new System.Drawing.Point(383, 395);
            this.txtFormula.Name = "txtFormula";
            this.txtFormula.Size = new System.Drawing.Size(201, 21);
            this.txtFormula.TabIndex = 13;
            // 
            // btAddFormula
            // 
            this.btAddFormula.Location = new System.Drawing.Point(590, 393);
            this.btAddFormula.Name = "btAddFormula";
            this.btAddFormula.Size = new System.Drawing.Size(50, 23);
            this.btAddFormula.TabIndex = 14;
            this.btAddFormula.Text = "添加";
            this.btAddFormula.UseVisualStyleBackColor = true;
            this.btAddFormula.Click += new System.EventHandler(this.btAddFormula_Click);
            // 
            // txtAsName
            // 
            this.txtAsName.Location = new System.Drawing.Point(383, 368);
            this.txtAsName.Name = "txtAsName";
            this.txtAsName.Size = new System.Drawing.Size(201, 21);
            this.txtAsName.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(336, 371);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 15;
            this.label3.Text = "别名：";
            // 
            // SelStatItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 474);
            this.Controls.Add(this.txtAsName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btAddFormula);
            this.Controls.Add(this.txtFormula);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOk);
            this.Controls.Add(this.btDel);
            this.Controls.Add(this.btAdd);
            this.Controls.Add(this.listSelColumn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listColumn);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbTable);
            this.Controls.Add(this.label6);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelStatItem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "选择统计指标";
            this.Load += new System.EventHandler(this.SelStatItem_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbTable;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListView listColumn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.Button btDel;
        private System.Windows.Forms.Button btOk;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        public System.Windows.Forms.ListView listSelColumn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFormula;
        private System.Windows.Forms.Button btAddFormula;
        private System.Windows.Forms.TextBox txtAsName;
        private System.Windows.Forms.Label label3;
    }
}