using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.UI;
using ErpCoreModel.Report;

public partial class Menu_EditMenu : System.Web.UI.Page
{
    public CTable m_Table = null;
    public CMenu m_BaseObject = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null)
        {
            Response.Redirect("../Login.aspx");
            Response.End();
        }

        m_Table = Global.GetCtx(Session["TopCompany"].ToString()).MenuMgr.Table;
        
        string id = Request["id"];
        if (string.IsNullOrEmpty(id))
        {
            Response.End();
        }
        m_BaseObject = (CMenu)Global.GetCtx(Session["TopCompany"].ToString()).MenuMgr.Find(new Guid(id));

        if (Request.Params["Action"] == "PostData")
        {
            PostData();
            Response.End();
        }

        if (!IsPostBack)
        {
            LoadData();
        }
    }
    void LoadData()
    {
        txtName.Value = m_BaseObject.Name;
        if (m_BaseObject.Parent_id != Guid.Empty)
        {
            CMenu pmenu = (CMenu)Global.GetCtx(Session["TopCompany"].ToString()).MenuMgr.Find(m_BaseObject.Parent_id);
            if (pmenu != null)
            {
                txtParent.Value = pmenu.Name;
                hidParent.Value = pmenu.Id.ToString();
            }
        }
        else
            txtParent.Value = "菜单";

        if (m_BaseObject.MType == enumMenuType.CatalogMenu)
        {
            rdType1.Checked = true;
            cbView.Attributes.Add("disabled","disabled");
            cbWindow.Attributes.Add("disabled", "disabled");
            txtUrl.Attributes.Add("disabled", "disabled");
            cbReport.Attributes.Add("disabled", "disabled");
        }
        else if (m_BaseObject.MType == enumMenuType.ViewMenu)
        {
            rdType2.Checked = true;
            CView view = (CView)Global.GetCtx(Session["TopCompany"].ToString()).ViewMgr.Find(m_BaseObject.UI_View_id);
            if (view != null)
            {
                cbView.Value = view.Name;
                hidView.Value = m_BaseObject.UI_View_id.ToString();
            }
            cbWindow.Attributes.Add("disabled", "disabled");
            txtUrl.Attributes.Add("disabled", "disabled");
            cbReport.Attributes.Add("disabled", "disabled");
        }
        else if (m_BaseObject.MType == enumMenuType.WindowMenu)
        {
            rdType3.Checked = true;
            cbView.Attributes.Add("disabled", "disabled");
            CWindow window = (CWindow)Global.GetCtx(Session["TopCompany"].ToString()).WindowMgr.Find(m_BaseObject.UI_Window_id);
            if (window != null)
            {
                cbWindow.Value = window.Name;
                hidWindow.Value = m_BaseObject.UI_Window_id.ToString();
            }
            txtUrl.Attributes.Add("disabled", "disabled");
            cbReport.Attributes.Add("disabled", "disabled");
        }
        else if (m_BaseObject.MType == enumMenuType.UrlMenu)
        {
            rdType4.Checked = true;
            cbView.Attributes.Add("disabled", "disabled");
            cbWindow.Attributes.Add("disabled", "disabled");
            txtUrl.Value = m_BaseObject.Url;
            cbReport.Attributes.Add("disabled", "disabled");
        }
        else if (m_BaseObject.MType == enumMenuType.ReportMenu)
        {
            rdType5.Checked = true;
            cbView.Attributes.Add("disabled", "disabled");
            cbWindow.Attributes.Add("disabled", "disabled");
            txtUrl.Attributes.Add("disabled", "disabled");
            CUser user = (CUser)Session["User"];
            CCompany Company = (CCompany)Global.GetCtx(Session["TopCompany"].ToString()).CompanyMgr.Find(user.B_Company_id);
            CReport Report = (CReport)Company.ReportMgr.Find(m_BaseObject.RPT_Report_id);
            if (Report != null)
            {
                cbReport.Value = Report.Name;
                hidReport.Value = m_BaseObject.RPT_Report_id.ToString();
            }
        }
        imgIcon.Src = string.Format("../{0}/MenuIcon/default.png", Global.GetDesktopIconPathName());
        //菜单图标
        if (m_BaseObject.IconUrl != "")
        {
            string sPath = ConfigurationManager.AppSettings["VirtualDir"]+"/";
            sPath += Global.GetDesktopIconPathName() + "/";
            sPath += "MenuIcon/";
            imgIcon.Src = sPath + m_BaseObject.IconUrl;
            hidIcon.Value = m_BaseObject.IconUrl;
        }
        txtOpenwinWidth.Value = m_BaseObject.OpenwinWidth.ToString();
        txtOpenwinHeight.Value = m_BaseObject.OpenwinHeight.ToString();
    }

    void PostData()
    {
        if (txtName.Value.Trim() == "")
        {
            Response.Write("<script>parent.callback('名称不能空！')</script>");
            return;
        }
        //同级不能有同名
        if (!txtName.Value.Trim().Equals(m_BaseObject.Name))
        {
            if (Global.GetCtx(Session["TopCompany"].ToString()).MenuMgr.FindByName(txtName.Value, m_BaseObject.Parent_id) != null)
            {
                Response.Write("<script>parent.callback('菜单已经存在！')</script>");
                return;
            }
        }

        m_BaseObject.Name = txtName.Value;
        if (hidParent.Value != "")
            m_BaseObject.Parent_id = new Guid(hidParent.Value);
        else
            m_BaseObject.Parent_id = Guid.Empty;
        if (rdType1.Checked)
            m_BaseObject.MType = enumMenuType.CatalogMenu;
        else if (rdType2.Checked)
            m_BaseObject.MType = enumMenuType.ViewMenu;
        else if (rdType3.Checked)
            m_BaseObject.MType = enumMenuType.WindowMenu;
        else if (rdType4.Checked)
            m_BaseObject.MType = enumMenuType.UrlMenu;
        else if (rdType5.Checked)
            m_BaseObject.MType = enumMenuType.ReportMenu;
        if (hidView.Value != "")
            m_BaseObject.UI_View_id = new Guid(hidView.Value);
        if (hidWindow.Value != "")
            m_BaseObject.UI_Window_id = new Guid(hidWindow.Value);
        m_BaseObject.Url = txtUrl.Value;
        if (hidReport.Value != "")
            m_BaseObject.RPT_Report_id = new Guid(hidReport.Value);
        if (hidIcon.Value != "")
            m_BaseObject.IconUrl = hidIcon.Value;
        m_BaseObject.OpenwinWidth = Convert.ToInt32(txtOpenwinWidth.Value);
        m_BaseObject.OpenwinHeight = Convert.ToInt32(txtOpenwinHeight.Value);


        if (!Global.GetCtx(Session["TopCompany"].ToString()).MenuMgr.Update(m_BaseObject, true))
        {
            Response.Write("<script>parent.callback('修改失败！')</script>");
            return;
        }
        Response.Write("<script>parent.callback('')</script>");
    }
}
