<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelStatItem.aspx.cs" Inherits="Report_SelStatItem" %>

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
        var grid;
        $(function() {
            grid = $("#gridColumn").ligerGrid({
                columns: [
                { display: '名称', name: 'Name', width: 180, align: 'left' }
                ],
                url: 'SelStatItem.aspx?Action=GetColumnData&rptid=<%=Request["rptid"] %>&Table_id=' + $("#hidTable").val() + '&B_Company_id=<%=Request["B_Company_id"] %>',
                dataAction: 'server',
                usePager: false,
                width: '200px', height: '200px',
                onSelectRow: function(data, rowindex, rowobj) {
                }
            });

        });
        
        var grid2;
        $(function() {
            grid2 = $("#gridStatItem").ligerGrid({
                columns: [
                { display: '表', name: 'TableName', width: 80 },
                { display: '字段', name: 'ColumnName', width: 120, align: 'left' }
                ],
                url: 'SelStatItem.aspx?Action=GetStatItemData&rptid=<%=Request["rptid"] %>&B_Company_id=<%=Request["B_Company_id"] %>',
                dataAction: 'server',
                usePager: false,
                width: '220px', height: '200px',
                onSelectRow: function(data, rowindex, rowobj) {
                    //$.ligerDialog.alert('1选择的是' + data.id);
                }
            });

        });
        
        function onSubmit() {

            if ($("#hidTable").val() == "") {
                $.ligerDialog.warn("表对象不能空！");
                return false;
            }
            
            //提交
            $.post(
                'SelStatItem.aspx',
                {
                    Action: 'PostData',
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
                'SelStatItem.aspx',
                {
                    Action: 'Cancel',
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

            var url = 'SelStatItem.aspx?Action=GetColumnData&rptid=<%=Request["rptid"] %>&Table_id=' + $("#hidTable").val() + '&B_Company_id=<%=Request["B_Company_id"] %>';
            grid.set({ url: url });
            //grid.loadData();
                 
            dialog.close();
        }
        function onSelectCancel(item, dialog) {
            dialog.close();
        }

        function btAdd_onclick() {
            var row = grid.getSelectedRow();
            if (row == null) {
                $.ligerDialog.alert('请选择字段！');
                return false;
            }

            //提交
            $.post(
                'SelStatItem.aspx',
                {
                    Action: 'AddStatItem',
                    rptid: '<%=Request["rptid"] %>',
                    Table_id: $("#hidTable").val(),
                    Column_id: row.id,
                    B_Company_id:'<%=Request["B_Company_id"] %>'
                },
                 function(data) {
                     if (data == "" || data == null) {
                         grid2.loadData();
                         return true;
                     }
                     else {
                         $.ligerDialog.warn(data);
                         return false;
                     }
                 },
                 'text');
        }

        function btDel_onclick() {
            var row = grid2.getSelectedRow();
            if (row == null) {
                $.ligerDialog.alert('请选择指标！');
                return false;
            }

            //提交
            $.post(
                'SelStatItem.aspx',
                {
                    Action: 'DeleteStatItem',
                    rptid: '<%=Request["rptid"] %>',
                    delid: row.id,
                    B_Company_id:'<%=Request["B_Company_id"] %>'
                },
                 function(data) {
                     if (data == "" || data == null) {
                         grid2.loadData();
                         return true;
                     }
                     else {
                         $.ligerDialog.warn(data);
                         return false;
                     }
                 },
                 'text');
        }

        function btAddFormula_onclick() {
            if ($("#txtAsName").val() == "") {
                $.ligerDialog.warn("请输入别名！");
                return false;
            }
            if ($("#txtFormula").val() == "") {
                $.ligerDialog.warn("请输入公式！");
                return false;
            }
            //提交
            $.post(
                'SelStatItem.aspx',
                {
                    Action: 'AddFormula',
                    rptid: '<%=Request["rptid"] %>',
                    AsName: $("#txtAsName").val(),
                    Formula: $("#txtFormula").val(),
                    B_Company_id:'<%=Request["B_Company_id"] %>'
                },
                 function(data) {
                     if (data == "" || data == null) {
                         grid2.loadData();
                         $("#txtAsName").val('');
                         $("#txtFormula").val('');
                         return true;
                     }
                     else {
                         $.ligerDialog.warn(data);
                         return false;
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
            <td align="right" class="l-table-edit-td">表对象:</td>
            <td align="left" class="l-table-edit-td"><input name="cbTable" type="text" id="cbTable"  value="<%=(m_Table!=null)?m_Table.Name:"" %>"/>
            <input name="hidTable" type="hidden" id="hidTable"  value="<%=(m_Table!=null)?m_Table.Id.ToString():"" %>"  /></td>
            
        </tr>
  </table>
  <table><tr><td valign="top">
      <div >字段：</div> 
       <div id="gridColumn" style="margin:0; padding:0"></div>
      </td><td align=center width="60">
      <input name="btAdd" id="btAdd" type="button" value=">"  style="width:40px"  onclick="return btAdd_onclick()" /><br />
      <input name="btDel" id="btDel" type="button" value="<"  style="width:40px"  onclick="return btDel_onclick()" />
      </td><td valign="top">
      <div >已选择的指标：</div> 
       <div id="gridStatItem" style="margin:0; padding:0"></div>
       <table>
       <tr>
        <td align="right" class="l-table-edit-td">别名:</td>
        <td align="left" class="l-table-edit-td"><input name="txtAsName" type="text" id="txtAsName"  /></td>
        <td></td>
       </tr>
       <tr>
        <td align="right" class="l-table-edit-td">公式:</td>
        <td align="left" class="l-table-edit-td"><input name="txtFormula" type="text" id="txtFormula"  />
        <td>
        <input name="btAddFormula" id="btAddFormula" type="button" value="添加"  style="width:40px"  onclick="return btAddFormula_onclick()" /></td>
       </tr>
       </table>
   </td></tr></table>  
</body>
</html>
