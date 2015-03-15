<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetTButton.aspx.cs" Inherits="View_SetTButton" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script> 
    <script src="../lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerMenu.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerMenuBar.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerTextBox.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerDateEditor.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerSpinner.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(function() {
            $("#toptoolbar").ligerToolBar({ items: [
                { text: '向上', click: onUp },
                { text: '向下', click: onDown },
                { text: '删除', click: onDelete, icon: 'delete' }
            ]
            });
        });
        //向上
        function onUp() {
            grid.up( grid.getSelectedRow());
        }
        //向下
        function onDown() {
            grid.down(grid.getSelectedRow());
        }
        //删除
        function onDelete() {
            var row = grid.getSelectedRow();
            if (row == null) {
                $.ligerDialog.alert('请选择行!');
                return;
            }

            $.post(
            'SetTButton.aspx',
            {
                Action: 'Delete',
                id: '<%=Request["id"] %>',
                delid: row.id
            },
             function(data) {
                 if (data == "" || data == null) {
                     //$.ligerDialog.close();
                     grid.loadData(true);
                     return true;
                 }
                 else {
                     $.ligerDialog.warn(data);
                     return false;
                 }
             },
            'text');
        }

    </script>
    <script type="text/javascript">
        var grid;
        $(function() {
            grid = $("#gridTable").ligerGrid({
                columns: [
                { display: '标题', name: 'Caption', align: 'left', width: 120, editor: { type: 'text'} },
                { display: 'Url', name: 'Url', align: 'left', width: 220, editor: { type: 'text'} }
                ],
                url: 'SetTButton.aspx?Action=GetData&id=<%=Request["id"] %> ',
                dataAction: 'server',
                usePager: false,
                enabledEdit: true,
                width: '100%', height: '75%',
                onBeforeEdit: function(e) {
                },
                onBeforeSubmitEdit: function(e) {
                },
                onAfterEdit: function(e) {
                }
            });
        });


        function btOk_onclick() {
            var postData = "";
            var rowData = grid.getData();
            for (var idx = 0; idx < rowData.length; idx++) {
                var id, caption,url;
                $.each(rowData[idx], function(key, val) {
                    if (key == "id")
                        id = val;
                    else if (key == "Caption")
                        caption = val;
                    else if (key == "Url")
                        url = val;
                });
                postData += id + "|" + caption+"|"+url;
                postData += ";";
            }
            //提交
            $.post(
                'SetTButton.aspx',
                {
                    Action: 'PostData',
                    id: '<%=Request["id"] %>',
                    GridData: postData
                },
                 function(data) {
                    if (data == "" || data == null) {
                        parent.$.ligerDialog.close();
                         return true;
                     }
                     else {
                         $.ligerDialog.warn(data);
                         return false;
                     }
                 },
                 'text'
                 );
        }

        function btCancel_onclick() {
            $.post(
                'SetTButton.aspx',
                {
                    id: '<%=Request["id"] %>',
                    Action: 'Cancel'
                },
                 function(data) {
                     if (data == "" || data == null) {
                         //parent.grid.loadData(true);
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

    </script>
</head>
<body style="padding:6px; overflow:hidden;"> 
  <div id="toptoolbar"></div> 
    <div id="gridTable" style="margin:0; padding:0"></div>
    
<form id="form1" runat="server">
<div>    
    标题：<asp:TextBox ID="txtCaption" runat="server"></asp:TextBox>
    Url：<asp:TextBox ID="txtUrl" runat="server"></asp:TextBox>
    <asp:Button ID="btAdd" runat="server" Text="添加" onclick="btAdd_Click" 
        Width="67px" />
</div>
<br />
<div>
    <input id="btOk" type="button" value="确定" style="width:60px" onclick="return btOk_onclick()" />&nbsp;&nbsp;&nbsp;&nbsp;
    <input id="btCancel" type="button" value="取消" style="width:60px" onclick="return btCancel_onclick()" />
</div>
</form>
</body>
</html>
