﻿namespace ErpCore.CommonCtrl
{
    partial class ExComboBox
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
            this.comboBox = new System.Windows.Forms.ComboBox();
            this.lbCaption = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBox
            // 
            this.comboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox.FormattingEnabled = true;
            this.comboBox.Location = new System.Drawing.Point(80, 0);
            this.comboBox.Name = "comboBox";
            this.comboBox.Size = new System.Drawing.Size(158, 20);
            this.comboBox.TabIndex = 3;
            // 
            // lbCaption
            // 
            this.lbCaption.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbCaption.Location = new System.Drawing.Point(0, 0);
            this.lbCaption.Name = "lbCaption";
            this.lbCaption.Size = new System.Drawing.Size(80, 22);
            this.lbCaption.TabIndex = 4;
            this.lbCaption.Text = "标题：";
            this.lbCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ExComboBox
            // 
            this.Controls.Add(this.comboBox);
            this.Controls.Add(this.lbCaption);
            this.Name = "ExComboBox";
            this.Size = new System.Drawing.Size(238, 22);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ComboBox comboBox;
        public System.Windows.Forms.Label lbCaption;
    }
}
