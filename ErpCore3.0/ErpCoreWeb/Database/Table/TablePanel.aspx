<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TablePanel.aspx.cs" Inherits="Database_Table_TablePanel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head >
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
    <script type="text/javascript">
        
        $(function() {
            $("#toptoolbar").ligerToolBar({ items: [
                { text: '增加', click: onAdd, icon: 'add' },
                { line: true },
                { text: '修改', click: onEdit, icon: 'modify' },
                { line: true },
                { text: '删除', click: onDelete, icon: 'delete' },
                { line: true },
                { text: '数据', click: onData }
            ]
            });
        });

        function onAdd() {
            $.ligerDialog.open({ title: '新建', url: 'Add.aspx', name: 'winAdd', height: 350, width: 700, showMax: true, showToggle: true, isResize: true, modal: false, slide: false, 
            buttons: [
                { text: '确定', onclick: function(item, dialog) {
                    var ret = document.getElementById('winAdd').contentWindow.onSubmit();
                    //alert(ret);
                }
                },
                { text: '取消', onclick: function(item, dialog) { dialog.close(); } }
             ], isResize: true
            });
        }
        function onEdit() {
            var row = grid.getSelectedRow();
            if (row == null) {
                $.ligerDialog.alert('请选择行!');
                return;
            }
            $.ligerDialog.open({ title: '修改', url: 'Edit.aspx?id=' + row.id, name: 'winEdit', height: 350, width: 700, showMax: true, showToggle: true, isResize: true, modal: false, slide: false, 
            buttons: [
                { text: '确定', onclick: function(item, dialog) {

                    var ret = document.getElementById('winEdit').contentWindow.onSubmit();
                    //alert(ret);
                }
                },
                { text: '取消', onclick: function(item, dialog) { dialog.close(); } }
             ], isResize: true
            });
        }
        function onDelete() {
            var row = grid.getSelectedRow();
            if (row == null) {
                $.ligerDialog.alert('请选择行!');
                return;
            }
            if (row.IsSystem=='True') {
                $.ligerDialog.warn('系统表不能删除！');
                return false;
            }
            $.ligerDialog.confirm('确认删除？', function(yes) {
                if (yes) {
                    $.post(
                    'TablePanel.aspx',
                    {
                        Action: 'Delete',
                        id: row.id
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
            });
        }
        function onData() {
            var row = grid.getSelectedRow();
            if (row == null) {
                $.ligerDialog.alert('请选择行!');
                return;
            }
            var tabid = row.id; //指定id
            parent.f_addTab(tabid, row.Name, 'Window/TableWindow.aspx?tid=' + row.id);
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
                { display: '名称', name: 'Name', align: 'left', width: 120 },
                { display: '编码', name: 'Code', align: 'left', minWidth: 60 },
                { display: '系统表', name: 'IsSystem', width: 50, align: 'left' },
                { display: '是否完成', name: 'IsFinish', minWidth: 140 },
                { display: '创建时间', name: 'Created', minWidth: 140 }
                ],
                url: 'TablePanel.aspx?Action=GetData',
                dataAction: 'server', pageSize: 30,
                width: '100%', height: '100%',
                onSelectRow: function (data, rowindex, rowobj)
                {
                    //$.ligerDialog.alert('1选择的是' + data.id);
                }
            });
        });
        
    </script>
</head>
<body style="padding:6px; overflow:hidden;"> 
  <div id="toptoolbar"></div> 
   <div id="gridTable" style="margin:0; padding:0"></div>

</body>
</html>
