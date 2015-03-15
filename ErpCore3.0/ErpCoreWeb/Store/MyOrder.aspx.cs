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

public partial class Store_MyOrder : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string Cancel_id = Request["Cancel_id"];
        if (!string.IsNullOrEmpty(Cancel_id))
        {
            COrder order = (COrder)Global.GetStore().OrderMgr.Find(new Guid(Cancel_id));
            if (order != null)
            {
                order.State = OrderState.Cancel;
                Global.GetStore().OrderMgr.Update(order, true);
            }
        }
    }
    public List<CBaseObject> GetOrderListOrderByCreated()
    {
        List<CBaseObject> lstObj = Global.GetStore().OrderMgr.GetList();
        var varObj = from obj in lstObj orderby (obj as COrder).Created descending select obj;
        return varObj.ToList();
    }
}
