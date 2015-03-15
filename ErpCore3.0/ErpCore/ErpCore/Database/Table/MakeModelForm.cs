using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using System.IO;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCore.Window;

namespace ErpCore.Database.Table
{
    public partial class MakeModelForm : Form
    {
        public List<CTable> m_lstTable = new List<CTable>();

        public MakeModelForm()
        {
            InitializeComponent();
        }

        private void MakeModelForm_Load(object sender, EventArgs e)
        {
            LoadTable();
            txtNameSpace.Text = "ErpCoreModel.SubSystem";
            foreach (CTable table in m_lstTable)
            {
                if (table.Code.IndexOf("_") > 0)
                {
                    string sName = table.Code.Substring(0, table.Code.IndexOf("_"));
                    txtNameSpace.Text = "ErpCoreModel." + sName;
                    listPrefix.Items.Add(sName+"_");
                    break;
                }
            }
            MakeModel();
        }
        void LoadTable()
        {
            listTable.Items.Clear();
            foreach (CTable table in m_lstTable)
            {
                listTable.Items.Add(table.Code);
            }
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            AddPrefixForm frm = new AddPrefixForm();
            if (frm.ShowDialog() != DialogResult.OK)
                return;
            string sPrefix = frm.txtPrefix.Text.Trim();
            for (int i = 0; i < listPrefix.Items.Count; i++)
            {
                if (listPrefix.Items[i].ToString().Equals(sPrefix, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("前缀已经存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            listPrefix.Items.Add(sPrefix);

            MakeModel();
        }

        private void btDel_Click(object sender, EventArgs e)
        {
            if (listPrefix.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选择一项！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            for (int i = listPrefix.SelectedItems.Count - 1; i >= 0; i--)
            {
                listPrefix.Items.Remove(listPrefix.SelectedItems[i]);
            }

            MakeModel();
        }

        void MakeModel()
        {
            MakeModel(false,"");
        }
        void MakeModel(bool bMakeFile,string sDir)
        {
            listClass.Items.Clear();
            foreach (CTable table in m_lstTable)
            {
                string sCode = table.Code;
                for (int i = 0; i < listPrefix.Items.Count; i++)
                {
                    string pre = listPrefix.Items[i].ToString();
                    if (sCode.Length > pre.Length)
                    {
                        if (sCode.Substring(0, pre.Length).Equals(pre, StringComparison.OrdinalIgnoreCase))
                        {
                            sCode = sCode.Substring(pre.Length);
                        }
                    }
                    //sCode = sCode.TrimStart(listPrefix.Items[i].ToString().ToCharArray());
                    
                }
                string sClsName = "C" + sCode;
                listClass.Items.Add(sClsName+".cs");
                if (bMakeFile)
                {
                    string sFile = sDir + sClsName + ".cs";
                    File.Delete(sFile);
                    string sAttr = MakeAttrContent(table);

                    string sContent = string.Format(sTemplete, sClsName, DateTime.Now.ToString(), txtNameSpace.Text.Trim(), table.Code, sAttr);
                    File.WriteAllText(sFile, sContent);
                }
                string sClsMgrName =sClsName+ "Mgr";
                listClass.Items.Add(sClsMgrName + ".cs");
                if (bMakeFile)
                {
                    string sFile = sDir + sClsMgrName + ".cs";
                    File.Delete(sFile);
                    string sContent = string.Format(sTempleteMgr, sClsName, DateTime.Now.ToString(), txtNameSpace.Text.Trim(), table.Code);
                    File.WriteAllText(sFile, sContent);
                }
            }
        }

        string MakeAttrContent(CTable table)
        {
            string sRet = "";
            List<CBaseObject> lstObj = table.ColumnMgr.GetList();
            foreach (CBaseObject obj in lstObj)
            {
                CColumn col = (CColumn)obj;
                if (col.Code.Equals("id", StringComparison.OrdinalIgnoreCase))
                    continue;
                else if (col.Code.Equals("created", StringComparison.OrdinalIgnoreCase))
                    continue;
                else if (col.Code.Equals("creator", StringComparison.OrdinalIgnoreCase))
                    continue;
                else if (col.Code.Equals("updated", StringComparison.OrdinalIgnoreCase))
                    continue;
                else if (col.Code.Equals("updator", StringComparison.OrdinalIgnoreCase))
                    continue;

                string F = col.Code[0].ToString();
                string sAttrName = F.ToUpper()+col.Code.Substring(1);
                string sAttrKey = col.Code.ToLower();

                switch (col.ColType)
                {
                    case ColumnType.guid_type:
                    case ColumnType.ref_type:
                        sRet += string.Format(sTAttr2, sAttrName, sAttrKey);
                        break;
                    case ColumnType.string_type:
                    case ColumnType.text_type:
                    case ColumnType.path_type:
                        sRet += string.Format(sTAttr1, sAttrName, sAttrKey);
                        break;
                    case ColumnType.enum_type:
                        {
                            if (col.RefShowCol != Guid.Empty)
                                sRet += string.Format(sTAttr2, sAttrName, sAttrKey);
                            else
                                sRet += string.Format(sTAttr1, sAttrName, sAttrKey);
                            break;
                        }
                    case ColumnType.int_type:
                        sRet += string.Format(sTAttr3, sAttrName, sAttrKey);
                        break;
                    case ColumnType.long_type:
                        sRet += string.Format(sTAttr4, sAttrName, sAttrKey);
                        break;
                    case ColumnType.numeric_type:
                        sRet += string.Format(sTAttr5, sAttrName, sAttrKey);
                        break;
                    case ColumnType.datetime_type:
                        sRet += string.Format(sTAttr7, sAttrName, sAttrKey);
                        break;
                    case ColumnType.bool_type:
                        sRet += string.Format(sTAttr6, sAttrName, sAttrKey);
                        break;
                    case ColumnType.object_type:
                        sRet += string.Format(sTAttr8, sAttrName, sAttrKey);
                        break;
                }
            }
            return sRet;
        }

        private void btBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() != DialogResult.OK)
                return;
            txtPath.Text = dlg.SelectedPath;
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            if (txtPath.Text.Trim() == "")
            {
                MessageBox.Show("请选择生成路径！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string sDir = txtPath.Text.Trim();
            if (sDir[sDir.Length - 1] != '\\')
                sDir += "\\";
            try
            {
                if (!Directory.Exists(sDir))
                    Directory.CreateDirectory(sDir);
            }
            catch
            {
                MessageBox.Show("创建路径失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MakeModel(true,sDir);

            MessageBox.Show("生成完毕！");
            System.Diagnostics.Process.Start("explorer.exe ", sDir);
        }



        string sTemplete = @"// File:    {0}.cs
// Author:  甘孝俭
// email:   ganxiaojian@hotmail.com 
// QQ:      154986287
// http://www.8088net.com
// 协议声明：本软件为开源系统，遵循国际开源组织协议。任何单位或个人可以使用或修改本软件源码，
//          可以用于作为非商业或商业用途，但由于使用本源码所引起的一切后果与作者无关。
//          未经作者许可，禁止任何企业或个人直接出售本源码或者把本软件作为独立的功能进行销售活动，
//          作者将保留追究责任的权利。
// Created: {1}
// Purpose: Definition of Class {0}

using System;
using System.Text;
using ErpCoreModel.Framework;

namespace {2}
{{
    
    public class {0} : CBaseObject
    {{

        public {0}()
        {{
            TbCode = ""{3}"";
            ClassName = ""{2}.{0}"";

        }}

        {4}
    }}
}}";
        string sTempleteMgr = @"// File:    {0}Mgr.cs
// Author:  甘孝俭
// email:   ganxiaojian@hotmail.com 
// QQ:      154986287
// http://www.8088net.com
// 协议声明：本软件为开源系统，遵循国际开源组织协议。任何单位或个人可以使用或修改本软件源码，
//          可以用于作为非商业或商业用途，但由于使用本源码所引起的一切后果与作者无关。
//          未经作者许可，禁止任何企业或个人直接出售本源码或者把本软件作为独立的功能进行销售活动，
//          作者将保留追究责任的权利。
// Created: {1}
// Purpose: Definition of Class {0}Mgr

using System;
using System.Text;
using System.Collections.Generic;
using System.Reflection;
using ErpCoreModel.Framework;

namespace {2}
{{
    public class {0}Mgr : CBaseObjectMgr
    {{

        public {0}Mgr()
        {{
            TbCode = ""{3}"";
            ClassName = ""{2}.{0}"";
            Assembly assembly = Assembly.LoadFrom(Assembly.GetExecutingAssembly().Location);
            ObjType = assembly.GetType(ClassName);
        }}

    }}
}}";

        string sTAttr1 = @"
        public string {0}
        {{
            get
            {{
                if (m_arrNewVal.ContainsKey(""{1}""))
                    return m_arrNewVal[""{1}""].StrVal;
                else
                    return """";
            }}
            set
            {{
                if (m_arrNewVal.ContainsKey(""{1}""))
                    m_arrNewVal[""{1}""].StrVal = value;
                else
                {{
                    CValue val = new CValue();
                    val.StrVal = value;
                    m_arrNewVal.Add(""{1}"", val);
                }}
            }}
        }}";
        string sTAttr2 = @"
        public Guid {0}
        {{
            get
            {{
                if (m_arrNewVal.ContainsKey(""{1}""))
                    return m_arrNewVal[""{1}""].GuidVal;
                else
                    return Guid.Empty;
            }}
            set
            {{        
                if (m_arrNewVal.ContainsKey(""{1}""))
                    m_arrNewVal[""{1}""].GuidVal = value;
                else
                {{
                    CValue val = new CValue();
                    val.GuidVal = value;
                    m_arrNewVal.Add(""{1}"", val);
                }}
            }}
        }}";
        string sTAttr3 = @"
        public int {0}
        {{
            get
            {{
                if (m_arrNewVal.ContainsKey(""{1}""))
                    return m_arrNewVal[""{1}""].IntVal;
                else
                    return 0;
            }}
            set
            {{           
                if (m_arrNewVal.ContainsKey(""{1}""))
                    m_arrNewVal[""{1}""].IntVal = value;
                else
                {{
                    CValue val = new CValue();
                    val.IntVal = value;
                    m_arrNewVal.Add(""{1}"", val);
                }}
            }}
        }}";
        string sTAttr4 = @"
        public long {0}
        {{
            get
            {{
                if (m_arrNewVal.ContainsKey(""{1}""))
                    return m_arrNewVal[""{1}""].LongVal;
                else
                    return 0;
            }}
            set
            {{
                if (m_arrNewVal.ContainsKey(""{1}""))
                    m_arrNewVal[""{1}""].LongVal = value;
                else
                {{
                    CValue val = new CValue();
                    val.LongVal = value;
                    m_arrNewVal.Add(""{1}"", val);
                }}
            }}
        }}";
        string sTAttr5 = @"
        public double {0}
        {{
            get
            {{
                if (m_arrNewVal.ContainsKey(""{1}""))
                    return m_arrNewVal[""{1}""].DoubleVal;
                else
                    return 0;
            }}
            set
            {{
                if (m_arrNewVal.ContainsKey(""{1}""))
                    m_arrNewVal[""{1}""].DoubleVal = value;
                else
                {{
                    CValue val = new CValue();
                    val.DoubleVal = value;
                    m_arrNewVal.Add(""{1}"", val);
                }}
            }}
        }}";
        string sTAttr6 = @"
        public bool {0}
        {{
            get
            {{
                if (m_arrNewVal.ContainsKey(""{1}""))
                    return m_arrNewVal[""{1}""].BoolVal;
                else
                    return false;
            }}
            set
            {{
                if (m_arrNewVal.ContainsKey(""{1}""))
                    m_arrNewVal[""{1}""].BoolVal = value;
                else
                {{
                    CValue val = new CValue();
                    val.BoolVal = value;
                    m_arrNewVal.Add(""{1}"", val);
                }}
            }}
        }}";
        string sTAttr7 = @"
        public DateTime {0}
        {{
            get
            {{
                if (m_arrNewVal.ContainsKey(""{1}""))
                    return m_arrNewVal[""{1}""].DatetimeVal;
                else
                    return DateTime.Now;
            }}
            set
            {{
                if (m_arrNewVal.ContainsKey(""{1}""))
                    m_arrNewVal[""{1}""].DatetimeVal = value;
                else
                {{
                    CValue val = new CValue();
                    val.DatetimeVal = value;
                    m_arrNewVal.Add(""{1}"", val);
                }}
            }}
        }}";
        string sTAttr8 = @"
        public object {0}
        {{
            get
            {{
                if (m_arrNewVal.ContainsKey(""{1}""))
                    return m_arrNewVal[""{1}""].ObjectVal;
                else
                    return null;
            }}
            set
            {{
                if (m_arrNewVal.ContainsKey(""{1}""))
                    m_arrNewVal[""{1}""].ObjectVal = value;
                else
                {{
                    CValue val = new CValue();
                    val.ObjectVal = value;
                    m_arrNewVal.Add(""{1}"", val);
                }}
            }}
        }}";
    }
}
