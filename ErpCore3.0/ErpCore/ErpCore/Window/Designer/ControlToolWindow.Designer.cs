namespace ErpCore.Window.Designer
{
    partial class ControlToolWindow
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("DataGrid", 0);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("TreeCtrl", 1);
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("TabCtrl", 2);
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("ToolBar", 3);
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("ComboBox", 4);
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("ListBox", 5);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlToolWindow));
            this.listControl = new System.Windows.Forms.ListView();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // listControl
            // 
            this.listControl.BackColor = System.Drawing.SystemColors.Control;
            this.listControl.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listControl.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.listControl.FullRowSelect = true;
            this.listControl.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listControl.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6});
            this.listControl.Location = new System.Drawing.Point(13, 22);
            this.listControl.Name = "listControl";
            this.listControl.Size = new System.Drawing.Size(213, 247);
            this.listControl.SmallImageList = this.imageList1;
            this.listControl.TabIndex = 1;
            this.listControl.UseCompatibleStateImageBehavior = false;
            this.listControl.View = System.Windows.Forms.View.Details;
            this.listControl.DoubleClick += new System.EventHandler(this.listControl_DoubleClick);
            this.listControl.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.listControl_ItemDrag);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "控件";
            this.columnHeader2.Width = 201;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.SystemColors.Control;
            this.imageList1.Images.SetKeyName(0, "datagrid.png");
            this.imageList1.Images.SetKeyName(1, "treectrl.png");
            this.imageList1.Images.SetKeyName(2, "tabctrl.png");
            this.imageList1.Images.SetKeyName(3, "Toolbar.png");
            this.imageList1.Images.SetKeyName(4, "comboBox.png");
            this.imageList1.Images.SetKeyName(5, "listbox.png");
            this.imageList1.Images.SetKeyName(6, "textbox.png");
            this.imageList1.Images.SetKeyName(7, "label.png");
            // 
            // ControlToolWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(238, 306);
            this.ControlBox = false;
            this.Controls.Add(this.listControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "ControlToolWindow";
            this.Text = "工具箱";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.TableToolWindow_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listControl;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ImageList imageList1;
    }
}