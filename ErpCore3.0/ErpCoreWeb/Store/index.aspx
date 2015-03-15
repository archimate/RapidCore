<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="Store_index" %>
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
<STYLE>
      	.mod-floor .mod-body .goods-list .attr .qbegin{top:-1px;}
      </STYLE>

<META name="GENERATOR" content="MSHTML 9.00.8112.16446">
<script language="jscript">
    function ShowTabContent(floor, itemidx, itemcount) {
        var tabname ;
        for (var i = 0; i < itemcount; i++) {
            tabname = "tabcontent" + floor + "_" + i;
            document.getElementById(tabname).style.display = "none";
        }
        tabname = "tabcontent" + floor + "_" + itemidx;
        document.getElementById(tabname).style.display = "block";

    }
</script>
</HEAD>
<BODY >
<form id="form1" runat="server">
<div style="margin-left: auto; margin-right: auto">

<top:top ID="top" runat="server" />

<DIV id="content" class="screen">

<DIV class="layout layout-s5m0">
<DIV class="grid-main">
<DIV class="mod-hot-goods">
<DIV class="header">
<DIV class="cell-header">
<H4 class="title ms-yh"><em>促销产品</em></H4>
</DIV></DIV>
<DIV class="content">
<DIV class="wrap">
<DIV class="f-tab-b  fd-clr">
<DIV class="offer-list">
<UL class="fd-clr">
<%
    List<CBaseObject> lstP = m_PromotionMgr.GetList();
    foreach (CBaseObject obj in lstP)
    {
        CPromotion promotion = (CPromotion)obj;
        CProduct product = (CProduct)m_ProductMgr.Find(promotion.SP_Product_id);
        if (product == null) continue;
        List<CBaseObject> lstPrice = product.PriceMgr.GetList();
        string sPrice1="",sPrice2="";
        foreach (CBaseObject objPr in lstPrice)
        {
            CPrice price = (CPrice)objPr;
            if (price.Type == PriceType.Promotion) //促销价
                sPrice1 = price.Price.ToString();
            else if (price.Type == PriceType.Retail) //零售价
                sPrice2 = price.Price.ToString();
        }
        //图片地址
        string sImg = "";
        CProductImg img = (CProductImg)product.ProductImgMgr.GetFirstObj();
        if (img != null)
            sImg = img.GetFileName();
%>
  <LI>
  <DL class="cell-product-3rd">
    <DT><A class="a-img" title="<%=product.Name %>" href="show.aspx?id=<%=product.Id %>" target=_blank><IMG 
    alt="<%=product.Name %>" src="ProductImg/<%=sImg %>" border="0"></A></DT>
    <DD class="description"><A title="<%=product.Name %>" href="show.aspx?id=<%=product.Id %>" target=_blank><%=product.Name %></A></DD>
    <DD><LABEL>促销价：</LABEL><EM><SPAN 
    class="fd-cny">&yen;<STRONG><%=sPrice1%></STRONG></SPAN></EM></DD>
    <DD class="sale-quantity">原　价：<EM><SPAN 
    class="fd-cny" style="text-decoration:line-through">&yen;<STRONG><%=sPrice2%></STRONG></SPAN></EM></DD>
    <DD class="company"><A 
    href=""><%=product.Factory %></A></DD></DL></LI>
    <%} %>
 
</UL>
</DIV>
</DIV>
</DIV></DIV>
</DIV></DIV></DIV><!--[if !IE]>layout-s5m0 end<![endif]--><!--[if !IE]>layout-col start<![endif]-->

<% //循环产品类别
    List<CBaseObject> lstCategory = m_ProductCategoryMgr.GetList("","Idx");
    for (int i = 0; i < lstCategory.Count; i++)
    {
        CProductCategory category = (CProductCategory)lstCategory[i];
        List<CBaseObject> lstTIC = m_TypeInCategoryMgr.GetList(category.Id);
%>
<DIV class="layout-col">
<DIV class="grid">
<DIV id="mod-floor-children" class="mod-floor mod-floor-children">
<DIV class="mod-header">
<H2><EM><span style="font-size: x-large"><%=category.Name%></span></EM></H2>
<DIV class="tag-list">
<UL>
<% for (int j = 0; j < lstTIC.Count; j++)
   {
       CTypeInCategory tic = (CTypeInCategory)lstTIC[j];
       CProductType ptype = (CProductType)m_ProductTypeMgr.Find(tic.SP_ProductType_id);
       if (ptype == null) continue;
       string sExtStyle = "";
       //if (j == 0)
       //    sExtStyle = "current";
       //else if (j == lstTIC.Count - 1)
       //    sExtStyle = "last";
%>
  <LI class="f-tab-t <%=sExtStyle%>" onmouseover="ShowTabContent(<%=i%>,<%=j%>,<%=lstTIC.Count%>);this.className='f-tab-t current'" onmouseout="this.className='f-tab-t'"><A title="" href="http://wholesale.china.alibaba.com/xshow/habitat_offer.htm?spm=b26110219.152011.0.101"><%=ptype.Name %></A></LI>
<% }%>
</UL></DIV>
<DIV class="more"><A 
href="category.aspx?id=<%=category.Id %>">进入<%=category.Name%>频道</A></DIV></DIV>
<DIV class="mod-body">
<DIV class="items"><!--[if !IE]>f-tab-b1 start<![endif]-->
<% for (int j = 0; j < lstTIC.Count; j++)
   {
       CTypeInCategory tic = (CTypeInCategory)lstTIC[j];
       CProductType ptype = (CProductType)m_ProductTypeMgr.Find(tic.SP_ProductType_id);
       if (ptype == null) continue;
       string sExtStyle = "";
       if (j > 0)
           sExtStyle = "style='display:none'";
%>
<DIV class="f-tab-b" id="tabcontent<%=i%>_<%=j%>" <%=sExtStyle%> >
<DIV class="goods">
<DIV class="goods-list">
<UL>
    <% List<CBaseObject> lstPIT = m_ProductInTypeMgr.GetList(ptype.Id);
       int iCount = 0;
       foreach (CBaseObject obj in lstPIT)
       {
           CProductInType pit = (CProductInType)obj;
           CProduct product = (CProduct)m_ProductMgr.Find(pit.SP_Product_id);
           if (product == null) continue;
           iCount++;
           if (iCount >= 5) break;//只显示前5个
           //图片地址
           string sImg = "";
           CProductImg img = (CProductImg)product.ProductImgMgr.GetFirstObj();
           if (img != null)
               sImg = img.GetFileName();
    %>
  <LI class="goods hover2">
  <DIV class="wrap">
  <DIV class="pic"><A title="<%=product.Name %>" href="show.aspx?id=<%=product.Id %>" target=_blank><IMG 
  alt="" width="180" height="220" src="ProductImg/<%=sImg %>" border="0"></A>
  <DIV class="mask"></DIV>
  </DIV>
  <H2><A title="<%=product.Name %>" href="show.aspx?id=<%=product.Id %>" target=_blank>
  <%=product.Name %></A></H2>
  <DIV>
  <%List<CBaseObject> lstPrice = GetPriceOrderByType(product.PriceMgr);
    foreach (CBaseObject objP in lstPrice)
    {
        CPrice price = (CPrice)objP;
        if (price.Idx != 0) //仅显示第一个价格
            continue;
        string sText = "零售价：";
        if (price.Type == PriceType.Wholesale)
            sText = "批发价：";
        else if (price.Type == PriceType.Promotion)
            sText = "促销价：";
  %>
  <P ><%=sText%><em><span class="fd-cny">&yen;<strong><%=price.Price%></strong></span></em></P>
  <%} %>
  </DIV>
  <DIV class="com">
  <H3><A title="" href=""><%=product.Factory %></A></H3>
  </DIV></DIV></LI>
  <%   }%>
</UL></DIV></DIV></DIV>
<% }%>
</DIV>
</DIV></DIV></DIV></DIV>
<%}%>
</DIV>
</div>

<!--#include file="bottom.htm"-->
</form>
</BODY></HTML>
