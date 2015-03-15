namespace ErpCore.FormF.Designer
{
    partial class ChildenForm
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
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuItemEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItemBringToFront = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemSendToBack = new System.Windows.Forms.ToolStripMenuItem();
            this.flowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemEdit,
            this.MenuItemDelete,
            this.toolStripMenuItem1,
            this.MenuItemBringToFront,
            this.MenuItemSendToBack});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(119, 98);
            // 
            // MenuItemEdit
            // 
            this.MenuItemEdit.Name = "MenuItemEdit";
            this.MenuItemEdit.Size = new System.Drawing.Size(118, 22);
            this.MenuItemEdit.Text = "编辑";
            this.MenuItemEdit.Click += new System.EventHandler(this.MenuItemEdit_Click);
            // 
            // MenuItemDelete
            // 
            this.MenuItemDelete.Name = "MenuItemDelete";
            this.MenuItemDelete.Size = new System.Drawing.Size(118, 22);
            this.MenuItemDelete.Text = "删除";
            this.MenuItemDelete.Click += new System.EventHandler(this.MenuItemDelete_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(115, 6);
            // 
            // MenuItemBringToFront
            // 
            this.MenuItemBringToFront.Name = "MenuItemBringToFront";
            this.MenuItemBringToFront.Size = new System.Drawing.Size(118, 22);
            this.MenuItemBringToFront.Text = "置于顶层";
            this.MenuItemBringToFront.Click += new System.EventHandler(this.MenuItemBringToFront_Click);
            // 
            // MenuItemSendToBack
            // 
            this.MenuItemSendToBack.Name = "MenuItemSendToBack";
            this.MenuItemSendToBack.Size = new System.Drawing.Size(118, 22);
            this.MenuItemSendToBack.Text = "置于底层";
            this.MenuItemSendToBack.Click += new System.EventHandler(this.MenuItemSendToBack_Click);
            // 
            // flowPanel
            // 
            this.flowPanel.AllowDrop = true;
            this.flowPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowPanel.Location = new System.Drawing.Point(0, 0);
            this.flowPanel.Name = "flowPanel";
            this.flowPanel.Size = new System.Drawing.Size(762, 616);
            this.flowPanel.TabIndex = 1;
            this.flowPanel.Layout += new System.Windows.Forms.LayoutEventHandler(this.flowPanel_Layout);
            this.flowPanel.DragDrop += new System.Windows.Forms.DragEventHandler(this.flowPanel_DragDrop);
            this.flowPanel.Resize += new System.EventHandler(this.flowPanel_Resize);
            this.flowPanel.DragEnter += new System.Windows.Forms.DragEventHandler(this.flowPanel_DragEnter);
            // 
            // ChildenForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 616);
            this.Controls.Add(this.flowPanel);
            this.Name = "ChildenForm";
            this.ShowInTaskbar = false;
            this.Text = "布局";
            this.Load += new System.EventHandler(this.ChildenWindow_Load);
            this.Click += new System.EventHandler(this.ChildenWindow_Click);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MenuItemEdit;
        private System.Windows.Forms.ToolStripMenuItem MenuItemDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem MenuItemBringToFront;
        private System.Windows.Forms.ToolStripMenuItem MenuItemSendToBack;
        private System.Windows.Forms.FlowLayoutPanel flowPanel;

    }
}