<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditReport.aspx.cs" Inherits="Report_EditReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
     <script src="../lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerButton.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerRadio.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerSpinner.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerTextBox.js" type="text/javascript"></script> 
    <script src="../lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script> 
    <script src="../lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
    <script type="text/javascript">
        
        $(function() {
            $("#toptoolbar").ligerToolBar({ items: [
                { text: '设置', click: onSet },
                { line: true },
                { text: '向上', click: onUp },
                { line: true },
                { text: '向下', click: onDown }
            ]
            });
        });

        function onSet() {
            var url = 'SelStatItem.aspx?rptid=<%=GetReport().Id %>&B_Company_id=<%=Request["B_Company_id"] %>';
            $.ligerDialog.open({ title: '设置指标', url: url, name: 'winSet', height: 380, width: 600, modal: false,
                buttons: [
                { text: '确定', onclick: function(item, dialog) {
                    var ret = document.getElementById('winSet').contentWindow.onSubmit();
                }
                },
                { text: '取消', onclick: function(item, dialog) {
                    dialog.close();
                } 
                }
             ], isResize: true
            });
        }

        function onUp() {
            var row = grid.getSelectedRow();
            if (row == null) {
                $.ligerDialog.alert('请选择行！');
                return false;
            }
            grid.up(row);
        }
        function onDown() {
            var row = grid.getSelectedRow();
            if (row == null) {
                $.ligerDialog.alert('请选择行！');
                return false;
            }
            grid.down(row);
        }

    </script>
    
    <script type="text/javascript">
        var StatTypeData = [{ StatType: '取数', text: '取数' },
        { StatType: '求和', text: '求和' },
        { StatType: '求平均', text: '求平均' },
        { StatType: '求最大值', text: '求最大值' },
        { StatType: '求最小值', text: '求最小值' },
        { StatType: '计数', text: '计数' }
        ];
        var OrderData = [{ Order: '默认', text: '默认' },
        { Order: '升序', text: '升序' },
        { Order: '降序', text: '降序' }
        ];
        var grid;
        $(function() {
            grid = $("#gridTable").ligerGrid({
                columns: [
                { display: '表', name: 'TableName', width: 80 },
                { display: '字段', name: 'ColumnName', width: 120, align: 'left' },
                { display: '统计类型', name: 'StatTypeName', width: 120, align: 'left',
                    editor: { type: 'select', data: StatTypeData, valueColumnName: 'StatType' }
                },
                { display: '排序', name: 'OrderName', width: 120, align: 'left',
                    editor: { type: 'select', data: OrderData, valueColumnName: 'Order' }
                }
                ],
                url: 'EditReport.aspx?Action=GetData&id=<%=Request["id"] %>&B_Company_id=<%=Request["B_Company_id"] %>',
                dataAction: 'server',
                usePager: false,
                enabledEdit: true,
                rownumbers: true, 
                rowDraggable: true,
                width: '100%', height: '200px',
                onSelectRow: function(data, rowindex, rowobj) {
                    //$.ligerDialog.alert('1选择的是' + data.id);
                }
            });

        });
        
        function onSubmit() {

            if ($("#txtName").val() == "") {
                $.ligerDialog.warn("名称不能空！");
                return false;
            }

            var postData = "";
            var rowData = grid.getData();
            for (var idx = 0; idx < rowData.length; idx++) {
                postData += rowData[idx]["id"];
                postData += ",";
                postData += rowData[idx]["StatTypeName"];
                postData += ",";
                postData += rowData[idx]["OrderName"];
                
                postData += ";";
            }
            //提交
            $.post(
                'EditReport.aspx',
                {
                    Action: 'PostData',
                    id:'<%=Request["id"] %>',
                    Name: $("#txtName").val(),
                    Catalog_id: $("#hidCatalog").val(),
                    Filter: $("#Filter").val(),
                    GridData: postData,
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
                'EditReport.aspx',
                {
                    Action: 'Cancel',
                    id:'<%=Request["id"] %>',
                    B_Company_id:'<%=Request["B_Company_id"] %>'
                },
                 function(data) {
                     if (data == "" || data == null) {
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
            $("#cbCatalog").ligerComboBox({
                onBeforeOpen: onSelectCatalog
            });
            $("#cbTable").ligerComboBox({
                onBeforeOpen: onSelectTable
            });
            $("#cbColumn").ligerComboBox({
                onBeforeOpen: onSelectColumn
            });
        });
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

            $("#cbColumn").val('');
            $("#hidColumn").val('');

            dialog.close();
        }

        function onSelectColumn() {
            if ($("#hidTable").val() == "") {
                $.ligerDialog.warn("请选择表！");
                return false;
            }
            $.ligerDialog.open({ title: '选择字段', name: 'tableselector', width: 400, height: 300, url: '../Database/Table/SelectCol.aspx?tid=' + $("#hidTable").val(), buttons: [
                { text: '确定', onclick: onSelectColumnOK },
                { text: '取消', onclick: onSelectCancel }
            ]
            });
            return false;
        }
        function onSelectColumnOK(item, dialog) {
            var fn = dialog.frame.onSelect || dialog.frame.window.onSelect;
            var data = fn();
            if (!data) {
                $.ligerDialog.warn('请选择行!');
                return false;
            }
            $("#cbColumn").val(data.Name);
            $("#hidColumn").val(data.id);

            dialog.close();
        }


        function btAdd_onclick() {
            if ($("#hidTable").val() == "") {
                $.ligerDialog.warn("请选择表！");
                return false;
            }
            if ($("#hidColumn").val() == "") {
                $.ligerDialog.warn("请选择字段！");
                return false;
            }
            if ($("#txtVal").val() == "") {
                $.ligerDialog.warn("请填写值！");
                return false;
            }
            $.post(
                'EditReport.aspx',
                {
                    Action: 'GetCondiction',
                    id:'<%=Request["id"] %>',
                    Table_id: $("#hidTable").val(),
                    Column_id: $("#hidColumn").val(),
                    Val: $("#txtVal").val(),
                    B_Company_id:'<%=Request["B_Company_id"] %>'
                },
                 function(data) {
                     if (data == "" || data == null) {
                         $.ligerDialog.warn("值错误！");
                         return false;
                     }
                     else {
                         var sExp = "";
                         if ($("#Filter").val() != "") {
                             sExp += $("#cbAndOr").val();
                         }
                         sExp += "[" + $("#cbTable").val() + "].";
                         sExp += "[" + $("#cbColumn").val() + "]";
                         sExp += $("#cbSign").val();
                         sExp += data;

                         sExp = $("#Filter").val() + sExp;
                         $("#Filter").val(sExp);
                         return true;
                     }
                 },
                 'text');
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
            <td align="left" class="l-table-edit-td"><input name="txtName" type="text" id="txtName" ltype="text"  value="<%=m_BaseObject.Name %>" /></td>
            
            <td align="right" class="l-table-edit-td">目录:</td>
            <td align="left" class="l-table-edit-td"><input name="cbCatalog" type="text" id="cbCatalog"  value="<%=GetCatalogName() %>" />
            <input name="hidCatalog" type="hidden" id="hidCatalog"   value="<%=m_BaseObject.RPT_ReportCatalog_id %>" /></td>
            
        </tr>
  </table>
  <div id="toptoolbar"></div> 
   <div id="gridTable" style="margin:0; padding:0"></div>
   
    <table>    
        <tr><td>过滤条件:</td></tr>
        <tr>
            <td align="left" class="l-table-edit-td">
            <table>
            <tr><td>
            <textarea cols="100" rows="4" class="l-textarea" id="Filter" style="width:450px" ><%=m_BaseObject.Filter %></textarea>
            </td></tr>
            <tr><td>
                <table><tr><td>
                <select name="cbAndOr" id="cbAndOr"  style="width:40px">
                  <option value=" and ">与</option>
                  <option value=" or ">或</option>
                  </select>
                  </td><td>
                <input name="cbTable" id="cbTable"  type="text" style="width:80px"/>
                <input name="hidTable" type="hidden" id="hidTable"  />
                </td><td>
                <input name="cbColumn" id="cbColumn" type="text"  style="width:80px"/>
                  <input name="hidColumn" type="hidden" id="hidColumn"  />
                  </td><td>
                  <select name="cbSign" id="cbSign"  style="width:40px">
                  <option >=</option>
                  <option >></option>
                  <option >>=</option>
                  <option ><</option>
                  <option ><=</option>
                  <option ><></option>
                  <option >like</option>
                  </select>
                  </td><td>
                  <input name="txtVal" type="text" id="txtVal"  style="width:80px"/>
                  </td><td>
                  <input name="btAdd" id="btAdd" type="button" value="添加"  style="width:60px" onclick="return btAdd_onclick()" />
                  </td></tr></table>
            </td></tr>
            </table>
            </td>
            <td align="left"></td>
        </tr>
    </table>
</body>
</html>
