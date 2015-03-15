using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.Invoicing;
using ErpCoreModel.Store;

public partial class Invoicing_CG_PurchaseOrder2Note : System.Web.UI.Page
{
    CUser m_User = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null)
        {
            Response.Write("<script>window.top.location='../../login.aspx'</script>");
            Response.End();
        }
        m_User = (CUser)Session["User"];
        

    }
    protected void btOk_Click(object sender, EventArgs e)
    {
        string sOrderId = hidOrderId.Value;
        string sNoteCode = txtNoteCode.Value;
        if(Global.GetInvoicing().PurchaseNoteMgr.FindByCode(sNoteCode)!=null)
        {
            Response.Write(string.Format("<script>parent.$.ligerDialog.warn('{0} 编号重复！');</script>", sNoteCode));
            return ;
        }
        CPurchaseOrder PurchaseOrder = (CPurchaseOrder)Global.GetInvoicing().PurchaseOrderMgr.Find(new Guid(sOrderId));
        CPurchaseNote PurchaseNote = new CPurchaseNote();
        PurchaseNote.Ctx = Global.GetCtx(Session["TopCompany"].ToString());
        PurchaseNote.Code = sNoteCode;
        PurchaseNote.PurchaseDate = PurchaseOrder.PurchaseDate;
        PurchaseNote.Supplier = PurchaseOrder.Supplier;
        PurchaseNote.Contacts = PurchaseOrder.Contacts;
        PurchaseNote.Tel = PurchaseOrder.Tel;
        PurchaseNote.Addr = PurchaseOrder.Addr;
        PurchaseNote.OtherCharge = PurchaseOrder.OtherCharge;
        PurchaseNote.Discount = PurchaseOrder.Discount;
        PurchaseNote.B_Company_id = PurchaseOrder.B_Company_id;
        PurchaseNote.Attn = PurchaseOrder.Attn;
        PurchaseNote.Remarks = PurchaseOrder.Remarks;

        Global.GetInvoicing().PurchaseNoteMgr.AddNew(PurchaseNote);
        if (!Global.GetInvoicing().PurchaseNoteMgr.Save(true))
        {
            Response.Write(string.Format("<script>parent.$.ligerDialog.warn('生成失败！');</script>"));
            return;
        }
        Response.Write(string.Format("<script>parent.$.ligerDialog.success('生成成功！', function(type) {{ parent.$.ligerDialog.close(); }});</script>"));
    }
}
