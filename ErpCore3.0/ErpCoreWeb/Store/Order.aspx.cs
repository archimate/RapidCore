using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.Store;

public partial class Store_Order : System.Web.UI.Page
{
    public COrder m_Order=null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Order"] != null)
            m_Order = (COrder)Session["Order"];

        string delid = Request["delid"];
        if (!string.IsNullOrEmpty(delid))
        {
            m_Order.OrderDetailMgr.Delete(new Guid(delid));
        }
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
        if(!bHasPrice)
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
    protected void btMod_Click(object sender, EventArgs e)
    {
        string id = hidEditId.Value;
        string sNum = txtEditNum.Value.Trim();
        if (sNum == "")
        {
            RegisterStartupScript("starup", "<script>alert('请填写订购数量！');</script>");
            return ;
        }
        if (!Util.IsInt(sNum))
        {
            RegisterStartupScript("starup", "<script>alert('订购数量请填写整数！');</script>");
            return ;
        }
        int iNum = Convert.ToInt32(sNum);
        if (iNum <= 0)
        {
            RegisterStartupScript("starup", "<script>alert('订购数量要大于0！');</script>");
            return ;
        }

        List<CBaseObject> lstObj = m_Order.OrderDetailMgr.GetList();
        foreach (CBaseObject obj in lstObj)
        {
            COrderDetail od = (COrderDetail)obj;
            if (od.Id==new Guid(id))
            {
                od.Num = iNum;
                break;
            }
        }
        Session["Order"] = m_Order;
    }
    protected void imgNext_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["Customer"] == null)
        {
            Response.Redirect("login.aspx?to=OrderAddr.aspx");
            return;
        }

        Response.Redirect("OrderAddr.aspx");
    }
}
