using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Reflection;
using System.IO;
//using Microsoft.Office.Core;

using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.Invoicing;
using ErpCoreModel.Store;

public partial class Invoicing_KC_StorageNoteImport : System.Web.UI.Page
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
        if (FileUpload1.PostedFile.ContentLength == 0)
        {
            RegisterStartupScript("starup", "<script>alert('请选择文件');</script>");
            return;
        }
        string sPath = Server.MapPath("Import.aspx");
        sPath = sPath.Substring(0, sPath.Length - "Import.aspx".Length);
        sPath += "import\\";
        if (!Directory.Exists(sPath))
            Directory.CreateDirectory(sPath);
        string ext = FileUpload1.FileName.Substring(FileUpload1.FileName.IndexOf('.'));
        DateTime dtime = DateTime.Now;
        string sFileName = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}",
            dtime.Year, dtime.Month, dtime.Day, dtime.Hour, dtime.Minute, dtime.Second, dtime.Millisecond,
            ext);
        FileUpload1.PostedFile.SaveAs(sPath + sFileName);

        bool bRet = ImportExcel(sPath + sFileName);
        //垃圾回收
        GC.Collect();

        if (bRet)
        {
            RegisterStartupScript("starup", "<script>parent.$.ligerDialog.success('导入完成');parent.grid.loadData(true);parent.$.ligerDialog.close();</script>");
        }
        else
        {
            RegisterStartupScript("starup", "<script>parent.$.ligerDialog.warn('导入失败');</script>");
        }
    }
    bool ImportExcel(string sFile)
    {
        string connStr = "Provider=Microsoft.Jet.OLEDB.4.0;" +
        "Extended Properties='Excel 8.0;HDR=YES;IMEX=1';" +
        "data source=" + sFile;

        if (!ValidateExcel(connStr))
            return false;
        ImportExcelRecord(connStr);

        return true;
    }
    bool ValidateExcel(string connStr)
    {
        // 主表
        string sql = "SELECT * FROM [Sheet1$]";
        DataSet ds = new DataSet();
        OleDbDataAdapter da = new OleDbDataAdapter(sql, connStr);
        da.Fill(ds); // 填充DataSet 
        DataTable dt = ds.Tables[0];

        string sMsg = "";
        if(dt.Rows.Count==0)
        {
            sMsg = string.Format("主表内容不全！");
            RegisterStartupScript("starup",string.Format("<script>parent.$.ligerDialog.warn('{0}');</script>",sMsg));
            return false;
        }
        {
            DataRow r = dt.Rows[0];
            string sFieldVal1 = r["编号"].ToString().Trim();
            string sFieldVal2 = r["入库日期"].ToString().Trim();
            string sFieldVal3 = r["公司"].ToString().Trim();
            string sFieldVal4 = r["经办人"].ToString().Trim();
            string sFieldVal5 = r["备注"].ToString().Trim();

            if (sFieldVal1=="")
            {
                sMsg = string.Format("{0} 行编号不能空！", 1);
                RegisterStartupScript("starup",string.Format("<script>parent.$.ligerDialog.warn('{0}');</script>",sMsg));
                return false;
            }
            if (Global.GetInvoicing().StorageNoteMgr.FindByCode(sFieldVal1) != null)
            {
                sMsg = string.Format("{0} 行编号 {1} 重复！",1, sFieldVal1);
                RegisterStartupScript("starup",string.Format("<script>parent.$.ligerDialog.warn('{0}');</script>",sMsg));
                return false;
            }
        }

        //明细
        string sql2 = "SELECT * FROM [Sheet2$]";
        DataSet ds2 = new DataSet();
        OleDbDataAdapter da2 = new OleDbDataAdapter(sql2, connStr);
        da2.Fill(ds2); // 填充DataSet 
        DataTable dt2 = ds2.Tables[0];
        
        for (int i=0;i<dt2.Rows.Count;i++)
        {
            DataRow r2=dt2.Rows[i];
            string sFieldVal21 = r2["商品编号"].ToString().Trim();
            string sFieldVal22 = r2["规格型号"].ToString().Trim();
            string sFieldVal23 = r2["数量"].ToString().Trim();

            CProduct Product = Global.GetStore().ProductMgr.FindByCode(sFieldVal21);
            if (Product == null)
            {
                sMsg = string.Format("{0} 行商品编号 {1} 不存在！", i + 1, sFieldVal21);
                RegisterStartupScript("starup", string.Format("<script>parent.$.ligerDialog.warn('{0}');</script>", sMsg));
                return false;
            }
            if (!Util.IsNum(sFieldVal23))
            {
                sMsg = string.Format("{0} 行数量错误！", i + 1, sFieldVal21);
                RegisterStartupScript("starup", string.Format("<script>parent.$.ligerDialog.warn('{0}');</script>", sMsg));
                return false;
            }
        }

        return true;
    }
    bool ImportExcelRecord(string connStr)
    {
        // 主表
        string sql = "SELECT * FROM [Sheet1$]";
        DataSet ds = new DataSet();
        OleDbDataAdapter da = new OleDbDataAdapter(sql, connStr);
        da.Fill(ds); // 填充DataSet 
        DataTable dt = ds.Tables[0];

        DataRow r = dt.Rows[0];
        string sFieldVal1 = r["编号"].ToString().Trim();
        string sFieldVal2 = r["入库日期"].ToString().Trim();
        string sFieldVal3 = r["公司"].ToString().Trim();
        string sFieldVal4 = r["经办人"].ToString().Trim();
        string sFieldVal5 = r["备注"].ToString().Trim();

        DateTime dtimeStoreDate = DateTime.Now;
        if(sFieldVal2!="")
        {
            try{dtimeStoreDate = Convert.ToDateTime(sFieldVal2);}
            catch{}
        }
        Guid guidCompanyId = m_User.B_Company_id;
        if(sFieldVal3!="")
        {
            CCompany Company = Global.GetCtx(Session["TopCompany"].ToString()).CompanyMgr.FindByName(sFieldVal3);
            if(Company!=null)
                guidCompanyId = Company.Id;
        }

        CStorageNote obj = new CStorageNote();
        obj.Ctx = Global.GetCtx(Session["TopCompany"].ToString());
        obj.Code = sFieldVal1;
        obj.StoreDate = dtimeStoreDate;
        obj.B_Company_id = guidCompanyId;
        obj.Attn = sFieldVal4;
        obj.Remarks = sFieldVal5;

        Global.GetInvoicing().StorageNoteMgr.AddNew(obj);


        //明细
        string sql2 = "SELECT * FROM [Sheet2$]";
        DataSet ds2 = new DataSet();
        OleDbDataAdapter da2 = new OleDbDataAdapter(sql2, connStr);
        da2.Fill(ds2); // 填充DataSet 
        DataTable dt2 = ds2.Tables[0];

        foreach (DataRow r2 in dt2.Rows)
        {
            string sFieldVal21 = r2["商品编号"].ToString().Trim();
            string sFieldVal22 = r2["规格型号"].ToString().Trim();
            string sFieldVal23 = r2["数量"].ToString().Trim();

            CProduct Product = Global.GetStore().ProductMgr.FindByCode(sFieldVal21);
            if (Product == null) continue;

            CStorageNoteDetail objDetail = new CStorageNoteDetail();
            objDetail.Ctx = Global.GetCtx(Session["TopCompany"].ToString());
            objDetail.SP_Product_id = Product.Id;
            objDetail.Specification = sFieldVal22;
            objDetail.Num = Convert.ToDouble(sFieldVal23);
            objDetail.KC_StorageNote_id = obj.Id;

            obj.StorageNoteDetailMgr.AddNew(objDetail);
        }

        if (!Global.GetInvoicing().StorageNoteMgr.Save(true))
        {
            return false;
        }

        return true;
    }
}
