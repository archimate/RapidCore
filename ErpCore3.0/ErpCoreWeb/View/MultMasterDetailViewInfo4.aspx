<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MultMasterDetailViewInfo4.aspx.cs" Inherits="View_MultMasterDetailViewInfo4" %>
<%@ Import Namespace="ErpCoreModel" %>
<%@ Import Namespace="ErpCoreModel.Base" %>
<%@ Import Namespace="ErpCoreModel.Framework" %>
<%@ Import Namespace="ErpCoreModel.UI" %>
<%@ Import Namespace="System.Collections.Generic" %>
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
    <script type="text/javascript">
        $(function() {
            $("#toptoolbar").ligerToolBar({ items: [
                { text: '删除', click: onDelete, icon: 'delete' }
            ]
            });
        });

        function onDelete() {
            var row = grid.getSelectedRow();
            if (row == null) {
                $.ligerDialog.alert('请选择行!');
                return;
            }
            
            $.post(
            'MultMasterDetailViewInfo4.aspx',
            {
                Action: 'Delete',
                id: '<%=Request["id"] %>',
                catalog_id: '<%=Request["catalog_id"] %>',
                delid: row.id
            },
             function(data) {
                 if (data == "" || data == null) {
                     $.ligerDialog.close();
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

        function btPrev_onclick() {
            window.location.href = 'MultMasterDetailViewInfo3.aspx?id=<%=Request["id"] %>&catalog_id=<%=Request["catalog_id"] %> ';
        }

        function btFinish_onclick() {
            
            //提交
            $.post(
                'MultMasterDetailViewInfo4.aspx',
                {
                    Action: 'PostData',
                    id: '<%=Request["id"] %>',
                    catalog_id: '<%=Request["catalog_id"] %>'
                },
                 function(data) {
                     if (data == "" || data == null) {
                         parent.grid.loadData(true);
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
                'MultMasterDetailViewInfo4.aspx',
                {
                    id: '<%=Request["id"] %>',
                    catalog_id: '<%=Request["catalog_id"] %>',
                    Action: 'Cancel'
                },
                 function(data) {
                     if (data == "" || data == null) {
                         parent.grid.loadData(true);
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
    <style type="text/css">
    #menu1,.l-menu-shadow{top:30px; left:50px;}
    #menu1{  width:200px;}
    </style>
    
    <script type="text/javascript">
        var grid;
        $(function ()
        {
            grid = $("#gridTable").ligerGrid({
            columns: [
                { display: '与或', name: 'AndOr' },
                { display: '字段', name: 'Column' },
                { display: '符号', name: 'Sign' },
                { display: '值', name: 'Val' }
                ],
                url: 'MultMasterDetailViewInfo4.aspx?Action=GetData&id=<%=Request["id"] %>&catalog_id=<%=Request["catalog_id"] %>',
                dataAction: 'server',
                usePager: false,
                width: '100%', height: '80%',
                onSelectRow: function (data, rowindex, rowobj)
                {
                    //$.ligerDialog.alert('1选择的是' + data.id);
                }
            });
        });
        
    </script>
</head>
<body style="padding:6px; overflow:hidden;"> 
    <div>过滤条件：</div>
    <div>主表：<input id="txtTbName" name="txtTbName" value="<%=m_MTable.Name %>" disabled="disabled" /></div>
  <div id="toptoolbar"></div> 
   <div id="gridTable" style="margin:0; padding:0"></div>
<form id="form1" runat="server">
<div>
    <asp:DropDownList ID="cbAndOr" runat="server">
        <asp:ListItem Value="and">与</asp:ListItem>
        <asp:ListItem Value="or">或</asp:ListItem>
    </asp:DropDownList>
    <asp:DropDownList ID="cbColumn" runat="server">
    </asp:DropDownList>
    <asp:DropDownList ID="cbSign" runat="server">
        <asp:ListItem>=</asp:ListItem>
        <asp:ListItem>&gt;</asp:ListItem>
        <asp:ListItem>&lt;</asp:ListItem>
        <asp:ListItem>&gt;=</asp:ListItem>
        <asp:ListItem>&lt;=</asp:ListItem>
        <asp:ListItem>!=</asp:ListItem>
        <asp:ListItem>like</asp:ListItem>
    </asp:DropDownList>
    <asp:TextBox ID="txtVal" runat="server"></asp:TextBox>
    <asp:Button ID="btAdd" runat="server" Text="添加" onclick="btAdd_Click" 
        Width="67px" />
</div>
<div>
    <input id="btPrev" type="button" value="上一步" style="width:60px" onclick="return btPrev_onclick()" />&nbsp;&nbsp;&nbsp;
    <input id="btFinish" type="button" value="完成" style="width:60px" onclick="return btFinish_onclick()" />&nbsp;&nbsp;&nbsp;&nbsp;
    <input id="btCancel" type="button" value="取消" style="width:60px" onclick="return btCancel_onclick()" />
</div>
</form>
</body>
</html>
