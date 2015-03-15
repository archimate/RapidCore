// File:    PO.cs
// Author:  甘孝俭
// email:   ganxiaojian@hotmail.com 
// QQ:      154986287
// 协议声明：本软件为开源系统，遵循国际开源组织协议。任何单位或个人可以使用或修改本软件源码，
//          可以用于作为非商业或商业用途，但由于使用本源码所引起的一切后果与作者无关。
//          未经作者许可，禁止任何企业或个人直接出售本源码或者把本软件作为独立的功能进行销售活动，
//          作者将保留追究责任的权利。
// Created: 2011年7月9日 15:41:41
// Purpose: Definition of Class PO

using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Data;
using System.Data.OleDb;
using System.Data.SQLite;

using ErpCoreModel.Base;

namespace ErpCoreModel.Framework
{
    public class PO
    {
        protected List<CBaseObject> m_lstObj=new List<CBaseObject>();
        protected SortedList<Guid, CBaseObject> m_sortObj = new SortedList<Guid, CBaseObject>();
        /// 为了保证在事务中可以回滚，使用新旧值双缓存机制
        public SortedList<string,CValue> m_arrOldVal=new SortedList<string,CValue>();
        public SortedList<string, CValue> m_arrNewVal=new SortedList<string,CValue>();

        protected string tbCode = "";
        /// 通过类名实例化对象
        protected string className = "ErpCoreModel.Framework.PO";
        protected CContext ctx = null;

        CTable table = null;

        Type objType = null; //对象类型，用于其他dll的

        public PO()
        {
        }
        public PO(CContext ctx)
        {
            Ctx = ctx;
        }

        #region 属性
        public string TbCode
        {
            get
            {
                return tbCode;
            }
            set
            {
                this.tbCode = value;
            }
        }

        public string ClassName
        {
            get
            {
                return className;
            }
            set
            {
                this.className = value;
            }
        }

        public Type ObjType
        {
            get
            {
                return objType;
            }
            set
            {
                this.objType = value;
            }
        }

        public CContext Ctx
        {
            get
            {
                return ctx;
            }
            set
            {
                this.ctx = value;
            }
        }

        public CTable Table
        {
            get
            {
                if (null == table)
                {
                    List<CBaseObject> lstTable = Ctx.TableMgr.GetList();
                    foreach (CBaseObject obj in lstTable)
                    {
                        CTable tb = (CTable)obj;
                        if (tb.Code.Equals(TbCode, StringComparison.OrdinalIgnoreCase))
                        {
                            table = tb;
                            break;
                        }
                    }
                }
                if (table == null)
                    Ctx.LastError = "表对象记录不存在！";
                return (CTable)table;
            }
            set { table = value; }
        }
        #endregion
        #region 持久化
        public virtual bool Save()
        {
            return false;
        }
        public virtual bool Save(bool bTrans)
        {
            if (!bTrans)
            {
                return Save();
            }
            Ctx.MainDB.BeginTransaction();
            if (!Save())
            {
                Ctx.MainDB.RollbackTransaction();
                Cancel();
                return false;
            }
            Ctx.MainDB.CommitTransaction();
            Commit();
            return true;
        }
        public virtual void Cancel()
        {
        }
        protected virtual bool AddNewPO()
        {
            try
            {  
                //sqlserver
                string sDeclare ="";
                string sFields ="";
                string sVal ="";
                List<CBaseObject> lstCol = Table.ColumnMgr.GetList();
                List<DbParameter> cmdParms = new List<DbParameter>();

                foreach(PO colPO in lstCol)
                {
                    CColumn col=(CColumn)colPO;


                    DbParameter para = new DbParameter();
                    if (Ctx.MainDB.m_DbType == DatabaseType.MySql)
                        para.ParameterName =string.Format("?{0}", col.Code);
                    else
                        para.ParameterName = col.Code;
                    cmdParms.Add(para);

                    if(!m_arrNewVal.ContainsKey(col.Code.ToLower()))
                    {
                        CValue val=new CValue();
                        m_arrNewVal.Add(col.Code.ToLower(),val);
                        m_arrOldVal.Add(col.Code.ToLower(),val.Clone());
                    }
                    //if (col.Code.ToLower() == "id")
                    //{
                    //    m_arrNewVal[col.Code.ToLower()].GuidVal = Guid.NewGuid();
                    //}

                    string ctype="";
                    switch(col.ColType)
                    {
                        case ColumnType.ref_type:
                        case ColumnType.guid_type:
                            ctype = string.Format("uniqueidentifier");
                            para.Value = m_arrNewVal[col.Code.ToLower()].GuidVal;
                            break;
                        case ColumnType.string_type:
                        case ColumnType.enum_type:
                        case ColumnType.path_type:
                            ctype=string.Format("nvarchar({0})",col.ColLen);
                            para.Value = m_arrNewVal[col.Code.ToLower()].StrVal;
                            break;
                        case ColumnType.text_type:
                            ctype=string.Format("nvarchar(4000)");
                            para.Value = m_arrNewVal[col.Code.ToLower()].StrVal;
                            break;
                        case ColumnType.int_type:
                            ctype=string.Format("int");
                            para.Value = m_arrNewVal[col.Code.ToLower()].IntVal;
                            break;
                        case ColumnType.long_type:
                            ctype=string.Format("bigint");
                            para.Value = m_arrNewVal[col.Code.ToLower()].LongVal;
                            break;
                        case ColumnType.bool_type:
                            ctype=string.Format("bit");
                            para.Value = m_arrNewVal[col.Code.ToLower()].BoolVal;
                            break;
                        case ColumnType.numeric_type:
                            ctype=string.Format("numeric({0},{1})",col.ColLen,col.ColDecimal);
                            para.Value = m_arrNewVal[col.Code.ToLower()].DoubleVal;
                            break;
                        case ColumnType.datetime_type:
                            ctype=string.Format("datetime");
                            para.Value = m_arrNewVal[col.Code.ToLower()].DatetimeVal;
                            break;
                        case ColumnType.object_type:
                            ctype = string.Format("image");
                            //para.OleDbType= OleDbType.Binary;
                            //if(m_arrNewVal[col.Code.ToLower()].ObjectVal!=null)
                            //    para.Size = ((byte[])m_arrNewVal[col.Code.ToLower()].ObjectVal).Length;
                            para.Value = m_arrNewVal[col.Code.ToLower()].ObjectVal;
                            break;
                    }

                    sDeclare +=string.Format("declare @{0} {1}\r\n",col.Code,ctype);
                    if (Ctx.MainDB.m_DbType == DatabaseType.MySql)
                        sFields += string.Format("`{0}`,", col.Code);
                    else
                        sFields+=string.Format("[{0}],",col.Code);
                    if (Ctx.MainDB.m_DbType == DatabaseType.MySql)
                        sVal += string.Format("?{0},", col.Code);
                    else
                        sVal+=string.Format("?,");
                }
                sFields = sFields.TrimEnd(',');
                sVal = sVal.TrimEnd(',');

                string sIns =string.Format( "insert into {0} ({1}) VALUES ({2})",TbCode,sFields,sVal);

                string sSql = /*sDeclare +*/ sIns ;

                if (Table.DataServerMgr.GetList().Count == 0)
                {
                    if (Ctx.MainDB.ExecuteSql(sSql, cmdParms) == -1)
                    {
                        Ctx.LastError = "添加失败！";
                        return false;
                    }
                }
                else
                {
                    List<CDataServer> lstDB = new List<CDataServer>();
                    List<CBaseObject> lstObj = Table.DataServerMgr.GetList();
                    foreach (CBaseObject obj in lstObj)
                    {
                        CDataServer DataServer = (CDataServer)obj;
                        if (!DataServer.IsWrite)
                            continue;
                        lstDB.Add(DataServer);
                    }
                    if (lstDB.Count == 0)
                    {
                        if (Ctx.MainDB.ExecuteSql(sSql, cmdParms) == -1)
                        {
                            Ctx.LastError = "添加失败！";
                            return false;
                        }
                    }
                    else //随机取数据服务器 , 暂时只考虑sqlserver
                    {
                        Random rand = new Random(DateTime.Now.Millisecond);
                        int idx =rand.Next(0, lstDB.Count);
                        CDataServer DataServer = lstDB[idx];
                        string connectionString =string.Format( "Provider=SQLOLEDB.1;Password={0};Persist Security Info=True;User ID={1};Initial Catalog={2};Data Source={3}",
                            DataServer.Pwd,DataServer.UserID,DataServer.DBName,DataServer.Server);
                        DB db = new DB(connectionString);
                        if (db.ExecuteSql(sSql, cmdParms) == -1)
                        {
                            Ctx.LastError = "添加失败！";
                            return false;
                        }
                    }
                }
                                

                //新旧值同步
                //Commit(); 考虑事务，在外部调用
            }
            catch (Exception er)
            {
                Ctx.LastError = er.Message;
                //Rollback(); 考虑事务，在外部调用
                return false;
            }
            return true;
        }

        protected virtual bool UpdatePO()
        {
            try
            {  
                //sqlserver
                string sDeclare ="";
                string sFields ="";
                List<CBaseObject> lstCol = Table.ColumnMgr.GetList();
                List<DbParameter> cmdParms = new List<DbParameter>();

                foreach (CBaseObject colPO in lstCol)
                {
                    CColumn col=(CColumn)colPO;

                    if(col.Code.Equals("id", StringComparison.OrdinalIgnoreCase))
                        continue;

                    DbParameter para = new DbParameter();
                    if (Ctx.MainDB.m_DbType == DatabaseType.MySql)
                        para.ParameterName = string.Format("?{0}", col.Code);
                    else
                        para.ParameterName = col.Code;
                    cmdParms.Add(para);
                    
                    if(!m_arrNewVal.ContainsKey(col.Code.ToLower()))
                    {
                        CValue val=new CValue();
                        m_arrNewVal.Add(col.Code.ToLower(),val);
                        m_arrOldVal.Add(col.Code.ToLower(),val.Clone());
                    }

                    string ctype="";
                    switch(col.ColType)
                    {
                        case ColumnType.ref_type:
                        case ColumnType.guid_type:
                            ctype = string.Format("uniqueidentifier");
                            para.Value = m_arrNewVal[col.Code.ToLower()].GuidVal;
                            break;
                        case ColumnType.string_type:
                        case ColumnType.enum_type:
                        case ColumnType.path_type:
                            ctype=string.Format("nvarchar({0})",col.ColLen);
                            para.Value = m_arrNewVal[col.Code.ToLower()].StrVal;
                            break;
                        case ColumnType.text_type:
                            ctype=string.Format("nvarchar(4000)");
                            para.Value = m_arrNewVal[col.Code.ToLower()].StrVal;
                            break;
                        case ColumnType.int_type:
                            ctype=string.Format("int");
                            para.Value = m_arrNewVal[col.Code.ToLower()].IntVal;
                            break;
                        case ColumnType.long_type:
                            ctype=string.Format("bigint");
                            para.Value = m_arrNewVal[col.Code.ToLower()].LongVal;
                            break;
                        case ColumnType.bool_type:
                            ctype=string.Format("bit");
                            para.Value = m_arrNewVal[col.Code.ToLower()].BoolVal;
                            break;
                        case ColumnType.numeric_type:
                            ctype=string.Format("numeric({0},{1})",col.ColLen,col.ColDecimal);
                            para.Value = m_arrNewVal[col.Code.ToLower()].DoubleVal;
                            break;
                        case ColumnType.datetime_type:
                            ctype=string.Format("datetime");
                            para.Value = m_arrNewVal[col.Code.ToLower()].DatetimeVal;
                            break;
                        case ColumnType.object_type:
                            ctype = string.Format("image");
                            //para.OleDbType = OleDbType.Binary;
                            //if (m_arrNewVal[col.Code.ToLower()].ObjectVal != null)
                            //    para.Size = ((byte[])m_arrNewVal[col.Code.ToLower()].ObjectVal).Length;
                            para.Value = m_arrNewVal[col.Code.ToLower()].ObjectVal;
                            break;
                    }

                    sDeclare += string.Format("declare @{0} {1}\r\n", col.Code, ctype);
                    if (Ctx.MainDB.m_DbType == DatabaseType.MySql)
                        sFields += string.Format("`{0}`=?{0},", col.Code);
                    else
                        sFields+=string.Format("[{0}]=?,",col.Code);
                }
                sFields = sFields.TrimEnd(',');

                string sUpd =string.Format( "update {0} set {1} where id= '{2}'",TbCode,sFields,m_arrOldVal["id"].GuidVal);

                string sSql = /*sDeclare +*/ sUpd ;

                if (Table.DataServerMgr.GetList().Count == 0)
                {
                    Ctx.MainDB.ExecuteSql(sSql, cmdParms);
                }
                else
                {
                    List<CDataServer> lstDB = new List<CDataServer>();
                    List<CBaseObject> lstObj = Table.DataServerMgr.GetList();
                    foreach (CBaseObject obj in lstObj)
                    {
                        CDataServer DataServer = (CDataServer)obj;
                        if (!DataServer.IsWrite)
                            continue;
                        lstDB.Add(DataServer);
                    }
                    if (lstDB.Count == 0)
                    {
                        Ctx.MainDB.ExecuteSql(sSql, cmdParms);
                    }
                    else 
                    {
                        foreach (CDataServer DataServer in lstDB)
                        {
                            string connectionString = string.Format("Provider=SQLOLEDB.1;Password={0};Persist Security Info=True;User ID={1};Initial Catalog={2};Data Source={3}",
                                DataServer.Pwd, DataServer.UserID, DataServer.DBName, DataServer.Server);
                            DB db = new DB(connectionString);
                            db.ExecuteSql(sSql, cmdParms);
                        }
                    }
                }        

                //新旧值同步
                //Commit(); 考虑事务，在外部调用
            }
            catch (Exception er)
            {
                Ctx.LastError = er.Message;
                //Rollback(); 考虑事务，在外部调用
                return false;
            }
            return true;
        }

        protected virtual bool DeletePO()
        {
            try
            {
                ////删除从表
                //List<CBaseObject> lstTable = Ctx.TableMgr.GetList();
                //foreach (CBaseObject obj in lstTable)
                //{
                //    CTable table = (CTable)obj;
                //    List<CBaseObject> lstCol = table.ColumnMgr.GetList();
                //    foreach (CBaseObject obj2 in lstCol)
                //    {
                //        CColumn col2 = (CColumn)obj2;
                //        if (col2.ColType == ColumnType.ref_type
                //            && col2.RefTable == Table.Id)
                //        {
                //            CColumn col = (CColumn)Table.ColumnMgr.Find(col2.RefCol);
                //            List<DbParameter> cmdParms = new List<DbParameter>();
                //            DbParameter para = new DbParameter();
                //            para.ParameterName = col.Code;
                //            cmdParms.Add(para);

                //            string ctype = "";
                //            switch (col.ColType)
                //            {
                //                case ColumnType.ref_type:
                //                case ColumnType.string_type:
                //                    ctype = string.Format("nvarchar({0})", col.ColLen);
                //                    para.Value = m_arrNewVal[col.Code.ToLower()].StrVal;
                //                    break;
                //                case ColumnType.text_type:
                //                    ctype = string.Format("nvarchar(4000)");
                //                    para.Value = m_arrNewVal[col.Code.ToLower()].StrVal;
                //                    break;
                //                case ColumnType.int_type:
                //                    ctype = string.Format("int");
                //                    para.Value = m_arrNewVal[col.Code.ToLower()].IntVal;
                //                    break;
                //                case ColumnType.long_type:
                //                    ctype = string.Format("bigint");
                //                    para.Value = m_arrNewVal[col.Code.ToLower()].LongVal;
                //                    break;
                //                case ColumnType.bool_type:
                //                    ctype = string.Format("bit");
                //                    para.Value = m_arrNewVal[col.Code.ToLower()].BoolVal;
                //                    break;
                //                case ColumnType.numeric_type:
                //                    ctype = string.Format("numeric({0},{1})", col.ColLen, col.ColDecimal);
                //                    para.Value = m_arrNewVal[col.Code.ToLower()].DoubleVal;
                //                    break;
                //                case ColumnType.datetime_type:
                //                    ctype = string.Format("datetime");
                //                    para.Value = m_arrNewVal[col.Code.ToLower()].DatetimeVal;
                //                    break;
                //                case ColumnType.object_type:
                //                    para.OleDbType = OleDbType.VarBinary;
                //                    //para.Size=((byte[])m_arrNewVal[col.Code.ToLower()].ObjectVal).Length;
                //                    para.Value = m_arrNewVal[col.Code.ToLower()].ObjectVal;
                //                    break;
                //            }

                //            string sDeclare = string.Format("declare @{0} {1}\r\n", col.Code, ctype);
                //            string sDel = string.Format("delete from {0} where {1}=?",table.Code,col.Code);
                //            Ctx.MainDB.ExecuteSql(sDeclare+sDel, cmdParms);
                //        }
                //    }
                //}
                string sSql = "";
                if (this.TbCode.Equals("FW_Table", StringComparison.OrdinalIgnoreCase)
                    || this.TbCode.Equals("FW_Column", StringComparison.OrdinalIgnoreCase)
                    || (!Ctx.IsDeletedFlag))
                {
                    sSql = string.Format("delete from {0} where id='{1}'", TbCode, m_arrOldVal["id"].GuidVal);
                }
                else
                {
                    sSql = string.Format("update {0} set IsDeleted=1 where id='{1}'", TbCode, m_arrOldVal["id"].GuidVal);
                }
                
                if (Table.DataServerMgr.GetList().Count == 0)
                {
                    Ctx.MainDB.ExecuteSql(sSql);
                }
                else
                {
                    List<CDataServer> lstDB = new List<CDataServer>();
                    List<CBaseObject> lstObj = Table.DataServerMgr.GetList();
                    foreach (CBaseObject obj in lstObj)
                    {
                        CDataServer DataServer = (CDataServer)obj;
                        if (!DataServer.IsWrite)
                            continue;
                        lstDB.Add(DataServer);
                    }
                    if (lstDB.Count == 0)
                    {
                        Ctx.MainDB.ExecuteSql(sSql);
                    }
                    else 
                    {

                        foreach (CDataServer DataServer in lstDB)
                        {
                            string connectionString = string.Format("Provider=SQLOLEDB.1;Password={0};Persist Security Info=True;User ID={1};Initial Catalog={2};Data Source={3}",
                                DataServer.Pwd, DataServer.UserID, DataServer.DBName, DataServer.Server);
                            DB db = new DB(connectionString);
                            db.ExecuteSql(sSql);
                        }
                    }
                }
            }
            catch (Exception er)
            {
                Ctx.LastError = er.Message;
                return false;
            }
            return true;
        }

        public virtual void Commit()
        {
            foreach (KeyValuePair<string, CValue> de in m_arrNewVal)
            {
                CValue val = de.Value.Clone();
                if (m_arrOldVal.ContainsKey(de.Key))
                    m_arrOldVal[de.Key] = val;
                else
                    m_arrOldVal.Add(de.Key, val);
            }
        }
        public virtual void Rollback()
        {
            foreach (KeyValuePair<string, CValue> de in m_arrOldVal)
            {
                CValue val = de.Value.Clone();
                if (m_arrNewVal.ContainsKey(de.Key))
                    m_arrNewVal[de.Key] = val;
                else
                    m_arrNewVal.Add(de.Key, val);
            }
        }
        #endregion

        #region 获取对象列表
        protected List<CBaseObject> GetListPO(string sWhere, List<DbParameter> cmdParas,string sOrderby)
        {
            return GetListPO(sWhere, cmdParas, -1, sOrderby);
        }
        protected virtual List<CBaseObject> GetListPO(string sWhere, List<DbParameter> cmdParas, int nTop, string sOrderby)
        {
            //try
            //{
                m_lstObj.Clear();
                m_sortObj.Clear();

                string sTopN = (nTop == -1) ? "" : string.Format(" top {0}", nTop);
                string sSql = string.Format("select {0} * from {1} where (IsDeleted=0 or IsDeleted Is Null) ", sTopN, TbCode);
                if (sWhere != "")
                    sSql += " and " + sWhere;
                if (sOrderby != "")
                    sSql += " order by " + sOrderby;
                else
                    sSql += " order by Created";
                //if (TbCode.Equals("FW_Column", StringComparison.OrdinalIgnoreCase))
                //{
                //    sSql += " order by Idx";
                //}

                //FW_Table FW_Column FW_DataServer需要特殊处理
                if (TbCode.Equals("FW_Table", StringComparison.OrdinalIgnoreCase))
                {
                    DataSet ds = Ctx.MainDB.Query(sSql, cmdParas);
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        CTable tb = new CTable();
                        tb.Ctx = Ctx;
                        tb.Id =ConvertToGuid(r["id"]);
                        tb.Name = r["Name"].ToString();
                        tb.Code = r["Code"].ToString();
                        tb.IsSystem =Convert.ToBoolean( r["IsSystem"]);
                        tb.IsFinish =Convert.ToBoolean( r["IsFinish"]);
                        tb.Created = (r["Created"] != DBNull.Value) ? DateTime.Parse(r["Created"].ToString()) : DateTime.Now;
                        tb.Updated = (r["Updated"] != DBNull.Value) ? DateTime.Parse(r["Updated"].ToString()) : DateTime.Now;
                        tb.Creator = (r["Creator"] != DBNull.Value)?ConvertToGuid(r["Creator"]):Guid.Empty;
                        tb.Updator = (r["Updator"] != DBNull.Value) ?ConvertToGuid(r["Updator"]) : Guid.Empty;
                       
                        tb.Commit();
                        m_lstObj.Add(tb);
                        m_sortObj.Add(tb.Id, tb);
                    }
                }
                else if (TbCode.Equals("FW_Column", StringComparison.OrdinalIgnoreCase))
                {
                    DataSet ds = Ctx.MainDB.Query(sSql, cmdParas);
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        CColumn col = new CColumn();
                        col.Ctx = Ctx;
                        col.Id = ConvertToGuid(r["id"]);
                        col.FW_Table_id = ConvertToGuid(r["FW_Table_id"]);
                        col.Name = r["Name"].ToString();
                        col.Code = r["Code"].ToString();
                        col.IsSystem = (r["IsSystem"] != DBNull.Value) ?Convert.ToBoolean(r["IsSystem"]):false;
                        col.IsUnique = (r["IsUnique"] != DBNull.Value) ?Convert.ToBoolean(r["IsUnique"]):false;
                        col.ColType = (r["ColType"] != DBNull.Value) ? (ColumnType)Convert.ToInt32(r["ColType"]) : (ColumnType)0;
                        col.ColLen = (r["ColLen"] != DBNull.Value) ?Convert.ToInt32(r["ColLen"]):0;
                        col.ColDecimal =(r["ColDecimal"] != DBNull.Value) ? Convert.ToInt32(r["ColDecimal"]):0;
                        col.RefTable = (r["RefTable"] != DBNull.Value) ? ConvertToGuid(r["RefTable"]) : Guid.Empty;
                        col.RefCol = (r["RefCol"] != DBNull.Value) ? ConvertToGuid(r["RefCol"]) : Guid.Empty;
                        col.RefShowCol = (r["RefShowCol"] != DBNull.Value) ? ConvertToGuid(r["RefShowCol"]) : Guid.Empty;
                        col.Formula = (r["Formula"] != DBNull.Value) ? r["Formula"].ToString() : "";
                        col.DefaultValue = (r["DefaultValue"] != DBNull.Value) ? r["DefaultValue"].ToString() : "";
                        col.AllowNull =(r["AllowNull"] != DBNull.Value) ?Convert.ToBoolean( r["AllowNull"]):true;
                        col.UIControl = (r["UIControl"] != DBNull.Value) ? r["UIControl"].ToString() : "";
                        col.WebUIControl = (r["WebUIControl"] != DBNull.Value) ? r["WebUIControl"].ToString() : "";
                        col.IsVisible = (r["IsVisible"] != DBNull.Value) ? Convert.ToBoolean(r["IsVisible"]) : true;
                        col.Idx = (r["Idx"] != DBNull.Value) ? Convert.ToInt32(r["Idx"]) : 0;
                        col.Created = (r["Created"] != DBNull.Value) ? DateTime.Parse(r["Created"].ToString()) : DateTime.Now;
                        col.Updated = (r["Updated"] != DBNull.Value) ? DateTime.Parse(r["Updated"].ToString()) : DateTime.Now;
                        col.Commit();
                        m_lstObj.Add(col);
                        m_sortObj.Add(col.Id, col);
                    }
                }
                else if (TbCode.Equals("FW_DataServer", StringComparison.OrdinalIgnoreCase))
                {
                    DataSet ds = Ctx.MainDB.Query(sSql, cmdParas);
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        CDataServer DataServer = new CDataServer();
                        DataServer.Ctx = Ctx;
                        DataServer.Id = ConvertToGuid(r["id"]);
                        DataServer.FW_Table_id = ConvertToGuid(r["FW_Table_id"]);
                        DataServer.Server = (r["Server"] != DBNull.Value) ? r["Server"].ToString() : "";
                        DataServer.DBName = (r["DBName"] != DBNull.Value) ? r["DBName"].ToString() : "";
                        DataServer.UserID = (r["UserID"] != DBNull.Value) ? r["UserID"].ToString() : "";
                        DataServer.Pwd = (r["Pwd"] != DBNull.Value) ? r["Pwd"].ToString() : "";
                        DataServer.IsWrite = (r["IsWrite"] != DBNull.Value) ? Convert.ToBoolean(r["IsWrite"]) : true;
                        DataServer.Commit();
                        m_lstObj.Add(DataServer);
                        m_sortObj.Add(DataServer.Id, DataServer);
                    }
                }
                else
                {
                    if (Table == null)
                        return null;

                    List<CBaseObject> lstDBServer = Table.DataServerMgr.GetList();
                    List<DB> lstDB = new List<DB>();
                    if (lstDBServer.Count == 0)
                    {
                        DB db = new DB(Ctx.ConnectionString);
                        lstDB.Add(db);
                    }
                    else
                    {
                        foreach (CBaseObject objDBServer in lstDBServer)
                        {
                            CDataServer DataServer = (CDataServer)objDBServer;
                            string connStr = string.Format("Provider=SQLOLEDB.1;Password={0};Persist Security Info=True;User ID={1};Initial Catalog={2};Data Source={3}",
                                DataServer.Pwd, DataServer.UserID, DataServer.DBName, DataServer.Server);
                            DB db = new DB(connStr);
                            lstDB.Add(db);
                        }
                    }
                    foreach (DB db in lstDB)
                    {
                        DataSet ds = db.Query(sSql, cmdParas);
                        if (ds == null)
                            continue;

                        List<CBaseObject> lstCols = Table.ColumnMgr.GetList();

                        foreach (DataRow r in ds.Tables[0].Rows)
                        {
                            if (ObjType == null)
                            {
                                //Assembly assembly = Assembly.LoadFrom(Assembly.GetExecutingAssembly().Location);
                                ObjType = Type.GetType(ClassName);
                            }
                            object obj = Activator.CreateInstance(ObjType);
                            CBaseObject objPO = (CBaseObject)obj;
                            objPO.Ctx = Ctx;
                            objPO.TbCode = TbCode;
                            objPO.m_ObjectMgr = (CBaseObjectMgr)this;

                            foreach (PO colPO in lstCols)
                            {
                                CColumn col = (CColumn)colPO;
                                CValue val = new CValue();
                                switch (col.ColType)
                                {
                                    case ColumnType.ref_type:
                                    case ColumnType.guid_type:
                                        val.GuidVal = (r[col.Code] != DBNull.Value) ? ConvertToGuid(r[col.Code]) : Guid.Empty;
                                        break;
                                    case ColumnType.string_type:
                                    case ColumnType.enum_type:
                                    case ColumnType.text_type:
                                    case ColumnType.path_type:
                                        val.StrVal = r[col.Code].ToString();
                                        break;
                                    case ColumnType.int_type:
                                        val.IntVal = (r[col.Code] != DBNull.Value) ? Convert.ToInt32(r[col.Code]) : 0;
                                        break;
                                    case ColumnType.long_type:
                                        val.LongVal = (r[col.Code] != DBNull.Value) ? Convert.ToInt64(r[col.Code]) : 0;
                                        break;
                                    case ColumnType.bool_type:
                                        val.BoolVal = (r[col.Code] != DBNull.Value) ? Convert.ToBoolean(r[col.Code]) : false;
                                        break;
                                    case ColumnType.datetime_type:
                                        val.DatetimeVal = (r[col.Code] != DBNull.Value) ? Convert.ToDateTime(r[col.Code]) : DateTime.Now;
                                        break;
                                    case ColumnType.numeric_type:
                                        val.DoubleVal = (r[col.Code] != DBNull.Value) ? Convert.ToDouble(r[col.Code]) : 0;
                                        break;
                                    case ColumnType.object_type:
                                        val.ObjectVal = (r[col.Code] == DBNull.Value) ? null : (byte[])r[col.Code];
                                        break;
                                }
                                if (objPO.m_arrOldVal.ContainsKey(col.Code.ToLower()))
                                    objPO.m_arrOldVal[col.Code.ToLower()] = val;
                                else
                                    objPO.m_arrOldVal.Add(col.Code.ToLower(), val);
                                if (objPO.m_arrNewVal.ContainsKey(col.Code.ToLower()))
                                    objPO.m_arrNewVal[col.Code.ToLower()] = val.Clone();
                                else
                                    objPO.m_arrNewVal.Add(col.Code.ToLower(), val.Clone());
                            }
                            objPO.Commit();

                            m_lstObj.Add(objPO);
                            m_sortObj.Add(objPO.Id, objPO);

                            if (nTop > 0 && m_lstObj.Count >= nTop)
                                return m_lstObj;
                        }
                    }
                }
            //}
            //catch (Exception er)
            //{
            //    m_lstObj.Clear();
             //   m_sortObj.Clear();
            //    Ctx.LastError = er.Message;
            //    return null;
            //}
            return m_lstObj;
        }
        //弱类型处理海量数据，直接返回记录集
        public List<DataSet> GetDataSet(string sWhere, List<DbParameter> cmdParas, int nTop, string sOrderby)
        {
            string sTopN = (nTop == -1) ? "" : string.Format(" top {0}", nTop);
            string sSql = string.Format("select {0} * from {1}  where (IsDeleted=0 or IsDeleted Is Null) ", sTopN, TbCode);
            if (sWhere != "")
                sSql += " and " + sWhere;
            if (sOrderby != "")
                sSql += " order by " + sOrderby;
            else
                sSql += " order by Created";

            List<DataSet> lstDS = new List<DataSet>();

            List<CBaseObject> lstDBServer = Table.DataServerMgr.GetList();
            if (lstDBServer.Count == 0)
            {
                DataSet ds = Ctx.MainDB.Query(sSql, cmdParas);
                lstDS.Add(ds);
            }
            else
            {
                foreach (CBaseObject objDBServer in lstDBServer)
                {
                    CDataServer DataServer = (CDataServer)objDBServer;
                    string connStr = string.Format("Provider=SQLOLEDB.1;Password={0};Persist Security Info=True;User ID={1};Initial Catalog={2};Data Source={3}",
                        DataServer.Pwd, DataServer.UserID, DataServer.DBName, DataServer.Server);
                    DB db = new DB(connStr);
                    DataSet ds = db.Query(sSql, cmdParas);

                    lstDS.Add(ds);
                }
            }

            return lstDS;
        }
        #endregion
        #region 获取/设置字段值
        public object GetColValue(CColumn col)
        {
            if (!m_arrNewVal.ContainsKey(col.Code.ToLower()))
            {
                if (col.ColType == ColumnType.guid_type
                    || col.ColType == ColumnType.ref_type)
                    return Guid.Empty;
                else
                    return null;
            }
            switch (col.ColType)
            {
                case ColumnType.guid_type:
                    return m_arrNewVal[col.Code.ToLower()].GuidVal;
                    break;
                case ColumnType.string_type:
                case ColumnType.text_type:
                case ColumnType.enum_type:
                case ColumnType.path_type:
                    return m_arrNewVal[col.Code.ToLower()].StrVal ;
                    break;
                case ColumnType.int_type:
                    return m_arrNewVal[col.Code.ToLower()].IntVal;
                    break;
                case ColumnType.long_type:
                    return m_arrNewVal[col.Code.ToLower()].LongVal;
                    break;
                case ColumnType.bool_type:
                    return m_arrNewVal[col.Code.ToLower()].BoolVal;
                    break;
                case ColumnType.datetime_type:
                    return m_arrNewVal[col.Code.ToLower()].DatetimeVal;
                    break;
                case ColumnType.numeric_type:
                    return m_arrNewVal[col.Code.ToLower()].DoubleVal;
                    break;
                case ColumnType.object_type:
                    return m_arrNewVal[col.Code.ToLower()].ObjectVal;
                    break;
                case ColumnType.ref_type:
                    return m_arrNewVal[col.Code.ToLower()].GuidVal;
                    break;
            }
            return null;
        }

        public void SetColValue(CColumn col,object objVal)
        {
            if (!m_arrNewVal.ContainsKey(col.Code.ToLower()))
            {
                CValue val = new CValue();
                m_arrNewVal.Add(col.Code.ToLower(), val);
            }
            switch (col.ColType)
            {
                case ColumnType.guid_type:
                    if (objVal == null)
                        objVal = Guid.Empty;
                    if (objVal.GetType() == Type.GetType("System.String"))
                    {
                        Guid guid = Guid.Empty;
                        if(objVal.ToString()!="")
                            guid = new Guid(objVal.ToString());
                        m_arrNewVal[col.Code.ToLower()].GuidVal = guid;
                    }
                    else
                        m_arrNewVal[col.Code.ToLower()].GuidVal = (Guid)objVal;
                    break;
                case ColumnType.string_type:
                case ColumnType.text_type:
                case ColumnType.enum_type:
                case ColumnType.path_type:
                    if (objVal == null)
                        objVal = "";
                    m_arrNewVal[col.Code.ToLower()].StrVal = objVal.ToString();
                    break;
                case ColumnType.int_type:
                    if (objVal == null)
                        objVal = 0;
                    m_arrNewVal[col.Code.ToLower()].IntVal=Convert.ToInt32(objVal);
                    break;
                case ColumnType.long_type:
                    if (objVal == null)
                        objVal = 0;
                    m_arrNewVal[col.Code.ToLower()].LongVal = Convert.ToInt64(objVal);
                    break;
                case ColumnType.bool_type:
                    if (objVal == null)
                        objVal = false;
                    m_arrNewVal[col.Code.ToLower()].BoolVal =Convert.ToBoolean (objVal);
                    break;
                case ColumnType.datetime_type:
                    if (objVal == null)
                        objVal = DateTime.Now;
                    m_arrNewVal[col.Code.ToLower()].DatetimeVal = Convert.ToDateTime(objVal);
                    break;
                case ColumnType.numeric_type:
                    if (objVal == null)
                        objVal = 0;
                    m_arrNewVal[col.Code.ToLower()].DoubleVal = Convert.ToDouble(objVal);
                    break;
                case ColumnType.object_type:
                    m_arrNewVal[col.Code.ToLower()].ObjectVal = objVal;
                    break;
                case ColumnType.ref_type:
                    if (objVal == null)
                        objVal = Guid.Empty;
                    if (objVal.GetType() == Type.GetType("System.String"))
                    {
                        Guid guid = Guid.Empty;
                        if (objVal.ToString() != "")
                            guid = new Guid(objVal.ToString());
                        m_arrNewVal[col.Code.ToLower()].GuidVal = guid;
                    }
                    else
                        m_arrNewVal[col.Code.ToLower()].GuidVal = (Guid)objVal;
                    break;
            }
        }
        //为了提高获取数据效率，不采用object封箱值
        public CValue GetColValue2(string sColCode)
        {
            return m_arrNewVal[sColCode];
        }
        public CValue GetColValue2(int idx)
        {
            return m_arrNewVal.Values[idx];
        }
        public int GetColIdx(string sColCode)
        {
            return m_arrNewVal.IndexOfKey(sColCode);
        }

        public void SetColValue2(string sColCode, CValue Val)
        {
            if (m_arrNewVal.ContainsKey(sColCode))
            {
                m_arrNewVal.Remove(sColCode);
            }
            m_arrNewVal.Add(sColCode, Val);
        }
        #endregion

        #region 物理表操作
        //删除数据表
        static public void DeleteDataTable(CTable table)
        {
            string sDrop = string.Format(@"drop table [{0}]",
                        table.Code);
            try {
                if (table.DataServerMgr.GetList().Count==0)
                {
                    table.Ctx.MainDB.ExecuteSql(sDrop);
                }
                else
                {
                    List<CBaseObject> lstObj = table.DataServerMgr.GetList();
                    foreach (CBaseObject obj in lstObj)
                    {
                        CDataServer DataServer = (CDataServer)obj;
                        string connStr = string.Format("Provider=SQLOLEDB.1;Password={0};Persist Security Info=True;User ID={1};Initial Catalog={2};Data Source={3}",
                            DataServer.Pwd, DataServer.UserID, DataServer.DBName, DataServer.Server);
                        DB db = new DB(connStr);
                        db.ExecuteSql(sDrop);
                    }
                }
            }
            catch { }
        }

        //创建数据表
        static public bool CreateDataTable(CTable table)
        {
            //如果表不存在，则创建表
            //如果存在，添加新字段，修改同名的字段，删除多余字段
            if (table == null)
            {
                table.Ctx.LastError = "表对象不存在！";
                return false;
            }
            
            List<CBaseObject> lstObj = table.DataServerMgr.GetList();
            List<DB> lstDb = new List<DB>();
            if(lstObj.Count==0)
            {
                DB db = new DB(table.Ctx.ConnectionString);
                lstDb.Add(db);
            }
            else
            {
                foreach (CBaseObject obj in lstObj)
                {
                    CDataServer DataServer = (CDataServer)obj;
                    string connStr = string.Format("Provider=SQLOLEDB.1;Password={0};Persist Security Info=True;User ID={1};Initial Catalog={2};Data Source={3}",
                        DataServer.Pwd, DataServer.UserID, DataServer.DBName, DataServer.Server);
                    DB db = new DB(connStr);
                    lstDb.Add(db);
                }
            }

            foreach (DB db in lstDb)
            {

                //sqlserver
                string sSql = string.Format("select * from {0} where id='{1}'", table.Code,Guid.Empty);
                DataSet ds = null;
                try
                {
                    ds = db.Query(sSql);
                }
                catch
                {
                    ds = null;
                }

                //try
                //{
                if (ds == null)  //数据表不存在
                {
                    List<CBaseObject> lstCols = table.ColumnMgr.GetList();
                    string sFields = "";
                    foreach (CBaseObject colPO in lstCols)
                    {
                        CColumn col = (CColumn)colPO;
                        //因为是新建表，忽略删除字段
                        if (col.m_CmdType == CmdType.Delete)
                            continue;

                        if (col.Code.Equals("id", StringComparison.OrdinalIgnoreCase))
                        {
                            if (db.m_DbType == DatabaseType.Sqlite)
                                sFields += "[id] [guid] PRIMARY KEY ,";
                            else if (db.m_DbType == DatabaseType.MySql)
                                sFields += "`id` varchar(50) PRIMARY KEY ,";
                            else
                                sFields += "[id] [uniqueidentifier] PRIMARY KEY ,";
                            continue;
                        }
                        else if (col.Code.Equals("Created", StringComparison.OrdinalIgnoreCase))
                        {
                            if (db.m_DbType == DatabaseType.Sqlite)
                                sFields += " [Created] [datetime] DEFAULT (datetime(CURRENT_TIMESTAMP,'localtime')) ,";
                            else if (db.m_DbType == DatabaseType.MySql)
                                sFields += " `Created` datetime ,";
                            else
                                sFields += "[Created] [datetime] DEFAULT getdate() ,";
                            continue;
                        }
                        else if (col.Code.Equals("Creator", StringComparison.OrdinalIgnoreCase))
                        {
                            if (db.m_DbType == DatabaseType.Sqlite)
                                sFields += "[Creator] [guid] NULL ,";
                            else if (db.m_DbType == DatabaseType.MySql)
                                sFields += " `Creator` varchar(50) ,";
                            else
                                sFields += "[Creator] [uniqueidentifier] NULL ,";
                            continue;
                        }
                        else if (col.Code.Equals("Updated", StringComparison.OrdinalIgnoreCase))
                        {
                            if (db.m_DbType == DatabaseType.Sqlite)
                                sFields += " [Updated] [datetime] DEFAULT (datetime(CURRENT_TIMESTAMP,'localtime')) ,";
                            else if (db.m_DbType == DatabaseType.MySql)
                                sFields += " `Updated` datetime ,";
                            else
                                sFields += "[Updated] [datetime] DEFAULT getdate() ,";
                            continue;
                        }
                        else if (col.Code.Equals("Updator", StringComparison.OrdinalIgnoreCase))
                        {
                            if (db.m_DbType == DatabaseType.Sqlite)
                                sFields += "[Updator] [guid] NULL ,";
                            else if (db.m_DbType == DatabaseType.MySql)
                                sFields += " `Updator` varchar(50) ,";
                            else
                                sFields += "[Updator] [uniqueidentifier] NULL ,";
                            continue;
                        }
                        string sDefault = (col.DefaultValue == "") ? "" : string.Format(" DEFAULT {0} ", col.DefaultValue);
                        switch (col.ColType)
                        {
                            case ColumnType.guid_type:
                            case ColumnType.ref_type:
                                if (db.m_DbType == DatabaseType.Sqlite)
                                    sFields += string.Format("[{0}] [guid]  {1} NULL ,", col.Code, sDefault);
                                else if (db.m_DbType == DatabaseType.MySql)
                                    sFields += string.Format(" `{0}` varchar(50) {1} NULL ,", col.Code, sDefault);
                                else
                                    sFields += string.Format("[{0}] [uniqueidentifier]  {1} NULL ,", col.Code, sDefault);
                                break;
                            case ColumnType.string_type:
                            case ColumnType.enum_type:
                            case ColumnType.path_type:
                                if (db.m_DbType == DatabaseType.MySql)
                                    sFields += string.Format("`{0}` varchar({1}) {2} NULL ", col.Code, col.ColLen, sDefault);
                                else
                                    sFields += string.Format("[{0}] [nvarchar]({1}) {2} NULL ", col.Code, col.ColLen, sDefault);

                                if (db.m_DbType == DatabaseType.Sqlite)
                                    sFields += " COLLATE NOCASE ,";
                                else
                                    sFields += " ,";
                                break;
                            case ColumnType.text_type:
                                if (db.m_DbType == DatabaseType.MySql)
                                    sFields += string.Format("`{0}` text  {1} NULL ,", col.Code, sDefault);
                                else
                                    sFields += string.Format("[{0}] [text]  {1} NULL ,", col.Code, sDefault);
                                break;
                            case ColumnType.int_type:
                                if (db.m_DbType == DatabaseType.MySql)
                                    sFields += string.Format("`{0}` int  {1} NULL ,", col.Code, sDefault);
                                else
                                    sFields += string.Format("[{0}] [int]  {1} NULL ,", col.Code, sDefault);
                                break;
                            case ColumnType.long_type:
                                if (db.m_DbType == DatabaseType.MySql)
                                    sFields += string.Format("`{0}` bigint  {1} NULL ,", col.Code, sDefault);
                                else
                                    sFields += string.Format("[{0}] [bigint]  {1} NULL ,", col.Code, sDefault);
                                break;
                            case ColumnType.bool_type:
                                if (db.m_DbType == DatabaseType.MySql)
                                    sFields += string.Format("`{0}` bit  {1} NULL ,", col.Code, sDefault);
                                else
                                    sFields += string.Format("[{0}] [bit]  {1} NULL ,", col.Code, sDefault);
                                break;
                            case ColumnType.datetime_type:
                                if (db.m_DbType == DatabaseType.MySql)
                                    sFields += string.Format("`{0}` datetime  {1} NULL ,", col.Code, sDefault);
                                else
                                    sFields += string.Format("[{0}] [datetime]  {1} NULL ,", col.Code, sDefault);
                                break;
                            case ColumnType.numeric_type:
                                if (db.m_DbType == DatabaseType.MySql)
                                    sFields += string.Format("`{0}` numeric({1},{2})  {3} NULL ,", col.Code, col.ColLen, col.ColDecimal, sDefault);
                                else
                                    sFields += string.Format("[{0}] [numeric]({1},{2})  {3} NULL ,", col.Code, col.ColLen, col.ColDecimal, sDefault);
                                break;
                            case ColumnType.object_type:
                                if (db.m_DbType == DatabaseType.MySql)
                                    sFields += string.Format("`{0}` binary  {1} NULL ,", col.Code, sDefault);
                                else
                                    sFields += string.Format("[{0}] [image]  {1} NULL ,", col.Code, sDefault);
                                break;
                        }
                    }
                    if (db.m_DbType == DatabaseType.MySql)
                        sFields += string.Format("`IsDeleted` bit  DEFAULT 0 ");
                    else
                        sFields += string.Format("[IsDeleted] [bit]  DEFAULT 0 ");
                    //sFields = sFields.TrimEnd(',');

                    //创建表
                    string sDrop = "";
                    if (db.m_DbType == DatabaseType.MySql)
                        sDrop = string.Format(@"drop table `{0}`", table.Code);
                    else
                        sDrop = string.Format(@"drop table [{0}]", table.Code);
                    db.ExecuteSql(sDrop);
                    string sCreate = "";
                    if (db.m_DbType == DatabaseType.MySql)
                        sCreate = string.Format(@"CREATE TABLE `{0}` (
	{1}
)",
                   table.Code,
                   sFields);
                    else
                        sCreate = string.Format(@"CREATE TABLE [{0}] (
	{1}
)",
                   table.Code,
                   sFields);

                    db.ExecuteSql(sCreate);

                    //创建约束
                    //                string sDefault = "";
                    //                foreach (PO colPO in lstCols)
                    //                {
                    //                    CColumn col = (CColumn)colPO;
                    //                    if (col.DefaultValue != "")
                    //                    {
                    //                        sDefault += string.Format("CONSTRAINT [DF__{0}__{1}] DEFAULT ({2}) FOR [{1}],",TbCode,col.Code,col.DefaultValue);
                    //                    }
                    //                }
                    //                sCreate = string.Format(@"ALTER TABLE [{0}] ADD 
                    //	{1}
                    //	CONSTRAINT [PK_{0}] PRIMARY KEY  CLUSTERED 
                    //	(
                    //		[id]
                    //	)  ON [PRIMARY] ",                     
                    //               TbCode,
                    //               sDefault);

                    //                db.ExecuteSql(sCreate);
                }
                else//数据表已经存在
                {
                    string sUpdate = "";
                    if (db.m_DbType == DatabaseType.MySql)
                        sUpdate = string.Format("ALTER TABLE `{0}` ", table.Code);
                    else
                        sUpdate = string.Format("ALTER TABLE [{0}] ", table.Code);

                    List<CBaseObject> lstCols = table.ColumnMgr.GetList();
                    //删除
                    foreach (CBaseObject colPO in lstCols)
                    {
                        CColumn col = (CColumn)colPO;
                        if (col.m_CmdType != CmdType.Delete)
                            continue;
                        DelColumn(db, table, col);
                    }
                    //添加
                    foreach (CBaseObject colPO in lstCols)
                    {
                        CColumn col = (CColumn)colPO;
                        if (col.m_CmdType != CmdType.AddNew)
                            continue;
                        AddColumn(db, table, col);
                    }
                    //修改
                    foreach (CBaseObject colPO in lstCols)
                    {
                        CColumn col = (CColumn)colPO;
                        if (col.m_CmdType != CmdType.Update)
                            continue;
                        UpdateColumn(db, table, col);
                    }


                    
                }

                //}
                //catch (Exception er)
                //{
                //    Ctx.LastError = er.Message;
                //    return false;
                //}
            }
            return true;
        }
        //删除字段
        static void DelColumn(DB db, CTable table, CColumn col)
        {
            string sUpdate = "";
            if (db.m_DbType == DatabaseType.MySql)
                sUpdate = string.Format("ALTER TABLE `{0}` ", table.Code);
            else
                sUpdate = string.Format("ALTER TABLE [{0}] ", table.Code);
            //删除默认值约束
            DelConstraint(db, table.Code, col.Code);
            string sDel = "";
            if (db.m_DbType == DatabaseType.MySql)
                sDel = string.Format(" DROP  `{0}` ", col.Code);
            else
                sDel = string.Format(" DROP COLUMN [{0}] ", col.Code);
            db.ExecuteSql(sUpdate + sDel);
        }
        //添加字段
        static void AddColumn(DB db, CTable table, CColumn col)
        {
            string sUpdate ="";
            if (db.m_DbType == DatabaseType.MySql)
                sUpdate = string.Format("ALTER TABLE `{0}` ", table.Code);
            else
                sUpdate = string.Format("ALTER TABLE [{0}] ", table.Code);
            string sAdd = "";
            if (col.Code.Equals("id", StringComparison.OrdinalIgnoreCase))
            {
                if (db.m_DbType == DatabaseType.Sqlite)
                    sAdd = " ADD [id] [guid]  PRIMARY KEY ";
                else if (db.m_DbType == DatabaseType.MySql)
                    sAdd = " ADD `id` varchar(50)  PRIMARY KEY ";
                else
                    sAdd = " ADD [id] [uniqueidentifier]  PRIMARY KEY ";
                db.ExecuteSql(sUpdate + sAdd);
                return;
            }
            else if (col.Code.Equals("Created", StringComparison.OrdinalIgnoreCase))
            {
                if (db.m_DbType == DatabaseType.Sqlite)
                    sAdd = " ADD [Created] [datetime] DEFAULT (datetime(CURRENT_TIMESTAMP,'localtime')) ";
                else if (db.m_DbType == DatabaseType.MySql)
                    sAdd = " ADD `Created` datetime  ";
                else
                    sAdd = " ADD [Created] [datetime] DEFAULT getdate() ";
                db.ExecuteSql(sUpdate + sAdd);
                return;
            }
            else if (col.Code.Equals("Creator", StringComparison.OrdinalIgnoreCase))
            {
                if (db.m_DbType == DatabaseType.Sqlite)
                    sAdd = " ADD [Creator] [guid] NULL ";
                else if (db.m_DbType == DatabaseType.MySql)
                    sAdd = " ADD `Creator` varchar(50)  ";
                else
                    sAdd = " ADD [Creator] [uniqueidentifier] NULL ";
                db.ExecuteSql(sUpdate + sAdd);
                return;
            }
            else if (col.Code.Equals("Updated", StringComparison.OrdinalIgnoreCase))
            {
                if (db.m_DbType == DatabaseType.Sqlite)
                    sAdd = " ADD [Updated] [datetime] DEFAULT (datetime(CURRENT_TIMESTAMP,'localtime')) ";
                else if (db.m_DbType == DatabaseType.MySql)
                    sAdd = " ADD `Updated` datetime  ";
                else
                    sAdd = " ADD [Updated] [datetime] DEFAULT getdate() ";
                db.ExecuteSql(sUpdate + sAdd);
                return;
            }
            else if (col.Code.Equals("Updator", StringComparison.OrdinalIgnoreCase))
            {
                if (db.m_DbType == DatabaseType.Sqlite)
                    sAdd = " ADD [Updator] [guid] NULL ";
                else if (db.m_DbType == DatabaseType.MySql)
                    sAdd = " ADD `Updator` varchar(50)  ";
                else
                    sAdd = " ADD [Updator] [uniqueidentifier] NULL ";
                db.ExecuteSql(sUpdate + sAdd);
                return;
            }

            string sDefault = (col.DefaultValue == "") ? "" : string.Format(" DEFAULT {0} ", col.DefaultValue);
            switch (col.ColType)
            {
                case ColumnType.guid_type:
                case ColumnType.ref_type:
                    if (db.m_DbType == DatabaseType.Sqlite)
                        sAdd = string.Format(" ADD [{0}] [guid] {1} NULL ", col.Code, sDefault);
                    else if (db.m_DbType == DatabaseType.MySql)
                        sAdd = string.Format(" ADD `{0}` varchar(50) {1} NULL ", col.Code, sDefault);
                    else
                        sAdd = string.Format(" ADD [{0}] [uniqueidentifier] {1} NULL ", col.Code, sDefault);
                    db.ExecuteSql(sUpdate + sAdd);
                    break;
                case ColumnType.string_type:
                case ColumnType.enum_type:
                case ColumnType.path_type:
                    if (db.m_DbType == DatabaseType.MySql)
                        sAdd = string.Format(" ADD `{0}` varchar({1}) {2} NULL ", col.Code, col.ColLen, sDefault);
                    else
                        sAdd = string.Format(" ADD [{0}] [nvarchar]({1}) {2} NULL ", col.Code, col.ColLen, sDefault);
                    if (db.m_DbType == DatabaseType.Sqlite)
                        sAdd += " COLLATE NOCASE";
                    db.ExecuteSql(sUpdate + sAdd);
                    break;
                case ColumnType.text_type:
                    if (db.m_DbType == DatabaseType.MySql)
                        sAdd = string.Format(" ADD `{0}` text {1} NULL ", col.Code, sDefault);
                    else
                        sAdd = string.Format(" ADD [{0}] [text] {1} NULL ", col.Code, sDefault);
                    db.ExecuteSql(sUpdate + sAdd);
                    break;
                case ColumnType.int_type:
                    if (db.m_DbType == DatabaseType.MySql)
                        sAdd = string.Format(" ADD `{0}` int {1} NULL ", col.Code, sDefault);
                    else
                        sAdd = string.Format(" ADD [{0}] [int] {1} NULL ", col.Code, sDefault);
                    db.ExecuteSql(sUpdate + sAdd);
                    break;
                case ColumnType.long_type:
                    if (db.m_DbType == DatabaseType.MySql)
                        sAdd = string.Format(" ADD `{0}` bigint {1} NULL ", col.Code, sDefault);
                    else
                        sAdd = string.Format(" ADD [{0}] [bigint] {1} NULL ", col.Code, sDefault);
                    db.ExecuteSql(sUpdate + sAdd);
                    break;
                case ColumnType.bool_type:
                    if (db.m_DbType == DatabaseType.MySql)
                        sAdd = string.Format(" ADD `{0}` bit {1} NULL ", col.Code, sDefault);
                    else
                        sAdd = string.Format(" ADD [{0}] [bit] {1} NULL ", col.Code, sDefault);
                    db.ExecuteSql(sUpdate + sAdd);
                    break;
                case ColumnType.datetime_type:
                    if (db.m_DbType == DatabaseType.MySql)
                        sAdd = string.Format(" ADD `{0}` datetime {1} NULL ", col.Code, sDefault);
                    else
                        sAdd = string.Format(" ADD [{0}] [datetime] {1} NULL ", col.Code, sDefault);
                    db.ExecuteSql(sUpdate + sAdd);
                    break;
                case ColumnType.numeric_type:
                    if (db.m_DbType == DatabaseType.MySql)
                        sAdd = string.Format(" ADD `{0}` numeric({1},{2}) {3} NULL ", col.Code, col.ColLen, col.ColDecimal, sDefault);
                    else
                        sAdd = string.Format(" ADD [{0}] [numeric]({1},{2}) {3} NULL ", col.Code, col.ColLen, col.ColDecimal, sDefault);
                    db.ExecuteSql(sUpdate + sAdd);
                    break;
                case ColumnType.object_type:
                    if (db.m_DbType == DatabaseType.MySql)
                        sAdd = string.Format(" ADD `{0}` binary {1} NULL ", col.Code, sDefault);
                    else
                        sAdd = string.Format(" ADD [{0}] [image] {1} NULL ", col.Code, sDefault);
                    db.ExecuteSql(sUpdate + sAdd);
                    break;
            }
        }
        //修改字段
        static void UpdateColumn(DB db, CTable table, CColumn col)
        {
            string sUpdate = "";
            if (db.m_DbType == DatabaseType.MySql)
                sUpdate = string.Format("ALTER TABLE `{0}` ", table.Code);
            else
                sUpdate = string.Format("ALTER TABLE [{0}] ", table.Code);
            if (col.Code.Equals("id", StringComparison.OrdinalIgnoreCase))
                return;
            else if (col.Code.Equals("Created", StringComparison.OrdinalIgnoreCase))
                return;
            else if (col.Code.Equals("Creator", StringComparison.OrdinalIgnoreCase))
                return;
            else if (col.Code.Equals("Updated", StringComparison.OrdinalIgnoreCase))
                return;
            else if (col.Code.Equals("Updator", StringComparison.OrdinalIgnoreCase))
                return;
            else if (col.Code.Equals("IsDeleted", StringComparison.OrdinalIgnoreCase))
                return;
            //text image uniqueidentifier类型的字段不支持修改，只能先删除后添加
            if ((col.m_arrOldVal["coltype"].IntVal == (int)ColumnType.text_type || col.m_arrNewVal["coltype"].IntVal == (int)ColumnType.text_type)
                || (col.m_arrOldVal["coltype"].IntVal == (int)ColumnType.object_type || col.m_arrNewVal["coltype"].IntVal == (int)ColumnType.object_type)
                || (col.m_arrOldVal["coltype"].IntVal == (int)ColumnType.guid_type || col.m_arrNewVal["coltype"].IntVal == (int)ColumnType.guid_type)
                || (col.m_arrOldVal["coltype"].IntVal == (int)ColumnType.ref_type || col.m_arrNewVal["coltype"].IntVal == (int)ColumnType.ref_type))
            {
                if (col.m_arrOldVal["code"].StrVal.Equals(col.m_arrNewVal["code"].StrVal, StringComparison.OrdinalIgnoreCase))
                    return;
                DelColumn(db, table, col);
                AddColumn(db, table, col);
                return;
            }

            string sUpd = "";
            //Sqlserver不支持w3c标准默认值修改
            //string sDefault = (col.DefaultValue == "") ? "" : string.Format(" DEFAULT {0} ", col.DefaultValue);
            string sDefault = "";
            switch (col.ColType)
            {
                case ColumnType.guid_type:
                case ColumnType.ref_type:
                    if (db.m_DbType == DatabaseType.Sqlite)
                        sUpd = string.Format(" ALTER COLUMN [{0}] [guid] {1} NULL ", col.Code, sDefault);
                    else if (db.m_DbType == DatabaseType.MySql)
                        sUpd = string.Format(" modify `{0}` varchar(50) {1} NULL ", col.Code, sDefault);
                    else
                        sUpd = string.Format(" ALTER COLUMN [{0}] [uniqueidentifier] {1} NULL ", col.Code, sDefault);
                    db.ExecuteSql(sUpdate + sUpd);
                    break;
                case ColumnType.string_type:
                case ColumnType.enum_type:
                case ColumnType.path_type:
                    if (db.m_DbType == DatabaseType.MySql)
                        sUpd = string.Format(" modify `{0}` varchar({1}) {2} NULL ", col.Code, col.ColLen, sDefault);
                    else
                        sUpd = string.Format(" ALTER COLUMN [{0}] [nvarchar]({1}) {2} NULL ", col.Code, col.ColLen, sDefault);
                    if (db.m_DbType == DatabaseType.Sqlite)
                        sUpd += " COLLATE NOCASE";
                    db.ExecuteSql(sUpdate + sUpd);
                    break;
                case ColumnType.text_type:
                    if (db.m_DbType == DatabaseType.MySql)
                        sUpd = string.Format(" modify `{0}` text {1} NULL ", col.Code, sDefault);
                    else
                        sUpd = string.Format(" ALTER COLUMN [{0}] [text] {1} NULL ", col.Code, sDefault);
                    db.ExecuteSql(sUpdate + sUpd);
                    break;
                case ColumnType.int_type:
                    if (db.m_DbType == DatabaseType.MySql)
                        sUpd = string.Format(" modify `{0}` int {1} NULL ", col.Code, sDefault);
                    else
                        sUpd = string.Format(" ALTER COLUMN [{0}] [int] {1} NULL ", col.Code, sDefault);
                    db.ExecuteSql(sUpdate + sUpd);
                    break;
                case ColumnType.long_type:
                    if (db.m_DbType == DatabaseType.MySql)
                        sUpd = string.Format(" modify `{0}` bigint {1} NULL ", col.Code, sDefault);
                    else
                        sUpd = string.Format(" ALTER COLUMN [{0}] [bigint] {1} NULL ", col.Code, sDefault);
                    db.ExecuteSql(sUpdate + sUpd);
                    break;
                case ColumnType.bool_type:
                    if (db.m_DbType == DatabaseType.MySql)
                        sUpd = string.Format(" modify `{0}` bit {1} NULL ", col.Code, sDefault);
                    else
                        sUpd = string.Format(" ALTER COLUMN [{0}] [bit] {1} NULL ", col.Code, sDefault);
                    db.ExecuteSql(sUpdate + sUpd);
                    break;
                case ColumnType.datetime_type:
                    if (db.m_DbType == DatabaseType.MySql)
                        sUpd = string.Format(" modify `{0}` datetime {1} NULL ", col.Code, sDefault);
                    else
                        sUpd = string.Format(" ALTER COLUMN [{0}] [datetime] {1} NULL ", col.Code, sDefault);
                    db.ExecuteSql(sUpdate + sUpd);
                    break;
                case ColumnType.numeric_type:
                    if (db.m_DbType == DatabaseType.MySql)
                        sUpd = string.Format(" modify `{0}` numeric({1},{2}) {3} NULL ", col.Code, col.ColLen, col.ColDecimal, sDefault);
                    else
                        sUpd = string.Format(" ALTER COLUMN [{0}] [numeric]({1},{2}) {3} NULL ", col.Code, col.ColLen, col.ColDecimal, sDefault);
                    db.ExecuteSql(sUpdate + sUpd);
                    break;
                case ColumnType.object_type:
                    if (db.m_DbType == DatabaseType.MySql)
                        sUpd = string.Format(" modify `{0}` binary {1} NULL ", col.Code, sDefault);
                    else
                        sUpd = string.Format(" ALTER COLUMN [{0}] [image] {1} NULL ", col.Code, sDefault);
                    db.ExecuteSql(sUpdate + sUpd);
                    break;
            }

            //修改默认值
            UpdateDefaultVal(db, table, col);
        }
        //修改默认值
        static void UpdateDefaultVal(DB db, CTable table, CColumn col)
        {
            if (col.m_arrNewVal["defaultvalue"].StrVal == col.m_arrOldVal["defaultvalue"].StrVal)
                return;

            string sUpdate = string.Format("ALTER TABLE [{0}] ", table.Code);
            //修改默认值,先删除所有默认值，然后再新建默认值
            if (col.m_arrOldVal["defaultvalue"].StrVal != "")
            {
                string sDelDef = string.Format(" DROP CONSTRAINT [DF__{0}__{1}],", table.Code, col.Code);
                db.ExecuteSql(sUpdate + sDelDef);
            }
            if (col.m_arrNewVal["defaultvalue"].StrVal != "")
            {
                string sDefault = string.Format(" ADD CONSTRAINT [DF__{0}__{1}] DEFAULT ({2}) FOR [{1}],", table.Code, col.Code, col.DefaultValue);
                db.ExecuteSql(sUpdate + sDefault);
            }
            
        }
        //删除默认值约束
        static void DelConstraint(DB db, string sTbCode,string sColName)
        {
            string sSql=string.Format("select  b. name from sysobjects b join syscolumns a on b. id = a. cdefault where a. id = object_id ( '{0} ' ) and a. name = '{1}'",
               sTbCode,sColName );
            object objVal= db.GetSingle(sSql);
            if(objVal==null)
                return ;

            sSql = string.Format("alter table {0} drop constraint {1}",
                sTbCode, objVal);
            db.ExecuteSql(sSql);
        }

        #endregion

        //转成Guid, 考虑到mysql没有guid，只能用文本
        Guid ConvertToGuid(object val)
        {
            if (val.GetType() == Type.GetType("System.String"))
                return Guid.Parse(val.ToString());
            else
                return (Guid)val;
        }
    }

}