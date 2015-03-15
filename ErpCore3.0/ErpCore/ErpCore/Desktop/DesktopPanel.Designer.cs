namespace ErpCore.Desktop
{
    partial class DesktopPanel
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
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuItemBackground = new System.Windows.Forms.ToolStripMenuItem();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.flowLayoutPanelBottom = new System.Windows.Forms.FlowLayoutPanel();
            this.rdDefault = new System.Windows.Forms.RadioButton();
            this.toolTip_Group = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStrip1.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.flowLayoutPanelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.AutoScroll = true;
            this.flowLayoutPanel.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel.ContextMenuStrip = this.contextMenuStrip1;
            this.flowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Padding = new System.Windows.Forms.Padding(50);
            this.flowLayoutPanel.Size = new System.Drawing.Size(729, 434);
            this.flowLayoutPanel.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemBackground});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 26);
            // 
            // MenuItemBackground
            // 
            this.MenuItemBackground.Name = "MenuItemBackground";
            this.MenuItemBackground.Size = new System.Drawing.Size(100, 22);
            this.MenuItemBackground.Text = "背景";
            this.MenuItemBackground.Click += new System.EventHandler(this.MenuItemBackground_Click);
            // 
            // panelBottom
            // 
            this.panelBottom.BackColor = System.Drawing.Color.Transparent;
            this.panelBottom.Controls.Add(this.flowLayoutPanelBottom);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 434);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(729, 63);
            this.panelBottom.TabIndex = 1;
            // 
            // flowLayoutPanelBottom
            // 
            this.flowLayoutPanelBottom.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.flowLayoutPanelBottom.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanelBottom.Controls.Add(this.rdDefault);
            this.flowLayoutPanelBottom.Location = new System.Drawing.Point(340, 19);
            this.flowLayoutPanelBottom.Name = "flowLayoutPanelBottom";
            this.flowLayoutPanelBottom.Size = new System.Drawing.Size(200, 27);
            this.flowLayoutPanelBottom.TabIndex = 1;
            // 
            // rdDefault
            // 
            this.rdDefault.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.rdDefault.AutoSize = true;
            this.rdDefault.BackColor = System.Drawing.Color.Transparent;
            this.rdDefault.Location = new System.Drawing.Point(3, 3);
            this.rdDefault.Name = "rdDefault";
            this.rdDefault.Size = new System.Drawing.Size(14, 13);
            this.rdDefault.TabIndex = 0;
            this.rdDefault.TabStop = true;
            this.toolTip_Group.SetToolTip(this.rdDefault, "主桌面");
            this.rdDefault.UseVisualStyleBackColor = false;
            this.rdDefault.Click += new System.EventHandler(this.DesktopGroup_rdButton_Click);
            // 
            // DesktopPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(729, 497);
            this.Controls.Add(this.flowLayoutPanel);
            this.Controls.Add(this.panelBottom);
            this.Name = "DesktopPanel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "桌面";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.DesktopPanel_Load);
            this.Shown += new System.EventHandler(this.DesktopPanel_Shown);
            this.contextMenuStrip1.ResumeLayout(false);
            this.panelBottom.ResumeLayout(false);
            this.flowLayoutPanelBottom.ResumeLayout(false);
            this.flowLayoutPanelBottom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MenuItemBackground;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.RadioButton rdDefault;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelBottom;
        private System.Windows.Forms.ToolTip toolTip_Group;
    }
}