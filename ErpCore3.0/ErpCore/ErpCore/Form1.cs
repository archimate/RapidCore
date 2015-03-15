using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using ErpCoreModel.Framework;
using ErpCoreModel.Base;

namespace ErpCore
{
    public partial class Form1 : Form
    {
        CBaseObjectMgr m_baseMgr = null;

        public delegate void myEventHandler(string userStatus);
        public void ChangeLabelText(string sText)
        {
            lbRec.Text = sText;
        } 

        public Form1()
        {
            InitializeComponent();

            string sPath = Application.StartupPath + "\\config.cfg";
            if (!File.Exists(sPath))
            {
                DbSetup frmDb = new DbSetup();
                if (frmDb.ShowDialog() != DialogResult.OK)
                {
                    Application.Exit();
                    return;
                }
            }
            string sConnectionString = ErpCore.Util.DESEncrypt.DesDecrypt(File.ReadAllText(sPath));
            Program.Ctx.ConnectionString = sConnectionString;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!m_bIsStart)
            {
                m_bIsStart = true;
                Thread thread = new Thread(new ThreadStart(trd));
                thread.Start();
            }
            else
            {
                m_bIsStart = false;
                timer1.Enabled = false;
            }
        }

        bool m_bIsStart = false;
        void trd()
        {
            timer1.Enabled = true;

            m_baseMgr = new CBaseObjectMgr();
            m_baseMgr.Ctx = Program.Ctx;
            m_baseMgr.TbCode = "test5";

            CTable tb = Program.Ctx.TableMgr.FindByCode("test5");
            CColumn col = tb.ColumnMgr.FindByCode("val");

            double dblCount = 0;
            while (m_bIsStart)
            {
                //for (int i = 0; i < 100; i++)
                //{
                    CBaseObject obj = new CBaseObject();
                    obj.Ctx = Program.Ctx;
                    obj.TbCode = "test5";
                    Random rand = new Random();
                    obj.SetColValue(col, rand.NextDouble() * 10);
                    m_baseMgr.AddNew(obj,true);

                    dblCount++;

                    this.Invoke(new myEventHandler(ChangeLabelText), new object[] { dblCount.ToString() });
                //}
                //m_baseMgr.Save(true);
            }
        }

        long m_ltime = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            m_ltime++;
            lbTime.Text = m_ltime.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            CTable tb = Program.Ctx.TableMgr.FindByCode("test5");
            CColumn col = tb.ColumnMgr.FindByCode("val");

            DateTime now1 = DateTime.Now;
            double dlbSum = 0;
            List<CBaseObject> lst = m_baseMgr.GetList();
            //foreach (CBaseObject obj in lst)
            //{
            //    //dlbSum += obj.m_arrNewVal["val"].DoubleVal;
            //}
            int idx = lst[0].GetColIdx("val");
            for (int i = 0; i < lst.Count;i++ )
            {
                CBaseObject obj = lst[i];
                dlbSum += obj.GetColValue2(idx).DoubleVal;
            }

            DateTime now2 = DateTime.Now;
            TimeSpan span = now2 - now1;

            string sText = string.Format("求和：{0} ,耗时：{1}",dlbSum,span.TotalSeconds );
            MessageBox.Show(sText);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DateTime now1 = DateTime.Now;
            string sSql = "select sum(val) from test5";
            object obj = Program.Ctx.MainDB.GetSingle(sSql);

            DateTime now2 = DateTime.Now;
            TimeSpan span = now2 - now1;

            string sText = string.Format("求和：{0} ,耗时：{1}", obj.ToString(), span.TotalSeconds);
            MessageBox.Show(sText);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            m_baseMgr = new CBaseObjectMgr();
            m_baseMgr.Ctx = Program.Ctx;
            m_baseMgr.TbCode = "test5";
            m_baseMgr.GetList();
            MessageBox.Show(m_baseMgr.GetList().Count.ToString());

        }

        private void button5_Click(object sender, EventArgs e)
        {
            DateTime now1 = DateTime.Now;
            List<CBaseObject> lst = new List<CBaseObject>();
            for (int i = 0; i < 1000000; i++)
            {
                CUser user = new CUser();
                lst.Add(user);
            }
            DateTime now2 = DateTime.Now;
            TimeSpan span0 = now2 - now1;
            string sText = string.Format("装载耗时：{0} ", span0.TotalSeconds);
            MessageBox.Show(sText);

            now1 = DateTime.Now;
            for (int i = 0; i < lst.Count; i++)
            {
                CBaseObject obj = lst[i];
            }
            now2 = DateTime.Now;
            TimeSpan span = now2 - now1;

            now1 = DateTime.Now;
            for (int i = 0; i < lst.Count; i++)
            {
                CUser obj = (CUser)lst[i];
            }
            now2 = DateTime.Now;
            TimeSpan span2 = now2 - now1;

            sText = string.Format("CBaseObject：{0} ,CUser：{1}", span.TotalSeconds, span2.TotalSeconds);
            MessageBox.Show(sText);
        }
    }
}
