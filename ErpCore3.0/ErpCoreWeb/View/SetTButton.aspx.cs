using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.UI;

public partial class View_SetTButton : System.Web.UI.Page
{
    public CView m_View = null;
    bool m_bIsNew = false;
    public CTable m_Table = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null)
        {
            Response.Redirect("../Login.aspx");
            Response.End();
        }
        string id = Request["id"];
        m_View = (CView)Global.GetCtx(Session["TopCompany"].ToString()).ViewMgr.Find(new Guid(id));
        
        m_Table = (CTable)Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.Find(m_View.FW_Table_id);


        if (Request.Params["Action"] == "GetData")
        {
            GetData();
            Response.End();
        }
        else if (Request.Params["Action"] == "PostData")
        {
            PostData();
            Response.End();
        }
        else if (Request.Params["Action"] == "Cancel")
        {
            Cancel();
            Response.End();
        }
        else if (Request.Params["Action"] == "Delete")
        {
            Delete();
            Response.End();
        }
    }

    void GetData()
    {
        List<CTButtonInView> lstObj = m_View.TButtonInViewMgr.GetListOrderByIdx();

        string sData = "";

        int iCount = 0;
        foreach (CTButtonInView tiv in lstObj)
        {
            sData += string.Format("{{ \"id\": \"{0}\",\"Caption\":\"{1}\",\"Url\":\"{2}\"}},"
                , tiv.Id, tiv.Caption, tiv.Url);

            iCount++;
        }
        sData = sData.TrimEnd(",".ToCharArray());
        sData = "[" + sData + "]";
        string sJson = string.Format("{{\"Rows\":{0},\"Total\":\"{1}\"}}"
            , sData, iCount);

        Response.Write(sJson);
    }
    void PostData()
    {

        string GridData = Request["GridData"];
        if (string.IsNullOrEmpty(GridData))
        {
            //已经被删除完，保存退出
            m_View.TButtonInViewMgr.Save();
            return;
        }

        string[] arr1 = GridData.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < arr1.Length; i++)
        {
            string[] arr2 = arr1[i].Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            CTButtonInView tiv = (CTButtonInView)m_View.TButtonInViewMgr.Find(new Guid(arr2[0]));
            tiv.Idx = i;
            tiv.Caption = arr2[1];
            tiv.Url = arr2[2];
            m_View.TButtonInViewMgr.Update(tiv);
        }
        m_View.TButtonInViewMgr.Save();
    }
    void Cancel()
    {
        m_View.TButtonInViewMgr.Cancel();
    }
    void Delete()
    {
        string delid = Request["delid"];
        if (string.IsNullOrEmpty(delid))
        {
            Response.Write("请选择一项！");
            return;
        }

        m_View.TButtonInViewMgr.Delete(new Guid(delid));

    }

    protected void btAdd_Click(object sender, EventArgs e)
    {
        if (txtCaption.Text.Trim() == "")
        {
            RegisterStartupScript("starup", "<script>alert('请输入标题！');</script>");
            return;
        }
        if (txtUrl.Text.Trim() == "")
        {
            RegisterStartupScript("starup", "<script>alert('请输入Url！');</script>");
            return;
        }

        CTButtonInView tiv = new CTButtonInView();
        tiv.Ctx = Global.GetCtx(Session["TopCompany"].ToString());
        tiv.UI_View_id = m_View.Id;
        tiv.Caption = txtCaption.Text.Trim();
        tiv.Url = txtUrl.Text.Trim();
        tiv.Idx = m_View.TButtonInViewMgr.NewIdx();
        tiv.Creator = ((CUser)Session["User"]).Id;
        m_View.TButtonInViewMgr.AddNew(tiv);

        txtCaption.Text = "";
        txtUrl.Text = "";
    }

}
