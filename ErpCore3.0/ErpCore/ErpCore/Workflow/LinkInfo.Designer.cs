namespace ErpCore.Workflow
{
    partial class LinkInfo
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
            this.label1 = new System.Windows.Forms.Label();
            this.cbResult = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCondiction = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbNextActives = new System.Windows.Forms.ComboBox();
            this.btCancel = new System.Windows.Forms.Button();
            this.btOk = new System.Windows.Forms.Button();
            this.cbAndOr = new System.Windows.Forms.ComboBox();
            this.cbColumn = new System.Windows.Forms.ComboBox();
            this.cbSign = new System.Windows.Forms.ComboBox();
            this.txtVal = new System.Windows.Forms.TextBox();
            this.btAdd = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPreActives = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "审批结果：";
            // 
            // cbResult
            // 
            this.cbResult.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbResult.FormattingEnabled = true;
            this.cbResult.Location = new System.Drawing.Point(97, 52);
            this.cbResult.Name = "cbResult";
            this.cbResult.Size = new System.Drawing.Size(190, 20);
            this.cbResult.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "条件：";
            // 
            // txtCondiction
            // 
            this.txtCondiction.Location = new System.Drawing.Point(97, 78);
            this.txtCondiction.Multiline = true;
            this.txtCondiction.Name = "txtCondiction";
            this.txtCondiction.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCondiction.Size = new System.Drawing.Size(369, 108);
            this.txtCondiction.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 244);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "后置活动：";
            // 
            // cbNextActives
            // 
            this.cbNextActives.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNextActives.FormattingEnabled = true;
            this.cbNextActives.Location = new System.Drawing.Point(97, 241);
            this.cbNextActives.Name = "cbNextActives";
            this.cbNextActives.Size = new System.Drawing.Size(190, 20);
            this.cbNextActives.TabIndex = 5;
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(389, 287);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 30;
            this.btCancel.Text = "取消";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // btOk
            // 
            this.btOk.Location = new System.Drawing.Point(271, 287);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(75, 23);
            this.btOk.TabIndex = 29;
            this.btOk.Text = "确定";
            this.btOk.UseVisualStyleBackColor = true;
            this.btOk.Click += new System.EventHandler(this.btOk_Click);
            // 
            // cbAndOr
            // 
            this.cbAndOr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAndOr.FormattingEnabled = true;
            this.cbAndOr.Location = new System.Drawing.Point(97, 193);
            this.cbAndOr.Name = "cbAndOr";
            this.cbAndOr.Size = new System.Drawing.Size(65, 20);
            this.cbAndOr.TabIndex = 31;
            // 
            // cbColumn
            // 
            this.cbColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbColumn.FormattingEnabled = true;
            this.cbColumn.Location = new System.Drawing.Point(169, 193);
            this.cbColumn.Name = "cbColumn";
            this.cbColumn.Size = new System.Drawing.Size(80, 20);
            this.cbColumn.TabIndex = 32;
            // 
            // cbSign
            // 
            this.cbSign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSign.FormattingEnabled = true;
            this.cbSign.Location = new System.Drawing.Point(256, 193);
            this.cbSign.Name = "cbSign";
            this.cbSign.Size = new System.Drawing.Size(64, 20);
            this.cbSign.TabIndex = 33;
            // 
            // txtVal
            // 
            this.txtVal.Location = new System.Drawing.Point(327, 193);
            this.txtVal.Name = "txtVal";
            this.txtVal.Size = new System.Drawing.Size(74, 21);
            this.txtVal.TabIndex = 34;
            // 
            // btAdd
            // 
            this.btAdd.Location = new System.Drawing.Point(408, 192);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(58, 23);
            this.btAdd.TabIndex = 35;
            this.btAdd.Text = "添加";
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 36;
            this.label4.Text = "前置活动：";
            // 
            // txtPreActives
            // 
            this.txtPreActives.Location = new System.Drawing.Point(97, 20);
            this.txtPreActives.Name = "txtPreActives";
            this.txtPreActives.ReadOnly = true;
            this.txtPreActives.Size = new System.Drawing.Size(190, 21);
            this.txtPreActives.TabIndex = 37;
            // 
            // LinkInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 322);
            this.Controls.Add(this.txtPreActives);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btAdd);
            this.Controls.Add(this.txtVal);
            this.Controls.Add(this.cbSign);
            this.Controls.Add(this.cbColumn);
            this.Controls.Add(this.cbAndOr);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOk);
            this.Controls.Add(this.cbNextActives);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCondiction);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbResult);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LinkInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "连接";
            this.Load += new System.EventHandler(this.LinkInfo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbResult;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCondiction;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbNextActives;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btOk;
        private System.Windows.Forms.ComboBox cbAndOr;
        private System.Windows.Forms.ComboBox cbColumn;
        private System.Windows.Forms.ComboBox cbSign;
        private System.Windows.Forms.TextBox txtVal;
        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPreActives;
    }
}