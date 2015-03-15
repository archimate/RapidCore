using System;
using System.Collections.Generic;
using System.Web;
using System.Text;

namespace ErpCoreModel.Framework
{
    /// <summary>
    ///Util 的摘要说明
    /// </summary>
    public class Util
    {
        public Util()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        //判断字符串是否是数字
        //作者:甘孝俭
        public static bool IsNum(string str)
        {
            str = str.Trim();
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
            for (int i = 0; i < str.Length; i++)
            {
                if ((str[i] < '0') || str[i] > '9')
                    return false;
            }
            return true;
        }

        //字符串加 1
        public static void AddOne(ref string code, int i)
        {
            code = code.ToUpper();
            if (code[i] >= '0' && code[i] < '9')
                ReplaceChar(ref code, i, Convert.ToChar((int)code[i] + 1));
            else if (code[i] == '9')
            {
                ReplaceChar(ref code, i, '0');
                if (i == 0)
                    code = "1" + code;
                else
                    AddOne(ref code, i - 1);
            }
            else if (code[i] >= 'A' && code[i] < 'Z')
                ReplaceChar(ref code, i, Convert.ToChar((int)code[i] + 1));
            else if (code[i] == 'Z')
            {
                ReplaceChar(ref code, i, 'A');
                if (i == 0)
                    code = "A" + code;
                else
                    AddOne(ref code, i - 1);
            }
        }
        //替换字符
        public static void ReplaceChar(ref string code, int i, char ch)
        {
            if (i == 0)
                code = ch + code.Substring(1);
            else if (i == code.Length - 1)
                code = code.Substring(0, i) + ch;
            else
                code = code.Substring(0, i) + ch + code.Substring(i + 1);
        }
    }
}