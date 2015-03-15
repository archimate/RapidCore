namespace ErpCore.CommonCtrl
{
    partial class BinaryCtrl
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
            this.picBox = new System.Windows.Forms.PictureBox();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.btBrowse = new System.Windows.Forms.Button();
            this.lbFileName = new System.Windows.Forms.Label();
            this.linkDel = new System.Windows.Forms.LinkLabel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.lbCaption = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
            this.SuspendLayout();
            // 
            // picBox
            // 
            this.picBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picBox.Location = new System.Drawing.Point(0, 0);
            this.picBox.Name = "picBox";
            this.picBox.Size = new System.Drawing.Size(100, 100);
            this.picBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBox.TabIndex = 0;
            this.picBox.TabStop = false;
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(111, 72);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(176, 21);
            this.txtUrl.TabIndex = 1;
            // 
            // btBrowse
            // 
            this.btBrowse.Location = new System.Drawing.Point(293, 71);
            this.btBrowse.Name = "btBrowse";
            this.btBrowse.Size = new System.Drawing.Size(37, 23);
            this.btBrowse.TabIndex = 2;
            this.btBrowse.Text = "...";
            this.btBrowse.UseVisualStyleBackColor = true;
            this.btBrowse.Click += new System.EventHandler(this.btBrowse_Click);
            // 
            // lbFileName
            // 
            this.lbFileName.AutoSize = true;
            this.lbFileName.Location = new System.Drawing.Point(111, 32);
            this.lbFileName.Name = "lbFileName";
            this.lbFileName.Size = new System.Drawing.Size(0, 12);
            this.lbFileName.TabIndex = 3;
            this.lbFileName.Visible = false;
            // 
            // linkDel
            // 
            this.linkDel.AutoSize = true;
            this.linkDel.Location = new System.Drawing.Point(111, 48);
            this.linkDel.Name = "linkDel";
            this.linkDel.Size = new System.Drawing.Size(29, 12);
            this.linkDel.TabIndex = 4;
            this.linkDel.TabStop = true;
            this.linkDel.Text = "删除";
            this.linkDel.Visible = false;
            this.linkDel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkDel_LinkClicked);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // lbCaption
            // 
            this.lbCaption.AutoSize = true;
            this.lbCaption.Location = new System.Drawing.Point(111, 9);
            this.lbCaption.Name = "lbCaption";
            this.lbCaption.Size = new System.Drawing.Size(41, 12);
            this.lbCaption.TabIndex = 5;
            this.lbCaption.Text = "标题：";
            // 
            // BinaryCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lbCaption);
            this.Controls.Add(this.linkDel);
            this.Controls.Add(this.lbFileName);
            this.Controls.Add(this.btBrowse);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.picBox);
            this.Name = "BinaryCtrl";
            this.Size = new System.Drawing.Size(336, 100);
            this.Load += new System.EventHandler(this.BinaryCtrl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picBox;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Button btBrowse;
        private System.Windows.Forms.Label lbFileName;
        private System.Windows.Forms.LinkLabel linkDel;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        public System.Windows.Forms.Label lbCaption;
    }
}
