<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectBg.aspx.cs" Inherits="Desktop_SelectBg" %>
<%@ Import Namespace="ErpCoreModel" %>
<%@ Import Namespace="ErpCoreModel.Base" %>
<%@ Import Namespace="ErpCoreModel.Framework" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="System.IO" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>选择图标</title>
    <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
     <script src="../lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
// <!CDATA[

        function btUpload_onclick() {
            document.getElementById("form1").submit();
        }

        function callback(msg) {
            if (msg == "" || msg == null) {
                window.location.reload();
                return true;
            }
            else {
                $.ligerDialog.warn(msg);
                return false;
            }
        }
        function btCancel_onclick() {
            parent.$.ligerDialog.close();
        }

        function onSelectBg(src, filename) {
            parent.onSelectBg(src, filename);
            parent.$.ligerDialog.close();
        }
// ]]>
    </script>
    <style type="text/css">
           body{ font-size:12px;}
        .l-table-edit {}
        .l-table-edit-td{ padding:4px;}
        .l-button-submit,.l-button-test{width:80px; float:left; margin-left:10px; padding-bottom:2px;}
        .l-verify-tip{ left:230px; top:120px;}
    </style>
</head>
<body>
    <form id="form1" runat="server"  enctype="multipart/form-data"  method="post" target="hidden_frame">
    <table><tr><td>
    <div style="height:400px">
    <% 
        string sPath = string.Format("../{0}/DesktopImg/", Global.GetDesktopIconPathName());
        DirectoryInfo di = new DirectoryInfo(Server.MapPath(sPath));
        FileInfo[] fis= di.GetFiles();
        foreach (FileInfo fi in fis)
        {
            string src = string.Format("../{0}/DesktopImg/{1}", Global.GetDesktopIconPathName(), fi.Name);
            %>
            <div style="float:left;padding:4px;"><img src="<%=src %>" height="128" width="128" onclick="onSelectBg('<%=src %>','<%=fi.Name%>')" /></div>
            <%
        }
        %>
    </div>
    </td></tr><tr><td>
    <div>
    <table cellpadding="0" cellspacing="0" class="l-table-edit" >
        <tr>
            <td align="left" class="l-table-edit-td">
                <asp:FileUpload ID="fileIcon" runat="server" />
            </td>
            <td align="right" class="l-table-edit-td">
                <input id="btUpload" type="button" value="上传" class="l-button-submit" onclick="return btUpload_onclick()" />
                </td>
            <td align="right" class="l-table-edit-td">
                <input id="btCancel" type="button" value="取消" class="l-button-submit" onclick="return btCancel_onclick()" />
                </td>
        </tr>
  </table>
  <input type="hidden" id="Action" value="PostData"  runat="server"/>
  <iframe name='hidden_frame' id="hidden_frame" style='display:none'></iframe>  
  </div>
  </td></tr></table>
    </form>
</body>
</html>
