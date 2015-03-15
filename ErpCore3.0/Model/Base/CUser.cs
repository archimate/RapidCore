// File:    CUser.cs
// Author:  甘孝俭
// email:   ganxiaojian@hotmail.com 
// QQ:      154986287
// http://www.8088net.com
// 协议声明：本软件为开源系统，遵循国际开源组织协议。任何单位或个人可以使用或修改本软件源码，
//          可以用于作为非商业或商业用途，但由于使用本源码所引起的一切后果与作者无关。
//          未经作者许可，禁止任何企业或个人直接出售本源码或者把本软件作为独立的功能进行销售活动，
//          作者将保留追究责任的权利。
// Created: 2011年7月9日 12:40:36
// Purpose: Definition of Class CUser

using System;
using System.Text;
using System.Collections.Generic;
using ErpCoreModel.Framework;
using ErpCoreModel.UI;
using ErpCoreModel.IM;

namespace ErpCoreModel.Base
{
    //用户权限设置：0-默认拥有所有表权限，1-通过设置权限
    public enum enumAccessSetting {All,Setting };
    public class CUser : CBaseObject
    {
        //记录用户最新在线时间，如果时间超过20秒钟，则认为用户下线
        DateTime m_dtimeOnline = DateTime.Now;
        //如果用户登录密码错误3次，则暂停10分钟后才能再登录
        public DateTime m_dtimeLoginErr = DateTime.Now;
        public int m_iLoginErrCount = 0;

        public CUser()
        {
            TbCode = "B_User";
            ClassName = "ErpCoreModel.Base.CUser";

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
        public int Type
        {
            get
            {
                if (m_arrNewVal.ContainsKey("type"))
                    return m_arrNewVal["type"].IntVal;
                else
                    return 0;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("type"))
                    m_arrNewVal["type"].IntVal = value;
                else
                {
                    CValue val = new CValue();
                    val.IntVal = value;
                    m_arrNewVal.Add("type", val);
                }
            }
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

        public string Pwd
        {
            get
            {
                if (m_arrNewVal.ContainsKey("pwd"))
                    return m_arrNewVal["pwd"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("pwd"))
                    m_arrNewVal["pwd"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("pwd", val);
                }
            }
        }
        public string TName
        {
            get
            {
                if (m_arrNewVal.ContainsKey("tname"))
                    return m_arrNewVal["tname"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("tname"))
                    m_arrNewVal["tname"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("tname", val);
                }
            }
        }
        public string Sex
        {
            get
            {
                if (m_arrNewVal.ContainsKey("sex"))
                    return m_arrNewVal["sex"].StrVal;
                else
                    return "男";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("sex"))
                    m_arrNewVal["sex"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("sex", val);
                }
            }
        }
        public string QQ
        {
            get
            {
                if (m_arrNewVal.ContainsKey("qq"))
                    return m_arrNewVal["qq"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("qq"))
                    m_arrNewVal["qq"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("qq", val);
                }
            }
        }
        public string Email
        {
            get
            {
                if (m_arrNewVal.ContainsKey("email"))
                    return m_arrNewVal["email"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("email"))
                    m_arrNewVal["email"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("email", val);
                }
            }
        }
        public string Phone
        {
            get
            {
                if (m_arrNewVal.ContainsKey("phone"))
                    return m_arrNewVal["phone"].StrVal;
                else
                    return "";
            }
            set
            {
                if (m_arrNewVal.ContainsKey("phone"))
                    m_arrNewVal["phone"].StrVal = value;
                else
                {
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add("phone", val);
                }
            }
        }


        public enumAccessSetting AccessSetting
        {
            get
            {
                if (m_arrNewVal.ContainsKey("accesssetting"))
                    return (enumAccessSetting)m_arrNewVal["accesssetting"].IntVal;
                else
                    return enumAccessSetting.All;
            }
            set
            {
                if (m_arrNewVal.ContainsKey("accesssetting"))
                    m_arrNewVal["accesssetting"].IntVal = (int)value;
                else
                {
                    CValue val = new CValue();
                    val.IntVal = (int)value;
                    m_arrNewVal.Add("accesssetting", val);
                }
            }
        }

        //是否某种角色
        public bool IsRole(string sRoleName)
        {
            CCompany Company = (CCompany)Ctx.CompanyMgr.Find(B_Company_id);
            CRole role = Company.RoleMgr.FindByName(sRoleName);
            if (role == null)
                return false;
            if (role.UserInRoleMgr.FindByUserid(this.Id)
                != null)
                return true;
            else
                return false;
        }

        //判断用户是否在线
        public bool IsOnline()
        {
            //如果时间超过20秒钟，则认为用户下线
            DateTime dtimeNow = DateTime.Now;
            TimeSpan span = dtimeNow - m_dtimeOnline;
            if (span.TotalSeconds > 20)
                return false;
            return true;
        }
        //更新用户在线时间
        public void UpdateOnlineTime()
        {
            m_dtimeOnline = DateTime.Now;
        }

        public CDesktopGroupAccessInUserMgr DesktopGroupAccessInUserMgr
        {
            get
            {
                return (CDesktopGroupAccessInUserMgr)this.GetSubObjectMgr("B_DesktopGroupAccessInUser", typeof(CDesktopGroupAccessInUserMgr));
            }
            set
            {
                this.SetSubObjectMgr("B_DesktopGroupAccessInUser", value);
            }
        }
        public CColumnAccessInUserMgr ColumnAccessInUserMgr
        {
            get
            {
                return (CColumnAccessInUserMgr)this.GetSubObjectMgr("B_ColumnAccessInUser", typeof(CColumnAccessInUserMgr));
            }
            set
            {
                this.SetSubObjectMgr("B_ColumnAccessInUser", value);
            }
        }
        public CTableAccessInUserMgr TableAccessInUserMgr
        {
            get
            {
                return (CTableAccessInUserMgr)this.GetSubObjectMgr("B_TableAccessInUser", typeof(CTableAccessInUserMgr));
            }
            set
            {
                this.SetSubObjectMgr("B_TableAccessInUser", value);
            }
        }
        public CReportAccessInUserMgr ReportAccessInUserMgr
        {
            get
            {
                return (CReportAccessInUserMgr)this.GetSubObjectMgr("B_ReportAccessInUser", typeof(CReportAccessInUserMgr));
            }
            set
            {
                this.SetSubObjectMgr("B_ReportAccessInUser", value);
            }
        }
        public CViewAccessInUserMgr ViewAccessInUserMgr
        {
            get
            {
                return (CViewAccessInUserMgr)this.GetSubObjectMgr("B_ViewAccessInUser", typeof(CViewAccessInUserMgr));
            }
            set
            {
                this.SetSubObjectMgr("B_ViewAccessInUser", value);
            }
        }
        public CUserMenuMgr UserMenuMgr
        {
            get
            {
                return (CUserMenuMgr)this.GetSubObjectMgr("UI_UserMenu", typeof(CUserMenuMgr));
            }
            set
            {
                this.SetSubObjectMgr("UI_UserMenu", value);
            }
        }
        public CDesktopMgr DesktopMgr
        {
            get
            {
                return (CDesktopMgr)this.GetSubObjectMgr("UI_Desktop", typeof(CDesktopMgr));
            }
            set
            {
                this.SetSubObjectMgr("UI_Desktop", value);
            }
        }
        public CDesktopAppMgr DesktopAppMgr
        {
            get
            {
                return (CDesktopAppMgr)this.GetSubObjectMgr("UI_DesktopApp", typeof(CDesktopAppMgr));
            }
            set
            {
                this.SetSubObjectMgr("UI_DesktopApp", value);
            }
        }

        public CFriendMgr FriendMgr
        {
            get
            {
                return (CFriendMgr)this.GetSubObjectMgr("IM_Friend", typeof(CFriendMgr));
            }
            set
            {
                this.SetSubObjectMgr("IM_Friend", value);
            }
        }

        //获取用户桌面组权限
        //可写优先，只读次之，禁止最后
        public AccessType GetDesktopGroupAccess(Guid UI_DesktopGroup_id)
        {
            //管理员有所有权限
            if (IsRole("管理员"))
                return AccessType.write;
            //
            AccessType accessType = AccessType.forbide;
            CDesktopGroupAccessInUser dgaiu = DesktopGroupAccessInUserMgr.FindByDesktopGroup(UI_DesktopGroup_id);
            if (dgaiu != null)
            {
                accessType = dgaiu.Access;
                if(accessType== AccessType.write)
                    return AccessType.write;
            }
            
            CCompany Company = (CCompany)Ctx.CompanyMgr.Find(B_Company_id);
            List<CBaseObject> lstObj = Company.RoleMgr.GetList();
            foreach(CBaseObject obj in lstObj)
            {
                CRole role = (CRole)obj;
                if(role.UserInRoleMgr.FindByUserid(Id)!=null)
                {
                    CDesktopGroupAccessInRole dgair=role.DesktopGroupAccessInRoleMgr.FindByDesktopGroup(UI_DesktopGroup_id);
                    if (dgair != null)
                    {
                        if (dgair.Access == AccessType.write)
                            return AccessType.write;
                        else if (dgair.Access == AccessType.read)
                            accessType = AccessType.read;
                    }
                }
            }

            return accessType;
        }
        //获取用户视图权限
        //可写优先，只读次之，禁止最后
        public AccessType GetViewAccess(Guid UI_View_id)
        {
            //管理员有所有权限
            if (IsRole("管理员"))
                return AccessType.write;
            //
            //默认拥有所有权限的用户
            if (AccessSetting == enumAccessSetting.All)
                return AccessType.write;

            AccessType accessType = AccessType.forbide;
            CViewAccessInUser vaiu = ViewAccessInUserMgr.FindByView(UI_View_id);
            if (vaiu != null)
            {
                accessType = vaiu.Access;
                if (accessType == AccessType.write)
                    return AccessType.write;
            }

            CCompany Company = (CCompany)Ctx.CompanyMgr.Find(B_Company_id);
            List<CBaseObject> lstObj = Company.RoleMgr.GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CRole role = (CRole)obj;
                if (role.UserInRoleMgr.FindByUserid(Id) != null)
                {
                    CViewAccessInRole vair = role.ViewAccessInRoleMgr.FindByView(UI_View_id);
                    if (vair != null)
                    {
                        if (vair.Access == AccessType.write)
                            return AccessType.write;
                        else if (vair.Access == AccessType.read)
                            accessType = AccessType.read;
                    }
                }
            }

            return accessType;
        }
        //获取用户表权限
        //可写优先，只读次之，禁止最后
        public AccessType GetTableAccess(Guid FW_Table_id)
        {
            //管理员有所有权限
            if (IsRole("管理员"))
                return AccessType.write;
            //默认拥有所有权限的用户
            if (AccessSetting == enumAccessSetting.All)
                return AccessType.write;

            AccessType accessType = AccessType.forbide;
            CTableAccessInUser taiu = TableAccessInUserMgr.FindByTable(FW_Table_id);
            if (taiu != null)
            {
                accessType = taiu.Access;
                if (accessType == AccessType.write)
                    return AccessType.write;
            }

            CCompany Company = (CCompany)Ctx.CompanyMgr.Find(B_Company_id);
            List<CBaseObject> lstObj = Company.RoleMgr.GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CRole role = (CRole)obj;
                if (role.UserInRoleMgr.FindByUserid(Id) != null)
                {
                    CTableAccessInRole tair = role.TableAccessInRoleMgr.FindByTable(FW_Table_id);
                    if (tair != null)
                    {
                        if (tair.Access == AccessType.write)
                            return AccessType.write;
                        else if (tair.Access == AccessType.read)
                            accessType = AccessType.read;
                    }
                }
            }

            return accessType;
        }
        //获取用户字段权限
        //可写优先，只读次之，禁止最后
        public AccessType GetColumnAccess(CColumn col)
        {
            //管理员有所有权限
            if (IsRole("管理员"))
                return AccessType.write;
            //
            //如果是系统字段，则权限都为只读，避免其他功能读取不到字段值
            if (col.IsSystem)
                return AccessType.read;
            //

            bool bHasSetAccess = false; //是否手动设置字段权限
            AccessType accessType = AccessType.forbide;
            CColumnAccessInUser caiu = ColumnAccessInUserMgr.FindByColumn(col.Id);
            if (caiu != null)
            {
                bHasSetAccess = true;
                accessType = caiu.Access;
                if (accessType == AccessType.write)
                    return AccessType.write;
            }

            CCompany Company = (CCompany)Ctx.CompanyMgr.Find(B_Company_id);
            List<CBaseObject> lstObj = Company.RoleMgr.GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CRole role = (CRole)obj;
                if (role.UserInRoleMgr.FindByUserid(Id) != null)
                {
                    CColumnAccessInRole cair = role.ColumnAccessInRoleMgr.FindByColumn(col.Id);
                    if (cair != null)
                    {
                        bHasSetAccess = true;
                        if (cair.Access == AccessType.write)
                            return AccessType.write;
                        else if (cair.Access == AccessType.read)
                            accessType = AccessType.read;
                    }
                }
            }

            //如果没有手动设置字段权限，默认字段权限为可写，即如果有表权限，则默认拥有所有字段写权限
            if (!bHasSetAccess)
                accessType = AccessType.write;

            return accessType;
        }
        //获取表中所有受限字段权限
        public SortedList<Guid, AccessType> GetRestrictColumnAccessTypeList(CTable table)
        {
            SortedList<Guid, AccessType> sortRestrictColumnAccessType=new SortedList<Guid,AccessType>();
            if (table == null)
                return sortRestrictColumnAccessType;

            List<CBaseObject> lstObj = table.ColumnMgr.GetList();
            foreach (CBaseObject obj in lstObj)
            {
                AccessType accessType = GetColumnAccess((CColumn)obj);
                if (accessType != AccessType.write)
                    sortRestrictColumnAccessType.Add(obj.Id, accessType);
            }
            return sortRestrictColumnAccessType;
        }
    }
}