using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.UI;
using ErpCoreModel.Report;

public partial class Menu_SelectReport : System.Web.UI.Page
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
        CUser user = (CUser)Session["User"];
        CCompany Company = (CCompany)Global.GetCtx(Session["TopCompany"].ToString()).CompanyMgr.Find(user.B_Company_id);

        int page = Convert.ToInt32(Request.Params["page"]);
        int pageSize = Convert.ToInt32(Request.Params["pagesize"]);
        
        string sData = "";
        List<CBaseObject> lstObj = Company.ReportMgr.GetList();

        int totalPage = lstObj.Count % pageSize == 0 ? lstObj.Count / pageSize : lstObj.Count / pageSize + 1; // 计算总页数

        int index = (page - 1) * pageSize; // 开始记录数  
        for (int i = index; i < pageSize + index && i < lstObj.Count; i++)
        {
            CReport Report = (CReport)lstObj[i];

            sData += string.Format("{{ \"id\": \"{0}\",\"Name\":\"{1}\"}},"
                , Report.Id, Report.Name);

        }
        sData = sData.TrimEnd(",".ToCharArray());
        sData = "[" + sData + "]";
        string sJson = string.Format("{{\"Rows\":{0},\"Total\":\"{1}\"}}"
            , sData, lstObj.Count);

        Response.Write(sJson);
    }
}
