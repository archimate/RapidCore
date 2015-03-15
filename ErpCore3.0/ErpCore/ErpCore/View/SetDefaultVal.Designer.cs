namespace ErpCore.View
{
    partial class SetDefaultVal
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
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colReadOnly = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btOk = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btVariable = new System.Windows.Forms.Button();
            this.cbTable = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(46, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 43;
            this.label4.Text = "表：";
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colName,
            this.colLen,
            this.colReadOnly});
            this.dataGridView.Location = new System.Drawing.Point(12, 46);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 23;
            this.dataGridView.Size = new System.Drawing.Size(519, 300);
            this.dataGridView.TabIndex = 45;
            // 
            // colName
            // 
            this.colName.HeaderText = "字段";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            // 
            // colLen
            // 
            this.colLen.HeaderText = "默认值表达式";
            this.colLen.Name = "colLen";
            this.colLen.Width = 240;
            // 
            // colReadOnly
            // 
            this.colReadOnly.HeaderText = "只读";
            this.colReadOnly.Name = "colReadOnly";
            // 
            // btOk
            // 
            this.btOk.Location = new System.Drawing.Point(331, 400);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(75, 23);
            this.btOk.TabIndex = 55;
            this.btOk.Text = "确定";
            this.btOk.UseVisualStyleBackColor = true;
            this.btOk.Click += new System.EventHandler(this.btOk_Click);
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(429, 400);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 56;
            this.btCancel.Text = "取消";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(22, 359);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(257, 52);
            this.label1.TabIndex = 57;
            this.label1.Text = "注：表达式支持[自定义变量]、常量、select SQL语句，例如[当前用户名]、123、sql:select getdate()";
            // 
            // btVariable
            // 
            this.btVariable.Location = new System.Drawing.Point(303, 353);
            this.btVariable.Name = "btVariable";
            this.btVariable.Size = new System.Drawing.Size(117, 23);
            this.btVariable.TabIndex = 58;
            this.btVariable.Text = "查看自定义变量";
            this.btVariable.UseVisualStyleBackColor = true;
            this.btVariable.Click += new System.EventHandler(this.btVariable_Click);
            // 
            // cbTable
            // 
            this.cbTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTable.FormattingEnabled = true;
            this.cbTable.Location = new System.Drawing.Point(81, 19);
            this.cbTable.Name = "cbTable";
            this.cbTable.Size = new System.Drawing.Size(210, 20);
            this.cbTable.TabIndex = 59;
            this.cbTable.SelectedIndexChanged += new System.EventHandler(this.cbTable_SelectedIndexChanged);
            // 
            // SetDefaultVal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 435);
            this.Controls.Add(this.cbTable);
            this.Controls.Add(this.btVariable);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btOk);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.label4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetDefaultVal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "设置默认值";
            this.Load += new System.EventHandler(this.SetDefaultVal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button btOk;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btVariable;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLen;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colReadOnly;
        private System.Windows.Forms.ComboBox cbTable;
    }
}