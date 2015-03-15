using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using System.Data;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.Store;

public partial class Store_OrderAddr : System.Web.UI.Page
{
    public CCustomer m_Customer = null;
    public COrder m_Order = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Customer"] == null)
        {
            Response.Redirect("login.aspx?to=OrderAddr.aspx");
            return;
        }
        m_Customer = (CCustomer)Session["Customer"];

        if (Session["Order"] != null)
            m_Order = (COrder)Session["Order"];

        string delid = Request["delid"];
        if (!string.IsNullOrEmpty(delid))
        {
            m_Order.OrderDetailMgr.Delete(new Guid(delid));
        }

        if (!IsPostBack)
        {
            LoadProvince();

            CAccount account = (CAccount)m_Customer.AccountMgr.GetFirstObj();
            lbAccount.Text = account.Score.ToString();
        }
    }
    void LoadProvince()
    {
        cbProvince.Items.Clear();
        cbProvince.Items.Add(new ListItem("请选择", ""));
        List<CBaseObject> lstObj= Global.GetCtx().ProvinceMgr.GetList();
        foreach (CBaseObject obj in lstObj)
        {
            CProvince province = (CProvince)obj;
            ListItem item = new ListItem(province.Name, province.Id.ToString());
            cbProvince.Items.Add(item);
        }
        cbProvince.SelectedIndex = 0;
    }
    void LoadCity()
    {
        cbCity.Items.Clear();
        cbCity.Items.Add(new ListItem("请选择", ""));
        if (cbProvince.SelectedIndex == 0)
            return;
        CProvince province = (CProvince)Global.GetCtx().ProvinceMgr.Find(new Guid(cbProvince.SelectedItem.Value));
        List<CBaseObject> lstObj = province.CityMgr.GetList();
        foreach (CBaseObject obj in lstObj)
        {
            CCity city = (CCity)obj;
            ListItem item = new ListItem(city.Name, city.Id.ToString());
            cbCity.Items.Add(item);
        }
        cbCity.SelectedIndex = 0;
    }
    void LoadDistrict()
    {
        cbDistrict.Items.Clear();
        cbDistrict.Items.Add(new ListItem("请选择", ""));
        if (cbCity.SelectedIndex == 0)
            return;
        CProvince province = (CProvince)Global.GetCtx().ProvinceMgr.Find(new Guid(cbProvince.SelectedItem.Value));
        CCity city = (CCity)province.CityMgr.Find(new Guid(cbCity.SelectedItem.Value));
        List<CBaseObject> lstObj = city.DistrictMgr.GetList();
        foreach (CBaseObject obj in lstObj)
        {
            CDistrict district = (CDistrict)obj;
            ListItem item = new ListItem(district.Name, district.Id.ToString());
            cbDistrict.Items.Add(item);
        }
        cbDistrict.SelectedIndex = 0;
    }
    //计算单价
    public double CalcPrice(CProduct product, COrderDetail od)
    {
        double dblPrice = 0;
        //如果是促销，取促销价
        bool bHasPrice = false;
        if (Global.GetStore().PromotionMgr.FindByProduct(product.Id) != null)
        {
            List<CBaseObject> lstPrice = product.PriceMgr.FindByType(PriceType.Promotion);
            if (lstPrice.Count > 0)
            {
                dblPrice = ((CPrice)lstPrice[0]).Price;
                bHasPrice = true;
            }
        }
        if (!bHasPrice)
        {
            //如果数量符合批发，则取批发价
            List<CBaseObject> lstPrice = product.PriceMgr.FindByType(PriceType.Wholesale);
            if (lstPrice.Count > 0)
            {
                for (int i = lstPrice.Count - 1; i >= 0; i--)
                {
                    CPrice price = (CPrice)lstPrice[i];
                    if (price.MinOrderNum <= od.Num)
                    {
                        dblPrice = price.Price;
                        bHasPrice = true;
                        break;
                    }
                }
            }
            //如果没有符合，则取零售价
            if (!bHasPrice)
            {
                List<CBaseObject> lstPrice2 = product.PriceMgr.FindByType(PriceType.Retail);
                if (lstPrice2.Count > 0)
                {
                    CPrice price = (CPrice)lstPrice2[0];
                    dblPrice = price.Price;
                    bHasPrice = true;
                }
            }
        }
        return dblPrice;
    }
    //计算总价
    public double CalcTotalPrice()
    {
        double dblTotal = 0;
        List<CBaseObject> lstObj = m_Order.OrderDetailMgr.GetList();
        foreach (CBaseObject obj in lstObj)
        {
            COrderDetail od = (COrderDetail)obj;
            string sName = "";
            CProduct product = (CProduct)Global.GetStore().ProductMgr.Find(od.SP_Product_id);
            sName = product.Name;
            //根据数量计算单价
            double dblPrice = CalcPrice(product, od);

            dblTotal += dblPrice * od.Num;
        }
        return dblTotal;
    }
    
    protected void cbProvince_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCity();
    }
    protected void cbCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDistrict();
    }
    
    protected void imgbtOk_Click(object sender, ImageClickEventArgs e)
    {
        if (m_Order.OrderDetailMgr.GetList().Count == 0)
        {
            RegisterStartupScript("starup", "<script>alert('您还没有购买商品！');</script>");
            return;
        }
        if (cbProvince.SelectedIndex == 0)
        {
            RegisterStartupScript("starup", "<script>alert('请选择省！');</script>");
            return;
        }
        if (cbCity.SelectedIndex == 0)
        {
            RegisterStartupScript("starup", "<script>alert('请选择城市！');</script>");
            return;
        }
        if (cbDistrict.SelectedIndex == 0)
        {
            RegisterStartupScript("starup", "<script>alert('请选择地区！');</script>");
            return;
        }
        if (txtAddr.Text.Trim()=="")
        {
            RegisterStartupScript("starup", "<script>alert('请填写详细地址！');</script>");
            return;
        }
        if (txtContacts.Text.Trim() == "")
        {
            RegisterStartupScript("starup", "<script>alert('请填写收货人姓名！');</script>");
            return;
        }
        if (txtTel.Text.Trim() == "" && txtPhone.Text.Trim()=="")
        {
            RegisterStartupScript("starup", "<script>alert('至少填写电话或手机一项！');</script>");
            return;
        }
        if (rdlistPayMode.SelectedItem == null)
        {
            RegisterStartupScript("starup", "<script>alert('请选择付款方式！');</script>");
            return;
        }

        double dblTotal = CalcTotalPrice();
        CAccount account = (CAccount)m_Customer.AccountMgr.GetFirstObj();
        if (rdlistPayMode.SelectedIndex == 0)
        {
            if (account.Score < dblTotal)
            {
                RegisterStartupScript("starup", "<script>alert('账户余额不足，请及时充值！');</script>");
                return;
            }
        }
        //保存订单
        m_Order.Ctx = Global.GetCtx();
        m_Order.B_Province_id = new Guid(cbProvince.SelectedItem.Value);
        m_Order.B_City_id = new Guid(cbCity.SelectedItem.Value);
        m_Order.B_District_id = new Guid(cbDistrict.SelectedItem.Value);
        m_Order.Addr = txtAddr.Text;
        m_Order.Zipcode = txtZipcode.Text;
        m_Order.Contacts = txtContacts.Text;
        m_Order.Tel = txtTel.Text;
        m_Order.Phone = txtPhone.Text;
        if (rdlistPayMode.SelectedIndex == 0)
            m_Order.PayMode = PayModeType.Account;
        else
            m_Order.PayMode = PayModeType.Delivery;
        //产生订单号
        m_Order.Code = MakeCode();

        //收款
        if (m_Order.PayMode == PayModeType.Account)
        {
            account.Score -= dblTotal;
            CAccountDetail detail = new CAccountDetail();
            detail.Ctx = account.Ctx;
            detail.KH_Account_id = account.Id;
            detail.Score = -dblTotal;
            detail.Content = string.Format("订单号：{0}",m_Order.Code);
            account.AccountDetailMgr.AddNew(detail);

            m_Customer.AccountMgr.Update(account);
        }
        
        Global.GetStore().OrderMgr.AddNew(m_Order);
        if (Global.GetStore().OrderMgr.Save(true))
        {
            if (m_Customer.AccountMgr.Save(true))
            {
                Session["Order"] = null;
                Response.Redirect(string.Format("OrderResult.aspx?ret=1&code={0}",m_Order.Code));
                return;
            }
        }
        Response.Redirect("OrderResult.aspx?ret=0");
    }
    //生成订单号
    string MakeCode()
    {
        string sCode = string.Format("{0}{1}{2}{3}{4}{5}{6}",
            DateTime.Now.Year,
            DateTime.Now.Month,
            DateTime.Now.Day,
            DateTime.Now.Hour,
            DateTime.Now.Minute,
            DateTime.Now.Second,
            DateTime.Now.Millisecond);

        while (Global.GetStore().OrderMgr.FindByCode(sCode) != null)
        {
            sCode = (Convert.ToInt64(sCode) + 1).ToString();
        }

        return sCode;
    }
}
