using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;

public partial class Database_Table_Add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null)
        {
            Response.Redirect("../../Login.aspx");
            Response.End();
        }

        if (Request.Params["Action"] == "GetData")
        {
            GetData();
            Response.End();
        }
        else if (Request.Params["Action"] == "PostData")
        {
            PostData();
            Response.End();
        }
    }
    void GetData()
    {
        string sData = "";
        sData += string.Format("{{ \"id\": \"{0}\",\"Name\":\"id\", \"Code\":\"id\", \"ColType\":\"GUID\", \"ColLen\":\"16\",\"AllowNull\":\"0\",\"IsSystem\":\"1\",\"DefaultValue\":\"\",\"ColDecimal\":\"\",\"Formula\":\"\",\"RefTable\":\"\",\"RefTableName\":\"\",\"RefCol\":\"\",\"RefColName\":\"\",\"RefShowCol\":\"\",\"RefShowColName\":\"\",\"EnumVal\":\"\", \"IsUnique\":\"1\" }},"
            , Guid.NewGuid());
        sData += string.Format("{{ \"id\": \"{0}\",\"Name\":\"创建时间\", \"Code\":\"Created\", \"ColType\":\"日期型\",\"ColLen\":\"8\", \"AllowNull\":\"1\",\"IsSystem\":\"1\",\"DefaultValue\":\"getdate()\",\"ColDecimal\":\"\",\"Formula\":\"\",\"RefTable\":\"\",\"RefTableName\":\"\",\"RefCol\":\"\",\"RefColName\":\"\",\"RefShowCol\":\"\",\"RefShowColName\":\"\",\"EnumVal\":\"\", \"IsUnique\":\"0\" }},"
            , Guid.NewGuid());
        sData += string.Format("{{ \"id\": \"{0}\",\"Name\":\"创建人\", \"Code\":\"Creator\", \"ColType\":\"GUID\",\"ColLen\":\"16\", \"AllowNull\":\"1\",\"IsSystem\":\"1\",\"DefaultValue\":\"\",\"ColDecimal\":\"\",\"Formula\":\"\",\"RefTable\":\"\",\"RefTableName\":\"\",\"RefCol\":\"\",\"RefColName\":\"\",\"RefShowCol\":\"\",\"RefShowColName\":\"\",\"EnumVal\":\"\", \"IsUnique\":\"0\" }},"
            , Guid.NewGuid());
        sData += string.Format("{{ \"id\": \"{0}\",\"Name\":\"修改时间\", \"Code\":\"Updated\", \"ColType\":\"日期型\",\"ColLen\":\"8\", \"AllowNull\":\"1\",\"IsSystem\":\"1\",\"DefaultValue\":\"getdate()\",\"ColDecimal\":\"\",\"Formula\":\"\",\"RefTable\":\"\",\"RefTableName\":\"\",\"RefCol\":\"\",\"RefColName\":\"\",\"RefShowCol\":\"\",\"RefShowColName\":\"\",\"EnumVal\":\"\", \"IsUnique\":\"0\" }},"
            , Guid.NewGuid());
        sData += string.Format("{{ \"id\": \"{0}\",\"Name\":\"修改人\", \"Code\":\"Updator\", \"ColType\":\"GUID\",\"ColLen\":\"16\", \"AllowNull\":\"1\",\"IsSystem\":\"1\",\"DefaultValue\":\"\",\"ColDecimal\":\"\",\"Formula\":\"\",\"RefTable\":\"\",\"RefTableName\":\"\",\"RefCol\":\"\",\"RefColName\":\"\",\"RefShowCol\":\"\",\"RefShowColName\":\"\",\"EnumVal\":\"\", \"IsUnique\":\"0\" }}"
            , Guid.NewGuid());
        sData = "[" + sData + "]";
        string sJson = string.Format("{{\"Rows\":{0},\"Total\":\"{1}\"}}"
            , sData, 5);

        Response.Write(sJson);
    }
    void PostData()
    {
        string id = Request["id"];
        string Name = Request["Name"];
        string Code = Request["Code"];
        string IsSystem = Request["IsSystem"];
        string GridData = Request["GridData"];

        CUser user = (CUser)Session["User"];

        if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Code))
        {
            Response.Write("数据不完整！");
            return;
        }
        else
        {
            if (Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.FindByName(Name) != null)
            {
                Response.Write("相同名称的表已经存在！");
                return;
            }
            if (Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.FindByCode(Code) != null)
            {
                Response.Write("相同编码的表已经存在！");
                return;
            }

            CTable table = new CTable();
            table.Ctx = Global.GetCtx(Session["TopCompany"].ToString());
            table.Id = new Guid(id);
            table.Name = Name;
            table.Code = Code;
            table.IsSystem = Convert.ToBoolean(IsSystem);
            table.IsFinish = true;
            table.Creator = user.Id;
            //系统字段
            {
                CColumn col = new CColumn();
                col.Ctx = table.Ctx;
                col.FW_Table_id = table.Id;
                col.Name = "id";
                col.Code = "id";
                col.ColType = ColumnType.guid_type;
                col.ColLen = 16;
                col.AllowNull = false;
                col.IsSystem = true;
                col.IsUnique = true;
                col.IsVisible = false;
                col.Idx = 0;
                col.Creator = user.Id;
                table.ColumnMgr.AddNew(col);

                col = new CColumn();
                col.Ctx = table.Ctx;
                col.FW_Table_id = table.Id;
                col.Name = "创建时间";
                col.Code = "Created";
                col.ColType = ColumnType.datetime_type;
                col.ColLen = 8;
                //col.DefaultValue = "getdate()";
                col.AllowNull = true;
                col.IsSystem = true;
                col.IsUnique = false;
                col.IsVisible = false;
                col.Idx = 1;
                col.Creator = user.Id;
                table.ColumnMgr.AddNew(col);

                col = new CColumn();
                col.Ctx = table.Ctx;
                col.FW_Table_id = table.Id;
                col.Name = "创建者";
                col.Code = "Creator";
                col.ColType = ColumnType.ref_type;
                CTable tableUser = (CTable)Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.FindByCode("B_User");
                Guid guidUid = Guid.Empty;
                Guid guidUname = Guid.Empty;
                if (tableUser != null)
                {
                    col.RefTable = tableUser.Id;
                    CColumn colUid = tableUser.ColumnMgr.FindByCode("id");
                    col.RefCol = colUid.Id;
                    guidUid = col.RefCol;
                    CColumn colUname = tableUser.ColumnMgr.FindByCode("name");
                    col.RefShowCol = colUname.Id;
                    guidUname = col.RefShowCol;
                }
                col.ColLen = 16;
                //col.DefaultValue = "0";
                col.AllowNull = true;
                col.IsSystem = true;
                col.IsUnique = false;
                col.IsVisible = false;
                col.Idx = 2;
                col.Creator = user.Id;
                table.ColumnMgr.AddNew(col);

                col = new CColumn();
                col.Ctx = table.Ctx;
                col.FW_Table_id = table.Id;
                col.Name = "修改时间";
                col.Code = "Updated";
                col.ColType = ColumnType.datetime_type;
                col.ColLen = 8;
                //col.DefaultValue = "getdate()";
                col.AllowNull = true;
                col.IsSystem = true;
                col.IsUnique = false;
                col.IsVisible = false;
                col.Idx = 3;
                col.Creator = user.Id;
                table.ColumnMgr.AddNew(col);

                col = new CColumn();
                col.Ctx = table.Ctx;
                col.FW_Table_id = table.Id;
                col.Name = "修改者";
                col.Code = "Updator";
                col.ColType = ColumnType.ref_type;
                if (tableUser != null)
                {
                    col.RefTable = tableUser.Id;
                    col.RefCol = guidUid;
                    col.RefShowCol = guidUname;
                }
                col.ColLen = 16;
                //col.DefaultValue = "0";
                col.AllowNull = true;
                col.IsSystem = true;
                col.IsUnique = false;
                col.IsVisible = false;
                col.Idx = 4;
                col.Creator = user.Id;
                table.ColumnMgr.AddNew(col);
            }
            //自定义字段
            int iLastIdx = 4;
            string[] arr1 = Regex.Split(GridData, ";");
            foreach (string str1 in arr1)
            {
                if (str1.Length == 0)
                    continue;
                iLastIdx++;
                
                string[] arr2 = Regex.Split(str1, ",");
                CColumn col = new CColumn();
                col.Ctx = table.Ctx;
                col.FW_Table_id = table.Id;
                col.Id = new Guid(arr2[0]);
                col.Name = arr2[1];
                col.Code = arr2[2];
                col.ColType = CColumn.ConvertStringToColType(arr2[3]);
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
                col.Creator = user.Id;
                table.ColumnMgr.AddNew(col);
            }

            Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.AddNew(table);

            if (!CTable.CreateDataTable(table))
            {
                Response.Write("创建表失败！");
                return;
            }
            if (!Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.Save(true))
            {
                Response.Write("添加失败！");
                return;
            }
        }

    }
}
