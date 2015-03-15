// File:    CWorkflowCatalogMgr.cs
// Created: 2012/7/21 21:38:30
// Purpose: Definition of Class CWorkflowCatalogMgr

using System;
using System.Text;
using System.Collections.Generic;
using ErpCoreModel.Framework;

namespace ErpCoreModel.Workflow
{
    public class CWorkflowCatalogMgr : CBaseObjectMgr
    {

        public CWorkflowCatalogMgr()
        {
            TbCode = "WF_WorkflowCatalog";
            ClassName = "ErpCoreModel.Workflow.CWorkflowCatalog";
        }

    }
}