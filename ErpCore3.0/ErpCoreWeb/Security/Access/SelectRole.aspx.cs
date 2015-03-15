using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;

public partial class Security_Access_SelectRole : System.Web.UI.Page
{
    public CCompany m_Company = null;
    protected void Page_Load(object sender, EventArgs e)
    {
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

        if (!IsPostBack)
        {
            cbCompany.Items.Clear();
            List<CBaseObject> lstObj = Global.GetCtx(Session["TopCompany"].ToString()).CompanyMgr.GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CCompany c = (CCompany)obj;
                ListItem item = new ListItem(c.Name,c.Id.ToString());
                if(c.Id==m_Company.Id)
                    item.Selected = true;
                cbCompany.Items.Add(item);
            }
        }
    }
    void GetData()
    {
        int page = Convert.ToInt32(Request.Params["page"]);
        int pageSize = Convert.ToInt32(Request.Params["pagesize"]);


        string sData = "";
        List<CBaseObject> lstObj = m_Company.RoleMgr.GetList();
        
        int totalPage = lstObj.Count % pageSize == 0 ? lstObj.Count / pageSize : lstObj.Count / pageSize + 1; // 计算总页数

        int index = (page - 1) * pageSize; // 开始记录数  
        for (int i = index; i < pageSize + index && i < lstObj.Count; i++)
        {
            CRole role = (CRole)lstObj[i];

            sData += string.Format("{{ \"id\": \"{0}\",\"Name\":\"{1}\" }},"
                , role.Id, role.Name);

        }
        sData = sData.TrimEnd(",".ToCharArray());
        sData = "[" + sData + "]";
        string sJson = string.Format("{{\"Rows\":{0},\"Total\":\"{1}\"}}"
            , sData, lstObj.Count);

        Response.Write(sJson);
    }
}
