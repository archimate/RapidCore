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

public partial class Store_login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void imgLogin_Click(object sender, ImageClickEventArgs e)
    {
        CCustomer customer = Global.GetStore().CustomerMgr.FindByName(txtName.Text.Trim());
        if (customer == null)
        {
            RegisterStartupScript("starup", "<script>alert('用户不存在！');</script>");
            return;
        }
        if (customer.Pwd != txtPwd.Text)
        {
            RegisterStartupScript("starup", "<script>alert('帐号不正确！');</script>");
            return;
        }
        Session["Customer"] = customer;

        string to = Request["to"];
        if (!string.IsNullOrEmpty(to))
            Response.Redirect(to);
        else
            Response.Redirect("index.aspx");
    }
}
