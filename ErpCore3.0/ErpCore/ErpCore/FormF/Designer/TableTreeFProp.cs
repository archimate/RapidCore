using System;
using System.Collections.Generic;

using System.Text;
using System.ComponentModel;
using System.Windows.Forms;

namespace ErpCore.FormF.Designer
{
    class TableTreeFProp
    {
        private string name;
        [CategoryAttribute("通用"),
        DescriptionAttribute("名称")]
        public string 名称
        {
            get { return name; }
            set { name = value; }
        }

        private int width;
        [CategoryAttribute("位置"),
        DescriptionAttribute("宽度")]
        public int 宽度
        {
            get { return width; }
            set { width = value; }
        }

        private int height;
        [CategoryAttribute("位置"),
        DescriptionAttribute("高度")]
        public int 高度
        {
            get { return height; }
            set { height = value; }
        }

        private bool showTitleBar;
        [CategoryAttribute("样式"),
        DescriptionAttribute("标题栏显示")]
        public bool 标题栏显示
        {
            get { return showTitleBar; }
            set { showTitleBar = value; }
        }


    }
    
}
