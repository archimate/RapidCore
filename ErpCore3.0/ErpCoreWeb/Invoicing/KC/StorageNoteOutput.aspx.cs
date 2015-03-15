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

public partial class Invoicing_KC_StorageNoteOutput : System.Web.UI.Page
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
        CStorageNote StorageNote = (CStorageNote)Global.GetInvoicing().StorageNoteMgr.Find(new Guid(id));
        if (StorageNote == null)
        {
            Response.Write("请选择一项！");
            return;
        }
        OutputExcel(StorageNote);
    }
    void OutputExcel(CStorageNote StorageNote)
    {
        DateTime dtNow = DateTime.Now;
        string sNewFile = string.Format("{0}{1}{2}{3}{4}{5}.xls",
            dtNow.Year, dtNow.Month, dtNow.Day,
            dtNow.Hour, dtNow.Minute, dtNow.Second);

        string sDir = Server.MapPath("StorageNoteOutput.aspx");
        sDir = sDir.Substring(0, sDir.Length - "StorageNoteOutput.aspx".Length);
        string sDir1 = sDir + "output/";
        if (!Directory.Exists(sDir1))
            Directory.CreateDirectory(sDir1);
        string sFileSrc = sDir + "templet/StorageNote.xls";
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
        string sIns = string.Format("insert into [Sheet1$] ([编号],[入库日期],[公司],[经办人],[备注]) values (?,?,?,?,?)");
        CCompany Company = (CCompany)Global.GetCtx(Session["TopCompany"].ToString()).CompanyMgr.Find(m_User.B_Company_id);
        OleDbCommand cmd = new OleDbCommand(sIns,conn);
        cmd.Parameters.Add(new OleDbParameter("编号", StorageNote.Code));
        cmd.Parameters.Add(new OleDbParameter("入库日期", StorageNote.StoreDate));
        cmd.Parameters.Add(new OleDbParameter("公司", Company.Name));
        cmd.Parameters.Add(new OleDbParameter("经办人", StorageNote.Attn));
        cmd.Parameters.Add(new OleDbParameter("备注", StorageNote.Remarks));
        try { cmd.ExecuteNonQuery(); }
        catch
        {
            conn.Close();
            Response.Write("导出失败！");
            return;
        }
        //从表
        List<CBaseObject> lstObj = StorageNote.StorageNoteDetailMgr.GetList();
        foreach (CBaseObject obj in lstObj)
        {
            CStorageNoteDetail StorageNoteDetail = (CStorageNoteDetail)obj;
            string sIns2 = string.Format("insert into [Sheet2$] ([商品编号],[规格型号],[数量]) values (?,?,?)");
            CProduct Product = (CProduct)Global.GetStore().ProductMgr.Find(StorageNoteDetail.SP_Product_id);
            if (Product == null) continue;
            OleDbCommand cmd2 = new OleDbCommand(sIns2, conn);
            cmd2.Parameters.Add(new OleDbParameter("商品编号", Product.Code));
            cmd2.Parameters.Add(new OleDbParameter("规格型号", StorageNoteDetail.Specification));
            cmd2.Parameters.Add(new OleDbParameter("数量", StorageNoteDetail.Num));
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
