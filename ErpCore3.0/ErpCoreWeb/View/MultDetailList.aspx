<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MultDetailList.aspx.cs" Inherits="View_MultDetailList" %>
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
    <script src="../lib/ligerUI/js/plugins/ligerResizable.js" type="text/javascript"></script>
    <script type="text/javascript">
        
    </script>
    <style type="text/css">
    #menu1,.l-menu-shadow{top:30px; left:50px;}
    #menu1{  width:200px;}
    </style>
    
    <script type="text/javascript">
                
        var grid;
        $(function ()
        {
            grid = $("#gridDetail").ligerGrid({
            columns: [
                <%
                List<CColumn> lstDetailCol=GetDetailColList();
                for (int i=0;i<lstDetailCol.Count;i++)
                {
                    CColumn col = lstDetailCol[i];
                    if(i<lstDetailCol.Count-1)
                        Response.Write(string.Format("{{ display: '{0}', name: '{1}',align: 'left'}},",col.Name,col.Code));
                    else
                        Response.Write(string.Format("{{ display: '{0}', name: '{1}',align: 'left'}}",col.Name,col.Code));
                }
                 %>
                ],
                url: 'MultDetailList.aspx?Action=GetData&vid=<%=Request["vid"] %>&vdid=<%=Request["vdid"] %>&pid=<%=Request["pid"] %>&ParentId=<%=Request["ParentId"] %>',
                dataAction: 'server', 
                usePager: false,
                width: '100%', height: '100%',
                onSelectRow: function (data, rowindex, rowobj)
                {
                    //$.ligerDialog.alert('1选择的是' + data.id);
                },
                onUnSelectRow: function (data, rowindex, rowobj)
                {
                    //alert('反选择的是' + data.id);
                }
            });
        });

    </script>
</head>
<body style="padding:6px; overflow:hidden;"> 
   <div id="gridTable" style="margin:0; padding:0"></div>
   <div id="gridDetail" style="margin:0; padding:0"></div>

</body>
</html>
