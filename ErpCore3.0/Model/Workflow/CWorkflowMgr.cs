// File:    CWorkflowMgr.cs
// Created: 2012/7/21 21:38:30
// Purpose: Definition of Class CWorkflowMgr

using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using ErpCoreModel.Framework;
using ErpCoreModel.Base;

namespace ErpCoreModel.Workflow
{
    public class CWorkflowMgr : CBaseObjectMgr
    {

        public CWorkflowMgr()
        {
            TbCode = "WF_Workflow";
            ClassName = "ErpCoreModel.Workflow.CWorkflow";
        }
        //获取一条记录所有工作流
        public List<CWorkflow> FindByRowid(Guid Row_id)
        {
            GetList();
            List<CWorkflow> lstRet = new List<CWorkflow>();
            foreach (CBaseObject obj in m_lstObj)
            {
                CWorkflow Workflow = (CWorkflow)obj;
                if (Workflow.Row_id == Row_id)
                    lstRet.Add(Workflow);
            }
            return lstRet;
        }
        //获取一条记录最新的工作流
        public List<CWorkflow> FindLastByRowid(Guid Row_id)
        {
            GetList();
            List<CWorkflow> lstRet = new List<CWorkflow>();
            foreach (CBaseObject obj in m_lstObj)
            {
                CWorkflow Workflow = (CWorkflow)obj;
                if (Workflow.Row_id == Row_id)
                {
                    bool bHas = false;
                    foreach (CWorkflow wf in lstRet)
                    {
                        if (wf.WF_WorkflowDef_id == Workflow.WF_WorkflowDef_id)
                        {
                            if (wf.Created > Workflow.Created)
                                bHas = true;
                            else
                                lstRet.Remove(wf);
                            break;
                        }
                    }
                    if(!bHas)
                        lstRet.Add(Workflow);
                }
            }
            return lstRet;
        }

        //启动工作流
        public bool StartWorkflow(CWorkflow wf,out string sErr)
        {
            
            sErr = "";
            CCompany Company = null;
            if (wf.B_Company_id == Guid.Empty)
                Company = (CCompany)Ctx.CompanyMgr.FindTopCompany();
            else
                Company = (CCompany)Ctx.CompanyMgr.Find(wf.B_Company_id);
            CWorkflowDef WorkflowDef = (CWorkflowDef)Company.WorkflowDefMgr.Find(wf.WF_WorkflowDef_id);
            if (WorkflowDef == null)
            {
                sErr = "工作流定义不存在！";
                return false;
            }
            CActivesDef start = WorkflowDef.ActivesDefMgr.FindStart();
            if (start == null)
            {
                sErr = "启动活动不存在！";
                return false;
            }
            List<CLink> lstLink = WorkflowDef.LinkMgr.FindByPreActives(start.Id);
            if (lstLink.Count == 0)
            {
                sErr = "启动活动没有连接！";
                return false;
            }
            //启动活动有且仅有一个连接
            CLink Link = lstLink[0];
            CActivesDef next = (CActivesDef)WorkflowDef.ActivesDefMgr.Find(Link.NextActives);
            if (next == null)
            {
                sErr = "启动活动没有连接！";
                return false;
            }
            //启动条件
            if(!ValidateCond(WorkflowDef,wf,Link.Condiction,out sErr))
            {
                return false;
            }

            Update(wf);
            wf.State = enumApprovalState.Running;
            //实例化活动
            CActives Actives = new CActives();
            Actives.Ctx = Ctx;
            Actives.WF_Workflow_id = wf.Id;
            Actives.WF_ActivesDef_id = next.Id;
            Actives.Result = enumApprovalResult.Init;
            Actives.AType = next.AType;
            if(Actives.AType=="按用户")
                Actives.B_User_id = next.B_User_id;
            else
                Actives.B_Role_id = next.B_Role_id;
            wf.ActivesMgr.AddNew(Actives);
            //考虑发邮件等通知方式

            //if (!Save(true))
            //{
            //    sErr = "保存失败！";
            //    return false;
            //}

            return true;
        }
        //取消工作流
        public bool CancelWorkflow(CWorkflow wf)
        {
            Update(wf);
            wf.State = enumApprovalState.Cancel;
            return Save(true);
        }
        //审批活动
        public bool Approval(CWorkflow wf, CActives Actives, out string sErr)
        {
            sErr = "";

            CCompany Company = null;
            if (wf.B_Company_id==Guid.Empty)
                Company = (CCompany)Ctx.CompanyMgr.FindTopCompany();
            else
                Company = (CCompany)Ctx.CompanyMgr.Find(wf.B_Company_id);
            if (Company == null)
            {
                sErr = "单位不存在！";
                return false;
            }
            CWorkflowDef WorkflowDef = (CWorkflowDef)Company.WorkflowDefMgr.Find(wf.WF_WorkflowDef_id);
            if (WorkflowDef == null)
            {
                sErr = "工作流定义不存在！";
                return false;
            }
            List<CLink> lstLink = WorkflowDef.LinkMgr.FindByPreActives(Actives.WF_ActivesDef_id);
            if (lstLink.Count == 0)
            {
                sErr = "活动没有连接！";
                return false;
            }
            //找出符合条件的连接,并获取下一个活动
            CActivesDef next = null;
            foreach (CLink link in lstLink)
            {
                if (Actives.Result == link.Result)
                {
                    //检验条件表达式
                    if (!ValidateCond(WorkflowDef,wf, link.Condiction, out sErr))
                    {
                        if (sErr != "")
                            return false;
                        else
                            continue;
                    }

                    next = (CActivesDef)WorkflowDef.ActivesDefMgr.Find( link.NextActives);
                    break;
                }
            }
            if (next == null)
            {
                sErr = "活动没有连接！";
                return false;
            }


            Update(wf);
            wf.ActivesMgr.Update(Actives);
            //如果是结束活动，则结束工作流
            if (next.WType == ActivesType.Success)
                wf.State = enumApprovalState.Accept;
            else if (next.WType == ActivesType.Failure)
                wf.State = enumApprovalState.Reject;
            else
            {
                //实例化下一个活动
                CActives nextActives = new CActives();
                nextActives.Ctx = Ctx;
                nextActives.WF_Workflow_id = wf.Id;
                nextActives.WF_ActivesDef_id = next.Id;
                nextActives.Result = enumApprovalResult.Init;
                nextActives.AType = next.AType;
                if (nextActives.AType == "按用户")
                    nextActives.B_User_id = next.B_User_id;
                else
                    nextActives.B_Role_id = next.B_Role_id;
                wf.ActivesMgr.AddNew(nextActives);
                //考虑发邮件等通知方式

            }

            if (!Save(true))
            {
                sErr = "保存失败！";
                return false;
            }

            return true;
        }
        //检验条件表达式，使用sql查询检验
        bool ValidateCond(CWorkflowDef WorkflowDef, CWorkflow wf, string sCond, out string sErr)
        {
            sErr = "";
            if (sCond.Trim() == "")
                return true;
            //把中括号里的中文替换成字段名
            CTable Table = (CTable)Ctx.TableMgr.Find(WorkflowDef.FW_Table_id);
            if (Table == null)
            {
                sErr = "表对象不存在！";
                return false;
            }
            List<CBaseObject> lstObj = Table.ColumnMgr.GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CColumn Column = (CColumn)obj;
                sCond = sCond.Replace(string.Format("[{0}]", Column.Name), string.Format("[{0}]",Column.Code));
            }
            string sSql = string.Format("select id from [{0}] where id='{1}' and ({2})",Table.Code,wf.Row_id, sCond);
            DataTable dt = Ctx.MainDB.QueryT(sSql);
            if (dt == null)
            {
                sErr = "条件表达式语法错误！";
                return false;
            }
            if (dt.Rows.Count == 0) //条件不符合
            {
                sErr = "条件不符合！";
                return false;
            }

            return true;
        }
    }
}