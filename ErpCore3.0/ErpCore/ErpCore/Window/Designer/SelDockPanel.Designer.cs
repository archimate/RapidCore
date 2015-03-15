namespace ErpCore.Window.Designer
{
    partial class SelDockPanel
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
            this.btCancel = new System.Windows.Forms.Button();
            this.btTop = new System.Windows.Forms.Button();
            this.btLeft = new System.Windows.Forms.Button();
            this.btFill = new System.Windows.Forms.Button();
            this.btRight = new System.Windows.Forms.Button();
            this.btBottom = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(174, 221);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 6;
            this.btCancel.Text = "取消";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // btTop
            // 
            this.btTop.Location = new System.Drawing.Point(12, 31);
            this.btTop.Name = "btTop";
            this.btTop.Size = new System.Drawing.Size(258, 38);
            this.btTop.TabIndex = 7;
            this.btTop.Text = "顶部";
            this.btTop.UseVisualStyleBackColor = true;
            this.btTop.Click += new System.EventHandler(this.btTop_Click);
            // 
            // btLeft
            // 
            this.btLeft.Location = new System.Drawing.Point(12, 75);
            this.btLeft.Name = "btLeft";
            this.btLeft.Size = new System.Drawing.Size(62, 77);
            this.btLeft.TabIndex = 8;
            this.btLeft.Text = "左边";
            this.btLeft.UseVisualStyleBackColor = true;
            this.btLeft.Click += new System.EventHandler(this.btLeft_Click);
            // 
            // btFill
            // 
            this.btFill.Location = new System.Drawing.Point(80, 75);
            this.btFill.Name = "btFill";
            this.btFill.Size = new System.Drawing.Size(124, 77);
            this.btFill.TabIndex = 9;
            this.btFill.Text = "中间";
            this.btFill.UseVisualStyleBackColor = true;
            this.btFill.Click += new System.EventHandler(this.btFill_Click);
            // 
            // btRight
            // 
            this.btRight.Location = new System.Drawing.Point(210, 75);
            this.btRight.Name = "btRight";
            this.btRight.Size = new System.Drawing.Size(60, 77);
            this.btRight.TabIndex = 10;
            this.btRight.Text = "右边";
            this.btRight.UseVisualStyleBackColor = true;
            this.btRight.Click += new System.EventHandler(this.btRight_Click);
            // 
            // btBottom
            // 
            this.btBottom.Location = new System.Drawing.Point(12, 158);
            this.btBottom.Name = "btBottom";
            this.btBottom.Size = new System.Drawing.Size(258, 38);
            this.btBottom.TabIndex = 11;
            this.btBottom.Text = "底部";
            this.btBottom.UseVisualStyleBackColor = true;
            this.btBottom.Click += new System.EventHandler(this.btBottom_Click);
            // 
            // SelDockPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.btBottom);
            this.Controls.Add(this.btRight);
            this.Controls.Add(this.btFill);
            this.Controls.Add(this.btLeft);
            this.Controls.Add(this.btTop);
            this.Controls.Add(this.btCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelDockPanel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "选择停靠面板";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btTop;
        private System.Windows.Forms.Button btLeft;
        private System.Windows.Forms.Button btFill;
        private System.Windows.Forms.Button btRight;
        private System.Windows.Forms.Button btBottom;
    }
}