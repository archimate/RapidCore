using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.IO;

using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.Invoicing;
using ErpCoreModel.Store;

public partial class Invoicing_XS_SaleNoteOutput : System.Web.UI.Page
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
        
        if (Request.Params["Action"] == "PostData")
        {
            PostData();
            Response.End();
        }
    }
    void PostData()
    {
        string id = Request["id"];
        if (string.IsNullOrEmpty(id))
        {
            Response.Write("请选择一项！");
            return;
        }
        CSaleNote SaleNote = (CSaleNote)Global.GetInvoicing().SaleNoteMgr.Find(new Guid(id));
        if (SaleNote == null)
        {
            Response.Write("请选择一项！");
            return;
        }
        OutputExcel(SaleNote);
    }
    void OutputExcel(CSaleNote SaleNote)
    {
        DateTime dtNow = DateTime.Now;
        string sNewFile = string.Format("{0}{1}{2}{3}{4}{5}.xls",
            dtNow.Year, dtNow.Month, dtNow.Day,
            dtNow.Hour, dtNow.Minute, dtNow.Second);

        string sDir = Server.MapPath("SaleNoteOutput.aspx");
        sDir = sDir.Substring(0, sDir.Length - "SaleNoteOutput.aspx".Length);
        string sDir1 = sDir + "output/";
        if (!Directory.Exists(sDir1))
            Directory.CreateDirectory(sDir1);
        string sFileSrc = sDir + "templet/SaleNote.xls";
        string sFileDst = sDir1 + sNewFile;
        if (File.Exists(sFileDst))
            File.Delete(sFileDst);
        File.Copy(sFileSrc, sFileDst);

        string connStr = "Provider=Microsoft.Jet.OLEDB.4.0;" +
        "Extended Properties='Excel 8.0;HDR=YES;IMEX=0';" +
        "data source=" + sFileDst;

        OleDbConnection conn = new OleDbConnection(connStr);
        try { conn.Open(); }
        catch
        {
            Response.Write("导出失败！");
            return;
        }
        //主表
        string sIns = string.Format("insert into [Sheet1$] ([编号],[销售日期],[供应商],[联系人],[联系电话],[送货地址],[其他费用],[运费],[优惠],[公司],[经办人],[备注]) values (?,?,?,?,?,?,?,?,?,?,?,?)");
        CCompany Company = (CCompany)Global.GetCtx(Session["TopCompany"].ToString()).CompanyMgr.Find(m_User.B_Company_id);
        OleDbCommand cmd = new OleDbCommand(sIns,conn);
        cmd.Parameters.Add(new OleDbParameter("编号", SaleNote.Code));
        cmd.Parameters.Add(new OleDbParameter("销售日期", SaleNote.SaleDate));
        cmd.Parameters.Add(new OleDbParameter("客户名称", SaleNote.Customer));
        cmd.Parameters.Add(new OleDbParameter("联系人", SaleNote.Contacts));
        cmd.Parameters.Add(new OleDbParameter("联系电话", SaleNote.Tel));
        cmd.Parameters.Add(new OleDbParameter("送货地址", SaleNote.Addr));
        cmd.Parameters.Add(new OleDbParameter("其他费用", SaleNote.OtherCharge));
        cmd.Parameters.Add(new OleDbParameter("运费", SaleNote.ShipCharge));
        cmd.Parameters.Add(new OleDbParameter("优惠", SaleNote.Discount));
        cmd.Parameters.Add(new OleDbParameter("公司", Company.Name));
        cmd.Parameters.Add(new OleDbParameter("经办人", SaleNote.Attn));
        cmd.Parameters.Add(new OleDbParameter("备注", SaleNote.Remarks));
        try { cmd.ExecuteNonQuery(); }
        catch
        {
            conn.Close();
            Response.Write("导出失败！");
            return;
        }
        //从表
        List<CBaseObject> lstObj = SaleNote.SaleNoteDetailMgr.GetList();
        foreach (CBaseObject obj in lstObj)
        {
            CSaleNoteDetail SaleNoteDetail = (CSaleNoteDetail)obj;
            string sIns2 = string.Format("insert into [Sheet2$] ([商品编号],[规格型号],[数量],[单价],[折扣],[赠送商品],[换货]) values (?,?,?,?,?,?,?)");
            CProduct Product = (CProduct)Global.GetStore().ProductMgr.Find(SaleNoteDetail.SP_Product_id);
            if (Product == null) continue;
            OleDbCommand cmd2 = new OleDbCommand(sIns2, conn);
            cmd2.Parameters.Add(new OleDbParameter("商品编号", Product.Code));
            cmd2.Parameters.Add(new OleDbParameter("规格型号", SaleNoteDetail.Specification));
            cmd2.Parameters.Add(new OleDbParameter("数量", SaleNoteDetail.Num));
            cmd2.Parameters.Add(new OleDbParameter("单价", SaleNoteDetail.Price));
            cmd2.Parameters.Add(new OleDbParameter("折扣", SaleNoteDetail.Discount));
            cmd2.Parameters.Add(new OleDbParameter("赠送商品", SaleNoteDetail.IsGive?"是":"否"));
            cmd2.Parameters.Add(new OleDbParameter("换货", SaleNoteDetail.IsExchange ? "是" : "否"));
            try { cmd2.ExecuteNonQuery(); }
            catch
            {
                conn.Close();
                Response.Write("导出失败！");
                return;
            }
        }
        conn.Close();

        Response.Write("OutXls.aspx?file=" + sNewFile);
    }
}
