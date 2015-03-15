using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ErpCoreModel.Framework;
using ErpCoreModel.Base;

public partial class _ModPwd : System.Web.UI.Page
{
    public CUser m_User = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null)
        {
            Response.Write("<script>window.location='login.aspx'</script>");
            Response.End();
        }
        m_User = (CUser)Session["User"];

    }
    protected void btMod_Click(object sender, EventArgs e)
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
        if (m_User.Pwd != txtPwd.Text)
        {
            RegisterStartupScript("starup", "<script>alert('密码不正确！');</script>");
            return;
        }

        if (txtNewPwd.Text.Trim() != txtNewPwd2.Text.Trim())
        {
            RegisterStartupScript("starup", "<script>alert('密码不一致！');</script>");
            return;
        }
        m_User.Pwd = txtNewPwd.Text.Trim();
        m_User.m_ObjectMgr.Update(m_User);
        if (!m_User.m_ObjectMgr.Save(true))
        {
            RegisterStartupScript("starup", "<script>alert('修改失败！');</script>");
            return;
        }

        RegisterStartupScript("starup", "<script>alert('修改成功！');parent.$.ligerDialog.close();</script>");
        //Response.Redirect("AdminForm.aspx");
    }
}
