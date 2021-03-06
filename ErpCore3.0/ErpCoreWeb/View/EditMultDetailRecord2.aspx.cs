﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using System.IO;
using System.Linq;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.UI;

public partial class View_EditMultDetailRecord2 : System.Web.UI.Page
{
    public CUser m_User = null;
    public CTable m_Table = null;
    public CView m_View = null;
    public CViewDetail m_ViewDetail = null;
    public CBaseObjectMgr m_BaseObjectMgr = null;
    public CBaseObject m_BaseObject = null;
    //受限的字段：禁止或者只读权限
    public SortedList<Guid, AccessType> m_sortRestrictColumnAccessType = new SortedList<Guid, AccessType>();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null)
        {
            Response.End();
        }
        m_User = (CUser)Session["User"];

        string vid = Request["vid"];
        if (string.IsNullOrEmpty(vid))
        {
            Response.End();
        }
        string vdid = Request["vdid"];
        if (string.IsNullOrEmpty(vdid))
        {
            Response.End();
        }
        m_View = (CView)Global.GetCtx(Session["TopCompany"].ToString()).ViewMgr.Find(new Guid(vid));
        if (m_View == null)
        {
            Response.End();
        }
        m_ViewDetail = (CViewDetail)m_View.ViewDetailMgr.Find(new Guid(vdid));
        if (m_ViewDetail == null)
        {
            Response.End();
        }
        m_Table = (CTable)Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.Find(m_ViewDetail.FW_Table_id);

        m_sortRestrictColumnAccessType = m_User.GetRestrictColumnAccessTypeList(m_Table);

        if (Session["EditMultMasterDetailViewRecord"] == null)
        {
            Response.End();
        }
        SortedList<Guid, CBaseObject> arrP = (SortedList<Guid, CBaseObject>)Session["EditMultMasterDetailViewRecord"];
        CBaseObject objP = (CBaseObject)arrP.Values[0];
        m_BaseObjectMgr = objP.GetSubObjectMgr(m_Table.Code, typeof(CBaseObjectMgr));

        string id = Request["id"];
        if (string.IsNullOrEmpty(id))
        {
            Response.End();
        }

        m_BaseObject = m_BaseObjectMgr.Find(new Guid(id));
        if (m_BaseObject == null)
        {
            Response.Write("请选择记录！");
            Response.End();
        }
        
        if (!IsPostBack)
        {
            recordCtrl.m_ViewDetail = m_ViewDetail;
            recordCtrl.m_Table = m_Table;
            recordCtrl.m_sortRestrictColumnAccessType = m_sortRestrictColumnAccessType;
            recordCtrl.m_BaseObject = m_BaseObject;
            if (!string.IsNullOrEmpty(Request["UIColCount"]))
                recordCtrl.m_iUIColCount = Convert.ToInt32(Request["UIColCount"]);
            //隐藏字段
            string sHideCols = Request["HideCols"];
            if (!string.IsNullOrEmpty(sHideCols))
            {
                string[] arr = sHideCols.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                foreach (string code in arr)
                {
                    recordCtrl.m_sortHideColumn.Add(code, code);
                }
            }
        }
        if (Request.Params["Action"] == "Cancel")
        {
            Response.End();
        }
        else if (Request.Params["Action"] == "PostData")
        {
            PostData();
            Response.End();
        }
    }

    void PostData()
    {
        if (!ValidateData())
            return;

        bool bHasVisible = false;
        //foreach (CBaseObject objCIVD in m_ViewDetail.ColumnInViewDetailMgr.GetList())
        foreach (CBaseObject objCol in m_Table.ColumnMgr.GetList())
        {
            //CColumnInViewDetail civd = (CColumnInViewDetail)objCIVD;

            //CColumn col = (CColumn)m_Table.ColumnMgr.Find(civd.FW_Column_id);
            CColumn col = (CColumn)objCol;
            if (col == null)
                continue;
            //判断禁止和只读权限字段
            if (m_sortRestrictColumnAccessType.ContainsKey(col.Id))
            {
                AccessType accessType = m_sortRestrictColumnAccessType[col.Id];
                if (accessType == AccessType.forbide)
                    continue;
                //只读只在界面控制,有些默认值需要只读也需要保存数据
                //else if (accessType == AccessType.read)
                //    continue;
            }
            //

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
                CUser u = (CUser)Session["User"];
                m_BaseObject.SetColValue(col, u.Id);
                continue;
            }

            if (col.ColType == ColumnType.object_type)
            {
                string ckVal = Request.Params["ckClear_" + col.Code];
                if (!string.IsNullOrEmpty(ckVal) && ckVal.ToLower() == "on")
                {
                    //清空附件
                    m_BaseObject.SetColValue(col, null);
                }
                else
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
            }
            else if (col.ColType == ColumnType.path_type)
            {
                string sUploadPath = col.UploadPath;
                if (sUploadPath[sUploadPath.Length - 1] != '\\')
                    sUploadPath += "\\";
                if (!Directory.Exists(sUploadPath))
                    Directory.CreateDirectory(sUploadPath);
                
                string ckVal = Request.Params["ckClear_" + col.Code];
                if (!string.IsNullOrEmpty(ckVal) && ckVal.ToLower() == "on")
                {
                    //清空附件
                    m_BaseObject.SetColValue(col, "");
                }
                else
                {
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
            Response.Write("<script>alert('没有可修改字段！');</script>");
            return ;
        }

        SortedList<Guid, CBaseObject> arrP = (SortedList<Guid, CBaseObject>)Session["EditMultMasterDetailViewRecord"];
        CBaseObject objP = (CBaseObject)arrP.Values[0];
        CColumn colP = (CColumn)objP.Table.ColumnMgr.Find(m_ViewDetail.PrimaryKey);
        CColumn colF = (CColumn)m_Table.ColumnMgr.Find(m_ViewDetail.ForeignKey);
        m_BaseObject.SetColValue(colF, objP.GetColValue(colP));

        CUser user = (CUser)Session["User"];
        m_BaseObject.Updator = user.Id;
        m_BaseObjectMgr.Update(m_BaseObject);
        if (!m_BaseObjectMgr.Save(true))
        {
            //Response.Write("修改失败！");
            Response.Write("<script>alert('修改失败！');</script>");
            return;
        }
        //在iframe里访问外面,需要parent.parent.
        //Response.Write("<script>parent.parent.grid.loadData(true);parent.parent.$.ligerDialog.close();</script>");
        Response.Write("<script>parent.parent.onOkEditMultDetailRecord2();</script>");
    }
    //验证数据
    bool ValidateData()
    {
        //foreach (CBaseObject objCIVD in m_ViewDetail.ColumnInViewDetailMgr.GetList())
        foreach (CBaseObject objCol in m_Table.ColumnMgr.GetList())
        {
            //CColumnInViewDetail civd = (CColumnInViewDetail)objCIVD;

            //CColumn col = (CColumn)m_Table.ColumnMgr.Find(civd.FW_Column_id);
            CColumn col = (CColumn)objCol;
            if (col == null)
                continue;
            //判断禁止和只读权限字段
            if (m_sortRestrictColumnAccessType.ContainsKey(col.Id))
            {
                AccessType accessType = m_sortRestrictColumnAccessType[col.Id];
                if (accessType == AccessType.forbide)
                    continue;
                //只读只在界面控制,有些默认值需要只读也需要保存数据
                //else if (accessType == AccessType.read)
                //    continue;
            }
            //

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
                Response.Write(string.Format("<script>alert('{0}不允许空！');</script>", col.Name));
                return false;
            }
            if (col.ColType == ColumnType.string_type)
            {
                if (val.Length > col.ColLen)
                {
                    Response.Write(string.Format("<script>alert('{0}长度不能超过{1}！');</script>", col.Name, col.ColLen));
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
                        Response.Write(string.Format("<script>alert('{0}日期格式错误！');</script>", col.Name));
                        return false;
                    }
                }
            }
            else if (col.ColType == ColumnType.int_type
                || col.ColType == ColumnType.long_type)
            {
                if (!Util.IsInt(val))
                {
                    Response.Write(string.Format("<script>alert('{0}为整型数字！');</script>", col.Name));
                    return false;
                }
            }
            else if (col.ColType == ColumnType.numeric_type)
            {
                if (!Util.IsNum(val))
                {
                    Response.Write(string.Format("<script>alert('{0}为数字！');</script>", col.Name));
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
                        Response.Write(string.Format("<script>alert('{0}为GUID格式！');</script>", col.Name));
                        return false;
                    }
                }
            }

            //唯一性字段判断
            if (col.IsUnique)
            {
                if (!IsUniqueValue(col, val))
                    return false;
            }
        }
        return true;
    }

    //唯一性字段判断
    bool IsUniqueValue(CColumn col, string val)
    {
        if (col.ColType == ColumnType.string_type
            || col.ColType == ColumnType.text_type
            || col.ColType == ColumnType.path_type)
        {
            CBaseObjectMgr BaseObjectMgr = m_BaseObjectMgr;
            if (BaseObjectMgr != null)
            {
                List<CBaseObject> lstObj = BaseObjectMgr.GetList();
                var varObj = from obj in lstObj
                             where obj.Id != m_BaseObject.Id && obj.m_arrNewVal[col.Code.ToLower()].StrVal.EndsWith(val, StringComparison.OrdinalIgnoreCase)
                             select obj;
                if (varObj.Count() > 0)
                    return false;
            }
            else
            {
                BaseObjectMgr = new CBaseObjectMgr();
                BaseObjectMgr.TbCode = m_Table.Code;
                BaseObjectMgr.Ctx = Global.GetCtx(Session["TopCompany"].ToString());
                string sWhere = string.Format(" [id]<>'{0}' and [{1}]='{2}'", m_BaseObject.Id, col.Code, val);
                List<CBaseObject> lstObj = BaseObjectMgr.GetList(sWhere);
                if (lstObj.Count > 0)
                    return false;
            }
        }
        else if (col.ColType == ColumnType.datetime_type)
        {
            CBaseObjectMgr BaseObjectMgr = m_BaseObjectMgr;
            if (BaseObjectMgr != null)
            {
                List<CBaseObject> lstObj = BaseObjectMgr.GetList();
                var varObj = from obj in lstObj
                             where obj.Id != m_BaseObject.Id && obj.m_arrNewVal[col.Code.ToLower()].DatetimeVal == DateTime.Parse(val)
                             select obj;
                if (varObj.Count() > 0)
                    return false;
            }
            else
            {
                BaseObjectMgr = new CBaseObjectMgr();
                BaseObjectMgr.TbCode = m_Table.Code;
                BaseObjectMgr.Ctx = Global.GetCtx(Session["TopCompany"].ToString());
                string sWhere = string.Format(" [id]<>'{0}' and [{1}]='{2}'", m_BaseObject.Id, col.Code, val);
                List<CBaseObject> lstObj = BaseObjectMgr.GetList(sWhere);
                if (lstObj.Count > 0)
                    return false;
            }
        }
        else if (col.ColType == ColumnType.int_type
            || col.ColType == ColumnType.long_type
            || col.ColType == ColumnType.numeric_type)
        {
            CBaseObjectMgr BaseObjectMgr = m_BaseObjectMgr;
            if (BaseObjectMgr != null)
            {
                List<CBaseObject> lstObj = BaseObjectMgr.GetList();
                if (col.ColType == ColumnType.int_type)
                {
                    var varObj = from obj in lstObj
                                 where obj.Id != m_BaseObject.Id && obj.m_arrNewVal[col.Code.ToLower()].IntVal == Convert.ToInt32(val)
                                 select obj;
                    if (varObj.Count() > 0)
                        return false;
                }
                else if (col.ColType == ColumnType.long_type)
                {
                    var varObj = from obj in lstObj
                                 where obj.Id != m_BaseObject.Id && obj.m_arrNewVal[col.Code.ToLower()].LongVal == Convert.ToInt64(val)
                                 select obj;
                    if (varObj.Count() > 0)
                        return false;
                }
                else
                {
                    var varObj = from obj in lstObj
                                 where obj.Id != m_BaseObject.Id && obj.m_arrNewVal[col.Code.ToLower()].DoubleVal == Convert.ToDouble(val)
                                 select obj;
                    if (varObj.Count() > 0)
                        return false;
                }
            }
            else
            {
                BaseObjectMgr = new CBaseObjectMgr();
                BaseObjectMgr.TbCode = m_Table.Code;
                BaseObjectMgr.Ctx = Global.GetCtx(Session["TopCompany"].ToString());
                string sWhere = string.Format(" [id]<>'{0}' and [{1}]={2}", m_BaseObject.Id, col.Code, val);
                List<CBaseObject> lstObj = BaseObjectMgr.GetList(sWhere);
                if (lstObj.Count > 0)
                    return false;
            }
        }
        else if (col.ColType == ColumnType.guid_type
        || col.ColType == ColumnType.ref_type)
        {
            CBaseObjectMgr BaseObjectMgr = m_BaseObjectMgr;
            if (BaseObjectMgr != null)
            {
                List<CBaseObject> lstObj = BaseObjectMgr.GetList();
                var varObj = from obj in lstObj
                             where obj.Id != m_BaseObject.Id && obj.m_arrNewVal[col.Code.ToLower()].GuidVal == new Guid(val)
                             select obj;
                if (varObj.Count() > 0)
                    return false;
            }
            else
            {
                BaseObjectMgr = new CBaseObjectMgr();
                BaseObjectMgr.TbCode = m_Table.Code;
                BaseObjectMgr.Ctx = Global.GetCtx(Session["TopCompany"].ToString());
                string sWhere = string.Format(" [id]<>'{0}' and [{1}]='{2}'", m_BaseObject.Id, col.Code, val);
                List<CBaseObject> lstObj = BaseObjectMgr.GetList(sWhere);
                if (lstObj.Count > 0)
                    return false;
            }
        }

        return true;
    }
}
