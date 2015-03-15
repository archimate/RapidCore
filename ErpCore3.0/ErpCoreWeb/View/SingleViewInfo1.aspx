<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SingleViewInfo1.aspx.cs" Inherits="View_SingleViewInfo1" %>

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
    <form id="form1" runat="server">
    <div >
        <p>基本定义：</p>
        <br />
        
        <table cellpadding="0" cellspacing="0" class="l-table-edit" style="height:50px">
            <tr>
                <td align="left">
                    &nbsp;名称：</td>
                <td align="left">
                    &nbsp;<asp:TextBox ID="txtName" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="left">
                    &nbsp;目录</td>
                <td align="left">
                    &nbsp;<asp:DropDownList ID="cbCatalog" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="left">
                    &nbsp;主表：</td>
                <td align="left">
                    &nbsp;<asp:DropDownList ID="cbMasterTable" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            
        </table>
        <br />
        <p>
            <asp:Button ID="btNext" runat="server" Text="下一步" Width="60px" 
                onclick="btNext_Click"  />
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btCancel" runat="server" Text="取消" Width="60px" 
                onclick="btCancel_Click"  />
            </p>
    </div>
    </form>
</body>
</html>
