using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ErpCoreModel.Framework;
using ErpCoreModel.Base;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        btLogin_Click(null, null);
    }

    protected void btLogin_Click(object sender, EventArgs e)
    {
        //CCompany Company = Global.GetCtx().CompanyMgr.FindByName(txtCompany.Text.Trim());
        //if (Company == null)
        //{
        //    RegisterStartupScript("starup", "<script>alert('单位不存在！');</script>");
        //    return;
        //}

        //CContext ctx = Global.GetCtx(txtCompany.Text.Trim());
        CContext ctx = Global.GetCtx();
        if (ctx == null)
        {
            RegisterStartupScript("starup", "<script>alert('单位没有注册！');</script>");
            return;
        }
        CUser user = ctx.UserMgr.FindByName(txtName.Text.Trim());
        if (user == null)
        {
            RegisterStartupScript("starup", "<script>alert('用户不存在！');</script>");
            return;
        }
        if (user.Pwd != txtPwd.Text)
        {
            RegisterStartupScript("starup", "<script>alert('密码不正确！');</script>");
            return;
        }

        //Session["TopCompany"] = txtCompany.Text.Trim();
        Session["TopCompany"] = ctx.CompanyMgr.FindTopCompany().Name;
        Session["User"] = user;

        //Response.Write("<script>parent.window.location.reload();</script>");
        //Response.Redirect("AdminForm.aspx");
        Response.Redirect("Desktop/Desktop.aspx");
    }
}
