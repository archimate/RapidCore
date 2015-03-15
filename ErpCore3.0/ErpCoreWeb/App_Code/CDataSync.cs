using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Threading;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using ErpCoreModel.Framework;
using ErpCoreModel.Base;

/// <summary>
///CDataSync 第三方系统基础数据同步类
/// </summary>
public class CDataSync
{
    public static bool m_bIsRunningSync = false;

	public CDataSync()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    //启动数据同步
    public static void StartSync()
    {
        if (m_bIsRunningSync)
            return;

        Thread thread = new Thread(new ThreadStart(SyncThread));
        thread.Start();

        m_bIsRunningSync = true;
    }
    static void SyncThread()
    {
        try
        {
            string connstr = ConfigurationManager.AppSettings["dbSyncConnectionString"];
            string Version = ConfigurationManager.AppSettings["Version"];
            while (true)
            {
                if (Version == "永康版")
                {
                }
                else if (Version == "广州版")
                {
                    SyncGuangzhou(connstr);
                }

                Thread.Sleep(10 * 60 * 1000); //15分钟同步一次
            }
        }
        catch
        {
        }
    }

    //广州财经数字化校园 同步
    static void SyncGuangzhou(string connstr)
    {
        string SystemName = "广州财经数字化校园";
        using (SqlConnection conn = new SqlConnection(connstr))
        {
            CCompany Company = Global.GetCtx().CompanyMgr.FindTopCompany();
            //同步班级
            COrgMgr OrgMgr = Company.OrgMgr;
            string TableCode = "LYDS_TEACHINGCLASSES";
            {
                ErpCoreModel.Base.CSyncTime SyncTime = Global.GetCtx().SyncTimeMgr.FindByTbCode(SystemName, TableCode);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "proc_LYDS_TEACHINGCLASSES";
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter para = new SqlParameter("@optime ", SqlDbType.DateTime);
                if (SyncTime == null)
                    para.Value = null;
                else
                    para.Value = SyncTime.LastTime;
                cmd.Parameters.Add(para);
                SqlDataAdapter dp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                dp.Fill(ds);
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        string ordid = r["id"].ToString();
                        string name = r["name"].ToString();
                        string speciality = r["speciality"].ToString();
                        string academy = r["academy"].ToString();
                        string typeflag = r["typeflag"].ToString();
                        if (r["optime"] != DBNull.Value)
                        {
                            DateTime dtime = (DateTime)r["optime"];
                            if (SyncTime == null)
                            {
                                SyncTime = new CSyncTime();
                                SyncTime.Ctx = Global.GetCtx();
                                SyncTime.SystemName = SystemName;
                                SyncTime.TableCode = TableCode;
                                SyncTime.LastTime = dtime;
                                Global.GetCtx().SyncTimeMgr.AddNew(SyncTime,true);
                            }
                            if (dtime > SyncTime.LastTime)
                                SyncTime.LastTime = dtime;
                        }

                        if (!speciality.Equals(ConfigurationManager.AppSettings["speciality"], StringComparison.OrdinalIgnoreCase))//仅仅处理会计专业
                            continue;

                        if (typeflag.Equals("I", StringComparison.OrdinalIgnoreCase)) //新增
                        {
                            if (OrgMgr.FindByName(ordid) != null)
                                continue;
                            COrg Org = new COrg();
                            Org.Ctx = OrgMgr.Ctx;
                            Org.Name = ordid;
                            Org.B_Company_id = Company.Id;
                            OrgMgr.AddNew(Org);
                            if (!OrgMgr.Save(true))
                            {
                            }
                        }
                        else if (typeflag.Equals("U", StringComparison.OrdinalIgnoreCase)) //修改
                        {
                            COrg Org = OrgMgr.FindByName(ordid);
                            if (Org != null)
                            {
                                Org.Name = ordid;
                            }
                            else
                            {
                                Org = new COrg();
                                Org.Ctx = OrgMgr.Ctx;
                                Org.Name = ordid;
                                Org.B_Company_id = Company.Id;
                                OrgMgr.AddNew(Org);
                            }
                            if (!OrgMgr.Save(true))
                            {
                            }
                        }
                        else if (typeflag.Equals("D", StringComparison.OrdinalIgnoreCase)) //删除
                        {
                            COrg Org = OrgMgr.FindByName(ordid);
                            if (Org != null)
                            {
                                OrgMgr.Delete(Org);
                                if (!OrgMgr.Save(true))
                                {
                                }
                            }
                        }
                    }
                }
                if (SyncTime != null)
                {
                    Global.GetCtx().SyncTimeMgr.Update(SyncTime);
                    if (!Global.GetCtx().SyncTimeMgr.Save(true))
                    {
                    }
                }
            }
            //同步教师
            TableCode = "LYDS_TEACHERS";
            {
                ErpCoreModel.Base.CSyncTime SyncTime = Global.GetCtx().SyncTimeMgr.FindByTbCode(SystemName, TableCode);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "proc_LYDS_TEACHERS";
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter para = new SqlParameter("@optime ", SqlDbType.DateTime);
                if (SyncTime == null)
                    para.Value = null;
                else
                    para.Value = SyncTime.LastTime;
                cmd.Parameters.Add(para);
                SqlDataAdapter dp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                dp.Fill(ds);
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        string userid = r["userid"].ToString();
                        string name = r["name"].ToString();
                        string password = r["password"].ToString();
                        string academy = r["academy"].ToString();
                        string status = r["status"].ToString();
                        string typeflag = r["typeflag"].ToString();
                        if (r["optime"] != DBNull.Value)
                        {
                            DateTime dtime = (DateTime)r["optime"];
                            if (SyncTime == null)
                            {
                                SyncTime = new CSyncTime();
                                SyncTime.Ctx = Global.GetCtx();
                                SyncTime.SystemName = SystemName;
                                SyncTime.TableCode = TableCode;
                                SyncTime.LastTime = dtime;
                                Global.GetCtx().SyncTimeMgr.AddNew(SyncTime, true);
                            }
                            if (dtime > SyncTime.LastTime)
                                SyncTime.LastTime = dtime;
                        }

                        if (!academy.Equals(ConfigurationManager.AppSettings["academy"], StringComparison.OrdinalIgnoreCase))//仅仅处理会计
                            continue;

                        if (typeflag.Equals("I", StringComparison.OrdinalIgnoreCase)) //新增
                        {
                            if (Global.GetCtx().UserMgr.FindByName(userid) != null)
                                continue;
                            CUser User = new CUser();
                            User.Ctx = Global.GetCtx().UserMgr.Ctx;
                            User.Name = userid;
                            User.TName = name;
                            User.B_Company_id = Company.Id;
                            User.Type = 1;//教师
                            Global.GetCtx().UserMgr.AddNew(User);
                            if (!Global.GetCtx().UserMgr.Save(true))
                            {
                            }
                        }
                        else if (typeflag.Equals("U", StringComparison.OrdinalIgnoreCase)) //修改
                        {
                            CUser User = Global.GetCtx().UserMgr.FindByName(userid);
                            if (User != null)
                            {
                                User.TName = name;
                                User.Pwd = password;
                            }
                            else
                            {
                                User = new CUser();
                                User.Ctx = Global.GetCtx().UserMgr.Ctx;
                                User.Name = userid;
                                User.TName = name;
                                User.Pwd = password;
                                User.B_Company_id = Company.Id;
                                User.Type = 1;//教师
                                Global.GetCtx().UserMgr.AddNew(User);
                            }
                            if (!Global.GetCtx().UserMgr.Save(true))
                            {
                            }
                        }
                        else if (typeflag.Equals("D", StringComparison.OrdinalIgnoreCase)) //删除
                        {
                            CUser User = Global.GetCtx().UserMgr.FindByName(userid);
                            if (User != null)
                            {
                                Global.GetCtx().UserMgr.Delete(User);
                                if (!Global.GetCtx().UserMgr.Save(true))
                                {
                                }
                            }
                        }
                    }
                }
                if (SyncTime != null)
                {
                    Global.GetCtx().SyncTimeMgr.Update(SyncTime);
                    if (!Global.GetCtx().SyncTimeMgr.Save(true))
                    {
                    }
                }
            }
            //同步学生
            TableCode = "LYDS_STUDENTS";
            {
                ErpCoreModel.Base.CSyncTime SyncTime = Global.GetCtx().SyncTimeMgr.FindByTbCode(SystemName, TableCode);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "proc_LYDS_STUDENTS";
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter para = new SqlParameter("@optime ", SqlDbType.DateTime);
                if (SyncTime == null)
                    para.Value = null;
                else
                    para.Value = SyncTime.LastTime;
                cmd.Parameters.Add(para);
                SqlDataAdapter dp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                dp.Fill(ds);
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        string userid = r["userid"].ToString();
                        string name = r["name"].ToString();
                        string password = r["password"].ToString();
                        string speciality = r["speciality"].ToString();
                        string teachingclass = r["teachingclass"].ToString();
                        string status = r["status"].ToString();
                        string typeflag = r["typeflag"].ToString();
                        if (r["optime"] != DBNull.Value)
                        {
                            DateTime dtime = (DateTime)r["optime"];
                            if (SyncTime == null)
                            {
                                SyncTime = new CSyncTime();
                                SyncTime.Ctx = Global.GetCtx();
                                SyncTime.SystemName = SystemName;
                                SyncTime.TableCode = TableCode;
                                SyncTime.LastTime = dtime;
                                Global.GetCtx().SyncTimeMgr.AddNew(SyncTime, true);
                            }
                            if (dtime > SyncTime.LastTime)
                                SyncTime.LastTime = dtime;
                        }

                        if (!speciality.Equals(ConfigurationManager.AppSettings["speciality"], StringComparison.OrdinalIgnoreCase))//仅仅处理会计
                            continue;

                        if (typeflag.Equals("I", StringComparison.OrdinalIgnoreCase)) //新增
                        {
                            if (Global.GetCtx().UserMgr.FindByName(userid) != null)
                                continue;
                            CUser User = new CUser();
                            User.Ctx = Global.GetCtx().UserMgr.Ctx;
                            User.Name = userid;
                            User.Pwd = password;
                            User.TName = name;
                            User.B_Company_id = Company.Id;
                            User.Type = 2;//学生
                            Global.GetCtx().UserMgr.AddNew(User);
                            if (!Global.GetCtx().UserMgr.Save(true))
                            {
                            }
                            //加入班级
                            COrg Org = OrgMgr.FindByName(teachingclass);
                            if(Org==null)
                            {
                                Org = new COrg();
                                Org.Ctx = OrgMgr.Ctx;
                                Org.Name = teachingclass;
                                Org.B_Company_id = Company.Id;
                                OrgMgr.AddNew(Org);
                                if (!OrgMgr.Save(true))
                                {
                                }
                            }
                            CUserInOrg UserInOrg=new CUserInOrg();
                            UserInOrg.Ctx = Org.Ctx;
                            UserInOrg.B_Org_id = Org.Id;
                            UserInOrg.B_User_id = User.Id;
                            Org.UserInOrgMgr.AddNew(UserInOrg);
                            if (!Org.UserInOrgMgr.Save(true))
                            {
                            }
                        }
                        else if (typeflag.Equals("U", StringComparison.OrdinalIgnoreCase)) //修改
                        {
                            CUser User = Global.GetCtx().UserMgr.FindByName(userid);
                            if (User != null)
                            {
                                User.TName = name;
                                User.Pwd = password;
                            }
                            else
                            {
                                User = new CUser();
                                User.Ctx = Global.GetCtx().UserMgr.Ctx;
                                User.Name = userid;
                                User.Pwd = password;
                                User.TName = name;
                                User.Pwd = password;
                                User.B_Company_id = Company.Id;
                                User.Type = 2;//学生
                                Global.GetCtx().UserMgr.AddNew(User);
                            }
                            if (!Global.GetCtx().UserMgr.Save(true))
                            {
                            }
                        }
                        else if (typeflag.Equals("D", StringComparison.OrdinalIgnoreCase)) //删除
                        {
                            CUser User = Global.GetCtx().UserMgr.FindByName(userid);
                            if (User != null)
                            {
                                Global.GetCtx().UserMgr.Delete(User);
                                if (!Global.GetCtx().UserMgr.Save(true))
                                {
                                }
                            }
                        }
                    }
                }
                if (SyncTime != null)
                {
                    Global.GetCtx().SyncTimeMgr.Update(SyncTime);
                    if (!Global.GetCtx().SyncTimeMgr.Save(true))
                    {
                    }
                }
            }
        }
    }
}