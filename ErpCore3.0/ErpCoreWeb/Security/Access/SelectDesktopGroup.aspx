<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectDesktopGroup.aspx.cs" Inherits="Security_Access_SelectDesktopGroup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="../../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <script src="../../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script> 
    <script src="../../lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script> 
    
    <script type="text/javascript">
        var grid;
        $(function() {
            grid = $("#gridTable").ligerGrid({
                columns: [
                { display: '名称', name: 'Name',  width: 120 }
                ],
                url: 'SelectDesktopGroup.aspx?Action=GetData',
                dataAction: 'server', pageSize: 30,
                width: '100%', height: '100%'
                
            });
        });


        function onSelect() {
            return grid.getSelectedRow();
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
    <div id="gridTable" style="margin:0; padding:0"></div>
</body>
</html>
