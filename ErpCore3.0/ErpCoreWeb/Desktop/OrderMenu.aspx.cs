using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ErpCoreModel.Framework;
using System.Collections.Generic;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.UI;

public partial class Desktop_OrderMenu : System.Web.UI.Page
{
    public CView m_View = null;
    public Guid m_Catalog_id = Guid.Empty;
    bool m_bIsNew = false;
    public CUserMenuMgr m_UserMenuMgr = null;
    public CTable m_Table = null;
    public Guid m_guidCurGroupId = Guid.Empty;
    public CCompany m_Company = null;
    public CDesktopAppMgr m_DesktopAppMgr = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null)
        {
            Response.Redirect("../Login.aspx");
            Response.End();
        }
        CUser user = (CUser)Session["User"];
        m_UserMenuMgr = user.UserMenuMgr;
        m_DesktopAppMgr = user.DesktopAppMgr;
        m_Company = (CCompany)Global.GetCtx(Session["TopCompany"].ToString()).CompanyMgr.Find(user.B_Company_id);
        string GroupId = Request["GroupId"];
        if (!string.IsNullOrEmpty(GroupId))
            m_guidCurGroupId = new Guid(GroupId);
        //string id = Request["id"];
        //if (!string.IsNullOrEmpty(id))
        //{
        //    m_View = (CView)Global.GetCtx(Session["TopCompany"].ToString()).ViewMgr.Find(new Guid(id));
        //}
        //else
        //{
        //    m_bIsNew = true;
        //    if (Session["NewView"] == null)
        //    {
        //        Response.Redirect("SingleViewInfo1.aspx?id=" + Request["id"] + "&catalog_id=" + Request["catalog_id"]);
        //        return;
        //    }
        //    else
        //    {
        //        SortedList<Guid, CView> sortObj = (SortedList<Guid, CView>)Session["NewView"];
        //        m_View = sortObj.Values[0];
        //    }
        //}
        //m_Table = (CTable)Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.Find(m_View.FW_Table_id);

        //string catalog_id = Request["catalog_id"];
        //if (!string.IsNullOrEmpty(catalog_id))
        //{
        //    m_Catalog_id = new Guid(catalog_id);
        //}


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
        else if (Request.Params["Action"] == "Cancel")
        {
            Cancel();
            Response.End();
        }
    }

    void GetData()
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
        List<CBaseObject> lstObj1 = varObj.ToList();
        



       // List<CBaseObject> lstObj = m_View.ColumnInViewMgr.GetList();
        //List<CColumnInView> sortObj = new List<CColumnInView>();
        //foreach (CBaseObject obj in lstObj)
        //{
        //    CColumnInView civ = (CColumnInView)obj;
        //    sortObj.Add(civ);
        //}
        //sortObj.Sort();

        string sData = "";

        int iCount = 0;
        foreach (CBaseObject civ in lstObj1)
        {
            //CColumn col = (CColumn)m_Table.ColumnMgr.Find(civ.FW_Column_id);
            //if (col == null)
            //    continue;
            if (typeof(CUserMenu) == civ.GetType())
            {
                CUserMenu UserMenu = (CUserMenu)civ;
                CMenu menu = (CMenu)Global.GetCtx(Session["TopCompany"].ToString()).MenuMgr.Find(UserMenu.UI_Menu_id);
                sData += string.Format("{{ \"id\": \"{0}\",\"ColName\":\"{1}\"}},", menu.Id, menu.Name);

                iCount++;
            }
            else if (typeof(CRoleMenu) == civ.GetType())
            {
                CRoleMenu RoleMenu = (CRoleMenu)civ;
                CMenu menu = (CMenu)Global.GetCtx(Session["TopCompany"].ToString()).MenuMgr.Find(RoleMenu.UI_Menu_id);

                sData += string.Format("{{ \"id\": \"{0}\",\"ColName\":\"{1}\"}},", menu.Id, menu.Name);

                iCount++;
            }
            else if (typeof(CDesktopApp) == civ.GetType())
            {
                CDesktopApp App = (CDesktopApp)civ;
                sData += string.Format("{{ \"id\": \"{0}\",\"ColName\":\"{1}\"}},", App.Id, App.Name);
                iCount++;
            }
         }
        sData = sData.TrimEnd(",".ToCharArray());
        sData = "[" + sData + "]";
        string sJson = string.Format("{{\"Rows\":{0},\"Total\":\"{1}\"}}"
             , sData, iCount);

        Response.Write(sJson);
    }
    void PostData()
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
        List<CBaseObject> lstObj1 = varObj.ToList();

        string GridData = Request["GridData"];
        if (string.IsNullOrEmpty(GridData))
        {
            Response.Write("字段不能空！");
            return;
        }

        string[] arr1 = GridData.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < arr1.Length; i++)
        {
            string[] arr2 = arr1[i].Split(":".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            //CMenu menu = (CMenu)m_UserMenuMgr.Find(new Guid(arr2[0]));

            foreach (CBaseObject civ in lstObj1)
            {
                if (typeof(CUserMenu) == civ.GetType())
                {
                    CUserMenu UserMenu = (CUserMenu)civ;
                    if (UserMenu.UI_Menu_id == new Guid(arr2[0]))
                    {
                        UserMenu.Idx = i + 1;
                        m_UserMenuMgr.Update(UserMenu);
                        if (m_UserMenuMgr.Save(true))
                        {

                        }
                    }
                }
                else if (typeof(CRoleMenu) == civ.GetType())
                {
                    CRoleMenu RoleMenu = (CRoleMenu)civ;
                    if (RoleMenu.UI_Menu_id == new Guid(arr2[0]))
                    {
                        RoleMenu.Idx = i + 1;
                        RoleMenu.m_ObjectMgr.Update(RoleMenu);
                        if (RoleMenu.m_ObjectMgr.Save(true))
                        { }
                    }
                }
                else if (typeof(CDesktopApp) == civ.GetType())
                {
                    CDesktopApp App = (CDesktopApp)civ;
                    if (App.Id == new Guid(arr2[0]))
                    {
                        App.Idx = i + 1;
                        m_DesktopAppMgr.Update(App);
                        if (m_DesktopAppMgr.Save(true))
                        { }
                    }
                }

            }

           // CUserMenu menu = (CUserMenu).GetCtx(Session["TopCompany"].ToString()).UserMenuMgr.Find(new Guid(arr2[0]));
           //// CMenu menu = (CMenu)Global.GetCtx(Session["TopCompany"].ToString()).MenuMgr.Find(new Guid(arr2[0]));
           // //CColumnInView civ = (CColumnInView)m_View.ColumnInViewMgr.Find(new Guid(arr2[0]));
           // //civ.Caption = arr2[1];
           // menu.Idx = i;
           // (CUserMenu).GetCtx(Session["TopCompany"].ToString()).UserMenuMgr.Update(menu);
           // m_View.ColumnInViewMgr.Update(menu);
        }
    }
    void Cancel()
    {
        Session["NewView"] = null;
    }
}
