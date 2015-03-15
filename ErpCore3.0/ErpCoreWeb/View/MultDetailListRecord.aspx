<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MultDetailListRecord.aspx.cs" Inherits="View_MultDetailListRecord" %>
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

        //根据字段类型确定窗体宽度
        var winWidth = 500;
        var winHeight=320;
        
        function onAdd() {
            $.ligerDialog.open({ title: '新建', url: 'AddMultDetailRecord.aspx?vid=<%=Request["vid"] %>&vdid=<%=Request["vdid"] %>', name: 'winAddRec', height: winHeight, width: winWidth, showMax: true, showToggle: true,  isResize: true, modal: false, slide: false, 
            buttons: [
                { text: '确定', onclick: function(item, dialog) {
                    var ret = document.getElementById('winAddDetailRec').contentWindow.onSubmit();
                }
                },
                { text: '取消', onclick: function(item, dialog) {
                    var ret = document.getElementById('winAddDetailRec').contentWindow.onCancel();
                } }
             ], isResize: true
            });
        }
        function onOkAddMultDetailRecord() {
            grid.loadData(true);
            $.ligerDialog.close();
        }
        function onCancelAddMultDetailRecord() {
            grid.loadData(true);
            $.ligerDialog.close();
        }
        function onEdit() {
            var row = grid.getSelectedRow();
            if (row == null) {
                $.ligerDialog.alert('请选择行!');
                return;
            }
            $.ligerDialog.open({ title: '修改', url: 'EditMultDetailRecord.aspx?vid=<%=Request["vid"] %>&vdid=<%=Request["vdid"] %>&id=' + row.id, name: 'winEditRec', height: winHeight, width: winWidth, showMax: true, showToggle: true,  isResize: true, modal: false, slide: false, 
            buttons: [
                { text: '确定', onclick: function(item, dialog) {
                    var ret = document.getElementById('winEditDetailRec').contentWindow.onSubmit();
                }
                },
                { text: '取消', onclick: function(item, dialog) {
                    var ret = document.getElementById('winEditDetailRec').contentWindow.onCancel();
                } }
             ], isResize: true
            });
        }
        function onOkEditMultDetailRecord() {
            grid.loadData(true);
            $.ligerDialog.close();
        }
        function onCancelEditMultDetailRecord() {
            grid.loadData(true);
            $.ligerDialog.close();
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
                    'MultDetailListRecord.aspx',
                    {
                        Action: 'Delete',
                        vid: '<%=Request["vid"] %>',
                        vdid: '<%=Request["vdid"] %>',
                        delid: row.id
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
            grid = $("#gridDetail").ligerGrid({
            columns: [
                <%
                List<CBaseObject> lstObjCIVD=m_ViewDetail.ColumnInViewDetailMgr.GetList();
                for (int i=0;i<lstObjCIVD.Count;i++)
                {
                    CColumnInViewDetail civd = (CColumnInViewDetail)lstObjCIVD[i];
                    CColumn col = (CColumn)m_Table.ColumnMgr.Find(civd.FW_Column_id);
                    if (col == null)
                        continue;
                    if(i<lstObjCIVD.Count-1)
                        Response.Write(string.Format("{{ display: '{0}', name: '{1}'}},",col.Name,col.Code));
                    else
                        Response.Write(string.Format("{{ display: '{0}', name: '{1}'}}",col.Name,col.Code));
                }
                 %>
                ],
                url: 'MultDetailListRecord.aspx?Action=GetData&vid=<%=Request["vid"] %>&vdid=<%=Request["vdid"] %>&pid=<%=Request["pid"] %>',
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
<div id="topmenu"></div> 
  <div id="toptoolbar"></div> 
   <div id="gridTable" style="margin:0; padding:0"></div>
   <div id="gridDetail" style="margin:0; padding:0"></div>

</body>
</html>
