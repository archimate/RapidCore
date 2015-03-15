<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectCatalog.aspx.cs" Inherits="Workflow_SelectCatalog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <script src="../lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerTree.js" type="text/javascript"></script>
    <script type="text/javascript">
        var tree = null;
        $(function ()
        {
            $("#treeCatalog").ligerTree({
            url: 'SelectCatalog.aspx?Action=GetData&B_Company_id=<%=Request["B_Company_id"] %>',
                checkbox :false,
                onBeforeExpand: function(node) {
                    if (node.data.children && node.data.children.length == 0) {
                        tree.loadData(node.target, 'SelectCatalog.aspx',
                        {
                            Action: 'GetData',
                            pid: node.data.id,
                            B_Company_id:'<%=Request["B_Company_id"] %>'
                        }
                        );
                    }
                }
            });
            tree = $("#treeCatalog").ligerGetTreeManager();
        });


        function onSelect() {
            return tree.getSelected().data;
        }
    </script>
</head>
<body style="padding:10px"> 
    <div style="width:300px; height:300px; border:1px solid #ccc; overflow:auto; clear:both;">
    <ul id="treeCatalog">   </ul>
    </div>

        <div style="display:none">
     
    </div>
</body>
</html>
