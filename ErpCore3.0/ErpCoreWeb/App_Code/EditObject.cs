using System;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;
using ErpCoreModel.Framework;
using ErpCoreModel.Base;

/// <summary>
///保存编辑的对象，避免在用户未保存异常退出，在Session_End()里取消编辑
/// </summary>
public class EditObject
{
    static SortedList<string, List<PO>> m_sortEditObject = new SortedList<string, List<PO>>();
    public EditObject()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }
    static public void Add(string SessionID,PO obj)
    {
        if (m_sortEditObject.ContainsKey(SessionID))
        {
            m_sortEditObject[SessionID].Add(obj);
        }
        else
        {
            List<PO> list = new List<PO>();
            list.Add(obj);
            m_sortEditObject.Add(SessionID, list);
        }

    }
    static public void Remove(string SessionID, PO obj)
    {
        if (!m_sortEditObject.ContainsKey(SessionID))
            return;
        m_sortEditObject[SessionID].Remove(obj);

    }
    static public List<PO> GetList(string SessionID)
    {
        List<PO> list = new List<PO>();
        if (m_sortEditObject.ContainsKey(SessionID))
        {
            list = m_sortEditObject[SessionID];
        }
        return list;
    }
    //取消所有编辑对象
    static public void Cancel(string SessionID)
    {
        if (m_sortEditObject.ContainsKey(SessionID))
        {
            List<PO> list = m_sortEditObject[SessionID];
            foreach (PO obj in list)
            {
                obj.Cancel();
            }
            m_sortEditObject.Remove(SessionID);
        }
    }
}
