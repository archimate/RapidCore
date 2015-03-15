namespace ErpCore.Database.Diagram
{
    partial class DesignerForm
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
            this.panelScroll = new System.Windows.Forms.Panel();
            this.panelDiagram = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tbtNewTable = new System.Windows.Forms.ToolStripButton();
            this.tbtAddTable = new System.Windows.Forms.ToolStripButton();
            this.tbtRemoveTable = new System.Windows.Forms.ToolStripButton();
            this.tbtSave = new System.Windows.Forms.ToolStripButton();
            this.panelScroll.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelScroll
            // 
            this.panelScroll.AutoScroll = true;
            this.panelScroll.Controls.Add(this.panelDiagram);
            this.panelScroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelScroll.Location = new System.Drawing.Point(0, 25);
            this.panelScroll.Name = "panelScroll";
            this.panelScroll.Size = new System.Drawing.Size(506, 410);
            this.panelScroll.TabIndex = 0;
            // 
            // panelDiagram
            // 
            this.panelDiagram.Location = new System.Drawing.Point(0, 0);
            this.panelDiagram.Name = "panelDiagram";
            this.panelDiagram.Size = new System.Drawing.Size(385, 304);
            this.panelDiagram.TabIndex = 0;
            this.panelDiagram.Paint += new System.Windows.Forms.PaintEventHandler(this.panelDiagram_Paint);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbtNewTable,
            this.tbtAddTable,
            this.tbtRemoveTable,
            this.tbtSave});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(506, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tbtNewTable
            // 
            this.tbtNewTable.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtNewTable.Image = global::ErpCore.Properties.Resources.newtable;
            this.tbtNewTable.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbtNewTable.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtNewTable.Name = "tbtNewTable";
            this.tbtNewTable.Size = new System.Drawing.Size(23, 22);
            this.tbtNewTable.Text = "新建表";
            this.tbtNewTable.Click += new System.EventHandler(this.tbtNewTable_Click);
            // 
            // tbtAddTable
            // 
            this.tbtAddTable.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtAddTable.Image = global::ErpCore.Properties.Resources.addtable;
            this.tbtAddTable.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbtAddTable.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtAddTable.Name = "tbtAddTable";
            this.tbtAddTable.Size = new System.Drawing.Size(23, 22);
            this.tbtAddTable.Text = "添加表到关系图";
            this.tbtAddTable.Click += new System.EventHandler(this.tbtAddTable_Click);
            // 
            // tbtRemoveTable
            // 
            this.tbtRemoveTable.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtRemoveTable.Image = global::ErpCore.Properties.Resources.removetable;
            this.tbtRemoveTable.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbtRemoveTable.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtRemoveTable.Name = "tbtRemoveTable";
            this.tbtRemoveTable.Size = new System.Drawing.Size(23, 22);
            this.tbtRemoveTable.Text = "从关系图移除表";
            this.tbtRemoveTable.Click += new System.EventHandler(this.tbtRemoveTable_Click);
            // 
            // tbtSave
            // 
            this.tbtSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtSave.Image = global::ErpCore.Properties.Resources.SAVE;
            this.tbtSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbtSave.ImageTransparentColor = System.Drawing.Color.Silver;
            this.tbtSave.Name = "tbtSave";
            this.tbtSave.Size = new System.Drawing.Size(23, 22);
            this.tbtSave.Text = "保存";
            this.tbtSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbtSave.Click += new System.EventHandler(this.tbtSave_Click);
            // 
            // DesignerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 435);
            this.Controls.Add(this.panelScroll);
            this.Controls.Add(this.toolStrip1);
            this.Name = "DesignerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "关系图";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.DesignerForm_Load);
            this.Resize += new System.EventHandler(this.DesignerForm_Resize);
            this.panelScroll.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelScroll;
        private System.Windows.Forms.Panel panelDiagram;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tbtNewTable;
        private System.Windows.Forms.ToolStripButton tbtAddTable;
        private System.Windows.Forms.ToolStripButton tbtRemoveTable;
        private System.Windows.Forms.ToolStripButton tbtSave;

    }
}