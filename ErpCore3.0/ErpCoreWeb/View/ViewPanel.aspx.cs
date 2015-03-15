using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.UI;

public partial class View_ViewPanel : System.Web.UI.Page
{
    public CTable m_Table = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null)
        {
            Response.Redirect("../Login.aspx");
            Response.End();
        }


        m_Table = (CTable)Global.GetCtx(Session["TopCompany"].ToString()).ViewMgr.Table;

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
        string catalog_id = Request["catalog_id"];

        string sData = "";
        List<CBaseObject> lstObj = new List<CBaseObject>();
        CBaseObjectMgr BaseObjectMgr = Global.GetCtx(Session["TopCompany"].ToString()).ViewMgr;
        List<CBaseObject> lstV = BaseObjectMgr.GetList();
        foreach (CBaseObject objV in lstV)
        {
            CView View = (CView)objV;
            if (!string.IsNullOrEmpty(catalog_id))
            {
                Guid guid = new Guid(catalog_id);
                if (View.UI_ViewCatalog_id == guid)
                    lstObj.Add(View);
            }
            else
            {
                //所有目录不存在的都放置根目录
                CBaseObject objCatalog = Global.GetCtx(Session["TopCompany"].ToString()).ViewCatalogMgr.Find(View.UI_ViewCatalog_id);
                if (objCatalog == null)
                    lstObj.Add(View);
            }
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
                    CColumn RefCol = (CColumn)RefTable.ColumnMgr.Find(col.RefCol);
                    CColumn RefShowCol = (CColumn)RefTable.ColumnMgr.Find(col.RefShowCol);
                    object objVal = obj.GetColValue(col);


                    string sVal = "";
                    Guid guidParentId = Guid.Empty;
                    if (BaseObjectMgr.m_Parent != null && BaseObjectMgr.m_Parent.Id == (Guid)objVal)
                    {
                        object objVal2 = BaseObjectMgr.m_Parent.GetColValue(RefShowCol);
                        if (objVal2 != null)
                            sVal = objVal2.ToString();
                    }
                    else
                    {
                        CBaseObjectMgr objRefMgr = Global.GetCtx(Session["TopCompany"].ToString()).FindBaseObjectMgrCache(RefTable.Code, guidParentId);
                        if (objRefMgr != null)
                        {
                            CBaseObject objCache = objRefMgr.FindByValue(RefCol, objVal);
                            if (objCache != null)
                            {
                                object objVal2 = objCache.GetColValue(RefShowCol);
                                if (objVal2 != null)
                                    sVal = objVal2.ToString();
                            }
                        }
                        else
                        {
                            objRefMgr = new CBaseObjectMgr();
                            objRefMgr.TbCode = RefTable.Code;
                            objRefMgr.Ctx = Global.GetCtx(Session["TopCompany"].ToString());

                            string sWhere = string.Format(" {0}=?", RefCol.Code);
                            List<DbParameter> cmdParas = new List<DbParameter>();
                            cmdParas.Add(new DbParameter(RefCol.Code, obj.GetColValue(col)));
                            List<CBaseObject> lstObj2 = objRefMgr.GetList(sWhere, cmdParas);
                            if (lstObj2.Count > 0)
                            {
                                CBaseObject obj2 = lstObj2[0];
                                object objVal2 = obj2.GetColValue(RefShowCol);
                                if (objVal2 != null)
                                    sVal = objVal2.ToString();
                            }
                        }
                    }
                    sRow += string.Format("\"{0}\":\"{1}\",", col.Code, sVal);
                }
                else
                {
                    string sVal = "";
                    object objVal = obj.GetColValue(col);
                    if (objVal != null)
                        sVal = objVal.ToString();
                    //转义特殊字符
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
        Global.GetCtx(Session["TopCompany"].ToString()).ViewMgr.Delete(new Guid(delid));
        if (!Global.GetCtx(Session["TopCompany"].ToString()).ViewMgr.Save(true))
        {
            Response.Write("删除失败！");
            return;
        }
    }
}
