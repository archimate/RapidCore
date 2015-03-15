using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.UI;

public partial class Security_Access_DesktopGroupAccessPanel : System.Web.UI.Page
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
        CUser user = null;
        CRole role = null;
        if (UType == "0") //用户
        {
            if (!string.IsNullOrEmpty(Uid))
            {
                user = (CUser)Global.GetCtx(Session["TopCompany"].ToString()).UserMgr.Find(new Guid(Uid));
            }
        }
        else if (UType == "1") //角色
        {
            if (!string.IsNullOrEmpty(Uid))
            {
                role = (CRole)m_Company.RoleMgr.Find(new Guid(Uid));
            }
        }

        string sData = "";
        List<CBaseObject> lstObj = Global.GetCtx(Session["TopCompany"].ToString()).DesktopGroupMgr.GetList();

        foreach (CBaseObject obj in lstObj)
        {
            CDesktopGroup group = (CDesktopGroup)obj;
            int iRead = 0;
            int iWrite = 0;
            if (UType == "0" && user!=null) //用户
            {
                //管理员有所有权限
                if (user.IsRole("管理员"))
                {
                    iRead = 1;
                    iWrite = 1;
                }
                else
                {
                    CDesktopGroupAccessInUser dgaiu = user.DesktopGroupAccessInUserMgr.FindByDesktopGroup(group.Id);
                    if (dgaiu != null)
                    {
                        if (dgaiu.Access == AccessType.read)
                            iRead = 1;
                        else if (dgaiu.Access == AccessType.write)
                        {
                            iRead = 1;
                            iWrite = 1;
                        }
                    }
                }
            }
            else if (UType == "1" && role != null) //用户
            {
                //管理员有所有权限
                if (role.Name == "管理员")
                {
                    iRead = 1;
                    iWrite = 1;
                }
                else
                {
                    CDesktopGroupAccessInRole dgair = role.DesktopGroupAccessInRoleMgr.FindByDesktopGroup(group.Id);
                    if (dgair != null)
                    {
                        if (dgair.Access == AccessType.read)
                            iRead = 1;
                        else if (dgair.Access == AccessType.write)
                        {
                            iRead = 1;
                            iWrite = 1;
                        }
                    }
                }
            }
            string sRow = string.Format("\"id\":\"{0}\",\"Name\":\"{1}\",\"Read\":\"{2}\",\"Write\":\"{3}\",", group.Id, group.Name, iRead, iWrite);

            sRow = "{" + sRow + "},";
            sData += sRow;
        }
        sData = sData.TrimEnd(",".ToCharArray());
        sData = "[" + sData + "]";
        string sJson = string.Format("{{\"Rows\":{0},\"Total\":\"{1}\"}}"
            , sData, lstObj.Count);

        Response.Write(sJson);
    }

    void PostData()
    {
        string UType = Request["UType"];
        string Uid = Request["Uid"];
        string postData = Request["postData"];
        CUser user = null;
        CRole role = null;
        if (UType == "0") //用户
        {
            user = (CUser)Global.GetCtx(Session["TopCompany"].ToString()).UserMgr.Find(new Guid(Uid));
            //管理员有所有权限，不能修改！
            if (user.IsRole("管理员"))
            {
                Response.Write("管理员有所有权限，不能修改！");
                return;
            }
            //
            string[] arr1= postData.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            foreach (string sItem1 in arr1)
            {
                string[] arr2 = sItem1.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                Guid groupid = new Guid(arr2[0]);
                CDesktopGroupAccessInUser dgaiu = user.DesktopGroupAccessInUserMgr.FindByDesktopGroup(groupid);
                if (dgaiu == null)
                {
                    dgaiu = new CDesktopGroupAccessInUser();
                    dgaiu.UI_DesktopGroup_id = groupid;
                    dgaiu.B_User_id = user.Id;
                    if (arr2[2]=="1")
                        dgaiu.Access = AccessType.write;
                    else if (arr2[1] == "1")
                        dgaiu.Access = AccessType.read;
                    else
                        dgaiu.Access = AccessType.forbide;

                    CUser user0 = (CUser)Session["User"];
                    dgaiu.Creator = user0.Id;
                    user.DesktopGroupAccessInUserMgr.AddNew(dgaiu);
                }
                else
                {
                    if (arr2[2] == "1")
                        dgaiu.Access = AccessType.write;
                    else if (arr2[1] == "1")
                        dgaiu.Access = AccessType.read;
                    else
                        dgaiu.Access = AccessType.forbide;

                    CUser user0 = (CUser)Session["User"];
                    dgaiu.Updator = user0.Id;
                    user.DesktopGroupAccessInUserMgr.Update(dgaiu);
                }
            }
            if (!user.DesktopGroupAccessInUserMgr.Save(true))
            {
                Response.Write("保存失败！");
            }
        }
        else if (UType == "1") //角色
        {
            role = (CRole)m_Company.RoleMgr.Find(new Guid(Uid));
            //管理员有所有权限，不能修改！
            if (role.Name == "管理员")
            {
                Response.Write("管理员有所有权限，不能修改！");
                return;
            }
            //
            string[] arr1 = postData.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            foreach (string sItem1 in arr1)
            {
                string[] arr2 = sItem1.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                Guid groupid = new Guid(arr2[0]);
                CDesktopGroupAccessInRole dgair = role.DesktopGroupAccessInRoleMgr.FindByDesktopGroup(groupid);
                if (dgair == null)
                {
                    dgair = new CDesktopGroupAccessInRole();
                    dgair.UI_DesktopGroup_id = groupid;
                    dgair.B_Role_id = role.Id;
                    if (arr2[2] == "1")
                        dgair.Access = AccessType.write;
                    else if (arr2[1] == "1")
                        dgair.Access = AccessType.read;
                    else
                        dgair.Access = AccessType.forbide;

                    CUser user0 = (CUser)Session["User"];
                    dgair.Creator = user0.Id;
                    role.DesktopGroupAccessInRoleMgr.AddNew(dgair);
                }
                else
                {
                    if (arr2[2] == "1")
                        dgair.Access = AccessType.write;
                    else if (arr2[1] == "1")
                        dgair.Access = AccessType.read;
                    else
                        dgair.Access = AccessType.forbide;

                    CUser user0 = (CUser)Session["User"];
                    dgair.Updator = user0.Id;
                    role.DesktopGroupAccessInRoleMgr.Update(dgair);
                }
            }
            if (!role.DesktopGroupAccessInRoleMgr.Save(true))
            {
                Response.Write("保存失败！");
            }
        }
    }
}
