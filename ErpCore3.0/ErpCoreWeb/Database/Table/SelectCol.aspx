<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectCol.aspx.cs" Inherits="Database_Table_SelectCol" %>

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
            grid = $("#gridCol").ligerGrid({
                columns: [
                { display: '名称', name: 'Name', align: 'left', width: 120 },
                { display: '编码', name: 'Code', minWidth: 60 },
                { display: '类型', name: 'ColType', width: 50, align: 'left' }
                ],
                url: 'SelectCol.aspx?Action=GetData&tid=<%=Request["tid"] %>',
                dataAction: 'server',
                usePager: false,
                width: '100%', height: '100%'

            });
        });


        function onSelect() {
            return grid.getSelectedRow();
        }
    </script>
</head>
<body style="padding:6px; overflow:hidden;"> 
    
    <div id="gridCol" style="margin:0; padding:0"></div>
</body>
</html>
