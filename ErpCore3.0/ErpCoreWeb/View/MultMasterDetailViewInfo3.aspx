<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MultMasterDetailViewInfo3.aspx.cs" Inherits="View_MultMasterDetailViewInfo3" %>
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
        $(function() {
            $("#toptoolbar").ligerToolBar({ items: [
                { text: '向上', click: onUp },
                { text: '向下', click: onDown }
            ]
            });
            $("#toptoolbar2").ligerToolBar({ items: [
                { text: '向上', click: onUp2 },
                { text: '向下', click: onDown2 }
            ]
            });
        });
        //向上
        function onUp() {
            grid.up( grid.getSelectedRow());
        }
        //向下
        function onDown() {
            grid.down(grid.getSelectedRow());
        }
        function onUp2() {
            grid2.up(grid2.getSelectedRow());
        }
        function onDown2() {
            grid2.down(grid2.getSelectedRow());
        }
    </script>
    <script type="text/javascript">
        var grid;
        $(function() {
            grid = $("#gridTable").ligerGrid({
                columns: [
                { display: '列名', name: 'ColName', align: 'left', width: 100 },
                { display: '标题', name: 'Caption', align: 'left', width: 100, editor: { type: 'text'} }
                ],
                url: 'MultMasterDetailViewInfo3.aspx?Action=GetData&id=<%=Request["id"] %>&catalog_id=<%=Request["catalog_id"] %> ',
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
        var grid2;
        $(function() {
            grid2 = $("#gridDetail").ligerGrid({
                columns: [
                { display: '列名', name: 'ColName', align: 'left', width: 100 },
                { display: '标题', name: 'Caption', align: 'left', width: 100, editor: { type: 'text'} }
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


        
        function btPrev_onclick() {
            window.location.href = 'MultMasterDetailViewInfo2.aspx?id=<%=Request["id"] %>&catalog_id=<%=Request["catalog_id"] %> ';
        }

        function btNext_onclick() {
            var postData = "";
            var rowData = grid.getData();
            for (var idx = 0; idx < rowData.length; idx++) {
                var id, caption;
                $.each(rowData[idx], function(key, val) {
                    if (key == "id")
                        id = val;
                    else if (key == "Caption")
                        caption = val;
                });
                postData += id + ":" + caption;
                postData += ";";
            }
            var postData2 = "";
            var rowData2 = grid2.getData();
            for (var idx = 0; idx < rowData2.length; idx++) {
                var id, caption;
                $.each(rowData2[idx], function(key, val) {
                    if (key == "id")
                        id = val;
                    else if (key == "Caption")
                        caption = val;
                });
                postData2 += id + ":" + caption;
                postData2 += ";";
            }
            //提交
            $.post(
                'MultMasterDetailViewInfo3.aspx',
                {
                    Action: 'PostData',
                    id: '<%=Request["id"] %>',
                    catalog_id: '<%=Request["catalog_id"] %>',
                    GridData: postData,
                    GridData2: postData2,
                    table_id: $("#cbDetailTable").val()
                },
                 function(data) {
                     if (data == "" || data == null) {
                         window.location.href = 'MultMasterDetailViewInfo4.aspx?id=<%=Request["id"] %>&catalog_id=<%=Request["catalog_id"] %> ';
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
                'MultMasterDetailViewInfo3.aspx',
                {
                    id: '<%=Request["id"] %>',
                    catalog_id: '<%=Request["catalog_id"] %>',
                    Action: 'Cancel'
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
        }

        function cbDetailTable_onchange() {
            //提交切换前字段排序
            submitDetailColumn();
            
            $("#hidOldDetailTable").val($("#cbDetailTable").val());
            //更新切换后字段排序
            var url = 'MultMasterDetailViewInfo3.aspx?Action=GetDetail&id=<%=Request["id"] %>&catalog_id=<%=Request["catalog_id"] %>&table_id=' + $("#cbDetailTable").val();
            grid2.set({ url: url });
            //grid2.loadData();
            
        }
        function submitDetailColumn() {
            var oldDetailTable = $("#hidOldDetailTable").val();
            if (oldDetailTable == null || oldDetailTable == "")
                return;

            var postData2 = "";
            var rowData2 = grid2.getData();
            for (var idx = 0; idx < rowData2.length; idx++) {
                var id, caption;
                $.each(rowData2[idx], function(key, val) {
                    if (key == "id")
                        id = val;
                    else if (key == "Caption")
                        caption = val;
                });
                postData2 += id + ":" + caption;
                postData2 += ";";
            }
            //提交
            $.post(
                'MultMasterDetailViewInfo3.aspx',
                {
                    Action: 'PostDetailData',
                    id: '<%=Request["id"] %>',
                    catalog_id: '<%=Request["catalog_id"] %>',
                    GridData2: postData2,
                    table_id: oldDetailTable
                },
                 function(data) {
                     if (data == "" || data == null) {
                         //window.location.href = 'MultMasterDetailViewInfo4.aspx?id=<%=Request["id"] %>&catalog_id=<%=Request["catalog_id"] %> ';
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
            cbDetailTable_onchange();
        }

    </script>
</head>
<body style="padding:6px; overflow:hidden;" onload="return window_onload()"> 
    <div>排序：</div>
    <table><tr><td valign="top">
    <div>主表：<input id="txtTbName" name="txtTbName" value="<%=m_MTable.Name %>" disabled="disabled" /></div>
  <div id="toptoolbar"></div> 
    <div id="gridTable" style="margin:0; padding:0"></div>
    </td><td valign="top">
    <div>从表：
    <select
        id="cbDetailTable" style="width:120px" onchange="return cbDetailTable_onchange()">
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
    <input type="hidden" id="hidOldDetailTable" name="hidOldDetailTable" />
    </div>
  <div id="toptoolbar2"></div> 
    <div id="gridDetail" style="margin:0; padding:0"></div>
    </td></tr></table>
    <input id="btPrev" type="button" value="上一步" style="width:60px" onclick="return btPrev_onclick()" />&nbsp;&nbsp;&nbsp;
    <input id="btNext" type="button" value="下一步" style="width:60px" onclick="return btNext_onclick()" />&nbsp;&nbsp;&nbsp;&nbsp;
    <input id="btCancel" type="button" value="取消" style="width:60px" onclick="return btCancel_onclick()" />
</body>
</html>
