using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.Workflow;

public partial class Workflow_AddLink : System.Web.UI.Page
{
    public CTable m_Table = null;
    public CWorkflowDef m_WorkflowDef = null;
    public CLinkMgr m_LinkMgr = null;
    public CActivesDef m_PreActives = null;
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

        string wfid = Request["wfid"];
        if (string.IsNullOrEmpty(wfid))
        {
            Response.End();
            return;
        }
        m_WorkflowDef = (CWorkflowDef)m_Company.WorkflowDefMgr.Find(new Guid(wfid));
        if (m_WorkflowDef == null) //可能是新建的
        {
            if (Session["AddWorkflowDef"] == null)
            {
                Response.End();
                return;
            }
            m_WorkflowDef = (CWorkflowDef)Session["AddWorkflowDef"];
        }
        string PreActives = Request["PreActives"];
        m_PreActives = (CActivesDef)m_WorkflowDef.ActivesDefMgr.Find(new Guid(PreActives));

        m_LinkMgr = m_WorkflowDef.LinkMgr;

        m_Table = m_LinkMgr.Table;

        if (Request.Params["Action"] == "Cancel")
        {
            m_LinkMgr.Cancel();
            Response.End();
        }
        else if (Request.Params["Action"] == "PostData")
        {
            PostData();
            Response.End();
        }
        else if (Request.Params["Action"] == "GetCondiction")
        {
            GetCondiction();
            Response.End();
        }
    }
    void PostData()
    {
        CUser user = (CUser)Session["User"];
        CLink BaseObject = new CLink();
        BaseObject.Ctx = Global.GetCtx(Session["TopCompany"].ToString());
        BaseObject.Creator = user.Id;
        
        List<CBaseObject> lstCol = m_Table.ColumnMgr.GetList();
        bool bHasVisible = false;
        foreach (CBaseObject obj in lstCol)
        {
            CColumn col = (CColumn)obj;

            if (col.Code.Equals("id", StringComparison.OrdinalIgnoreCase))
                continue;
            else if (col.Code.Equals("Created", StringComparison.OrdinalIgnoreCase))
                continue;
            else if (col.Code.Equals("Creator", StringComparison.OrdinalIgnoreCase))
            {
                //BaseObject.SetColValue(col, Program.User.Id);
                continue;
            }
            else if (col.Code.Equals("Updated", StringComparison.OrdinalIgnoreCase))
                continue;
            else if (col.Code.Equals("Updator", StringComparison.OrdinalIgnoreCase))
            {
                //BaseObject.SetColValue(col, Program.User.Id);
                continue;
            }

            BaseObject.SetColValue(col, Request.Params[col.Code]);
            bHasVisible = true;
        }
        if (!bHasVisible)
        {
            Response.Write("没有可修改字段！");
            return ;
        }
        m_LinkMgr.AddNew(BaseObject);
    }

    void GetCondiction()
    {
        string Column = Request["Column"];
        string Val = Request["Val"];

        CTable table = (CTable)Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.Find(m_WorkflowDef.FW_Table_id);
        if (table == null)
            return;
        CColumn col = table.ColumnMgr.FindByName(Column);
        if (col == null)
            return;
        if (col.ColType == ColumnType.int_type
                || col.ColType == ColumnType.long_type
                || col.ColType == ColumnType.numeric_type
                || col.ColType == ColumnType.bool_type)
        {
            if (Val == "")
                Val = "0";
            else
            {
                try { Convert.ToDouble(Val); }
                catch
                {
                    return;
                }
            }
        }
        else
        {
            if (Val == "")
                Val = "''";
            else
            {
                if (Val[0] != '\'')
                    Val = "\'" + Val;
                if (Val[Val.Length - 1] != '\'')
                    Val += "\'";
            }
        }

        Response.Write(Val);
    }
}
