<%@ Page Language="C#" AutoEventWireup="true" CodeFile="regOk.aspx.cs" Inherits="regOk" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" /> 
    <link href="lib/ligerUI/skins/Gray/css/all.css" rel="stylesheet" type="text/css" /> 
    <script src="lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
     <script src="lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="lib/ligerUI/js/plugins/ligerForm.js" type="text/javascript"></script>
    <script src="lib/ligerUI/js/plugins/ligerDateEditor.js" type="text/javascript"></script>
    <script src="lib/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>
    <script src="lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>
    <script src="lib/ligerUI/js/plugins/ligerButton.js" type="text/javascript"></script>
    <script src="lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="lib/ligerUI/js/plugins/ligerRadio.js" type="text/javascript"></script>
    <script src="lib/ligerUI/js/plugins/ligerSpinner.js" type="text/javascript"></script>
    <script src="lib/ligerUI/js/plugins/ligerTextBox.js" type="text/javascript"></script> 
    <script src="lib/ligerUI/js/plugins/ligerTip.js" type="text/javascript"></script>
    <script src="lib/jquery-validation/jquery.validate.min.js" type="text/javascript"></script> 
    <script src="lib/jquery-validation/jquery.metadata.js" type="text/javascript"></script>
    <script src="lib/jquery-validation/messages_cn.js" type="text/javascript"></script>
    
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
    <div>
    
        注册成功! 请记住您的登录帐号：<br />
        <table style="width:200px;">
            <tr>
                <td>
        单位名称：</td>
                <td>
                    <asp:Label ID="lbCompany" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    用户名：</td>
                <td>
                    <asp:Label ID="lbName" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
        密码：</td>
                <td>
                    <asp:Label ID="lbPwd" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <asp:Button ID="btLogin" runat="server" Text="登录" onclick="btLogin_Click" 
            Width="81px" />
    
    </div>
    </form>
</body>
</html>
