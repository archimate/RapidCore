namespace ErpCore.Window.Designer
{
    partial class ChildenWindow
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
            this.panelTop = new System.Windows.Forms.Panel();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.splitter3 = new System.Windows.Forms.Splitter();
            this.panelRight = new System.Windows.Forms.Panel();
            this.splitter4 = new System.Windows.Forms.Splitter();
            this.panelFill = new System.Windows.Forms.Panel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuItemEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItemBringToFront = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemSendToBack = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.AllowDrop = true;
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(762, 132);
            this.panelTop.TabIndex = 0;
            this.panelTop.DragDrop += new System.Windows.Forms.DragEventHandler(this.panelTop_DragDrop);
            this.panelTop.Resize += new System.EventHandler(this.panelTop_Resize);
            this.panelTop.DragEnter += new System.Windows.Forms.DragEventHandler(this.panelTop_DragEnter);
            // 
            // panelBottom
            // 
            this.panelBottom.AllowDrop = true;
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 469);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(762, 147);
            this.panelBottom.TabIndex = 1;
            this.panelBottom.DragDrop += new System.Windows.Forms.DragEventHandler(this.panelBottom_DragDrop);
            this.panelBottom.Resize += new System.EventHandler(this.panelBottom_Resize);
            this.panelBottom.DragEnter += new System.Windows.Forms.DragEventHandler(this.panelBottom_DragEnter);
            // 
            // splitter1
            // 
            this.splitter1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 132);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(762, 3);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // splitter2
            // 
            this.splitter2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter2.Location = new System.Drawing.Point(0, 466);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(762, 3);
            this.splitter2.TabIndex = 3;
            this.splitter2.TabStop = false;
            // 
            // panelLeft
            // 
            this.panelLeft.AllowDrop = true;
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 135);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(178, 331);
            this.panelLeft.TabIndex = 4;
            this.panelLeft.DragDrop += new System.Windows.Forms.DragEventHandler(this.panelLeft_DragDrop);
            this.panelLeft.Resize += new System.EventHandler(this.panelLeft_Resize);
            this.panelLeft.DragEnter += new System.Windows.Forms.DragEventHandler(this.panelLeft_DragEnter);
            // 
            // splitter3
            // 
            this.splitter3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitter3.Location = new System.Drawing.Point(178, 135);
            this.splitter3.Name = "splitter3";
            this.splitter3.Size = new System.Drawing.Size(3, 331);
            this.splitter3.TabIndex = 5;
            this.splitter3.TabStop = false;
            // 
            // panelRight
            // 
            this.panelRight.AllowDrop = true;
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelRight.Location = new System.Drawing.Point(600, 135);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(162, 331);
            this.panelRight.TabIndex = 6;
            this.panelRight.DragDrop += new System.Windows.Forms.DragEventHandler(this.panelRight_DragDrop);
            this.panelRight.Resize += new System.EventHandler(this.panelRight_Resize);
            this.panelRight.DragEnter += new System.Windows.Forms.DragEventHandler(this.panelRight_DragEnter);
            // 
            // splitter4
            // 
            this.splitter4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitter4.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter4.Location = new System.Drawing.Point(597, 135);
            this.splitter4.Name = "splitter4";
            this.splitter4.Size = new System.Drawing.Size(3, 331);
            this.splitter4.TabIndex = 7;
            this.splitter4.TabStop = false;
            // 
            // panelFill
            // 
            this.panelFill.AllowDrop = true;
            this.panelFill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFill.Location = new System.Drawing.Point(181, 135);
            this.panelFill.Name = "panelFill";
            this.panelFill.Size = new System.Drawing.Size(416, 331);
            this.panelFill.TabIndex = 8;
            this.panelFill.DragDrop += new System.Windows.Forms.DragEventHandler(this.panelFill_DragDrop);
            this.panelFill.Resize += new System.EventHandler(this.panelFill_Resize);
            this.panelFill.DragEnter += new System.Windows.Forms.DragEventHandler(this.panelFill_DragEnter);
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
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 120);
            // 
            // MenuItemEdit
            // 
            this.MenuItemEdit.Name = "MenuItemEdit";
            this.MenuItemEdit.Size = new System.Drawing.Size(152, 22);
            this.MenuItemEdit.Text = "编辑";
            this.MenuItemEdit.Click += new System.EventHandler(this.MenuItemEdit_Click);
            // 
            // MenuItemDelete
            // 
            this.MenuItemDelete.Name = "MenuItemDelete";
            this.MenuItemDelete.Size = new System.Drawing.Size(152, 22);
            this.MenuItemDelete.Text = "删除";
            this.MenuItemDelete.Click += new System.EventHandler(this.MenuItemDelete_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 6);
            // 
            // MenuItemBringToFront
            // 
            this.MenuItemBringToFront.Name = "MenuItemBringToFront";
            this.MenuItemBringToFront.Size = new System.Drawing.Size(152, 22);
            this.MenuItemBringToFront.Text = "置于顶层";
            this.MenuItemBringToFront.Click += new System.EventHandler(this.MenuItemBringToFront_Click);
            // 
            // MenuItemSendToBack
            // 
            this.MenuItemSendToBack.Name = "MenuItemSendToBack";
            this.MenuItemSendToBack.Size = new System.Drawing.Size(152, 22);
            this.MenuItemSendToBack.Text = "置于底层";
            this.MenuItemSendToBack.Click += new System.EventHandler(this.MenuItemSendToBack_Click);
            // 
            // ChildenWindow
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 616);
            this.Controls.Add(this.panelFill);
            this.Controls.Add(this.splitter4);
            this.Controls.Add(this.panelRight);
            this.Controls.Add(this.splitter3);
            this.Controls.Add(this.panelLeft);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelTop);
            this.Name = "ChildenWindow";
            this.ShowInTaskbar = false;
            this.Text = "布局";
            this.Load += new System.EventHandler(this.ChildenWindow_Load);
            this.Activated += new System.EventHandler(this.ChildenWindow_Activated);
            this.Click += new System.EventHandler(this.ChildenWindow_Click);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.Splitter splitter3;
        private System.Windows.Forms.Splitter splitter4;
        public System.Windows.Forms.Panel panelTop;
        public System.Windows.Forms.Panel panelBottom;
        public System.Windows.Forms.Panel panelLeft;
        public System.Windows.Forms.Panel panelRight;
        public System.Windows.Forms.Panel panelFill;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MenuItemEdit;
        private System.Windows.Forms.ToolStripMenuItem MenuItemDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem MenuItemBringToFront;
        private System.Windows.Forms.ToolStripMenuItem MenuItemSendToBack;

    }
}