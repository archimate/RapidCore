<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StorageNoteImport.aspx.cs" Inherits="Invoicing_KC_StorageNoteImport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>导入</title>
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

function btCancel_onclick() {
    parent.$.ligerDialog.close(); 
}


// ]]>
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div>请选择报表Excel文件，Excel表的格式有严格的要求，<a href="templet/StorageNote.xls" target=_blank>模板下载</a>。</div>
    <div>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" 
                runat="server" ControlToValidate="FileUpload1" ErrorMessage=" (必选)"></asp:RequiredFieldValidator>
        <asp:FileUpload ID="FileUpload1" runat="server" Width="298px" />
    </div>
    <div>
        <asp:Button ID="btOk" runat="server" Text="确定" style="width:60px"   onclick="btOk_Click" />
        &nbsp;
        <input id="btCancel" type="button" value="取消" style="width:60px"  onclick="return btCancel_onclick()" /> 
     </div>   
            
    </div>
    </form>
</body>
</html>