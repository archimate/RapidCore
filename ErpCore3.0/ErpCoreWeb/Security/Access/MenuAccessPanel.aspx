<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MenuAccessPanel.aspx.cs" Inherits="Security_Access_MenuAccessPanel" %>
<%@ Import Namespace="ErpCoreModel" %>
<%@ Import Namespace="ErpCoreModel.Base" %>
<%@ Import Namespace="ErpCoreModel.Framework" %>
<%@ Import Namespace="System.Collections.Generic" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="../../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../../lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <script src="../../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script> 
    <script src="../../lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerMenu.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerMenuBar.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerTextBox.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerDateEditor.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerSpinner.js" type="text/javascript"></script> 
    <script src="../../lib/ligerUI/js/plugins/ligerTree.js" type="text/javascript"></script>
    <script type="text/javascript">
        
    </script>
    <style type="text/css">
    #menu1,.l-menu-shadow{top:30px; left:50px;}
    #menu1{  width:200px;}
    </style>
    
    <script type="text/javascript">

        var tree = null;
        var actionNode = null;
        $(function() {
            //树
            $("#tree1").ligerTree({
                url: 'MenuAccessPanel.aspx?Action=GetData&B_Company_id=<%=Request["B_Company_id"] %>',
                checkbox: true,
                autoCheckboxEven: false,
                slide: false,
                nodeWidth: 120,
                attribute: ['nodename', 'url'],
                onSelect: function(node) {
                    actionNode = node;
                },
                onBeforeExpand: function(node) {
                }
            });

            tree = $("#tree1").ligerGetTreeManager();
        });

        //下拉框
        $(function() {
            $("#cbUid").ligerComboBox({
                onBeforeOpen: onSelectUid
            });
            $("#cbGroup").ligerComboBox({
            onBeforeOpen: onSelectDesktopGroup
            });
        });

        function onSelectUid() {
            if (document.getElementById("cbUType").selectedIndex < 1) {
                $.ligerDialog.warn('请选择类型!');
                return false;
            }
            var title = '选择用户';
            var url = 'SelectUser.aspx?B_Company_id=<%=Request["B_Company_id"] %>';
            if (document.getElementById("cbUType").selectedIndex == 2) {
                title = '选择角色';
                url = 'SelectRole.aspx?B_Company_id=<%=Request["B_Company_id"] %>';
            }
            $.ligerDialog.open({ title: title, name: 'userselector', width: 400, height: 300, url: url, buttons: [
                { text: '确定', onclick: onSelectUidOK },
                { text: '取消', onclick: onSelectCancel }
            ]
            });
            return false;
        }
        function onSelectUidOK(item, dialog) {
            var fn = dialog.frame.onSelect || dialog.frame.window.onSelect;
            var data = fn();
            if (!data) {
                $.ligerDialog.warn('请选择行!');
                return false;
            }
            var utype = "0";
            if (document.getElementById("cbUType").selectedIndex == 2)
                utype = "1";
            var GroupId = $("#hidGroupId").val();
            if (GroupId != "") {
                tree.clear();
                var url = 'MenuAccessPanel.aspx?Action=GetData&B_Company_id=<%=Request["B_Company_id"] %>&UType=' + utype + '&GroupId=' + GroupId + '&Uid=' + data.id;
                tree.set({ url: url });
                tree.loadData();
            }

            $("#cbUid").val(data.Name);
            $("#hidUid").val(data.id);
            dialog.close();
        }
        function onSelectDesktopGroup() {
            var title = '选择桌面组';
            var url = 'SelectDesktopGroup.aspx';
            $.ligerDialog.open({ title: title, name: 'desktopgroupselector', width: 400, height: 300, url: url, buttons: [
                { text: '确定', onclick: onSelectDesktopGroupOK },
                { text: '取消', onclick: onSelectCancel }
            ]
            });
            return false;
        }
        function onSelectDesktopGroupOK(item, dialog) {
            var fn = dialog.frame.onSelect || dialog.frame.window.onSelect;
            var data = fn();
            if (!data) {
                $.ligerDialog.warn('请选择行!');
                return false;
            }
            var utype = "0";
            if (document.getElementById("cbUType").selectedIndex == 2)
                utype = "1";
            var Uid = $("#hidUid").val();
            if (Uid != "") {
                tree.clear();
                var url = 'MenuAccessPanel.aspx?Action=GetData&B_Company_id=<%=Request["B_Company_id"] %>&UType=' + utype + '&Uid=' + Uid + '&GroupId=' + data.id;
                tree.set({ url: url });
                tree.loadData();
            }

            $("#cbGroup").val(data.Name);
            $("#hidGroupId").val(data.id);
            dialog.close();
        }
        function onSelectCancel(item, dialog) {
            dialog.close();
        }

        function btSave_onclick() {
            if (document.getElementById("cbUType").selectedIndex < 1) {
                $.ligerDialog.warn('请选择类型！');
            }
            if ($("#hidUid").val() == "") {
                if (document.getElementById("cbUType").selectedIndex == 1)
                    $.ligerDialog.warn('请选择用户！');
                else
                    $.ligerDialog.warn('请选择角色！');
            }
            if ($("#hidGroupId").val() == "") {
                $.ligerDialog.warn('请选择桌面组！');
            }
            var utype = "0";
            if (document.getElementById("cbUType").selectedIndex == 2)
                utype = "1";
            var postData = "";
            var nodes = tree.getChecked();
            for (var idx = 0; idx < nodes.length; idx++) {
                postData += nodes[idx].data.id;
                postData += ",";
            }
            $.post(
                'MenuAccessPanel.aspx',
                {
                    Action: 'PostData',
                    B_Company_id: '<%=Request["B_Company_id"] %>',
                    UType: utype,
                    Uid: $("#hidUid").val(),
                    GroupId: $("#hidGroupId").val(),
                    postData: postData
                },
                 function(data) {
                     if (data == "" || data == null) {
                         $.ligerDialog.success('保存成功！');
                         return true;
                     }
                     else {
                         $.ligerDialog.warn(data);
                         return false;
                     }
                 },
                 'text');
        }

        function cbUType_onchange() {
            $("#cbUid").val('');
            $("#hidUid").val('');
        }

    </script>
    <style type="text/css">
           body{ font-size:12px;}
        .l-table-edit {}
        .l-table-edit-td{ padding:4px;}
        .l-button-submit,.l-button-test{width:80px; float:left; margin-left:10px; padding-bottom:2px;}
        .l-verify-tip{ left:230px; top:120px;}
    </style>
</head>
<body style="padding:6px; overflow:hidden;"> 
<form id="from1">
    <table cellpadding="0" cellspacing="0" class="l-table-edit" >
        <tr>
            <td align="right" class="l-table-edit-td">选择:</td>
            <td align="left" class="l-table-edit-td">
                <select id="cbUType" onchange="return cbUType_onchange()">
                    <option></option>
                    <option value="0">用户</option>
                    <option value="1">角色</option>
                </select>
            </td>
            
            <td align="left" class="l-table-edit-td"><input name="cbUid" type="text" id="cbUid" ltype="text"  />
            <input id="hidUid" name="hidUid" type="hidden" />
            </td>
            
            <td align="left" class="l-table-edit-td"><input name="cbGroup" type="text" id="cbGroup" ltype="text"  />
            <input id="hidGroupId" name="hidGroupId" type="hidden" />
            </td>
        </tr>
  </table>
  <div style="overflow:auto;  ">
   <ul id="tree1">   </ul>
   </div>
    
<input id="btSave" type="button" value="保存" class="l-button-submit" onclick="return btSave_onclick()" />
</form>
</body>
</html>
