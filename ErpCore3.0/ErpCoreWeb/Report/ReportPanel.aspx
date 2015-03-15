<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportPanel.aspx.cs" Inherits="Report_ReportPanel" %>
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
    <script src="../lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script> 
    <script src="../lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerMenu.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerMenuBar.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
    <script type="text/javascript">
        
        $(function() {
            $("#toptoolbar").ligerToolBar({ items: [
                { text: '增加', click: onAdd, icon: 'add' },
                { line: true },
                { text: '修改', click: onEdit, icon: 'modify' },
                { line: true },
                { text: '删除', click: onDelete, icon: 'delete' },
                { line: true },
                { text: '运行报表', click: onRunReport }
            ]
            });
        });

        function onRunReport() {
            var row = grid.getSelectedRow();
            if (row == null) {
                $.ligerDialog.alert('请选择行!');
                return;
            }
            parent.$.ligerDialog.open({ title: '报表', url: 'Report/RunReport.aspx?id=' + row.id + '&B_Company_id=<%=Request["B_Company_id"] %>', name: 'winRun', height: 600, width: 1000, showMax: true, showToggle: true, modal: false, slide: false
                , isResize: true
            });
        }
        function onAdd() {
            $.ligerDialog.open({ title: '新建', url: 'AddReport.aspx?catalog_id=<%=Request["catalog_id"] %>&B_Company_id=<%=Request["B_Company_id"] %>', name: 'winAddRec', height: 460, width: 600, showMax: true, showToggle: true,  isResize: true, modal: false, slide: false, 
            buttons: [
                { text: '确定', onclick: function(item, dialog) {
                    var ret = document.getElementById('winAddRec').contentWindow.onSubmit();
                }
                },
                { text: '取消', onclick: function(item, dialog) {
                    var ret = document.getElementById('winAddRec').contentWindow.onCancel();
                } }
             ], isResize: true
            });
        }
        function onEdit() {
            var row = grid.getSelectedRow();
            if (row == null) {
                $.ligerDialog.alert('请选择行!');
                return;
            }
            $.ligerDialog.open({ title: '修改', url: 'EditReport.aspx?catalog_id=<%=Request["catalog_id"] %>&id=' + row.id + '&B_Company_id=<%=Request["B_Company_id"] %>', name: 'winEditRec', height: 460, width: 600, showMax: true, showToggle: true,  isResize: true, modal: false, slide: false,  
            buttons: [
                { text: '确定', onclick: function(item, dialog) {

                    var ret = document.getElementById('winEditRec').contentWindow.onSubmit();
                }
                },
                { text: '取消', onclick: function(item, dialog) {
                    var ret = document.getElementById('winEditRec').contentWindow.onCancel();
                } }
             ], isResize: true
            });
        }
        function onDelete() {
            var row = grid.getSelectedRow();
            if (row == null) {
                $.ligerDialog.alert('请选择行!');
                return;
            }
            $.ligerDialog.confirm('确认删除？', function(yes) {
                if (yes) {
                    $.post(
                    'ReportPanel.aspx',
                    {
                        Action: 'Delete',
                        catalog_id: '<%=Request["catalog_id"] %>',
                        delid: row.id,
                        B_Company_id:'<%=Request["B_Company_id"] %>'
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
                <%
                List<CBaseObject> lstObjC=m_Table.ColumnMgr.GetList();
                for (int i=0;i<lstObjC.Count;i++)
                {
                    CColumn col = (CColumn)lstObjC[i];
                    if (col.Code.Equals("id", StringComparison.OrdinalIgnoreCase))
                        continue;
                    else if (col.Code.Equals("Created", StringComparison.OrdinalIgnoreCase))
                        continue;
                    else if (col.Code.Equals("Creator", StringComparison.OrdinalIgnoreCase))
                        continue;
                    else if (col.Code.Equals("Updated", StringComparison.OrdinalIgnoreCase))
                        continue;
                    else if (col.Code.Equals("Updator", StringComparison.OrdinalIgnoreCase))
                        continue;
                    if(i<lstObjC.Count-1)
                        Response.Write(string.Format("{{ display: '{0}', name: '{1}'}},",col.Name,col.Code));
                    else
                        Response.Write(string.Format("{{ display: '{0}', name: '{1}'}}",col.Name,col.Code));
                }
                 %>
                ],
                url: 'ReportPanel.aspx?Action=GetData&catalog_id=<%=Request["catalog_id"] %>&B_Company_id=<%=Request["B_Company_id"] %>',
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
