<%@ Page Language="C#" AutoEventWireup="true" CodeFile="category.aspx.cs" Inherits="Store_category" %>
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

<% 
  List<CBaseObject> lstTIC = m_TypeInCategoryMgr.GetList(m_ProductCategory.Id);
%>
<DIV class="layout-col">
<DIV class="grid">
<DIV id="mod-floor-children" class="mod-floor mod-floor-children">
<DIV class="mod-header">
<H2><EM><span style="font-size: x-large"><%=m_ProductCategory.Name%></span></EM></H2>
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
  <LI class="f-tab-t <%=sExtStyle%>" onmouseover="ShowTabContent(0,<%=j%>,<%=lstTIC.Count%>);this.className='f-tab-t current'" onmouseout="this.className='f-tab-t'"><A title="" href="http://wholesale.china.alibaba.com/xshow/habitat_offer.htm?spm=b26110219.152011.0.101"><%=ptype.Name %></A></LI>
<% }%>
</UL></DIV>
</DIV>
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
<DIV class="f-tab-b" id="tabcontent0_<%=j%>" <%=sExtStyle%> >
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

</DIV>
</div>

<!--#include file="bottom.htm"-->
</form>
</BODY></HTML>
