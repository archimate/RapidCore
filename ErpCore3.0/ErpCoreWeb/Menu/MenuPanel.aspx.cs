using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.UI;

public partial class Menu_MenuPanel : System.Web.UI.Page
{
    public CTable m_Table = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null)
        {
            Response.Redirect("../Login.aspx");
            Response.End();
        }

        m_Table = (CTable)Global.GetCtx(Session["TopCompany"].ToString()).MenuMgr.Table;

        if (Request.Params["Action"] == "GetMenu")
        {
            GetMenu();
            Response.End();
        }
        else if (Request.Params["Action"] == "GetMenuName")
        {
            GetMenuName();
            Response.End();
        }
        else if (Request.Params["Action"] == "Delete")
        {
            Delete();
            Response.End();
        }
    }
    void GetMenu()
    {
        string pid = Request["pid"];
        Guid Parent_id = Guid.Empty;
        if (!string.IsNullOrEmpty(pid))
            Parent_id = new Guid(pid);
        string sJson = "[";
        List<CBaseObject> lstObj = Global.GetCtx(Session["TopCompany"].ToString()).MenuMgr.GetList();
        foreach (CBaseObject obj in lstObj)
        {
            CMenu Menu = (CMenu)obj;
            if (Menu.Parent_id == Parent_id)
            {
                string sIconUrl = string.Format("../{0}/MenuIcon/default.png",
                    Global.GetDesktopIconPathName());
                if (Menu.IconUrl != "")
                {
                    sIconUrl = string.Format("../{0}/MenuIcon/{1}",
                        Global.GetDesktopIconPathName(), Menu.IconUrl);
                }
                string sItem = string.Format("{{ isexpand: \"false\", \"id\":\"{0}\",\"text\": \"{1}\",\"icon\":\"{2}\", children: [] }},",
                    Menu.Id,
                    Menu.Name,
                    sIconUrl);
                sJson += sItem;
            }
        }
        sJson = sJson.TrimEnd(",".ToCharArray());
        sJson += "]";
        Response.Write(sJson);
    }
    void GetMenuName()
    {
        string id = Request["id"];
        if (string.IsNullOrEmpty(id))
            return;
        CMenu menu = (CMenu)Global.GetCtx(Session["TopCompany"].ToString()).MenuMgr.Find(new Guid(id));
        Response.Write(menu.Name);
    }

    void Delete()
    {
        string delid = Request["delid"];
        if (string.IsNullOrEmpty(delid))
        {
            Response.Write("请选择记录！");
            return;
        }
        Global.GetCtx(Session["TopCompany"].ToString()).MenuMgr.Delete(new Guid(delid));
        if (!Global.GetCtx(Session["TopCompany"].ToString()).MenuMgr.Save(true))
        {
            Response.Write("删除失败！");
            return;
        }
    }
}
