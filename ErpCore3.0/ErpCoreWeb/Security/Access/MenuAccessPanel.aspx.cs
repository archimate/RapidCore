using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.UI;

public partial class Security_Access_MenuAccessPanel : System.Web.UI.Page
{
    public CCompany m_Company = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetNoStore();
        if (Session["User"] == null)
        {
            Response.Redirect("../Login.aspx");
            Response.End();
        }

        string B_Company_id = Request["B_Company_id"];
        if (string.IsNullOrEmpty(B_Company_id))
            m_Company = Global.GetCtx(Session["TopCompany"].ToString()).CompanyMgr.FindTopCompany();
        else
            m_Company = (CCompany)Global.GetCtx(Session["TopCompany"].ToString()).CompanyMgr.Find(new Guid(B_Company_id));

        if (Request.Params["Action"] == "GetData")
        {
            GetData();
            Response.End();
        }
        else if (Request.Params["Action"] == "PostData")
        {
            PostData();
            Response.End();
        }
    }
    void GetData()
    {
        string UType = Request["UType"];
        string Uid = Request["Uid"];
        string GroupId = Request["GroupId"];
        Guid guidGroupId = Guid.Empty;
        if (!string.IsNullOrEmpty(GroupId))
            guidGroupId = new Guid(GroupId);
        CUser user = null;
        CRole role = null;
        string sData = "";
        if (UType == "0") //用户
        {
            if (!string.IsNullOrEmpty(Uid) && !string.IsNullOrEmpty(GroupId))
            {
                user = (CUser)Global.GetCtx(Session["TopCompany"].ToString()).UserMgr.Find(new Guid(Uid));
                LoopGetMenu(ref sData, user, Guid.Empty, guidGroupId);
            }
        }
        else if (UType == "1") //角色
        {
            if (!string.IsNullOrEmpty(Uid))
            {
                role = (CRole)m_Company.RoleMgr.Find(new Guid(Uid));
                LoopGetMenu(ref sData, role, Guid.Empty, guidGroupId);
            }
        }

        if(sData=="")
            LoopGetMenu(ref sData,Guid.Empty);
        
        sData = "[" + sData + "]";

        Response.Write(sData);
    }
    void LoopGetMenu(ref string sData,Guid Parent_id)
    {
        List<CBaseObject> lstObj= Global.GetCtx(Session["TopCompany"].ToString()).MenuMgr.GetList();
        foreach (CBaseObject obj in lstObj)
        {
            CMenu menu = (CMenu)obj;
            if (menu.Parent_id != Parent_id)
                continue;
            string sChildren = "";
            LoopGetMenu(ref sChildren, menu.Id);

            string sIconUrl = string.Format("../../{0}/MenuIcon/default.png",
                Global.GetDesktopIconPathName());
            if (menu.IconUrl != "")
            {
                sIconUrl = string.Format("../../{0}/MenuIcon/{1}",
                    Global.GetDesktopIconPathName(), menu.IconUrl);
            }
            sData += string.Format("{{\"id\":\"{0}\",\"text\":\"{1}\",\"icon\":\"{2}\", children: [{3}]}},",
                menu.Id, menu.Name,sIconUrl, sChildren);
        }
        sData = sData.TrimEnd(",".ToCharArray());
    }
    void LoopGetMenu(ref string sData, CUser user, Guid Parent_id, Guid UI_DesktopGroup_id)
    {
        List<CBaseObject> lstObj = Global.GetCtx(Session["TopCompany"].ToString()).MenuMgr.GetList();
        foreach (CBaseObject obj in lstObj)
        {
            CMenu menu = (CMenu)obj;
            if (menu.Parent_id != Parent_id)
                continue;
            string sChildren = "";
            LoopGetMenu(ref sChildren, user, menu.Id, UI_DesktopGroup_id);
            string sIsCheck = "";
            
            if (user.UserMenuMgr.FindByMenu(menu.Id, UI_DesktopGroup_id) != null)
                sIsCheck = "\"ischecked\":\"true\",";

            string sIconUrl = string.Format("../../{0}/MenuIcon/default.png",
                Global.GetDesktopIconPathName());
            if (menu.IconUrl != "")
            {
                sIconUrl = string.Format("../../{0}/MenuIcon/{1}",
                    Global.GetDesktopIconPathName(), menu.IconUrl);
            }
            sData += string.Format("{{\"id\":\"{0}\",\"text\":\"{1}\",{2} \"icon\":\"{3}\",children: [{4}]}},",
                menu.Id, menu.Name, sIsCheck, sIconUrl, sChildren);
        }
        sData = sData.TrimEnd(",".ToCharArray());
    }
    void LoopGetMenu(ref string sData, CRole role, Guid Parent_id, Guid UI_DesktopGroup_id)
    {
        List<CBaseObject> lstObj = Global.GetCtx(Session["TopCompany"].ToString()).MenuMgr.GetList();
        foreach (CBaseObject obj in lstObj)
        {
            CMenu menu = (CMenu)obj;
            if (menu.Parent_id != Parent_id)
                continue;
            string sChildren = "";
            LoopGetMenu(ref sChildren, role, menu.Id, UI_DesktopGroup_id);
            string sIsCheck = "";
            if (role.RoleMenuMgr.FindByMenu(menu.Id, UI_DesktopGroup_id) != null)
                    sIsCheck = "\"ischecked\":\"true\",";

            string sIconUrl = string.Format("../../{0}/MenuIcon/default.png",
                Global.GetDesktopIconPathName());
            if (menu.IconUrl != "")
            {
                sIconUrl = string.Format("../../{0}/MenuIcon/{1}",
                    Global.GetDesktopIconPathName(), menu.IconUrl);
            }
            sData += string.Format("{{\"id\":\"{0}\",\"text\":\"{1}\",{2} \"icon\":\"{3}\",children: [{4}]}},",
                menu.Id, menu.Name, sIsCheck, sIconUrl,sChildren);
        }
        sData = sData.TrimEnd(",".ToCharArray());
    }

    void PostData()
    {
        string UType = Request["UType"];
        string Uid = Request["Uid"];
        string GroupId = Request["GroupId"];
        Guid guidGroupId = Guid.Empty;
        if (!string.IsNullOrEmpty(GroupId))
            guidGroupId = new Guid(GroupId);
        string postData = Request["postData"];
        CUser user = null;
        CRole role = null;
        if (UType == "0") //用户
        {
            user = (CUser)Global.GetCtx(Session["TopCompany"].ToString()).UserMgr.Find(new Guid(Uid));
           
            user.UserMenuMgr.RemoveByDesktopGroupId(guidGroupId);
            string[] arr1= postData.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            foreach (string sItem1 in arr1)
            {
                Guid menuid = new Guid(sItem1);
                CUserMenu UserMenu = new CUserMenu();
                UserMenu.B_User_id = user.Id;
                UserMenu.UI_Menu_id = menuid;
                UserMenu.UI_DesktopGroup_id = guidGroupId;

                CUser user0 = (CUser)Session["User"];
                UserMenu.Creator = user0.Id;
                user.UserMenuMgr.AddNew(UserMenu);
            }
            if (!user.UserMenuMgr.Save(true))
            {
                Response.Write("保存失败！");
            }
        }
        else if (UType == "1") //角色
        {
            role = (CRole)m_Company.RoleMgr.Find(new Guid(Uid));
            
            role.RoleMenuMgr.RemoveByDesktopGroupId(guidGroupId);
            string[] arr1 = postData.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            foreach (string sItem1 in arr1)
            {
                Guid menuid = new Guid(sItem1);
                CRoleMenu RoleMenu = new CRoleMenu();
                RoleMenu.B_Role_id = role.Id;
                RoleMenu.UI_Menu_id = menuid;
                RoleMenu.UI_DesktopGroup_id = guidGroupId;

                CUser user0 = (CUser)Session["User"];
                RoleMenu.Creator = user0.Id;
                role.RoleMenuMgr.AddNew(RoleMenu);
            }
            if (!role.RoleMenuMgr.Save(true))
            {
                Response.Write("保存失败！");
            }
        }
    }
}
