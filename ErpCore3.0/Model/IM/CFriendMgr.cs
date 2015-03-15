// File:    CFriendMgr.cs
// Created: 2012/10/9 22:08:37
// Purpose: Definition of Class CFriendMgr

using System;
using System.Text;
using System.Collections.Generic;
using ErpCoreModel.Framework;

namespace ErpCoreModel.IM
{
    public class CFriendMgr : CBaseObjectMgr
    {

        public CFriendMgr()
        {
            TbCode = "IM_Friend";
            ClassName = "ErpCoreModel.IM.CFriend";
        }

        public CFriend FindByFriendId(Guid fid)
        {
            GetList();
            foreach (CBaseObject obj in m_lstObj)
            {
                CFriend friend = (CFriend)obj;
                if (friend.Friend_id == fid)
                    return friend;
            }
            return null;
        }
    }
}