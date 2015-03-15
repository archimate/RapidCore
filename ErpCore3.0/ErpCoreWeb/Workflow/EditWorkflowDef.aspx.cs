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

public partial class Workflow_EditWorkflowDef : System.Web.UI.Page
{
    public CWorkflowDef m_BaseObject = null;
    public CCompany m_Company = null;
    protected void Page_Load(object sender, EventArgs e)
    {
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

        string id = Request["id"];
        if (string.IsNullOrEmpty(id))
        {
            Response.End();
            return;
        }
        m_BaseObject = (CWorkflowDef)m_Company.WorkflowDefMgr.Find(new Guid(id));
        if (m_BaseObject == null)
        {
            Response.End();
            return;
        }

        //保存到编辑对象
        EditObject.Add(Session.SessionID, m_BaseObject);

        if (Request.Params["Action"] == "Cancel")
        {
            m_BaseObject.Cancel();
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
            //从编辑对象移除
            EditObject.Remove(Session.SessionID, m_BaseObject);

            Response.End();
        }
    }

    public CWorkflowDef GetWorkflowDef()
    {
        return m_BaseObject;
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
            string AType = "", UserName = "", RoleName = "";
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

        if (!GetWorkflowDef().Name.Equals(Name, StringComparison.OrdinalIgnoreCase))
        {
            if (m_Company.WorkflowDefMgr.FindByName(Name) != null)
            {
                Response.Write("相同名称的工作流已经存在！");
                return;
            }
        }
        GetWorkflowDef().Name = Name;
        if (Catalog_id != "")
            GetWorkflowDef().WF_WorkflowCatalog_id = new Guid(Catalog_id);
        GetWorkflowDef().FW_Table_id = new Guid(Table_id);


        CUser user = (CUser)Session["User"];
        GetWorkflowDef().Updator = user.Id;
        m_Company.WorkflowDefMgr.Update(GetWorkflowDef());

        if (!m_Company.WorkflowDefMgr.Save(true))
        {
            Response.Write("修改失败！");
            return;
        }

    }

    public string GetWorkflowCatalogName()
    {
        if (m_BaseObject == null)
            return "";
        CWorkflowCatalog Catalog = (CWorkflowCatalog)m_Company.WorkflowCatalogMgr.Find(m_BaseObject.WF_WorkflowCatalog_id);
        if (Catalog == null)
            return "";
        return Catalog.Name;
    }
    public string GetTableName()
    {
        if (m_BaseObject == null)
            return "";
        CTable Table = (CTable)Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.Find(m_BaseObject.FW_Table_id);
        if (Table == null)
            return "";
        return Table.Name;
    }
}
