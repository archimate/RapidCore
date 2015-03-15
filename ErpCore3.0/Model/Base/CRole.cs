// File:    CRole.cs
// Author:  甘孝俭
// email:   ganxiaojian@hotmail.com 
// QQ:      154986287
// http://www.8088net.com
// 协议声明：本软件为开源系统，遵循国际开源组织协议。任何单位或个人可以使用或修改本软件源码，
//          可以用于作为非商业或商业用途，但由于使用本源码所引起的一切后果与作者无关。
//          未经作者许可，禁止任何企业或个人直接出售本源码或者把本软件作为独立的功能进行销售活动，
//          作者将保留追究责任的权利。
// Created: 2011年7月16日 12:49:31
// Purpose: Definition of Class CRole

using System;
using System.Text;
using System.Collections.Generic;
using ErpCoreModel.Framework;
using ErpCoreModel.UI;

namespace ErpCoreModel.Base
{
   public class CRole : CBaseObject
   {
        public CRole()
        {
            TbCode = "B_Role";
            ClassName = "ErpCoreModel.Base.CRole";

        }
        public string Name
        {
            get
            {
                if (m_arrNewVal.ContainsKey("name"))
                    return m_arrNewVal["name"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("name"))
                    m_arrNewVal["name"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("name", val);
                }
            }
        }
        public Guid B_Company_id
        {
            get
            {
                if (m_arrNewVal.ContainsKey("b_company_id"))
                    return m_arrNewVal["b_company_id"].GuidVal;
                else
                    return Guid.Empty;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("b_company_id"))
                    m_arrNewVal["b_company_id"].GuidVal = value;
                else
                {
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add("b_company_id", val);
                }
            }
        }
        public CDesktopGroupAccessInRoleMgr DesktopGroupAccessInRoleMgr
        {
            get
            {
                return (CDesktopGroupAccessInRoleMgr)this.GetSubObjectMgr("B_DesktopGroupAccessInRole", typeof(CDesktopGroupAccessInRoleMgr));
            }
            set
            {
                this.SetSubObjectMgr("B_DesktopGroupAccessInRole", value);
            }
        }
        public CReportAccessInRoleMgr ReportAccessInRoleMgr
        {
            get
            {
                return (CReportAccessInRoleMgr)this.GetSubObjectMgr("B_ReportAccessInRole", typeof(CReportAccessInRoleMgr));
            }
            set
            {
                this.SetSubObjectMgr("B_ReportAccessInRole", value);
            }
        }
        public CColumnAccessInRoleMgr ColumnAccessInRoleMgr
        {
            get
            {
                return (CColumnAccessInRoleMgr)this.GetSubObjectMgr("B_ColumnAccessInRole", typeof(CColumnAccessInRoleMgr));
            }
            set
            {
                this.SetSubObjectMgr("B_ColumnAccessInRole", value);
            }
        }
        public CTableAccessInRoleMgr TableAccessInRoleMgr
        {
            get
            {
                return (CTableAccessInRoleMgr)this.GetSubObjectMgr("B_TableAccessInRole", typeof(CTableAccessInRoleMgr));
            }
            set
            {
                this.SetSubObjectMgr("B_TableAccessInRole", value);
            }
        }


        public CUserInRoleMgr UserInRoleMgr
        {
            get
            {
                return (CUserInRoleMgr)this.GetSubObjectMgr("B_UserInRole", typeof(CUserInRoleMgr));
            }
            set
            {
                this.SetSubObjectMgr("B_UserInRole", value);
            }
        }
        public CSystemAccessInRoleMgr SystemAccessInRoleMgr
        {
            get
            {
                return (CSystemAccessInRoleMgr)this.GetSubObjectMgr("B_SystemAccessInRole", typeof(CSystemAccessInRoleMgr));
            }
            set
            {
                this.SetSubObjectMgr("B_SystemAccessInRole", value);
            }
        }
        public CViewAccessInRoleMgr ViewAccessInRoleMgr
        {
            get
            {
                return (CViewAccessInRoleMgr)this.GetSubObjectMgr("B_ViewAccessInRole", typeof(CViewAccessInRoleMgr));
            }
            set
            {
                this.SetSubObjectMgr("B_ViewAccessInRole", value);
            }
        }
        public CRoleMenuMgr RoleMenuMgr
        {
            get
            {
                return (CRoleMenuMgr)this.GetSubObjectMgr("UI_RoleMenu", typeof(CRoleMenuMgr));
            }
            set
            {
                this.SetSubObjectMgr("UI_RoleMenu", value);
            }
        }

   }
}