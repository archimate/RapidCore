<%@ Page Language="C#" AutoEventWireup="true" CodeFile="show.aspx.cs" Inherits="Store_show" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="ErpCoreModel.Framework" %>
<%@ Import Namespace="ErpCoreModel.Store" %>
<%@ Register tagprefix="top" tagname="top" src="top.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<HEAD>
<TITLE>ErpCore电子商务</TITLE>
<LINK rel="stylesheet" 
href="image/css1.css">
<LINK rel="stylesheet" href="image/css2.css">
<META name="GENERATOR" content="MSHTML 9.00.8112.16446">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
    <script language="jscript">
        function ShowTabContent(i) {
            if (i == 0) {
                document.getElementById("detail").style.display = "block";
                document.getElementById("scoring").style.display = "none";
            }
            else {
                document.getElementById("detail").style.display = "none";
                document.getElementById("scoring").style.display = "block";
            }
        }
</script>
</HEAD>
<BODY>

<div style="margin-left: auto; margin-right: auto">
<form runat=server id="form1">

<top:top ID="top" runat="server" />

<div style="font-size: large; font-weight: bold; color: #000000" >
<table border="0" cellspacing="0" cellpadding="0" width="1000px" align="center">
	<tr>
		<td>
<h2 ><em>
    <asp:Label ID="lbProductName" runat="server" Text=""></asp:Label></em></h2>
		</td>
	</tr>
</table>
</div>
<div>
<table border="0" cellspacing="0" cellpadding="0" width="1000px" align="center">
	<tr>
		<td>
<div style="border-style: dotted; border-width: 1px; width: 400px; height: 500px; ">
    <table class="style1">
        <tr>
            <td>
            <%CProductImg pimg = (CProductImg)m_Product.ProductImgMgr.GetFirstObj();
              string sImgUrl = "";
              if (pimg != null)
                  sImgUrl = pimg.GetFileName(); %>
                <img id="showImg"  width="400" height="400" src="ProductImg/<%=sImgUrl %>"/></td>
        </tr>
        <tr>
            <td>
                <table cellspacing="10"><tr>
                <%List<CBaseObject> lstPImg = m_Product.ProductImgMgr.GetList();
                  for (int i = 0; i < lstPImg.Count;i++ )
                  {
                      CProductImg img = (CProductImg)lstPImg[i];
                         %>
                <td><img id="icon<%=i%>" border="1"  width="100" height="100" src="ProductImg/<%=img.GetFileName() %>" onmouseover="document.getElementById('showImg').src='ProductImg/<%=img.GetFileName() %>'"/></td>
                <%} %>
                </tr></table>
            </td>
        </tr>
    </table>
            </div>
		</td>
		<td>
<div style=" padding: 10px; width: 600px; height: 500px; color: #000000;">
<table border="0" cellspacing="0" cellpadding="0" width="100%">
<%//如果是促销商品，显示促销价格，否则显示零售价与批发价
    if (m_PromotionMgr.FindByProduct(m_Product.Id) != null)
    {
        List<CBaseObject> lstPrice = m_Product.PriceMgr.FindByType(PriceType.Promotion);
        string sPrice = "";
        if (lstPrice.Count > 0)
        {
            CPrice price = (CPrice)lstPrice[0];
            sPrice = price.Price.ToString();
        }
%>
	<tr>
		<td width="100">促销价格：</td><td><span style="color: #FF0000; font-size: x-large"><%=sPrice%> </span>元/<%=m_Product.Unit%></td>
	</tr>
	<%}
    else
    {        
        List<CBaseObject> lstPrice = m_Product.PriceMgr.FindByType(PriceType.Retail);
        string sPrice = "";
        if (lstPrice.Count > 0)
        {
            CPrice price = (CPrice)lstPrice[0];
            sPrice = price.Price.ToString();
    %>
	<tr>
		<td width="100">零售价格：</td><td><span style="color: #FF0000; font-size: x-large"><%=sPrice%> </span>元/<%=m_Product.Unit%></td>
	</tr>
	    <%}
        lstPrice =  m_Product.PriceMgr.FindByType(PriceType.Wholesale);
        if (lstPrice.Count > 0)
        {
        %>
	<tr>
		<td width="100">批发价格：</td>
		<td>
		<%foreach (CBaseObject obj in lstPrice)
        {
            CPrice price = (CPrice)obj;
            sPrice = price.Price.ToString();
        %>
		<p><span style="color: #FF0000; font-size: x-large"><%=sPrice%> </span>元/<%=m_Product.Unit%></p>
		<%} %>
		</td>
	</tr>
	    <%}
    }%>
</table>
<div style="height:20px"></div>
<DIV class="d-sku-title fd-clr">
<H3 class="title"><SPAN>选择规格</SPAN></H3>
</DIV>
<div style="height:20px"></div>
<table border="0"  width="100%" cellpadding="10" cellspacing="10">
	<tr>
		<td width="100">颜色：</td><td>
        <asp:RadioButtonList ID="rdlistColor" runat="server">
        </asp:RadioButtonList>
        </td>
	</tr>
	<tr><td>&nbsp;</td><td>&nbsp;</td></tr>
	<tr>
		<td>尺码：</td><td>
        <asp:RadioButtonList ID="rdlistSpecification" runat="server">
        </asp:RadioButtonList>
        </td>
	</tr>
	<tr><td>&nbsp;</td><td>&nbsp;</td></tr>
	<tr>
		<td>我要订购：</td><td>
        <asp:TextBox ID="txtNum" runat="server"></asp:TextBox>
        <asp:Label ID="lbUnit" runat="server"></asp:Label>
        </td>
	</tr>
	<tr><td>&nbsp;</td><td>&nbsp;</td></tr>
	<tr>
		<td>&nbsp;</td><td>
        <asp:ImageButton ID="imgbtOrder" runat="server" 
            ImageUrl="image/btn4.png" onclick="imgbtOrder_Click" />
&nbsp;
        <asp:ImageButton ID="imgbtAdd" runat="server" 
            ImageUrl="image/btn3.png" onclick="imgbtAdd_Click" />
        </td>
	</tr>
</table>
</div>
		</td>
	</tr>
</table>
</div>
<div></div>
<DIV id="mod-detail-otabs" class="mod-detail-new-otabs" 
    style="overflow: auto; margin: auto;width:1000px">
<UL>
  <LI class="first de-selected"  onmouseover="ShowTabContent(0);this.className='first de-selected'" onmouseout="this.className='first'">
      <a ><SPAN>详细信息</SPAN></a>
  </LI>
  <LI  onmouseover="ShowTabContent(1);this.className='de-selected'" onmouseout="this.className=''">
  <a ><SPAN>成交(<EM><asp:Label ID="lbOrderNum" runat="server" Text=""></asp:Label></EM>)&nbsp;/&nbsp;评价(<EM>
  <asp:Label
      ID="lbScoringNum" runat="server" Text=""></asp:Label></EM>)</SPAN></a>
  </LI>
  
</DIV>

<div id="detail" style="overflow: auto; margin: auto;width:1000px"><%=m_Product.Detail %></div>

<div id="scoring" style="overflow: auto; margin: auto;display:none;width:1000px">
    <asp:GridView ID="gridScoring" runat="server" CellPadding="4" ForeColor="#333333" 
        GridLines="None">
        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
</div>


<!--#include file="bottom.htm"-->
</form>
</div>
</BODY></HTML>
