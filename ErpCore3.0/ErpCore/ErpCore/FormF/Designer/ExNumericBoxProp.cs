using System;
using System.Collections.Generic;

using System.Text;
using System.ComponentModel;
using System.Windows.Forms;

namespace ErpCore.FormF.Designer
{
    class ExNumericBoxProp
    {
        private string name;
        [CategoryAttribute("通用"),
        DescriptionAttribute("名称")]
        public string 名称
        {
            get { return name; }
            set { name = value; }
        }

        private string caption;
        [CategoryAttribute("通用"),
        DescriptionAttribute("标题")]
        public string 标题
        {
            get { return caption; }
            set { caption = value; }
        }

        private int length;
        [CategoryAttribute("格式"),
        DescriptionAttribute("长度")]
        public int 长度
        {
            get { return length; }
            set { length = value; }
        }


        private int decimallen;
        [CategoryAttribute("格式"),
        DescriptionAttribute("小数位数")]
        public int 小数位数
        {
            get { return decimallen; }
            set { decimallen = value; }
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
