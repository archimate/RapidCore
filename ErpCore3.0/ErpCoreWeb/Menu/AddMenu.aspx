<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddMenu.aspx.cs" Inherits="Menu_AddMenu" %>

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

        //下拉框
        $(function() {
            $("#txtParent").ligerComboBox({
                onBeforeOpen: onSelectParent
            });
            $("#cbView").ligerComboBox({
                onBeforeOpen: onSelectView
            });
            $("#cbWindow").ligerComboBox({
                onBeforeOpen: onSelectWindow
            });
            $("#cbReport").ligerComboBox({
                onBeforeOpen: onSelectReport
            });
        });

        function onSelectParent() {
            $.ligerDialog.open({ title: '选择菜单', name: 'menuselector', width: 350, height: 260, url: 'SelectParentMenu.aspx', buttons: [
                { text: '确定', onclick: onSelectParentOK },
                { text: '取消', onclick: onSelectCancel }
            ]
            });
            return false;
        }
        function onSelectParentOK(item, dialog) {
            var fn = dialog.frame.onSelect || dialog.frame.window.onSelect;
            var data = fn();
            if (!data) {
                $.ligerDialog.alert('请选择行!');
                return false;
            }
            $("#txtParent").val(data.text);
            $("#hidParent").val(data.id);
            dialog.close();
        }
        function onSelectView() {
            $.ligerDialog.open({ title: '选择视图', name: 'viewselector', width: 350, height: 260, url: 'SelectView.aspx', buttons: [
                { text: '确定', onclick: onSelectViewOK },
                { text: '取消', onclick: onSelectCancel }
            ]
            });
            return false;
        }
        function onSelectCancel(item, dialog) {
            dialog.close();
        }
        function onSelectViewOK(item, dialog) {
            var fn = dialog.frame.onSelect || dialog.frame.window.onSelect;
            var data = fn();
            if (!data) {
                $.ligerDialog.alert('请选择行!');
                return false;
            }
            $("#cbView").val(data.Name);
            $("#hidView").val(data.id);
            dialog.close();
        }

        function onSelectWindow() {
            $.ligerDialog.open({ title: '选择窗体', name: 'windowselector', width: 350, height: 260, url: 'SelectWindow.aspx', buttons: [
                { text: '确定', onclick: onSelectWindowOK },
                { text: '取消', onclick: onSelectCancel }
            ]
            });
            return false;
        }
        function onSelectWindowOK(item, dialog) {
            var fn = dialog.frame.onSelect || dialog.frame.window.onSelect;
            var data = fn();
            if (!data) {
                $.ligerDialog.alert('请选择行!');
                return false;
            }
            $("#cbWindow").val(data.Name);
            $("#hidWindow").val(data.id);
            dialog.close();
        }

        function onSelectReport() {
            $.ligerDialog.open({ title: '选择报表', name: 'reportselector', width: 350, height: 260, url: 'SelectReport.aspx', buttons: [
                { text: '确定', onclick: onSelectReportOK },
                { text: '取消', onclick: onSelectCancel }
            ]
            });
            return false;
        }
        function onSelectReportOK(item, dialog) {
            var fn = dialog.frame.onSelect || dialog.frame.window.onSelect;
            var data = fn();
            if (!data) {
                $.ligerDialog.alert('请选择行!');
                return false;
            }
            $("#cbReport").val(data.Name);
            $("#hidReport").val(data.id);
            dialog.close();
        }
        function rdType1_onclick() {
            document.getElementById("cbView").disabled = true;
            document.getElementById("cbWindow").disabled = true;
            document.getElementById("txtUrl").disabled = true;
            document.getElementById("cbReport").disabled = true;
        }

        function rdType2_onclick() {
            document.getElementById("cbView").disabled = false;
            document.getElementById("cbWindow").disabled = true;
            document.getElementById("txtUrl").disabled = true;
            document.getElementById("cbReport").disabled = true;
        }

        function rdType3_onclick() {
            document.getElementById("cbView").disabled = true;
            document.getElementById("cbWindow").disabled = false;
            document.getElementById("txtUrl").disabled = true;
            document.getElementById("cbReport").disabled = true;
        }

        function rdType4_onclick() {
            document.getElementById("cbView").disabled = true;
            document.getElementById("cbWindow").disabled = true;
            document.getElementById("txtUrl").disabled = false;
            document.getElementById("cbReport").disabled = true;
        }
        function rdType5_onclick() {
            document.getElementById("cbView").disabled = true;
            document.getElementById("cbWindow").disabled = true;
            document.getElementById("txtUrl").disabled = true;
            document.getElementById("cbReport").disabled = false;
        }

        function onSubmit() {
            if ($("#txtName").val() == "") {
                $.ligerDialog.warn("名称不能空！");
                return false;
            }
            if (document.getElementById("rdType1").checked) {
            }
            else if (document.getElementById("rdType2").checked) {
                if ($("#hidView").val() == "") {
                    $.ligerDialog.warn("请选择视图！");
                    return false;
                }
            }
            else if (document.getElementById("rdType3").checked) {
                if ($("#hidWindow").val() == "") {
                    $.ligerDialog.warn("请选择窗体！");
                    return false;
                }
            }
            else if (document.getElementById("rdType4").checked) {
                if ($("#txtUrl").val() == "") {
                    $.ligerDialog.warn("请输入Url！");
                    return false;
                }
            }
            else if (document.getElementById("rdType5").checked) {
                if ($("#hidReport").val() == "") {
                    $.ligerDialog.warn("请选择报表！");
                    return false;
                }
            }
            document.getElementById("form1").submit();
        }
        
        function callback(msg) {
            if (msg == "" || msg == null) {
                parent.reloadActionNodeChildren();
                parent.$.ligerDialog.close();
                return true;
            }
            else {
                $.ligerDialog.warn(msg);
                return false;
            }
        }
        function btBrowser_onclick() {
            parent.win.max();
            var win = $.ligerDialog.open(
            { url: '../Desktop/SelectIcon.aspx', height: 500, width: 600, isResize: true, modal: false, title: '选择图标', slide: false
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
    <form id="form1" runat="server"  enctype="multipart/form-data"  method="post" target="hidden_frame">
    <div>
    
    <table cellpadding="0" cellspacing="0" class="l-table-edit" >
        <tr>
            <td align="right" class="l-table-edit-td">名称：</td>
            <td align="left" class="l-table-edit-td"><input name="txtName" type="text" id="txtName" ltype="text"  runat="server"/></td>
        </tr>
        <tr>
            <td align="right" class="l-table-edit-td">父级：</td>
            <td align="left" class="l-table-edit-td"><input name="txtParent" type="text" id="txtParent" ltype="text"  readonly="readonly"  runat="server"/>
            <input id="hidParent" type="hidden"  runat="server"/></td>
        </tr>
        <tr>
            <td align="right" class="l-table-edit-td">类型：</td>
            <td align="left" class="l-table-edit-td">
                <input id="rdType1" name="rdType" type="radio" checked onclick="return rdType1_onclick()"  runat="server"/>分级菜单<br />
                <input id="rdType2" name="rdType" type="radio" onclick="return rdType2_onclick()"  runat="server"/>视图菜单<br />
                <input id="rdType3" name="rdType" type="radio" onclick="return rdType3_onclick()"  runat="server"/>窗体菜单<br />
                <input id="rdType4" name="rdType" type="radio" onclick="return rdType4_onclick()"  runat="server"/>url菜单<br />
                <input id="rdType5" name="rdType" type="radio" onclick="return rdType5_onclick()"  runat="server"/>报表菜单
            </td>
        </tr>
        <tr>
            <td align="right" class="l-table-edit-td">视图：</td>
            <td align="left" class="l-table-edit-td"><input name="cbView" type="text" id="cbView" ltype="text"  runat="server"/>
                <input id="hidView" type="hidden"  runat="server"/>
            </td>
        </tr>
        <tr>
            <td align="right" class="l-table-edit-td">窗体：</td>
            <td align="left" class="l-table-edit-td"><input name="cbWindow" type="text" id="cbWindow" ltype="text"   runat="server"/>
            <input id="hidWindow" type="hidden"  runat="server"/></td>
        </tr>
        <tr>
            <td align="right" class="l-table-edit-td">Url：</td>
            <td align="left" class="l-table-edit-td"><input name="txtUrl" type="text" id="txtUrl" ltype="text"  runat="server"/></td>
        </tr>
        <tr>
            <td align="right" class="l-table-edit-td">报表：</td>
            <td align="left" class="l-table-edit-td"><input name="cbReport" type="text" id="cbReport" ltype="text"   runat="server"/>
            <input id="hidReport" type="hidden"  runat="server"/></td>
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
  <input type="hidden" id="Action" value="PostData"  runat="server"/>
  <iframe name='hidden_frame' id="hidden_frame" style='display:none'></iframe>
    </div>
    </form>
</body>
</html>
