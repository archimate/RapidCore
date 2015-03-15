namespace ErpCore.Database.Table
{
    partial class TableInfoForm
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
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.tbtSave = new System.Windows.Forms.ToolStripButton();
            this.tbtCancel = new System.Windows.Forms.ToolStripButton();
            this.tbtDel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tbtUp = new System.Windows.Forms.ToolStripButton();
            this.tbtDown = new System.Windows.Forms.ToolStripButton();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ckIsSystem = new System.Windows.Forms.CheckBox();
            this.btSetDataServer = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTableCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTableName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colLen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAllowNull = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colIsSystem = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colIsUnique = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbtSave,
            this.tbtCancel,
            this.tbtDel,
            this.toolStripSeparator1,
            this.tbtUp,
            this.tbtDown});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(792, 38);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip1";
            // 
            // tbtSave
            // 
            this.tbtSave.Image = global::ErpCore.Properties.Resources.SAVE;
            this.tbtSave.ImageTransparentColor = System.Drawing.Color.Silver;
            this.tbtSave.Name = "tbtSave";
            this.tbtSave.Size = new System.Drawing.Size(35, 35);
            this.tbtSave.Text = "保存";
            this.tbtSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbtSave.Click += new System.EventHandler(this.tbtSave_Click);
            // 
            // tbtCancel
            // 
            this.tbtCancel.Image = global::ErpCore.Properties.Resources.exit;
            this.tbtCancel.ImageTransparentColor = System.Drawing.Color.Silver;
            this.tbtCancel.Name = "tbtCancel";
            this.tbtCancel.Size = new System.Drawing.Size(35, 35);
            this.tbtCancel.Text = "取消";
            this.tbtCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbtCancel.Click += new System.EventHandler(this.tbtCancel_Click);
            // 
            // tbtDel
            // 
            this.tbtDel.Image = global::ErpCore.Properties.Resources.DELETE;
            this.tbtDel.ImageTransparentColor = System.Drawing.Color.Silver;
            this.tbtDel.Name = "tbtDel";
            this.tbtDel.Size = new System.Drawing.Size(35, 35);
            this.tbtDel.Text = "删除";
            this.tbtDel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbtDel.Click += new System.EventHandler(this.tbtDel_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 38);
            // 
            // tbtUp
            // 
            this.tbtUp.Image = global::ErpCore.Properties.Resources.up;
            this.tbtUp.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbtUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtUp.Name = "tbtUp";
            this.tbtUp.Size = new System.Drawing.Size(35, 35);
            this.tbtUp.Text = "向上";
            this.tbtUp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbtUp.Click += new System.EventHandler(this.tbtUp_Click);
            // 
            // tbtDown
            // 
            this.tbtDown.Image = global::ErpCore.Properties.Resources.down;
            this.tbtDown.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbtDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtDown.Name = "tbtDown";
            this.tbtDown.Size = new System.Drawing.Size(35, 35);
            this.tbtDown.Text = "向下";
            this.tbtDown.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbtDown.Click += new System.EventHandler(this.tbtDown_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colName,
            this.colType,
            this.colLen,
            this.colAllowNull,
            this.colIsSystem,
            this.colIsUnique});
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(0, 42);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 23;
            this.dataGridView.Size = new System.Drawing.Size(788, 215);
            this.dataGridView.TabIndex = 2;
            this.dataGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellEndEdit);
            this.dataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellClick);
            this.dataGridView.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView_EditingControlShowing);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 38);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridView);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl);
            this.splitContainer1.Size = new System.Drawing.Size(792, 528);
            this.splitContainer1.SplitterDistance = 261;
            this.splitContainer1.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ckIsSystem);
            this.panel1.Controls.Add(this.btSetDataServer);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtTableCode);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtTableName);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(788, 42);
            this.panel1.TabIndex = 3;
            // 
            // ckIsSystem
            // 
            this.ckIsSystem.AutoSize = true;
            this.ckIsSystem.Location = new System.Drawing.Point(688, 15);
            this.ckIsSystem.Name = "ckIsSystem";
            this.ckIsSystem.Size = new System.Drawing.Size(60, 16);
            this.ckIsSystem.TabIndex = 6;
            this.ckIsSystem.Text = "系统表";
            this.ckIsSystem.UseVisualStyleBackColor = true;
            // 
            // btSetDataServer
            // 
            this.btSetDataServer.Location = new System.Drawing.Point(564, 14);
            this.btSetDataServer.Name = "btSetDataServer";
            this.btSetDataServer.Size = new System.Drawing.Size(99, 23);
            this.btSetDataServer.TabIndex = 5;
            this.btSetDataServer.Text = "数据服务器";
            this.btSetDataServer.UseVisualStyleBackColor = true;
            this.btSetDataServer.Click += new System.EventHandler(this.btSetDataServer_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(439, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "(生成数据库表名)";
            // 
            // txtTableCode
            // 
            this.txtTableCode.Location = new System.Drawing.Point(296, 15);
            this.txtTableCode.Name = "txtTableCode";
            this.txtTableCode.Size = new System.Drawing.Size(136, 21);
            this.txtTableCode.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(248, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "编码：";
            // 
            // txtTableName
            // 
            this.txtTableName.Location = new System.Drawing.Point(72, 14);
            this.txtTableName.Name = "txtTableName";
            this.txtTableName.Size = new System.Drawing.Size(136, 21);
            this.txtTableName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "名称：";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(408, 259);
            this.tabControl.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.propertyGrid);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(400, 233);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "列";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // propertyGrid
            // 
            this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid.HelpVisible = false;
            this.propertyGrid.Location = new System.Drawing.Point(3, 3);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.propertyGrid.Size = new System.Drawing.Size(394, 227);
            this.propertyGrid.TabIndex = 0;
            this.propertyGrid.ToolbarVisible = false;
            this.propertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid_PropertyValueChanged);
            // 
            // colName
            // 
            this.colName.HeaderText = "列名";
            this.colName.Name = "colName";
            // 
            // colType
            // 
            this.colType.HeaderText = "数据类型";
            this.colType.Items.AddRange(new object[] {
            "字符型",
            "整型",
            "长整型",
            "布尔型",
            "数值型",
            "日期型",
            "备注型",
            "二进制",
            "引用型",
            "GUID",
            "枚举型",
            "附件型"});
            this.colType.Name = "colType";
            // 
            // colLen
            // 
            this.colLen.HeaderText = "长度";
            this.colLen.Name = "colLen";
            // 
            // colAllowNull
            // 
            this.colAllowNull.HeaderText = "允许空";
            this.colAllowNull.Name = "colAllowNull";
            // 
            // colIsSystem
            // 
            this.colIsSystem.HeaderText = "系统字段";
            this.colIsSystem.Name = "colIsSystem";
            // 
            // colIsUnique
            // 
            this.colIsUnique.HeaderText = "唯一性";
            this.colIsUnique.Name = "colIsUnique";
            // 
            // TableInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 566);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip);
            this.Name = "TableInfoForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "表";
            this.Load += new System.EventHandler(this.TableInfoForm_Load);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.ToolStripButton tbtSave;
        private System.Windows.Forms.ToolStripButton tbtCancel;
        private System.Windows.Forms.ToolStripButton tbtDel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTableCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTableName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripButton tbtUp;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tbtDown;
        private System.Windows.Forms.Button btSetDataServer;
        private System.Windows.Forms.CheckBox ckIsSystem;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewComboBoxColumn colType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLen;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colAllowNull;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colIsSystem;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colIsUnique;
    }
}