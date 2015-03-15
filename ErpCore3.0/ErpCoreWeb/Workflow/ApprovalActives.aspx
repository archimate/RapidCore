<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApprovalActives.aspx.cs" Inherits="Workflow_ApprovalActives" %>
<%@ Import Namespace="ErpCoreModel" %>
<%@ Import Namespace="ErpCoreModel.Base" %>
<%@ Import Namespace="ErpCoreModel.Framework" %>
<%@ Import Namespace="ErpCoreModel.Workflow" %>
<%@ Import Namespace="System.Collections.Generic" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" /> 
    <link href="../lib/ligerUI/skins/Gray/css/all.css" rel="stylesheet" type="text/css" /> 
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
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
    
    <script type="text/javascript">
        var eee;
        $(function() {
            $.metadata.setType("attr", "validate");
            var v = $("form").validate({
                debug: true,
                errorPlacement: function(lable, element) {
                    if (element.hasClass("l-textarea")) {
                        element.ligerTip({ content: lable.html(), target: element[0] });
                    }
                    else if (element.hasClass("l-text-field")) {
                        element.parent().ligerTip({ content: lable.html(), target: element[0] });
                    }
                    else {
                        lable.appendTo(element.parents("td:first").next("td"));
                    }
                },
                success: function(lable) {
                    lable.ligerHideTip();
                    lable.remove();
                },
                submitHandler: function() {
                    $("form .l-text,.l-textarea").ligerHideTip();
                    alert("Submitted!")
                }
            });
            $("form").ligerForm();
        });


        function onAccept() {
            $.post(
                'ApprovalActives.aspx',
                {
                    Action: 'Accept',
                    TbCode: '<%=Request["TbCode"] %>',
                    id: '<%=Request["id"] %>',
                    WF_Workflow_id: '<%=Request["WF_Workflow_id"] %>',
                    ParentId: '<%=Request["ParentId"] %>',
                    Comment: $("#Comment").val()
                },
                 function(data) {
                     if (data == "" || data == null) {
                         parent.grid2.loadData();
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
        function onReject() {
            $.post(
                'ApprovalActives.aspx',
                {
                    Action: 'Reject',
                    TbCode: '<%=Request["TbCode"] %>',
                    id: '<%=Request["id"] %>',
                    WF_Workflow_id: '<%=Request["WF_Workflow_id"] %>',
                    ParentId: '<%=Request["ParentId"] %>',
                    Comment: $("#Comment").val()
                },
                 function(data) {
                     if (data == "" || data == null) {
                         parent.grid2.loadData();
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
    <form id="form1" >
    <div>
    <%
        CActivesDefMgr ActivesDefMgr = new CActivesDefMgr();
        ActivesDefMgr.Ctx = Global.GetCtx(Session["TopCompany"].ToString());
        string sWhere = string.Format("id='{0}'", m_Actives.WF_ActivesDef_id);
        ActivesDefMgr.GetList(sWhere);
        CActivesDef ActivesDef = (CActivesDef)ActivesDefMgr.GetFirstObj();
        string sName = "";
        if (ActivesDef != null)
            sName = ActivesDef.Name;
        %>
        <table cellpadding="0" cellspacing="0" class="l-table-edit" >
        <tr><td class="l-table-edit-td">活动名称：</td><td class="l-table-edit-td">
        <input name="Name" readonly="readonly" value="<%=sName %>" /></td></tr>
        <tr><td class="l-table-edit-td">审批内容：</td><td class="l-table-edit-td">
            <textarea id="Comment" name="Comment" cols="50" rows="10" class="l-textarea"></textarea>
        </td></tr>            
        </table>
    </div>
    </form>
</body>
</html>
