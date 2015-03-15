using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.Store;

public partial class Store_modpwd : System.Web.UI.Page
{
    public CCustomer m_Customer = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Customer"] == null)
        {
            Response.Redirect("login.aspx");
            return;
        }

        m_Customer = (CCustomer)Session["Customer"];

        if (!IsPostBack)
        {
            txtName.Text = m_Customer.Name;
        }
    }

    protected void imgSave_Click(object sender, ImageClickEventArgs e)
    {
        if (txtPwd.Text != txtPwd2.Text)
        {
            RegisterStartupScript("starup", "<script>alert('密码不一致！');</script>");
            return;
        }

        m_Customer.Pwd = txtPwd.Text;

        Global.GetStore().CustomerMgr.Update(m_Customer);
        if (Global.GetStore().CustomerMgr.Save(true))
        {
            Session["Customer"] = m_Customer;

            RegisterStartupScript("starup", "<script>alert('保存成功！');</script>");
        }
        else
        {
            RegisterStartupScript("starup", "<script>alert('保存失败，请与管理员联系！');</script>");
            return;
        }
    }
}
