<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddLink.aspx.cs" Inherits="Workflow_AddLink" %>
<%@ Import Namespace="ErpCoreModel" %>
<%@ Import Namespace="ErpCoreModel.Base" %>
<%@ Import Namespace="ErpCoreModel.Framework" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="ErpCoreModel.Workflow" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" /> 
    <link href="../lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
     <script src="../lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerDateEditor.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerComboBox.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerCheckBox.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerButton.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerRadio.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerSpinner.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerTextBox.js" type="text/javascript"></script> 
    
    <script type="text/javascript">

        function onSubmit() {
            $.post(
                'AddLink.aspx',
                {
                <% 
                List<CBaseObject> lstCol = m_Table.ColumnMgr.GetList();
                foreach (CBaseObject obj in lstCol)
                {
                    CColumn col = (CColumn)obj;
                    if (col.Code.Equals("id", StringComparison.OrdinalIgnoreCase))
                        continue;
                    else if (col.Code.Equals("Created", StringComparison.OrdinalIgnoreCase))
                        continue;
                    else if (col.Code.Equals("Creator", StringComparison.OrdinalIgnoreCase))
                        continue;
                    else if (col.Code.Equals("Updated", StringComparison.OrdinalIgnoreCase))
                        continue;
                    else if (col.Code.Equals("Updator", StringComparison.OrdinalIgnoreCase))
                        continue;
                    
                    if (col.ColType == ColumnType.bool_type)
                    {
                    %>
                    <%=col.Code %>: $("#<%=col.Code %>").attr("checked"),
                    <%}
                    else
                    { %>
                    <%=col.Code %>: $("#<%=col.Code %>").val(),
                    <%}
                  } %>
                    Action: 'PostData',
                    wfid:'<%=Request["wfid"]%>',
                    PreActives:'<%=Request["PreActives"]%>',
                    B_Company_id:'<%=Request["B_Company_id"] %>'
                },
                 function(data) {
                     if (data == "" || data == null) {
                         parent.grid2.loadData();
                         parent.$.ligerDialog.close();
                         return true;
                     }
                     else {
                         $.ligerDialog.warn(data);
                         return false;
                     }
                 },
                 'text');
        } 
        function onCancel() {
            $.post(
                'AddLink.aspx',
                {
                    Action: 'Cancel',
                    wfid:'<%=Request["wfid"]%>',
                    PreActives:'<%=Request["PreActives"]%>',
                    B_Company_id:'<%=Request["B_Company_id"] %>'
                },
                 function(data) {
                     if (data == "" || data == null) {
                         parent.grid2.loadData();
                         parent.$.ligerDialog.close();
                         return true;
                     }
                     else {
                         $.ligerDialog.warn(data);
                         return false;
                     }
                 },
                 'text');
        } 
        
function btAdd_onclick() {
    if(form1.cbColumn.selectedIndex==-1)
    {
        $.ligerDialog.warn("请选择字段！");
        return false;
    }
    $.post(
        'AddLink.aspx',
        {
            Action: 'GetCondiction',
            wfid:'<%=Request["wfid"]%>',
            PreActives:'<%=Request["PreActives"]%>',
            Column:$("#cbColumn").val(),
            Val:$("#txtVal").val(),
            B_Company_id:'<%=Request["B_Company_id"] %>'
        },
         function(data) {
             if (data == "" || data == null) {
                 $.ligerDialog.warn("值错误！");
                 return false;
             }
             else {
                 var sExp="";
                 if($("#Condiction").val()!="")
                 {
                    sExp += $("#cbAndOr").val();
                 }
                 sExp += "["+$("#cbColumn").val()+"]";
                 sExp += $("#cbSign").val();
                 sExp += data;
                 
                 sExp = $("#Condiction").val()+sExp;
                 $("#Condiction").val(sExp);
                 return true;
             }
         },
         'text');
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
<body style="padding:10px">
    <form id="form1" runat=server>
    <div>
        <table cellpadding="0" cellspacing="0" class="l-table-edit" >
        <input name="WF_WorkflowDef_id" type="hidden" id="WF_WorkflowDef_id" value="<%=Request["wfid"] %>"/>
       
            <tr>
                <td align="right" class="l-table-edit-td">前置活动:</td>
                <td align="left" class="l-table-edit-td">
                <input name="PreActives" type="text" id="PreActives" ltype="text" readonly="readonly" value="<%=m_PreActives.Name %>"/>
                </td>
                <td align="left"></td>
            </tr>
            <tr>
                <td align="right" class="l-table-edit-td">审批结果:</td>
                <td align="left" class="l-table-edit-td">
                <select name="Result" id="Result" ltype="select">
                  <option value="1">接受</option>
                  <option value="2">拒绝</option>
                  </select>
                </td>
                <td align="left"></td>
            </tr>
            <tr>
                <td align="right" class="l-table-edit-td" valign="top">条件:</td>
                <td align="left" class="l-table-edit-td">
                <table>
                <tr><td>
                <textarea cols="100" rows="4" class="l-textarea" id="Condiction" style="width:400px" ></textarea>
                </td></tr>
                <tr><td>
                    <select name="cbAndOr" id="cbAndOr"  style="width:40px">
                      <option value=" and ">与</option>
                      <option value=" or ">或</option>
                      </select>
                    <select name="cbColumn" id="cbColumn"  style="width:80px">
                        <%                        
                            CTable Table = (CTable)Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.Find(m_WorkflowDef.FW_Table_id);
                        if (Table != null)
                        {
                            List<CBaseObject> lstObj = Table.ColumnMgr.GetList();
                            foreach (CBaseObject obj in lstObj)
                            {
                                CColumn Column = (CColumn)obj;
                                %>
                      <option value="<%=Column.Name%>"><%=Column.Name%></option>
                                <%
                            }
                        }
                         %>
                      </select>
                      <select name="cbSign" id="cbSign"  style="width:40px">
                      <option >=</option>
                      <option >></option>
                      <option >>=</option>
                      <option ><</option>
                      <option ><=</option>
                      <option ><></option>
                      <option >like</option>
                      </select>
                      <input name="txtVal" type="text" id="txtVal"  style="width:80px"/>
                      <input name="btAdd" id="btAdd" type="button" value="添加"  style="width:60px" onclick="return btAdd_onclick()" />
                </td></tr>
                </table>
                </td>
                <td align="left"></td>
            </tr>
            <tr>
                <td align="right" class="l-table-edit-td">后置活动:</td>
                <td align="left" class="l-table-edit-td">
                <select name="NextActives" id="NextActives" ltype="select">
                  <%                      
                      List<CBaseObject> lstAD = m_WorkflowDef.ActivesDefMgr.GetList();
                      foreach (CBaseObject obj in lstAD)
                      {
                          CActivesDef ActivesDef = (CActivesDef)obj;
                          if (ActivesDef == m_PreActives)
                              continue;
                          if (ActivesDef.WType == ActivesType.Start)
                              continue;
                          %>
                  <option value="<%=ActivesDef.Id%>"><%=ActivesDef.Name%></option>
                          <%
                      }
                       %>
                  </select>
                </td>
                <td align="left"></td>
            </tr>
            
        </table>
    </div>
    </form>
</body>
</html>
