namespace ErpCore.Database.Diagram
{
    partial class DiagramPanel
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DiagramPanel));
            this.listDiagram = new System.Windows.Forms.ListView();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tbtNew = new System.Windows.Forms.ToolStripButton();
            this.tbtEdit = new System.Windows.Forms.ToolStripButton();
            this.tbtDel = new System.Windows.Forms.ToolStripButton();
            this.tbtOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // listDiagram
            // 
            this.listDiagram.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listDiagram.LargeImageList = this.imageList1;
            this.listDiagram.Location = new System.Drawing.Point(0, 35);
            this.listDiagram.Name = "listDiagram";
            this.listDiagram.Size = new System.Drawing.Size(176, 115);
            this.listDiagram.SmallImageList = this.imageList1;
            this.listDiagram.TabIndex = 8;
            this.listDiagram.UseCompatibleStateImageBehavior = false;
            this.listDiagram.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listDiagram_MouseDoubleClick);
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbtNew,
            this.tbtEdit,
            this.tbtDel,
            this.tbtOpen});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(176, 35);
            this.toolStrip.TabIndex = 9;
            this.toolStrip.Text = "toolStrip1";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "association.png");
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
            // tbtOpen
            // 
            this.tbtOpen.Image = global::ErpCore.Properties.Resources.open;
            this.tbtOpen.ImageTransparentColor = System.Drawing.Color.Silver;
            this.tbtOpen.Name = "tbtOpen";
            this.tbtOpen.Size = new System.Drawing.Size(33, 32);
            this.tbtOpen.Text = "打开";
            this.tbtOpen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbtOpen.Click += new System.EventHandler(this.tbtOpen_Click);
            // 
            // DiagramPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listDiagram);
            this.Controls.Add(this.toolStrip);
            this.Name = "DiagramPanel";
            this.Size = new System.Drawing.Size(176, 150);
            this.Load += new System.EventHandler(this.DiagramPanel_Load);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listDiagram;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton tbtNew;
        private System.Windows.Forms.ToolStripButton tbtEdit;
        private System.Windows.Forms.ToolStripButton tbtDel;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripButton tbtOpen;
    }
}
