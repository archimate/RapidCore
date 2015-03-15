using System;
using System.Collections.Generic;
using System.Text;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;

namespace ErpCore
{
    //自定义变量类
    public class CVariable
    {
        public static Dictionary<string, string> g_VarName = new Dictionary<string, string>();

        public CVariable()
        {
            if (g_VarName.Count == 0)
            {
                g_VarName.Add("[当前用户ID]","当前登录用户的guid");
                g_VarName.Add("[当前用户名]", "当前登录用户的名称");
                g_VarName.Add("[当前单位ID]","当前登录用户所属单位的guid");
                g_VarName.Add("[当前单位名]", "当前登录用户所属单位的名称");
                g_VarName.Add("[顶级单位ID]", "单位级别中最上级单位的guid");
                g_VarName.Add("[顶级单位名]", "单位级别中最上级单位的名称");
            }
        }

        public object GetVarValue(string sKey)
        {
            if (sKey.Equals( "[当前用户ID]", StringComparison.OrdinalIgnoreCase))
            {
                return Program.User.Id;
            }
            else if (sKey.Equals("[当前用户名]", StringComparison.OrdinalIgnoreCase))
            {
                return Program.User.Name;
            }
            else if (sKey.Equals("[当前单位ID]", StringComparison.OrdinalIgnoreCase))
            {
                return Program.User.B_Company_id;
            }
            else if (sKey.Equals("[当前单位名]", StringComparison.OrdinalIgnoreCase))
            {
                CCompany Company = (CCompany)Program.Ctx.CompanyMgr.Find(Program.User.B_Company_id);
                if(Company!=null)
                    return Company.Name;
            }
            else if (sKey.Equals("[顶级单位ID]", StringComparison.OrdinalIgnoreCase))
            {
                CCompany Company = (CCompany)Program.Ctx.CompanyMgr.FindTopCompany();
                if (Company != null)
                    return Company.Id;
            }
            else if (sKey.Equals("[顶级单位名]", StringComparison.OrdinalIgnoreCase))
            {
                CCompany Company = (CCompany)Program.Ctx.CompanyMgr.FindTopCompany();
                if (Company != null)
                    return Company.Name;
            }

            return null;
        }
    }
}
