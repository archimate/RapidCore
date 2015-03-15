<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectMenu.aspx.cs" Inherits="Desktop_SelectMenu" %>
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
    <script src="../lib/ligerUI/js/plugins/ligerMenu.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerMenuBar.js" type="text/javascript"></script>
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
            //树
            $("#tree1").ligerTree({
                url: 'SelectMenu.aspx?Action=GetData&pid=<%=Request["pid"] %>',
                checkbox: false,
                slide: false,
                nodeWidth: 120,
                attribute: ['nodename', 'url'],
                onSelect: function(node) {
                    actionNode = node;
                    if (node.data.mtype != "0") {
                        parent.$.ligerDialog.close();
                        parent.f_open(node.data.url, node.data.text, node.data.icon, node.data.mtype, 0, 0);
                    }
                },
                onBeforeExpand: function(node) {
                }
            });

            tree = $("#tree1").ligerGetTreeManager();
        });

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
   <ul id="tree1">   </ul>
</form>
</body>
</html>
