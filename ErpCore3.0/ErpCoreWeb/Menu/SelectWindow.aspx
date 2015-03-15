<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectWindow.aspx.cs" Inherits="Menu_SelectWindow" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script> 
    <script src="../lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script> 
    
    <script type="text/javascript">
        var grid;
        $(function() {
            grid = $("#gridTable").ligerGrid({
                columns: [
                { display: '名称', name: 'Name', align: 'left', width: 120 }
                ],
                url: 'SelectWindow.aspx?Action=GetData',
                dataAction: 'server', pageSize: 30,
                width: '100%', height: '100%'
                
            });
        });


        function onSelect() {
            return grid.getSelectedRow();
        }
    </script>
</head>
<body style="padding:6px; overflow:hidden;"> 
    <div id="gridTable" style="margin:0; padding:0"></div>
</body>
</html>
