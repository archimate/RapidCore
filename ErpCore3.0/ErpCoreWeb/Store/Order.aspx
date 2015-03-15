<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Order.aspx.cs" Inherits="Store_Order" %>
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
<script type="text/javascript">
    function showEditDiv(odid,val) {
        document.getElementById("hidEditId").value = odid;
        document.getElementById("txtEditNum").value = val;
        document.getElementById("editdiv").style.display = "block";
    }
    function btCancel_onclick() {
        document.getElementById("editdiv").style.display = "none";
    }

</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>

<top:top ID="top" runat="server" />
    
    <div style="margin: auto; width: 800px;height: 600px; overflow: auto; color: #000000;">
    <div style=" color: #000000;"><span style="font-size: large; font-weight: bold; color: #000000;"><em>购买商品清单</em></span></div>
    <table align="center" style="color: #000000; width: 800px;" border="1">
    <tr><td>名称</td><td>颜色</td><td>型号</td><td>数量</td><td>单价</td><td>&nbsp;</td></tr>
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
    <tr><td><%=sName%></td><td><%=od.Color%></td><td><%=od.Specification%></td><td><%=od.Num%></td><td><span style="color: #FF0000;"><%=dblPrice%></span>元/<%=product.Unit %></td><td><a href="#" onclick="showEditDiv('<%=od.Id %>',<%=od.Num%>);">修改</a>&nbsp;&nbsp;<a href="Order.aspx?delid=<%=od.Id %>">删除</a></td></tr>
        <%}
      } %>
    </table>
    <div style="margin: auto; width: 800px; overflow: auto;"><span style=" color: #000000;">合计：<span style="color: #FF0000; font-size: x-large"><%=dblTotal %></span>元</span></div>
    <div style="height:20px"></div>
    <div>
        <asp:ImageButton ID="imgNext" runat="server" ImageUrl="image/bt5.png" 
            onclick="imgNext_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <a href="index.aspx"><img src="image/btn6.png" border="0" /></a>
    </div>
    </div>
    
    <div id="editdiv" 
            style="border-style: outset; position:absolute; display:none; top:100px; height:50pt; left: 35%; right:35%;width: 30%; color: #000000; background-color: #FFFF00;">
        <p>请输入新的订购数量</p>
        <input id="hidEditId" type="hidden"  runat=server/>
        数量：<input id="txtEditNum" type="text" runat=server/>
    <asp:Button ID="btMod" runat="server"
        Text="修改" onclick="btMod_Click" Width="60px" />&nbsp; <input id="btCancel" type="button" value="取消" style="width:60px" onclick="return btCancel_onclick()" />
    </div>
    
<!--#include file="bottom.htm"-->
    </div>
    </form>
</body>
</html>
