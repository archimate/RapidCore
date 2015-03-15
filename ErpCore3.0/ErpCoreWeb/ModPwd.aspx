<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="ModPwd.aspx.cs" Inherits="_ModPwd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" /> 
    <link href="lib/ligerUI/skins/Gray/css/all.css" rel="stylesheet" type="text/css" /> 
    <script src="lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
     <script src="lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="lib/ligerUI/js/plugins/ligerDateEditor.js" type="text/javascript"></script>
    <script src="lib/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>
    <script src="lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>
    <script src="lib/ligerUI/js/plugins/ligerButton.js" type="text/javascript"></script>
    <script src="lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="lib/ligerUI/js/plugins/ligerRadio.js" type="text/javascript"></script>
    <script src="lib/ligerUI/js/plugins/ligerSpinner.js" type="text/javascript"></script>
    <script src="lib/ligerUI/js/plugins/ligerTextBox.js" type="text/javascript"></script> 
    <script src="lib/ligerUI/js/plugins/ligerTip.js" type="text/javascript"></script>
    
    <script type="text/javascript">

        function btCancel_onclick() {
            parent.$.ligerDialog.close();
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
    <p>&nbsp;</p>
    <p>&nbsp;</p>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:300px;" align="center">
            <!--
            <tr>
                <td style="text-align: right">
                    单位名称：</td>
                <td>
                    asp:TextBox ID="txtCompany" runat="server" Width="230px">ErpCore</asp:TextBox>
                    asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="txtCompany" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
            -->
            <tr>
                <td style="text-align: right; height:36px; width:60px">
                    用户名：</td>
                <td>
                    <asp:TextBox ID="txtName" runat="server" Width="200px">admin</asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; height:36px; width:60px">
                    密&nbsp;&nbsp;&nbsp;码：</td>
                <td>
                    <asp:TextBox ID="txtPwd" runat="server" TextMode="Password" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; height:36px; width:60px">
                    新密码：</td>
                <td>
                    <asp:TextBox ID="txtNewPwd" runat="server" TextMode="Password" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="txtNewPwd" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; height:36px; width:60px">
                    确认密码：</td>
                <td>
                    <asp:TextBox ID="txtNewPwd2" runat="server" TextMode="Password" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center; height:48px">
                    <asp:Button ID="btMod" runat="server" Text="确认" Width="90px" 
                        onclick="btMod_Click"  style="line-height24px"/>
&nbsp;&nbsp;<input id="btCancel" type="button" style="width:90px" value="取消" onclick="return btCancel_onclick()" /></td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
