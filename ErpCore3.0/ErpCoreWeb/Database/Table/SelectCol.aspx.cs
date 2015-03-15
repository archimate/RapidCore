using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;

public partial class Database_Table_SelectCol : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["Action"] == "GetData")
        {
            GetData();
            Response.End();
        }
    }
    void GetData()
    {
        string tid = Request["tid"];
        if (string.IsNullOrEmpty(tid))
        {
            Response.End();
            return;
        }

        CTable table = (CTable)Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.Find(new Guid(tid));
        if (table==null)
        {
            Response.End();
            return;
        }

        string sData = "";
        List<CBaseObject> lstObj = table.ColumnMgr.GetList();
        foreach (CBaseObject obj in lstObj)
        {
            CColumn col = (CColumn)obj;

            sData += string.Format("{{ \"id\": \"{0}\",\"Name\":\"{1}\", \"Code\":\"{2}\", \"ColType\":\"{3}\" }},"
                , col.Id, col.Name, col.Code, CColumn.ConvertColTypeToString(col.ColType));

        }
        sData = sData.TrimEnd(",".ToCharArray());
        sData = "[" + sData + "]";
        string sJson = string.Format("{{\"Rows\":{0},\"Total\":\"{1}\"}}"
            , sData, lstObj.Count);

        Response.Write(sJson);
    }
}
