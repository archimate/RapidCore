<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SingleViewInfo3.aspx.cs" Inherits="View_SingleViewInfo3" %>

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
                { text: '向下', click: onDown }
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
    </script>
    <script type="text/javascript">
        var grid;
        $(function() {
            grid = $("#gridTable").ligerGrid({
                columns: [
                { display: '列名', name: 'ColName', align: 'left', width: 120 },
                { display: '标题', name: 'Caption', align: 'left', width: 120, editor: { type: 'text'} }
                ],
                url: 'SingleViewInfo3.aspx?Action=GetData&id=<%=Request["id"] %>&catalog_id=<%=Request["catalog_id"] %> ',
                dataAction: 'server',
                usePager: false,
                enabledEdit: true,
                width: '100%', height: '80%',
                onBeforeEdit: function(e) {
                },
                onBeforeSubmitEdit: function(e) {
                },
                onAfterEdit: function(e) {
                }
            });
        });


        
        function btPrev_onclick() {
            window.location.href = 'SingleViewInfo2.aspx?id=<%=Request["id"] %>&catalog_id=<%=Request["catalog_id"] %> ';
        }

        function btNext_onclick() {
            var postData = "";
            var rowData = grid.getData();
            for (var idx = 0; idx < rowData.length; idx++) {
                var id, caption;
                $.each(rowData[idx], function(key, val) {
                    if (key == "id")
                        id = val;
                    else if (key == "Caption")
                        caption = val;
                });
                postData += id + ":" + caption;
                postData += ";";
            }
            //提交
            $.post(
                'SingleViewInfo3.aspx',
                {
                    Action: 'PostData',
                    id: '<%=Request["id"] %>',
                    catalog_id: '<%=Request["catalog_id"] %>',
                    GridData: postData
                },
                 function(data) {
                     if (data == "" || data == null) {
                         window.location.href = 'SingleViewInfo4.aspx?id=<%=Request["id"] %>&catalog_id=<%=Request["catalog_id"] %> ';
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
                'SingleViewInfo3.aspx',
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
</head>
<body style="padding:6px; overflow:hidden;"> 
    <div>排序：</div>
    <div>主表：<input id="txtTbName" name="txtTbName" value="<%=m_Table.Name %>" disabled="disabled" /></div>
  <div id="toptoolbar"></div> 
    <div id="gridTable" style="margin:0; padding:0"></div>
    <input id="btPrev" type="button" value="上一步" style="width:60px" onclick="return btPrev_onclick()" />&nbsp;&nbsp;&nbsp;
    <input id="btNext" type="button" value="下一步" style="width:60px" onclick="return btNext_onclick()" />&nbsp;&nbsp;&nbsp;&nbsp;
    <input id="btCancel" type="button" value="取消" style="width:60px" onclick="return btCancel_onclick()" />
</body>
</html>
