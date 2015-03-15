<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectRole.aspx.cs" Inherits="Security_Access_SelectRole" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="../../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <script src="../../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script> 
    <script src="../../lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../../lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script> 
    
    <script type="text/javascript">
        var grid;
        $(function() {
            grid = $("#gridTable").ligerGrid({
                columns: [
                { display: '名称', name: 'Name',  width: 120 }
                ],
                url: 'SelectRole.aspx?Action=GetData&B_Company_id=<%=Request["B_Company_id"] %>',
                dataAction: 'server', pageSize: 30,
                width: '100%', height: '100%'
                
            });
        });


        function onSelect() {
            return grid.getSelectedRow();
        }

        function cbCompany_onchange() {
            var url = 'SelectRole.aspx?Action=GetData&B_Company_id=' +document.getElementById("cbCompany").value;
            grid.set({ url: url });
            grid.loadData();
        }

    </script>
    <style type="text/css">
           body{ font-size:12px;}
        .l-table-edit {}
        .l-table-edit-td{ padding:4px;}
        .l-button-submit,.l-button-test{width:80px; float:left; margin-left:10px; padding-bottom:2px;}
        .l-verify-tip{ left:230px; top:120px;}
    </style>
</head>
<body style="padding:6px; overflow:hidden;"> 
    <table cellpadding="0" cellspacing="0" class="l-table-edit" >
        <tr>
            <td align="right" class="l-table-edit-td">选择:</td>
            <td align="left" class="l-table-edit-td">
                <select id="cbCompany" runat="server"  onchange="return cbCompany_onchange()">
                    <option></option>
                </select>
            </td>
        </tr>
  </table>
    <div id="gridTable" style="margin:0; padding:0"></div>
</body>
</html>
