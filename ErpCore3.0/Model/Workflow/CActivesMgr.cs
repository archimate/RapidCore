// File:    CActivesMgr.cs
// Created: 2012/7/21 21:38:30
// Purpose: Definition of Class CActivesMgr

using System;
using System.Text;
using System.Collections.Generic;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Workflow
{
    public class CActivesMgr : CBaseObjectMgr
    {

        public CActivesMgr()
        {
            TbCode = "WF_Actives";
            ClassName = "ErpCoreModel.Workflow.CActives";
        }
        //查找未审批活动
        public CActives FindNotApproval()
        {
            GetList();
            foreach (CBaseObject obj in m_lstObj)
            {
                CActives Actives = (CActives)obj;
                if (Actives.Result == enumApprovalResult.Init)
                    return Actives;
            }
            return null;
        }
    }
}