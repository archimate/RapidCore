using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;

public partial class Database_Table_TablePanel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null)
        {
            Response.Redirect("../../Login.aspx");
            Response.End();
        }

        if (Request.Params["Action"] == "GetData")
        {
            GetData();
            Response.End();
        }
        else if (Request.Params["Action"] == "Delete")
        {
            DeleteTable();
            Response.End();
        }
    }
    void GetData()
    {
        int page = Convert.ToInt32(Request.Params["page"]);
        int pageSize = Convert.ToInt32(Request.Params["pagesize"]);


        string sData = "";
        List<CBaseObject> lstTable = Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.GetList();

        int totalPage = lstTable.Count % pageSize == 0 ? lstTable.Count / pageSize : lstTable.Count / pageSize + 1; // 计算总页数

        int index = (page - 1) * pageSize; // 开始记录数  
        for (int i = index; i < pageSize + index && i < lstTable.Count; i++)
        {
            CTable tb = (CTable)lstTable[i];

            sData += string.Format("{{ \"id\": \"{0}\",\"Name\":\"{1}\", \"Code\":\"{2}\", \"IsSystem\":\"{3}\", \"IsFinish\":\"{4}\",\"Created\":\"{5}\" }},"
                , tb.Id, tb.Name, tb.Code, tb.IsSystem, tb.IsFinish, tb.Created);

        }
        sData = sData.TrimEnd(",".ToCharArray());
        sData = "[" + sData + "]";
        string sJson = string.Format("{{\"Rows\":{0},\"Total\":\"{1}\"}}"
            ,  sData, lstTable.Count);

        Response.Write(sJson);
    }

    void DeleteTable()
    {
        string id = Request["id"];
        if (string.IsNullOrEmpty(id))
        {
            Response.Write("请选择表！");
            return;
        }
        Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.Delete(new Guid(id));

        if (!Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.Save(true))
        {
            Response.Write("删除失败！");
            return;
        }
    }
}
