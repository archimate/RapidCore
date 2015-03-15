<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderAddr.aspx.cs" Inherits="Store_OrderAddr" %>
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
    <div style=" color: #000000;"><span style="font-size: large; font-weight: bold; color: #000000;"><em>购买商品清单</em></span></div>
    <table align="center" style="color: #000000; width: 800px;" border="1">
    <tr><td>名称</td><td>颜色</td><td>型号</td><td>数量</td><td>单价</td></tr>
    <%double dblTotal=0;
      if (m_Order != null)
      {
          List<CBaseObject> lstObj = m_Order.OrderDetailMgr.GetList();
          foreach (CBaseObject obj in lstObj)
          {
              COrderDetail od = (COrderDetail)obj;
              string sName = "";
              CProduct product = (CProduct)Global.GetStore().ProductMgr.Find(od.SP_Product_id);
              sName = product.Name;
              //根据数量计算单价
              double dblPrice = CalcPrice(product, od);

              dblTotal += dblPrice * od.Num;
              
    %>
    <tr><td><%=sName%></td><td><%=od.Color%></td><td><%=od.Specification%></td><td><%=od.Num%></td><td><span style="color: #FF0000;"><%=dblPrice%></span>元/<%=product.Unit %></td></tr>
        <%}
      } %>
    </table>
    <div style="margin: auto; width: 800px; overflow: auto;"><span style=" color: #000000;">合计：<span style="color: #FF0000; font-size: x-large"><%=dblTotal%></span>元</span></div>
    <div style="height:20px"></div>
    
    <div style=" color: #000000;"><span style="font-size: large; font-weight: bold; color: #000000;"><em>送货信息</em></span></div>
    <div>
    <table cellpadding="10" cellspacing="10" width="500">
    <tr>
    <td>省：</td><td>
        <asp:DropDownList ID="cbProvince" runat="server" 
            onselectedindexchanged="cbProvince_SelectedIndexChanged" 
            AutoPostBack="True">
        </asp:DropDownList>
    </td>
    </tr>
    <tr>
    <td>城市：</td><td>
        <asp:DropDownList ID="cbCity" runat="server" 
            onselectedindexchanged="cbCity_SelectedIndexChanged" AutoPostBack="True">
        </asp:DropDownList>
    </td>
    </tr>
    <tr>
    <td>地区：</td><td>
        <asp:DropDownList ID="cbDistrict" runat="server">
        </asp:DropDownList>
    </td>
    </tr>
    <tr>
    <td>详细地址：</td><td>
        <asp:TextBox ID="txtAddr" runat="server" Width="281px"></asp:TextBox>
    </td>
    </tr>
    <tr>
    <td>邮编：</td><td>
        <asp:TextBox ID="txtZipcode" runat="server"></asp:TextBox>
    </td>
    </tr>
    <tr>
    <td>收货人姓名：</td><td>
        <asp:TextBox ID="txtContacts" runat="server"></asp:TextBox>
    </td>
    </tr>
    <tr>
    <td>联系电话：</td><td>
        <asp:TextBox ID="txtTel" runat="server"></asp:TextBox>
    </td>
    </tr>
    <tr>
    <td>手机：</td><td>
        <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
    </td>
    </tr>
    </table>
    </div>
    
    <div style=" color: #000000;"><span style="font-size: large; font-weight: bold; color: #000000;"><em>付款方式</em></span></div>
    <div>您的账户余额为：<asp:Label ID="lbAccount" runat="server" ForeColor="#FF0066" 
            Font-Size="Large"></asp:Label>元</div>
    <div>
        <asp:RadioButtonList ID="rdlistPayMode" runat="server">
            <asp:ListItem Value="0">从账户扣</asp:ListItem>
            <asp:ListItem Value="1">货到付款</asp:ListItem>
        </asp:RadioButtonList>
    </div>
    <div style="height: 20px"></div>
    <div>
        <asp:ImageButton ID="imgbtOk" runat="server" ImageUrl="image/btn2.png" 
            onclick="imgbtOk_Click" /></div>
    </div>
    
<!--#include file="bottom.htm"-->
    </div>
    </form>
</body>
</html>
