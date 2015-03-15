<%@ Page Language="C#" AutoEventWireup="true" CodeFile="reg.aspx.cs" Inherits="Store_reg" %>

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
        <div style="margin: auto;width: 100px; font-size: large; font-weight: bold;"><span>免费注册</span></div>
        <table class="style1" align="center" border="1">
            <tr>
                <td>
                    用户名：</td>
                <td>
                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="txtName" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    密码：</td>
                <td>
                    <asp:TextBox ID="txtPwd" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="txtPwd" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
确认密码：</td>
                <td>
                    <asp:TextBox ID="txtPwd2" runat="server" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    姓名：</td>
                <td>
                    <asp:TextBox ID="txtTName" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    性别：</td>
                <td>
                    <asp:DropDownList ID="cbSex" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    生日：</td>
                <td>
                    <asp:DropDownList ID="cbYear" runat="server">
                    </asp:DropDownList>
                    年<asp:DropDownList ID="cbMonth" runat="server">
                    </asp:DropDownList>
                    月<asp:DropDownList ID="cbDay" runat="server">
                    </asp:DropDownList>
                    日</td>
            </tr>
            <tr>
                <td>
                    通讯地址：</td>
                <td>
                    <asp:TextBox ID="txtAddr" runat="server" Width="291px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    邮编：</td>
                <td>
                    <asp:TextBox ID="txtZipcode" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    单位：</td>
                <td>
                    <asp:TextBox ID="txtCompany" runat="server" Width="288px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    电话：</td>
                <td>
                    <asp:TextBox ID="txtTel" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    手机：</td>
                <td>
                    <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Email：</td>
                <td>
                    <asp:TextBox ID="txtEmail" runat="server" Width="285px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    QQ：</td>
                <td>
                    <asp:TextBox ID="txtQQ" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    旺旺：</td>
                <td>
                    <asp:TextBox ID="txtWW" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:ImageButton ID="imgReg" runat="server" ImageUrl="image/btn8.png" 
                        onclick="imgReg_Click" />
                </td>
            </tr>
        </table>
   </div>
    
<!--#include file="bottom.htm"--> h 
    </div>
    </form>
</body>
</html>
