using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.UI;
using ErpCoreModel.IM;

public partial class IM_Friend : System.Web.UI.Page
{
    public CTable m_Table = null;
    public CUser m_User = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null)
        {
            Response.Redirect("../Login.aspx");
            Response.End();
        }
        m_User = (CUser)Session["User"];

        m_Table = m_User.FriendMgr.Table;

        if (Request.Params["Action"] == "GetData")
        {
            GetData();
            Response.End();
        }
        else if (Request.Params["Action"] == "Add")
        {
            Add();
            Response.End();
        }
        else if (Request.Params["Action"] == "Delete")
        {
            Delete();
            Response.End();
        }
        else if (Request.Params["Action"] == "GetFriendState")
        {
            GetFriendState();
            Response.End();
        }
    }
    void GetData()
    {
        string sData = "";
        List<CBaseObject> lstObj = m_User.FriendMgr.GetList();

        foreach(CBaseObject obj in lstObj)
        {
            CFriend Friend = (CFriend)obj;

            string sRow="";
            sRow += string.Format("\"id\":\"{0}\",", Friend.Id);
            sRow += string.Format("\"Friend_id\":\"{0}\",", Friend.Friend_id);
            CUser UF = (CUser)Global.GetCtx(Session["TopCompany"].ToString()).UserMgr.Find(Friend.Friend_id);
            if (UF == null)
                continue;
            string sIcon = string.Format("<img src='../images/{0}' width='16' height='16'/>&nbsp;&nbsp;",UF.IsOnline()?"on.png":"off.png");
            sRow += string.Format("\"Friend\":\"{0}{1}\",",sIcon, UF.Name);
            sRow += string.Format("\"group\":\"{0}\",", Friend.IsStrange?"陌生人":"我的好友");
            
            sRow = "{" + sRow + "},";
            sData += sRow;
        }
        sData = sData.TrimEnd(",".ToCharArray());
        sData = "[" + sData + "]";
        string sJson = string.Format("{{\"Rows\":{0},\"Total\":\"{1}\"}}"
            , sData, lstObj.Count);

        Response.Write(sJson);
    }
    void Add()
    {
        string fid = Request["fid"];
        if (string.IsNullOrEmpty(fid))
        {
            Response.Write("请选择用户！");
            return;
        }
        Guid guidFid = new Guid(fid);
        if (guidFid == m_User.Id)
        {
            Response.Write("不能添加自己为好友！");
            return;
        }
        CFriend Friend = m_User.FriendMgr.FindByFriendId(guidFid);
        if (Friend != null) //可能是从陌生人变成好友
        {
            Friend.IsStrange = false;
            m_User.FriendMgr.Update(Friend);
            if (!m_User.FriendMgr.Save(true))
            {
                Response.Write("添加失败！");
                return;
            }
        }
        else
        {
            Friend = new CFriend();
            Friend.Ctx = Global.GetCtx(Session["TopCompany"].ToString());
            Friend.B_User_id = m_User.Id;
            Friend.Friend_id = guidFid;
            Friend.IsStrange = false;

            m_User.FriendMgr.AddNew(Friend);
            if (!m_User.FriendMgr.Save(true))
            {
                Response.Write("添加失败！");
                return;
            }
        }
    }
    void Delete()
    {
        string delid = Request["delid"];
        if (string.IsNullOrEmpty(delid))
        {
            Response.Write("请选择记录！");
            return;
        }

        if (!m_User.FriendMgr.Delete(new Guid(delid), true))
        {
            Response.Write("删除失败！");
            return;
        }
    }
    public string GetCompanyId()
    {
        CUser user = (CUser)Session["User"];
        return user.B_Company_id.ToString();
    }
    //获取好友状态及消息数
    public void GetFriendState()
    {
        CMessageMgr MessageMgr = Global.GetCtx(Session["TopCompany"].ToString()).MessageMgr;
        string sData = "";
        List<CBaseObject> lstObj = m_User.FriendMgr.GetList();
        bool bHasNewMsg = false;
        foreach(CBaseObject obj in lstObj)
        {
            CFriend Friend = (CFriend)obj;

            string sRow="";
            sRow += string.Format("\"id\":\"{0}\",", Friend.Id);
            sRow += string.Format("\"Friend_id\":\"{0}\",", Friend.Friend_id);
            CUser UF = (CUser)Global.GetCtx(Session["TopCompany"].ToString()).UserMgr.Find(Friend.Friend_id);
            if (UF == null)
                continue;
            string sIcon = string.Format("<img src='../images/{0}' width='16' height='16'/>&nbsp;&nbsp;",UF.IsOnline()?"on.png":"off.png");
            string sCount = "";
            int iCount = MessageMgr.GetNewCount(UF.Id,m_User.Id);
            if (iCount > 0)
            {
                bHasNewMsg = true;
                sCount = string.Format("({0})", iCount);
            }
            sRow += string.Format("\"Friend\":\"{0}{1}{2}\",", sIcon, UF.Name, sCount);
            sRow += string.Format("\"group\":\"{0}\",", Friend.IsStrange?"陌生人":"我的好友");
            
            sRow = "{" + sRow + "},";
            sData += sRow;
        }
        sData = sData.TrimEnd(",".ToCharArray());
        sData = "[" + sData + "]";
        string sJson = string.Format("{{\"Rows\":{0},\"Total\":\"{1}\",\"HasNewMsg\":\"{2}\"}}"
            , sData, lstObj.Count, bHasNewMsg?"1":"0");

        Response.Write(sJson);
    }
}
