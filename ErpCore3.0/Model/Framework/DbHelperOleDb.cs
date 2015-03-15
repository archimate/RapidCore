// File:    DbHelperOleDb.cs
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

namespace ErpCoreModel.Framework
{
    /// <summary>
    /// 数据访问基础类
    /// </summary>
    public class DbHelperOleDb
    {
        //public string m_connectionString = "Provider=SQLOLEDB.1;Password=sa;Persist Security Info=True;User ID=sa;Initial Catalog=ErpCore;Data Source=(local)";     		
        private string m_connectionString = "";
        OleDbConnection m_conn = new OleDbConnection();
        OleDbTransaction m_transaction = null;

        public DbHelperOleDb(string connStr)
        {
            ConnectionString = connStr;
        }

        public OleDbConnection Conn
        {
            get { return m_conn; }
            set { m_conn = value; }
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
            }
        }
        public OleDbTransaction Transaction
        {
            get { return m_transaction; }
        }

        #region 打开关闭
        public bool OpenConn()
        {
            Conn.ResetState();
            if (Conn.State != ConnectionState.Open)
            {
                try { Conn.Close(); }
                catch { }
                try
                {
                    Conn.ConnectionString = ConnectionString;
                    Conn.Open();
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }
        public bool CloseConn()
        {
            try
            {
                Conn.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }
        #endregion

        #region 事务
        public void BeginTransaction()
        {
            try
            {
                m_transaction = Conn.BeginTransaction();
            }
            catch { }
        }
        public void CommitTransaction()
        {
            try
            {
                if (m_transaction != null)
                    m_transaction.Commit();
            }
            catch { }
            m_transaction = null;
        }
        public void RollbackTransaction()
        {
            try
            {
                if (m_transaction != null)
                    m_transaction.Rollback();
            }
            catch { }
            m_transaction = null;
        }
        #endregion

        #region 公用方法
       
        public  int GetMaxID(string FieldName, string TableName)
        {
            string sSql = "select max(" + FieldName + ")+1 from " + TableName;
            object obj = GetSingle(sSql);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return int.Parse(obj.ToString());
            }
        }
        public  bool Exists(string sSql)
        {
            object obj = GetSingle(sSql);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public  bool Exists(string sSql, List<OleDbParameter> cmdParms)
        {
            object obj = GetSingle(sSql, cmdParms);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
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
            if(!OpenConn())
                return -1;

            using (OleDbCommand cmd = new OleDbCommand(sSql, Conn))
            {
                try
                {
                    if (Transaction != null)
                        cmd.Transaction = Transaction;
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (System.Data.OleDb.OleDbException E)
                {
                    //throw new Exception(E.Message);
                    return -1;
                }
            }
        }

        /// <summary>
        /// 向数据库里插入/修改图像格式的字段
        /// </summary>
        /// <param name="sSql">SQL语句</param>
        /// <param name="fs">图像字节,数据库的字段类型为image的情况</param>
        /// <returns>影响的记录数</returns>
        public  int ExecuteSqlImg(string sSql, byte[] fs)
        {
            if (!OpenConn())
                return -1;
            //try
            //{
                using (OleDbCommand cmd = new OleDbCommand(sSql, Conn))
                {
                    if (Transaction != null)
                        cmd.Transaction = Transaction;
                    System.Data.OleDb.OleDbParameter myParameter = new System.Data.OleDb.OleDbParameter("@fs", OleDbType.Binary);
                    myParameter.Value = fs;
                    cmd.Parameters.Add(myParameter);
                    
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
            //}
            //catch
            //{
            //    return -1;
            //}
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="sSql">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public  object GetSingle(string sSql)
        {
            if (!OpenConn())
                return null;
            try
            {
                using (OleDbCommand cmd = new OleDbCommand(sSql, Conn))
                {
                    if (Transaction != null)
                        cmd.Transaction = Transaction;
                    object obj = cmd.ExecuteScalar();
                    if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                    {
                        return null;
                    }
                    else
                    {
                        return obj;
                    }
                }
            }
            catch
            {
                return null;
            }

        }
        /// <summary>
        /// 执行查询语句，返回OleDbDataReader
        /// </summary>
        /// <param name="sSql">查询语句</param>
        /// <returns>OleDbDataReader</returns>
        public  OleDbDataReader ExecuteReader(string sSql)
        {
            if (!OpenConn())
                return null;
            //try
            //{
                using (OleDbCommand cmd = new OleDbCommand(sSql, Conn))
                {
                    if (Transaction != null)
                        cmd.Transaction = Transaction;
                    OleDbDataReader myReader = cmd.ExecuteReader();
                    return myReader;
                }
            //}
            //catch
            //{
            //    return null;
            //}
        }
        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="sSql">查询语句</param>
        /// <returns>DataSet</returns>
        public  DataSet Query(string sSql)
        {
            if (!OpenConn())
                return null;
            //try
            //{
                DataSet ds = new DataSet();
                OleDbDataAdapter Adapter = new OleDbDataAdapter(sSql, Conn);
                Adapter.Fill(ds, "T");

                return ds;
            //}
            //catch
            //{
            //    return null;
            //}
        }
        /// <summary>
        /// 执行查询语句，返回DataTable
        /// </summary>
        /// <param name="sSql">查询语句</param>
        /// <returns>DataSet</returns>
        public DataTable QueryT(string sSql)
        {
            if (!OpenConn())
                return null;
            try
            {
                DataSet ds = new DataSet();
                OleDbDataAdapter Adapter = new OleDbDataAdapter(sSql, Conn);
                Adapter.Fill(ds, "T");

                return ds.Tables[0];
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 执行带参数的SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="sSql">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public  int ExecuteSql(string sSql, List<OleDbParameter> cmdParms)
        {
            if (!OpenConn())
                return -1;
            //try
            //{
                using (OleDbCommand cmd = new OleDbCommand())
                {
                    PrepareCommand(cmd, Conn, Transaction, sSql, cmdParms);
                    int rows = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    return rows;
                }
            //}
            //catch
            //{
            //    return -1;
            //}
        }



        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="sSql">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public  object GetSingle(string sSql, List<OleDbParameter> cmdParms)
        {
            if (!OpenConn())
                return null;
            //try
            //{
                using (OleDbCommand cmd = new OleDbCommand())
                {
                    PrepareCommand(cmd, Conn, Transaction, sSql, cmdParms);
                    object obj = cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                    if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                    {
                        return null;
                    }
                    else
                    {
                        return obj;
                    }
                }
            //}
            //catch
            //{
            //    return null;
            //}
        }

        /// <summary>
        /// 执行查询语句，返回OleDbDataReader
        /// </summary>
        /// <param name="sSql">查询语句</param>
        /// <returns>OleDbDataReader</returns>
        public  OleDbDataReader ExecuteReader(string sSql, List<OleDbParameter> cmdParms)
        {
            if (!OpenConn())
                return null;
            //try
            //{
                using (OleDbCommand cmd = new OleDbCommand())
                {
                    PrepareCommand(cmd, Conn, Transaction, sSql, cmdParms);
                    OleDbDataReader myReader = cmd.ExecuteReader();
                    cmd.Parameters.Clear();
                    return myReader;
                }
            //}
            //catch
            //{
            //    return null;
            //}
        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="sSql">查询语句</param>
        /// <returns>DataSet</returns>
        public  DataSet Query(string sSql, List<OleDbParameter> cmdParms)
        {
            if (!OpenConn())
                return null;
            //try
            //{
                using (OleDbCommand cmd = new OleDbCommand())
                {
                    PrepareCommand(cmd, Conn, Transaction, sSql, cmdParms);
                    using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds, "T");
                        cmd.Parameters.Clear();
                        return ds;
                    }
                }
            //}
            //catch
            //{
            //    return null;
            //}
        }

        /// <summary>
        /// 执行查询语句，返回DataTable
        /// </summary>
        /// <param name="sSql">查询语句</param>
        /// <returns>DataTable</returns>
        public DataTable QueryT(string sSql, List<OleDbParameter> cmdParms)
        {
            if (!OpenConn())
                return null;
            //try
            //{
                using (OleDbCommand cmd = new OleDbCommand())
                {
                    PrepareCommand(cmd, Conn, Transaction, sSql, cmdParms);
                    using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds, "T");
                        cmd.Parameters.Clear();
                        return ds.Tables[0];
                    }
                }
            //}
            //catch
            //{
            //    return null;
            //}
        }

        private  void PrepareCommand(OleDbCommand cmd, OleDbConnection conn, OleDbTransaction trans, string cmdText,List<OleDbParameter> cmdParms)
        {
            if (!OpenConn())
                return;

            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {
                foreach (OleDbParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }

        #endregion

    

    }
}
