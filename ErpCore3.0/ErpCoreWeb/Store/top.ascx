<%@ Control Language="C#" AutoEventWireup="true" CodeFile="top.ascx.cs" Inherits="Store_top" %>
<%@ Import Namespace="ErpCoreModel.Framework" %>
<%@ Import Namespace="ErpCoreModel.Store" %>

<DIV style="overflow: auto;overflow-x:hidden;">
<div style="margin: auto; width: 1000px; height: 80px; border-bottom-style: groove;">
<DIV class="ali-logo" style="float: left"><IMG 
alt="erpcore" src="image/logo.gif">
</DIV>
<div style="float: right">
<%if (Session["Customer"] == null)
  { %>
    <a href="login.aspx">登录</a>
    &nbsp;&nbsp;&nbsp;
    <a href="reg.aspx">注册</a>
    <%}
  else
  { %>
        欢迎您 <%=((CCustomer)Session["Customer"]).Name%>
    &nbsp;&nbsp;&nbsp;
    <a href="UserInfo.aspx">我的资料</a>
    &nbsp;&nbsp;&nbsp;
    <a href="MyOrder.aspx">我的订单</a>
    <%} %>
    &nbsp;&nbsp;&nbsp;
    <a href="Order.aspx">购物篮</a>
</div>
</div>
</DIV>

