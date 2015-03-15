namespace ErpCore.View
{
    partial class SetViewFilter
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
            this.cbAndOr = new System.Windows.Forms.ComboBox();
            this.btAdd4 = new System.Windows.Forms.Button();
            this.txtVal4 = new System.Windows.Forms.TextBox();
            this.cbSign4 = new System.Windows.Forms.ComboBox();
            this.cbMasterColumn4 = new System.Windows.Forms.ComboBox();
            this.btDel4 = new System.Windows.Forms.Button();
            this.txtMasterTable4 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridView4 = new System.Windows.Forms.DataGridView();
            this.AndOr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sign = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Val = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btOk = new System.Windows.Forms.Button();
            this.btCancel4 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).BeginInit();
            this.SuspendLayout();
            // 
            // cbAndOr
            // 
            this.cbAndOr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAndOr.FormattingEnabled = true;
            this.cbAndOr.Items.AddRange(new object[] {
            "与",
            "或"});
            this.cbAndOr.Location = new System.Drawing.Point(31, 349);
            this.cbAndOr.Name = "cbAndOr";
            this.cbAndOr.Size = new System.Drawing.Size(59, 20);
            this.cbAndOr.TabIndex = 61;
            // 
            // btAdd4
            // 
            this.btAdd4.Location = new System.Drawing.Point(397, 347);
            this.btAdd4.Name = "btAdd4";
            this.btAdd4.Size = new System.Drawing.Size(59, 23);
            this.btAdd4.TabIndex = 60;
            this.btAdd4.Text = "添加";
            this.btAdd4.UseVisualStyleBackColor = true;
            this.btAdd4.Click += new System.EventHandler(this.btAdd4_Click);
            // 
            // txtVal4
            // 
            this.txtVal4.AcceptsReturn = true;
            this.txtVal4.Location = new System.Drawing.Point(272, 349);
            this.txtVal4.Name = "txtVal4";
            this.txtVal4.Size = new System.Drawing.Size(119, 21);
            this.txtVal4.TabIndex = 59;
            // 
            // cbSign4
            // 
            this.cbSign4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSign4.FormattingEnabled = true;
            this.cbSign4.Items.AddRange(new object[] {
            "=",
            ">",
            "<",
            ">=",
            "<=",
            "!=",
            "like"});
            this.cbSign4.Location = new System.Drawing.Point(207, 349);
            this.cbSign4.Name = "cbSign4";
            this.cbSign4.Size = new System.Drawing.Size(59, 20);
            this.cbSign4.TabIndex = 58;
            // 
            // cbMasterColumn4
            // 
            this.cbMasterColumn4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMasterColumn4.FormattingEnabled = true;
            this.cbMasterColumn4.Location = new System.Drawing.Point(95, 349);
            this.cbMasterColumn4.Name = "cbMasterColumn4";
            this.cbMasterColumn4.Size = new System.Drawing.Size(106, 20);
            this.cbMasterColumn4.TabIndex = 57;
            // 
            // btDel4
            // 
            this.btDel4.Location = new System.Drawing.Point(397, 310);
            this.btDel4.Name = "btDel4";
            this.btDel4.Size = new System.Drawing.Size(59, 23);
            this.btDel4.TabIndex = 56;
            this.btDel4.Text = "删除";
            this.btDel4.UseVisualStyleBackColor = true;
            this.btDel4.Click += new System.EventHandler(this.btDel4_Click);
            // 
            // txtMasterTable4
            // 
            this.txtMasterTable4.AcceptsReturn = true;
            this.txtMasterTable4.Location = new System.Drawing.Point(79, 9);
            this.txtMasterTable4.Name = "txtMasterTable4";
            this.txtMasterTable4.ReadOnly = true;
            this.txtMasterTable4.Size = new System.Drawing.Size(177, 21);
            this.txtMasterTable4.TabIndex = 55;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 54;
            this.label4.Text = "主表：";
            // 
            // dataGridView4
            // 
            this.dataGridView4.AllowUserToAddRows = false;
            this.dataGridView4.AllowUserToResizeRows = false;
            this.dataGridView4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView4.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AndOr,
            this.Col,
            this.Sign,
            this.Val});
            this.dataGridView4.Location = new System.Drawing.Point(31, 41);
            this.dataGridView4.Name = "dataGridView4";
            this.dataGridView4.RowHeadersVisible = false;
            this.dataGridView4.RowTemplate.Height = 23;
            this.dataGridView4.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView4.Size = new System.Drawing.Size(360, 292);
            this.dataGridView4.TabIndex = 53;
            // 
            // AndOr
            // 
            this.AndOr.HeaderText = "与或";
            this.AndOr.Name = "AndOr";
            this.AndOr.Width = 40;
            // 
            // Col
            // 
            this.Col.HeaderText = "字段";
            this.Col.Name = "Col";
            // 
            // Sign
            // 
            this.Sign.HeaderText = "符号";
            this.Sign.Name = "Sign";
            this.Sign.Width = 60;
            // 
            // Val
            // 
            this.Val.HeaderText = "值";
            this.Val.Name = "Val";
            this.Val.Width = 150;
            // 
            // btOk
            // 
            this.btOk.Location = new System.Drawing.Point(255, 411);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(75, 23);
            this.btOk.TabIndex = 62;
            this.btOk.Text = "确定";
            this.btOk.UseVisualStyleBackColor = true;
            this.btOk.Click += new System.EventHandler(this.btOk_Click);
            // 
            // btCancel4
            // 
            this.btCancel4.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel4.Location = new System.Drawing.Point(353, 411);
            this.btCancel4.Name = "btCancel4";
            this.btCancel4.Size = new System.Drawing.Size(75, 23);
            this.btCancel4.TabIndex = 63;
            this.btCancel4.Text = "取消";
            this.btCancel4.UseVisualStyleBackColor = true;
            // 
            // SetViewFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 446);
            this.Controls.Add(this.btOk);
            this.Controls.Add(this.btCancel4);
            this.Controls.Add(this.cbAndOr);
            this.Controls.Add(this.btAdd4);
            this.Controls.Add(this.txtVal4);
            this.Controls.Add(this.cbSign4);
            this.Controls.Add(this.cbMasterColumn4);
            this.Controls.Add(this.btDel4);
            this.Controls.Add(this.txtMasterTable4);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dataGridView4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetViewFilter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "设置过滤条件";
            this.Load += new System.EventHandler(this.SetViewFilter_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbAndOr;
        private System.Windows.Forms.Button btAdd4;
        private System.Windows.Forms.TextBox txtVal4;
        private System.Windows.Forms.ComboBox cbSign4;
        private System.Windows.Forms.ComboBox cbMasterColumn4;
        private System.Windows.Forms.Button btDel4;
        private System.Windows.Forms.TextBox txtMasterTable4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGridView4;
        private System.Windows.Forms.DataGridViewTextBoxColumn AndOr;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sign;
        private System.Windows.Forms.DataGridViewTextBoxColumn Val;
        private System.Windows.Forms.Button btOk;
        private System.Windows.Forms.Button btCancel4;
    }
}