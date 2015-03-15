using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.UI;

public partial class View_SelViewType : System.Web.UI.Page
{
    public CView m_View = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null)
        {
            Response.Redirect("../Login.aspx");
            Response.End();
        }

        //编辑视图
        string id = Request["id"];
        if (!string.IsNullOrEmpty(id))
        {
            m_View = (CView)Global.GetCtx(Session["TopCompany"].ToString()).ViewMgr.Find(new Guid(id));
            if(m_View.VType== enumViewType.Single)
                Response.Redirect("SingleViewInfo1.aspx?id="+Request["id"]+"&catalog_id="+Request["catalog_id"]);
            else if(m_View.VType== enumViewType.MasterDetail)
                Response.Redirect("MasterDetailViewInfo1.aspx?id="+Request["id"]+"&catalog_id="+Request["catalog_id"]);
            else
                Response.Redirect("MultMasterDetailViewInfo1.aspx?id="+Request["id"]+"&catalog_id="+Request["catalog_id"]);
        }
    }
    protected void btOk_Click(object sender, EventArgs e)
    {
        if (rdSingle.Checked)
            Response.Redirect("SingleViewInfo1.aspx?id=" + Request["id"] + "&catalog_id=" + Request["catalog_id"]);
        else if (rdMasterDetail.Checked)
            Response.Redirect("MasterDetailViewInfo1.aspx?id=" + Request["id"] + "&catalog_id=" + Request["catalog_id"]);
        else
            Response.Redirect("MultMasterDetailViewInfo1.aspx?id=" + Request["id"] + "&catalog_id=" + Request["catalog_id"]);
    }
}
