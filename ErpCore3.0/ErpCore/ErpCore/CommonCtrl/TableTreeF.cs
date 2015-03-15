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

namespace ErpCore.CommonCtrl
{
    public partial class TableTreeF : UserControl, IWindowCtrl
    {
        string captionText = "";
        bool showToolBar = false;
        bool showTitleBar = false;

        CFormControl formControl = null;

        public TableTreeF()
        {
            InitializeComponent();
        }

        public ControlType GetCtrlType()
        {
            return ControlType.TableTree;
        }
        public CFormControl FormControl
        {
            get { return formControl; }
            set { formControl = value; }
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

            treeView.ExpandAll();

        }
        void LoadTree()
        {
            if (FormControl == null)
                return;
            List<CBaseObject> lstObj = FormControl.TableInFormControlMgr.GetList();
            LoopTreeNode(0, lstObj,null);
        }
        void LoopTreeNode(int iLevel, List<CBaseObject> lstTIWC,TreeNode pnode)
        {
            if (iLevel >= lstTIWC.Count)
                return;
            CTableInFormControl tiwc = (CTableInFormControl)lstTIWC[iLevel];
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
                    node.Tag = obj;
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
        List<TreeNode> SelfLoop(CTableInFormControl tiwc, string sFilter, TreeNode pnode)
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
                node.Tag = obj;
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


        //界面公式取值
        public object GetSelectValue(string sColCode)
        {
            if (treeView.SelectedNode == null)
                return null;
            CBaseObject obj = (CBaseObject)treeView.SelectedNode.Tag;

            CColumn col = obj.Table.ColumnMgr.FindByCode(sColCode);
            if (col == null)
                return null;
            return obj.GetColValue(col);
        }
    }
}
