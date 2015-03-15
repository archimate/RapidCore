<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MenuPanel.aspx.cs" Inherits="Menu_MenuPanel" %>
<%@ Import Namespace="ErpCoreModel" %>
<%@ Import Namespace="ErpCoreModel.Base" %>
<%@ Import Namespace="ErpCoreModel.Framework" %>
<%@ Import Namespace="System.Collections.Generic" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/core/base.js" type="text/javascript"></script> 
    <script src="../lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerResizable.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerMenu.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerMenuBar.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerTree.js" type="text/javascript"></script>
    <script type="text/javascript">
        
        
        $(function() {
            $("#toptoolbar").ligerToolBar({ items: [
                { text: '增加', click: onAdd, icon: 'add' },
                { line: true },
                { text: '修改', click: onEdit, icon: 'modify' },
                { line: true },
                { text: '删除', click: onDelete, icon: 'delete' }
            ]
            });
        });

        var win;
        function onAdd() {
            if (actionNode == null) {
                $.ligerDialog.warn("请选择父级菜单！");
                return;
            }
            var pid = "";
            if (actionNode.data.id != null) {
                pid = actionNode.data.id;
            }
            win=$.ligerDialog.open({ title: '新建', url: 'AddMenu.aspx?pid=' + pid, name: 'winAddRec', height: 350, width: 450, showMax: true, showToggle: true,  isResize: true, modal: false, slide: false,
                buttons: [
                { text: '确定', onclick: function(item, dialog) {
                    var ret = document.getElementById('winAddRec').contentWindow.onSubmit();
                }
                },
                { text: '取消', onclick: function(item, dialog) {
                    dialog.close();
                } 
                }
             ], isResize: true
            });
        }
        function onEdit() {
            if (actionNode == null) return;
            if (actionNode.data.id == null || actionNode.data.id == "") {
                $.ligerDialog.warn("请选择菜单！");
                return;
            }
            win=$.ligerDialog.open({ title: '修改', url: 'EditMenu.aspx?id=' + actionNode.data.id, name: 'winEditRec', height: 350, width: 450, showMax: true, showToggle: true,  isResize: true, modal: false, slide: false, 
            buttons: [
                { text: '确定', onclick: function(item, dialog) {

                    var ret = document.getElementById('winEditRec').contentWindow.onSubmit();
                }
                },
                { text: '取消', onclick: function(item, dialog) {
                    dialog.close();
                } }
             ], isResize: true
            });
        }
        function onDelete() {
            if (actionNode == null) return;
            if (actionNode.data.id == null || actionNode.data.id == "") {
                $.ligerDialog.warn("请选择菜单！");
                return;
            }
            $.ligerDialog.confirm('确认删除？', function(yes) {
                if (yes) {
                    $.post(
                    'MenuPanel.aspx',
                    {
                        Action: 'Delete',
                        delid: actionNode.data.id
                    },
                     function(data) {
                         if (data == "" || data == null) {
                             tree.remove(actionNode.target);
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
    </script>
    <style type="text/css">
    #menu1,.l-menu-shadow{top:30px; left:50px;}
    #menu1{  width:200px;}
    </style>
    
    <script type="text/javascript">


        var tree = null;
        var actionNode = null;
        $(function() {
            //树节点
            var treedata =
                [
                    { isexpand: "false", text: "菜单", name: "nodeMenuRoot", id: "",
                        url: 'Menu/MenuPanel.aspx?Action=GetMenu',
                        children: []
                    }
                ];
            //树
            $("#tree1").ligerTree({
                data: treedata,
                checkbox: false,
                slide: false,
                nodeWidth: 120,
                attribute: ['nodename', 'url'],
                onSelect: function(node) {
                    actionNode = node;
                },
                onBeforeExpand: function(node) {
                    if (node.data.children && node.data.children.length == 0) {
                        tree.loadData(node.target, 'MenuPanel.aspx',
                        {
                            Action: 'GetMenu',
                            pid: node.data.id
                        }
                        );
                    }

                },
                onContextmenu: function(node, e) {
                    actionNode = node;
                    menuMenu.show({ top: e.pageY, left: e.pageX });
                    return false;
                }
            });

            tree = $("#tree1").ligerGetTreeManager();
        });


        function reloadActionNodeChildren() {
            if (actionNode == null) return;
            var len = actionNode.data.children.length;
            for (var i = 0; i < len; i++)
                tree.remove(actionNode.data.children[i]);

            tree.loadData(actionNode.target, 'MenuPanel.aspx',
                    {
                        Action: 'GetMenu',
                        pid: actionNode.data.id
                    }
                    );
        }
        function updateActionNode() {
            if (actionNode == null) return;
            $.get("MenuPanel.aspx",
                {
                    Action: 'GetMenuName',
                    id: actionNode.data.id
                },
                function(result) {
                    tree.update(actionNode.target, { text: result });
                });
        }
    </script>
</head>
<body  style="padding:5px"> 
  <div id="toptoolbar"></div> 
   <div style="overflow: auto">
   <ul id="tree1">   </ul> 
   </div>
    <div style="display:none"/>

</body>
</html>
