namespace ErpCore.Window
{
    partial class TableWindow
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
            this.tableCtrl = new ErpCore.CommonCtrl.TableGrid();
            this.SuspendLayout();
            // 
            // tableCtrl
            // 
            this.tableCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableCtrl.Location = new System.Drawing.Point(0, 0);
            this.tableCtrl.Name = "tableCtrl";
            this.tableCtrl.Size = new System.Drawing.Size(792, 566);
            this.tableCtrl.TabIndex = 0;
            // 
            // TableWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 566);
            this.Controls.Add(this.tableCtrl);
            this.Name = "TableWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "表数据";
            this.Load += new System.EventHandler(this.TableWindow_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ErpCore.CommonCtrl.TableGrid tableCtrl;
    }
}