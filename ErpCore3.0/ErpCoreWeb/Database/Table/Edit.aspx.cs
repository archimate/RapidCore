using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;

public partial class Database_Table_Edit : System.Web.UI.Page
{
    public CTable m_Table = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null)
        {
            Response.Redirect("../../Login.aspx");
            Response.End();
        }

        string id = Request["id"];
        if (string.IsNullOrEmpty(id))
        {
            Response.Write("请选择表！");
            Response.End();
        }
        m_Table = (CTable)Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.Find(new Guid(id));
        if (m_Table == null)
        {
            Response.Write("表不存在！");
            Response.End();
        }
        //保存到编辑对象
        EditObject.Add(Session.SessionID,m_Table);

        if (Request.Params["Action"] == "GetData")
        {
            GetData();
            Response.End();
        }
        else if (Request.Params["Action"] == "Cancel")
        {
            m_Table.Cancel();
            Response.End();
        }
        else if (Request.Params["Action"] == "PostData")
        {
            PostData();
            //从编辑对象移除
            EditObject.Remove(Session.SessionID, m_Table);

            Response.End();
        }
    }
    void GetData()
    {

        string sData = "";
        List<CBaseObject> lstObj = m_Table.ColumnMgr.GetList();
        foreach (CBaseObject obj in lstObj)
        {
            CColumn col = (CColumn)obj;
            string sRefTableName = "", sRefColName = "", sRefShowColName = "", sEnumVal = "";
            if (col.RefTable != Guid.Empty)
            {
                CTable refTable = (CTable)Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.Find(col.RefTable);
                if (refTable != null)
                {
                    sRefTableName = refTable.Name;
                    if (col.RefCol != Guid.Empty)
                    {
                        CColumn refCol = (CColumn)refTable.ColumnMgr.Find(col.RefCol);
                        if (refCol != null)
                            sRefColName = refCol.Name;
                        CColumn refShowCol = (CColumn)refTable.ColumnMgr.Find(col.RefShowCol);
                        if (refShowCol != null)
                            sRefShowColName = refShowCol.Name;
                    }
                }
            }
            List<CBaseObject> lstObjEV = col.ColumnEnumValMgr.GetList();
            foreach (CBaseObject objEV in lstObjEV)
            {
                CColumnEnumVal ev = (CColumnEnumVal)objEV;
                sEnumVal += ev.Val + "/";
            }
            sEnumVal = sEnumVal.TrimEnd("/".ToCharArray());

            sData += string.Format("{{ \"id\": \"{0}\",\"Name\":\"{1}\", \"Code\":\"{2}\", \"ColType\":\"{3}\", \"ColLen\":\"{4}\",\"AllowNull\":\"{5}\",\"IsSystem\":\"{6}\",\"DefaultValue\":\"{7}\",\"ColDecimal\":\"{8}\",\"Formula\":\"{9}\",\"RefTable\":\"{10}\",\"RefTableName\":\"{11}\",\"RefCol\":\"{12}\",\"RefColName\":\"{13}\",\"RefShowCol\":\"{14}\",\"RefShowColName\":\"{15}\",\"EnumVal\":\"{16}\",\"IsUnique\":\"{17}\" }},"
                , col.Id, col.Name, col.Code, CColumn.ConvertColTypeToString(col.ColType), col.ColLen, col.AllowNull ? 1 : 0, col.IsSystem ? 1 : 0, col.DefaultValue, col.ColDecimal, col.Formula, col.RefTable, sRefTableName, col.RefCol, sRefColName, col.RefShowCol, sRefShowColName, sEnumVal, col.IsUnique ? 1 : 0);
        }
        sData = sData.TrimEnd(",".ToCharArray());
        sData = "[" + sData + "]";
        string sJson = string.Format("{{\"Rows\":{0},\"Total\":\"{1}\"}}"
            , sData, 5);

        Response.Write(sJson);
    }
    void PostData()
    {
        string Name = Request["Name"];
        string Code = Request["Code"];
        string IsSystem = Request["IsSystem"];
        string GridData = Request["GridData"];

        CUser user = (CUser)Session["User"];

        if ( string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Code))
        {
            Response.Write("数据不完整！");
            return;
        }
        else
        {
            if (!m_Table.Name.Equals(Name, StringComparison.OrdinalIgnoreCase)
                && Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.FindByName(Name) != null)
            {
                Response.Write("相同名称的表已经存在！");
                return;
            }
            if (!m_Table.Code.Equals(Code, StringComparison.OrdinalIgnoreCase)
                && Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.FindByCode(Code) != null)
            {
                Response.Write("相同编码的表已经存在！");
                return;
            }

            m_Table.Name = Name;
            m_Table.Code = Code;
            m_Table.IsSystem = Convert.ToBoolean(IsSystem);
            m_Table.IsFinish = true;
            m_Table.Updator = user.Id;
            
            //自定义字段
            int iLastIdx = 4;
            string[] arr1 = Regex.Split(GridData, ";");
            SortedList<Guid, Guid> sortColID = new SortedList<Guid, Guid>();
            foreach (string str1 in arr1)
            {
                if (str1.Length == 0)
                    continue;
                iLastIdx++;
                
                string[] arr2 = Regex.Split(str1, ",");
                Guid guidID = new Guid(arr2[0]);
                if (sortColID.ContainsKey(guidID))
                    continue;

                sortColID.Add(guidID, guidID);
                CColumn col = (CColumn)m_Table.ColumnMgr.Find(guidID);
                if (col == null)
                {
                    col = new CColumn();
                    col.Ctx = m_Table.Ctx;
                    col.FW_Table_id = m_Table.Id;
                    col.Id = guidID;
                    col.Creator = user.Id;
                    m_Table.ColumnMgr.AddNew(col);
                }
                else
                {
                    col.Updator = user.Id;
                    m_Table.ColumnMgr.Update(col);
                }
                col.Name = arr2[1];
                col.Code = arr2[2];
                col.ColType =CColumn.ConvertStringToColType(arr2[3]);
                col.ColLen = Convert.ToInt32(arr2[4]);
                col.AllowNull = (arr2[5]=="1")?true:false;
                col.IsSystem = (arr2[6] == "1") ? true : false;
                col.IsUnique = (arr2[17] == "1") ? true : false;
                col.IsVisible = true;
                col.DefaultValue = arr2[7];
                col.ColDecimal = (arr2[8] != "") ? Convert.ToInt32(arr2[8]) : 0;
                col.Formula = arr2[9];
                if (col.ColType == ColumnType.ref_type)
                {
                    col.RefTable = new Guid(arr2[10]);
                    col.RefCol = new Guid(arr2[12]);
                    col.RefShowCol = new Guid(arr2[14]);
                }
                if (arr2[16].Trim() != "")
                {
                    col.ColumnEnumValMgr.RemoveAll();
                    string[] arrEnum = Regex.Split(arr2[16].Trim(), "/");
                    for (int i = 0; i < arrEnum.Length;i++ )
                    {
                        string sEnumVal = arrEnum[i];
                        CColumnEnumVal ev = new CColumnEnumVal();
                        ev.Ctx = col.Ctx;
                        ev.FW_Column_id = col.Id;
                        ev.Val = sEnumVal;
                        ev.Idx = i;
                        col.ColumnEnumValMgr.AddNew(ev);
                    }
                }
                col.Idx = iLastIdx;
            }
            //要删除的字段
            SortedList<Guid, Guid> sortOldColID = new SortedList<Guid, Guid>();
            List<CBaseObject> lstOldCol = m_Table.ColumnMgr.GetList();
            foreach (CBaseObject objOld in lstOldCol)
            {
                CColumn col = (CColumn)objOld;
                if (col.Code.Equals("id", StringComparison.OrdinalIgnoreCase)
                    || col.Code.Equals("Created", StringComparison.OrdinalIgnoreCase)
                    || col.Code.Equals("Creator", StringComparison.OrdinalIgnoreCase)
                    || col.Code.Equals("Updated", StringComparison.OrdinalIgnoreCase)
                    || col.Code.Equals("Updator", StringComparison.OrdinalIgnoreCase))
                    continue;
                sortOldColID.Add(objOld.Id, objOld.Id);
            }
            foreach (KeyValuePair<Guid, Guid> pair in sortOldColID)
            {
                if (!sortColID.ContainsKey(pair.Key))
                    m_Table.ColumnMgr.Delete(pair.Key);
            }
            //

            Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.Update(m_Table);

            if (!CTable.CreateDataTable(m_Table))
            {
                Response.Write("创建表失败！");
                return;
            }
            if (!Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.Save(true))
            {
                Response.Write("修改失败！");
                return;
            }
        }

    }
}
