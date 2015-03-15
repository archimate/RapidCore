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

public partial class CommonCtrl_WindowRecordCtrl : System.Web.UI.UserControl
{
    public CTable m_Table = null;
    //受限的字段：禁止或者只读权限
    //public SortedList<Guid, AccessType> m_sortRestrictColumnAccessType = new SortedList<Guid, AccessType>();
    //界面控件列数
    public int m_iUIColCount = 2;
    //界面控件宽度
    public int m_iUICtrlWidth = 180;
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
    //日期型字段是否显示时间部分
    public string GetShowTimel(CColumn col)
    {
        if (m_sortShowTimeColumn.ContainsKey(col.Code))
            return "true";
        else
            return "false";
    }
}
