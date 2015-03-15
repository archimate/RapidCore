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

public partial class Report_SelStatItem : System.Web.UI.Page
{
    public CReport m_Report = null;
    public CTable m_Table = null;
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

        string rptid = Request["rptid"];
        if (string.IsNullOrEmpty(rptid))
        {
            Response.End();
            return;
        }
        m_Report = (CReport)m_Company.ReportMgr.Find(new Guid(rptid));
        if (m_Report == null) //可能是新建的
        {
            if (Session["AddReport"] == null)
            {
                Response.End();
                return;
            }
            m_Report = (CReport)Session["AddReport"];
        }
        if (m_Report.StatItemMgr.GetList().Count > 0)
        {
            CStatItem StatItem = (CStatItem)m_Report.StatItemMgr.GetList()[0];
            m_Table = (CTable)Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.Find(StatItem.FW_Table_id);
        }

        if (Request.Params["Action"] == "GetColumnData")
        {
            GetColumnData();
            Response.End();
        }
        else if (Request.Params["Action"] == "GetStatItemData")
        {
            GetStatItemData();
            Response.End();
        }
        else if (Request.Params["Action"] == "AddStatItem")
        {
            AddStatItem();
            Response.End();
        }
        else if (Request.Params["Action"] == "DeleteStatItem")
        {
            DeleteStatItem();
            Response.End();
        }
        else if (Request.Params["Action"] == "AddFormula")
        {
            m_Report.StatItemMgr.Cancel();
            Response.End();
        }
        else if (Request.Params["Action"] == "Cancel")
        {
            AddFormula();
            Response.End();
        }   
        else if (Request.Params["Action"] == "PostData")
        {
            PostData();
            Response.End();
        }
    }


    void GetColumnData()
    {
        string Table_id = Request["Table_id"];
        if (string.IsNullOrEmpty(Table_id))
            return;
        CTable table = (CTable)Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.Find(new Guid(Table_id));
        if (table == null)
            return;

        List<CBaseObject> lstObj = table.ColumnMgr.GetList();
        string sData = "";
        foreach (CBaseObject obj in lstObj)
        {
            CColumn column = (CColumn)obj;
            
            sData += string.Format("{{ \"id\": \"{0}\",\"Name\":\"{1}\" }},"
                , column.Id
                , column.Name);
        }

        sData = sData.TrimEnd(",".ToCharArray());
        sData = "[" + sData + "]";
        string sJson = string.Format("{{\"Rows\":{0},\"Total\":\"{1}\"}}"
            , sData, lstObj.Count);

        Response.Write(sJson);
    }
    void GetStatItemData()
    {
        List<CBaseObject> lstObj = m_Report.StatItemMgr.GetList();

        string sData = "";
        int iCount = 0;
        foreach (CBaseObject obj in lstObj)
        {
            CStatItem StatItem = (CStatItem)obj;

            string sTableName = "", sColumnName = "";
            if (StatItem.ItemType == CStatItem.enumItemType.Field)
            {
                CTable table = (CTable)Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.Find(StatItem.FW_Table_id);
                if (table != null)
                {
                    sTableName = table.Name;
                    CColumn column = (CColumn)table.ColumnMgr.Find(StatItem.FW_Column_id);
                    if (column != null)
                        sColumnName = column.Name;
                }
            }
            else
            {
                sTableName = StatItem.Name;
                sColumnName = StatItem.Formula;
            }

            sData += string.Format("{{ \"id\": \"{0}\",\"FW_Table_id\":\"{1}\",\"TableName\":\"{2}\",\"FW_Column_id\":\"{3}\", \"ColumnName\":\"{4}\", \"StatType\":\"{5}\", \"Idx\":\"{6}\" }},"
                , StatItem.Id
                , StatItem.FW_Table_id
                , sTableName
                , StatItem.FW_Column_id
                , sColumnName
                , StatItem.StatType
                , StatItem.Idx);
            iCount++;
        }
        sData = sData.TrimEnd(",".ToCharArray());
        sData = "[" + sData + "]";
        string sJson = string.Format("{{\"Rows\":{0},\"Total\":\"{1}\"}}"
            , sData, iCount);

        Response.Write(sJson);
    }
    void AddStatItem()
    {
        string Table_id = Request["Table_id"];
        if (string.IsNullOrEmpty(Table_id))
        {
            Response.Write("表不存在！");
            return;
        }
        CTable table = (CTable)Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.Find(new Guid(Table_id));
        if (table == null)
        {
            Response.Write("表不存在！");
            return;
        }
        string Column_id = Request["Column_id"];
        if (string.IsNullOrEmpty(Column_id))
        {
            Response.Write("字段不存在！");
            return;
        }
        CColumn column = (CColumn)table.ColumnMgr.Find(new Guid(Column_id));
        if (column == null)
        {
            Response.Write("字段不存在！");
            return;
        }
        if (m_Report.StatItemMgr.FindByColumn(table.Id, column.Id) != null)
        {
            Response.Write("指标已经存在！");
            return;
        }

        CStatItem StatItem = new CStatItem();
        StatItem.Ctx = Global.GetCtx(Session["TopCompany"].ToString());
        StatItem.RPT_Report_id = m_Report.Id;
        StatItem.FW_Table_id=table.Id;
        StatItem.FW_Column_id=column.Id;
        StatItem.Name=column.Name;
        StatItem.Idx = m_Report.StatItemMgr.GetList().Count;

        CUser user = (CUser)Session["User"];
        StatItem.Creator = user.Id;

        m_Report.StatItemMgr.AddNew(StatItem);
    }
    void DeleteStatItem()
    {
        string delid = Request["delid"];
        if (string.IsNullOrEmpty(delid))
        {
            Response.Write("请选择指标！");
            return;
        }
        m_Report.StatItemMgr.Delete(new Guid(delid));
    }
    void AddFormula()
    {
        string AsName = Request["AsName"];
        if (string.IsNullOrEmpty(AsName))
        {
            Response.Write("别名不能空！");
            return;
        }
        string Formula = Request["Formula"];
        if (string.IsNullOrEmpty(Formula))
        {
            Response.Write("公式不能空！");
            return;
        }

        CStatItem StatItem = new CStatItem();
        StatItem.Ctx = Global.GetCtx(Session["TopCompany"].ToString());
        StatItem.RPT_Report_id = m_Report.Id;
        StatItem.ItemType = CStatItem.enumItemType.Formula;
        StatItem.Name = AsName;
        StatItem.Formula = Formula;

        CUser user = (CUser)Session["User"];
        StatItem.Creator = user.Id;

        m_Report.StatItemMgr.AddNew(StatItem);
    }
    void PostData()
    {
    }
}
