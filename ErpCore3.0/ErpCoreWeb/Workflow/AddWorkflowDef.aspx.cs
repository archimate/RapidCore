using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.Workflow;

public partial class Workflow_AddWorkflowDef : System.Web.UI.Page
{
    public CCompany m_Company = null;
    public string m_sCatalogName = "";
    public Guid m_guidCatalogId = Guid.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null)
        {
            Response.Redirect("../Login.aspx");
            Response.End();
        }

        string B_Company_id = Request["B_Company_id"];
        if(string.IsNullOrEmpty( B_Company_id))
            m_Company=Global.GetCtx(Session["TopCompany"].ToString()).CompanyMgr.FindTopCompany();
        else
            m_Company = (CCompany)Global.GetCtx(Session["TopCompany"].ToString()).CompanyMgr.Find(new Guid(B_Company_id));

        if (Request.Params["Action"] == "Cancel")
        {
            GetWorkflowDef().Cancel();
            Session["AddWorkflowDef"] = null;
            Response.End();
        }
        else if (Request.Params["Action"] == "GetActivesData")
        {
            GetActivesData();
            Response.End();
        }
        else if (Request.Params["Action"] == "GetLinkData")
        {
            GetLinkData();
            Response.End();
        }
        else if (Request.Params["Action"] == "DeleteActivesDef")
        {
            DeleteActivesDef();
            Response.End();
        }
        else if (Request.Params["Action"] == "SelectTable")
        {
            SelectTable();
            Response.End();
        }
        else if (Request.Params["Action"] == "DeleteLink")
        {
            DeleteLink();
            Response.End();
        }       
        else if (Request.Params["Action"] == "PostData")
        {
            PostData();
            Response.End();
        }

        if (!IsPostBack)
        {
            string catalog_id = Request["catalog_id"];
            if (!string.IsNullOrEmpty(catalog_id))
            {
                CWorkflowCatalog Catalog =(CWorkflowCatalog) m_Company.WorkflowCatalogMgr.Find(new Guid(catalog_id));
                if (Catalog != null)
                {
                    m_sCatalogName = Catalog.Name;
                    m_guidCatalogId = Catalog.Id;
                }
            }
        }
    }

    public CWorkflowDef GetWorkflowDef()
    {
        if (Session["AddWorkflowDef"] == null)
        {
            CUser user = (CUser)Session["User"];
            CWorkflowDef WorkflowDef = new CWorkflowDef();
            WorkflowDef.Ctx = Global.GetCtx(Session["TopCompany"].ToString());
            Guid Catalog_id = Guid.Empty;
            if (!string.IsNullOrEmpty(Request["catalog_id"]))
                Catalog_id = new Guid(Request["catalog_id"]);
            WorkflowDef.WF_WorkflowCatalog_id = Catalog_id;
            WorkflowDef.B_Company_id = m_Company.Id;
            WorkflowDef.Creator = user.Id;
            CActivesDef startActivesDef = new CActivesDef();
            startActivesDef.Ctx = Global.GetCtx(Session["TopCompany"].ToString());
            startActivesDef.WF_WorkflowDef_id = WorkflowDef.Id;
            startActivesDef.WType = ActivesType.Start;
            startActivesDef.Name = "启动";
            startActivesDef.Idx = 0;
            startActivesDef.Creator = user.Id;
            WorkflowDef.ActivesDefMgr.AddNew(startActivesDef);

            CActivesDef SuccessActivesDef = new CActivesDef();
            SuccessActivesDef.Ctx = Global.GetCtx(Session["TopCompany"].ToString());
            SuccessActivesDef.WF_WorkflowDef_id = WorkflowDef.Id;
            SuccessActivesDef.WType = ActivesType.Success;
            SuccessActivesDef.Name = "成功结束";
            SuccessActivesDef.Idx = -1;
            SuccessActivesDef.Creator = user.Id;
            WorkflowDef.ActivesDefMgr.AddNew(SuccessActivesDef);

            CActivesDef FailureActivesDef = new CActivesDef();
            FailureActivesDef.Ctx = Global.GetCtx(Session["TopCompany"].ToString());
            FailureActivesDef.WF_WorkflowDef_id = WorkflowDef.Id;
            FailureActivesDef.WType = ActivesType.Failure;
            FailureActivesDef.Name = "失败结束";
            FailureActivesDef.Idx = -2;
            FailureActivesDef.Creator = user.Id;
            WorkflowDef.ActivesDefMgr.AddNew(FailureActivesDef);

            CLink Link = new CLink();
            Link.Ctx = Global.GetCtx(Session["TopCompany"].ToString());
            Link.WF_WorkflowDef_id = WorkflowDef.Id;
            Link.Result = enumApprovalResult.Accept;
            Link.PreActives = startActivesDef.Id;
            Link.NextActives = SuccessActivesDef.Id;
            Link.Creator = user.Id;
            WorkflowDef.LinkMgr.AddNew(Link);

            Session["AddWorkflowDef"] = WorkflowDef;
        }
        return (CWorkflowDef)Session["AddWorkflowDef"];
    }

    void GetActivesData()
    {
        CWorkflowDef WorkflowDef = GetWorkflowDef();
        List<CBaseObject> lstObj= WorkflowDef.ActivesDefMgr.GetList();
        //按序号排序
        SortedList<int, CActivesDef> sortObj = new SortedList<int, CActivesDef>();
        foreach (CBaseObject obj in lstObj)
        {
            CActivesDef ActivesDef = (CActivesDef)obj;
            sortObj.Add(ActivesDef.Idx, ActivesDef);
        }
        CActivesDef SuccessActivesDef = null;
        CActivesDef FailureActivesDef = null;

        string sData = "";
        foreach (KeyValuePair<int, CActivesDef> pair in sortObj)
        {
            CActivesDef ActivesDef = pair.Value;
            if (ActivesDef.WType == ActivesType.Success)
            {
                SuccessActivesDef = ActivesDef;
                continue;
            }
            if (ActivesDef.WType == ActivesType.Failure)
            {
                FailureActivesDef = ActivesDef;
                continue;
            }
            CUser User = (CUser)Global.GetCtx(Session["TopCompany"].ToString()).UserMgr.Find(ActivesDef.B_User_id);
            CRole Role = (CRole)Global.GetCtx(Session["TopCompany"].ToString()).CompanyMgr.FindTopCompany().RoleMgr.Find(ActivesDef.B_Role_id);
            string AType="", UserName = "", RoleName = "";
            AType = ActivesDef.AType;
            UserName = (User != null) ? User.Name : "";
            RoleName = (Role != null) ? Role.Name : "";

            sData += string.Format("{{ \"id\": \"{0}\",\"Name\":\"{1}\", \"Idx\":\"{2}\", \"WType\":\"{3}\", \"AType\":\"{4}\", \"UserName\":\"{5}\", \"RoleName\":\"{6}\" }},"
                , ActivesDef.Id
                , ActivesDef.Name
                , ActivesDef.Idx
                , ActivesDef.WType
                , AType
                , UserName
                , RoleName);
        }

        //成功/失败结束活动放最后
        if (SuccessActivesDef != null)
        {
            sData += string.Format("{{ \"id\": \"{0}\",\"Name\":\"{1}\", \"Idx\":\"{2}\", \"WType\":\"{3}\", \"B_User_id\":\"{4}\", \"UserName\":\"{5}\" }},"
                , SuccessActivesDef.Id
                , SuccessActivesDef.Name
                , SuccessActivesDef.Idx
                , SuccessActivesDef.WType
                , SuccessActivesDef.B_User_id
                , "");
        }
        if (FailureActivesDef != null)
        {
            sData += string.Format("{{ \"id\": \"{0}\",\"Name\":\"{1}\", \"Idx\":\"{2}\", \"WType\":\"{3}\", \"B_User_id\":\"{4}\", \"UserName\":\"{5}\" }},"
                , FailureActivesDef.Id
                , FailureActivesDef.Name
                , FailureActivesDef.Idx
                , FailureActivesDef.WType
                , FailureActivesDef.B_User_id
                , "");
        }

        sData = "[" + sData + "]";
        string sJson = string.Format("{{\"Rows\":{0},\"Total\":\"{1}\"}}"
            , sData, lstObj.Count);

        Response.Write(sJson);
    }
    void GetLinkData()
    {
        string ActivesId = Request["ActivesId"];
        if (string.IsNullOrEmpty(ActivesId))
        {
            return;
        }
        Guid guidActivesId = new Guid(ActivesId);

        CWorkflowDef WorkflowDef = GetWorkflowDef();
        List<CBaseObject> lstObj = WorkflowDef.LinkMgr.GetList();

        string sData = "";
        int iCount = 0;
        foreach (CBaseObject obj in lstObj)
        {
            CLink Link = (CLink)obj;
            if (Link.PreActives != guidActivesId)
                continue;
            CActivesDef next = (CActivesDef)WorkflowDef.ActivesDefMgr.Find(Link.NextActives);

            sData += string.Format("{{ \"id\": \"{0}\",\"Result\":\"{1}\",\"ResultName\":\"{2}\", \"Condiction\":\"{3}\", \"NextActives\":\"{4}\", \"NextActivesName\":\"{5}\"}},"
                , Link.Id
                , Link.Result
                ,(Link.Result== enumApprovalResult.Accept)?"接受":"拒绝"
                , Link.Condiction
                , Link.NextActives
                ,(next!=null)?next.Name:"");
            iCount++;
        }
        sData = "[" + sData + "]";
        string sJson = string.Format("{{\"Rows\":{0},\"Total\":\"{1}\"}}"
            , sData, iCount);

        Response.Write(sJson);
    }

    void DeleteActivesDef()
    {
        string delid = Request["delid"];
        if (string.IsNullOrEmpty(delid))
        {
            Response.Write("请选择活动！");
            return;
        }
        GetWorkflowDef().ActivesDefMgr.Delete(new Guid(delid));
    }
    void SelectTable()
    {
        string Table_id = Request["Table_id"];
        GetWorkflowDef().FW_Table_id = new Guid(Table_id);
    }

    void DeleteLink()
    {
        string delid = Request["delid"];
        if (string.IsNullOrEmpty(delid))
        {
            Response.Write("请选择连接！");
            return;
        }
        GetWorkflowDef().LinkMgr.Delete(new Guid(delid));
    }
    void PostData()
    {
        string Name = Request["Name"];
        string Catalog_id = Request["Catalog_id"];
        string Table_id = Request["Table_id"];


        if (string.IsNullOrEmpty(Name))
        {
            Response.Write("名称不能空！");
            return;
        }
        if (string.IsNullOrEmpty(Table_id))
        {
            Response.Write("请选择表对象！");
            return;
        }

        if (m_Company.WorkflowDefMgr.FindByName(Name) != null)
        {
            Response.Write("相同名称的工作流已经存在！");
            return;
        }
        GetWorkflowDef().Name = Name;
        if (Catalog_id != "")
            GetWorkflowDef().WF_WorkflowCatalog_id = new Guid(Catalog_id);
        GetWorkflowDef().FW_Table_id = new Guid(Table_id);


        m_Company.WorkflowDefMgr.AddNew(GetWorkflowDef());

        if (!m_Company.WorkflowDefMgr.Save(true))
        {
            Response.Write("添加失败！");
            return;
        }

    }
}
