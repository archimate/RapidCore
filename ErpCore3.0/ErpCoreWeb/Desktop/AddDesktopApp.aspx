<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddDesktopApp.aspx.cs" Inherits="Desktop_AddDesktopApp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>新建菜单</title>
    <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
     <script src="../lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerButton.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerRadio.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerSpinner.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerTextBox.js" type="text/javascript"></script> 
    <script type="text/javascript">

        function onSubmit() {
            if ($("#txtName").val() == "") {
                $.ligerDialog.warn("名称不能空！");
                return false;
            }
            else {
                if ($("#txtUrl").val() == "") {
                    $.ligerDialog.warn("请输入Url！");
                    return false;
                }
            }
            $.post(
                'AddDesktopApp.aspx',
                {
                    Action: 'PostData',
                    Name: $("#txtName").val(),
                    Url: $("#txtUrl").val(),
                    Icon: $("#hidIcon").val(),
                    OpenwinWidth:$("#txtOpenwinWidth").val(),
                    OpenwinWidth:$("#txtOpenwinHeight").val(),
                    GroupId:'<%=Request["GroupId"] %>'
                },
                 function(data) {
                     if (data == "" || data == null) {
                         parent.window.location.reload();
                         parent.$.ligerDialog.close();
                         return true;
                     }
                     else {
                         $.ligerDialog.warn(data);
                         return false;
                     }
                 },
                 'text');
        }

        function btBrowser_onclick() {
            parent.win.max();
            var win = $.ligerDialog.open(
            { url: 'SelectIcon.aspx',height: 500,  width: 600,  isResize: true, modal: false, title: '选择图标', slide: false
                //        , buttons: [
                //            { text: '确定', onclick: function (item, Dialog, index) {
                //                win.hide();
                //            }
                //            }
                //        ]
            });
        }

        function onSelectIcon(src, filename) {
            document.getElementById("imgIcon").src = src;
            $("#hidIcon").val(filename);
        }
    </script>
    <style type="text/css">
           body{ font-size:12px;}
        .l-table-edit {}
        .l-table-edit-td{ padding:4px;}
        .l-button-submit,.l-button-test{width:80px;  margin-left:10px; padding-bottom:2px;}
        .l-verify-tip{ left:230px; top:120px;}
    </style>
</head>
<body  style="padding:20px">
    <form id="form1" runat="server">
    <div>
    
    <table cellpadding="0" cellspacing="0" class="l-table-edit" >
        <tr>
            <td align="right" class="l-table-edit-td">名称：</td>
            <td align="left" class="l-table-edit-td"><input name="txtName" type="text" id="txtName" ltype="text"  runat="server"/></td>
        </tr>
        <tr>
            <td align="right" class="l-table-edit-td">Url：</td>
            <td align="left" class="l-table-edit-td"><input name="txtUrl" style="width:200px" type="text" id="txtUrl" ltype="text"  runat="server"/></td>
        </tr>
        <tr>
            <td align="right" class="l-table-edit-td">图标：</td>
            <td align="left" class="l-table-edit-td">
                <img id="imgIcon" alt="" src=""  runat="server" Height="48" Width="48"/>
                <input type="hidden" name="hidIcon" id="hidIcon"  runat="server"/>
                <input id="btBrowser" type="button" class="l-button-submit" value="浏览" onclick="return btBrowser_onclick()" />
            </td>
        </tr>
        <tr>
            <td align="right" class="l-table-edit-td">窗口尺寸：</td>
            <td align="left" class="l-table-edit-td">
            <input name="txtOpenwinWidth" type="text" id="txtOpenwinWidth" ltype="text"  runat="server" value="0" />(宽)<br />
            <input name="txtOpenwinHeight" type="text" id="txtOpenwinHeight" ltype="text"  runat="server" value="0" />(高)
            </td>
        </tr>
  </table>
    </div>
    </form>
</body>
</html>
