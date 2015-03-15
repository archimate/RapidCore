using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.UI;

public partial class Common_Variable : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["Action"] == "GetData")
        {
            GetData();
            Response.End();
        }
    }

    void GetData()
    {
        string sData = "";
        CVariable Variable = new CVariable();
        foreach (KeyValuePair<string, string> kv in CVariable.g_VarName)
        {
            sData += string.Format("{{ \"Name\": \"{0}\",\"Desp\":\"{1}\"}},"
                , kv.Key, kv.Value);
        }

        sData = sData.TrimEnd(",".ToCharArray());
        sData = "[" + sData + "]";
        string sJson = string.Format("{{\"Rows\":{0},\"Total\":\"{1}\"}}"
            , sData, CVariable.g_VarName.Count);

        Response.Write(sJson);
    }
}
