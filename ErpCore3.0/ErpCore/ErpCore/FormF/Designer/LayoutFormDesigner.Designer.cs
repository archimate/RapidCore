namespace ErpCore.FormF.Designer
{
    partial class LayoutFormDesigner
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
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.tbtEdit = new System.Windows.Forms.ToolStripButton();
            this.tbtSave = new System.Windows.Forms.ToolStripButton();
            this.tbtDel = new System.Windows.Forms.ToolStripButton();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbtEdit,
            this.tbtSave,
            this.tbtDel});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(600, 35);
            this.toolStrip.TabIndex = 2;
            this.toolStrip.Text = "toolStrip1";
            // 
            // tbtEdit
            // 
            this.tbtEdit.Image = global::ErpCore.Properties.Resources.edit;
            this.tbtEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbtEdit.ImageTransparentColor = System.Drawing.Color.Silver;
            this.tbtEdit.Name = "tbtEdit";
            this.tbtEdit.Size = new System.Drawing.Size(33, 32);
            this.tbtEdit.Text = "编辑";
            this.tbtEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbtEdit.Click += new System.EventHandler(this.tbtEdit_Click);
            // 
            // tbtSave
            // 
            this.tbtSave.Image = global::ErpCore.Properties.Resources.SAVE;
            this.tbtSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbtSave.ImageTransparentColor = System.Drawing.Color.Silver;
            this.tbtSave.Name = "tbtSave";
            this.tbtSave.Size = new System.Drawing.Size(33, 32);
            this.tbtSave.Text = "保存";
            this.tbtSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbtSave.Click += new System.EventHandler(this.tbtSave_Click);
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
            // LayoutFormDesigner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 469);
            this.Controls.Add(this.toolStrip);
            this.IsMdiContainer = true;
            this.Name = "LayoutFormDesigner";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "表单设计器";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.LayoutWindowDesigner_Load);
            this.SizeChanged += new System.EventHandler(this.LayoutWindowDesigner_SizeChanged);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton tbtSave;
        private System.Windows.Forms.ToolStripButton tbtDel;
        private System.Windows.Forms.ToolStripButton tbtEdit;
    }
}