using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;

public partial class Security_Role_AddRole : System.Web.UI.Page
{
    public CTable m_Table = null;
    public CCompany m_Company = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        string B_Company_id = Request["B_Company_id"];
        if (string.IsNullOrEmpty(B_Company_id))
            m_Company = Global.GetCtx(Session["TopCompany"].ToString()).CompanyMgr.FindTopCompany();
        else
            m_Company = (CCompany)Global.GetCtx(Session["TopCompany"].ToString()).CompanyMgr.Find(new Guid(B_Company_id));

        m_Table = (CTable)m_Company.RoleMgr.Table;

        if (Request.Params["Action"] == "Cancel")
        {
            Response.End();
        }
        else if (Request.Params["Action"] == "PostData")
        {
            PostData();
            Response.End();
        }
    }
    void PostData()
    {
        CUser user = (CUser)Session["User"];
        CRole BaseObject = new CRole();
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
        if (!m_Company.RoleMgr.AddNew(BaseObject, true))
        {
            Response.Write("添加失败！");
            return ;
        }
    }
}
