// File:    CMessageMgr.cs
// Created: 2012/10/9 22:08:37
// Purpose: Definition of Class CMessageMgr

using System;
using System.Text;
using System.Collections.Generic;
using ErpCoreModel.Framework;

namespace ErpCoreModel.IM
{
    public class CMessageMgr : CBaseObjectMgr
    {
        SortedList<Guid, List<CMessage>> m_sortMsg = new SortedList<Guid, List<CMessage> >();

        public CMessageMgr()
        {
            TbCode = "IM_Message";
            ClassName = "ErpCoreModel.IM.CMessage";
        }

        public override bool AddNew(CBaseObject obj, bool bSave)
        {
            CMessage msg = (CMessage)obj;
            if (!m_sortMsg.ContainsKey(msg.Receiver))
            {
                List<CMessage> lst = new List<CMessage>();
                lst.Add(msg);
                m_sortMsg.Add(msg.Receiver, lst);
            }
            else
            {
                m_sortMsg[msg.Receiver].Add(msg);
            }
            return base.AddNew(obj, bSave);
        }

        protected override List<CBaseObject> GetListPO(string sWhere, List<DbParameter> cmdParas, int nTop, string sOrderby)
        {
            base.GetListPO(sWhere, cmdParas,nTop, sOrderby);
            foreach (CBaseObject obj in m_lstObj)
            {
                CMessage msg = (CMessage)obj;
                if (!m_sortMsg.ContainsKey(msg.Receiver))
                {
                    List<CMessage> lst = new List<CMessage>();
                    lst.Add(msg);
                    m_sortMsg.Add(msg.Receiver, lst);
                }
                else
                {
                    m_sortMsg[msg.Receiver].Add(msg);
                }
            }
            return m_lstObj;
        }
        //获取新消息数目
        public int GetNewCount(Guid Sender_id, Guid Receiver_id)
        {
            int iCount = 0;
            List<CMessage> lst = Find(Sender_id, Receiver_id);
            foreach (CMessage msg in lst)
            {
                if (msg.IsNew)
                    iCount++;
            }
            return iCount;
        }
        //获取好友消息列表
        public List<CMessage> Find(Guid Sender_id, Guid Receiver_id)
        {
            GetList();
            List<CMessage> list = new List<CMessage>();
            if (!m_sortMsg.ContainsKey(Receiver_id))
                return list;
            List<CMessage> lstMsg = m_sortMsg[Receiver_id];
            foreach (CMessage msg in lstMsg)
            {
                if (msg.Sender==Sender_id)
                    list.Add(msg);
            }
            return list;
        }
        //删除好友消息列表
        public void Remove(Guid Sender_id, Guid Receiver_id)
        {
            GetList();
            if (!m_sortMsg.ContainsKey(Receiver_id))
                return;
            List<CMessage> lstMsg = m_sortMsg[Receiver_id];
            for (int i = lstMsg.Count - 1; i >= 0; i--)
            {
                CMessage msg = (CMessage)lstMsg[i];
                if (msg.Sender == Sender_id)
                {
                    m_sortObj.Remove(msg.Id);
                    m_lstObj.Remove(msg);
                    lstMsg.RemoveAt(i);
                }
            }
        }
    }
}