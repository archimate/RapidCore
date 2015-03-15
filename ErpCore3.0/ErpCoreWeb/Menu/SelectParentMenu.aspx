<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectParentMenu.aspx.cs" Inherits="Menu_SelectParentMenu" %>
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
                        url: 'Menu/SelectParentMenu.aspx?Action=GetMenu',
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
                        tree.loadData(node.target, 'SelectParentMenu.aspx',
                        {
                            Action: 'GetMenu',
                            pid: node.data.id
                        }
                        );
                    }

                }
            });

            tree = $("#tree1").ligerGetTreeManager();
        });


        function reloadActionNodeChildren() {
            if (actionNode == null) return;
            var len = actionNode.data.children.length;
            for (var i = 0; i < len; i++)
                tree.remove(actionNode.data.children[i]);

            tree.loadData(actionNode.target, 'SelectParentMenu.aspx',
                    {
                        Action: 'GetMenu',
                        pid: actionNode.data.id
                    }
                    );
        }
        function updateActionNode() {
            if (actionNode == null) return;
            $.get("SelectParentMenu.aspx",
                {
                    Action: 'GetMenuName',
                    id: actionNode.data.id
                },
                function(result) {
                    tree.update(actionNode.target, { text: result });
                });
            }

        function onSelect() {
            if (actionNode == null)
                return null;
            return actionNode.data;
        }
    </script>
</head>
<body  style="padding:5px">
   <ul id="tree1">   </ul> 
    <div style="display:none"/>

</body>
</html>
