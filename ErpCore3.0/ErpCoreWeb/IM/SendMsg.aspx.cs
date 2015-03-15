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

public partial class IM_SendMsg : System.Web.UI.Page
{
    public CUser m_User = null;
    public CUser m_FriendUser = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null)
        {
            Response.Redirect("../Login.aspx");
            Response.End();
        }
        m_User = (CUser)Session["User"];

        string to_id = Request["to_id"];
        if (string.IsNullOrEmpty(to_id))
        {
            Response.Write("没有好友！");
            Response.End();
        }
        m_FriendUser = (CUser)Global.GetCtx(Session["TopCompany"].ToString()).UserMgr.Find(new Guid(to_id));
        if (m_FriendUser == null)
        {
            Response.Write("没有好友！");
            Response.End();
        }


        if (Request.Params["Action"] == "Send")
        {
            Send();
            Response.End();
        }
        else if (Request.Params["Action"] == "GetMessage")
        {
            GetMessage();
            Response.End();
        }
    }
    //发送消息
    void Send()
    {
        string sMsg = Request["msg"];
        if (string.IsNullOrEmpty(sMsg))
            return;
        //消息处理
        sMsg = sMsg.Replace("\n", "<br>");
        string sHead = "<span style='color:Blue'>" + m_User.Name + " " + DateTime.Now.ToString() + "</span><br>";
        sMsg = sHead + sMsg;

        CMessage msg = new CMessage();
        msg.Ctx = Global.GetCtx(Session["TopCompany"].ToString());
        msg.Content = sMsg;
        msg.Sender = m_User.Id;
        msg.Receiver = m_FriendUser.Id;

        CMessageMgr MessageMgr = Global.GetCtx(Session["TopCompany"].ToString()).MessageMgr;
        if (!MessageMgr.AddNew(msg, true))
        {
        }
        //如果发送者不在接收者的好友里，则添加到接收者的陌生人里
        if (m_FriendUser.FriendMgr.FindByFriendId(m_User.Id) == null)
        {
            CFriend Friend = new CFriend();
            Friend.Ctx = Global.GetCtx(Session["TopCompany"].ToString());
            Friend.B_User_id = m_FriendUser.Id;
            Friend.Friend_id = m_User.Id;
            Friend.IsStrange = true;

            m_FriendUser.FriendMgr.AddNew(Friend, true);
        }
    }
    //获取好友消息
    void GetMessage()
    {
        string to_id = Request["to_id"];
        if (string.IsNullOrEmpty(to_id))
            return;

        CMessageMgr MessageMgr = Global.GetCtx(Session["TopCompany"].ToString()).MessageMgr;
        List<CMessage> lst = MessageMgr.Find(new Guid(to_id), m_User.Id);
        lst.Sort();
        string sData = "";
        for(int i=lst.Count-1;i>=0;i--)
        {
            CMessage msg = lst[i];
            if (!msg.IsNew)
                break;
            sData =  msg.Content + "<br>" + sData;
            //更新消息状态
            msg.IsNew = false;
            MessageMgr.Update(msg);
        }
        Response.Write(sData);

        MessageMgr.Save(true);
    }
}
