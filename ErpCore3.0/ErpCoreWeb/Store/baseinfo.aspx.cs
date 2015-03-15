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

public partial class Store_baseinfo : System.Web.UI.Page
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
            LoadSex();
            LoadBirthday();
            LoadData();
        }
    }
    void LoadData()
    {
        txtName.Text = m_Customer.Name;
        txtTName.Text = m_Customer.TName;
        if (m_Customer.Sex)
            cbSex.SelectedIndex = 1;
        ListItem item = cbYear.Items.FindByText(m_Customer.Birthday.Year.ToString());
        item.Selected=true;
        item = cbMonth.Items.FindByText(m_Customer.Birthday.Month.ToString());
        item.Selected = true;
        item = cbDay.Items.FindByText(m_Customer.Birthday.Day.ToString());
        item.Selected = true;
        txtAddr.Text = m_Customer.Addr;
        txtZipcode.Text = m_Customer.Zipcode;
        txtCompany.Text = m_Customer.Company;
        txtTel.Text = m_Customer.Tel;
        txtPhone.Text = m_Customer.Phone;
        txtEmail.Text = m_Customer.E_mail;
        txtQQ.Text = m_Customer.QQ;
        txtWW.Text = m_Customer.WW;
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

    protected void btSave_Click(object sender, EventArgs e)
    {
        if (cbYear.SelectedIndex == 0 || cbMonth.SelectedIndex == 0 || cbDay.SelectedIndex == 0)
        {
            RegisterStartupScript("starup", "<script>alert('请选择生日！');</script>");
            return;
        }


        m_Customer.TName = txtTName.Text.Trim();
        m_Customer.Sex = cbSex.SelectedIndex == 0 ? false : true;
        m_Customer.Birthday = DateTime.Parse(string.Format("{0}-{1}-{2}",
            cbYear.SelectedItem.Text,
            cbMonth.SelectedItem.Text,
            cbDay.SelectedItem.Text));
        m_Customer.Addr = txtAddr.Text.Trim();
        m_Customer.Zipcode = txtZipcode.Text.Trim();
        m_Customer.Company = txtCompany.Text.Trim();
        m_Customer.Tel = txtTel.Text.Trim();
        m_Customer.Phone = txtPhone.Text.Trim();
        m_Customer.E_mail = txtEmail.Text.Trim();
        m_Customer.QQ = txtQQ.Text.Trim();
        m_Customer.WW = txtWW.Text.Trim();

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
