using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using Microsoft.Win32;
using System.IO;
using System.Diagnostics;
using System.Threading;
using MySql.Data.MySqlClient;

namespace ErpCore
{
    public partial class DbSetup : Form
    {
        public DbSetup()
        {
            InitializeComponent();
        }


        private void btOK_Click(object sender, EventArgs e)
        {
            string sConnectionString = "";
            if (cbDbType.SelectedIndex == 0
                || cbDbType.SelectedIndex == 1)
            {
                if (!TestConnectServer(cbServer.Text.Trim(), txtUser.Text.Trim(), txtPwd.Text.Trim()))
                {
                    MessageBox.Show("连接数据库服务器失败，请安装sqlserver2000或sqlserver2005，并确认登录帐号正确。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (!TestConnectDb(cbServer.Text.Trim(),txtDbName.Text.Trim(), txtUser.Text.Trim(), txtPwd.Text.Trim()))
                {
                    if (MessageBox.Show("检测到没有安装数据库，是否确定安装数据库？如果安装数据库，请确认服务器为本机！", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                        != DialogResult.OK)
                        return;
                    if (!CreateDB(cbServer.Text.Trim(), txtDbName.Text.Trim(), txtUser.Text.Trim(), txtPwd.Text.Trim()))
                    {
                        MessageBox.Show("安装出现异常！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("安装成功！");
                    }
                }
                else
                {
                }
                sConnectionString = string.Format("Provider=SQLOLEDB.1;Password={0};Persist Security Info=true;User ID={1};Initial Catalog={2};Data Source={3}", txtPwd.Text.Trim(), txtUser.Text.Trim(), txtDbName.Text.Trim(), cbServer.Text.Trim());

            }
            else if (cbDbType.SelectedIndex == 2) //sqlite
            {
                if (cbServer.Text.Trim() == "")
                {
                    MessageBox.Show("请选择路径！");
                    return;
                }
                string strDataPath = cbServer.Text.Trim();
                if (strDataPath[strDataPath.Length - 1] != '\\')
                    strDataPath += "\\";
                strDataPath += "sqlite\\";
                if (!Directory.Exists(strDataPath))
                    Directory.CreateDirectory(strDataPath);

                string path = Application.StartupPath;
                path += "\\database\\sqlite\\ErpCore.db";
                string sDest = strDataPath + string.Format("{0}.db",txtDbName.Text.Trim());
                
                if(!File.Exists(sDest))
                    File.Copy(path, sDest,false);

                sConnectionString = string.Format("Data Source={0};Version=3;UseUTF16Encoding=True;BinaryGUID=False;", sDest); 
            }
            else if (cbDbType.SelectedIndex == 3) //mysql
            {
                sConnectionString = String.Format("server={0};Port=3306;uid={1};pwd={2};database={3}",
                    cbServer.Text.Trim(), txtUser.Text.Trim(), txtPwd.Text, txtDbName.Text.Trim());

                if (!TestConnectDb(cbServer.Text.Trim(), txtDbName.Text.Trim(), txtUser.Text.Trim(), txtPwd.Text.Trim()))
                {
                    if (MessageBox.Show("检测到没有安装数据库，是否确定安装数据库？如果安装数据库，请确认服务器为本机！", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                        != DialogResult.OK)
                        return;
                    if (!CreateDB(cbServer.Text.Trim(), txtDbName.Text.Trim(), txtUser.Text.Trim(), txtPwd.Text.Trim()))
                    {
                        MessageBox.Show("安装出现异常！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("安装成功！");
                    }
                }
            }

            
            string sPath = Application.StartupPath + "\\config.cfg";
            if (File.Exists(sPath))
                File.Delete(sPath);
            File.WriteAllText(sPath, ErpCore.Util.DESEncrypt.DesEncrypt(sConnectionString));

            DialogResult = DialogResult.OK;
            this.Close();
        }
        public bool TestConnectServer(string strServer, string strUser, string strPwd)
        {
            try
            {
                OleDbConnection con = new OleDbConnection();
                con.ConnectionString = string.Format("Provider=SQLOLEDB.1;Password={0};Persist Security Info=true;User ID={1};Data Source={2}", strPwd, strUser, strServer);
                try { con.Open(); }
                catch (Exception e)
                {
                    return false;
                }
                if (con.State != ConnectionState.Open)
                {
                    return false;
                }
                con.Close();

                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool TestConnectDb(string strServer,string strDbName, string strUser, string strPwd)
        {
            if (cbDbType.SelectedIndex == 0
                || cbDbType.SelectedIndex == 1)//sqlserver
            {
                try
                {
                    OleDbConnection con = new OleDbConnection();
                    con.ConnectionString = string.Format("Provider=SQLOLEDB.1;Password={0};Persist Security Info=true;User ID={1};Initial Catalog={2};Data Source={3}", strPwd, strUser, strDbName, strServer);
                    try { con.Open(); }
                    catch (Exception e)
                    {
                        return false;
                    }
                    if (con.State != ConnectionState.Open)
                    {
                        return false;
                    }
                    con.Close();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else if (cbDbType.SelectedIndex == 3) //mysql
            {
                try{
                    MySqlConnection con = new MySqlConnection();
                    con.ConnectionString = String.Format("server={0};Port=3306;uid={1};pwd={2};database={3}",
                        strServer, strUser, strPwd, strDbName);
                    try { con.Open(); }
                    catch (Exception e)
                    {
                        return false;
                    }
                    if (con.State != ConnectionState.Open)
                    {
                        return false;
                    }
                    con.Close();

                    return true;
                }
                catch
                {
                    return false;
                }
            }


            return false;
        }
        //创建sql server 数据库
        public bool CreateDB(string strServer, string strDbName, string strUser, string strPwd)
        {
            if (cbDbType.SelectedIndex == 0
                || cbDbType.SelectedIndex == 1)//sqlserver
            {
                try
                {
                    OleDbConnection con = new OleDbConnection();
                    con.ConnectionString = string.Format("Provider=SQLOLEDB.1;Password={0};Persist Security Info=true;User ID={1};Data Source={2}", strPwd, strUser, strServer);
                    try { con.Open(); }
                    catch (Exception e)
                    {
                        return false;
                    }
                    if (con.State != ConnectionState.Open)
                    {
                        return false;
                    }
                    else
                    {
                        //还原数据库
                        string strSqlDataPath = Application.StartupPath + "\\data\\";
                        if (!Directory.Exists(strSqlDataPath))
                            Directory.CreateDirectory(strSqlDataPath);

                        string path = Application.StartupPath;
                        if (cbDbType.SelectedIndex == 0)
                        {
                            path += "\\database\\sql2000\\ErpCore.bak";
                            strSqlDataPath += "sql2000\\";
                            if (!Directory.Exists(strSqlDataPath))
                                Directory.CreateDirectory(strSqlDataPath);
                        }
                        else
                        {
                            path += "\\database\\sql2005\\ErpCore.bak";
                            strSqlDataPath += "sql2005\\";
                            if (!Directory.Exists(strSqlDataPath))
                                Directory.CreateDirectory(strSqlDataPath);
                        }
                        string strDbSql = string.Format("RESTORE   DATABASE   {0} FROM   DISK   =   '{1}'", strDbName, path);
                        strDbSql += string.Format(" with move 'ErpCore_Data' to '{0}{1}_Data.mdf',", strSqlDataPath, strDbName);
                        strDbSql += string.Format("move 'ErpCore_log'  to '{0}{1}_log.ldf'", strSqlDataPath, strDbName);


                        OleDbCommand cmdDB = new OleDbCommand(strDbSql, con);
                        cmdDB.ExecuteNonQuery();

                    }

                    con.Close();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else if (cbDbType.SelectedIndex == 3)//mysql
            {

                MessageBox.Show("本操作将需要大约6分钟时间，请耐心等待！");

                string sContent =string.Format( "CREATE DATABASE `{0}` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci",txtDbName.Text.Trim());
                string sFile = Application.StartupPath + "/createdb.txt";
                File.Delete(sFile);
                File.WriteAllText(sFile, sContent);

                string sDir = Application.StartupPath.Replace("\\", "/");
                Process p = new Process();
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.CreateNoWindow = true;
                p.Start();
                p.StandardInput.AutoFlush = true;
                string sCmd = string.Format("mysql -h {0} -u {1}  --password={2} < {3}/createdb.txt"
                    , cbServer.Text.Trim(), txtUser.Text.Trim(), txtPwd.Text, sDir);
                p.StandardInput.WriteLine(sCmd);
                p.StandardInput.WriteLine("exit");

                string sRet = p.StandardOutput.ReadToEnd();
                string sErr = p.StandardError.ReadToEnd();
                p.WaitForExit();
                p.Close();

                if (sErr.Trim() != "")
                {
                    MessageBox.Show(sErr, "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return false;
                }

                p = new Process();
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.CreateNoWindow = true;
                p.Start();
                p.StandardInput.AutoFlush = true;
                sCmd = string.Format("mysql -h {0} -u {1}  --password={2}  {3} < {4}/database/mysql/erpcore.sql"
                    , cbServer.Text.Trim(), txtUser.Text.Trim(), txtPwd.Text, txtDbName.Text.Trim(), sDir);
                p.StandardInput.WriteLine(sCmd);
                p.StandardInput.WriteLine("exit");

                sRet = p.StandardOutput.ReadToEnd();
                sErr = p.StandardError.ReadToEnd();
                p.WaitForExit();
                p.Close();


                if (sErr.Trim() != "")
                {
                    MessageBox.Show(sErr, "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return false;
                }

                return true;
            }

            return false;
        }

        private void DbSetup_Load(object sender, EventArgs e)
        {
            cbDbType.SelectedIndex = 0;
        }

        private void cbDbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDbType.SelectedIndex == 2)//sqlite
            {
                btBrowse.Visible = true;
                label3.Text = "路径：";
                cbServer.Text = "";
                label1.Visible = false;
                label2.Visible = false;
                txtUser.Visible = false;
                txtPwd.Visible = false;
            }
            else if (cbDbType.SelectedIndex == 3)//mysql
            {
                btBrowse.Visible = false;
                label3.Text = "服务器：";
                label1.Visible = true;
                label2.Visible = true;
                cbServer.Text = "localhost";
                txtUser.Visible = true;
                txtUser.Text = "root";
                txtPwd.Visible = true;
            }
            else
            {
                btBrowse.Visible = false;
                label3.Text = "服务器：";
                label1.Visible = true;
                label2.Visible = true;
                cbServer.Text = "(local)";
                txtUser.Visible = true;
                txtPwd.Visible = true;
            }
        }

        private void btBrowse_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowNewFolderButton = true;
            if (folderBrowserDialog1.ShowDialog() != DialogResult.OK)
                return;
            cbServer.Text = folderBrowserDialog1.SelectedPath;

        }
    }
}