<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewWorkflow.aspx.cs" Inherits="Workflow_ViewWorkflow" %>

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
    <script src="../lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerTextBox.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerDateEditor.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerSpinner.js" type="text/javascript"></script> 
    <script type="text/javascript">
        
        $(function() {
            $("#toptoolbar").ligerToolBar({ items: [
                { text: '撤销', click: onCancelWF }
            ]
            });
        });

        function onCancelWF() {
            var row = grid.getSelectedRow();
            if (row == null) {
                $.ligerDialog.alert('请选择工作流！');
                return false;
            }
            $.post(
                'ViewWorkflow.aspx',
                {
                    Action:'CancelWF',
                    TbCode: '<%=Request["TbCode"] %>',
                    id: '<%=Request["id"] %>',
                    ParentId:'<%=Request["ParentId"] %>',
                    WF_Workflow_id: row.id
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
        }


        $(function() {
            $("#toptoolbar2").ligerToolBar({ items: [
                { text: '审批', click: onApproval }
            ]
            });
        });

        function onApproval() {
            var row = grid.getSelectedRow();
            if (row == null) {
                $.ligerDialog.alert('请选择工作流！');
                return false;
            }
            $.post(
                'ViewWorkflow.aspx',
                {
                    Action: 'CanApproval',
                    TbCode: '<%=Request["TbCode"] %>',
                    id: '<%=Request["id"] %>',
                    ParentId: '<%=Request["ParentId"] %>',
                    WF_Workflow_id: row.id
                },
                 function(data) {
                     if (data == "" || data == null) {
                         $.ligerDialog.open({ title: '审批', url: 'ApprovalActives.aspx?TbCode=<%=Request["TbCode"] %>&ParentId=<%=Request["ParentId"] %>&id=<%=Request["id"] %>&WF_Workflow_id=' + row.id, name: 'winApproval', height: 250, width: 500, showMax: true, showToggle: true, isResize: true, modal: false, slide: false,
                             buttons: [
                            { text: '接受', onclick: function(item, dialog) {
                                var ret = document.getElementById('winApproval').contentWindow.onAccept();
                            }
                            },
                            { text: '拒绝', onclick: function(item, dialog) {
                                var ret = document.getElementById('winApproval').contentWindow.onReject();
                            }
                            },
                            { text: '取消', onclick: function(item, dialog) {
                                $.ligerDialog.close();
                            }
                            }
                         ], isResize: true
                         });
                     }
                     else {
                         $.ligerDialog.warn(data);
                         return false;
                     }
                 },
                 'text');
        }
    </script>
    
    <script type="text/javascript">
        var grid;
        $(function() {
        grid = $("#gridWorkflow").ligerGrid({
                columns: [
                { display: '名称', name: 'Name', width: 80 },
                { display: '启动时间', name: 'Created', width: 120, align: 'left' },
                { display: '审批状态', name: 'State', width: 120, align: 'left' }
                ],
                url: 'ViewWorkflow.aspx?Action=GetData&TbCode=<%=Request["TbCode"] %>&ParentId=<%=Request["ParentId"] %>&id=<%=Request["id"] %>',
                dataAction: 'server',
                usePager: false,
                width: '100%', height: '160px',
                onSelectRow: function(data, rowindex, rowobj) {
                    LoadGridActives(data.id);
                }
            });

        });
        
        var grid2;
        $(function() {
        grid2 = $("#gridActives").ligerGrid({
        columns: [
                { display: '活动名称', name: 'Name', width: 80 },
                { display: '审批结果', name: 'Result', width: 80 },
                { display: '审批内容', name: 'Comment', width: 120, align: 'left' },
                { display: '审批人', name: 'UserName', width: 80, align: 'left' },
                { display: '审批角色', name: 'RoleName', width: 80, align: 'left' }
                ],
                url: '',
                dataAction: 'server',
                usePager: false,
                width: '100%', height: '200px',
                onSelectRow: function(data, rowindex, rowobj) {
                    //$.ligerDialog.alert('1选择的是' + data.id);
                }
            });

        });

        function LoadGridActives(WorkflowId) {
            var url = "ViewWorkflow.aspx?Action=GetActivesData&WF_Workflow_id=" + WorkflowId + '&TbCode=<%=Request["TbCode"] %>&ParentId=<%=Request["ParentId"] %>&id=<%=Request["id"] %>';
            grid2.set({ url: url});
            //grid2.loadData();
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

  <div id="toptoolbar"></div> 
   <div id="gridWorkflow" style="margin:0; padding:0"></div>
    
  <div id="toptoolbar2"></div> 
   <div id="gridActives" style="margin:0; padding:0"></div>
</body>
</html>
