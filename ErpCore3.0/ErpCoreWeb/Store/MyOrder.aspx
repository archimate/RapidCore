<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyOrder.aspx.cs" Inherits="Store_MyOrder" %>
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

<div style="margin: auto; width: 800px;height: 600px; overflow: auto; color: #000000;">
<div style="font-size: large; font-weight: bold">我的订单</div>
<table style=" width: 100%;" border="1">
<tr><td>订单编号</td><td>订购商品</td><td>收货人姓名</td><td>订单状态</td><td>下单时间</td><td></td></tr>
<%List<CBaseObject> lstObj = GetOrderListOrderByCreated();
  foreach (CBaseObject obj in lstObj)
  {
      COrder order = (COrder)obj; %>
<tr><td><%=order.Code %></td>
<td>
<%List<CBaseObject> lstObj2 =  order.OrderDetailMgr.GetList();
  string sText = "";
  foreach (CBaseObject obj2 in lstObj2)
  {
      COrderDetail detail = (COrderDetail)obj2;
      CProduct product = (CProduct)Global.GetStore().ProductMgr.Find(detail.SP_Product_id);
      sText += string.Format("商品:<span style='color: #FF0000;'>{0}</span> 数量:<span style='color: #FF0000;'>{1}</span><br/>", product.Name, detail.Num);
  }
  Response.Write(sText);
 %>
</td>
<td><%=order.Contacts %></td>
<td>
<%=order.GetStateStr()%>
</td>
<td>
    <%=order.Created %></td>
<td>
<%if (order.State == OrderState.Init)
  {%>
      <a href="MyOrder.aspx?Cancel_id=<%=order.Id %>">撤单</a>
  <%}
  else if (order.State == OrderState.Accept)
  {
  }
  else if (order.State == OrderState.Finish)
  {
  }
  else if (order.State == OrderState.Disavailable)
  {
  }
  else if (order.State == OrderState.Cancel)
  {
  }
%>
</td></tr>
<%} %>
</table>
</div>
    
<!--#include file="bottom.htm"-->
    </div>
    </form>
</body>
</html>
