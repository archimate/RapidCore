using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class Invoicing_KC_OutXls : System.Web.UI.Page
{
    public string m_url = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null)
        {
            Response.Redirect("../../login.aspx");
            Response.End();
        }
        string file = Request["file"];
        m_url = "output/"+file;

        //string path = Server.MapPath(filename);
        //System.IO.FileInfo file = new System.IO.FileInfo(path);

        //Response.Clear();
        //Response.Charset = "GB2312";
        //Response.ContentEncoding = System.Text.Encoding.UTF8;
        //Response.AddHeader("Content-Disposition", "attachment; filename = " + Server.UrlEncode(file.Name));
        //Response.AddHeader("Content-Length", file.Length.ToString());
        //// 指定返回的是一个不能被客户端读取的流，必须被下载   
        //Response.ContentType = "application/ms-excel";
        //// 把文件流发送到客户端   
        //Response.WriteFile(file.FullName);
        //// 停止页面的执行   
        //Response.End();
        //Response.Redirect(filename);
    }
}
