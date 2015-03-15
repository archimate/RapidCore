<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditWorkflowDef.aspx.cs" Inherits="Workflow_EditWorkflowDef" %>

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
                { text: '增加', click: onAdd, icon: 'add' },
                { line: true },
                { text: '修改', click: onEdit, icon: 'modify' },
                { line: true },
                { text: '删除', click: onDelete, icon: 'delete' }
            ]
            });
        });

        function onAdd() {
            var url = 'AddActivesDef.aspx?wfid=<%=GetWorkflowDef().Id %>&B_Company_id=<%=Request["B_Company_id"] %>';
            $.ligerDialog.open({ title: '新建', url: url, name: 'winAddRec', height: 300, width: 400, showMax: true, showToggle: true, isResize: true, modal: false,
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
                $.ligerDialog.alert('请选择行！');
                return false;
            }
            if (row.WType != "Middle") {
                $.ligerDialog.warn('系统活动不能修改！');
                return false;
            }
            var url = 'EditActivesDef.aspx?wfid=<%=GetWorkflowDef().Id %>&id='+row.id+'&B_Company_id=<%=Request["B_Company_id"] %>';
            $.ligerDialog.open({ title: '修改', url: url, name: 'winEditRec', height: 300, width: 400, showMax: true, showToggle: true, isResize: true, modal: false,
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
                $.ligerDialog.alert('请选择行！');
                return false;
            }
            if (row.WType != "Middle") {
                $.ligerDialog.warn('系统活动不能删除！');
                return false;
            }
            //提交
            $.post(
                'AddWorkflowDef.aspx',
                {
                    Action: 'DeleteActivesDef',
                    delid: row.id,
                    B_Company_id:'<%=Request["B_Company_id"] %>'
                },
                 function(data) {
                     if (data == "" || data == null) {
                         return true;
                     }
                     else {
                         return false;
                     }
                 },
                 'text');
                 
            grid.deleteRow(row);
        }

        $(function() {
            $("#toptoolbar2").ligerToolBar({ items: [
                { text: '增加', click: onAdd2, icon: 'add' },
                { line: true },
                { text: '修改', click: onEdit2, icon: 'modify' },
                { line: true },
                { text: '删除', click: onDelete2, icon: 'delete' }
            ]
            });
        });

        function onAdd2() {
            var row = grid.getSelectedRow();
            if (row == null) {
                $.ligerDialog.alert('请选择活动！');
                return false;
            }
            if ($("#hidTable").val() == "") {
                $.ligerDialog.warn("请选择表对象！");
                return false;
            }
            var url = 'AddLink.aspx?wfid=<%=GetWorkflowDef().Id %>&PreActives='+row.id+'&B_Company_id=<%=Request["B_Company_id"] %>';
            $.ligerDialog.open({ title: '新建', url: url, name: 'winAddRec', height: 350, width: 600, showMax: true, showToggle: true, isResize: true, modal: false,
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
        function onEdit2() {
            var row = grid.getSelectedRow();
            if (row == null) {
                $.ligerDialog.alert('请选择活动！');
                return false;
            }
            if ($("#hidTable").val() == "") {
                $.ligerDialog.warn("请选择表对象！");
                return false;
            }
            var row2 = grid2.getSelectedRow();
            if (row2 == null) {
                $.ligerDialog.alert('请选择连接！');
                return false;
            }
            var url = 'EditLink.aspx?wfid=<%=GetWorkflowDef().Id %>&PreActives=' + row.id+'&id='+row2.id+'&B_Company_id=<%=Request["B_Company_id"] %>';
            $.ligerDialog.open({ title: '修改', url: url, name: 'winEditRec', height: 350, width: 600, showMax: true, showToggle: true, isResize: true, modal: false,
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

        function onDelete2() {
            var row = grid2.getSelectedRow();
            if (row == null) {
                $.ligerDialog.alert('请选择连接！');
                return false;
            }
            //提交
            $.post(
                'EditWorkflowDef.aspx',
                {
                    Action: 'DeleteLink',
                    id: '<%=Request["id"] %>',
                    delid: row.id,
                    B_Company_id:'<%=Request["B_Company_id"] %>'
                },
                 function(data) {
                     if (data == "" || data == null) {
                         return true;
                     }
                     else {
                         return false;
                     }
                 },
                 'text');

            grid2.deleteRow(row);
        }
    </script>
    
    <script type="text/javascript">
        var grid;
        $(function() {
            grid = $("#gridActives").ligerGrid({
                columns: [
                { display: '序号', name: 'Idx', width: 80 },
                { display: '活动名称', name: 'Name', width: 120, align: 'left' },
                { display: '审批类型', name: 'AType', width: 80, align: 'left' },
                { display: '审批人', name: 'UserName', width: 80, align: 'left' },
                { display: '审批角色', name: 'RoleName', width: 80, align: 'left' }
                ],
                url: 'EditWorkflowDef.aspx?Action=GetActivesData&id=<%=Request["id"] %>&B_Company_id=<%=Request["B_Company_id"] %>',
                dataAction: 'server',
                usePager: false,
                width: '100%', height: '200px', allowUnSelectRow: true,
                onSelectRow: function(data, rowindex, rowobj) {
                    //$.ligerDialog.alert('1选择的是' + data.id);
                    LoadGridLink(data.id);
                }
            });

        });
        
        var grid2;
        $(function() {
            grid2 = $("#gridLink").ligerGrid({
                columns: [
                { display: '审批结果', name: 'ResultName', width: 80 },
                { display: '转向条件', name: 'Condiction', width: 120, align: 'left' },
                { display: '后置活动', name: 'NextActivesName', width: 120, align: 'left' }
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

        function LoadGridLink(ActivesId) {
            var url = "EditWorkflowDef.aspx?Action=GetLinkData&id=<%=Request["id"] %>&ActivesId=" + ActivesId+'&B_Company_id=<%=Request["B_Company_id"] %>';
            grid2.set({ url: url});
            //grid2.loadData();
        }
        
        function onSubmit() {

            if ($("#txtName").val() == "") {
                $.ligerDialog.warn("名称不能空！");
                return false;
            }
            if ($("#hidTable").val() == "") {
                $.ligerDialog.warn("表对象不能空！");
                return false;
            }

            
            //提交
            $.post(
                'EditWorkflowDef.aspx',
                {
                    Action: 'PostData',
                    id:'<%=Request["id"] %>',
                    Name: $("#txtName").val(),
                    Catalog_id:$("#hidCatalog").val(),
                    Table_id: $("#hidTable").val(),
                    B_Company_id:'<%=Request["B_Company_id"] %>'
                },
                 function(data) {
                     if (data == "" || data == null) {
                         parent.grid.loadData(true);
                         parent.$.ligerDialog.close();
                         return true;
                     }
                     else {
                         $.ligerDialog.warn(data);
                         return false;
                     }
                 },
                 'text');
            return false;
        }
        function onCancel() {

            //提交
            $.post(
                'EditWorkflowDef.aspx',
                {
                    Action: 'Cancel',
                    id:'<%=Request["id"] %>',
                    B_Company_id:'<%=Request["B_Company_id"] %>'
                },
                 function(data) {
                     if (data == "" || data == null) {
                         parent.grid.loadData(true);
                         parent.$.ligerDialog.close();
                         return true;
                     }
                     else {
                         $.ligerDialog.warn(data);
                         return false;
                     }
                 },
                 'text');
            return false;
        }


        function GuidS() {
            return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1)
        }
        function NewGuid() {
            var guid = (GuidS() + GuidS() + "-" + GuidS() + "-" + GuidS() + "-" + GuidS() + "-" + GuidS() + GuidS() + GuidS()).toLowerCase();
            return guid;
        }

        //下拉框
        $(function() {
            $("#cbTable").ligerComboBox({
                onBeforeOpen: onSelectTable
            });
            $("#cbCatalog").ligerComboBox({
                onBeforeOpen: onSelectCatalog
            });
        });
        function onSelectTable() {
            $.ligerDialog.open({ title: '选择表', name: 'tableselector', width: 400, height: 300, url: '../Database/Table/SelectTable.aspx', buttons: [
                { text: '确定', onclick: onSelectTableOK },
                { text: '取消', onclick: onSelectCancel }
            ]
            });
            return false;
        }
        function onSelectTableOK(item, dialog) {
            var fn = dialog.frame.onSelect || dialog.frame.window.onSelect;
            var data = fn();
            if (!data) {
                $.ligerDialog.alert('请选择行!');
                return false;
            }
            $("#cbTable").val(data.Name);
            $("#hidTable").val(data.id);

            //提交
            $.post(
                'EditWorkflowDef.aspx',
                {
                    Action: 'SelectTable',
                    id:'<%=Request["id"] %>',
                    Table_id: data.id,
                    B_Company_id:'<%=Request["B_Company_id"] %>'
                },
                 function(data) {
                     if (data == "" || data == null) {
                         return true;
                     }
                     else {
                         return false;
                     }
                 },
                 'text');
                 
            dialog.close();
        }
        function onSelectCancel(item, dialog) {
            dialog.close();
        }
        function onSelectCatalog() {
            $.ligerDialog.open({ title: '选择目录', name: 'catalogselector', width: 400, height: 350, url: 'SelectCatalog.aspx', buttons: [
                { text: '确定', onclick: onSelectCatalogOK },
                { text: '取消', onclick: onSelectCancel }
            ]
            });
            return false;
        }
        function onSelectCatalogOK(item, dialog) {
            var fn = dialog.frame.onSelect || dialog.frame.window.onSelect;
            var data = fn();
            if (!data) {
                $.ligerDialog.alert('请选择行!');
                return false;
            }
            $("#cbCatalog").val(data.text);
            $("#hidCatalog").val(data.id);
            dialog.close();
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

    <table cellpadding="0" cellspacing="0" class="l-table-edit" >
        <tr>
            <td align="right" class="l-table-edit-td">名称:</td>
            <td align="left" class="l-table-edit-td"><input name="txtName" type="text" id="txtName" ltype="text" validate="{required:true,maxlength:50}" value="<%=m_BaseObject.Name %>"/></td>
            
            <td align="right" class="l-table-edit-td">目录:</td>
            <td align="left" class="l-table-edit-td"><input name="cbCatalog" type="text" id="cbCatalog"  value="<%=GetWorkflowCatalogName() %>"/>
            <input name="hidCatalog" type="hidden" id="hidCatalog"   value="<%=m_BaseObject.WF_WorkflowCatalog_id %>"/></td>
            
            <td align="right" class="l-table-edit-td">表对象:</td>
            <td align="left" class="l-table-edit-td"><input name="cbTable" type="text" id="cbTable"   value="<%=GetTableName() %>"/>
            <input name="hidTable" type="hidden" id="hidTable"   value="<%=m_BaseObject.FW_Table_id %>"/></td>
            
        </tr>
  </table>
  <div id="toptoolbar"></div> 
   <div id="gridActives" style="margin:0; padding:0"></div>
    
  <div id="toptoolbar2"></div> 
   <div id="gridLink" style="margin:0; padding:0"></div>
</body>
</html>
