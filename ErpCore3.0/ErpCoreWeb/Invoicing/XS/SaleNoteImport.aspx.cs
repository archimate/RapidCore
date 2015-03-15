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

public partial class Invoicing_XS_SaleNoteImport : System.Web.UI.Page
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
            string sFieldVal2 = r["销售日期"].ToString().Trim();
            string sFieldVal3 = r["客户名称"].ToString().Trim();
            string sFieldVal4 = r["联系人"].ToString().Trim();
            string sFieldVal5 = r["联系电话"].ToString().Trim();
            string sFieldVal6 = r["送货地址"].ToString().Trim();
            string sFieldVal7 = r["其他费用"].ToString().Trim();
            string sFieldVal8 = r["运费"].ToString().Trim();
            string sFieldVal9 = r["优惠"].ToString().Trim();
            string sFieldVal10 = r["公司"].ToString().Trim();
            string sFieldVal11 = r["经办人"].ToString().Trim();
            string sFieldVal12 = r["备注"].ToString().Trim();

            if (sFieldVal1=="")
            {
                sMsg = string.Format("{0} 行编号不能空！", 1);
                RegisterStartupScript("starup",string.Format("<script>parent.$.ligerDialog.warn('{0}');</script>",sMsg));
                return false;
            }
            if (Global.GetInvoicing().SaleNoteMgr.FindByCode(sFieldVal1) != null)
            {
                sMsg = string.Format("{0} 行编号 {1} 重复！",1, sFieldVal1);
                RegisterStartupScript("starup",string.Format("<script>parent.$.ligerDialog.warn('{0}');</script>",sMsg));
                return false;
            }
            if (sFieldVal7 != "" && (!Util.IsNum(sFieldVal7)))
            {
                sMsg = string.Format("其他费用格式错误！");
                RegisterStartupScript("starup", string.Format("<script>parent.$.ligerDialog.warn('{0}');</script>", sMsg));
                return false;
            }
            if (sFieldVal8 != "" && (!Util.IsNum(sFieldVal8)))
            {
                sMsg = string.Format("运费格式错误！");
                RegisterStartupScript("starup", string.Format("<script>parent.$.ligerDialog.warn('{0}');</script>", sMsg));
                return false;
            }
            if (sFieldVal9 != "" && (!Util.IsNum(sFieldVal9)))
            {
                sMsg = string.Format("优惠格式错误！");
                RegisterStartupScript("starup", string.Format("<script>parent.$.ligerDialog.warn('{0}');</script>", sMsg));
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
            string sFieldVal24 = r2["单价"].ToString().Trim();
            string sFieldVal25 = r2["折扣"].ToString().Trim();
            string sFieldVal26 = r2["赠送商品"].ToString().Trim();
            string sFieldVal27 = r2["换货"].ToString().Trim();

            CProduct Product = Global.GetStore().ProductMgr.FindByCode(sFieldVal21);
            if (Product == null)
            {
                sMsg = string.Format("{0} 行商品编号 {1} 不存在！", i + 1, sFieldVal21);
                RegisterStartupScript("starup", string.Format("<script>parent.$.ligerDialog.warn('{0}');</script>", sMsg));
                return false;
            }
            if (!Util.IsNum(sFieldVal23))
            {
                sMsg = string.Format("{0} 行数量错误！", i + 1);
                RegisterStartupScript("starup", string.Format("<script>parent.$.ligerDialog.warn('{0}');</script>", sMsg));
                return false;
            }
            if (!Util.IsNum(sFieldVal24))
            {
                sMsg = string.Format("{0} 行单价错误！", i + 1);
                RegisterStartupScript("starup", string.Format("<script>parent.$.ligerDialog.warn('{0}');</script>", sMsg));
                return false;
            }
            if (sFieldVal25 != "" && !Util.IsNum(sFieldVal25))
            {
                sMsg = string.Format("{0} 行折扣错误！", i + 1);
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
        string sFieldVal2 = r["销售日期"].ToString().Trim();
        string sFieldVal3 = r["客户名称"].ToString().Trim();
        string sFieldVal4 = r["联系人"].ToString().Trim();
        string sFieldVal5 = r["联系电话"].ToString().Trim();
        string sFieldVal6 = r["送货地址"].ToString().Trim();
        string sFieldVal7 = r["其他费用"].ToString().Trim();
        string sFieldVal8 = r["运费"].ToString().Trim();
        string sFieldVal9 = r["优惠"].ToString().Trim();
        string sFieldVal10 = r["公司"].ToString().Trim();
        string sFieldVal11 = r["经办人"].ToString().Trim();
        string sFieldVal12 = r["备注"].ToString().Trim();

        DateTime dtimeSaleDate = DateTime.Now;
        if(sFieldVal2!="")
        {
            try { dtimeSaleDate = Convert.ToDateTime(sFieldVal2); }
            catch{}
        }
        Guid guidCompanyId = m_User.B_Company_id;
        if(sFieldVal10!="")
        {
            CCompany Company = Global.GetCtx(Session["TopCompany"].ToString()).CompanyMgr.FindByName(sFieldVal10);
            if(Company!=null)
                guidCompanyId = Company.Id;
        }

        CSaleNote obj = new CSaleNote();
        obj.Ctx = Global.GetCtx(Session["TopCompany"].ToString());
        obj.Code = sFieldVal1;
        obj.SaleDate = dtimeSaleDate;
        obj.Customer = sFieldVal3;
        obj.Contacts = sFieldVal4;
        obj.Tel = sFieldVal5;
        obj.Addr = sFieldVal6;
        obj.OtherCharge = Convert.ToDouble(sFieldVal7);
        obj.ShipCharge = Convert.ToDouble(sFieldVal8);
        obj.Discount = Convert.ToDouble(sFieldVal9);
        obj.B_Company_id = guidCompanyId;
        obj.Attn = sFieldVal11;
        obj.Remarks = sFieldVal12;

        Global.GetInvoicing().SaleNoteMgr.AddNew(obj);


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
            string sFieldVal24 = r2["单价"].ToString().Trim();
            string sFieldVal25 = r2["折扣"].ToString().Trim();
            string sFieldVal26 = r2["赠送商品"].ToString().Trim();
            string sFieldVal27 = r2["换货"].ToString().Trim();

            CProduct Product = Global.GetStore().ProductMgr.FindByCode(sFieldVal21);
            if (Product == null) continue;

            CSaleNoteDetail objDetail = new CSaleNoteDetail();
            objDetail.Ctx = Global.GetCtx(Session["TopCompany"].ToString());
            objDetail.SP_Product_id = Product.Id;
            objDetail.Specification = sFieldVal22;
            objDetail.Num = Convert.ToDouble(sFieldVal23);
            objDetail.Price = Convert.ToDouble(sFieldVal24);
            if(sFieldVal25!="")
                objDetail.Discount = Convert.ToDouble(sFieldVal25);
            objDetail.IsGive = (sFieldVal26 != "是") ? true : false;
            objDetail.IsExchange = (sFieldVal27 != "是") ? true : false;
            objDetail.XS_SaleNote_id = obj.Id;

            obj.SaleNoteDetailMgr.AddNew(objDetail);
        }

        if (!Global.GetInvoicing().SaleNoteMgr.Save(true))
        {
            return false;
        }

        return true;
    }
}
