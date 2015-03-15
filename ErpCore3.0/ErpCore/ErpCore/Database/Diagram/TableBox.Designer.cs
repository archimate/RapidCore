namespace ErpCore.Database.Diagram
{
    partial class TableBox
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
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuItemStandard = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemEditTable = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemStandard,
            this.MenuItemEditTable});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(107, 48);
            // 
            // MenuItemStandard
            // 
            this.MenuItemStandard.Name = "MenuItemStandard";
            this.MenuItemStandard.Size = new System.Drawing.Size(106, 22);
            this.MenuItemStandard.Text = "标准";
            this.MenuItemStandard.Click += new System.EventHandler(this.MenuItemStandard_Click);
            // 
            // MenuItemEditTable
            // 
            this.MenuItemEditTable.Name = "MenuItemEditTable";
            this.MenuItemEditTable.Click += new System.EventHandler(MenuItemEditTable_Click);
            this.MenuItemEditTable.Size = new System.Drawing.Size(106, 22);
            this.MenuItemEditTable.Text = "设计表";
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }



        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem MenuItemStandard;
        private System.Windows.Forms.ToolStripMenuItem MenuItemEditTable;
    }
}
