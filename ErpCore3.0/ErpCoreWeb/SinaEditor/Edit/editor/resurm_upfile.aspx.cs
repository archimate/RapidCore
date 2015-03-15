using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;

public partial class SinaEditor_Edit_editor_resurm_upfile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Files.Count == 0)
        {
            Response.End();
            return;
        }
        string sUploadPath = ConfigurationManager.AppSettings["VirtualDir"] + "/uploadfiles/";
        if (sUploadPath[sUploadPath.Length - 1] != '/')
            sUploadPath += "/";
        string sDir = Server.MapPath(sUploadPath);
        if (!Directory.Exists(sDir))
            Directory.CreateDirectory(sDir);
        if (!Directory.Exists(sDir))
        {
            Response.Write("<script>alert('请先创建上传文件的路径！');history.back();</script>");
            Response.End();
            return;
        }

        HttpPostedFile postfile = Request.Files.Get("file1");
        if (postfile.ContentLength > 0)
        {
            string sFileName = postfile.FileName;
            if (sFileName.LastIndexOf('\\') > -1)//有些浏览器不带路径
                sFileName = sFileName.Substring(sFileName.LastIndexOf('\\'));

            FileInfo fi = new FileInfo(sDir + sFileName);
            string sExt = fi.Extension.ToLower();
            if (sExt != ".jpg" && sExt != ".jpeg" && sExt != ".gif" && sExt != ".png" && sExt != ".bmp")
            {
                Response.Write("<script>alert('不允许上传该类型的文件！');history.back();</script>");
                Response.End();
                return;
            }
            try
            {
                Guid guid = Guid.NewGuid();
                string sDestFile = string.Format("{0}{1}", guid.ToString().Replace("-", ""), fi.Extension);
                string sFile = sUploadPath + sDestFile;
                postfile.SaveAs(Server.MapPath(sFile));

                
                Response.Write("<script>window.parent.document.getElementById('imgpath').value='" + sFile + "';window.parent.chk_imgpath();</script>");
            }
            catch
            {
                Response.Write("<script>alert('文件上传错误！');history.back();</script>");
                Response.End();
                return;
            }
        }
        else
        {
 	        Response.Write( "<script>alert('请先选择你要上传的文件！');history.back();</script>");
            Response.End();
            return;
        }
    }
}
