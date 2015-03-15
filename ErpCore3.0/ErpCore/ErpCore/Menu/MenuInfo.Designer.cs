namespace ErpCore.Menu
{
    partial class MenuInfo
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.rdType1 = new System.Windows.Forms.RadioButton();
            this.rdType2 = new System.Windows.Forms.RadioButton();
            this.rdType4 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdType3 = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtParent = new System.Windows.Forms.TextBox();
            this.cbView = new System.Windows.Forms.ComboBox();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.cbWindow = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btBrowse = new System.Windows.Forms.Button();
            this.picIcon = new System.Windows.Forms.PictureBox();
            this.rdType5 = new System.Windows.Forms.RadioButton();
            this.cbReport = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // btOk
            // 
            this.btOk.Location = new System.Drawing.Point(181, 403);
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
            this.btCancel.Location = new System.Drawing.Point(300, 403);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 6;
            this.btCancel.Text = "取消";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "名称：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(46, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "父级：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(45, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "类型：";
            // 
            // rdType1
            // 
            this.rdType1.AutoSize = true;
            this.rdType1.Checked = true;
            this.rdType1.Location = new System.Drawing.Point(24, 15);
            this.rdType1.Name = "rdType1";
            this.rdType1.Size = new System.Drawing.Size(71, 16);
            this.rdType1.TabIndex = 10;
            this.rdType1.TabStop = true;
            this.rdType1.Text = "分级菜单";
            this.rdType1.UseVisualStyleBackColor = true;
            this.rdType1.Click += new System.EventHandler(this.rdType1_Click);
            // 
            // rdType2
            // 
            this.rdType2.AutoSize = true;
            this.rdType2.Location = new System.Drawing.Point(24, 38);
            this.rdType2.Name = "rdType2";
            this.rdType2.Size = new System.Drawing.Size(71, 16);
            this.rdType2.TabIndex = 11;
            this.rdType2.Text = "视图菜单";
            this.rdType2.UseVisualStyleBackColor = true;
            this.rdType2.Click += new System.EventHandler(this.rdType2_Click);
            // 
            // rdType4
            // 
            this.rdType4.AutoSize = true;
            this.rdType4.Location = new System.Drawing.Point(24, 82);
            this.rdType4.Name = "rdType4";
            this.rdType4.Size = new System.Drawing.Size(65, 16);
            this.rdType4.TabIndex = 12;
            this.rdType4.Text = "url菜单";
            this.rdType4.UseVisualStyleBackColor = true;
            this.rdType4.Click += new System.EventHandler(this.rdType4_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdType5);
            this.groupBox1.Controls.Add(this.rdType3);
            this.groupBox1.Controls.Add(this.rdType1);
            this.groupBox1.Controls.Add(this.rdType4);
            this.groupBox1.Controls.Add(this.rdType2);
            this.groupBox1.Location = new System.Drawing.Point(94, 61);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 133);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            // 
            // rdType3
            // 
            this.rdType3.AutoSize = true;
            this.rdType3.Location = new System.Drawing.Point(24, 60);
            this.rdType3.Name = "rdType3";
            this.rdType3.Size = new System.Drawing.Size(71, 16);
            this.rdType3.TabIndex = 13;
            this.rdType3.TabStop = true;
            this.rdType3.Text = "窗体菜单";
            this.rdType3.UseVisualStyleBackColor = true;
            this.rdType3.Click += new System.EventHandler(this.rdType3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(49, 204);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "视图：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(55, 256);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 15;
            this.label5.Text = "Url：";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(93, 11);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(247, 21);
            this.txtName.TabIndex = 16;
            // 
            // txtParent
            // 
            this.txtParent.Enabled = false;
            this.txtParent.Location = new System.Drawing.Point(93, 37);
            this.txtParent.Name = "txtParent";
            this.txtParent.Size = new System.Drawing.Size(247, 21);
            this.txtParent.TabIndex = 17;
            // 
            // cbView
            // 
            this.cbView.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbView.Enabled = false;
            this.cbView.FormattingEnabled = true;
            this.cbView.Location = new System.Drawing.Point(93, 200);
            this.cbView.Name = "cbView";
            this.cbView.Size = new System.Drawing.Size(247, 20);
            this.cbView.TabIndex = 18;
            // 
            // txtUrl
            // 
            this.txtUrl.Enabled = false;
            this.txtUrl.Location = new System.Drawing.Point(93, 252);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(247, 21);
            this.txtUrl.TabIndex = 19;
            // 
            // cbWindow
            // 
            this.cbWindow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWindow.Enabled = false;
            this.cbWindow.FormattingEnabled = true;
            this.cbWindow.Location = new System.Drawing.Point(94, 226);
            this.cbWindow.Name = "cbWindow";
            this.cbWindow.Size = new System.Drawing.Size(247, 20);
            this.cbWindow.TabIndex = 21;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(50, 230);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 20;
            this.label6.Text = "窗体：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(50, 305);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 22;
            this.label7.Text = "图标：";
            // 
            // btBrowse
            // 
            this.btBrowse.Location = new System.Drawing.Point(181, 359);
            this.btBrowse.Name = "btBrowse";
            this.btBrowse.Size = new System.Drawing.Size(75, 23);
            this.btBrowse.TabIndex = 25;
            this.btBrowse.Text = "浏览";
            this.btBrowse.UseVisualStyleBackColor = true;
            this.btBrowse.Click += new System.EventHandler(this.btBrowse_Click);
            // 
            // picIcon
            // 
            this.picIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picIcon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picIcon.Image = global::ErpCore.Properties.Resources.DLL;
            this.picIcon.Location = new System.Drawing.Point(93, 305);
            this.picIcon.Name = "picIcon";
            this.picIcon.Size = new System.Drawing.Size(82, 77);
            this.picIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picIcon.TabIndex = 26;
            this.picIcon.TabStop = false;
            // 
            // rdType5
            // 
            this.rdType5.AutoSize = true;
            this.rdType5.Location = new System.Drawing.Point(24, 104);
            this.rdType5.Name = "rdType5";
            this.rdType5.Size = new System.Drawing.Size(71, 16);
            this.rdType5.TabIndex = 14;
            this.rdType5.Text = "报表菜单";
            this.rdType5.UseVisualStyleBackColor = true;
            this.rdType5.Click += new System.EventHandler(this.rdType5_Click);
            // 
            // cbReport
            // 
            this.cbReport.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbReport.Enabled = false;
            this.cbReport.FormattingEnabled = true;
            this.cbReport.Location = new System.Drawing.Point(94, 279);
            this.cbReport.Name = "cbReport";
            this.cbReport.Size = new System.Drawing.Size(247, 20);
            this.cbReport.TabIndex = 28;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(50, 283);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 27;
            this.label8.Text = "报表：";
            // 
            // MenuInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 446);
            this.Controls.Add(this.cbReport);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.picIcon);
            this.Controls.Add(this.btBrowse);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbWindow);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.cbView);
            this.Controls.Add(this.txtParent);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btOk);
            this.Controls.Add(this.btCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MenuInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "菜单";
            this.Load += new System.EventHandler(this.MenuInfo_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btOk;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rdType1;
        private System.Windows.Forms.RadioButton rdType2;
        private System.Windows.Forms.RadioButton rdType4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtParent;
        private System.Windows.Forms.ComboBox cbView;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.RadioButton rdType3;
        private System.Windows.Forms.ComboBox cbWindow;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btBrowse;
        private System.Windows.Forms.PictureBox picIcon;
        private System.Windows.Forms.RadioButton rdType5;
        private System.Windows.Forms.ComboBox cbReport;
        private System.Windows.Forms.Label label8;
    }
}