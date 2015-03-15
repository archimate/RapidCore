<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderResult.aspx.cs" Inherits="Store_OrderResult" %>

<%@ Register tagprefix="top" tagname="top" src="top.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<LINK rel="stylesheet" 
href="image/css1.css">
<LINK rel="stylesheet" href="image/css2.css">
</head>
<body>
    <form id="form1" runat="server">
    <div>
<top:top ID="top" runat="server" />


<div style="margin: auto; width: 800px;height: 600px; overflow: auto; color: #000000;">
<%if (Request["ret"] == "1")
  { %>
  订单提交成功，您的订单号是：<span style="color: #FF0000; font-size: large;"><%=Request["code"] %></span>
  <%}
  else
  { %>
  订单提交失败，请与管理员联系！
  <%} %>
</div>
    
<!--#include file="bottom.htm"-->
    </div>
    </form>
</body>
</html>
