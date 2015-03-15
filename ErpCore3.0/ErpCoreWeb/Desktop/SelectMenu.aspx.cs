using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.UI;

public partial class Desktop_SelectMenu : System.Web.UI.Page
{
    public CCompany m_Company = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetNoStore();
        if (Session["User"] == null)
        {
            Response.Redirect("../Login.aspx");
            Response.End();
        }
        CUser user = (CUser)Session["User"];
        m_Company = (CCompany)Global.GetCtx(Session["TopCompany"].ToString()).CompanyMgr.Find(user.B_Company_id);

        if (Request.Params["Action"] == "GetData")
        {
            GetData();
            Response.End();
        }
    }
    void GetData()
    {
        string pid = Request["pid"];
        Guid Parent_id = Guid.Empty;
        if (!string.IsNullOrEmpty(pid))
            Parent_id = new Guid(pid);
        string sData = "";
        LoopGetMenu(ref sData, Parent_id);
        
        sData = "[" + sData + "]";

        Response.Write(sData);
    }
    void LoopGetMenu(ref string sData,Guid Parent_id)
    {
        List<CBaseObject> lstObj= Global.GetCtx(Session["TopCompany"].ToString()).MenuMgr.GetList();
        foreach (CBaseObject obj in lstObj)
        {
            CMenu menu = (CMenu)obj;
            if (menu.Parent_id != Parent_id)
                continue;
            string sChildren = "";
            LoopGetMenu(ref sChildren, menu.Id);
            string sIconUrl = string.Format("../{0}/MenuIcon/default.png",
                Global.GetDesktopIconPathName());
            if (menu.IconUrl != "")
            {
                sIconUrl = string.Format("../{0}/MenuIcon/{1}",
                    Global.GetDesktopIconPathName(), menu.IconUrl);
            }
            string url = menu.Url;
            if (menu.MType == enumMenuType.CatalogMenu)
                url = "SelectMenu.aspx?pid=" + menu.Id.ToString();
            else if (menu.MType == enumMenuType.ViewMenu)
            {
                CView view = (CView)Global.GetCtx(Session["TopCompany"].ToString()).ViewMgr.Find(menu.UI_View_id);
                if (view == null)
                    continue;
                if (view.VType == enumViewType.Single)
                    url = "../View/SingleView.aspx?vid=" + view.Id.ToString();
                else if (view.VType == enumViewType.MasterDetail)
                    url = "../View/MasterDetailView.aspx?vid=" + view.Id.ToString();
                else
                    url = "../View/MultMasterDetailView.aspx?vid=" + view.Id.ToString();
            }
            else if (menu.MType == enumMenuType.WindowMenu)
            {
            }
            else if (menu.MType == enumMenuType.ReportMenu)
            {
                url = "../Report/RunReport.aspx?id=" + menu.RPT_Report_id.ToString();
            }
            sData += string.Format("{{\"id\":\"{0}\",\"text\":\"{1}\",\"icon\":\"{2}\",\"mtype\":\"{3}\",\"url\":\"{4}\", children: [{5}]}},",
                menu.Id, menu.Name, sIconUrl, (int)menu.MType, url, sChildren);
        }
        sData = sData.TrimEnd(",".ToCharArray());
    }
}
