using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.UI;

public partial class View_SetDefaultVal : System.Web.UI.Page
{
    public CView m_View = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null)
        {
            Response.Redirect("../Login.aspx");
            Response.End();
        }
        string id = Request["id"];
        m_View = (CView)Global.GetCtx(Session["TopCompany"].ToString()).ViewMgr.Find(new Guid(id));
        

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
    }

    void GetData()
    {
        string table_id=Request["table_id"];
        CTable table = (CTable)Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.Find(new Guid(table_id));

        string sData = "";
        
        List<CBaseObject> lstObj = table.ColumnMgr.GetList();

        int iCount = 0;
        foreach (CBaseObject obj in lstObj)
        {
            CColumn col = (CColumn)obj;
            if (col.Code.Equals("id", StringComparison.OrdinalIgnoreCase)
                || col.Code.Equals("Creator", StringComparison.OrdinalIgnoreCase)
                || col.Code.Equals("Created", StringComparison.OrdinalIgnoreCase)
                || col.Code.Equals("Updator", StringComparison.OrdinalIgnoreCase)
                || col.Code.Equals("Updated", StringComparison.OrdinalIgnoreCase))
                continue;
            if (table.Id == m_View.FW_Table_id) //主表
            {
                CColumnDefaultValInView cdviv = m_View.ColumnDefaultValInViewMgr.FindByColumn(col.Id);
                string DefaultVal = cdviv != null ? cdviv.DefaultVal : "";
                string ReadOnly = (cdviv != null && cdviv.ReadOnly) ? "1" : "0";
                
                sData += string.Format("{{ \"id\": \"{0}\",\"ColName\":\"{1}\",\"DefaultVal\":\"{2}\",\"ReadOnly\":\"{3}\"}},"
                    , col.Id, col.Name, DefaultVal, ReadOnly);
            }
            else //从表
            {
                CViewDetail ViewDetail = m_View.ViewDetailMgr.FindByTable(table.Id);
                CColumnDefaultValInViewDetail cdvivd = ViewDetail.ColumnDefaultValInViewDetailMgr.FindByColumn(col.Id);
                string DefaultVal = cdvivd != null ? cdvivd.DefaultVal : "";
                string ReadOnly = (cdvivd != null && cdvivd.ReadOnly) ? "1" : "0";

                sData += string.Format("{{ \"id\": \"{0}\",\"ColName\":\"{1}\",\"DefaultVal\":\"{2}\",\"ReadOnly\":\"{3}\"}},"
                    , col.Id, col.Name, DefaultVal, ReadOnly );
            }
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
        string table_id = Request["table_id"];
        CTable table = (CTable)Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.Find(new Guid(table_id));

        string GridData = Request["GridData"];

        if (table.Id == m_View.FW_Table_id) //主表
        {
            string[] arr1 = GridData.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < arr1.Length; i++)
            {
                string[] arr2 = arr1[i].Split(":".ToCharArray());
                Guid colid = new Guid(arr2[0]);

                if (arr2[1].Trim() == "")
                {
                    m_View.ColumnDefaultValInViewMgr.RemoveByColumn(colid);
                }
                else
                {
                    CColumnDefaultValInView cdviv = m_View.ColumnDefaultValInViewMgr.FindByColumn(colid);
                    if (cdviv == null)
                    {
                        cdviv = new CColumnDefaultValInView();
                        cdviv.Ctx = Global.GetCtx(Session["TopCompany"].ToString());
                        cdviv.FW_Table_id = table.Id;
                        cdviv.FW_Column_id = colid;
                        cdviv.DefaultVal = arr2[1].Trim();
                        cdviv.ReadOnly = (arr2[2] != "1") ? true : false;
                        cdviv.UI_View_id = m_View.Id;
                        m_View.ColumnDefaultValInViewMgr.AddNew(cdviv);
                    }
                    else
                    {
                        cdviv.DefaultVal = arr2[1].Trim();
                        cdviv.ReadOnly = (arr2[2] != "1") ? true : false;
                        m_View.ColumnDefaultValInViewMgr.Update(cdviv);
                    }
                }
            }
        }
        else
        {
            CViewDetail ViewDetail = m_View.ViewDetailMgr.FindByTable(table.Id);
            
            string[] arr1 = GridData.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < arr1.Length; i++)
            {
                string[] arr2 = arr1[i].Split(":".ToCharArray());
                Guid colid = new Guid(arr2[0]);

                if (arr2[1].Trim() == "")
                {
                    ViewDetail.ColumnDefaultValInViewDetailMgr.RemoveByColumn(colid);
                }
                else
                {
                    CColumnDefaultValInViewDetail cdvivd = ViewDetail.ColumnDefaultValInViewDetailMgr.FindByColumn(colid);
                    if (cdvivd == null)
                    {
                        cdvivd = new CColumnDefaultValInViewDetail();
                        cdvivd.Ctx = Global.GetCtx(Session["TopCompany"].ToString());
                        cdvivd.FW_Table_id = table.Id;
                        cdvivd.FW_Column_id = colid;
                        cdvivd.DefaultVal = arr2[1].Trim();
                        cdvivd.ReadOnly = (arr2[2] != "1") ? true : false;
                        cdvivd.UI_ViewDetail_id = ViewDetail.Id;
                        ViewDetail.ColumnDefaultValInViewDetailMgr.AddNew(cdvivd);
                    }
                    else
                    {
                        cdvivd.DefaultVal = arr2[1].Trim();
                        cdvivd.ReadOnly = (arr2[2] != "1") ? true : false;
                        ViewDetail.ColumnDefaultValInViewDetailMgr.Update(cdvivd);
                    }
                }
            }
        }

    }
    void Cancel()
    {
        m_View.ColumnDefaultValInViewMgr.Cancel();
    }
}
