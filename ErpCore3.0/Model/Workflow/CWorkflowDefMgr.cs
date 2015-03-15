// File:    CWorkflowDefMgr.cs
// Created: 2012/7/21 21:38:30
// Purpose: Definition of Class CWorkflowDefMgr

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Workflow
{
    public class CWorkflowDefMgr : CBaseObjectMgr
    {

        public CWorkflowDefMgr()
        {
            TbCode = "WF_WorkflowDef";
            ClassName = "ErpCoreModel.Workflow.CWorkflowDef";
        }


        public CWorkflowDef FindByName(string sName)
        {
            GetList();
            foreach (CBaseObject obj in m_lstObj)
            {
                CWorkflowDef WorkflowDef = (CWorkflowDef)obj;
                if (WorkflowDef.Name.Equals(sName, StringComparison.OrdinalIgnoreCase))
                    return WorkflowDef;
            }
            return null;
        }
        //查找关联表对象的工作流
        public List<CBaseObject> FindByTable(Guid FW_Table_id)
        {
            List<CBaseObject> lstObj = GetList();
            var varObj = from obj in lstObj
                         where (obj as CWorkflowDef).FW_Table_id == FW_Table_id
                         select obj;

            return varObj.ToList() ;
        }
    }
}