using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using System.IO;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;

public partial class Window_Edit : System.Web.UI.Page
{
    public CTable m_Table = null;
    public CBaseObjectMgr m_BaseObjectMgr = null;
    public CBaseObject m_BaseObject = null;
    public Guid m_guidParentId = Guid.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        string tid = Request["tid"];
        if (string.IsNullOrEmpty(tid))
        {
            Response.Write("请选择表！");
            Response.End();
        }
        m_Table = (CTable)Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.Find(new Guid(tid));
        string id = Request["id"];
        if (string.IsNullOrEmpty(id))
        {
            Response.Write("请选择记录！");
            Response.End();
        }
        string ParentId = Request["ParentId"];
        if (!string.IsNullOrEmpty(ParentId))
            m_guidParentId = new Guid(ParentId);
        

        m_BaseObjectMgr =Global.GetCtx(Session["TopCompany"].ToString()).FindBaseObjectMgrCache(m_Table.Code, m_guidParentId);
        if (m_BaseObjectMgr == null)
        {
            m_BaseObjectMgr = new CBaseObjectMgr();
            m_BaseObjectMgr.TbCode = m_Table.Code;
            m_BaseObjectMgr.Ctx = Global.GetCtx(Session["TopCompany"].ToString());
            string sWhere = string.Format(" id='{0}'", id);
            m_BaseObjectMgr.GetList(sWhere);
        }
        m_BaseObject = m_BaseObjectMgr.Find(new Guid(id));
        if (m_BaseObject == null)
        {
            Response.Write("请选择记录！");
            Response.End();
        }

        //保存到编辑对象
        EditObject.Add(Session.SessionID, m_BaseObject);

        if (!IsPostBack)
        {
            recordCtrl.m_Table = m_Table;
            //recordCtrl.m_sortRestrictColumnAccessType = m_sortRestrictColumnAccessType;
            recordCtrl.m_BaseObject = m_BaseObject;
        }
        if (Request.Params["Action"] == "Cancel")
        {
            m_BaseObject.Cancel();
            Response.End();
        }
        else if (Request.Params["Action"] == "PostData")
        {
            PostData();
            //从编辑对象移除
            EditObject.Remove(Session.SessionID, m_BaseObject);

            Response.End();
        }
    }
    void PostData()
    {
        if (!ValidateData())
            return;

        List<CBaseObject> lstCol = m_Table.ColumnMgr.GetList();
        bool bHasVisible = false;
        foreach (CBaseObject obj in lstCol)
        {
            CColumn col = (CColumn)obj;

            if (col.Code.Equals("id", StringComparison.OrdinalIgnoreCase))
                continue;
            else if (col.Code.Equals("Created", StringComparison.OrdinalIgnoreCase))
                continue;
            else if (col.Code.Equals("Creator", StringComparison.OrdinalIgnoreCase))
            {
                //BaseObject.SetColValue(col, Program.User.Id);
                continue;
            }
            else if (col.Code.Equals("Updated", StringComparison.OrdinalIgnoreCase))
            {
                m_BaseObject.SetColValue(col, DateTime.Now);
                continue;
            }
            else if (col.Code.Equals("Updator", StringComparison.OrdinalIgnoreCase))
            {
                CUser user = (CUser)Session["User"];
                m_BaseObject.SetColValue(col, user.Id);
                continue;
            }

            if (col.ColType == ColumnType.object_type)
            {
                HttpPostedFile postfile = Request.Files.Get("_" + col.Code);
                if (postfile != null && postfile.ContentLength > 0)
                {
                    string sFileName = postfile.FileName;
                    if (sFileName.LastIndexOf('\\') > -1)//有些浏览器不带路径
                        sFileName = sFileName.Substring(sFileName.LastIndexOf('\\'));

                    byte[] byteFileName = System.Text.Encoding.Default.GetBytes(sFileName);
                    byte[] byteValue = new byte[254 + postfile.ContentLength];
                    byte[] byteData = new byte[postfile.ContentLength];
                    postfile.InputStream.Read(byteData, 0, postfile.ContentLength);

                    Array.Copy(byteFileName, byteValue, byteFileName.Length);
                    Array.Copy(byteData, 0, byteValue, 254, byteData.Length);

                    m_BaseObject.SetColValue(col, byteValue);
                }
            }
            else if (col.ColType == ColumnType.path_type)
            {
                string sUploadPath = col.UploadPath;
                if (sUploadPath[sUploadPath.Length - 1] != '\\')
                    sUploadPath += "\\";
                if (!Directory.Exists(sUploadPath))
                    Directory.CreateDirectory(sUploadPath);

                HttpPostedFile postfile = Request.Files.Get("_" + col.Code);
                if (postfile != null && postfile.ContentLength > 0)
                {
                    string sFileName = postfile.FileName;
                    if (sFileName.LastIndexOf('\\') > -1)//有些浏览器不带路径
                        sFileName = sFileName.Substring(sFileName.LastIndexOf('\\'));

                    FileInfo fi = new FileInfo(sUploadPath + sFileName);
                    Guid guid = Guid.NewGuid();
                    string sDestFile = string.Format("{0}{1}", guid.ToString().Replace("-", ""), fi.Extension);
                    postfile.SaveAs(sUploadPath + sDestFile);

                    string sVal = string.Format("{0}|{1}", sDestFile, sFileName);
                    m_BaseObject.SetColValue(col, sVal);
                }
            }
            else if (col.ColType == ColumnType.bool_type)
            {
                string val = Request.Params["_" + col.Code];
                if (!string.IsNullOrEmpty(val) && val.ToLower() == "on")
                    m_BaseObject.SetColValue(col, true);
                else
                    m_BaseObject.SetColValue(col, false);
            }
            else if (col.ColType == ColumnType.datetime_type)
            {
                string val = Request.Params["_" + col.Code];
                if (!string.IsNullOrEmpty(val))
                    m_BaseObject.SetColValue(col, Convert.ToDateTime(val));
            }
            else
                m_BaseObject.SetColValue(col, Request.Params["_" + col.Code]);
            bHasVisible = true;
        }
        if (!bHasVisible)
        {
            //Response.Write("没有可修改字段！");
            Response.Write("<script>parent.$.ligerDialog.warn('没有可修改字段！');</script>");
            return ;
        }
        if (!m_BaseObjectMgr.Update(m_BaseObject, true))
        {
            //Response.Write("修改失败！");
            Response.Write("<script>parent.$.ligerDialog.warn('修改失败！');</script>");
            return ;
        }
        //在iframe里访问外面,需要parent.parent.
        //Response.Write("<script>parent.parent.grid.loadData(true);parent.parent.$.ligerDialog.close();</script>");
        Response.Write("<script>parent.parent.onOkEdit();</script>");
    }
    //验证数据
    bool ValidateData()
    {
        List<CBaseObject> lstCol = m_Table.ColumnMgr.GetList();
        foreach (CBaseObject obj in lstCol)
        {
            CColumn col = (CColumn)obj;

            if (col.Code.Equals("id", StringComparison.OrdinalIgnoreCase))
                continue;
            else if (col.Code.Equals("Created", StringComparison.OrdinalIgnoreCase))
                continue;
            else if (col.Code.Equals("Creator", StringComparison.OrdinalIgnoreCase))
                continue;
            else if (col.Code.Equals("Updated", StringComparison.OrdinalIgnoreCase))
                continue;
            else if (col.Code.Equals("Updator", StringComparison.OrdinalIgnoreCase))
                continue;

            string val = Request.Params["_" + col.Code];
            if (!col.AllowNull && string.IsNullOrEmpty(val))
            {
                Response.Write(string.Format("<script>parent.$.ligerDialog.warn('{0}不允许空！');</script>", col.Name));
                return false;
            }

            if (col.ColType == ColumnType.string_type)
            {
                if (val.Length > col.ColLen)
                {
                    Response.Write(string.Format("<script>parent.$.ligerDialog.warn('{0}长度不能超过{1}！');</script>", col.Name, col.ColLen));
                    return false;
                }
            }
            else if (col.ColType == ColumnType.datetime_type)
            {
                if (!string.IsNullOrEmpty(val))
                {
                    try { Convert.ToDateTime(val); }
                    catch
                    {
                        Response.Write(string.Format("<script>parent.$.ligerDialog.warn('{0}日期格式错误！');</script>", col.Name));
                        return false;
                    }
                }
            }
            else if (col.ColType == ColumnType.int_type
                || col.ColType == ColumnType.long_type)
            {
                if (!Util.IsInt(val))
                {
                    Response.Write(string.Format("<script>parent.$.ligerDialog.warn('{0}为整型数字！');</script>", col.Name));
                    return false;
                }
            }
            else if (col.ColType == ColumnType.numeric_type)
            {
                if (!Util.IsNum(val))
                {
                    Response.Write(string.Format("<script>parent.$.ligerDialog.warn('{0}为数字！');</script>", col.Name));
                    return false;
                }
            }
            else if (col.ColType == ColumnType.guid_type
            || col.ColType == ColumnType.ref_type)
            {
                if (!string.IsNullOrEmpty(val))
                {
                    try { Guid guid = new Guid(val); }
                    catch
                    {
                        Response.Write(string.Format("<script>parent.$.ligerDialog.warn('{0}为GUID格式！');</script>", col.Name));
                        return false;
                    }
                }
            }

        }
        return true;
    }
}
