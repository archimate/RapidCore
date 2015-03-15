<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectWorkflowDef.aspx.cs" Inherits="Workflow_SelectWorkflowDef" %>

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
                { display: '名称', name: 'Name', align: 'left', width: 200 }
                ],
                url: 'SelectWorkflowDef.aspx?Action=GetData&B_Company_id=<%=Request["B_Company_id"] %>&FW_Table_id=<%=Request["FW_Table_id"] %>',
                dataAction: 'server', usePager: false,
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
