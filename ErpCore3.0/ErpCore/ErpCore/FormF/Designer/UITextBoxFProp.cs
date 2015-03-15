using System;
using System.Collections.Generic;

using System.Text;
using System.ComponentModel;
using System.Windows.Forms;

namespace ErpCore.FormF.Designer
{
    class UITextBoxFProp
    {
        private string name;
        [CategoryAttribute("通用"),
        DescriptionAttribute("名称")]
        public string 名称
        {
            get { return name; }
            set { name = value; }
        }

        private string text;
        [CategoryAttribute("通用"),
        DescriptionAttribute("文本")]
        public string 文本
        {
            get { return text; }
            set { text = value; }
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

    }
    
}
