<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TableWindow.aspx.cs" Inherits="Window_TableWindow" %>
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
                { text: '工作流', click: onWorkflow }
            ]
            });
        });
        //根据字段类型确定窗体宽度
        var winWidth = 600;
        var winHeight=320;

        function onAdd() {
            $.ligerDialog.open({ title: '新建', url: 'Add.aspx?tid=<%=Request["tid"] %>', name: 'winAddRec', height: winHeight, width: winWidth, showMax: true, modal: false, 
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
        function onOkAdd() {
            grid.loadData(true);
            $.ligerDialog.close();
        }
        function onCancelAdd() {
            grid.loadData(true);
            $.ligerDialog.close();
        }
        function onEdit() {
            var row = grid.getSelectedRow();
            if (row == null) {
                $.ligerDialog.alert('请选择行!');
                return;
            }
            $.ligerDialog.open({ title: '修改', url: 'Edit.aspx?tid=<%=Request["tid"] %>&id=' + row.id, name: 'winEditRec', height: winHeight, width: winWidth, showMax: true,  modal: false, 
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
        function onOkEdit() {
            grid.loadData(true);
            $.ligerDialog.close();
        }
        function onCancelEdit() {
            grid.loadData(true);
            $.ligerDialog.close();
        }
        function onDelete() {
            var row = grid.getSelectedRow();
            if (row == null) {
                $.ligerDialog.alert('请选择行!');
                return;
            }
            if (row.IsSystem=='True') {
                $.ligerDialog.warn('系统表不能删除！');
                return false;
            }
            $.ligerDialog.confirm('确认删除？', function(yes) {
                if (yes) {
                    $.post(
                    'TableWindow.aspx',
                    {
                        Action: 'Delete',
                        tid:'<%=Request["tid"] %>',
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
        var dlgMenuWorkflow;
        function onWorkflow() {
            var row = grid.getSelectedRow();
            if (row == null) {
                $.ligerDialog.alert('请选择行!');
                return;
            }
            if (dlgMenuWorkflow == null) {
                dlgMenuWorkflow = $.ligerDialog.open({ target: $("#menuWorkflow"), width: 130, modal: false });

                $(".l-dialog-close").bind('mousedown', function()  //dialog右上角的叉
                {
                    dlgMenuWorkflow.hide();
                });
            }
            else {
                dlgMenuWorkflow.show();
            }
        }
        function onStartWF() {
            var row = grid.getSelectedRow();
            if (row == null) {
                $.ligerDialog.alert('请选择行!');
                return;
            }
            dlgMenuWorkflow.hide();
            $.ligerDialog.open({ title: '选择工作流', url: '../Workflow/SelectWorkflowDef.aspx?B_Company_id=<%=GetCompanyId() %>&FW_Table_id=<%=m_Table.Id %>', name: 'winSelWF', height: 200, width: 300, showMax: true, showToggle: true, showMin: true, isResize: true, modal: false, slide: false,
                buttons: [
                { text: '确定', onclick: function(item, dialog) {
                    var fn = dialog.frame.onSelect || dialog.frame.window.onSelect;
                    var data = fn();
                    if (!data) {
                        $.ligerDialog.alert('请选择行!');
                        return false;
                    }
                    $.post(
                    '../Workflow/StartWorkflow.aspx',
                    {
                        TbCode: '<%=m_Table.Code %>',
                        id: row.id,
                        WF_WorkflowDef_id: data.id,
                        ParentId: '<%=Request["ParentId"]%>'
                    },
                     function(data) {
                         if (data == "" || data == null) {
                             grid.loadData(true);
                         }
                         else {
                             $.ligerDialog.warn(data);
                             return false;
                         }
                     },
                    'text');
                    dialog.close();
                }
                },
                { text: '取消', onclick: function(item, dialog) {
                    dialog.close();
                }
                }
             ], isResize: true
            });
        }
        function onViewWF() {
            var row = grid.getSelectedRow();
            if (row == null) {
                $.ligerDialog.alert('请选择行!');
                return;
            }
            dlgMenuWorkflow.hide();
            $.ligerDialog.open({ title: '查看工作流', url: '../Workflow/ViewWorkflow.aspx?TbCode=<%=m_Table.Code %>&ParentId=<%=Request["ParentId"] %>&id=' + row.id, name: 'winViewWF', height: 400, width: 600, showMax: true, showToggle: true, isResize: true, modal: false, slide: false
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
                    if(i<lstObjC.Count-1)
                        Response.Write(string.Format("{{ display: '{0}', name: '{1}'}},",col.Name,col.Code));
                    else
                        Response.Write(string.Format("{{ display: '{0}', name: '{1}'}}",col.Name,col.Code));
                }
                 %>
                ],
                url: 'TableWindow.aspx?Action=GetData&tid=<%=Request["tid"] %>',
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

<div id="menuWorkflow" style=" margin:3px; display:none;">
    <input id="btStartWF" type="button" value="启动工作流" onclick="onStartWF()" style="width:100px; height:30px" /><br />
    <input id="btViewWF" type="button" value="查看工作流" onclick="onViewWF()" style="width:100px; height:30px" />
</div>
</body>
</html>
