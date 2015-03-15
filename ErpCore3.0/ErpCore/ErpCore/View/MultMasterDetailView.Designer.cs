namespace ErpCore.View
{
    partial class MultMasterDetailView
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
            this.viewGrid = new ErpCore.CommonCtrl.MultMasterDetailViewGrid();
            this.SuspendLayout();
            // 
            // viewGrid
            // 
            this.viewGrid.BaseObjectMgr = null;
            this.viewGrid.CaptionText = "";
            this.viewGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewGrid.Location = new System.Drawing.Point(0, 0);
            this.viewGrid.Name = "viewGrid";
            this.viewGrid.ShowTitleBar = false;
            this.viewGrid.ShowToolBar = true;
            this.viewGrid.Size = new System.Drawing.Size(818, 532);
            this.viewGrid.TabIndex = 1;
            this.viewGrid.View = null;
            // 
            // MultMasterDetailView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(818, 532);
            this.Controls.Add(this.viewGrid);
            this.Name = "MultMasterDetailView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "多主从表视图";
            this.Load += new System.EventHandler(this.MultMasterDetailView_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ErpCore.CommonCtrl.MultMasterDetailViewGrid viewGrid;

    }
}