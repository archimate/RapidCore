using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using System.IO;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.UI;

public partial class CommonCtrl_ViewRecordCtrl : System.Web.UI.UserControl
{
    public CTable m_Table = null;
    public CView m_View = null;
    //受限的字段：禁止或者只读权限
    public SortedList<Guid, AccessType> m_sortRestrictColumnAccessType = new SortedList<Guid, AccessType>();
    //界面控件列数
    public int m_iUIColCount = 2;
    //界面控件宽度
    public int m_iUICtrlWidth = 180;
    //外面传进来的默认值，比设置的优先
    public SortedList<string, string> m_sortDefVal = new SortedList<string, string>();
    //需要隐藏的字段
    public SortedList<string, string> m_sortHideColumn = new SortedList<string, string>();
    //外部传进来的引用字段或枚举字段的集合
    public SortedList<string, CBaseObjectMgr> m_sortRefBaseObjectMgr = new SortedList<string, CBaseObjectMgr>();
    //日期型显示时间部分的字段
    public SortedList<string, string> m_sortShowTimeColumn = new SortedList<string, string>();
    //下拉框弹出选择方式的字段
    public SortedList<string, string> m_sortPopupSelDialogColumn = new SortedList<string, string>();


    public CBaseObject m_BaseObject = null;

    protected void Page_Load(object sender, EventArgs e)
    {


    }

    public string GetColumnDefaultVal(CColumnDefaultValInView cdviv, CColumn col)
    {
        //外面传进来的默认值，比设置的优先
        if (m_sortDefVal.ContainsKey(col.Code))
        {
            return m_sortDefVal[col.Code];
        }
        //

        if (cdviv == null||cdviv.DefaultVal.Trim() == "")
            return "";
        //变量
        if (cdviv.DefaultVal.Length > 2 && cdviv.DefaultVal[0] == '[' && cdviv.DefaultVal[cdviv.DefaultVal.Length - 1] == ']')
        {
            CVariable Variable = new CVariable();
            return Variable.GetVarValue(cdviv.DefaultVal);
        }
        //sql语句
        else if (cdviv.DefaultVal.Length > 4 && cdviv.DefaultVal.Substring(0, 4).Equals("sql:", StringComparison.OrdinalIgnoreCase))
        {
            string sSql = cdviv.DefaultVal.Substring(4);
            object obj = Global.GetCtx(Session["TopCompany"].ToString()).MainDB.GetSingle(sSql);
            if (obj == null)
                return "";
            else
                return obj.ToString();
        }
        //常量
        else
            return cdviv.DefaultVal;
    }

    public string GetColumnRefDefaultVal(CColumnDefaultValInView cdviv,CColumn col)
    {
        CTable RefTable = (CTable)Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.Find(col.RefTable);
        CBaseObjectMgr BaseObjectMgr = Global.GetCtx(Session["TopCompany"].ToString()).FindBaseObjectMgrCache(RefTable.Code, Guid.Empty);
        if (BaseObjectMgr == null)
        {
            BaseObjectMgr = new CBaseObjectMgr();
            BaseObjectMgr.TbCode = RefTable.Code;
            BaseObjectMgr.Ctx = Global.GetCtx(Session["TopCompany"].ToString());
        }

        CColumn RefCol = (CColumn)RefTable.ColumnMgr.Find(col.RefCol);
        CColumn RefShowCol = (CColumn)RefTable.ColumnMgr.Find(col.RefShowCol);
        List<CBaseObject> lstObjRef = BaseObjectMgr.GetList();
        string defval = GetColumnDefaultVal(cdviv, col).ToString();
        foreach (CBaseObject objRef in lstObjRef)
        {
            string val = objRef.GetColValue(RefCol).ToString();
            if (defval == val)
            {
                return objRef.GetColValue(RefShowCol).ToString();
            }
        }
        return "";
    }

    //日期型字段是否显示时间部分
    public string GetShowTimel(CColumn col)
    {
        if (m_sortShowTimeColumn.ContainsKey(col.Code))
            return "true";
        else
            return "false";
    }
}
