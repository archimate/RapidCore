<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetDefaultVal.aspx.cs" Inherits="View_SetDefaultVal" %>
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
    <script src="../lib/ligerUI/js/plugins/ligerTextBox.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerDateEditor.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerSpinner.js" type="text/javascript"></script>

    <script type="text/javascript">
        
        var grid;
        $(function() {
            grid = $("#gridTable").ligerGrid({
                columns: [
                { display: '字段', name: 'ColName', align: 'left', width: 120 },
                { display: '默认值表达式', name: 'DefaultVal', align: 'left', width: 200, editor: { type: 'text'} },
                { display: '只读', name: 'ReadOnly', width: 80, editor: { type: 'checkbox' },
                    render: function(item) {
                    if (parseInt(item.ReadOnly) == 1)
                            return 'Yes';
                        else
                            return 'No';
                    }
                }
                ],
                //url: 'MultMasterDetailViewInfo3.aspx?Action=GetDetail&id=<%=Request["id"] %>&catalog_id=<%=Request["catalog_id"] %> ',
                url: '',
                dataAction: 'server',
                usePager: false,
                enabledEdit: true,
                width: '100%', height: '80%',
                onBeforeEdit: function(e) {
                },
                onBeforeSubmitEdit: function(e) {
                },
                onAfterEdit: function(e) {
                }
            });
        });


        function btOk_onclick() {
            var postData = "";
            var rowData = grid.getData();
            for (var idx = 0; idx < rowData.length; idx++) {
                var id, DefaultVal,ReadOnly;
                $.each(rowData[idx], function(key, val) {
                    if (key == "id")
                        id = val;
                    else if (key == "DefaultVal")
                        DefaultVal = val;
                    else if (key == "ReadOnly")
                        ReadOnly = val;
                });
                postData += id + ":" + DefaultVal + ":" + ReadOnly;
                postData += ";";
            }
            //提交
            $.post(
                'SetDefaultVal.aspx',
                {
                    Action: 'PostData',
                    id: '<%=Request["id"] %>',
                    GridData: postData,
                    table_id: $("#cbTable").val()
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
                 'text'
                 );
        }

        function btCancel_onclick() {
            $.post(
                'SetDefaultVal.aspx',
                {
                    id: '<%=Request["id"] %>',
                    Action: 'Cancel'
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
        }

        function cbTable_onchange() {
            //提交切换前
            submitGrid();
            
            $("#hidOldTable").val($("#cbTable").val());
            //更新切换后字段排序
            var url = 'SetDefaultVal.aspx?Action=GetData&id=<%=Request["id"] %>&table_id=' + $("#cbTable").val();
            grid.set({ url: url });
            //grid.loadData();
            
        }
        function submitGrid() {
            var oldTable = $("#hidOldTable").val();
            if (oldTable == null || oldTable == "")
                return;

            var postData = "";
            var rowData = grid.getData();
            for (var idx = 0; idx < rowData.length; idx++) {
                var id, DefaultVal, ReadOnly;
                $.each(rowData[idx], function(key, val) {
                    if (key == "id")
                        id = val;
                    else if (key == "DefaultVal")
                        DefaultVal = val;
                    else if (key == "ReadOnly")
                        ReadOnly = val;
                });
                postData += id + ":" + DefaultVal + ":" + ReadOnly;
                postData += ";";
            }
            //提交
            $.post(
                'SetDefaultVal.aspx',
                {
                    Action: 'PostData',
                    id: '<%=Request["id"] %>',
                    GridData: postData,
                    table_id: oldTable
                },
                 function(data) {
                     if (data == "" || data == null) {
                         return true;
                     }
                     else {
                         $.ligerDialog.warn(data);
                         return false;
                     }
                 },
                 'text'
                 );    
        }

        function window_onload() {
            cbTable_onchange();
        }

        function btVariable_onclick() {
            parent.$.ligerDialog.open({ title: '自定义变量', url: '../Common/Variable.aspx', name: 'winVariable', height: 300, width: 500, showMax: true, showToggle: true, isResize: true, modal: false, slide: false,
            buttons: [
                { text: '关闭', onclick: function(item, dialog) {
                    dialog.close();
                } }
             ], isResize: true
            });
        }

    </script>
</head>
<body style="padding:6px; overflow:hidden;" onload="return window_onload()"> 
    <div>表：
    <select
        id="cbTable" style="width:120px" onchange="return cbTable_onchange()">
        <%  
            CTable tableM = (CTable)Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.Find(m_View.FW_Table_id);
             %>
                <option value="<%=tableM.Id %>"><%=tableM.Name%></option>
            <%     
            List<CBaseObject> lstObj = m_View.ViewDetailMgr.GetList();
            List<CViewDetail> sortObj = new List<CViewDetail>();
            foreach (CBaseObject obj in lstObj)
            {
                CViewDetail vd = (CViewDetail)obj;
                sortObj.Add(vd);
            }
            sortObj.Sort();
            foreach (CViewDetail ViewDetail in sortObj)
            {
                CTable table = (CTable)Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.Find(ViewDetail.FW_Table_id);
                %>
                <option value="<%=table.Id %>"><%=table.Name %></option>
                <%
            }
         %>
        
    </select>
    <input type="hidden" id="hidOldTable" name="hidOldTable" />
    </div>
    <div id="gridTable" style="margin:0; padding:0"></div>
    <div>注：表达式支持[自定义变量]、常量、select SQL语句，例如[当前用户名]、123、sql:select getdate()</div>
    
    <input id="btVariable" type="button" value="查看自定义变量" onclick="return btVariable_onclick()" />&nbsp;&nbsp;&nbsp;&nbsp;
    <input id="btOk" type="button" value="确定" style="width:60px" onclick="return btOk_onclick()" />&nbsp;&nbsp;&nbsp;&nbsp;
    <input id="btCancel" type="button" value="取消" style="width:60px" onclick="return btCancel_onclick()" />
</body>
</html>
