using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ErpCoreModel.Framework;
using ErpCoreModel.Base;

public partial class reg : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btReg_Click(object sender, EventArgs e)
    {
        string sCompany = txtCompany.Text.Trim();
        if (sCompany == "")
        {
            RegisterStartupScript("starup", "<script>alert('单位名称不能空！');</script>");
            return;
        }
        if (sCompany.IndexOfAny(@".,\;|/?".ToCharArray()) > -1)
        {
            RegisterStartupScript("starup", "<script>alert('单位名称不能有特殊字符！');</script>");
            return;
        }
        if (Global.GetCtx().CompanyMgr.FindByName(sCompany) != null)
        {
            RegisterStartupScript("starup", "<script>alert('单位已经注册！');</script>");
            return;
        }

        CCompany Company = new CCompany();
        Company.Name = sCompany;
        Company.Addr = txtAddr.Text.Trim();
        Company.Zipcode = txtZipcode.Text.Trim();
        Company.Tel = txtTel.Text.Trim();
        Company.Contact = txtContact.Text.Trim();
        Company.Email = txtEmail.Text.Trim();
        Company.RegState = enumRegState.Reg;
        Company.ChargeDate = DateTime.Now;

        if (!Global.GetCtx().CompanyMgr.AddNew(Company, true))
        {
            RegisterStartupScript("starup", "<script>alert('注册失败！');</script>");
            return;
        }
        //创建数据库
        if (!Global.CreateDB(sCompany))
        {
            Global.GetCtx().CompanyMgr.Delete(Company, true);
            RegisterStartupScript("starup", "<script>alert('注册失败！');</script>");
            return;
        }
        //随机生成管理员密码
        CContext ctx = Global.GetCtx(sCompany);
        if (ctx == null)
        {
            Global.GetCtx().CompanyMgr.Delete(Company, true);
            RegisterStartupScript("starup", "<script>alert('注册失败！');</script>");
            return;
        }
        CUser user = ctx.UserMgr.FindByName("admin");
        if (user == null)
        {
            user = new CUser();
            user.Name = "admin";
            user.Pwd = Util.RandPwd(6);

            if (!ctx.UserMgr.AddNew(user, true))
            {
                Global.GetCtx().CompanyMgr.Delete(Company, true);
                RegisterStartupScript("starup", "<script>alert('注册失败！');</script>");
                return;
            }
        }
        else
        {
            user.Name = "admin";
            user.Pwd = Util.RandPwd(6);

            if (!ctx.UserMgr.Update(user, true))
            {
                Global.GetCtx().CompanyMgr.Delete(Company, true);
                RegisterStartupScript("starup", "<script>alert('注册失败！');</script>");
                return;
            }
        }

        Session["Company"] = sCompany;
        Session["User"] = user;

        Response.Redirect("regOk.aspx");
    }
}
