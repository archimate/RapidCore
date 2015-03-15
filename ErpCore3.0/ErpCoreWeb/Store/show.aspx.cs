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

public partial class Store_show : System.Web.UI.Page
{
    public CProductMgr m_ProductMgr;
    public CProduct m_Product;
    public CPromotionMgr m_PromotionMgr;

    protected void Page_Load(object sender, EventArgs e)
    {
        m_PromotionMgr = Global.GetStore().PromotionMgr;
        m_ProductMgr = Global.GetStore().ProductMgr;

        string id = Request["id"];
        if (string.IsNullOrEmpty(id))
        {
            Response.End();
            return;
        }
        m_Product = (CProduct)m_ProductMgr.Find(new Guid(id));
        if (m_Product == null)
        {
            Response.End();
            return;
        }

        if (!IsPostBack)
        {
            lbProductName.Text = m_Product.Name;
            lbUnit.Text = m_Product.Unit;

            List<CBaseObject> lstCr = m_Product.ColorInProductMgr.GetList();
            rdlistColor.Items.Clear();
            foreach (CBaseObject obj in lstCr)
            {
                CColorInProduct cr = (CColorInProduct)obj;
                ListItem item = new ListItem(cr.Name,cr.Id.ToString());
                rdlistColor.Items.Add(item);
            }
            List<CBaseObject> lstSpec = m_Product.SpecificationInProductMgr.GetList();
            rdlistSpecification.Items.Clear();
            foreach (CBaseObject obj in lstSpec)
            {
                CSpecificationInProduct sp = (CSpecificationInProduct)obj;
                ListItem item = new ListItem(sp.Name, sp.Id.ToString());
                rdlistSpecification.Items.Add(item);
            }

            LoadGrid();
        }
    }
    protected void imgbtOrder_Click(object sender, ImageClickEventArgs e)
    {
        if (AddToOrder())
            Response.Redirect("Order.aspx");
    }
    protected void imgbtAdd_Click(object sender, ImageClickEventArgs e)
    {
        if (AddToOrder())
        {
            RegisterStartupScript("starup", "<script>alert('添加成功！');</script>");
        }
    }
    bool AddToOrder()
    {
        if (rdlistColor.SelectedItem == null)
        {
            RegisterStartupScript("starup", "<script>alert('请选择颜色！');</script>");
            return false;
        }
        if (rdlistSpecification.SelectedItem == null)
        {
            RegisterStartupScript("starup", "<script>alert('请选择规格！');</script>");
            return false;
        }
        if (txtNum.Text.Trim() == "")
        {
            RegisterStartupScript("starup", "<script>alert('请填写订购数量！');</script>");
            return false;
        }
        if (!Util.IsInt(txtNum.Text.Trim()))
        {
            RegisterStartupScript("starup", "<script>alert('订购数量请填写整数！');</script>");
            return false;
        }
        int iNum = Convert.ToInt32(txtNum.Text.Trim());
        if (iNum <= 0)
        {
            RegisterStartupScript("starup", "<script>alert('订购数量要大于0！');</script>");
            return false;
        }

        COrder order = null;
        if (Session["Order"] == null)
        {
            order = new COrder();
            order.Ctx = Global.GetCtx();
        }
        else
        {
            order = (COrder)Session["Order"];
        }
        bool bAddDetail = false;
        List<CBaseObject> lstObj = order.OrderDetailMgr.GetList();
        foreach (CBaseObject obj in lstObj)
        {
            COrderDetail od = (COrderDetail)obj;
            if (od.SP_Product_id == m_Product.Id
                && od.Color.Equals(rdlistColor.SelectedItem.Text, StringComparison.OrdinalIgnoreCase)
                && od.Specification.Equals(rdlistSpecification.SelectedItem.Text, StringComparison.OrdinalIgnoreCase))
            {
                od.Num += iNum;
                bAddDetail = true;
                break;
            }
        }
        if (!bAddDetail)
        {
            COrderDetail od = new COrderDetail();
            od.Ctx = Global.GetCtx();
            od.DD_Order_id = order.Id;
            od.SP_Product_id = m_Product.Id;
            od.Color = rdlistColor.SelectedItem.Text;
            od.Specification = rdlistSpecification.SelectedItem.Text;
            od.Num = iNum;

            order.OrderDetailMgr.AddNew(od);
        }

        Session["Order"] = order;

        return true;
    }
    void LoadGrid()
    {
        int iOrderNum = 0, iScoringNum = 0;
        DataTable dt = new DataTable();
        dt.Columns.Add("客户");
        dt.Columns.Add("打分");
        dt.Columns.Add("评论");
        COrderDetailMgr OrderDetailMgr = new COrderDetailMgr();
        OrderDetailMgr.Ctx = Global.GetCtx();
        string sWhere = string.Format("SP_Product_id='{0}' and State=2",
            m_Product.Id);//仅考虑结束的订单
        string sOrderby = "Created desc";
        List<CBaseObject> lstObj = OrderDetailMgr.GetList(sWhere, sOrderby);
        foreach (CBaseObject obj in lstObj)
        {
            COrderDetail detail = (COrderDetail)obj;
            iOrderNum++;
            COrder order = (COrder)Global.GetStore().OrderMgr.Find(detail.DD_Order_id);
            if(order.Scoring== ScoringType.NoScoring)
                continue;
            iScoringNum++;
            CCustomer customer = (CCustomer)Global.GetStore().CustomerMgr.Find(order.KH_Customer_id);
            DataRow row = dt.NewRow();
            row[0] = customer != null ? customer.Name : "";
            string sScore="赞";
            if(order.Scoring== ScoringType.General)
                sScore="一般";
            else if(order.Scoring== ScoringType.Bad)
                sScore="差";
            row[1] = sScore;
            row[2] = order.Comment;

            dt.Rows.Add(row);
        }

        gridScoring.DataSource = dt;
        gridScoring.DataBind();

        lbOrderNum.Text = iOrderNum.ToString();
        lbScoringNum.Text = iScoringNum.ToString();
    }
}
