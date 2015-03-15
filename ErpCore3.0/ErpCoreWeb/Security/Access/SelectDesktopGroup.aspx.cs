using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.UI;

public partial class Security_Access_SelectDesktopGroup : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["Action"] == "GetData")
        {
            GetData();
            Response.End();
        }

        if (!IsPostBack)
        {
        }
    }
    void GetData()
    {
        int page = Convert.ToInt32(Request.Params["page"]);
        int pageSize = Convert.ToInt32(Request.Params["pagesize"]);


        string sData = "";
        List<CBaseObject> lstObj = Global.GetCtx(Session["TopCompany"].ToString()).DesktopGroupMgr.GetList();
        //添加主桌面
        CDesktopGroup group0 = new CDesktopGroup();
        group0.Id = Guid.Empty;
        group0.Name = "主桌面";
        lstObj.Insert(0,group0);
        //
       
        int totalPage = lstObj.Count % pageSize == 0 ? lstObj.Count / pageSize : lstObj.Count / pageSize + 1; // 计算总页数

        int index = (page - 1) * pageSize; // 开始记录数  
        for (int i = index; i < pageSize + index && i < lstObj.Count; i++)
        {
            CDesktopGroup group = (CDesktopGroup)lstObj[i];

            sData += string.Format("{{ \"id\": \"{0}\",\"Name\":\"{1}\" }},"
                , group.Id, group.Name);

        }
        sData = sData.TrimEnd(",".ToCharArray());
        sData = "[" + sData + "]";
        string sJson = string.Format("{{\"Rows\":{0},\"Total\":\"{1}\"}}"
            , sData, lstObj.Count);

        Response.Write(sJson);
    }
}
