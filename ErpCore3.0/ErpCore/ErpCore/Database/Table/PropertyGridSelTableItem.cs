using System;
using System.Collections.Generic;

using System.Text;
using System.Windows.Forms;
using System.Drawing.Design;
using System.Windows.Forms.Design; 


namespace ErpCore.Database.Table
{
    class PropertyGridSelTableItem : UITypeEditor 
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

                SelTableForm frm = new SelTableForm();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    return frm.m_SelTable.Code;
                }

            }

            return value;

        }

    }
}
