<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OutXls.aspx.cs" Inherits="View_OutXls" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

</head>
<body>
    <form id="form1" runat="server">
    <div>
    如果不能自动下载文件，请点击这里 <a href="<%=m_url%>">下载</a>
    </div>
    </form>
    <script>
    window.location='<%=m_url%>';
    </script>
</body>
</html>
