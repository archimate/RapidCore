<%@ Page Language="C#" AutoEventWireup="true" CodeFile="account.aspx.cs" Inherits="Store_account" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="ErpCoreModel.Framework" %>
<%@ Import Namespace="ErpCoreModel.Store" %>

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
    
    <div>您的账户余额是：<span style="color: #FF0000; font-size: large; font-weight: bold"><%=m_Account.Score%></span>元</div>
    <div style="height:20px"></div>
    <div>使用记录：</div>
    <div style="color: #000000;">
        <table class="style1" border="1">
            <tr>
                <td>
                    金额</td>
                <td>
                    内容
                </td>
                <td>
                    日期
                </td>
            </tr>
            <%List<CBaseObject> lstObj = GetDetailListOrderByCreated();
              foreach (CBaseObject obj in lstObj)
              {
                  CAccountDetail detail = (CAccountDetail)obj;
                %>
            <tr>
                <td>
                    <span style="color: #FF0000"><%=detail.Score%></span>元</td>
                <td>
                    <%=detail.Content%>
                </td>
                <td>
                    <%=detail.Created%>
                </td>
            </tr>
            <%} %>
        </table>
</div>

    </div>
    </form>
</body>
</html>
