using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;


    //自定义变量类
    public class CVariable
    {
        public static Dictionary<string, string> g_VarName = new Dictionary<string, string>();

        public CVariable()
        {
            if (g_VarName.Count == 0)
            {
                g_VarName.Add("[当前用户ID]", "当前登录用户的guid");
                g_VarName.Add("[当前用户名]", "当前登录用户的名称");
                g_VarName.Add("[当前单位ID]", "当前登录用户所属单位的guid");
                g_VarName.Add("[当前单位名]", "当前登录用户所属单位的名称");
                g_VarName.Add("[顶级单位ID]", "单位级别中最上级单位的guid");
                g_VarName.Add("[顶级单位名]", "单位级别中最上级单位的名称");
            }
        }

        public string GetVarValue(string sKey)
        {
            if (sKey.Equals( "[当前用户ID]", StringComparison.OrdinalIgnoreCase))
            {
                if (HttpContext.Current.Session["User"] != null)
                {
                    CUser User = (CUser)HttpContext.Current.Session["User"];
                    return User.Id.ToString();
                }
            }
            else if (sKey.Equals("[当前用户名]", StringComparison.OrdinalIgnoreCase))
            {
                if (HttpContext.Current.Session["User"] != null)
                {
                    CUser User = (CUser)HttpContext.Current.Session["User"];
                    return User.Name;
                }
            }
            else if (sKey.Equals("[当前单位ID]", StringComparison.OrdinalIgnoreCase))
            {
                if (HttpContext.Current.Session["User"] != null)
                {
                    CUser User = (CUser)HttpContext.Current.Session["User"];
                    return User.B_Company_id.ToString();
                }
            }
            else if (sKey.Equals("[当前单位名]", StringComparison.OrdinalIgnoreCase))
            {
                if (HttpContext.Current.Session["User"] != null)
                {
                    CUser User = (CUser)HttpContext.Current.Session["User"];
                    CCompany Company = (CCompany)Global.GetCtx(HttpContext.Current.Session["TopCompany"].ToString()).CompanyMgr.Find(User.B_Company_id);
                    if (Company != null)
                        return Company.Name;
                }
            }
            else if (sKey.Equals("[顶级单位ID]", StringComparison.OrdinalIgnoreCase))
            {
                if (HttpContext.Current.Session["TopCompany"] != null)
                {
                    CCompany Company = (CCompany)Global.GetCtx(HttpContext.Current.Session["TopCompany"].ToString()).CompanyMgr.FindTopCompany();
                    if (Company != null)
                        return Company.Id.ToString();
                }
            }
            else if (sKey.Equals("[顶级单位名]", StringComparison.OrdinalIgnoreCase))
            {
                if (HttpContext.Current.Session["TopCompany"] != null)
                {
                    CCompany Company = (CCompany)Global.GetCtx(HttpContext.Current.Session["TopCompany"].ToString()).CompanyMgr.FindTopCompany();
                    if (Company != null)
                        return Company.Name;
                }
            }

            return "";
        }
    }
