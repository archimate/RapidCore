<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="Store_login" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="ErpCoreModel.Framework" %>
<%@ Import Namespace="ErpCoreModel.Store" %>
<%@ Register tagprefix="top" tagname="top" src="top.ascx" %>

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
<top:top ID="top" runat="server" />

<div style="margin: auto; width: 800px;height: 600px; overflow: auto; color: #000000;">
        <div style="height: 30px"></div>
        <div style="margin: auto;width: 100px; font-size: large; font-weight: bold;"><span>会员登录</span></div>
        <table class="style1" align="center" border="1">
            <tr>
                <td align="right">
                    用户名：</td>
                <td>
                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="txtName" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    密码：</td>
                <td>
                    <asp:TextBox ID="txtPwd" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="txtPwd" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:ImageButton ID="imgLogin" runat="server" 
                        ImageUrl="image/btn7.png" onclick="imgLogin_Click" />
&nbsp;&nbsp;&nbsp; <a href="reg.aspx?to=<%=Request["to"] %>"><img src="image/btn8.png" border="0" /></a></td>
            </tr>
        </table>
</div>
    
<!--#include file="bottom.htm"-->
    </div>
    </form>
</body>
</html>
