using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Reflection;
using System.Diagnostics;
//using Microsoft.Office.Interop;
namespace ErpCore.Util
{
    /// <summary>
    ///Util 的摘要说明
    /// </summary>
    public class CUtil
    {
        public CUtil()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        //随机生成密码
        static public string RandPwd(int iCount)
        {
            string str = @"1234567890qwertyuiopasdfghjklzxcvbnm";
            string sPwd = "";
            Random rand = new Random(DateTime.Now.Millisecond);
            while (sPwd.Length < iCount)
            {
                int idx = rand.Next(str.Length - 1);
                char letter = str[idx];
                sPwd = sPwd + letter;
            }
            return sPwd;
        }

        //判断字符串是否是数字
        //作者:甘孝俭
        public static bool IsNum(string str)
        {
            str = str.Trim();
            if (str == "")
                return false;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '.') continue;
                if (str[i] == '+') continue;
                if (str[i] == '-') continue;
                if ((str[i] == 'e') || (str[i] == 'E')) continue; //科学计数法
                if ((str[i] < '0') || str[i] > '9')
                    return false;
            }
            //不能多个小数点
            int idx = str.IndexOf('.');
            if (idx != -1)
                if (str.IndexOf('.', idx + 1) != -1)
                    return false;
            return true;
        }
        //判断字符串是否是整数
        //作者:甘孝俭
        public static bool IsInt(string str)
        {
            str = str.Trim();
            if (str == "")
                return false;
            for (int i = 0; i < str.Length; i++)
            {
                if ((str[i] < '0') || str[i] > '9')
                    return false;
            }
            return true;
        }

        //转义json特殊字符
        public static void ConvertJsonSymbol(ref string s)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                switch (c)
                {
                    case '"':
                        sb.Append("\\\"");
                        break;
                    case '\\':
                        sb.Append("\\\\");
                        break;
                    case '/':
                        sb.Append("\\/");
                        break;
                    case '\b':
                        sb.Append("\\b");
                        break;
                    case '\f':
                        sb.Append("\\f");
                        break;
                    case '\n':
                        sb.Append("\\n");
                        break;
                    case '\r':
                        sb.Append("\\r");
                        break;
                    case '\t':
                        sb.Append("\\t");
                        break;
                    default:
                        sb.Append(c);
                        break;
                }
            }
            s = sb.ToString();

        }

        /// 导出到Excel
        public static bool OutExcel(System.Data.DataTable dt, string sFile)
        {
            /*
            object objOpt = System.Reflection.Missing.Value;
            int rowCount = dt.Rows.Count;
            int columnCount = dt.Columns.Count;
            //
            Microsoft.Office.Interop.Excel.Application exc = new Microsoft.Office.Interop.Excel.Application();
            if (exc == null)
            {
                return false;
            }
            //
            exc.Visible = false;
            //
            Microsoft.Office.Interop.Excel.Workbooks workbooks = exc.Workbooks;
            //
            Microsoft.Office.Interop.Excel._Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            //
            Microsoft.Office.Interop.Excel.Sheets sheets = exc.Sheets;
            Microsoft.Office.Interop.Excel._Worksheet worksheet = (Microsoft.Office.Interop.Excel._Worksheet)sheets[1];
            if (worksheet == null)
            {
                exc.Quit();
                workbook = null;
                sheets = null;
                workbooks = null;
                exc = null;
                return false;
            }
            //
            Microsoft.Office.Interop.Excel.Range r = worksheet.get_Range("A1", Missing.Value);
            if (r == null)
            {
                workbook.Close(objOpt, objOpt, objOpt);
                exc.Quit();
                workbook = null;
                sheets = null;
                workbooks = null;
                exc = null;
                return false;
            }


            //以上是一些例行的初始化工作,下面进行具体的信息填充

            //填充标题
            int ColIndex = 1;
            foreach (DataColumn dHeader in dt.Columns)
            {
                worksheet.Cells[1, ColIndex++] = dHeader.ColumnName;
            }

            //获取DataTable中的所有行和列的数值,填充到一个二维数组中.
            r = worksheet.get_Range("A2", Missing.Value);
            r = r.get_Resize(rowCount, columnCount);
            r.RowHeight = 20;
            //object[,] myData = new object[rowCount , columnCount];
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    //myData[i, j] = lvw.Items[i].SubItems[j].Text;;     //这里的获取注意行列次序

                    worksheet.Cells[i + 2, j + 1] = dt.Rows[i][j].ToString();

                }
            }
            //将填充好的二维数组填充到Excel对象中.
            //r = worksheet.get_Range(worksheet.Cells[2, 1], worksheet.Cells[rowCount + 2, columnCount+1]);
            //r = r.get_Resize(rowCount, columnCount);
            //r.Value2 = myData;

            worksheet.SaveAs(sFile, objOpt, objOpt, objOpt, objOpt, objOpt,
                objOpt, objOpt, objOpt, objOpt);
            workbook.Close(objOpt, objOpt, objOpt);
            exc.Quit();
            workbook = null;
            sheets = null;
            workbooks = null;
            exc = null;
             */
            return true;
        }

        public static void KillProcess(string processName)
        {
            Process myproc = new Process(); 　　　　　　//得到所有打开的进程　　　　　　

            try
            {
                foreach (Process thisproc in Process.GetProcessesByName(processName))
                {
                    if (!thisproc.CloseMainWindow())
                    {
                        if (thisproc != null)
                            thisproc.Kill();
                    }
                }
            }
            catch (Exception Exc)
            {
            }

        }

    }
}