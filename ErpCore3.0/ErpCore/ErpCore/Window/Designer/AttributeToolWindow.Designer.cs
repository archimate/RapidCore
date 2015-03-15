namespace ErpCore.Window.Designer
{
    partial class AttributeToolWindow
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.listColumn = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.listToolBarButton = new System.Windows.Forms.ListView();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btUIFormula = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.richTextFilter = new System.Windows.Forms.RichTextBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.listLinkageWindowControl = new System.Windows.Forms.ListView();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage4);
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.Controls.Add(this.tabPage5);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(238, 306);
            this.tabControl.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.propertyGrid);
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(230, 281);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "属性";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // propertyGrid
            // 
            this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid.Location = new System.Drawing.Point(3, 3);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(224, 275);
            this.propertyGrid.TabIndex = 0;
            this.propertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid_PropertyValueChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.listColumn);
            this.tabPage2.Location = new System.Drawing.Point(4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(230, 281);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "列";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // listColumn
            // 
            this.listColumn.CheckBoxes = true;
            this.listColumn.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listColumn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listColumn.Location = new System.Drawing.Point(3, 3);
            this.listColumn.Name = "listColumn";
            this.listColumn.Size = new System.Drawing.Size(224, 275);
            this.listColumn.TabIndex = 0;
            this.listColumn.UseCompatibleStateImageBehavior = false;
            this.listColumn.View = System.Windows.Forms.View.Details;
            this.listColumn.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.listColumn_ItemCheck);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "名称";
            this.columnHeader1.Width = 207;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.listToolBarButton);
            this.tabPage4.Location = new System.Drawing.Point(4, 4);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(230, 281);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "工具栏";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // listToolBarButton
            // 
            this.listToolBarButton.CheckBoxes = true;
            this.listToolBarButton.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.listToolBarButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listToolBarButton.Location = new System.Drawing.Point(0, 0);
            this.listToolBarButton.Name = "listToolBarButton";
            this.listToolBarButton.Size = new System.Drawing.Size(230, 281);
            this.listToolBarButton.TabIndex = 1;
            this.listToolBarButton.UseCompatibleStateImageBehavior = false;
            this.listToolBarButton.View = System.Windows.Forms.View.Details;
            this.listToolBarButton.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.listToolBarButton_ItemCheck);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "标题";
            this.columnHeader2.Width = 207;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btUIFormula);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Controls.Add(this.richTextFilter);
            this.tabPage3.Location = new System.Drawing.Point(4, 4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(230, 281);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "过滤";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btUIFormula
            // 
            this.btUIFormula.Location = new System.Drawing.Point(145, 167);
            this.btUIFormula.Name = "btUIFormula";
            this.btUIFormula.Size = new System.Drawing.Size(75, 23);
            this.btUIFormula.TabIndex = 2;
            this.btUIFormula.Text = "界面公式";
            this.btUIFormula.UseVisualStyleBackColor = true;
            this.btUIFormula.Click += new System.EventHandler(this.btUIFormula_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 198);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(214, 69);
            this.label2.TabIndex = 1;
            this.label2.Text = "表达式符合T-sql的where从句格式";
            // 
            // richTextFilter
            // 
            this.richTextFilter.Location = new System.Drawing.Point(6, 8);
            this.richTextFilter.Name = "richTextFilter";
            this.richTextFilter.Size = new System.Drawing.Size(214, 153);
            this.richTextFilter.TabIndex = 0;
            this.richTextFilter.Text = "";
            this.richTextFilter.TextChanged += new System.EventHandler(this.richTextFilter_TextChanged);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.listLinkageWindowControl);
            this.tabPage5.Controls.Add(this.label1);
            this.tabPage5.Location = new System.Drawing.Point(4, 4);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(230, 281);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "联动控件";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // listLinkageWindowControl
            // 
            this.listLinkageWindowControl.CheckBoxes = true;
            this.listLinkageWindowControl.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3});
            this.listLinkageWindowControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listLinkageWindowControl.Location = new System.Drawing.Point(0, 0);
            this.listLinkageWindowControl.Name = "listLinkageWindowControl";
            this.listLinkageWindowControl.Size = new System.Drawing.Size(230, 243);
            this.listLinkageWindowControl.TabIndex = 1;
            this.listLinkageWindowControl.UseCompatibleStateImageBehavior = false;
            this.listLinkageWindowControl.View = System.Windows.Forms.View.Details;
            this.listLinkageWindowControl.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listLinkageWindowControl_ItemChecked);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "名称";
            this.columnHeader3.Width = 207;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Location = new System.Drawing.Point(0, 243);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(230, 38);
            this.label1.TabIndex = 2;
            this.label1.Text = "联动控件是当界面上切换主表的记录时，从表的数据关联刷新，也称主从表。";
            // 
            // AttributeToolWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(238, 306);
            this.ControlBox = false;
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "AttributeToolWindow";
            this.Text = "属性";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.AttributeToolWindow_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.ListView listColumn;
        private System.Windows.Forms.RichTextBox richTextFilter;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ListView listToolBarButton;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        public System.Windows.Forms.ListView listLinkageWindowControl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btUIFormula;
    }
}