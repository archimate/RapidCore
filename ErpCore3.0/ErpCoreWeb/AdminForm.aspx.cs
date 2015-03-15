using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.Workflow;
using ErpCoreModel.Report;
using ErpCoreModel.UI;

public partial class AdminForm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetNoStore();
        if (Session["User"] == null)
        {
            Response.Redirect("Login.aspx");
            Response.End();
        }

        string Action = Request["Action"];
        if (Action == null) Action = "";

        if (Action.Equals("GetWorkflowCatalog", StringComparison.OrdinalIgnoreCase))
        {
            GetWorkflowCatalog();
            Response.End();
        }
        else if (Action.Equals("GetWorkflowCatalogName", StringComparison.OrdinalIgnoreCase))
        {
            GetWorkflowCatalogName();
            Response.End();
        }
        else if (Action.Equals("DelWorkflowCatalog", StringComparison.OrdinalIgnoreCase))
        {
            DelWorkflowCatalog();
            Response.End();
        }
        else if (Action.Equals("GetReportCatalog", StringComparison.OrdinalIgnoreCase))
        {
            GetReportCatalog();
            Response.End();
        }
        else if (Action.Equals("GetReportCatalogName", StringComparison.OrdinalIgnoreCase))
        {
            GetReportCatalogName();
            Response.End();
        }
        else if (Action.Equals("DelReportCatalog", StringComparison.OrdinalIgnoreCase))
        {
            DelReportCatalog();
            Response.End();
        }
        else if (Action.Equals("GetReportCompany", StringComparison.OrdinalIgnoreCase))
        {
            GetReportCompany();
            Response.End();
        }
        else if (Action.Equals("GetWorkflowCompany", StringComparison.OrdinalIgnoreCase))
        {
            GetWorkflowCompany();
            Response.End();
        }
        else if (Action.Equals("GetViewCatalog", StringComparison.OrdinalIgnoreCase))
        {
            GetViewCatalog();
            Response.End();
        }
        else if (Action.Equals("GetViewCatalogName", StringComparison.OrdinalIgnoreCase))
        {
            GetViewCatalogName();
            Response.End();
        }
        else if (Action.Equals("DelViewCatalog", StringComparison.OrdinalIgnoreCase))
        {
            DelViewCatalog();
            Response.End();
        }
        else if (Action.Equals("GetDesktopGroup", StringComparison.OrdinalIgnoreCase))
        {
            GetDesktopGroup();
            Response.End();
        }
        else if (Action.Equals("GetDesktopGroupName", StringComparison.OrdinalIgnoreCase))
        {
            GetDesktopGroupName();
            Response.End();
        }
        else if (Action.Equals("DelDesktopGroup", StringComparison.OrdinalIgnoreCase))
        {
            DelDesktopGroup();
            Response.End();
        }
        else if (Action.Equals("GetSecurityCompany", StringComparison.OrdinalIgnoreCase))
        {
            GetSecurityCompany();
            Response.End();
        }
        else if (Action.Equals("GetSecurityCatalog", StringComparison.OrdinalIgnoreCase))
        {
            GetSecurityCatalog();
            Response.End();
        }
        else if (Action.Equals("GetSecurityAccess", StringComparison.OrdinalIgnoreCase))
        {
            GetSecurityAccess();
            Response.End();
        }

    }
    #region 工作流
    //获取工作流目录
    void GetWorkflowCatalog()
    {
        string B_Company_id = Request["B_Company_id"];
        if (string.IsNullOrEmpty(B_Company_id))
            return;
        CCompany Company = (CCompany)Global.GetCtx(Session["TopCompany"].ToString()).CompanyMgr.Find(new Guid(B_Company_id));
        if (Company == null)
            return;
        string pid = Request["pid"];
        Guid Parent_id = Guid.Empty;
        if (!string.IsNullOrEmpty(pid))
            Parent_id = new Guid(pid);
        //context.Response.Write(@"[{text: '工作流'}]");
        string sJson = "[";
        List<CBaseObject> lstWorkflowCatalog = Company.WorkflowCatalogMgr.GetList();
        foreach (CBaseObject obj in lstWorkflowCatalog)
        {
            CWorkflowCatalog catalog = (CWorkflowCatalog)obj;
            if (catalog.Parent_id == Parent_id)
            {
                string sItem = string.Format("{{ isexpand: \"false\", name: \"nodeWorkflowCatalog\",\"id\":\"{0}\",\"text\": \"{1}\",\"url\": \"Workflow/WorkflowDefPanel.aspx?catalog_id={0}&B_Company_id={2}\",\"B_Company_id\":\"{2}\", children: [] }},",
                    catalog.Id,
                    catalog.Name,
                    B_Company_id);
                sJson += sItem;
            }
        }
        sJson = sJson.TrimEnd(",".ToCharArray());
        sJson += "]";
        Response.Write(sJson);
    }
    
    
    //获取工作流目录名
    void GetWorkflowCatalogName()
    {
        string B_Company_id = Request["B_Company_id"];
        if (string.IsNullOrEmpty(B_Company_id))
            return;
        CCompany Company = (CCompany)Global.GetCtx(Session["TopCompany"].ToString()).CompanyMgr.Find(new Guid(B_Company_id));
        if (Company == null)
            return;
        string id = Request["id"];
        Guid guid = Guid.Empty;
        if (!string.IsNullOrEmpty(id))
            guid = new Guid(id);
        else
            return;

        CWorkflowCatalog catalog = (CWorkflowCatalog)Company.WorkflowCatalogMgr.Find(guid);
        if(catalog!=null)
            Response.Write(catalog.Name);
    }

    //删除工作流目录
    void DelWorkflowCatalog()
    {
        string B_Company_id = Request["B_Company_id"];
        if (string.IsNullOrEmpty(B_Company_id))
            return;
        CCompany Company = (CCompany)Global.GetCtx(Session["TopCompany"].ToString()).CompanyMgr.Find(new Guid(B_Company_id));
        if (Company == null)
            return;
        string delid = Request["delid"];
        if (string.IsNullOrEmpty(delid))
        {
            Response.Write("请选择目录！");
            return;
        }
        Guid guid = new Guid(delid);

        Company.WorkflowCatalogMgr.Delete(guid);
        if (!Company.WorkflowCatalogMgr.Save(true))  
        {
            Response.Write("删除失败！");
        }
    }
    //获取工作流的单位目录
    void GetWorkflowCompany()
    {
        string sJson = "[";
        List<CBaseObject> lstObj = Global.GetCtx(Session["TopCompany"].ToString()).CompanyMgr.GetList();
        foreach (CBaseObject obj in lstObj)
        {
            CCompany Company = (CCompany)obj;

            string sItem = string.Format("{{ isexpand: \"false\", name: \"nodeWorkflowCompany\",\"id\":\"{0}\",\"text\": \"{1}\",\"url\": \"Workflow/WorkflowDefPanel.aspx?B_Company_id={0}\", children: [] }},",
                Company.Id,
                Company.Name);
            sJson += sItem;
        }
        sJson = sJson.TrimEnd(",".ToCharArray());
        sJson += "]";
        Response.Write(sJson);
    }

    #endregion 工作流


    #region 报表
    //获取报表目录
    void GetReportCatalog()
    {
        string B_Company_id = Request["B_Company_id"];
        if (string.IsNullOrEmpty(B_Company_id))
            return;
        CCompany Company = (CCompany)Global.GetCtx(Session["TopCompany"].ToString()).CompanyMgr.Find(new Guid(B_Company_id));
        if (Company == null)
            return;
        string pid = Request["pid"];
        Guid Parent_id = Guid.Empty;
        if (!string.IsNullOrEmpty(pid))
            Parent_id = new Guid(pid);
        //context.Response.Write(@"[{text: 'Report'}]");
        string sJson = "[";
        List<CBaseObject> lstReportCatalog = Company.ReportCatalogMgr.GetList();
        foreach (CBaseObject obj in lstReportCatalog)
        {
            CReportCatalog catalog = (CReportCatalog)obj;
            if (catalog.Parent_id == Parent_id)
            {
                string sItem = string.Format("{{ isexpand: \"false\", name: \"nodeReportCatalog\",\"id\":\"{0}\",\"text\": \"{1}\",\"url\": \"Report/ReportPanel.aspx?catalog_id={0}&B_Company_id={2}\",\"B_Company_id\":\"{2}\", children: [] }},",
                    catalog.Id,
                    catalog.Name,
                    B_Company_id);
                sJson += sItem;
            }
        }
        sJson = sJson.TrimEnd(",".ToCharArray());
        sJson += "]";
        Response.Write(sJson);
    }


    //获取报表目录名
    void GetReportCatalogName()
    {
        string B_Company_id = Request["B_Company_id"];
        if (string.IsNullOrEmpty(B_Company_id))
            return;
        CCompany Company = (CCompany)Global.GetCtx(Session["TopCompany"].ToString()).CompanyMgr.Find(new Guid(B_Company_id));
        if (Company == null)
            return;
        string id = Request["id"];
        Guid guid = Guid.Empty;
        if (!string.IsNullOrEmpty(id))
            guid = new Guid(id);
        else
            return;

        CReportCatalog catalog = (CReportCatalog)Company.ReportCatalogMgr.Find(guid);
        if (catalog != null)
            Response.Write(catalog.Name);
    }

    //删除报表目录
    void DelReportCatalog()
    {
        string B_Company_id = Request["B_Company_id"];
        if (string.IsNullOrEmpty(B_Company_id))
            return;
        CCompany Company = (CCompany)Global.GetCtx(Session["TopCompany"].ToString()).CompanyMgr.Find(new Guid(B_Company_id));
        if (Company == null)
            return;
        string delid = Request["delid"];
        if (string.IsNullOrEmpty(delid))
        {
            Response.Write("请选择目录！");
            return;
        }
        Guid guid = new Guid(delid);

        Company.ReportCatalogMgr.Delete(guid);
        if (!Company.ReportCatalogMgr.Save(true))
        {
            Response.Write("删除失败！");
        }
    }
    //获取报表的单位目录
    void GetReportCompany()
    {
        string sJson = "[";
        List<CBaseObject> lstObj = Global.GetCtx(Session["TopCompany"].ToString()).CompanyMgr.GetList();
        foreach (CBaseObject obj in lstObj)
        {
            CCompany Company = (CCompany)obj;

            string sItem = string.Format("{{ isexpand: \"false\", name: \"nodeReportCompany\",\"id\":\"{0}\",\"text\": \"{1}\",\"url\": \"Report/ReportPanel.aspx?B_Company_id={0}\", children: [] }},",
                Company.Id,
                Company.Name);
            sJson += sItem;
        }
        sJson = sJson.TrimEnd(",".ToCharArray());
        sJson += "]";
        Response.Write(sJson);
    }

    #endregion 报表


    #region 视图
    //获取视图目录
    void GetViewCatalog()
    {
        string pid = Request["pid"];
        Guid Parent_id = Guid.Empty;
        if (!string.IsNullOrEmpty(pid))
            Parent_id = new Guid(pid);
        //context.Response.Write(@"[{text: 'Report'}]");
        string sJson = "[";
        List<CBaseObject> lstViewCatalog = Global.GetCtx(Session["TopCompany"].ToString()).ViewCatalogMgr.GetList();
        foreach (CBaseObject obj in lstViewCatalog)
        {
            CViewCatalog catalog = (CViewCatalog)obj;
            if (catalog.Parent_id == Parent_id)
            {
                string sItem = string.Format("{{ isexpand: \"false\", name: \"nodeViewCatalog\",\"id\":\"{0}\",\"text\": \"{1}\",\"url\": \"View/ViewPanel.aspx?catalog_id={0}\", children: [] }},",
                    catalog.Id,
                    catalog.Name);
                sJson += sItem;
            }
        }
        sJson = sJson.TrimEnd(",".ToCharArray());
        sJson += "]";
        Response.Write(sJson);
    }

    //获取视图目录名
    void GetViewCatalogName()
    {
        string id = Request["id"];
        Guid guid = Guid.Empty;
        if (!string.IsNullOrEmpty(id))
            guid = new Guid(id);
        else
            return;

        CViewCatalog catalog = (CViewCatalog)Global.GetCtx(Session["TopCompany"].ToString()).ViewCatalogMgr.Find(guid);
        if (catalog != null)
            Response.Write(catalog.Name);
    }
    //删除视图目录
    void DelViewCatalog()
    {
        string delid = Request["delid"];
        if (string.IsNullOrEmpty(delid))
        {
            Response.Write("请选择目录！");
            return;
        }
        Guid guid = new Guid(delid);

        CViewCatalogMgr ViewCatalogMgr = Global.GetCtx(Session["TopCompany"].ToString()).ViewCatalogMgr;
        ViewCatalogMgr.Delete(guid);
        if (!ViewCatalogMgr.Save(true))
        {
            Response.Write("删除失败！");
        }
    }
    #endregion 视图

    #region 桌面组
    //获取桌面组
    void GetDesktopGroup()
    {
        string sJson = "[";
        List<CBaseObject> lstDesktopGroupCatalog = Global.GetCtx(Session["TopCompany"].ToString()).DesktopGroupMgr.GetList();
        foreach (CBaseObject obj in lstDesktopGroupCatalog)
        {
            CDesktopGroup group = (CDesktopGroup)obj;

            string sItem = string.Format("{{ isexpand: \"false\", name: \"nodeDesktopGroup\",\"id\":\"{0}\",\"text\": \"{1}\",\"url\": \"\", children: [] }},",
                group.Id,
                group.Name);
            sJson += sItem;
        }
        sJson = sJson.TrimEnd(",".ToCharArray());
        sJson += "]";
        Response.Write(sJson);
    }

    //获取桌面组名
    void GetDesktopGroupName()
    {
        string id = Request["id"];
        Guid guid = Guid.Empty;
        if (!string.IsNullOrEmpty(id))
            guid = new Guid(id);
        else
            return;

        CDesktopGroup group = (CDesktopGroup)Global.GetCtx(Session["TopCompany"].ToString()).ViewCatalogMgr.Find(guid);
        if (group != null)
            Response.Write(group.Name);
    }
    //删除桌面组
    void DelDesktopGroup()
    {
        string delid = Request["delid"];
        if (string.IsNullOrEmpty(delid))
        {
            Response.Write("请选择目录！");
            return;
        }
        Guid guid = new Guid(delid);

        CDesktopGroupMgr DesktopGroupMgr = Global.GetCtx(Session["TopCompany"].ToString()).DesktopGroupMgr;
        DesktopGroupMgr.Delete(guid);
        if (!DesktopGroupMgr.Save(true))
        {
            Response.Write("删除失败！");
        }
    }
    #endregion 桌面组

    #region 安全性
    //获取安全性的单位目录
    void GetSecurityCompany()
    {
        string sJson = "[";
        List<CBaseObject> lstObj = Global.GetCtx(Session["TopCompany"].ToString()).CompanyMgr.GetList();
        foreach (CBaseObject obj in lstObj)
        {
            CCompany Company = (CCompany)obj;

            string sItem = string.Format("{{ isexpand: \"false\", name: \"nodeSecurityCompany\",\"id\":\"{0}\",\"text\": \"{1}\", children: [] }},",
                Company.Id,
                Company.Name);
            sJson += sItem;
        }
        sJson = sJson.TrimEnd(",".ToCharArray());
        sJson += "]";
        Response.Write(sJson);
    }
    //获取安全性目录
    void GetSecurityCatalog()
    {
        string B_Company_id = Request["B_Company_id"];
        if (string.IsNullOrEmpty(B_Company_id))
            return;
        CCompany Company = (CCompany)Global.GetCtx(Session["TopCompany"].ToString()).CompanyMgr.Find(new Guid(B_Company_id));
        if (Company == null)
            return;

        string sJson = "[";

        string sItem = string.Format("{{ isexpand: \"false\", name: \"nodeSecurityUser\",\"id\":\"\",\"text\": \"{0}\",\"url\": \"Security/User/UserPanel.aspx?B_Company_id={1}\",\"B_Company_id\":\"{1}\" }},",
            "用户",
            B_Company_id);
        sJson += sItem;
        sItem = string.Format("{{ isexpand: \"false\", name: \"nodeSecurityOrg\",\"id\":\"\",\"text\": \"{0}\",\"url\": \"Security/Org/OrgPanel.aspx?B_Company_id={1}\",\"B_Company_id\":\"{1}\" }},",
            "组织",
            B_Company_id);
        sJson += sItem;
        sItem = string.Format("{{ isexpand: \"false\", name: \"nodeSecurityRole\",\"id\":\"\",\"text\": \"{0}\",\"url\": \"Security/Role/RolePanel.aspx?B_Company_id={1}\",\"B_Company_id\":\"{1}\" }},",
            "角色",
            B_Company_id);
        sJson += sItem;
        sItem = string.Format("{{ isexpand: \"false\", name: \"nodeSecurityAccess\",\"id\":\"\",\"text\": \"{0}\",\"B_Company_id\":\"{1}\",children: [] }},",
            "权限",
            B_Company_id);
        sJson += sItem;

        sJson = sJson.TrimEnd(",".ToCharArray());
        sJson += "]";
        Response.Write(sJson);
    }
    //获取权限目录
    void GetSecurityAccess()
    {
        string B_Company_id = Request["B_Company_id"];
        if (string.IsNullOrEmpty(B_Company_id))
            return;
        CCompany Company = (CCompany)Global.GetCtx(Session["TopCompany"].ToString()).CompanyMgr.Find(new Guid(B_Company_id));
        if (Company == null)
            return;

        string sJson = "[";

        string sItem = string.Format("{{ isexpand: \"false\", name: \"nodeSecurityDesktopGroupAccess\",\"id\":\"\",\"text\": \"{0}\",\"url\": \"Security/Access/DesktopGroupAccessPanel.aspx?B_Company_id={1}\",\"B_Company_id\":\"{1}\" }},",
            "桌面组权限",
            B_Company_id);
        sJson += sItem;
        sItem = string.Format("{{ isexpand: \"false\", name: \"nodeSecurityViewAccess\",\"id\":\"\",\"text\": \"{0}\",\"url\": \"Security/Access/ViewAccessPanel.aspx?B_Company_id={1}\",\"B_Company_id\":\"{1}\" }},",
            "视图权限",
            B_Company_id);
        sJson += sItem;
        sItem = string.Format("{{ isexpand: \"false\", name: \"nodeSecurityTableAccess\",\"id\":\"\",\"text\": \"{0}\",\"url\": \"Security/Access/TableAccessPanel.aspx?B_Company_id={1}\",\"B_Company_id\":\"{1}\" }},",
            "表权限",
            B_Company_id);
        sJson += sItem;
        sItem = string.Format("{{ isexpand: \"false\", name: \"nodeSecurityColumnAccess\",\"id\":\"\",\"text\": \"{0}\",\"url\": \"Security/Access/ColumnAccessPanel.aspx?B_Company_id={1}\",\"B_Company_id\":\"{1}\" }},",
            "字段权限",
            B_Company_id);
        sJson += sItem;
        sItem = string.Format("{{ isexpand: \"false\", name: \"nodeSecurityMenuAccess\",\"id\":\"\",\"text\": \"{0}\",\"url\": \"Security/Access/MenuAccessPanel.aspx?B_Company_id={1}\",\"B_Company_id\":\"{1}\" }},",
            "菜单权限",
            B_Company_id);
        sJson += sItem;
        sItem = string.Format("{{ isexpand: \"false\", name: \"nodeSecurityReportAccess\",\"id\":\"\",\"text\": \"{0}\",\"url\": \"Security/Access/ReportAccessPanel.aspx?B_Company_id={1}\",\"B_Company_id\":\"{1}\" }},",
            "报表权限",
            B_Company_id);
        sJson += sItem;

        sJson = sJson.TrimEnd(",".ToCharArray());
        sJson += "]";
        Response.Write(sJson);
    }
    #endregion 安全性
}