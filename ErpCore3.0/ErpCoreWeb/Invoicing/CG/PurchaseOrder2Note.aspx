<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PurchaseOrder2Note.aspx.cs" Inherits="Invoicing_CG_PurchaseOrder2Note" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>生成采购单</title>
    <link href="../../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" /> 
    <link href="../../lib/ligerUI/skins/Gray/css/all.css" rel="stylesheet" type="text/css" /> 
    <script src="../../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
     <script src="../../lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerDateEditor.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerButton.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerRadio.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerSpinner.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerTextBox.js" type="text/javascript"></script> 
    <script src="../../lib/ligerUI/js/plugins/ligerTip.js" type="text/javascript"></script>
    <script src="../../lib/jquery-validation/jquery.validate.min.js" type="text/javascript"></script> 
    <script src="../../lib/jquery-validation/jquery.metadata.js" type="text/javascript"></script>
    <script src="../../lib/jquery-validation/messages_cn.js" type="text/javascript"></script>
    

    <script language="javascript" type="text/javascript">
// <!CDATA[

        $(function() {
            var row = parent.grid.getSelectedRow();
            if (row == null) {
                $.ligerDialog.alert('请选择行!', function(type) { parent.$.ligerDialog.close(); });
                return;
            }
            $("#hidOrderId").val(row.id);
            $("#txtOrderCode").val(row.Code);
            $("#txtNoteCode").val(row.Code);
        });


        function btCancel_onclick() {
            parent.$.ligerDialog.close();
        }

// ]]>
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <input type="hidden" id="hidOrderId"  runat="server"/>
    <div>
        <div>生成采购单</div>
        <table>
            <tr><td>采购订单编号：</td><td>
                <input id="txtOrderCode" type="text" disabled="disabled"/>
                </td></tr>
            <tr><td>生成采购单编号：</td><td>
                <input id="txtNoteCode" type="text"  runat="server"/>
                </td></tr>
            <tr><td colspan="2">
                <asp:Button ID="btOk" runat="server" style=" width:60px" Text="生成" 
                    onclick="btOk_Click" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <input id="btCancel" type="button"  style=" width:60px"  value="取消" onclick="return btCancel_onclick()" /></td></tr>
        </table>
    </div>
    </form>
</body>
</html>
