<%@ Page Language="C#" AutoEventWireup="true" CodeFile="modpwd.aspx.cs" Inherits="Store_modpwd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<LINK rel="stylesheet" 
href="image/css1.css">
<LINK rel="stylesheet" href="image/css2.css">
    <style type="text/css">
        .style1
        {
            width: 400px;
            color:Black;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div style="height: 30px"></div>
        <div style="margin: auto;width: 100px; font-size: large; font-weight: bold;">
            <span style="color: #000000">修改密码</span></div>
    <div style="color: #000000;">
        
        <table class="style1" align="center" border="1">
            <tr>
                <td align="right">
                    用户名：</td>
                <td>
                    <asp:TextBox ID="txtName" runat="server" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    新密码：</td>
                <td>
                    <asp:TextBox ID="txtPwd" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="txtPwd" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    确认密码：</td>
                <td>
                    <asp:TextBox ID="txtPwd2" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="txtPwd2" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    &nbsp;<asp:ImageButton ID="imgSave" runat="server" 
                        ImageUrl="image/btn2.png" onclick="imgSave_Click" />
                </td>
            </tr>
        </table>
</div>
    </div>
    </form>
</body>
</html>
