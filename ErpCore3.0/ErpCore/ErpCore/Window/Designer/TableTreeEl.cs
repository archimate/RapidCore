using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;

using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.UI;
using ErpCore.Window;

namespace ErpCore.Window.Designer
{
    public partial class TableTreeEl : UserControl, IDesignEl
    {
        string captionText = "";
        bool showTitleBar = false;

        CWindowControl windowControl = null;

        public TableTreeEl()
        {
            InitializeComponent();
        }

        public ControlType GetCtrlType()
        {
            return ControlType.TableTree;
        }
        public void OnEdit()
        {
        }

        public CWindowControl WindowControl
        {
            get { return windowControl; }
            set { windowControl = value; }
        }

        public string CaptionText
        {
            get { return captionText; }
            set
            {
                captionText = value;
                lbTitle.Text = captionText;
            }
        }

        public bool ShowTitleBar
        {
            get { return showTitleBar; }
            set
            {
                showTitleBar = value;
                tbTitle.Visible = showTitleBar;
            }
        }
        private void TableCtrlEl_Load(object sender, EventArgs e)
        {
            LoadTree();
        }
        void LoadTree()
        {
            if (WindowControl == null)
                return;
            List<CBaseObject> lstObj = WindowControl.TableInWindowControlMgr.GetList();
            LoopTreeNode(0, lstObj,null);
        }
        void LoopTreeNode(int iLevel, List<CBaseObject> lstTIWC,TreeNode pnode)
        {
            if (iLevel >= lstTIWC.Count)
                return;
            CTableInWindowControl tiwc = (CTableInWindowControl)lstTIWC[iLevel];
            CTable table = (CTable)Program.Ctx.TableMgr.Find(tiwc.FW_Table_id);
            if (table == null)
                return;
            if (!tiwc.IsLoop)
            {
                CBaseObjectMgr BaseObjectMgr = new CBaseObjectMgr();
                BaseObjectMgr.Ctx = Program.Ctx;
                BaseObjectMgr.TbCode = table.Code;
                List<CBaseObject> lstObj = BaseObjectMgr.GetList(tiwc.QueryFilter);
                foreach (CBaseObject obj in lstObj)
                {
                    string sText = tiwc.Text.ToLower();
                    int iStart = 0;
                    while (sText.IndexOf('[',iStart) > -1)
                    {
                        int idx1 = sText.IndexOf('[',iStart);
                        int idx2 = sText.IndexOf(']', idx1);
                        iStart = idx1+1;
                        if (idx2 > idx1)
                        {
                            string sCodeN = sText.Substring(idx1, idx2 - idx1 + 1);
                            string sCode = sCodeN.Substring(1, sCodeN.Length - 2);
                            CColumn column = table.ColumnMgr.FindByCode(sCode);
                            if(column==null)
                                continue;
                            string sVal = obj.GetColValue(column).ToString();
                            sText = sText.Replace(sCodeN, sVal);
                        }
                    }

                    TreeNode node = new TreeNode();
                    node.Text = sText;
                    if (pnode == null)
                        treeView.Nodes.Add(node);
                    else
                        pnode.Nodes.Add(node);

                    LoopTreeNode(iLevel + 1, lstTIWC, node);
                }
            }
            else
            {
                string sFilter = tiwc.QueryFilter;
                if (sFilter.Trim() != "")
                    sFilter += " and ";
                sFilter += tiwc.RootFilter;
                List<TreeNode> lstTreeNode = SelfLoop(tiwc, sFilter, pnode);
                foreach (TreeNode node in lstTreeNode)
                {
                    LoopTreeNode(iLevel + 1, lstTIWC, node);
                }
            }
        }
        //生成自引用树节点
        List<TreeNode> SelfLoop(CTableInWindowControl tiwc,string sFilter, TreeNode pnode)
        {
            List<TreeNode> lstTreeNode = new List<TreeNode>();

            CTable table = (CTable)Program.Ctx.TableMgr.Find(tiwc.FW_Table_id);
            if (table == null)
                return lstTreeNode;
            CBaseObjectMgr BaseObjectMgr = new CBaseObjectMgr();
            BaseObjectMgr.Ctx = Program.Ctx;
            BaseObjectMgr.TbCode = table.Code;
            List<CBaseObject> lstObj = BaseObjectMgr.GetList(sFilter);
            foreach (CBaseObject obj in lstObj)
            {
                string sText = tiwc.Text.ToLower();
                int iStart = 0;
                while (sText.IndexOf('[', iStart) > -1)
                {
                    int idx1 = sText.IndexOf('[', iStart);
                    int idx2 = sText.IndexOf(']', idx1);
                    iStart = idx1 + 1;
                    if (idx2 > idx1)
                    {
                        string sCodeN = sText.Substring(idx1, idx2 - idx1 + 1);
                        string sCode = sCodeN.Substring(1, sCodeN.Length - 2);
                        CColumn column = table.ColumnMgr.FindByCode(sCode);
                        if (column == null)
                            continue;
                        string sVal = obj.GetColValue(column).ToString();
                        sText = sText.Replace(sCodeN, sVal);
                    }
                }

                TreeNode node = new TreeNode();
                node.Text = sText;
                if (pnode == null)
                    treeView.Nodes.Add(node);
                else
                    pnode.Nodes.Add(node);

                CColumn col = (CColumn)table.ColumnMgr.Find(tiwc.NodeIDCol);
                CColumn pcol = (CColumn)table.ColumnMgr.Find(tiwc.PNodeIDCol);
                string sVal2 = obj.GetColValue(col).ToString();
                if (col.ColType == ColumnType.string_type
                    || col.ColType == ColumnType.ref_type
                    || col.ColType == ColumnType.guid_type
                    || col.ColType == ColumnType.datetime_type
                    || col.ColType == ColumnType.text_type)
                    sVal2 = "'" + sVal2 + "'";
                string sSubFilter2 = string.Format(" {0}={1}", pcol.Code, sVal2);

                string sFilter2 = tiwc.QueryFilter;
                if (sFilter2.Trim() != "")
                    sFilter2 += " and ";
                sFilter2 += sSubFilter2;
                SelfLoop(tiwc, sFilter2, node);

                lstTreeNode.Add(node);
            }

            return lstTreeNode;
        }


        private void MenuItemBringToFront_Click(object sender, EventArgs e)
        {
            this.BringToFront();

        }

        private void MenuItemSendToBack_Click(object sender, EventArgs e)
        {
            this.SendToBack();

        }

        public void MenuItemDelete_Click(object sender, EventArgs e)
        {
            CWindowControl WindowControl = (CWindowControl)this.Tag;
            if (WindowControl == null)
                return;
            WindowControl.m_ObjectMgr.Delete(WindowControl);
            this.Parent.Controls.Remove(this);

        }

        private void tbTitle_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point pt = this.PointToScreen(e.Location);
                contextMenu.Show(pt.X, pt.Y);
            }
        }

    }
}
