using System;
using System.Collections.Generic;

using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using ErpCore.Database.Table;

namespace ErpCore.FormF.Designer
{
    class ExComboBoxProp
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


        private string refTable;
        [CategoryAttribute("字段属性"),
        ReadOnlyAttribute(false),
        EditorAttribute(typeof(PropertyGridSelTableItem), typeof(System.Drawing.Design.UITypeEditor)),
        DescriptionAttribute("引用表")]
        public string 引用表
        {
            get { return refTable; }
            set { refTable = value; }
        }

        private string refCol;
        [CategoryAttribute("字段属性"),
        ReadOnlyAttribute(false),
        TypeConverter(typeof(PropertyGridComboBoxItem)),
        DescriptionAttribute("引用字段")]
        public string 引用字段
        {
            get { return refCol; }
            set { refCol = value; }
        }

        private string refShowCol;
        [CategoryAttribute("字段属性"),
        ReadOnlyAttribute(false),
        TypeConverter(typeof(PropertyGridComboBoxItem)),
        DescriptionAttribute("引用显示字段")]
        public string 引用显示字段
        {
            get { return refShowCol; }
            set { refShowCol = value; }
        }
    }
    
}
