<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SingleView.aspx.cs" Inherits="View_SingleView" %>
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
    <script src="../lib/jquery/jquery.PrintArea.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script> 
    <script src="../lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerMenu.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerMenuBar.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerResizable.js" type="text/javascript"></script> 
    <script type="text/javascript">
        $(function() {
            $("#toptoolbar").ligerToolBar({ items: [
                { text: '增加', click: onAdd, icon: 'add' },
                { text: '修改', click: onEdit, icon: 'modify' },
                { text: '删除', click: onDelete, icon: 'delete' },
                { text: '查看', click: onShow },
                { line: true },
                { text: '打印', click: onPrint, icon: 'print' },
                { text: '导出', click: onOutPut },
                { line: true },
                { text: '过滤', click: onFilter },
                { line: true },
                { text: '刷新', click: onRefresh, icon: 'refresh'},
                <%if(Request["Workflow"]=="1"){ %>
                { line: true },
                { text: '工作流', click: onWorkflow, icon: 'settings' },
                <%} %>
                { line: true }
                <%=GetTButtonInView() %>
            ]
            });
        });
        
        function onOutPut() {
            $.post(
            'SingleView.aspx',
            {
                Action: 'OutPut',
                page:<%=m_iCurPage %>,
                pagesize:<%=m_iCurPageSize %>,
                vid: '<%=Request["vid"] %>',
                ParentId: '<%=Request["ParentId"] %>'
            },
            function(data) {
                if (data == "" || data == null) {
                    $.ligerDialog.warn("导出失败！");
                    return true;
                }
                else {
                    $.ligerDialog.open({ title: '导出', url: 'OutXls.aspx?file=' + data, name: 'winOutPut', height: winHeight, width: winWidth, showMax: true, showToggle: true, isResize: true, modal: false, slide: false, 
                    buttons: [
                        { text: '确定', onclick: function(item, dialog) {
                            dialog.close();
                        }
                        }
                     ], isResize: true
                    });
                    return false;
                }
            },
            'text');
        }
        //查看
        function onShow() {
            var row = grid.getSelectedRow();
            if (row == null) {
                $.ligerDialog.alert('请选择行!');
                return;
            }
            $.ligerDialog.open({ title: '查看', url: 'ShowSingleViewRecord.aspx?vid=<%=Request["vid"] %>&ParentId=<%=Request["ParentId"] %>&id=' + row.id, name: 'winShow', height: winHeight, width: winWidth, showMax: true, showToggle: true, isResize: true, modal: false, slide: false, 
            buttons: [
                { text: '确定', onclick: function(item, dialog) {
                    dialog.close();
                }
                }
             ], isResize: true
            });
        }
        //打印
        function onPrint(){
            $("#gridTable").printArea();
        }
        
        //其他工具栏按钮
        function onTButton(item) {
            var caption=item.text;  
            var url=item.url;
            $.ligerDialog.open({ title: caption, url: url, name: 'winTButton', height: 350, width: 500, showMax: true, showToggle: true, isResize: true, modal: false, slide: false
            });
        }
        //刷新
        function onRefresh() {
            var url = 'SingleView.aspx?Action=GetData&vid=<%=Request["vid"] %>&ParentId=<%=Request["ParentId"] %>';
            grid.set({ url: url });
            //grid.loadData();
        }
        //过滤
        function onFilter() {
            $.ligerDialog.open({ title: '过滤', url: 'SetViewFilter.aspx?vid=<%=Request["vid"] %>', name: 'winFilter', height: 350, width: 500, showMax: true, showToggle: true, isResize: true, modal: false, slide: false, 
            buttons: [
                { text: '确定', onclick: function(item, dialog) {
                    $.ligerDialog.close();
                    var url = 'SingleView.aspx?Action=GetData&vid=<%=Request["vid"] %>&ParentId=<%=Request["ParentId"] %>&Filter=1';
                    grid.set({ url: url });
                    //grid.loadData();
                }
                },
                { text: '取消', onclick: function(item, dialog) {
                    $.ligerDialog.close();
                } }
             ], isResize: true
            });
        }
        //根据字段类型确定窗体宽度
        var winWidth = 600;
        var winHeight=320;
        
        function onAdd() {
            $.ligerDialog.open({ title: '新建', url: 'AddSingleViewRecord.aspx?vid=<%=Request["vid"] %>&ParentId=<%=Request["ParentId"] %>', name: 'winAddRec', height: winHeight, width: winWidth, showMax: true, showToggle: true, isResize: true, modal: false, slide: false, 
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
        function onOkAddSingleViewRecord(){
            grid.loadData(true);
            $.ligerDialog.close();
        }
        function onCancelAddSingleViewRecord(){
            grid.loadData(true);
            $.ligerDialog.close();
        }
        function onEdit() {
            var row = grid.getSelectedRow();
            if (row == null) {
                $.ligerDialog.alert('请选择行!');
                return;
            }
            $.ligerDialog.open({ title: '修改', url: 'EditSingleViewRecord.aspx?vid=<%=Request["vid"] %>&ParentId=<%=Request["ParentId"] %>&id=' + row.id, name: 'winEditRec', height: winHeight, width: winWidth, showMax: true, showToggle: true, isResize: true, modal: false, slide: false, 
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
        function onOkEditSingleViewRecord(){
            grid.loadData(true);
            $.ligerDialog.close();
        }
        function onCancelEditSingleViewRecord(){
            grid.loadData(true);
            $.ligerDialog.close();
        }
        function onDelete() {
            var ids = getCheckedData();
            if (ids == "") {
                $.ligerDialog.alert('请选择行!');
                return;
            }
            $.ligerDialog.confirm('确认删除？', function(yes) {
                if (yes) {
                    $.post(
                    'SingleView.aspx',
                    {
                        Action: 'Delete',
                        vid: '<%=Request["vid"] %>',
                        ParentId: '<%=Request["ParentId"] %>',
                        ids: ids
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
        function getCheckedData() {
            var rows = grid.getCheckedRows();
            var str = "";
            $(rows).each(function () {
                str += this.id + ",";
            });
            return str;
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
            $.ligerDialog.open({ title: '选择工作流', url: '../Workflow/SelectWorkflowDef.aspx?B_Company_id=<%=GetCompanyId() %>&FW_Table_id=<%=m_Table.Id %>', name: 'winSelWF', height: 200, width: 300, showMax: true, showToggle: true,  isResize: true, modal: false, slide: false,
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
            $.ligerDialog.open({ title: '查看工作流', url: '../Workflow/ViewWorkflow.aspx?TbCode=<%=m_Table.Code %>&ParentId=<%=Request["ParentId"] %>&id='+row.id, name: 'winViewWF', height: 400, width: 600, showMax: true, showToggle: true,  isResize: true, modal: false, slide: false
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
                List<CColumn> lstCol=GetColList();
                for (int i=0;i<lstCol.Count;i++)
                {
                    CColumn col = (CColumn)lstCol[i];
                    if(i<lstCol.Count-1)
                        Response.Write(string.Format("{{ display: '{0}', name: '{1}',width:120}},",col.Name,col.Code));
                    else
                        Response.Write(string.Format("{{ display: '{0}', name: '{1}',width:120}}",col.Name,col.Code));
                }
                 %>
                ],
                url: 'SingleView.aspx?Action=GetData&vid=<%=Request["vid"] %>&ParentId=<%=Request["ParentId"] %>',
                dataAction: 'server', pageSize: 30,
                width: '100%', height: '100%',
                checkbox: true,
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
