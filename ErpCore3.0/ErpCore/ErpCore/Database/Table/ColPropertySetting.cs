using System;
using System.Collections.Generic;

using System.Text;
using System.ComponentModel;

namespace ErpCore.Database.Table
{
    class ColPropertySetting
    {
        private string name;
        [CategoryAttribute("字段属性"),
        ReadOnlyAttribute(false),
        DescriptionAttribute("名称")]
        public string 名称
        {
            get { return name; }
            set { name = value; }
        }

        private string defaultVal;
        [CategoryAttribute("字段属性"),
        ReadOnlyAttribute(false),
        DescriptionAttribute("默认值")]
        public string 默认值
        {
            get { return defaultVal; }
            set { defaultVal = value; }
        }

        private int colDecimal;
        [CategoryAttribute("字段属性"),
        ReadOnlyAttribute(false),
        DescriptionAttribute("小数位数")]
        public int 小数位数
        {
            get { return colDecimal; }
            set { colDecimal = value; }
        }

        private string formula;
        [CategoryAttribute("字段属性"),
        ReadOnlyAttribute(false),
        DescriptionAttribute("公式")]
        public string 公式
        {
            get { return formula; }
            set { formula = value; }
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

        private string enumVal;
        [CategoryAttribute("字段属性"),
        ReadOnlyAttribute(false),
        EditorAttribute(typeof(PropertyGridEnumItem), typeof(System.Drawing.Design.UITypeEditor)),
        DescriptionAttribute("枚举值")]
        public string 枚举值
        {
            get { return enumVal; }
            set { enumVal = value; }
        }

        private string uIControl;
        [CategoryAttribute("字段属性"),
        ReadOnlyAttribute(false),
        DescriptionAttribute("界面控件")]
        public string 界面控件
        {
            get { return uIControl; }
            set { uIControl = value; }
        }

        private string webUIControl;
        [CategoryAttribute("字段属性"),
        ReadOnlyAttribute(false),
        DescriptionAttribute("web界面控件")]
        public string web界面控件
        {
            get { return webUIControl; }
            set { webUIControl = value; }
        }
    }
}
