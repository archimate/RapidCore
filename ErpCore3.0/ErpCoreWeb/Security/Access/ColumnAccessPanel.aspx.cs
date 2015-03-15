using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.UI;

public partial class Security_Access_ColumnAccessPanel : System.Web.UI.Page
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

        if (!IsPostBack)
        {
            LoadTable();
        }
    }
    void LoadTable()
    {
        cbTable.Items.Clear();
        List<CBaseObject> lstObj = Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.GetList();
        foreach (CBaseObject obj in lstObj)
        {
            CTable table = (CTable)obj;
            ListItem item= new ListItem(table.Name,table.Id.ToString());
            cbTable.Items.Add(item);
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
        string FW_Table_id = Request["FW_Table_id"];
        CTable table = null;
        if (!string.IsNullOrEmpty(FW_Table_id))
            table = (CTable)Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.Find(new Guid(FW_Table_id));

        string sData = "";
        int iCount = 0;
        if (table != null)
        {
            List<CBaseObject> lstObj = table.ColumnMgr.GetList();
            iCount = lstObj.Count;

            foreach (CBaseObject obj in lstObj)
            {
                CColumn column = (CColumn)obj;
                int iRead = 0;
                int iWrite = 0;
                if (UType == "0" && user != null) //用户
                {
                    //管理员有所有权限
                    if (user.IsRole("管理员"))
                    {
                        iRead = 1;
                        iWrite = 1;
                    }
                    else
                    {
                        CColumnAccessInUser caiu = user.ColumnAccessInUserMgr.FindByColumn(column.Id);
                        if (caiu != null)
                        {
                            if (caiu.Access == AccessType.read)
                                iRead = 1;
                            else if (caiu.Access == AccessType.write)
                            {
                                iRead = 1;
                                iWrite = 1;
                            }
                        }
                        else  //没有手动设置字段权限， 则默认都可写
                        {
                            iRead = 1;
                            iWrite = 1;
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
                        CColumnAccessInRole cair = role.ColumnAccessInRoleMgr.FindByColumn(column.Id);
                        if (cair != null)
                        {
                            if (cair.Access == AccessType.read)
                                iRead = 1;
                            else if (cair.Access == AccessType.write)
                            {
                                iRead = 1;
                                iWrite = 1;
                            }
                        }
                        else  //没有手动设置字段权限， 则默认都可写
                        {
                            iRead = 1;
                            iWrite = 1;
                        }
                    }
                }
                string sRow = string.Format("\"id\":\"{0}\",\"Name\":\"{1}\",\"Read\":\"{2}\",\"Write\":\"{3}\",", column.Id, column.Name, iRead, iWrite);

                sRow = "{" + sRow + "},";
                sData += sRow;
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
        string UType = Request["UType"];
        string Uid = Request["Uid"];
        string FW_Table_id = Request["FW_Table_id"];
        string postData = Request["postData"];
        CUser user = null;
        CRole role = null;
        CTable table = (CTable)Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.Find(new Guid(FW_Table_id));
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
                Guid columnid = new Guid(arr2[0]);
                CColumnAccessInUser caiu = user.ColumnAccessInUserMgr.FindByColumn(columnid);
                if (caiu == null)
                {
                    caiu = new CColumnAccessInUser();
                    caiu.FW_Column_id = columnid;
                    caiu.B_User_id = user.Id;
                    if (arr2[2]=="1")
                        caiu.Access = AccessType.write;
                    else if (arr2[1] == "1")
                        caiu.Access = AccessType.read;
                    else
                        caiu.Access = AccessType.forbide;

                    CUser user0 = (CUser)Session["User"];
                    caiu.Creator = user0.Id;
                    user.ColumnAccessInUserMgr.AddNew(caiu);
                }
                else
                {
                    if (arr2[2] == "1")
                        caiu.Access = AccessType.write;
                    else if (arr2[1] == "1")
                        caiu.Access = AccessType.read;
                    else
                        caiu.Access = AccessType.forbide;

                    CUser user0 = (CUser)Session["User"];
                    caiu.Updator = user0.Id;
                    user.ColumnAccessInUserMgr.Update(caiu);
                }
            }
            if (!user.ColumnAccessInUserMgr.Save(true))
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
                Guid columnid = new Guid(arr2[0]);
                CColumnAccessInRole cair = role.ColumnAccessInRoleMgr.FindByColumn(columnid);
                if (cair == null)
                {
                    cair = new CColumnAccessInRole();
                    cair.FW_Column_id = columnid;
                    cair.B_Role_id = role.Id;
                    if (arr2[2] == "1")
                        cair.Access = AccessType.write;
                    else if (arr2[1] == "1")
                        cair.Access = AccessType.read;
                    else
                        cair.Access = AccessType.forbide;

                    CUser user0 = (CUser)Session["User"];
                    cair.Creator = user0.Id;
                    role.ColumnAccessInRoleMgr.AddNew(cair);
                }
                else
                {
                    if (arr2[2] == "1")
                        cair.Access = AccessType.write;
                    else if (arr2[1] == "1")
                        cair.Access = AccessType.read;
                    else
                        cair.Access = AccessType.forbide;

                    CUser user0 = (CUser)Session["User"];
                    cair.Updator = user0.Id;
                    role.ColumnAccessInRoleMgr.Update(cair);
                }
            }
            if (!role.ColumnAccessInRoleMgr.Save(true))
            {
                Response.Write("保存失败！");
            }
        }
    }
}
