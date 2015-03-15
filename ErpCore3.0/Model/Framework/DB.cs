// File:    DB.cs
// Author:  甘孝俭
// email:   ganxiaojian@hotmail.com 
// QQ:      154986287
// http://www.8088net.com
// 协议声明：本软件为开源系统，遵循国际开源组织协议。任何单位或个人可以使用或修改本软件源码，
//          可以用于作为非商业或商业用途，但由于使用本源码所引起的一切后果与作者无关。
//          未经作者许可，禁止任何企业或个人直接出售本源码或者把本软件作为独立的功能进行销售活动，
//          作者将保留追究责任的权利。
// Created: 2011年7月9日 13:37:16
// Purpose: Definition of Class DbHelperOleDb
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.Data.SQLite;
using MySql.Data.MySqlClient;

namespace ErpCoreModel.Framework
{
    public enum DatabaseType { OleDb,Sqlite,MySql};
    /// <summary>
    /// 数据访问基础类
    /// </summary>
    public class DB
    {
        //public string m_connectionString = "Provider=SQLOLEDB.1;Password=sa;Persist Security Info=True;User ID=sa;Initial Catalog=ErpCore;Data Source=(local)";     		
        private string m_connectionString = "";

        public DatabaseType m_DbType = DatabaseType.OleDb;
        DbHelperOleDb m_DbHelperOleDb = null;
        DbHelperSqlite m_DbHelperSqlite = null;
        DbHelperMySql m_DbHelperMySql = null;

        public DB(string connStr)
        {
            ConnectionString = connStr;
        }
        ~DB()
        {
            CloseConn();
        }

        public string ConnectionString
        {
            get
            {
                return m_connectionString;
            }
            set
            {
                this.m_connectionString = value;
                if (this.m_connectionString.IndexOf("OleDb", StringComparison.OrdinalIgnoreCase) > -1)
                {
                    m_DbType = DatabaseType.OleDb;
                    if (m_DbHelperOleDb == null)
                        m_DbHelperOleDb = new DbHelperOleDb(this.m_connectionString);
                }
                else if (this.m_connectionString.IndexOf("Port", StringComparison.OrdinalIgnoreCase) > -1)
                {
                    m_DbType = DatabaseType.MySql;
                    if(m_DbHelperMySql==null)
                        m_DbHelperMySql = new DbHelperMySql(this.m_connectionString);
                }
                else
                {
                    m_DbType = DatabaseType.Sqlite;
                    if(m_DbHelperSqlite==null)
                        m_DbHelperSqlite = new DbHelperSqlite(this.m_connectionString);
                }
            }
        }


        #region 打开关闭
        public bool OpenConn()
        {
            if (m_DbType == DatabaseType.OleDb)
                return m_DbHelperOleDb.OpenConn();
            else if (m_DbType == DatabaseType.MySql)
                return m_DbHelperMySql.OpenConn();
            else
                return m_DbHelperSqlite.OpenConn();
        }
        public bool CloseConn()
        {
            if (m_DbType == DatabaseType.OleDb)
                return m_DbHelperOleDb.CloseConn();
            else if (m_DbType == DatabaseType.MySql)
                return m_DbHelperMySql.CloseConn();
            else
                return m_DbHelperSqlite.CloseConn();
        }
        #endregion

        #region 事务
        public void BeginTransaction()
        {
            if (m_DbType == DatabaseType.OleDb)
                m_DbHelperOleDb.BeginTransaction();
            else if (m_DbType == DatabaseType.MySql)
                m_DbHelperMySql.BeginTransaction();
            else
                m_DbHelperSqlite.BeginTransaction();
        }
        public void CommitTransaction()
        {
            if (m_DbType == DatabaseType.OleDb)
                m_DbHelperOleDb.CommitTransaction();
            else if (m_DbType == DatabaseType.MySql)
                m_DbHelperMySql.CommitTransaction();
            else
                m_DbHelperSqlite.CommitTransaction();
        }
        public void RollbackTransaction()
        {
            if (m_DbType == DatabaseType.OleDb)
                m_DbHelperOleDb.RollbackTransaction();
            else if (m_DbType == DatabaseType.MySql)
                m_DbHelperMySql.RollbackTransaction();
            else
                m_DbHelperSqlite.RollbackTransaction();
        }
        #endregion

        #region 公用方法

        public  int GetMaxID(string sFieldName, string sTableName)
        {
            if (m_DbType == DatabaseType.OleDb)
                return m_DbHelperOleDb.GetMaxID(sFieldName, sTableName);
            else if (m_DbType == DatabaseType.MySql)
                return m_DbHelperMySql.GetMaxID(sFieldName, sTableName);
            else
                return m_DbHelperSqlite.GetMaxID(sFieldName, sTableName);
        }
        public  bool Exists(string sSql)
        {
            if (m_DbType == DatabaseType.OleDb)
                return m_DbHelperOleDb.Exists(sSql);
            else if (m_DbType == DatabaseType.MySql)
                return m_DbHelperMySql.Exists(sSql);
            else
                return m_DbHelperSqlite.Exists(sSql);
        }

        #endregion

        #region  执行简单SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="sSql">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public  int ExecuteSql(string sSql)
        {
            if (m_DbType == DatabaseType.OleDb)
                return m_DbHelperOleDb.ExecuteSql(sSql);
            else if (m_DbType == DatabaseType.MySql)
                return m_DbHelperMySql.ExecuteSql(sSql);
            else
                return m_DbHelperSqlite.ExecuteSql(sSql);
        }

        /// <summary>
        /// 向数据库里插入/修改图像格式的字段
        /// </summary>
        /// <param name="sSql">SQL语句</param>
        /// <param name="fs">图像字节,数据库的字段类型为image的情况</param>
        /// <returns>影响的记录数</returns>
        public int ExecuteSqlImg(string sSql, byte[] fs)
        {
            if (m_DbType == DatabaseType.OleDb)
                return m_DbHelperOleDb.ExecuteSqlImg(sSql, fs);
            else if (m_DbType == DatabaseType.MySql)
                return m_DbHelperMySql.ExecuteSqlImg(sSql, fs);
            else
                return m_DbHelperSqlite.ExecuteSqlImg(sSql, fs);
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="sSql">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public  object GetSingle(string sSql)
        {
            if (m_DbType == DatabaseType.OleDb)
                return m_DbHelperOleDb.GetSingle(sSql);
            else if (m_DbType == DatabaseType.MySql)
                return m_DbHelperMySql.GetSingle(sSql);
            else
                return m_DbHelperSqlite.GetSingle(sSql);

        }
        
        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="sSql">查询语句</param>
        /// <returns>DataSet</returns>
        public  DataSet Query(string sSql)
        {
            if (m_DbType == DatabaseType.OleDb)
                return m_DbHelperOleDb.Query(sSql);
            else if (m_DbType == DatabaseType.MySql)
                return m_DbHelperMySql.Query(sSql);
            else
                return m_DbHelperSqlite.Query(sSql);

        }

        /// <summary>
        /// 执行查询语句，返回DataTable
        /// </summary>
        /// <param name="sSql">查询语句</param>
        /// <returns>DataTable</returns>
        public DataTable QueryT(string sSql)
        {
            if (m_DbType == DatabaseType.OleDb)
                return m_DbHelperOleDb.QueryT(sSql);
            else if (m_DbType == DatabaseType.MySql)
                return m_DbHelperMySql.QueryT(sSql);
            else
                return m_DbHelperSqlite.QueryT(sSql);

        }

        #endregion

        #region 执行带参数的SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="sSql">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public  int ExecuteSql(string sSql, List<DbParameter> cmdParms)
        {
            if (m_DbType == DatabaseType.OleDb)
            {
                List<OleDbParameter> lstParam = new List<OleDbParameter>();
                if (cmdParms != null)
                {
                    foreach (DbParameter p in cmdParms)
                    {
                        OleDbParameter opara = new OleDbParameter();
                        opara.ParameterName = p.ParameterName;
                        opara.Value = p.Value;
                        lstParam.Add(opara);
                    }
                }
                return m_DbHelperOleDb.ExecuteSql(sSql, lstParam);
            }
            else if (m_DbType == DatabaseType.MySql)
            {
                List<MySqlParameter> lstParam = new List<MySqlParameter>();
                if (cmdParms != null)
                {
                    foreach (DbParameter p in cmdParms)
                    {
                        MySqlParameter opara = new MySqlParameter();
                        opara.ParameterName = p.ParameterName;
                        opara.Value = p.Value;
                        lstParam.Add(opara);
                    }
                }
                return m_DbHelperMySql.ExecuteSql(sSql, lstParam);
            }
            else
            {
                List<SQLiteParameter> lstParam = new List<SQLiteParameter>();
                if (cmdParms != null)
                {
                    foreach (DbParameter p in cmdParms)
                    {
                        SQLiteParameter opara = new SQLiteParameter();
                        opara.ParameterName = p.ParameterName;
                        opara.Value = p.Value;
                        lstParam.Add(opara);
                    }
                }
                return m_DbHelperSqlite.ExecuteSql(sSql, lstParam);
            }
        }


        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="sSql">查询语句</param>
        /// <returns>DataSet</returns>
        public  DataSet Query(string sSql, List<DbParameter> cmdParms)
        {
            if (m_DbType == DatabaseType.OleDb)
            {
                List<OleDbParameter> lstParam = new List<OleDbParameter>();
                if (cmdParms != null)
                {
                    foreach (DbParameter p in cmdParms)
                    {
                        OleDbParameter opara = new OleDbParameter();
                        opara.ParameterName = p.ParameterName;
                        opara.Value = p.Value;
                        lstParam.Add(opara);
                    }
                }
                return m_DbHelperOleDb.Query(sSql, lstParam);
            }
            else if (m_DbType == DatabaseType.MySql)
            {
                List<MySqlParameter> lstParam = new List<MySqlParameter>();
                if (cmdParms != null)
                {
                    foreach (DbParameter p in cmdParms)
                    {
                        MySqlParameter opara = new MySqlParameter();
                        opara.ParameterName = p.ParameterName;
                        opara.Value = p.Value;
                        lstParam.Add(opara);
                    }
                }
                return m_DbHelperMySql.Query(sSql, lstParam);
            }
            else
            {
                List<SQLiteParameter> lstParam = new List<SQLiteParameter>();
                if (cmdParms != null)
                {
                    foreach (DbParameter p in cmdParms)
                    {
                        SQLiteParameter opara = new SQLiteParameter();
                        opara.ParameterName = p.ParameterName;
                        opara.Value = p.Value;
                        lstParam.Add(opara);
                    }
                }
                return m_DbHelperSqlite.Query(sSql, lstParam);
            }
        }


        /// <summary>
        /// 执行查询语句，返回DataTable
        /// </summary>
        /// <param name="sSql">查询语句</param>
        /// <returns>DataTable</returns>
        public DataTable QueryT(string sSql, List<DbParameter> cmdParms)
        {
            if (m_DbType == DatabaseType.OleDb)
            {
                List<OleDbParameter> lstParam = new List<OleDbParameter>();
                if (cmdParms != null)
                {
                    foreach (DbParameter p in cmdParms)
                    {
                        OleDbParameter opara = new OleDbParameter();
                        opara.ParameterName = p.ParameterName;
                        opara.Value = p.Value;
                        lstParam.Add(opara);
                    }
                }
                return m_DbHelperOleDb.QueryT(sSql, lstParam);
            }
            else if (m_DbType == DatabaseType.MySql)
            {
                List<MySqlParameter> lstParam = new List<MySqlParameter>();
                if (cmdParms != null)
                {
                    foreach (DbParameter p in cmdParms)
                    {
                        MySqlParameter opara = new MySqlParameter();
                        opara.ParameterName = p.ParameterName;
                        opara.Value = p.Value;
                        lstParam.Add(opara);
                    }
                }
                return m_DbHelperMySql.QueryT(sSql, lstParam);
            }
            else
            {
                List<SQLiteParameter> lstParam = new List<SQLiteParameter>();
                if (cmdParms != null)
                {
                    foreach (DbParameter p in cmdParms)
                    {
                        SQLiteParameter opara = new SQLiteParameter();
                        opara.ParameterName = p.ParameterName;
                        opara.Value = p.Value;
                        lstParam.Add(opara);
                    }
                }
                return m_DbHelperSqlite.QueryT(sSql, lstParam);
            }
        }


        #endregion


    }
}
