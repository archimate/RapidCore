<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserInfo.aspx.cs" Inherits="Store_UserInfo" %>
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
</head>
<body>
    <form id="form1" runat="server">
    <div>
<top:top ID="top" runat="server" />
    
    
 <div style="margin: auto; width: 1000px;height: 600px; overflow: auto; color: #000000;"> 
 <div style="padding: 10px; float: left; width: 200px;">
 <ul style="border-style: groove; border-width: thin; ">
    <li><a href="baseinfo.aspx" target="show">基本资料</a></li>
    <li><a href="modpwd.aspx" target="show">修改密码</a></li>
    <li><a href="account.aspx" target="show">账户余额</a></li>
 </ul>
 </div>
 <div style="float: right; width: 750px;">
 <iframe id="show" name="show" style="width: 700px; height: 580px" frameborder="0" 
         scrolling="no" src="baseinfo.aspx"></iframe>
 </div>
 </div>
    
<!--#include file="bottom.htm"-->
    </div>
    </form>
</body>
</html>
