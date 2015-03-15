using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ErpCoreModel.Framework;
using ErpCoreModel.Base;

public partial class regOk : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadData();
        }
    }
    void LoadData()
    {
        if (Session["Company"] != null)
            lbCompany.Text = Session["Company"].ToString();
        if (Session["User"] != null)
        {
            CUser user = (CUser)Session["User"];
            lbName.Text = user.Name;
            lbPwd.Text = user.Pwd;
        }
    }
    protected void btLogin_Click(object sender, EventArgs e)
    {
        Response.Redirect("SubSystem/SelSystemForm.aspx");
    }
}
