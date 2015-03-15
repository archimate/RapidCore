
// File:    CContext.cs
// Author:  甘孝俭
// email:   ganxiaojian@hotmail.com 
// QQ:      154986287
// http://www.8088net.com
// 协议声明：本软件为开源系统，遵循国际开源组织协议。任何单位或个人可以使用或修改本软件源码，
//          可以用于作为非商业或商业用途，但由于使用本源码所引起的一切后果与作者无关。
//          未经作者许可，禁止任何企业或个人直接出售本源码或者把本软件作为独立的功能进行销售活动，
//          作者将保留追究责任的权利。
// Created: 2011年7月11日 22:08:01
// Purpose: Definition of Class CContext

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ErpCoreModel.Framework;
using ErpCoreModel.SubSystem;
using ErpCoreModel.UI;
using ErpCoreModel.Report;
using ErpCoreModel.Workflow;
using ErpCoreModel.IM;

namespace ErpCoreModel.Base
{
    /// 上下文内容，保存顶层对象
    public class CContext
    {
        private CTableMgr tableMgr=null;
        private CDiagramMgr diagramMgr = null;
        private CSystemMgr systemMgr = null;
        private CWindowMgr windowMgr = null;
        private CWindowCatalogMgr windowCatalogMgr = null;
        private CViewMgr viewMgr = null;
        private CViewCatalogMgr viewCatalogMgr = null;
        private CFormMgr formMgr = null;
        private CFormCatalogMgr formCatalogMgr = null;
        private CCompanyMgr companyMgr = null;
        private CProvinceMgr provinceMgr = null;
        private CUserMgr userMgr = null;
        private CMenuMgr menuMgr = null;
        private CMessageMgr messageMgr = null;
        private CDesktopGroupMgr desktopGroupMgr = null;
        private CSyncTimeMgr syncTimeMgr = null;

        #region 第三方接口
        public CSyncTimeMgr SyncTimeMgr
        {
            get
            {
                if (syncTimeMgr == null)
                {
                    syncTimeMgr = new CSyncTimeMgr();
                    syncTimeMgr.Ctx = this;
                    syncTimeMgr.Load("", false);
                    AddBaseObjectMgrCache(syncTimeMgr.TbCode, Guid.Empty, syncTimeMgr);
                }
                return syncTimeMgr;
            }
            set
            {
                this.syncTimeMgr = value;
            }
        }
        #endregion 第三方接口

        #region 缓存数据管理
        //保存所有缓存数据,避免多个地方数据库不同步
        private SortedList<string, SortedList<Guid, CBaseObjectMgr>> m_sortBaseObjectMgrCache = new SortedList<string, SortedList<Guid, CBaseObjectMgr>>();

        public SortedList<string, SortedList<Guid, CBaseObjectMgr>> GetBaseObjectMgrCacheList()
        {
            return m_sortBaseObjectMgrCache;
        }

        public SortedList<Guid, CBaseObjectMgr> FindBaseObjectMgrCache(string sTbCode)
        {
            if (!m_sortBaseObjectMgrCache.ContainsKey(sTbCode))
                return null;
            return m_sortBaseObjectMgrCache[sTbCode];
        }
        public CBaseObjectMgr FindBaseObjectMgrCache(string sTbCode, Guid guidParentId)
        {
            if (!m_sortBaseObjectMgrCache.ContainsKey(sTbCode))
                return null;
            SortedList<Guid, CBaseObjectMgr> sortObjMgr = m_sortBaseObjectMgrCache[sTbCode];
            if (!sortObjMgr.ContainsKey(guidParentId))
                return null;
            return sortObjMgr[guidParentId];
        }
        public void AddBaseObjectMgrCache(string sTbCode, Guid guidParentId, CBaseObjectMgr BaseObjectMgr)
        {
            if (m_sortBaseObjectMgrCache.ContainsKey(sTbCode))
            {
                SortedList<Guid, CBaseObjectMgr> sortObjMgr = m_sortBaseObjectMgrCache[sTbCode];
                if (sortObjMgr.ContainsKey(guidParentId))
                    return;
                else
                    sortObjMgr.Add(guidParentId, BaseObjectMgr);
            }
            else
            {
                SortedList<Guid, CBaseObjectMgr> sortObjMgr = new SortedList<Guid, CBaseObjectMgr>();
                sortObjMgr.Add(guidParentId, BaseObjectMgr);
                m_sortBaseObjectMgrCache.Add(sTbCode, sortObjMgr);
            }
        }
        public void RemoveBaseObjectMgrCache(string sTbCode, Guid guidParentId)
        {
            if (!m_sortBaseObjectMgrCache.ContainsKey(sTbCode))
                return;
            SortedList<Guid, CBaseObjectMgr> sortObjMgr = m_sortBaseObjectMgrCache[sTbCode];
            sortObjMgr.Remove(guidParentId);
            if (sortObjMgr.Count == 0)
                m_sortBaseObjectMgrCache.Remove(sTbCode);
        }
        #endregion 缓存数据管理

        #region 附件型类型字段的文件上传路径
        private string m_sUploadPath = "";
        public string UploadPath
        {
            get
            {
                return this.m_sUploadPath;
            }
            set
            {
                this.m_sUploadPath = value;
            }
        }
        #endregion

        #region 不直接删除数据库记录，只设置删除标志位
        private bool isDeletedFlag = false; 
        public bool IsDeletedFlag
        {
            get
            {
                return isDeletedFlag;
            }
            set
            {
                this.isDeletedFlag = value;
            }
        }
        #endregion

        #region 数据库连接串
        private string connectionString = "";
        public string ConnectionString
        {
            get
            {
                return connectionString;
            }
            set
            {
                this.connectionString = value;
            }
        }
        #endregion

        #region 主数据库
        private DB mainDb = null;
        public DB MainDB
        {
            get
            {
                if (mainDb == null)
                {
                    mainDb = new DB(ConnectionString);
                }
                return mainDb;
            }
            set { mainDb = value; }
        }
        #endregion

        #region 最后错误描述
        private string lastError = "";
        public string LastError
        {
            get
            {
                return lastError;
            }
            set
            {
                this.lastError = value;
            }
        }
        #endregion

        public CTableMgr TableMgr
        {
            get
            {
                if (tableMgr == null)
                {
                    tableMgr = new CTableMgr();
                    tableMgr.Ctx = this;
                    tableMgr.Load("", false);
                    AddBaseObjectMgrCache(tableMgr.TbCode, Guid.Empty, tableMgr);
                }
                return tableMgr;
            }
            set
            {
                this.tableMgr = value;
            }
        }
        public CDiagramMgr DiagramMgr
        {
            get
            {
                if (diagramMgr == null)
                {
                    diagramMgr = new CDiagramMgr();
                    diagramMgr.Ctx = this;
                    diagramMgr.Load("", false);
                    AddBaseObjectMgrCache(diagramMgr.TbCode, Guid.Empty, diagramMgr);
                }
                return diagramMgr;
            }
            set
            {
                this.diagramMgr = value;
            }
        }

        public CSystemMgr SystemMgr
        {
            get
            {
                if (systemMgr == null)
                {
                    systemMgr = new CSystemMgr();
                    systemMgr.Ctx = this;
                    systemMgr.Load("", false);
                    AddBaseObjectMgrCache(systemMgr.TbCode, Guid.Empty, systemMgr);
                }
                return systemMgr;
            }
            set
            {
                this.systemMgr = value;
            }
        }
        public CWindowMgr WindowMgr
        {
            get
            {
                if (windowMgr == null)
                {
                    windowMgr = new CWindowMgr();
                    windowMgr.Ctx = this;
                    windowMgr.Load("", false);
                    AddBaseObjectMgrCache(windowMgr.TbCode, Guid.Empty, windowMgr);
                }
                return windowMgr;
            }
            set
            {
                this.windowMgr = value;
            }
        }
        public CWindowCatalogMgr WindowCatalogMgr
        {
            get
            {
                if (windowCatalogMgr == null)
                {
                    windowCatalogMgr = new CWindowCatalogMgr();
                    windowCatalogMgr.Ctx = this;
                    windowCatalogMgr.Load("", false);
                    AddBaseObjectMgrCache(windowCatalogMgr.TbCode, Guid.Empty, windowCatalogMgr);
                }
                return windowCatalogMgr;
            }
            set
            {
                this.windowCatalogMgr = value;
            }
        }
        public CViewMgr ViewMgr
        {
            get
            {
                if (viewMgr == null)
                {
                    viewMgr = new CViewMgr();
                    viewMgr.Ctx = this;
                    viewMgr.Load("", false);
                    AddBaseObjectMgrCache(viewMgr.TbCode, Guid.Empty, viewMgr);
                }
                return viewMgr;
            }
            set
            {
                this.viewMgr = value;
            }
        }
        public CViewCatalogMgr ViewCatalogMgr
        {
            get
            {
                if (viewCatalogMgr == null)
                {
                    viewCatalogMgr = new CViewCatalogMgr();
                    viewCatalogMgr.Ctx = this;
                    viewCatalogMgr.Load("", false);
                    AddBaseObjectMgrCache(viewCatalogMgr.TbCode, Guid.Empty, viewCatalogMgr);
                }
                return viewCatalogMgr;
            }
            set
            {
                this.viewCatalogMgr = value;
            }
        }
        public CFormMgr FormMgr
        {
            get
            {
                if (formMgr == null)
                {
                    formMgr = new CFormMgr();
                    formMgr.Ctx = this;
                    formMgr.Load("", false);
                    AddBaseObjectMgrCache(formMgr.TbCode, Guid.Empty, formMgr);
                }
                return formMgr;
            }
            set
            {
                this.formMgr = value;
            }
        }
        public CFormCatalogMgr FormCatalogMgr
        {
            get
            {
                if (formCatalogMgr == null)
                {
                    formCatalogMgr = new CFormCatalogMgr();
                    formCatalogMgr.Ctx = this;
                    formCatalogMgr.Load("", false);
                    AddBaseObjectMgrCache(formCatalogMgr.TbCode, Guid.Empty, formCatalogMgr);
                }
                return formCatalogMgr;
            }
            set
            {
                this.formCatalogMgr = value;
            }
        }
        public CCompanyMgr CompanyMgr
        {
            get
            {
                if (companyMgr == null)
                {
                    companyMgr = new CCompanyMgr();
                    companyMgr.Ctx = this;
                    companyMgr.Load("", false);
                    AddBaseObjectMgrCache(companyMgr.TbCode, Guid.Empty, companyMgr);
                }
                return companyMgr;
            }
            set
            {
                this.companyMgr = value;
            }
        }
        //省
        public CProvinceMgr ProvinceMgr
        {
            get
            {
                if (provinceMgr == null)
                {
                    provinceMgr = new CProvinceMgr();
                    provinceMgr.Ctx = this;
                    provinceMgr.Load("", false);
                    AddBaseObjectMgrCache(provinceMgr.TbCode, Guid.Empty, provinceMgr);
                }
                return provinceMgr;
            }
            set
            {
                this.provinceMgr = value;
            }
        }
        public CUserMgr UserMgr
        {
            get
            {
                if (userMgr == null)
                {
                    userMgr = new CUserMgr();
                    userMgr.Ctx = this;
                    userMgr.Load("", false);
                    AddBaseObjectMgrCache(userMgr.TbCode, Guid.Empty, userMgr);
                }
                return userMgr;
            }
            set
            {
                this.userMgr = value;
            }
        }

        public CMenuMgr MenuMgr
        {
            get
            {
                if (menuMgr == null)
                {
                    menuMgr = new CMenuMgr();
                    menuMgr.Ctx = this;
                    menuMgr.Load("", false);
                    AddBaseObjectMgrCache(menuMgr.TbCode, Guid.Empty, menuMgr);
                }
                return menuMgr;
            }
            set
            {
                this.menuMgr = value;
            }
        }
        public CMessageMgr MessageMgr
        {
            get
            {
                if (messageMgr == null)
                {
                    messageMgr = new CMessageMgr();
                    messageMgr.Ctx = this;
                    messageMgr.Load("", false);
                    AddBaseObjectMgrCache(messageMgr.TbCode, Guid.Empty, messageMgr);
                }
                return messageMgr;
            }
            set
            {
                this.messageMgr = value;
            }
        }
        public CDesktopGroupMgr DesktopGroupMgr
        {
            get
            {
                if (desktopGroupMgr == null)
                {
                    desktopGroupMgr = new CDesktopGroupMgr();
                    desktopGroupMgr.Ctx = this;
                    desktopGroupMgr.Load("", false);
                    AddBaseObjectMgrCache(desktopGroupMgr.TbCode, Guid.Empty, desktopGroupMgr);
                }
                return desktopGroupMgr;
            }
            set
            {
                this.desktopGroupMgr = value;
            }
        }
    }
}