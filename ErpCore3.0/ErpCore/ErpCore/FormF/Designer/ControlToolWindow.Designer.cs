namespace ErpCore.FormF.Designer
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
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem("文本型", 6);
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem("备注型", 8);
            System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem("整型", 11);
            System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem("长整型", 12);
            System.Windows.Forms.ListViewItem listViewItem11 = new System.Windows.Forms.ListViewItem("布尔型", 10);
            System.Windows.Forms.ListViewItem listViewItem12 = new System.Windows.Forms.ListViewItem("引用型", 4);
            System.Windows.Forms.ListViewItem listViewItem13 = new System.Windows.Forms.ListViewItem("数值型", 9);
            System.Windows.Forms.ListViewItem listViewItem14 = new System.Windows.Forms.ListViewItem("日期型", 13);
            this.listDataControl = new System.Windows.Forms.ListView();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.listFormControl = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // listDataControl
            // 
            this.listDataControl.BackColor = System.Drawing.SystemColors.Control;
            this.listDataControl.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listDataControl.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.listDataControl.FullRowSelect = true;
            this.listDataControl.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listDataControl.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6});
            this.listDataControl.Location = new System.Drawing.Point(13, 204);
            this.listDataControl.Name = "listDataControl";
            this.listDataControl.Size = new System.Drawing.Size(213, 152);
            this.listDataControl.SmallImageList = this.imageList1;
            this.listDataControl.TabIndex = 1;
            this.listDataControl.UseCompatibleStateImageBehavior = false;
            this.listDataControl.View = System.Windows.Forms.View.Details;
            this.listDataControl.DoubleClick += new System.EventHandler(this.listControl_DoubleClick);
            this.listDataControl.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.listControl_ItemDrag);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "数据域";
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
            this.imageList1.Images.SetKeyName(8, "textarea.png");
            this.imageList1.Images.SetKeyName(9, "numberbox.png");
            this.imageList1.Images.SetKeyName(10, "boolbox.png");
            this.imageList1.Images.SetKeyName(11, "intbox.png");
            this.imageList1.Images.SetKeyName(12, "longbox.png");
            this.imageList1.Images.SetKeyName(13, "datetimepicker.png");
            // 
            // listFormControl
            // 
            this.listFormControl.BackColor = System.Drawing.SystemColors.Control;
            this.listFormControl.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listFormControl.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listFormControl.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listFormControl.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem7,
            listViewItem8,
            listViewItem9,
            listViewItem10,
            listViewItem11,
            listViewItem12,
            listViewItem13,
            listViewItem14});
            this.listFormControl.Location = new System.Drawing.Point(12, 2);
            this.listFormControl.Name = "listFormControl";
            this.listFormControl.Size = new System.Drawing.Size(214, 196);
            this.listFormControl.SmallImageList = this.imageList1;
            this.listFormControl.TabIndex = 2;
            this.listFormControl.UseCompatibleStateImageBehavior = false;
            this.listFormControl.View = System.Windows.Forms.View.Details;
            this.listFormControl.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.listFormControl_ItemDrag);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "表单域";
            this.columnHeader1.Width = 208;
            // 
            // ControlToolWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(238, 368);
            this.ControlBox = false;
            this.Controls.Add(this.listFormControl);
            this.Controls.Add(this.listDataControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "ControlToolWindow";
            this.Text = "工具箱";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.TableToolWindow_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listDataControl;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ListView listFormControl;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}