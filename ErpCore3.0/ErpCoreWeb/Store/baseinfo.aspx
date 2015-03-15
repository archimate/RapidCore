<%@ Page Language="C#" AutoEventWireup="true" CodeFile="baseinfo.aspx.cs" Inherits="Store_baseinfo" %>

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
    
 <div style="color: #000000;">   
        <table class="style1" align="center" border="1" cellpadding="10" cellspacing="10">
            <tr>
                <td>
                    用户名：</td>
                <td>
                    <asp:TextBox ID="txtName" runat="server" ReadOnly="True"></asp:TextBox>
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
                    <asp:Button ID="btSave" runat="server" Text="保存" Width="82px" 
                        onclick="btSave_Click"/>
                </td>
            </tr>
        </table>
   </div>
   
    </div>
    </form>
</body>
</html>
