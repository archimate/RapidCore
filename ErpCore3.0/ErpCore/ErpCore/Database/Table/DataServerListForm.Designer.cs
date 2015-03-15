namespace ErpCore.Database.Table
{
    partial class DataServerListForm
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
            this.tbtDel = new System.Windows.Forms.ToolStripButton();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.tbtNew = new System.Windows.Forms.ToolStripButton();
            this.tbtEdit = new System.Windows.Forms.ToolStripButton();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.colServer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDBName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUserID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPwd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIsWrite = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // tbtDel
            // 
            this.tbtDel.Image = global::ErpCore.Properties.Resources.DELETE;
            this.tbtDel.ImageTransparentColor = System.Drawing.Color.Silver;
            this.tbtDel.Name = "tbtDel";
            this.tbtDel.Size = new System.Drawing.Size(33, 32);
            this.tbtDel.Text = "删除";
            this.tbtDel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbtDel.Click += new System.EventHandler(this.tbtDel_Click);
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbtNew,
            this.tbtEdit,
            this.tbtDel});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(488, 35);
            this.toolStrip.TabIndex = 8;
            this.toolStrip.Text = "toolStrip1";
            // 
            // tbtNew
            // 
            this.tbtNew.Image = global::ErpCore.Properties.Resources.NEW;
            this.tbtNew.ImageTransparentColor = System.Drawing.Color.Silver;
            this.tbtNew.Name = "tbtNew";
            this.tbtNew.Size = new System.Drawing.Size(33, 32);
            this.tbtNew.Text = "新建";
            this.tbtNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbtNew.Click += new System.EventHandler(this.tbtNew_Click);
            // 
            // tbtEdit
            // 
            this.tbtEdit.Image = global::ErpCore.Properties.Resources.edit;
            this.tbtEdit.ImageTransparentColor = System.Drawing.Color.Silver;
            this.tbtEdit.Name = "tbtEdit";
            this.tbtEdit.Size = new System.Drawing.Size(33, 32);
            this.tbtEdit.Text = "修改";
            this.tbtEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbtEdit.Click += new System.EventHandler(this.tbtEdit_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colServer,
            this.colDBName,
            this.colUserID,
            this.colPwd,
            this.colIsWrite});
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(0, 35);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.RowTemplate.Height = 23;
            this.dataGridView.Size = new System.Drawing.Size(488, 369);
            this.dataGridView.TabIndex = 9;
            // 
            // colServer
            // 
            this.colServer.HeaderText = "数据服务器";
            this.colServer.Name = "colServer";
            this.colServer.ReadOnly = true;
            // 
            // colDBName
            // 
            this.colDBName.HeaderText = "数据库名";
            this.colDBName.Name = "colDBName";
            this.colDBName.ReadOnly = true;
            // 
            // colUserID
            // 
            this.colUserID.HeaderText = "登录帐号";
            this.colUserID.Name = "colUserID";
            this.colUserID.ReadOnly = true;
            // 
            // colPwd
            // 
            this.colPwd.HeaderText = "登录密码";
            this.colPwd.Name = "colPwd";
            this.colPwd.ReadOnly = true;
            // 
            // colIsWrite
            // 
            this.colIsWrite.HeaderText = "可写";
            this.colIsWrite.Name = "colIsWrite";
            this.colIsWrite.ReadOnly = true;
            // 
            // DataServerListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 404);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.toolStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DataServerListForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "数据服务器列表";
            this.Load += new System.EventHandler(this.DataServerListForm_Load);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripButton tbtDel;
        public System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton tbtNew;
        private System.Windows.Forms.ToolStripButton tbtEdit;
        public System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn colServer;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDBName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUserID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPwd;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colIsWrite;
    }
}