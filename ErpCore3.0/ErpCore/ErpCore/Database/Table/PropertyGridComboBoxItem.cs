using System;
using System.Collections.Generic;
using System.Collections;

using System.Text;
using System.Windows.Forms;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.ComponentModel;

namespace ErpCore.Database.Table
{
    public abstract class ComboBoxItemTypeConvert : TypeConverter
    {
        public static Hashtable _hash = new Hashtable();

        public ComboBoxItemTypeConvert()
        {
            GetConvertHash();
        }

        public abstract void GetConvertHash();
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            string[] ids = new string[_hash.Values.Count];
            int i = 0;
            foreach (DictionaryEntry myDE in _hash)
            {
                ids[i++] = myDE.Key.ToString();
            }
            return new StandardValuesCollection(ids);
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object v)
        {
            if (v is string)
            {
                foreach (DictionaryEntry myDE in _hash)
                {
                    if (myDE.Value.Equals((v.ToString())))
                        return myDE.Key;
                }
            }

            return base.ConvertFrom(context, culture, v);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture,object v, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                foreach (DictionaryEntry myDE in _hash)
                {
                    if (myDE.Key.Equals(v))
                        return myDE.Value.ToString();
                }
                return "";
            }

            return base.ConvertTo(context, culture, v, destinationType);
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return false;
        }

    }

    class PropertyGridComboBoxItem : ComboBoxItemTypeConvert
    {
        public override void GetConvertHash()
        {
            //_hash.Add("0", "炒肝");
            //_hash.Add("1", "豆汁");
            //_hash.Add("2", "灌肠");
        }
    }
}
