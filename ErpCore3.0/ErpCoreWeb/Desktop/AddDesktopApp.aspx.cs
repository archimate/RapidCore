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

public partial class Desktop_AddDesktopApp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null)
        {
            Response.Redirect("../Login.aspx");
            Response.End();
        }

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
        imgIcon.Src =string.Format( "../{0}/MenuIcon/default.png",Global.GetDesktopIconPathName());
    }

    void PostData()
    {
        if (Request["Name"] == "")
        {
            Response.Write("名称不能空！");
            return;
        }
        if (Request["Url"] == "")
        {
            Response.Write("Url不能空！");
            return;
        }
        CUser user = (CUser)Session["User"];

        CDesktopApp BaseObject = new CDesktopApp();
        BaseObject.Ctx = Global.GetCtx(Session["TopCompany"].ToString());
        BaseObject.Name = Request["Name"];
        BaseObject.Url = Request["Url"];
        BaseObject.B_User_id = user.Id;
        if(!string.IsNullOrEmpty( Request["Icon"]))
            BaseObject.IconUrl = Request["Icon"];
        BaseObject.OpenwinWidth = Convert.ToInt32(Request["OpenwinWidth"]);
        BaseObject.OpenwinHeight = Convert.ToInt32(Request["OpenwinHeight"]);
        BaseObject.Creator = user.Id;

        string GroupId = Request["GroupId"];
        if (!string.IsNullOrEmpty(GroupId))
            BaseObject.UI_DesktopGroup_id = new Guid(GroupId);

        if (!user.DesktopAppMgr.AddNew(BaseObject, true))
        {
            Response.Write("添加失败！");
            return;
        }
    }
}
