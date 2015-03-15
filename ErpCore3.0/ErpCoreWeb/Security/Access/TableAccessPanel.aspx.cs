using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.UI;

public partial class Security_Access_TableAccessPanel : System.Web.UI.Page
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
        List<CBaseObject> lstObj = Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.GetList();

        foreach (CBaseObject obj in lstObj)
        {
            CTable table = (CTable)obj;
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
                    CTableAccessInUser taiu = user.TableAccessInUserMgr.FindByTable(table.Id);
                    if (taiu != null)
                    {
                        if (taiu.Access == AccessType.read)
                            iRead = 1;
                        else if (taiu.Access == AccessType.write)
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
                    CTableAccessInRole tair = role.TableAccessInRoleMgr.FindByTable(table.Id);
                    if (tair != null)
                    {
                        if (tair.Access == AccessType.read)
                            iRead = 1;
                        else if (tair.Access == AccessType.write)
                        {
                            iRead = 1;
                            iWrite = 1;
                        }
                    }
                }
            }
            string sRow = string.Format("\"id\":\"{0}\",\"Name\":\"{1}\",\"Read\":\"{2}\",\"Write\":\"{3}\",", table.Id, table.Name, iRead, iWrite);

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
                Guid tableid = new Guid(arr2[0]);
                CTableAccessInUser taiu = user.TableAccessInUserMgr.FindByTable(tableid);
                if (taiu == null)
                {
                    taiu = new CTableAccessInUser();
                    taiu.FW_Table_id = tableid;
                    taiu.B_User_id = user.Id;
                    if (arr2[2]=="1")
                        taiu.Access = AccessType.write;
                    else if (arr2[1] == "1")
                        taiu.Access = AccessType.read;
                    else
                        taiu.Access = AccessType.forbide;

                    CUser user0 = (CUser)Session["User"];
                    taiu.Creator = user0.Id;
                    user.TableAccessInUserMgr.AddNew(taiu);
                }
                else
                {
                    if (arr2[2] == "1")
                        taiu.Access = AccessType.write;
                    else if (arr2[1] == "1")
                        taiu.Access = AccessType.read;
                    else
                        taiu.Access = AccessType.forbide;

                    CUser user0 = (CUser)Session["User"];
                    taiu.Updator = user0.Id;
                    user.TableAccessInUserMgr.Update(taiu);
                }
            }
            if (!user.TableAccessInUserMgr.Save(true))
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
                Guid tableid = new Guid(arr2[0]);
                CTableAccessInRole tair = role.TableAccessInRoleMgr.FindByTable(tableid);
                if (tair == null)
                {
                    tair = new CTableAccessInRole();
                    tair.FW_Table_id = tableid;
                    tair.B_Role_id = role.Id;
                    if (arr2[2] == "1")
                        tair.Access = AccessType.write;
                    else if (arr2[1] == "1")
                        tair.Access = AccessType.read;
                    else
                        tair.Access = AccessType.forbide;

                    CUser user0 = (CUser)Session["User"];
                    tair.Creator = user0.Id;
                    role.TableAccessInRoleMgr.AddNew(tair);
                }
                else
                {
                    if (arr2[2] == "1")
                        tair.Access = AccessType.write;
                    else if (arr2[1] == "1")
                        tair.Access = AccessType.read;
                    else
                        tair.Access = AccessType.forbide;

                    CUser user0 = (CUser)Session["User"];
                    tair.Updator = user0.Id;
                    role.TableAccessInRoleMgr.Update(tair);
                }
            }
            if (!role.TableAccessInRoleMgr.Save(true))
            {
                Response.Write("保存失败！");
            }
        }
    }
}
