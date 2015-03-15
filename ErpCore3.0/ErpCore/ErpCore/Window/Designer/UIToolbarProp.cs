using System;
using System.Collections.Generic;

using System.Text;
using System.ComponentModel;
using System.Windows.Forms;

namespace ErpCore.Window.Designer
{
    class UIToolbarProp
    {

        private string name;
        [CategoryAttribute("通用"),
        ReadOnlyAttribute(false),
        DescriptionAttribute("名称")]
        public string 名称
        {
            get { return name; }
            set { name = value; }
        }

        private DockStyle dock;
        [CategoryAttribute("位置"),
        ReadOnlyAttribute(false),
        DescriptionAttribute("停靠")]
        public DockStyle 停靠
        {
            get { return dock; }
            set { dock = value; }
        }


        private int width;
        [CategoryAttribute("位置"),
        ReadOnlyAttribute(true),
        DescriptionAttribute("宽度")]
        public int 宽度
        {
            get { return width; }
            set { width = value; }
        }

        private int height;
        [CategoryAttribute("位置"),
        ReadOnlyAttribute(true),
        DescriptionAttribute("高度")]
        public int 高度
        {
            get { return height; }
            set { height = value; }
        }


    }
    
}
