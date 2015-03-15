namespace ErpCore.View
{
    partial class SelViewType
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
            this.btOk = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdSingle = new System.Windows.Forms.RadioButton();
            this.rdMasterDetail = new System.Windows.Forms.RadioButton();
            this.rdMultMasterDetail = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btOk
            // 
            this.btOk.Location = new System.Drawing.Point(142, 246);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(75, 23);
            this.btOk.TabIndex = 5;
            this.btOk.Text = "确定";
            this.btOk.UseVisualStyleBackColor = true;
            this.btOk.Click += new System.EventHandler(this.btOk_Click);
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(261, 246);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 6;
            this.btCancel.Text = "取消";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdMultMasterDetail);
            this.groupBox1.Controls.Add(this.rdMasterDetail);
            this.groupBox1.Controls.Add(this.rdSingle);
            this.groupBox1.Location = new System.Drawing.Point(41, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(295, 207);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择视图类型";
            // 
            // rdSingle
            // 
            this.rdSingle.AutoSize = true;
            this.rdSingle.Checked = true;
            this.rdSingle.Location = new System.Drawing.Point(43, 45);
            this.rdSingle.Name = "rdSingle";
            this.rdSingle.Size = new System.Drawing.Size(71, 16);
            this.rdSingle.TabIndex = 0;
            this.rdSingle.TabStop = true;
            this.rdSingle.Text = "单表视图";
            this.rdSingle.UseVisualStyleBackColor = true;
            // 
            // rdMasterDetail
            // 
            this.rdMasterDetail.AutoSize = true;
            this.rdMasterDetail.Location = new System.Drawing.Point(43, 91);
            this.rdMasterDetail.Name = "rdMasterDetail";
            this.rdMasterDetail.Size = new System.Drawing.Size(83, 16);
            this.rdMasterDetail.TabIndex = 1;
            this.rdMasterDetail.TabStop = true;
            this.rdMasterDetail.Text = "主从表视图";
            this.rdMasterDetail.UseVisualStyleBackColor = true;
            // 
            // rdMultMasterDetail
            // 
            this.rdMultMasterDetail.AutoSize = true;
            this.rdMultMasterDetail.Location = new System.Drawing.Point(43, 137);
            this.rdMultMasterDetail.Name = "rdMultMasterDetail";
            this.rdMultMasterDetail.Size = new System.Drawing.Size(95, 16);
            this.rdMultMasterDetail.TabIndex = 2;
            this.rdMultMasterDetail.TabStop = true;
            this.rdMultMasterDetail.Text = "多主从表视图";
            this.rdMultMasterDetail.UseVisualStyleBackColor = true;
            // 
            // SelViewType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 281);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btOk);
            this.Controls.Add(this.btCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelViewType";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "选择视图类型";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btOk;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdMultMasterDetail;
        private System.Windows.Forms.RadioButton rdMasterDetail;
        private System.Windows.Forms.RadioButton rdSingle;
    }
}