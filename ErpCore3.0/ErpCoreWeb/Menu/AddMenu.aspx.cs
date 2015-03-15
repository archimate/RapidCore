using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.UI;

public partial class Menu_AddMenu : System.Web.UI.Page
{
    public CTable m_Table = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null)
        {
            Response.Redirect("../Login.aspx");
            Response.End();
        }

        m_Table = Global.GetCtx(Session["TopCompany"].ToString()).MenuMgr.Table;

        if (Request.Params["Action"] == "PostData")
        {
            PostData();
            Response.End();
        }

        if (!IsPostBack)
        {
            LoadData();
        }
    }
    void LoadData()
    {
        string pid = Request["pid"];
        if (string.IsNullOrEmpty(pid))
        {
            txtParent.Value = "菜单";
        }
        else
        {
            CMenu pmenu = (CMenu)Global.GetCtx(Session["TopCompany"].ToString()).MenuMgr.Find(new Guid(pid));
            txtParent.Value = pmenu.Name;
            hidParent.Value = pmenu.Id.ToString();
        }
        cbView.Attributes.Add("disabled", "disabled");
        cbWindow.Attributes.Add("disabled", "disabled");
        txtUrl.Attributes.Add("disabled", "disabled");
        cbReport.Attributes.Add("disabled", "disabled");
        imgIcon.Src = string.Format("../{0}/MenuIcon/default.png", Global.GetDesktopIconPathName());
    }

    void PostData()
    {
        if (txtName.Value.Trim() == "")
        {
            Response.Write("<script>parent.callback('名称不能空！')</script>");
            return;
        }
        //同级不能有同名
        Guid Parent_id = Guid.Empty;
        if (hidParent.Value != "")
            Parent_id = new Guid(hidParent.Value);
        if (Global.GetCtx(Session["TopCompany"].ToString()).MenuMgr.FindByName(txtName.Value, Parent_id) != null)
        {
            Response.Write("<script>parent.callback('菜单已经存在！')</script>");
            return;
        }
        CUser user = (CUser)Session["User"];

        CMenu BaseObject = new CMenu();
        BaseObject.Ctx = Global.GetCtx(Session["TopCompany"].ToString());
        BaseObject.Name = txtName.Value;
        BaseObject.Parent_id = Parent_id;
        if (rdType1.Checked)
            BaseObject.MType = enumMenuType.CatalogMenu;
        else if (rdType2.Checked)
            BaseObject.MType = enumMenuType.ViewMenu;
        else if (rdType3.Checked)
            BaseObject.MType = enumMenuType.WindowMenu;
        else if (rdType4.Checked)
            BaseObject.MType = enumMenuType.UrlMenu;
        else if (rdType5.Checked)
            BaseObject.MType = enumMenuType.ReportMenu;
        if (hidView.Value != "")
            BaseObject.UI_View_id = new Guid(hidView.Value);
        if (hidWindow.Value != "")
            BaseObject.UI_Window_id = new Guid(hidWindow.Value);
        BaseObject.Url = txtUrl.Value;
        if (hidReport.Value != "")
            BaseObject.RPT_Report_id = new Guid(hidReport.Value);
        if(hidIcon.Value!="")
            BaseObject.IconUrl = hidIcon.Value;
        BaseObject.OpenwinWidth = Convert.ToInt32(txtOpenwinWidth.Value);
        BaseObject.OpenwinHeight = Convert.ToInt32(txtOpenwinHeight.Value);
        BaseObject.Creator = user.Id;


        if (!Global.GetCtx(Session["TopCompany"].ToString()).MenuMgr.AddNew(BaseObject, true))
        {
            Response.Write("<script>parent.callback('添加失败！')</script>");
            return;
        }
        Response.Write("<script>parent.callback('')</script>");
    }
}
