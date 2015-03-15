using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.Store;

public partial class Store_category : System.Web.UI.Page
{
    public CProductCategoryMgr m_ProductCategoryMgr;
    public CProductTypeMgr m_ProductTypeMgr;
    public CTypeInCategoryMgr m_TypeInCategoryMgr;
    public CProductInTypeMgr m_ProductInTypeMgr;
    public CProductMgr m_ProductMgr;
    public CPromotionMgr m_PromotionMgr;
    public CProductCategory m_ProductCategory = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        m_ProductCategoryMgr = Global.GetStore().ProductCategoryMgr;
        m_ProductTypeMgr = Global.GetStore().ProductTypeMgr;
        m_TypeInCategoryMgr = Global.GetStore().TypeInCategoryMgr;
        m_ProductInTypeMgr = Global.GetStore().ProductInTypeMgr;
        m_ProductMgr = Global.GetStore().ProductMgr;
        m_PromotionMgr = Global.GetStore().PromotionMgr;

        string id = Request["id"];
        if (string.IsNullOrEmpty(id))
        {
            Response.End();
            return;
        }
        m_ProductCategory = (CProductCategory)m_ProductCategoryMgr.Find(new Guid(id));

    }
    public List<CBaseObject> GetPriceOrderByType(CPriceMgr PriceMgr)
    {
        List<CBaseObject> lstPrice = PriceMgr.GetList();
        var arrPrice = from objP in lstPrice orderby (objP as CPrice).Type select objP;
        return arrPrice.ToList();
    }
}
