using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;

using System.Text;
using System.Windows.Forms;

namespace ErpCore.Window.Designer
{
    [ToolboxItem(false)] 
    public partial class UISizeDot : UserControl
    {
        private bool _movable= false;
        private Pen pen = new Pen(Color.Red);

        bool m_bIsMouseDown = false;
        Point m_ptStart = new Point(0, 0);
        Point m_ptEnd = new Point(0, 0);
        public int m_dx = 0;
        public int m_dy = 0;

        public bool m_bIsDragging = false;

        public UISizeDot()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            this.TabStop = false;
            this._movable = true;
        }

        /// <summary> 
        /// UISizeDot的边框颜色 
        /// </summary> 
        public Color BorderColor
        {
            get { return pen.Color; }
            set
            {
                this.pen = new Pen(value);
                this.Refresh();
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            // TODO: 在此处添加自定义绘制代码 
            //this.BackColor = Color.White; 
            pe.Graphics.DrawRectangle(pen, 0, 0, this.Width - 1, this.Height - 1);

            // 调用基类 OnPaint 
            base.OnPaint(pe);
        }

        public bool Movable
        {
            get { return this._movable; }
            set { this._movable = value; }
        }

        private void UISizeDot_MouseDown(object sender, MouseEventArgs e)
        {
            m_bIsMouseDown = true;
            m_ptStart = e.Location;
        }

        private void UISizeDot_MouseMove(object sender, MouseEventArgs e)
        {
            if (Movable && m_bIsMouseDown)
            {
                m_bIsDragging = true;
                m_ptEnd = e.Location;

                m_dx = e.Location.X - m_ptStart.X;
                m_dy = e.Location.Y - m_ptStart.Y;

                if (this.Cursor == Cursors.SizeNWSE)
                {
                    this.Left += m_dx;
                    this.Top += m_dy;
                }
                else if (this.Cursor == Cursors.SizeNS)
                {
                    this.Top += m_dy;
                }
                else if (this.Cursor == Cursors.SizeNESW)
                {
                    this.Left += m_dx;
                    this.Top += m_dy;
                }
                else if (this.Cursor == Cursors.SizeWE)
                {
                    this.Left += m_dx;
                }

                m_ptStart = e.Location;
                //m_dx = 0;
                //m_dy = 0;
            }
        }

        private void UISizeDot_MouseUp(object sender, MouseEventArgs e)
        {
            m_bIsMouseDown = false;
            m_bIsDragging = false;
        } 

    }
}
