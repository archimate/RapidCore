using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCore.Desktop;
using ErpCoreModel.Store;
using ErpCoreModel.Invoicing;

namespace ErpCore
{
    static class Program
    {
        //保存上下文，如果是web，考虑用Session
        public static CContext Ctx = new CContext();
        //登录用户
        public static CUser User = new CUser();
        //在线商店
        public static CStore Store = new CStore();
        //进销存
        public static CInvoicing Invoicing = new CInvoicing();
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //设置默认附件上传路径
            Ctx.UploadPath = Application.StartupPath + "\\UploadPath\\";

            Store.Ctx = Ctx;
            Invoicing.Ctx = Ctx;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new UpdateForm());
            Application.Run(new DesktopPanel());
            //Application.Run(new AdminForm());
            //Application.Run(new Form1());
        }
    }
}
