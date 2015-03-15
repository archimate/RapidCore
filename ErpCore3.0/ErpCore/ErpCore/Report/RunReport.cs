using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.UI;
using ErpCoreModel.Report;
using ErpCoreModel.SubSystem;

namespace ErpCore.Report
{
    public partial class RunReport : Form
    {
        public CReport m_Report = null;

        public RunReport()
        {
            InitializeComponent();
        }

        private void RunReport_Load(object sender, EventArgs e)
        {
            Run();
        }
        void Run()
        {
            if (m_Report == null)
                return;
            dataGridView.Columns.Clear();
            List<CStatItem> lstStatItem = new List<CStatItem>();
            List<CBaseObject> lstObj = m_Report.StatItemMgr.GetList();
            foreach (CBaseObject obj in lstObj)
            {
                lstStatItem.Add((CStatItem)obj);
            }
            lstStatItem.Sort();//按索引idx排序

            foreach (CStatItem StatItem in lstStatItem)
            {
                dataGridView.Columns.Add(StatItem.Name, StatItem.Name);
            }

            List<string> lstTable = new List<string>();
            string sFields = "";
            string sGroupBy = "";
            string sOrderBy = "";
            foreach (CStatItem StatItem in lstStatItem)
            {
                string sOrderFiled="";
                if (StatItem.ItemType == CStatItem.enumItemType.Field)
                {
                    CTable table = (CTable)Program.Ctx.TableMgr.Find(StatItem.FW_Table_id);
                    if (table == null)
                        continue;
                    CColumn column = (CColumn)table.ColumnMgr.Find(StatItem.FW_Column_id);
                    if (column == null)
                        continue;

                    if (!lstTable.Contains(table.Code))
                        lstTable.Add(table.Code);
                    if (StatItem.StatType == CStatItem.enumStatType.Val)
                    {
                        sFields += string.Format("[{0}].[{1}],", table.Code, column.Code);
                        sGroupBy += string.Format("[{0}].[{1}],", table.Code, column.Code);
                        sOrderFiled=string.Format("[{0}].[{1}]", table.Code, column.Code);
                    }
                    else
                    {
                        sFields += string.Format("{0}([{1}].[{2}]) as [{3}],",
                            StatItem.GetStatTypeFunc(), table.Code, column.Code, StatItem.Name);
                        sOrderFiled=StatItem.Name;
                    }
                }
                else
                {
                    sFields += string.Format("({0}) as [{1}],", StatItem.Formula, StatItem.Name);
                    sOrderFiled=StatItem.Name;
                }

                if (StatItem.Order == CStatItem.enumOrder.Asc)
                    sOrderBy += sOrderFiled + ",";
                else if (StatItem.Order == CStatItem.enumOrder.Desc)
                    sOrderBy += sOrderFiled + " desc,";
            }
            sFields = sFields.TrimEnd(",".ToCharArray());
            sGroupBy = sGroupBy.TrimEnd(",".ToCharArray());
            sOrderBy = sOrderBy.TrimEnd(",".ToCharArray());

            string sTable = "";
            foreach (string sTb in lstTable)
            {
                sTable += sTb + ",";
            }
            sTable = sTable.TrimEnd(",".ToCharArray());

            string sSql =string.Format("select {0} from {1} ",sFields,sTable);
            sSql += " where IsDeleted=0 ";
            if(m_Report.Filter.Trim()!="")
                sSql += " and "+m_Report.Filter;
            if (sGroupBy != "")
                sSql += " group by " + sGroupBy;
            if (sOrderBy != "")
                sSql += " order by " + sOrderBy;

            //因为采用构造sql语句的方法来运行报表，所以仅考虑单数据库的情况，
            //即取主数据库。如果考虑多数据库分布存储情况，则使用对象来计算报表。
            DataTable dt = Program.Ctx.MainDB.QueryT(sSql);
            if (dt == null)
            {
                MessageBox.Show("运行报表失败，请修改报表定义！");
                return;
            }

            foreach (DataRow r in dt.Rows)
            {
                dataGridView.Rows.Add(1);
                DataGridViewRow row = dataGridView.Rows[dataGridView.Rows.Count - 1];
                for (int col = 0; col< dataGridView.Columns.Count; col++)
                {
                    row.Cells[col].Value =r[col];
                }
            }
        }

        private void tbtExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
