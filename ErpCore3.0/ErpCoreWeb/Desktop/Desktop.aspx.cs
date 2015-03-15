using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.UI;

public partial class Desktop_Desktop : System.Web.UI.Page
{
    public CCompany m_Company = null;
    public string m_sBackImg = "../images/default.jpg";
    public CUserMenuMgr m_UserMenuMgr = null;
    public CDesktopAppMgr m_DesktopAppMgr = null;
    public CDesktopGroupMgr m_DesktopGroupMgr = null;
    public Guid m_guidCurGroupId = Guid.Empty;
    public CDesktopGroup m_CurGroup = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null)
        {
            //Response.Redirect("../Login.aspx");
            //Response.End();
            return;
        }

        m_DesktopGroupMgr = Global.GetCtx(Session["TopCompany"].ToString()).DesktopGroupMgr;

        string GroupId = Request["GroupId"];
        if (!string.IsNullOrEmpty(GroupId))
        {
            m_guidCurGroupId = new Guid(GroupId);
            m_CurGroup = (CDesktopGroup)m_DesktopGroupMgr.Find(m_guidCurGroupId);
        }

        if (Request.Params["Action"] == "DelDesktopApp")
        {
            DelDesktopApp();
            Response.End();
        }
        else if (Request.Params["Action"] == "SetBackImg")
        {
            SetBackImg();
            Response.End();
        }
        else if (Request.Params["Action"] == "UpdateOnlineState")
        {
            UpdateOnlineState();
            Response.End();
        }
        
        //桌面背景
        m_sBackImg = string.Format("../{0}/DesktopImg/default.jpg", Global.GetDesktopIconPathName());
        CUser user = (CUser)Session["User"];
        m_Company = (CCompany)Global.GetCtx(Session["TopCompany"].ToString()).CompanyMgr.Find(user.B_Company_id);

        CDesktop desktop = (CDesktop)user.DesktopMgr.GetFirstObj();
        if (desktop != null && desktop.BackImg != "")
            m_sBackImg = string.Format("../{0}/DesktopImg/{1}", Global.GetDesktopIconPathName(), desktop.BackImg); 

        m_UserMenuMgr = user.UserMenuMgr;
        m_DesktopAppMgr = user.DesktopAppMgr;

    }
    void DelDesktopApp()
    {
        string delid = Request["delid"];
        if (string.IsNullOrEmpty(delid))
            return;
        CUser user = (CUser)Session["User"];
        if (!user.DesktopAppMgr.Delete(new Guid(delid), true))
        {
            Response.Write("删除应用失败！");
        }
    }

    //生成菜单
    public string MakeContextMenu()
    {
        string sRet = "";
        List<CBaseObject> lstObj = m_UserMenuMgr.GetList();
        for (int i = 0; i < lstObj.Count; i++)
        {
            CUserMenu UserMenu = (CUserMenu)lstObj[i];
            CMenu menu = (CMenu)Global.GetCtx(Session["TopCompany"].ToString()).MenuMgr.Find(UserMenu.UI_Menu_id);
            if (menu == null || menu.MType!= enumMenuType.CatalogMenu)
                continue;
            LoadMenu(ref sRet, menu);
        }
        return sRet;
    }
    void LoadMenu(ref string sRet, CMenu pmenu)
    {
        string sId = pmenu.Id.ToString().Replace("-", "");
        sRet += string.Format("var menu{0} = $.ligerMenu({{width: 120, items:[", sId);
        List<CBaseObject> lstObj = Global.GetCtx(Session["TopCompany"].ToString()).MenuMgr.GetList();
        foreach(CBaseObject obj in lstObj)
        {
            CMenu menu= (CMenu)obj;
            if(menu.Parent_id!=pmenu.Id)
                continue;
            if (menu.MType == enumMenuType.CatalogMenu)
            {
                string sChildren = "";
                LoopSubMenu(ref sChildren, menu);
                sRet += string.Format("{{id:'{0}', text: '{1}',{2} }},", menu.Id, menu.Name, sChildren);
            }
            else
            {
                sRet += string.Format("{{id:'{0}', text: '{1}', click: menuitemclick }},", menu.Id, menu.Name);
            }
        }
        sRet = sRet.TrimEnd(",".ToCharArray());
        sRet +="]});\r\n";

    }
    void LoopSubMenu(ref string sRet, CMenu pmenu)
    {
        sRet = "children:[";
        List<CBaseObject> lstObj = Global.GetCtx(Session["TopCompany"].ToString()).MenuMgr.GetList();
        foreach (CBaseObject obj in lstObj)
        {
            CMenu menu = (CMenu)obj;
            if (menu.Parent_id != pmenu.Id)
                continue;
            if (menu.MType == enumMenuType.CatalogMenu)
            {
                string sChildren = "";
                LoopSubMenu(ref sChildren, menu);
                sRet += string.Format("{{id:'{0}', text: '{1}',{2} }},", menu.Id, menu.Name, sChildren);
            }
            else
            {
                sRet += string.Format("{{id:'{0}', text: '{1}', click: menuitemclick }},", menu.Id, menu.Name);
            }
        }
        sRet = sRet.TrimEnd(",".ToCharArray());
        sRet += "]";
    }

    void SetBackImg()
    {
        string sBackImg = Request["BackImg"];
        if (string.IsNullOrEmpty(sBackImg))
            return;
        CUser user = (CUser)Session["User"];
        CDesktop desktop = (CDesktop)user.DesktopMgr.GetFirstObj();
        if (desktop == null)
        {
            desktop = new CDesktop();
            desktop.Ctx = Global.GetCtx();
            desktop.B_User_id = user.Id;
            desktop.Creator = user.Id;
            user.DesktopMgr.AddNew(desktop);
        }
        else
        {
            desktop.Updator = user.Id;
            user.DesktopMgr.Update(desktop);
        }
        desktop.BackImg = sBackImg;
        if (!user.DesktopMgr.Save(true))
        {
            Response.Write("保存失败！");
            return;
        }

        m_sBackImg = string.Format("../{0}/DesktopImg/{1}", Global.GetDesktopIconPathName(), desktop.BackImg);
    }

    //更新用户在线状态
    public void UpdateOnlineState()
    {
        CUser user = (CUser)Session["User"];
        user.UpdateOnlineTime();
        Session["User"] = user;
    }

    //排序菜单
    public List<CBaseObject> GetOrderMenu()
    {
        List<CBaseObject> lstRet = new List<CBaseObject>();

        //用户菜单
        List<CBaseObject> lstObj = m_UserMenuMgr.GetList();
        for (int i = 0; i < lstObj.Count; i++)
        {
            CUserMenu UserMenu = (CUserMenu)lstObj[i];
            if (UserMenu.UI_DesktopGroup_id != m_guidCurGroupId)
                continue;

            lstRet.Add(UserMenu);
        }
        //角色菜单
        List<CBaseObject> lstObjR = m_Company.RoleMgr.GetList();
        foreach (CBaseObject objR in lstObjR)
        {
            CRole Role = (CRole)objR;
            if (!((CUser)Session["User"]).IsRole(Role.Name))
                continue;
            List<CBaseObject> lstObjRM = Role.RoleMenuMgr.GetList();
            foreach (CBaseObject objRM in lstObjRM)
            {
                CRoleMenu RoleMenu = (CRoleMenu)objRM;
                if (RoleMenu.UI_DesktopGroup_id != m_guidCurGroupId)
                    continue;

                lstRet.Add(RoleMenu);
            }
        }
        //桌面应用
        lstObj = m_DesktopAppMgr.GetList();
        for (int i = 0; i < lstObj.Count; i++)
        {
            CDesktopApp App = (CDesktopApp)lstObj[i];
            if (App.UI_DesktopGroup_id != m_guidCurGroupId)
                continue;

            lstRet.Add(App);
        }

        var varObj = from obj in lstRet
                     orderby obj.m_arrNewVal["idx"].IntVal
                     select obj;

        return varObj.ToList();
    }
}
