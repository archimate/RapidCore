<%@ Page Language="C#" AutoEventWireup="true" CodeFile="reg.aspx.cs" Inherits="reg" %>

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
    <style type="text/css">
        
        #btCancel
        {
            width: 80px;
        }
    </style>

    <script language="javascript" type="text/javascript">
// <!CDATA[

        function btCancel_onclick() {
            window.history.back();
        }

// ]]>
    </script>
</head>
<body style="padding:10px">
    <form id="form1" runat="server">
    <div>
    
        <table style="width:500px;" align="center">
            <tr>
                <td colspan="2" style="text-align: center">
                    注册</td>
            </tr>
            <tr>
                <td style="text-align: right">
                    单位名称：</td>
                <td>
                    <asp:TextBox ID="txtCompany" runat="server" Width="240px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="txtCompany" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">
                    地址：</td>
                <td>
                    <asp:TextBox ID="txtAddr" runat="server" Width="240px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                        ControlToValidate="txtAddr" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">
                    邮编：</td>
                <td>
                    <asp:TextBox ID="txtZipcode" runat="server" Width="240px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">
                    电话：</td>
                <td>
                    <asp:TextBox ID="txtTel" runat="server" Width="240px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                        ControlToValidate="txtTel" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">
                    联系人：</td>
                <td>
                    <asp:TextBox ID="txtContact" runat="server" Width="240px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">
                    邮箱：</td>
                <td>
                    <asp:TextBox ID="txtEmail" runat="server" Width="240px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="txtEmail" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                    <asp:Button ID="btReg" runat="server" Text="提交" Width="81px" 
                        onclick="btReg_Click" />
&nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="btCancel" type="button" value="取消" onclick="return btCancel_onclick()" /></td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
