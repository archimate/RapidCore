<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:400px;" align="center">
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
                <td style="text-align: right">
                    用户名：</td>
                <td>
                    <asp:TextBox ID="txtName" runat="server" Width="229px">admin</asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="txtName" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">
                    密码：</td>
                <td>
                    <asp:TextBox ID="txtPwd" runat="server" TextMode="Password" Width="228px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                    <asp:Button ID="btLogin" runat="server" Text="登录" Width="90px" 
                        onclick="btLogin_Click" />
&nbsp;&nbsp; 
                    <a href="ForgetPwd.aspx">忘记密码</a>&nbsp;&nbsp; <a href="reg.aspx">注册</a></td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
