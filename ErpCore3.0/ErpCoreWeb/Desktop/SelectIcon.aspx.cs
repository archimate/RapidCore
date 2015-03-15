using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using ErpCoreModel;
using ErpCoreModel.Base;
using ErpCoreModel.Framework;
using ErpCoreModel.UI;

public partial class Desktop_SelectIcon : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null)
        {
            Response.Redirect("../Login.aspx");
            Response.End();
        }

        if (Request.Params["Action"] == "PostData")
        {
            PostData();
            Response.End();
        }
    }

    void PostData()
    {
        //图标文件
        if (fileIcon.PostedFile.ContentLength > 0)
        {
            string sPath = Server.MapPath(ConfigurationManager.AppSettings["VirtualDir"]);
            sPath += "\\" + Global.GetDesktopIconPathName() + "\\";
            if (!Directory.Exists(sPath))
                Directory.CreateDirectory(sPath);
            sPath += "MenuIcon\\";
            if (!Directory.Exists(sPath))
                Directory.CreateDirectory(sPath);
            int idx = fileIcon.PostedFile.FileName.LastIndexOf('.');
            if (idx > -1)
            {
                string sExt = fileIcon.PostedFile.FileName.Substring(idx);
                sExt = sExt.ToLower();
                if (sExt != ".jpg" && sExt != ".png" && sExt != ".gif")
                {
                    Response.Write("<script>parent.callback('仅支持jpg,png,gif图片文件！')</script>");
                    return;
                }
                string sFileName = Guid.NewGuid().ToString() + sExt;
                string sFile = sPath + sFileName;
                fileIcon.PostedFile.SaveAs(sFile);
                Response.Write("<script>parent.callback('')</script>");
            }
            else
            {
                Response.Write("<script>parent.callback('请选择图片文件！')</script>");
                return;
            }
        }
        else
        {
            Response.Write("<script>parent.callback('请选择图片文件！')</script>");
            return;
        }

    }
}
