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

public partial class Store_account : System.Web.UI.Page
{
    public CCustomer m_Customer = null;
    public CAccount m_Account = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Customer"] == null)
        {
            Response.Redirect("login.aspx");
            return;
        }
        m_Customer = (CCustomer)Session["Customer"];
        m_Account = (CAccount)m_Customer.AccountMgr.GetFirstObj();
    }
    public List<CBaseObject> GetDetailListOrderByCreated()
    {
        List<CBaseObject> lstObj = m_Account.AccountDetailMgr.GetList();
        var listDetail = from obj in lstObj orderby obj.Created descending select obj;
        return listDetail.ToList();
    }
}
