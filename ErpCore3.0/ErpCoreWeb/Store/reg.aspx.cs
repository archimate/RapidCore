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

public partial class Store_reg : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadSex();
            LoadBirthday();
        }
    }
    void LoadSex()
    {
        cbSex.Items.Clear();
        cbSex.Items.Add("男");
        cbSex.Items.Add("女");
    }
    void LoadBirthday()
    {
        cbYear.Items.Clear();
        for (int i = 1900; i < DateTime.Now.Year; i++)
        {
            cbYear.Items.Insert(0,i.ToString());
        }
        cbYear.Items.Insert(0, "请选择");

        cbMonth.Items.Clear();
        for (int i = 1; i <= 12; i++)
        {
            cbMonth.Items.Add(i.ToString());
        }
        cbMonth.Items.Insert(0, "请选择");

        cbDay.Items.Clear();
        for (int i = 1; i <= 31; i++)
        {
            cbDay.Items.Add(i.ToString());
        }
        cbDay.Items.Insert(0, "请选择");
    }

    protected void imgReg_Click(object sender, ImageClickEventArgs e)
    {
        if (txtName.Text.Trim() == "")
        {
            RegisterStartupScript("starup", "<script>alert('用户名不能空！');</script>");
            return;
        }
        if (txtPwd.Text.Trim() == "")
        {
            RegisterStartupScript("starup", "<script>alert('用户名不能空！');</script>");
            return;
        }
        if (txtPwd.Text != txtPwd2.Text)
        {
            RegisterStartupScript("starup", "<script>alert('密码不一致！');</script>");
            return;
        }
        if (cbYear.SelectedIndex == 0 || cbMonth.SelectedIndex == 0 || cbDay.SelectedIndex == 0)
        {
            RegisterStartupScript("starup", "<script>alert('请选择生日！');</script>");
            return;
        }


        CCustomer customer = Global.GetStore().CustomerMgr.FindByName(txtName.Text.Trim());
        if (customer != null)
        {
            RegisterStartupScript("starup", "<script>alert('用户已经不存在！');</script>");
            return;
        }
        customer = new CCustomer();
        customer.Ctx = Global.GetCtx();
        customer.Name = txtName.Text.Trim();
        customer.Pwd = txtPwd.Text;
        customer.TName = txtTName.Text.Trim();
        customer.Sex = cbSex.SelectedIndex == 0 ? false : true;
        customer.Birthday = DateTime.Parse(string.Format("{0}-{1}-{2}",
            cbYear.SelectedItem.Text,
            cbMonth.SelectedItem.Text,
            cbDay.SelectedItem.Text));
        customer.Addr = txtAddr.Text.Trim();
        customer.Zipcode = txtZipcode.Text.Trim();
        customer.Company = txtCompany.Text.Trim();
        customer.Tel = txtTel.Text.Trim();
        customer.Phone = txtPhone.Text.Trim();
        customer.E_mail = txtEmail.Text.Trim();
        customer.QQ = txtQQ.Text.Trim();
        customer.WW = txtWW.Text.Trim();

        Global.GetStore().CustomerMgr.AddNew(customer);
        if (Global.GetStore().CustomerMgr.Save(true))
        {
            Session["Customer"] = customer;

            string to = Request["to"];
            if (string.IsNullOrEmpty(to))
                to = "index.aspx";
            RegisterStartupScript("starup", string.Format("<script>alert('注册成功！');window.location='{0}';</script>", to));

        }
        else
        {
            RegisterStartupScript("starup", "<script>alert('注册失败，请与管理员联系！');</script>");
            return;
        }
    }
}
