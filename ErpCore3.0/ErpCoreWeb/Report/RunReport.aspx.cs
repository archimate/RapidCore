using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.Report;

public partial class Report_RunReport : System.Web.UI.Page
{
    public CTable m_Table = null;
    public CCompany m_Company = null;
    public CReport m_Report = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null)
        {
            Response.Redirect("../Login.aspx");
            Response.End();
        }

        string RPT_Reprot_id = Request["id"];
        if (string.IsNullOrEmpty(RPT_Reprot_id))
        {
            Response.End();
            return;
        }

        string B_Company_id = Request["B_Company_id"];
        if (string.IsNullOrEmpty(B_Company_id))
            m_Company = Global.GetCtx(Session["TopCompany"].ToString()).CompanyMgr.FindTopCompany();
        else
            m_Company = (CCompany)Global.GetCtx(Session["TopCompany"].ToString()).CompanyMgr.Find(new Guid(B_Company_id));

        m_Table = (CTable)m_Company.ReportMgr.Table;

        m_Report = (CReport)m_Company.ReportMgr.Find(new Guid(RPT_Reprot_id));

        if (Request.Params["Action"] == "GetData")
        {
            GetData();
            Response.End();
        }
    }
    public List<CStatItem> GetStatItemList()
    {
        List<CStatItem> lstStatItem = new List<CStatItem>();
        List<CBaseObject> lstObj = m_Report.StatItemMgr.GetList();
        foreach (CBaseObject obj in lstObj)
        {
            lstStatItem.Add((CStatItem)obj);
        }
        lstStatItem.Sort();//按索引idx排序
        return lstStatItem;
    }
    void GetData()
    {

        List<CStatItem> lstStatItem = GetStatItemList();


        List<string> lstTable = new List<string>();
        string sFields = "";
        string sGroupBy = "";
        string sOrderBy = "";
        foreach (CStatItem StatItem in lstStatItem)
        {
            string sOrderFiled = "";
            if (StatItem.ItemType == CStatItem.enumItemType.Field)
            {
                CTable table = (CTable)Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.Find(StatItem.FW_Table_id);
                if (table == null)
                    continue;
                CColumn column = (CColumn)table.ColumnMgr.Find(StatItem.FW_Column_id);
                if (column == null)
                    continue;

                if (!lstTable.Contains(table.Code))
                    lstTable.Add(table.Code);
                if (StatItem.StatType == CStatItem.enumStatType.Val)
                {
                    sFields += string.Format("[{0}].[{1}],", table.Code, column.Code);
                    sGroupBy += string.Format("[{0}].[{1}],", table.Code, column.Code);
                    sOrderFiled = string.Format("[{0}].[{1}]", table.Code, column.Code);
                }
                else
                {
                    sFields += string.Format("{0}([{1}].[{2}]) as [{3}],",
                        StatItem.GetStatTypeFunc(), table.Code, column.Code, StatItem.Name);
                    sOrderFiled = StatItem.Name;
                }
            }
            else
            {
                sFields += string.Format("({0}) as [{1}],", StatItem.Formula, StatItem.Name);
                sOrderFiled = StatItem.Name;
            }

            if (StatItem.Order == CStatItem.enumOrder.Asc)
                sOrderBy += sOrderFiled + ",";
            else if (StatItem.Order == CStatItem.enumOrder.Desc)
                sOrderBy += sOrderFiled + " desc,";
        }
        sFields = sFields.TrimEnd(",".ToCharArray());
        sGroupBy = sGroupBy.TrimEnd(",".ToCharArray());
        sOrderBy = sOrderBy.TrimEnd(",".ToCharArray());

        string sTable = "";
        foreach (string sTb in lstTable)
        {
            sTable += sTb + ",";
        }
        sTable = sTable.TrimEnd(",".ToCharArray());

        string sSql = string.Format("select {0} from {1} ", sFields, sTable);
        sSql += " where IsDeleted=0 ";
        if (m_Report.Filter.Trim() != "")
            sSql += " and " + m_Report.Filter;
        if (sGroupBy != "")
            sSql += " group by " + sGroupBy;
        if (sOrderBy != "")
            sSql += " order by " + sOrderBy;

        //因为采用构造sql语句的方法来运行报表，所以仅考虑单数据库的情况，
        //即取主数据库。如果考虑多数据库分布存储情况，则使用对象来计算报表。
        DataTable dt = Global.GetCtx(Session["TopCompany"].ToString()).MainDB.QueryT(sSql);
        if (dt == null)
        {
            //MessageBox.Show("运行报表失败，请修改报表定义！");
            return;
        }


        string sData = "";

        foreach (DataRow r in dt.Rows)
        {
            string sRow = "";
            int iCol = 0;
            foreach (CStatItem StatItem in lstStatItem)
            {
                string sVal = r[iCol].ToString();
                sRow += string.Format("\"{0}\":\"{1}\",", StatItem.Name, sVal);
                iCol++;
            }

            sRow = sRow.TrimEnd(",".ToCharArray());
            sRow = "{" + sRow + "},";
            sData += sRow;
        }
        sData = sData.TrimEnd(",".ToCharArray());
        sData = "[" + sData + "]";
        string sJson = string.Format("{{\"Rows\":{0},\"Total\":\"{1}\"}}"
            , sData, dt.Rows.Count);

        Response.Write(sJson);
    }

}
