using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.UI;

public partial class View_MultDetailListRecord2 : System.Web.UI.Page
{
    public CUser m_User = null;
    public CTable m_Table = null;
    public CView m_View = null;
    public CViewDetail m_ViewDetail = null;
    public CBaseObjectMgr m_BaseObjectMgr = null;
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

        if (Session["EditMultMasterDetailViewRecord"] == null)
        {
            Response.End();
        }

        SortedList<Guid, CBaseObject> arrP = (SortedList<Guid, CBaseObject>)Session["EditMultMasterDetailViewRecord"];
        CBaseObject objP = (CBaseObject)arrP.Values[0];
        m_BaseObjectMgr = objP.GetSubObjectMgr(m_Table.Code, typeof(CBaseObjectMgr));

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
        m_ViewAccessType = m_User.GetViewAccess(m_View.Id);
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

        string sData = "";
        CBaseObjectMgr BaseObjectMgr = m_BaseObjectMgr;
        List<CBaseObject> lstObj = BaseObjectMgr.GetList();

        foreach (CBaseObject obj in lstObj)
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


                    Guid guidParentId = Guid.Empty;
                    if (BaseObjectMgr.m_Parent != null && col.Code.Equals(BaseObjectMgr.m_Parent.TbCode + "_id", StringComparison.OrdinalIgnoreCase))
                        guidParentId = BaseObjectMgr.m_Parent.Id;
                    CBaseObjectMgr objRefMgr = Global.GetCtx(Session["TopCompany"].ToString()).FindBaseObjectMgrCache(RefTable.Code, guidParentId);
                    string sVal = "";
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
        m_BaseObjectMgr.Delete(new Guid(delid));
    }
}
