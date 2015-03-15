using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.UI;

public partial class View_MultDetailList : System.Web.UI.Page
{
    public CUser m_User = null;
    public CTable m_Table = null;
    public CView m_View = null;
    public CViewDetail m_ViewDetail = null;
    public Guid m_guidParentId = Guid.Empty;
    AccessType m_ViewAccessType = AccessType.forbide;
    AccessType m_TableAccessType = AccessType.forbide;
    //受限的字段：禁止或者只读权限
    SortedList<Guid, AccessType> m_sortRestrictColumnAccessType = new SortedList<Guid, AccessType>();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null)
        {
            Response.End();
        }
        m_User = (CUser)Session["User"];

        string vid = Request["vid"];
        if (string.IsNullOrEmpty(vid))
        {
            Response.End();
        }
        string vdid = Request["vdid"];
        if (string.IsNullOrEmpty(vdid))
        {
            Response.End();
        }
        m_View = (CView)Global.GetCtx(Session["TopCompany"].ToString()).ViewMgr.Find(new Guid(vid));
        if (m_View==null)
        {
            Response.End();
        }
        m_ViewDetail = (CViewDetail)m_View.ViewDetailMgr.Find(new Guid(vdid));
        if (m_ViewDetail == null)
        {
            Response.End();
        }
        m_Table = (CTable)Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.Find(m_ViewDetail.FW_Table_id);

        //检查权限
        if (!CheckAccess())
        {
            Response.End();
        }

        string ParentId = Request["ParentId"];
        if (!string.IsNullOrEmpty(ParentId))
            m_guidParentId = new Guid(ParentId);

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
    //检查权限
    bool CheckAccess()
    {
        //判断视图权限
        m_ViewAccessType = m_User.GetViewAccess(m_ViewDetail.UI_View_id);
        if (m_ViewAccessType == AccessType.forbide)
        {
            Response.Write("没有视图权限！");
            return false;
        }

        //判断表权限
        m_TableAccessType = m_User.GetTableAccess(m_Table.Id);
        if (m_TableAccessType == AccessType.forbide)
        {
            Response.Write("没有表权限！");
            return false;
        }
        else if (m_TableAccessType == AccessType.read)
        {
        }
        else
        {
        }
        m_sortRestrictColumnAccessType = m_User.GetRestrictColumnAccessTypeList(m_Table);

        return true;
    }

    void GetData()
    {
        string pid = Request["pid"];
        if (string.IsNullOrEmpty(pid))
            return;

        CBaseObjectMgr BaseObjectMgr = Global.GetCtx(Session["TopCompany"].ToString()).FindBaseObjectMgrCache(m_Table.Code, new Guid(pid));
        if (BaseObjectMgr == null)
        {
            BaseObjectMgr = new CBaseObjectMgr();
            BaseObjectMgr.TbCode = m_Table.Code;
            BaseObjectMgr.Ctx = Global.GetCtx(Session["TopCompany"].ToString());

            CColumn colF = (CColumn)m_Table.ColumnMgr.Find(m_ViewDetail.ForeignKey);
            if (colF == null)
                return;
            string sWhere0 = string.Format(" {0}='{1}'", colF.Code, pid);
            BaseObjectMgr.GetList(sWhere0);
        }
        List<CBaseObject> lstObj = BaseObjectMgr.GetList();

        string sData = "";
        foreach(CBaseObject obj in lstObj)
        {
            string sRow = "";
            foreach (CBaseObject objC in m_Table.ColumnMgr.GetList())
            {
                CColumn col = (CColumn)objC;
                //判断禁止权限字段
                if (m_sortRestrictColumnAccessType.ContainsKey(col.Id))
                {
                    AccessType accessType = m_sortRestrictColumnAccessType[col.Id];
                    if (accessType == AccessType.forbide)
                    {
                        string sVal = "";
                        sRow += string.Format("\"{0}\":\"{1}\",", col.Code, sVal);
                        continue;
                    }
                }
                //

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
        CBaseObjectMgr BaseObjectMgr = Global.GetCtx(Session["TopCompany"].ToString()).FindBaseObjectMgrCache(m_Table.Code, m_guidParentId);
        if (BaseObjectMgr == null)
        {
            BaseObjectMgr = new CBaseObjectMgr();
            BaseObjectMgr.TbCode = m_Table.Code;
            BaseObjectMgr.Ctx = Global.GetCtx(Session["TopCompany"].ToString());
            string sWhere = string.Format(" id='{0}'", delid);
            BaseObjectMgr.GetList(sWhere);
        }
        if (!BaseObjectMgr.Delete(new Guid(delid)))
        {
            Response.Write(BaseObjectMgr.Ctx.LastError);
            return;
        }
        if (!BaseObjectMgr.Save( true))
        {
            Response.Write("删除失败！");
            return;
        }
    }


    public List<CColumn> GetDetailColList()
    {
        List<CColumn> lstRet = new List<CColumn>();

        List<CBaseObject> lstObjCIVD = m_ViewDetail.ColumnInViewDetailMgr.GetList();
        for (int i = 0; i < lstObjCIVD.Count; i++)
        {
            CColumnInViewDetail civd = (CColumnInViewDetail)lstObjCIVD[i];
            CColumn col = (CColumn)m_Table.ColumnMgr.Find(civd.FW_Column_id);
            if (col == null)
                continue;
            lstRet.Add(col);
        }
        return lstRet;
    }
}
