<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditMasterDetailViewRecord.aspx.cs" Inherits="View_EditMasterDetailViewRecord" %>
<%@ Import Namespace="ErpCoreModel" %>
<%@ Import Namespace="ErpCoreModel.Base" %>
<%@ Import Namespace="ErpCoreModel.Framework" %>
<%@ Import Namespace="ErpCoreModel.UI" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Register TagPrefix="uc1" TagName="ViewRecordCtrl" Src="../CommonCtrl/ViewRecordCtrl.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" /> 
    <script charset="utf-8" src="../kindeditor/examples/jquery.js"></script>
    <!--<script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>-->
     <script src="../lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerDateEditor.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerButton.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerRadio.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerSpinner.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerTextBox.js" type="text/javascript"></script> 
    <script src="../lib/ligerUI/js/plugins/ligerTip.js" type="text/javascript"></script>
    <script src="../lib/jquery-validation/jquery.validate.min.js" type="text/javascript"></script> 
    <script src="../lib/jquery-validation/jquery.metadata.js" type="text/javascript"></script>
    <script src="../lib/jquery-validation/messages_cn.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script> 
    
    <!--在线编辑器-->
	<link rel="stylesheet" href="../kindeditor/jquery-ui/css/smoothness/jquery-ui-1.9.2.custom.css" />
	<link rel="stylesheet" href="../kindeditor/themes/default/default.css" />
	<script charset="utf-8" src="../kindeditor/jquery-ui/js/jquery-ui-1.9.2.custom.js"></script>
	<script charset="utf-8" src="../kindeditor/kindeditor.js"></script>
	<script charset="utf-8" src="../kindeditor/lang/zh_CN.js"></script>

    <script type="text/javascript">
       

        function onSubmit() {
            document.getElementById("form1").submit();
        } 
        function onCancel() {
            $.post(
                'EditMasterDetailViewRecord.aspx',
                {
                vid:'<%=Request["vid"] %>',
                id:'<%=Request["id"] %>',
                ParentId:'<%=Request["ParentId"] %>',
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
        var winWidth = 450;
        var winHeight=240;
        
        function onAdd() {
            $.ligerDialog.open({ title: '新建', url: 'AddDetailRecord2.aspx?vid=<%=Request["vid"] %>', name: 'winAddRec', height: winHeight, width: winWidth, showMax: true, showToggle: true, showMin: false, isResize: true, modal: false, slide: false, 
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
        function onOkAddDetailRecord2(){
            grid.loadData(true);
            $.ligerDialog.close();
        }
        function onCancelAddDetailRecord2(){
            grid.loadData(true);
            $.ligerDialog.close();
        }
        function onEdit() {
            var row = grid.getSelectedRow();
            if (row == null) {
                $.ligerDialog.alert('请选择行!');
                return;
            }
            $.ligerDialog.open({ title: '修改', url: 'EditDetailRecord2.aspx?vid=<%=Request["vid"] %>&id=' + row.id, name: 'winEditRec', height: winHeight, width: winWidth, showMax: true, showToggle: true, showMin: false, isResize: true, modal: false, slide: false, 
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
        function onOkEditDetailRecord2(){
            grid.loadData(true);
            $.ligerDialog.close();
        }
        function onCancelEditDetailRecord2(){
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
                    'EditMasterDetailViewRecord.aspx',
                    {
                        Action: 'Delete',
                        vid: '<%=Request["vid"] %>',
                        ParentId:'<%=Request["ParentId"] %>',
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
        
        var grid;
        $(function ()
        {
            grid = $("#gridDetail").ligerGrid({
            columns: [
                <%
                CViewDetail ViewDetail = (CViewDetail)m_View.ViewDetailMgr.GetFirstObj();
                CTable table = (CTable)Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.Find(ViewDetail.FW_Table_id);
                List<CBaseObject> lstObjCIVD=ViewDetail.ColumnInViewDetailMgr.GetList();
                for (int i=0;i<lstObjCIVD.Count;i++)
                {
                    CColumnInViewDetail civd = (CColumnInViewDetail)lstObjCIVD[i];
                    CColumn col = (CColumn)table.ColumnMgr.Find(civd.FW_Column_id);
                    if (col == null)
                        continue;
                    if(i<lstObjCIVD.Count-1)
                        Response.Write(string.Format("{{ display: '{0}', name: '{1}'}},",col.Name,col.Code));
                    else
                        Response.Write(string.Format("{{ display: '{0}', name: '{1}'}}",col.Name,col.Code));
                }
                 %>
                ],
                url: 'EditMasterDetailViewRecord.aspx?Action=GetDetail&vid=<%=Request["vid"] %>&ParentId=<%=Request["ParentId"] %>&id=<%=Request["id"] %>',
                dataAction: 'server', 
                usePager: false,
                width: '100%', height: 150,
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
    <style type="text/css">
           body{ font-size:12px;}
        .l-table-edit {}
        .l-table-edit-td{ padding:4px;}
        .l-button-submit,.l-button-test{width:80px; float:left; margin-left:10px; padding-bottom:2px;}
        .l-verify-tip{ left:230px; top:120px;}
    </style>
</head>
<body style="padding:10px">
    <iframe id="submitfrm" name="submitfrm" style="display: none"></iframe>
    <form id="form1" enctype="multipart/form-data" method="post" target="submitfrm">
    <input type="hidden" name="Action" value="PostData"/>
    <input type="hidden" name="vid" value="<%=Request["vid"] %>"/>
    <input type="hidden" name="id" value="<%=Request["id"] %>"/>
    <input type="hidden" name="ParentId" value="<%=Request["ParentId"] %>"/>
    <div>
        <uc1:ViewRecordCtrl ID="recordCtrl" runat="server" />
  <div id="toptoolbar"></div> 
   <div id="gridDetail" style="margin:0; padding:0"></div>
    </div>
    </form>
    
</body>
</html>
