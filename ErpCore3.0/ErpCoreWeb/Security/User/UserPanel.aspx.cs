using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.Report;

public partial class Security_User_UserPanel : System.Web.UI.Page
{
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

        m_Table = (CTable)Global.GetCtx(Session["TopCompany"].ToString()).UserMgr.Table;

        if (Request.Params["Action"] == "GetData")
        {
            GetData();
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
        int page = Convert.ToInt32(Request.Params["page"]);
        int pageSize = Convert.ToInt32(Request.Params["pagesize"]);

        string sData = "";
        List<CBaseObject> lstObj = new List<CBaseObject>();
        List<CBaseObject> lstUser = Global.GetCtx(Session["TopCompany"].ToString()).UserMgr.GetList();
        foreach (CBaseObject objUser in lstUser)
        {
            CUser User = (CUser)objUser;
            if (User.B_Company_id != m_Company.Id)
                continue;
            lstObj.Add(User);
        }

        int totalPage = lstObj.Count % pageSize == 0 ? lstObj.Count / pageSize : lstObj.Count / pageSize + 1; // 计算总页数

        int index = (page - 1) * pageSize; // 开始记录数  
        for (int i = index; i < pageSize + index && i < lstObj.Count; i++)
        {
            CBaseObject obj = (CBaseObject)lstObj[i];

            string sRow="";
            foreach (CBaseObject objC in m_Table.ColumnMgr.GetList())
            {
                CColumn col = (CColumn)objC;
                if (col.ColType == ColumnType.object_type)
                {
                    string sVal = "";
                    if (obj.GetColValue(col) != null)
                        sVal = "long byte";
                    sRow += string.Format("\"{0}\":\"{1}\",", col.Code, sVal);
                }
                else if (col.ColType == ColumnType.ref_type)
                {
                    CTable RefTable = (CTable)Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.Find(col.RefTable);
                    if (RefTable == null) continue;
                    CBaseObjectMgr objRefMgr = new CBaseObjectMgr();
                    objRefMgr.TbCode = RefTable.Code;
                    objRefMgr.Ctx = Global.GetCtx(Session["TopCompany"].ToString());

                    CColumn RefCol = (CColumn)RefTable.ColumnMgr.Find(col.RefCol);
                    CColumn RefShowCol = (CColumn)RefTable.ColumnMgr.Find(col.RefShowCol);
                    string sWhere = string.Format(" {0}=?", RefCol.Code);
                    List<DbParameter> cmdParas = new List<DbParameter>();
                    cmdParas.Add(new DbParameter(RefCol.Code, obj.GetColValue(col)));
                    List<CBaseObject> lstObj2 = objRefMgr.GetList(sWhere, cmdParas);
                    string sVal = "";
                    if (lstObj2.Count > 0)
                    {
                        CBaseObject obj2 = lstObj2[0];
                        object objVal = obj2.GetColValue(RefShowCol);
                        if (objVal != null)
                            sVal = objVal.ToString();
                    }
                    sRow += string.Format("\"{0}\":\"{1}\",", col.Code, sVal);
                }
                else
                {
                    string sVal = "";
                    object objVal = obj.GetColValue(col);
                    if (objVal != null)
                        sVal = objVal.ToString();
                    Util.ConvertJsonSymbol(ref sVal);
                    sRow += string.Format("\"{0}\":\"{1}\",", col.Code, sVal);
                }

            }
            sRow = sRow.TrimEnd(",".ToCharArray());
            sRow = "{" + sRow + "},";
            sData += sRow;
        }
        sData = sData.TrimEnd(",".ToCharArray());
        sData = "[" + sData + "]";
        string sJson = string.Format("{{\"Rows\":{0},\"Total\":\"{1}\"}}"
            , sData, lstObj.Count);

        Response.Write(sJson);
    }
    void Delete()
    {
        string delid = Request["delid"];
        if (string.IsNullOrEmpty(delid))
        {
            Response.Write("请选择记录！");
            return;
        }
        Global.GetCtx(Session["TopCompany"].ToString()).UserMgr.Delete(new Guid(delid));
        if (!Global.GetCtx(Session["TopCompany"].ToString()).UserMgr.Save(true))
        {
            Response.Write("删除失败！");
            return;
        }
    }
}
