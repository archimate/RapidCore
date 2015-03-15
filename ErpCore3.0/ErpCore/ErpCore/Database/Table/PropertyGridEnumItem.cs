using System;
using System.Collections.Generic;

using System.Text;
using System.Windows.Forms;
using System.Drawing.Design;
using System.Windows.Forms.Design; 


namespace ErpCore.Database.Table
{
    class PropertyGridEnumItem : UITypeEditor 
    {
        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {

            return UITypeEditorEditStyle.Modal;

        }



        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context,System.IServiceProvider provider,object value)
        {

            IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

            if (edSvc != null)
            {

                TextBox box = new TextBox();
                box.Multiline = true;
                box.Height = 100;
                box.ScrollBars = ScrollBars.Vertical;
                string sVal = "";
                if (value != null)
                {
                    sVal = value as string;
                    sVal = sVal.Replace("\\", "\r\n");
                    box.Text = sVal;
                }
                //box.Text = value as string;

                edSvc.DropDownControl(box);

                string sRet = box.Text.Replace("\r\n", "\\");
                return sRet;

            }

            return value;

        }

    }
}
