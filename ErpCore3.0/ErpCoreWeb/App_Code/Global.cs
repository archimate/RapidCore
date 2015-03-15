using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Threading;
using System.Configuration;
using ErpCoreModel.Framework;
using ErpCoreModel.Base;
using ErpCoreModel.Store;
using ErpCoreModel.Invoicing;

public class CtxTime
{
    public string Company = "";
    public DateTime LastTime = DateTime.Now;
}
/// <summary>
///DB 的摘要说明
/// </summary>
public class Global
{
    static bool m_bIsRunningCtxThread = true;
    //保存上次操作时间，如果超时，则释放ctx
    static List<CtxTime> m_lstCtxTime = new List<CtxTime>();


    public Global()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }
    //创建数据库
    static public bool CreateDB(string sCompany)
    {
        string sPath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["VirtualDir"]);
        int idx = sPath.LastIndexOf('\\');
        sPath = sPath.Substring(0, idx + 1);
        string sDestDir =sPath+ string.Format("database\\sqlite\\{0}\\", sCompany);
        if (!Directory.Exists(sDestDir))
        {
            try
            {
                Directory.CreateDirectory(sDestDir);
            }
            catch
            {
                return false;
            }
        }
        string sSrc = sPath + string.Format("database\\sqlite\\ErpCore_template.db");
        string sDest = sDestDir + string.Format("ErpCore.db");
        try
        {
            File.Copy(sSrc, sDest);
        }
        catch
        {
            return false;
        }
        return true;
    }
    static public CContext GetCtx(string sCompany)
    {
        return GetCtx();//暂时测试用

        SortedList<string, CContext> sortContext = new SortedList<string, CContext>();
        if (HttpContext.Current.Application["Context"] != null)
            sortContext = (SortedList<string, CContext>)HttpContext.Current.Application["Context"];

        CContext ctx = null;
        if (!sortContext.ContainsKey(sCompany))
        {
            ctx = new CContext();
            string connstr = ConfigurationManager.AppSettings["dbConnectionString"];
            string sPath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["VirtualDir"]);
            int idx = sPath.LastIndexOf('\\');
            sPath=sPath.Substring(0,idx+1);
            sPath += string.Format("database\\sqlite\\{0}\\ErpCore.db", sCompany);
            if (!File.Exists(sPath))
                return null;
            connstr=string.Format(connstr,sPath);

            ctx.ConnectionString = connstr;
            HttpContext.Current.Application[sCompany] = ctx;
        }
        else
            ctx = sortContext[sCompany];

        bool bHas = false;
        foreach (CtxTime ct in m_lstCtxTime)
        {
            if (ct.Company.Equals(sCompany, StringComparison.OrdinalIgnoreCase))
            {
                ct.LastTime = DateTime.Now;
                bHas = true;
                break;
            }
        }
        if (!bHas)
        {
            CtxTime ct = new CtxTime();
            ct.Company=sCompany;
            ct.LastTime=DateTime.Now;
            m_lstCtxTime.Add(ct);
        }
        return ctx;
    }
    static public CContext GetCtx()
    {
        //启动缓存监控
        StartCtxCacheMonitor();

        string sCompany = "ErpCore";
        SortedList<string, CContext> sortContext = new SortedList<string, CContext>();
        if (HttpContext.Current.Application["Context"] != null)
            sortContext = (SortedList<string, CContext>)HttpContext.Current.Application["Context"];

        CContext ctx = null;
        if (!sortContext.ContainsKey(sCompany))
        {
            ctx = new CContext();
            string connstr = ConfigurationManager.AppSettings["dbConnectionString"];
            //string sPath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["VirtualDir"]);
            //int idx = sPath.LastIndexOf('\\');
            //string sDbPath = sPath.Substring(0, idx + 1);
            //sDbPath += "database\\sqlite\\ErpCore.db";
            //connstr = string.Format(connstr, sDbPath);

            ctx.ConnectionString = connstr;
            sortContext.Add(sCompany, ctx);

            HttpContext.Current.Application["Context"] = sortContext;
        }
        else
            ctx = sortContext[sCompany];

        bool bHas = false;
        foreach (CtxTime ct in m_lstCtxTime)
        {
            if (ct.Company.Equals(sCompany, StringComparison.OrdinalIgnoreCase))
            {
                ct.LastTime = DateTime.Now;
                bHas = true;
                break;
            }
        }
        if (!bHas)
        {
            CtxTime ct = new CtxTime();
            ct.Company = sCompany;
            ct.LastTime = DateTime.Now;
            m_lstCtxTime.Add(ct);
        }
        return ctx;
    }
    //在线商店
    static public CStore GetStore(string sCompany)
    {
        SortedList<string, CStore> sortStore = new SortedList<string, CStore>();
        if(HttpContext.Current.Application["Store"]!=null)
            sortStore = (SortedList<string, CStore>)HttpContext.Current.Application["Store"];

        if (sortStore.ContainsKey(sCompany))
        {
            return sortStore[sCompany];
        }
        else
        {
            CStore store = new CStore();
            store.Ctx = GetCtx(sCompany);

            sortStore.Add(sCompany, store);
            HttpContext.Current.Application["Store"] = sortStore;
            return store;
        }
    }
    static public CStore GetStore()
    {
        string sCompany = "ErpCore";
        if (HttpContext.Current.Session["TopCompany"] != null)
            sCompany = HttpContext.Current.Session["TopCompany"].ToString();
        return GetStore(sCompany);
    }

    //启动缓存监控
    static void StartCtxCacheMonitor()
    {
        if (m_bIsRunningCtxThread)
            return;

        Thread thread = new Thread(new ThreadStart(CtxCacheMonitorThread));
        thread.Start();

        m_bIsRunningCtxThread = true;
    }
    static void CtxCacheMonitorThread()
    {
        while (true)
        {
            foreach (CtxTime ct in m_lstCtxTime)
            {
                TimeSpan span = DateTime.Now - ct.LastTime;
                if (span.TotalHours > 24)//超过1天不使用的释放内存
                {
                    string sCompany = ct.Company;
                    HttpContext.Current.Application[sCompany] = null;
                    m_lstCtxTime.Remove(ct);
                    break;
                }
            }
        }
    }
    //设置文件上传路径
    static public void SetUploadPath()
    {
        CContext ctx = GetCtx();
        string sPath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["VirtualDir"]);
        //设置默认文件上传路径
        ctx.UploadPath = sPath + "\\UploadPath\\";
        if (!Directory.Exists(ctx.UploadPath))
            Directory.CreateDirectory(ctx.UploadPath);

        //设置产品图片上传路径
        string sUploadPath = sPath + "\\Store\\ProductImg\\";
        if (!Directory.Exists(sUploadPath))
            Directory.CreateDirectory(sUploadPath);

        CTable table = ctx.TableMgr.FindByCode("SP_ProductImg");
        if (table != null)
        {
            CColumn col = (CColumn)table.ColumnMgr.FindByCode("Url");
            if (col != null)
                col.UploadPath = sUploadPath;
        }
    }


    //获取桌面图标路径名称,用于拼接路径
    static public string GetDesktopIconPathName()
    {
        //在非SAAS系统中统一使用固定目录 ErpCore
        //if (HttpContext.Current.Session["TopCompany"] != null)
        //    return HttpContext.Current.Session["TopCompany"].ToString();
        return "ErpCore";
    }


    //进销存
    static public CInvoicing GetInvoicing(string sCompany)
    {
        SortedList<string, CInvoicing> sortInvoicing = new SortedList<string, CInvoicing>();
        if (HttpContext.Current.Application["Invoicing"] != null)
            sortInvoicing = (SortedList<string, CInvoicing>)HttpContext.Current.Application["Invoicing"];

        if (sortInvoicing.ContainsKey(sCompany))
        {
            return sortInvoicing[sCompany];
        }
        else
        {
            CInvoicing invoicing = new CInvoicing();
            invoicing.Ctx = GetCtx(sCompany);

            sortInvoicing.Add(sCompany, invoicing);
            HttpContext.Current.Application["Invoicing"] = sortInvoicing;
            return invoicing;
        }
    }
    static public CInvoicing GetInvoicing()
    {
        string sCompany = "ErpCore";
        if (HttpContext.Current.Session["TopCompany"] != null)
            sCompany = HttpContext.Current.Session["TopCompany"].ToString();
        return GetInvoicing(sCompany);
    }
}
