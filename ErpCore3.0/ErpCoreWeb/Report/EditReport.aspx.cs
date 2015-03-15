using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.Report;

public partial class Report_EditReport : System.Web.UI.Page
{
    public CReport m_BaseObject = null;
    public CCompany m_Company = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null)
        {
            Response.Redirect("../Login.aspx");
            Response.End();
        }

        string B_Company_id = Request["B_Company_id"];
        if (string.IsNullOrEmpty(B_Company_id))
            m_Company = Global.GetCtx(Session["TopCompany"].ToString()).CompanyMgr.FindTopCompany();
        else
            m_Company = (CCompany)Global.GetCtx(Session["TopCompany"].ToString()).CompanyMgr.Find(new Guid(B_Company_id));

        string id = Request["id"];
        if (string.IsNullOrEmpty(id))
        {
            Response.End();
            return;
        }
        m_BaseObject = (CReport)m_Company.ReportMgr.Find(new Guid(id));
        if (m_BaseObject == null)
        {
            Response.End();
            return;
        }

        //保存到编辑对象
        EditObject.Add(Session.SessionID, m_BaseObject);

        if (Request.Params["Action"] == "GetData")
        {
            GetData();
            Response.End();
        }
        else if (Request.Params["Action"] == "Cancel")
        {
            Session["AddReport"] = null;
            Response.End();
        }
        else if (Request.Params["Action"] == "PostData")
        {
            PostData();
            //从编辑对象移除
            EditObject.Remove(Session.SessionID, m_BaseObject);

            Response.End();
        }
        else if (Request.Params["Action"] == "GetCondiction")
        {
            GetCondiction();
            Response.End();
        }
    }
    void GetData()
    {
        CReport Report = GetReport();
        List<CBaseObject> lstObj = Report.StatItemMgr.GetList();
        //按序号排序
        List<CStatItem> sortObj = new List<CStatItem>();
        foreach (CBaseObject obj in lstObj)
        {
            CStatItem StatItem = (CStatItem)obj;
            sortObj.Add(StatItem);
        }
        sortObj.Sort();

        string sData = "";
        foreach (CStatItem StatItem in sortObj)
        {
            string sTableName = "", sColumnName = "";
            CTable table = (CTable)Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.Find(StatItem.FW_Table_id);
            if (table != null)
            {
                sTableName = table.Name;
                CColumn column = (CColumn)table.ColumnMgr.Find(StatItem.FW_Column_id);
                if (column != null)
                    sColumnName = column.Name;
            }

            sData += string.Format("{{ \"id\": \"{0}\",\"FW_Table_id\":\"{1}\",\"TableName\":\"{2}\",\"FW_Column_id\":\"{3}\", \"ColumnName\":\"{4}\", \"StatType\":\"{5}\", \"StatTypeName\":\"{6}\", \"Order\":\"{7}\", \"OrderName\":\"{8}\" }},"
                , StatItem.Id
                , StatItem.FW_Table_id
                , sTableName
                , StatItem.FW_Column_id
                , sColumnName
                , (int)StatItem.StatType
                , StatItem.GetStatTypeName()
                , (int)StatItem.Order
                , StatItem.GetOrderName());
        }


        sData = "[" + sData + "]";
        string sJson = string.Format("{{\"Rows\":{0},\"Total\":\"{1}\"}}"
            , sData, lstObj.Count);

        Response.Write(sJson);
    }
    public CReport GetReport()
    {
        return m_BaseObject;
    }

    void PostData()
    {
        string Name = Request["Name"];
        string Catalog_id = Request["catalog_id"];
        string GridData = Request["GridData"];
        string Filter = Request["Filter"];
        
        if (string.IsNullOrEmpty(Name))
        {
            Response.Write("名称不能空！");
            return;
        }
        
        GetReport().Name = Name;
        if (Catalog_id != "")
            GetReport().RPT_ReportCatalog_id = new Guid(Catalog_id);
        GetReport().Filter = Filter;

        int iLastIdx = 0;
        string[] arr1 = Regex.Split(GridData, ";");
        foreach (string str1 in arr1)
        {
            if (str1.Length == 0)
                continue;
            iLastIdx++;

            string[] arr2 = Regex.Split(str1, ",");
            string id = arr2[0];
            string StatTypeName = arr2[1];
            string OrderName = arr2[2];
            CStatItem StatItem = (CStatItem)GetReport().StatItemMgr.Find(new Guid(id));
            if (StatItem != null)
            {
                StatItem.Idx = iLastIdx;
                StatItem.SetStatTypeByName(StatTypeName);
                StatItem.SetOrderByName(OrderName);
            }
        }

        CUser user = (CUser)Session["User"];
        GetReport().Updator = user.Id;

        m_Company.ReportMgr.Update(GetReport());

        if (!m_Company.ReportMgr.Save(true))
        {
            Response.Write("修改失败！");
            return;
        }

    }
    void GetCondiction()
    {
        string Table_id = Request["Table_id"];
        string Column_id = Request["Column_id"];
        string Val = Request["Val"];

        CTable table = (CTable)Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.Find(new Guid(Table_id));
        if (table == null)
            return;
        CColumn col = (CColumn)table.ColumnMgr.Find(new Guid(Column_id));
        if (col == null)
            return;
        if (col.ColType == ColumnType.int_type
                || col.ColType == ColumnType.long_type
                || col.ColType == ColumnType.numeric_type
                || col.ColType == ColumnType.bool_type)
        {
            if (Val == "")
                Val = "0";
            else
            {
                try { Convert.ToDouble(Val); }
                catch
                {
                    return;
                }
            }
        }
        else
        {
            if (Val == "")
                Val = "''";
            else
            {
                if (Val[0] != '\'')
                    Val = "\'" + Val;
                if (Val[Val.Length - 1] != '\'')
                    Val += "\'";
            }
        }

        Response.Write(Val);
    }

    public string GetCatalogName()
    {
        if (m_BaseObject == null)
            return "";
        CReportCatalog Catalog = (CReportCatalog)m_Company.ReportCatalogMgr.Find(m_BaseObject.RPT_ReportCatalog_id);
        if (Catalog == null)
            return "";
        return Catalog.Name;
    }
}
