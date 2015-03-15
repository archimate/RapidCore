namespace ErpCore.Database.Table
{
    partial class TablePanel
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
            this.listTable = new System.Windows.Forms.ListView();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.tbtNew = new System.Windows.Forms.ToolStripButton();
            this.tbtEdit = new System.Windows.Forms.ToolStripButton();
            this.tbtDel = new System.Windows.Forms.ToolStripButton();
            this.tbtData = new System.Windows.Forms.ToolStripButton();
            this.tbtMakeModel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // listTable
            // 
            this.listTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listTable.FullRowSelect = true;
            this.listTable.Location = new System.Drawing.Point(0, 40);
            this.listTable.Name = "listTable";
            this.listTable.Size = new System.Drawing.Size(365, 187);
            this.listTable.TabIndex = 3;
            this.listTable.UseCompatibleStateImageBehavior = false;
            this.listTable.View = System.Windows.Forms.View.Details;
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbtNew,
            this.tbtEdit,
            this.tbtDel,
            this.tbtData,
            this.tbtMakeModel,
            this.toolStripSeparator1});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(365, 40);
            this.toolStrip.TabIndex = 4;
            this.toolStrip.Text = "toolStrip1";
            // 
            // tbtNew
            // 
            this.tbtNew.Image = global::ErpCore.Properties.Resources.NEW;
            this.tbtNew.ImageTransparentColor = System.Drawing.Color.Silver;
            this.tbtNew.Name = "tbtNew";
            this.tbtNew.Size = new System.Drawing.Size(36, 37);
            this.tbtNew.Text = "新建";
            this.tbtNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbtNew.Click += new System.EventHandler(this.tbtNew_Click);
            // 
            // tbtEdit
            // 
            this.tbtEdit.Image = global::ErpCore.Properties.Resources.edit;
            this.tbtEdit.ImageTransparentColor = System.Drawing.Color.Silver;
            this.tbtEdit.Name = "tbtEdit";
            this.tbtEdit.Size = new System.Drawing.Size(36, 37);
            this.tbtEdit.Text = "修改";
            this.tbtEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbtEdit.Click += new System.EventHandler(this.tbtEdit_Click);
            // 
            // tbtDel
            // 
            this.tbtDel.Image = global::ErpCore.Properties.Resources.DELETE;
            this.tbtDel.ImageTransparentColor = System.Drawing.Color.Silver;
            this.tbtDel.Name = "tbtDel";
            this.tbtDel.Size = new System.Drawing.Size(36, 37);
            this.tbtDel.Text = "删除";
            this.tbtDel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbtDel.Click += new System.EventHandler(this.tbtDel_Click);
            // 
            // tbtData
            // 
            this.tbtData.Image = global::ErpCore.Properties.Resources.data;
            this.tbtData.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbtData.ImageTransparentColor = System.Drawing.Color.Silver;
            this.tbtData.Name = "tbtData";
            this.tbtData.Size = new System.Drawing.Size(36, 37);
            this.tbtData.Text = "数据";
            this.tbtData.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbtData.Click += new System.EventHandler(this.tbtData_Click);
            // 
            // tbtMakeModel
            // 
            this.tbtMakeModel.Image = global::ErpCore.Properties.Resources._class;
            this.tbtMakeModel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbtMakeModel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtMakeModel.Name = "tbtMakeModel";
            this.tbtMakeModel.Size = new System.Drawing.Size(60, 37);
            this.tbtMakeModel.Text = "代码生成";
            this.tbtMakeModel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbtMakeModel.Click += new System.EventHandler(this.tbtMakeModel_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 40);
            // 
            // TablePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listTable);
            this.Controls.Add(this.toolStrip);
            this.Name = "TablePanel";
            this.Size = new System.Drawing.Size(365, 227);
            this.Load += new System.EventHandler(this.TablePanel_Load);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listTable;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton tbtNew;
        private System.Windows.Forms.ToolStripButton tbtEdit;
        private System.Windows.Forms.ToolStripButton tbtDel;
        private System.Windows.Forms.ToolStripButton tbtData;
        private System.Windows.Forms.ToolStripButton tbtMakeModel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}
