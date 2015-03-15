<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Edit.aspx.cs" Inherits="Database_Table_Edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="../../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../../lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <script src="../../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script> 
    <script src="../../lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerTextBox.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerDateEditor.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerSpinner.js" type="text/javascript"></script> 
    <script type="text/javascript">
        
        $(function() {
            $("#toptoolbar").ligerToolBar({ items: [
                { text: '增加', click: onAdd, icon: 'add' },
                { line: true },
                { text: '删除', click: onDelete, icon: 'delete' }
            ]
            });
        });

        function onAdd() {
            //var row = grid.getSelectedRow();
            grid.addEditRow({
                id: NewGuid(),
                Name: '',
                Code: '',
                ColType: '字符型',
                ColLen: 50,
                AllowNull: 1,
                IsSystem: 0,
                DefaultValue:'',
                ColDecimal:0,
                Formula:'',
                RefTable:'',
                RefTableName:'',
                RefCol:'',
                RefColName:'',
                RefShowCol:'',
                RefShowColName:'',
                EnumVal: '',
                IsUnique: 0
            });
            //grid.beginEdit(row);
        }

        function onDelete() {
            var row = grid.getSelectedRow();
            if (row == null) {
                $.ligerDialog.alert('请选择行！');
                return false;
            }
            if (row.Code == 'id'
                || row.Code == 'Created'
                || row.Code == 'Creator'
                || row.Code == 'Updated'
                || row.Code == 'Updator') {
                $.ligerDialog.warn('系统字段不能删除！');
                return false;
            }
            grid.deleteRow(row);
        }
    </script>
    
    <script type="text/javascript">
        var colTypeData = [{ val: '字符型', text: '字符型' },
        { ColType: '整型', text: '整型' },
        { ColType: '长整型', text: '长整型' },
        { ColType: '布尔型', text: '布尔型' },
        { ColType: '数值型', text: '数值型' },
        { ColType: '日期型', text: '日期型' },
        { ColType: '备注型', text: '备注型' },
        { ColType: '二进制', text: '二进制' },
        { ColType: '引用型', text: '引用型' },
        { ColType: 'GUID', text: 'GUID' },
        { ColType: '枚举型', text: '枚举型' },
        { ColType: '附件型', text: '附件型' }
        ];
        var grid;
        $(function() {
            grid = $("#gridTableInfo").ligerGrid({
                columns: [
                { display: '列名', name: 'Code', width: 120, editor: { type: 'text'} },
                { display: '数据类型', name: 'ColType', width: 120, align: 'left',
                    editor: { type: 'select', data: colTypeData, valueColumnName: 'ColType',
                        ext:
                    function(rowdata) {
                        return {
                            onSelected: function(value, text) {
                                if (rowdata.ColType != value) {
                                    onChangeColType(value, rowdata);
                                }
                            }
                        };
                    }
                    },
                    render: function(item) {
                        for (var i = 0; i < colTypeData.length; i++) {
                            if (colTypeData[i]['text'] == item.ColType)
                                return colTypeData[i]['text']
                        }
                        return item.ColType;
                    }
                },
                { display: '长度', name: 'ColLen', width: 80, align: 'left', editor: { type: 'text'} },
                { display: '允许空', name: 'AllowNull', width: 80, editor: { type: 'checkbox' },
                    render: function(item) {
                        if (parseInt(item.AllowNull) == 1)
                            return 'Yes';
                        else
                            return 'No';
                    }
                },
                { display: '系统字段', name: 'IsSystem', width: 80, editor: { type: 'checkbox' },
                    render: function(item) {
                        if (parseInt(item.IsSystem) == 1)
                            return 'Yes';
                        else
                            return 'No';
                    }
                },
                { display: '唯一性', name: 'IsUnique', width: 80, editor: { type: 'checkbox' },
                    render: function(item) {
                        if (parseInt(item.IsUnique) == 1)
                            return 'Yes';
                        else
                            return 'No';
                    }
                }
                ],
                url: 'Edit.aspx?Action=GetData&id=<%=Request["id"] %>',
                dataAction: 'server',
                usePager: false,
                enabledEdit: true,
                rownumbers: true,
                rowDraggable: true,
                width: '100%', height: '230px', allowUnSelectRow: true,
                onSelectRow: function(data, rowindex, rowobj) {
                    //$.ligerDialog.alert('1选择的是' + data.id);
                    $("#txtName").val(data.Name);
                    $("#txtDefault").val(data.DefaultValue);
                    $("#txtDecimal").val(data.ColDecimal);
                    $("#txtFormla").val(data.Formla);
                    $("#cbRefTable").val(data.RefTableName);
                    $("#cbRefCol").val(data.RefColName);
                    $("#cbRefShowCol").val(data.RefShowColName);
                    $("#txtEnumVal").val(data.EnumVal);
                },
                onUnSelectRow: function(data, rowindex, rowobj) {
                    //alert('反选择的是' + data.id);
                },
                onBeforeEdit: function(e) {
                    if (e.record.Code == 'id'
                || e.record.Code == 'Created'
                || e.record.Code == 'Creator'
                || e.record.Code == 'Updated'
                || e.record.Code == 'Updator')
                        return false;
                    return true;
                },
                onBeforeSubmitEdit: function(e) {
                    if (e.column.columnname == 'Code') {
                        $("#txtName").val(e.value);
                        e.record.Name = e.value;
                    };
                }
            });

            //            grid.bind('selectRow', function(data) {
            //                //$.ligerDialog.alert('2选择的是' + data.id);
            //            });
        });


        function onChangeColType(type, rowdata) {
            var ColLen = 50, ColDecimal = 0;
            if (type == "字符型" || type == "枚举型") {
                ColLen = 50;
                ColDecimal = 0;
            }
            else if (type == "整型") {
                ColLen = 4;
                ColDecimal = 0;
            }
            else if (type == "长整型") {
                ColLen = 8;
                ColDecimal = 0;
            }
            else if (type == "布尔型") {
                ColLen = 2;
                ColDecimal = 0;
            }
            else if (type == "数值型") {
                ColLen = 18;
                ColDecimal = 2;
            }
            else if (type == "日期型") {
                ColLen = 8;
                ColDecimal = 0;
            }
            else if (type == "备注型") {
                ColLen = 16;
                ColDecimal = 0;
            }
            else if (type == "二进制") {
                ColLen = 8;
                ColDecimal = 0;
            }
            else if (type == "引用型" || type == "GUID") {
                ColLen = 16;
                ColDecimal = 0;
            }
            else if (type == "附件型") {
                ColLen = 250;
                ColDecimal = 0;
            }

            grid.updateCell('ColLen', ColLen, rowdata);
            $("#txtDecimal").val(ColDecimal);
        }

        $(function() {
            $("#txtName").ligerTextBox({ nullText: '不能为空',
                onChangeValue: function(value) {
                    var row = grid.getSelectedRow();
                    if (row == null) {
                        $.ligerDialog.alert('请选择行!');
                        return;
                    }
                    if (row.Code == 'id'
                || row.Code == 'Created'
                || row.Code == 'Creator'
                || row.Code == 'Updated'
                || row.Code == 'Updator') {
                        $.ligerDialog.warn('系统字段不能修改！');
                        return false;
                    }
                    row.Name = value;
                }
            });
            $("#txtDefault").ligerTextBox({
                onChangeValue: function(value) {
                    var row = grid.getSelectedRow();
                    if (row == null) {
                        $.ligerDialog.alert('请选择行!');
                        return;
                    }
                    if (row.Code == 'id'
                || row.Code == 'Created'
                || row.Code == 'Creator'
                || row.Code == 'Updated'
                || row.Code == 'Updator') {
                        $.ligerDialog.warn('系统字段不能修改！');
                        return false;
                    }
                    row.DefaultValue = value;
                }
            });
            $("#txtDecimal").ligerTextBox({
                onChangeValue: function(value) {
                    var row = grid.getSelectedRow();
                    if (row == null) {
                        $.ligerDialog.alert('请选择行!');
                        return false;
                    }
                    if (row.Code == 'id'
                || row.Code == 'Created'
                || row.Code == 'Creator'
                || row.Code == 'Updated'
                || row.Code == 'Updator') {
                        $.ligerDialog.warn('系统字段不能修改！');
                        return false;
                    }
                    if (!value.match(/^\d.*$/)) {
                        $.ligerDialog.warn('小数位数必须为数字');
                        $("#txtDecimal").val(row.ColDecimal);
                        return false;
                    }
                    row.ColDecimal = value;
                }
            });
            $("#txtFormula").ligerTextBox({
                onChangeValue: function(value) {
                    var row = grid.getSelectedRow();
                    if (row == null) {
                        $.ligerDialog.alert('请选择行!');
                        return;
                    }
                    if (row.Code == 'id'
                || row.Code == 'Created'
                || row.Code == 'Creator'
                || row.Code == 'Updated'
                || row.Code == 'Updator') {
                        $.ligerDialog.warn('系统字段不能修改！');
                        return false;
                    }
                    row.Formula = value;
                }
            });
        });
        
        function getCheckedData()
        {
            var rows = grid.getCheckedRows();
            var str = "";
            $(rows).each(function ()
            {
                str += this.id + ",";
            });
            $.ligerDialog.alert('选择的是' + str);
        }

        function onSubmit() {

            if ($("#txtTbName").val() == "") {
                $.ligerDialog.warn("表名称不能空！");
                return false;
            }
            if ($("#txtTbCode").val() == "") {
                $.ligerDialog.warn("表编码不能空！");
                return false;
            }
            var sCode = $("#txtTbCode").val();
            if (!ValidateColName(sCode)) {
                $.ligerDialog.warn("表编码只能由字母、数字、下划线组成，且数字不能在前面！");
                return false;
            }


            var postData = "";
            var rowData = grid.getData();
            for (var idx = 0; idx < rowData.length; idx++) {
                if (rowData[idx]["Name"] == "" || rowData[idx]["Code"] == "") {
                    $.ligerDialog.warn("字段名称和编码不能空！");
                    return false;
                }
                for (var idx2 = 0; idx2 < idx; idx2++) {
                    if (rowData[idx]["Code"].toLowerCase() == rowData[idx2]["Code"].toLowerCase()) {
                        $.ligerDialog.warn("字段编码重复:" + rowData[idx]["Code"]);
                        return false;
                    }
                    if (rowData[idx]["Name"].toLowerCase() == rowData[idx2]["Name"].toLowerCase()) {
                        $.ligerDialog.warn("字段名称重复:" + rowData[idx]["Name"]);
                        return false;
                    }
                }
                if (!ValidateColName(rowData[idx]["Code"])) {
                    $.ligerDialog.warn("列名只能由字母、数字、下划线组成，且数字不能在前面:" + rowData[idx]["Code"]);
                    return false;
                }
                if (rowData[idx]["ColType"] == "引用型") {
                    if (rowData[idx]["RefTable"] == "") {
                        $.ligerDialog.warn(rowData[idx]["Name"] + " 引用表不能空！");
                        return false;
                    }
                    if (rowData[idx]["RefCol"] == "") {
                        $.ligerDialog.warn(rowData[idx]["Name"] + " 引用字段不能空！");
                        return false;
                    }
                    if (rowData[idx]["RefShowCol"] == "") {
                        $.ligerDialog.warn(rowData[idx]["Name"] + " 显示字段不能空！");
                        return false;
                    }
                }

                var Code = rowData[idx]["Code"];
                if (Code != "id" && Code != "Creator" && Code != "Created" && Code != "Updator" && Code != "Updated") {
                    $.each(rowData[idx], function(key, val) {
                        postData += val + ",";
                    });
                    postData += ";";
                }
            }

            //提交
            var tableid = '<%=Request["id"] %>';
            $.post(
                'Edit.aspx',
                {
                    Action: 'PostData',
                    id: tableid,
                    Name: $("#txtTbName").val(),
                    Code: $("#txtTbCode").val(),
                    IsSystem: $("#ckIsSystem").attr("checked"),
                    GridData: postData
                },
                 function(data) {
                     if (data == "" || data == null) {
                         parent.$.ligerDialog.close();
                         parent.grid.loadData(true);
                         return true;
                     }
                     else {
                         $.ligerDialog.warn(data);
                         return false;
                     }
                 },
                 'text'
                 );
            return false;
        }
        function onCancel() {

            //提交
            var tableid = '<%=Request["id"] %>';
            $.post(
                'Edit.aspx',
                {
                    Action: 'Cancel',
                    id: tableid
                },
                 function(data) {
                     if (data == "" || data == null) {
                         parent.$.ligerDialog.close();
                         parent.grid.loadData(true);
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

        //判断列名合法性：列名只能由字母、数字、下划线组成，且数字不能在前面
        function ValidateColName(sColName) {
            if (sColName == "")
                return false;
            sColName = sColName.toUpperCase();
            if (sColName[0] < 'A' || sColName[0] > 'Z')
                return false;
            for (var i = 1; i < sColName.length; i++) {
                var bFlag = false;
                if (sColName[i] >= 'A' && sColName[i] <= 'Z')
                    bFlag = true;
                else if (sColName[i] >= '0' && sColName[i] <= '9')
                    bFlag = true;
                else if (sColName[i] == '_')
                    bFlag = true;

                if (!bFlag)
                    return false;
            }

            return true;
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
            $("#cbRefTable").ligerComboBox({
                onBeforeOpen: onSelectTable
            });
            $("#cbRefCol").ligerComboBox({
                onBeforeOpen: onSelectCol
            });
            $("#cbRefShowCol").ligerComboBox({
                onBeforeOpen: onSelectShowCol
            });
            $("#txtEnumVal").ligerComboBox({
                onBeforeOpen: onEditEnumVal
            });
        });
        function onSelectTable() {
            var row = grid.getSelectedRow();
            if (row == null) {
                $.ligerDialog.alert('请选择行!');
                return false;
            }
            if (row.Code == 'id'
                || row.Code == 'Created'
                || row.Code == 'Creator'
                || row.Code == 'Updated'
                || row.Code == 'Updator') {
                $.ligerDialog.warn('系统字段不能修改！');
                return false;
            }
            $.ligerDialog.open({ title: '选择表', name: 'tableselector', width: 400, height: 300, url: 'SelectTable.aspx', buttons: [
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
            var row = grid.getSelectedRow();
            if (row == null) {
                $.ligerDialog.alert('请选择行!');
                return false;
            }
            row.RefTableName = data.Name;
            row.RefTable = data.id;
            
            $("#cbRefTable").val(data.Name);
            $("#cbRefCol").val('');
            $("#cbRefShowCol").val('');
            dialog.close();
        }
        function onSelectCancel(item, dialog) {
            dialog.close();
        }

        function onSelectCol() {
            var row = grid.getSelectedRow();
            if (row == null) {
                $.ligerDialog.alert('请选择行!');
                return false;
            }
            if (row.Code == 'id'
                || row.Code == 'Created'
                || row.Code == 'Creator'
                || row.Code == 'Updated'
                || row.Code == 'Updator') {
                $.ligerDialog.warn('系统字段不能修改！');
                return false;
            }
            $.ligerDialog.open({ title: '选择字段', name: 'colselector', width: 400, height: 300,
            url: 'SelectCol.aspx?tid=' + row.RefTable,
                buttons: [
                { text: '确定', onclick: onSelectColOK },
                { text: '取消', onclick: onSelectCancel }
            ]
            });
            return false;
        }
        function onSelectColOK(item, dialog) {
            var fn = dialog.frame.onSelect || dialog.frame.window.onSelect;
            var data = fn();
            if (!data) {
                $.ligerDialog.alert('请选择行!');
                return;
            }
            var row = grid.getSelectedRow();
            if (row == null) {
                $.ligerDialog.alert('请选择行!');
                return false;
            }
            row.RefColName = data.Name;
            row.RefCol = data.id;
            $("#cbRefCol").val(data.Name);
            dialog.close();
        }
        function onSelectShowCol() {
            var row = grid.getSelectedRow();
            if (row == null) {
                $.ligerDialog.alert('请选择行!');
                return false;
            }
            if (row.Code == 'id'
                || row.Code == 'Created'
                || row.Code == 'Creator'
                || row.Code == 'Updated'
                || row.Code == 'Updator') {
                $.ligerDialog.warn('系统字段不能修改！');
                return false;
            }
            $.ligerDialog.open({ title: '选择字段', name: 'showcolselector', width: 400, height: 300,
            url: 'SelectCol.aspx?tid=' + row.RefTable,
            buttons: [
                { text: '确定', onclick: onSelectShowColOK },
                { text: '取消', onclick: onSelectCancel }
            ]
            });
            return false;
        }
        function onSelectShowColOK(item, dialog) {
            var fn = dialog.frame.onSelect || dialog.frame.window.onSelect;
            var data = fn();
            if (!data) {
                $.ligerDialog.alert('请选择行!');
                return;
            }
            var row = grid.getSelectedRow();
            if (row == null) {
                $.ligerDialog.alert('请选择行');
                return false;
            }
            row.RefShowColName = data.Name;
            row.RefShowCol = data.id;
            $("#cbRefShowCol").val(data.Name);
            dialog.close();
        }

        var dlgEditEnumVal;
        function onEditEnumVal() {
            var row = grid.getSelectedRow();
            if (row == null) {
                $.ligerDialog.alert('请选择行!');
                return false;
            }
            if (row.Code == 'id'
                || row.Code == 'Created'
                || row.Code == 'Creator'
                || row.Code == 'Updated'
                || row.Code == 'Updator') {
                $.ligerDialog.warn('系统字段不能修改！');
                return false;
            }
            var newVal = $("#txtEnumVal").val();
            var conv="";
            for (var idx = 0; idx < newVal.length; idx++) {
                if (newVal[idx] != '/')
                    conv += newVal[idx];
                else
                    conv += "\n";
            }
            //newVal=newVal.replace("/", "\n");
            $("#txtEditEnumVal").val(conv);
            if (dlgEditEnumVal == null) {
                dlgEditEnumVal = $.ligerDialog.open({ target: $("#divEditEnumVal"),
                    buttons: [
                { text: '确定', onclick: onEditEnumValOK },
                { text: '取消', onclick: onEditEnumValCancel }
            ]
                });
                $(".l-dialog-close").bind('mousedown', function()  //dialog右上角的叉
                {
                    dlgEditEnumVal.hide();
                });
            }
            else {
                dlgEditEnumVal.show();
            }
        }
        function onEditEnumValOK(item, dialog) {
            var newVal = $("#txtEditEnumVal").val();
            newVal = newVal.replace(/[\r\n]/g, "/");
            $("#txtEnumVal").val(newVal);
            var row = grid.getSelectedRow();
            if (row != null) {
                row.EnumVal = newVal;
            }
            dialog.hide();
        }
        function onEditEnumValCancel(item, dialog) {
            dialog.hide();
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
<body style="padding:6px; ">
    <div style=" width:600px;">
    <table cellpadding="0" cellspacing="0" class="l-table-edit" >
        <tr>
            <td align="right" class="l-table-edit-td">名称:</td>
            <td align="left" class="l-table-edit-td"><input name="txtTbName" type="text" id="txtTbName" ltype="text" validate="{required:true,maxlength:50}" value="<%=m_Table.Name %>" /></td>
            
            <td align="right" class="l-table-edit-td">编码:</td>
            <td align="left" class="l-table-edit-td"><input name="txtTbCode" type="text" id="txtTbCode" ltype="text" validate="{required:true,maxlength:50}" value="<%=m_Table.Code %>" /></td>
            
            <td align="right" class="l-table-edit-td"><input id="ckIsSystem" type="checkbox" <%if(m_Table.IsSystem) {%> checked<%} %> />系统表</td>
        </tr>
  </table>
  <div id="toptoolbar"></div> 
   <div id="gridTableInfo" style="margin:0; padding:0"></div>
    
    <table cellpadding="0" cellspacing="0" class="l-table-edit" >
        <tr>
            <td align="right" class="l-table-edit-td">名称:</td>
            <td align="left" class="l-table-edit-td"><input name="txtName" type="text" id="txtName" ltype="text" validate="{required:true,maxlength:50}" /></td>
        
            <td align="right" class="l-table-edit-td">默认值:</td>
            <td align="left" class="l-table-edit-td"><input name="txtDefault" type="text" id="txtDefault" ltype="text"  /></td>
        </tr>
        <tr>
            <td align="right" class="l-table-edit-td">小数位数:</td>
            <td align="left" class="l-table-edit-td"><input name="txtDecimal" type="text" id="txtDecimal"  ltype='spinner' ligerui="{type:'int'}" value="0" class="required" validate="{digits:true,min:1,max:100}" /></td>
        
            <td align="right" class="l-table-edit-td">公式:</td>
            <td align="left" class="l-table-edit-td"><input name="txtFormula" type="text" id="txtFormula" ltype="text"  /></td>
        </tr>
        <tr>
            <td align="right" class="l-table-edit-td">引用表:</td>
            <td align="left" class="l-table-edit-td"><input name="cbRefTable" type="text" id="cbRefTable"  /></td>
        
            <td align="right" class="l-table-edit-td">引用字段:</td>
            <td align="left" class="l-table-edit-td"><input name="cbRefCol" type="text" id="cbRefCol"  /></td>
        </tr>
        <tr>
            <td align="right" class="l-table-edit-td">引用显示字段:</td>
            <td align="left" class="l-table-edit-td"><input name="cbRefShowCol" type="text" id="cbRefShowCol"  /></td>
        
            <td align="right" class="l-table-edit-td">枚举值:</td>
            <td align="left" class="l-table-edit-td"><input name="txtEnumVal" type="text" id="txtEnumVal" ltype="text"  /></td>
        </tr>
  </table>
  <div id="divEditEnumVal"  style="width:200px; margin:3px; display:none;">
  <h3>枚举值：</h3>
  <div>
  </div>
  <textarea id="txtEditEnumVal" cols="40" rows="10"></textarea><br />
  （一行一个值）
  </div>
  </div>
</body>
</html>
