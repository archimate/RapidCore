<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WindowRecordCtrl.ascx.cs" Inherits="CommonCtrl_WindowRecordCtrl" %>
<%@ Import Namespace="ErpCoreModel" %>
<%@ Import Namespace="ErpCoreModel.Base" %>
<%@ Import Namespace="ErpCoreModel.Framework" %>
<%@ Import Namespace="ErpCoreModel.UI" %>
<%@ Import Namespace="System.Collections.Generic" %>
    
<script type="text/javascript">
    function showEditor(id) {
        var editorHtml = "#editorHtml" + id;
        var editorText = "#editorText" + id;
        var txt = "#txt_" + id;
        var hid = "#_" + id;

        var dialogRet = false;
        $(editorHtml).dialog({
            title: '编辑器',
            width: 700,
            modal: true,
            open: function (event, ui) {
                // 打开Dialog后创建编辑器
                KindEditor.create('textarea[name="txt_' + id + '"]', {
                    resizeType: 1,
                    cssPath: '../kindeditor/plugins/code/prettify.css',
                    uploadJson: '../kindeditor/asp.net/upload_json.ashx',
                    fileManagerJson: '../kindeditor/asp.net/file_manager_json.ashx',
                    allowFileManager: true
                });
            },
            buttons: {
                "确定": function () {
                    dialogRet = true;
                    $(this).dialog("close");
                },
                "取消": function () {
                    dialogRet = false;
                    $(this).dialog("close");
                }
            },
            beforeClose: function (event, ui) {
                // 关闭Dialog前移除编辑器
                KindEditor.remove('textarea[name="txt_' + id + '"]');
                if (dialogRet)
                    $(hid).val($(txt).val());
                else
                    $(txt).val($(hid).val());
            }
        });
    }
    function onEditorTextChange(id) {
        var txt = "#txt_" + id;
        var hid = "#_" + id;
        $(txt).val($(hid).val());
    }
</script>

<table cellpadding="0" cellspacing="0" class="l-table-edit" >
<% 
    int iUICol = 0;
    List<CBaseObject> lstCol = m_Table.ColumnMgr.GetList();
    foreach (CBaseObject obj in lstCol)
    {
        CColumn col = (CColumn)obj;
        //判断禁止权限字段
        //bool bReadOnly = false;
        //if (m_sortRestrictColumnAccessType.ContainsKey(col.Id))
        //{
        //    AccessType accessType = m_sortRestrictColumnAccessType[col.Id];
        //    if (accessType == AccessType.forbide)
        //        continue;
        //    else if (accessType == AccessType.read)
        //        bReadOnly = true;
        //}
        //
        bool bReadOnly = false;
        //下拉框弹出选择方式的字段
        bool bPopupSelDialog = false;
        if (m_sortPopupSelDialogColumn.ContainsKey(col.Code))
            bPopupSelDialog = true;

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

        //界面控件列
        if (iUICol % m_iUIColCount == 0)
            Response.Write("<tr>");
        iUICol++;
        
        if (m_BaseObject == null)
        {
        %>
            <!--<tr>-->
                <td align="right" class="l-table-edit-td"><%=col.Name%>:</td>
                <td align="left" class="l-table-edit-td">
                <%if (col.ColType == ColumnType.string_type)
                  { %>
                <input name="_<%=col.Code %>" type="text" id="_<%=col.Code %>" ltype="text"  <%if(bReadOnly){ %>readonly="readonly"<%} %>/>
                <%}
                  else if (col.ColType == ColumnType.text_type)
                  { %>
                  
                    <div id="editorText<%=col.Code %>">
                    <textarea id="_<%=col.Code %>" name="_<%=col.Code %>" cols="30" rows="3" ltype="textarea" style=" width:<%=m_iUICtrlWidth%>px" onchange="onEditorTextChange('<%=col.Code %>')"></textarea>
                    </div>
                    <div id="editorHtml<%=col.Code %>" style=" width:100%; height:100%; display:none">
                    <textarea id="txt_<%=col.Code %>" name="txt_<%=col.Code %>" cols="30" rows="3" ltype="textarea" style=" width:100%; height:100%;" ></textarea>
                    </div>
                    <div><input type="button" id="btEditor<%=col.Code %>" value="显示编辑器" onclick="showEditor('<%=col.Code %>')"/></div>
                <%}
                  else if (col.ColType == ColumnType.int_type)
                  { %>
                  <input name="_<%=col.Code %>" type="text" id="_<%=col.Code %>" ltype="text"  <%if(bReadOnly){ %>readonly="readonly"<%} %>/>
                  <%}
                  else if (col.ColType == ColumnType.long_type)
                  { %>
                  <input name="_<%=col.Code %>" type="text" id="_<%=col.Code %>" ltype="text"  <%if(bReadOnly){ %>readonly="readonly"<%} %>/>
                  <%}
                  else if (col.ColType == ColumnType.bool_type)
                  { %>
                  <input id="_<%=col.Code %>" type="checkbox" name="_<%=col.Code %>"  <%if(bReadOnly){ %>readonly="readonly"<%} %>/>
                  <%}
                  else if (col.ColType == ColumnType.numeric_type)
                  { %>
                  <input name="_<%=col.Code %>" type="text" id="_<%=col.Code %>" ltype="text"  <%if(bReadOnly){ %>readonly="readonly"<%} %>/>
                  <%}
                  else if (col.ColType == ColumnType.guid_type)
                  { %>
                  <input name="_<%=col.Code %>" type="text" id="_<%=col.Code %>" ltype="text"  <%if(bReadOnly){ %>readonly="readonly"<%} %>/>
                  <%}
                  else if (col.ColType == ColumnType.datetime_type)
                  { %>
                  <input name="_<%=col.Code %>" type="text" id="_<%=col.Code %>" ltype="date" validate="{required:true}"  <%if(bReadOnly){ %>readonly="readonly"<%} %>/>
                  <script type="text/javascript"> 
                  $(function() {
                  $("#_<%=col.Code %>").ligerDateEditor({ initValue: '', showTime: <%=GetShowTimel(col) %> });
                  });
                  </script>
                  <%}
                  else if (col.ColType == ColumnType.ref_type)
                  {
                      if (bReadOnly)
                      {%>
                    <input name="Ref_<%=col.Code %>" type="text" id="Ref_<%=col.Code %>" ltype="text"  readonly="readonly" />
                    <input name="_<%=col.Code %>" type="hidden" id="_<%=col.Code %>" />
                    <%}
                      else if (bPopupSelDialog)
                      {%>
                  <input name="Ref_<%=col.Code %>" type="text" id="Ref_<%=col.Code %>"  ltype="text"/>
                  <input name="_<%=col.Code %>" type="hidden" id="_<%=col.Code %>"  />
                    <%}
                      else
                      { %>
                  <select name="_<%=col.Code %>" id="_<%=col.Code %>" ltype="select">
                  <option value="">(空)</option>
                  <%CTable RefTable = (CTable)Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.Find(col.RefTable);
                    CBaseObjectMgr BaseObjectMgr = null;
                    if (m_sortRefBaseObjectMgr.ContainsKey(col.Code))
                        BaseObjectMgr = m_sortRefBaseObjectMgr[col.Code];
                    else
                        BaseObjectMgr = Global.GetCtx(Session["TopCompany"].ToString()).FindBaseObjectMgrCache(RefTable.Code, Guid.Empty);
                    if (BaseObjectMgr == null)
                    {
                        BaseObjectMgr = new CBaseObjectMgr();
                        BaseObjectMgr.TbCode = RefTable.Code;
                        BaseObjectMgr.Ctx = Global.GetCtx(Session["TopCompany"].ToString());
                    }

                    CColumn RefCol = (CColumn)RefTable.ColumnMgr.Find(col.RefCol);
                    CColumn RefShowCol = (CColumn)RefTable.ColumnMgr.Find(col.RefShowCol);
                    List<CBaseObject> lstObjRef = BaseObjectMgr.GetList();
                    foreach (CBaseObject objRef in lstObjRef)
                    { %>
	                <option value="<%=objRef.GetColValue(RefCol) %>"><%=objRef.GetColValue(RefShowCol)%></option>
	                <%} %>
                </select>
                  <%}
                  }
                  else if (col.ColType == ColumnType.enum_type)
                  {
                      if (bReadOnly)
                      {%>
                    <input name="_<%=col.Code %>" type="text" id="_<%=col.Code %>" ltype="text" value="<%=m_BaseObject.GetColValue(col) %>"
                        readonly="readonly" />
                    <%}
                      else
                      { %>
                  <select name="_<%=col.Code %>" id="_<%=col.Code %>" ltype="select">
                  <option value="">(空)</option>
                        <%//引用显示字段优先
                          if (col.RefShowCol != Guid.Empty)
                          {
                              CTable RefTable = (CTable)Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.Find(col.RefTable);
                              CBaseObjectMgr BaseObjectMgr = null;
                              if (m_sortRefBaseObjectMgr.ContainsKey(col.Code))
                                  BaseObjectMgr = m_sortRefBaseObjectMgr[col.Code];
                              else
                                  BaseObjectMgr = Global.GetCtx(Session["TopCompany"].ToString()).FindBaseObjectMgrCache(RefTable.Code, Guid.Empty);
                              if (BaseObjectMgr == null)
                              {
                                  BaseObjectMgr = new CBaseObjectMgr();
                                  BaseObjectMgr.TbCode = RefTable.Code;
                                  BaseObjectMgr.Ctx = Global.GetCtx(Session["TopCompany"].ToString());
                              }

                              CColumn RefShowCol = (CColumn)RefTable.ColumnMgr.Find(col.RefShowCol);
                              List<CBaseObject> lstObjRef = BaseObjectMgr.GetList();
                              foreach (CBaseObject objRef in lstObjRef)
                              { %>
	                     <option value="<%=objRef.GetColValue(RefShowCol) %>"><%=objRef.GetColValue(RefShowCol)%></option>
	                        <%}
                          }
                          else
                          {
                              List<CBaseObject> lstObjEV = col.ColumnEnumValMgr.GetList();
                              foreach (CBaseObject objEV in lstObjEV)
                              {
                                  CColumnEnumVal cev = (CColumnEnumVal)objEV;
                             %>
                          <option value="<%=cev.Val%>"><%=cev.Val%></option>
                            <%}
                          } %>
                </select>
                  <%}
                  }
                  else if (col.ColType == ColumnType.object_type || col.ColType == ColumnType.path_type)
                  { %>
                  <input  name="_<%=col.Code %>" id="_<%=col.Code %>" type="file"  <%if(bReadOnly){ %>readonly="readonly"<%} %>/>
                  <%} %>
                </td>
                <td align="left" style=" width:50px">
                  <%if (!col.AllowNull)
                  { %><span style="color:Red">*</span><%} %>
                  </td>
            <!--</tr>-->
        <%}
        else
        {%>                    
            <!--<tr>-->
                <td align="right" class="l-table-edit-td"><%=col.Name %>:</td>
                <td align="left" class="l-table-edit-td">
                <%if (col.ColType == ColumnType.string_type)
                  { %>
                <input name="_<%=col.Code %>" type="text" id="_<%=col.Code %>" ltype="text" value="<%=m_BaseObject.GetColValue(col) %>" <%if(bReadOnly){ %>readonly="readonly"<%} %>/>
                <%}
                  else if (col.ColType == ColumnType.text_type)
                  { 
                      //特殊字符处理
                      string val = m_BaseObject.GetColValue(col).ToString();
                      val = val.Replace("\"","&quot;");
                      val = val.Replace("'","&#039;");
                      %>
                      <div id="editorText<%=col.Code %>">
                    <textarea id="_<%=col.Code %>" name="_<%=col.Code %>" cols="30" rows="3" ltype="textarea" style=" width:<%=m_iUICtrlWidth%>px" onchange="onEditorTextChange('<%=col.Code %>')"><%=val%></textarea>
                    </div>
                    <div id="editorHtml<%=col.Code %>" style=" width:100%; height:100%;display:none">
                    <textarea id="txt_<%=col.Code %>" name="txt_<%=col.Code %>" cols="30" rows="3" ltype="textarea" style=" width:100%; height:100%;" ><%=val%></textarea>
                    </div>
                    <div><input type="button" id="btEditor<%=col.Code %>" value="显示编辑器" onclick="showEditor('<%=col.Code %>')"/></div>
                <%}
                  else if (col.ColType == ColumnType.int_type)
                  { %>
                  <input name="_<%=col.Code %>" type="text" id="_<%=col.Code %>" value="<%=m_BaseObject.GetColValue(col) %>" ltype="text"  <%if(bReadOnly){ %>readonly="readonly"<%} %>/>
                  <%}
                  else if (col.ColType == ColumnType.long_type)
                  { %>
                  <input name="_<%=col.Code %>" type="text" id="_<%=col.Code %>" value="<%=m_BaseObject.GetColValue(col) %>" ltype="text"  <%if(bReadOnly){ %>readonly="readonly"<%} %>/>
                  <%}
                  else if (col.ColType == ColumnType.bool_type)
                  { %>
                  <input id="_<%=col.Code %>" type="checkbox" name="_<%=col.Code %>" <%if(Convert.ToBoolean(m_BaseObject.GetColValue(col))){ %>checked<%} %>  <%if(bReadOnly){ %>readonly="readonly"<%} %>/>
                  <%}
                  else if (col.ColType == ColumnType.numeric_type)
                  { %>
                  <input name="_<%=col.Code %>" type="text" id="_<%=col.Code %>" value="<%=m_BaseObject.GetColValue(col) %>" ltype="text"  <%if(bReadOnly){ %>readonly="readonly"<%} %>/>
                  <%}
                  else if (col.ColType == ColumnType.guid_type)
                  { %>
                  <input name="_<%=col.Code %>" type="text" id="_<%=col.Code %>" value="<%=m_BaseObject.GetColValue(col) %>" ltype="text"  <%if(bReadOnly){ %>readonly="readonly"<%} %>/>
                  <%}
                  else if (col.ColType == ColumnType.datetime_type)
                  { %>
                  <input name="_<%=col.Code %>" type="text" id="_<%=col.Code %>"  ltype="date" validate="{required:true}"  <%if(bReadOnly){ %>readonly="readonly"<%} %>/>
                  <script type="text/javascript"> 
                  $(function() {
                  $("#_<%=col.Code %>").ligerDateEditor({ initValue: '<%=m_BaseObject.GetColValue(col) %>', showTime: <%=GetShowTimel(col) %> });
                  });
                  </script>
                  <%}
                  else if (col.ColType == ColumnType.ref_type)
                  {
                      if (bReadOnly)
                      {%>
                    <input name="Ref_<%=col.Code %>" type="text" id="Ref_<%=col.Code %>" ltype="text" value="<%=m_BaseObject.GetRefShowColVal(col) %>"  readonly="readonly" />
                    <input name="_<%=col.Code %>" type="hidden" id="_<%=col.Code %>"  value="<%=m_BaseObject.GetColValue(col) %>"  />
                    <%}
                      else if (bPopupSelDialog)
                      {%>
                  <input name="Ref_<%=col.Code %>" type="text" id="Ref_<%=col.Code %>" value="<%=m_BaseObject.GetRefShowColVal(col) %>" ltype="text"/>
                  <input name="_<%=col.Code %>" type="hidden" id="_<%=col.Code %>" value="<%=m_BaseObject.GetColValue(col) %>" />
                    <%}
                      else
                      { %>
                  <select name="_<%=col.Code %>" id="_<%=col.Code %>" ltype="select" >
                  <option value="">(空)</option>
                      <%CTable RefTable = (CTable)Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.Find(col.RefTable);
                        CBaseObjectMgr BaseObjectMgr = null;
                        if (m_sortRefBaseObjectMgr.ContainsKey(col.Code))
                            BaseObjectMgr = m_sortRefBaseObjectMgr[col.Code];
                        else
                            BaseObjectMgr = Global.GetCtx(Session["TopCompany"].ToString()).FindBaseObjectMgrCache(RefTable.Code, Guid.Empty);
                        if (BaseObjectMgr == null)
                        {
                            BaseObjectMgr = new CBaseObjectMgr();
                            BaseObjectMgr.TbCode = RefTable.Code;
                            BaseObjectMgr.Ctx = Global.GetCtx(Session["TopCompany"].ToString());
                        }

                        CColumn RefCol = (CColumn)RefTable.ColumnMgr.Find(col.RefCol);
                        CColumn RefShowCol = (CColumn)RefTable.ColumnMgr.Find(col.RefShowCol);
                        List<CBaseObject> lstObjRef = BaseObjectMgr.GetList();
                        foreach (CBaseObject objRef in lstObjRef)
                        {
                            bool bIsCurrent = m_BaseObject.GetColValue(col).ToString().Equals(objRef.GetColValue(RefCol).ToString(), StringComparison.OrdinalIgnoreCase);
                            %>
	                    <option value="<%=objRef.GetColValue(RefCol) %>" <%if(bIsCurrent){ %>selected<%} %>><%=objRef.GetColValue(RefShowCol)%></option>
	                    <%} %>
                </select>
                  <%}
                  }
                  else if (col.ColType == ColumnType.enum_type)
                  {
                      if (bReadOnly)
                      {%>
                    <input name="_<%=col.Code %>" type="text" id="_<%=col.Code %>" ltype="text" value="<%=m_BaseObject.GetColValue(col) %>"
                        readonly="readonly" />
                    <%}
                      else
                      { %>
                  <select name="_<%=col.Code %>" id="_<%=col.Code %>" ltype="select" >
                  <option value="">(空)</option>
                        <%//引用显示字段优先
                          if (col.RefShowCol != Guid.Empty)
                          {
                              CTable RefTable = (CTable)Global.GetCtx(Session["TopCompany"].ToString()).TableMgr.Find(col.RefTable);
                              CBaseObjectMgr BaseObjectMgr = null;
                              if (m_sortRefBaseObjectMgr.ContainsKey(col.Code))
                                  BaseObjectMgr = m_sortRefBaseObjectMgr[col.Code];
                              else
                                  BaseObjectMgr = Global.GetCtx(Session["TopCompany"].ToString()).FindBaseObjectMgrCache(RefTable.Code, Guid.Empty);
                              if (BaseObjectMgr == null)
                              {
                                  BaseObjectMgr = new CBaseObjectMgr();
                                  BaseObjectMgr.TbCode = RefTable.Code;
                                  BaseObjectMgr.Ctx = Global.GetCtx(Session["TopCompany"].ToString());
                              }

                              CColumn RefShowCol = (CColumn)RefTable.ColumnMgr.Find(col.RefShowCol);
                              List<CBaseObject> lstObjRef = BaseObjectMgr.GetList();
                              foreach (CBaseObject objRef in lstObjRef)
                              {
                                  bool bIsCurrent = m_BaseObject.GetColValue(col).ToString().Equals(objRef.GetColValue(RefShowCol).ToString(), StringComparison.OrdinalIgnoreCase);
                                  %>
	                            <option value="<%=objRef.GetColValue(RefShowCol) %>" <%if(bIsCurrent){ %>selected<%} %>><%=objRef.GetColValue(RefShowCol)%></option>
	                        <%}
                          }
                          else
                          {
                              List<CBaseObject> lstObjEV = col.ColumnEnumValMgr.GetList();
                              foreach (CBaseObject objEV in lstObjEV)
                              {
                                  CColumnEnumVal cev = (CColumnEnumVal)objEV;
                             %>
                             <option value="<%=cev.Val%>" <%if(m_BaseObject.GetColValue(col).ToString()==cev.Val){ %>selected<%} %>><%=cev.Val%></option>
                            <%}
                          } %>
                </select>
                  <%}
                  }
                  else if (col.ColType == ColumnType.object_type || col.ColType == ColumnType.path_type)
                  { %>
                  <input  name="_<%=col.Code %>" id="_<%=col.Code %>" type="file"  <%if(bReadOnly){ %>readonly="readonly"<%} %>/>
                  <input type="checkbox" id="ckClear_<%=col.Code %>" name="ckClear_<%=col.Code %>" />清空
                  <%} %>
                </td>
                <td align="left" style=" width:50px">
                  <%if (!col.AllowNull)
                  { %><span style="color:Red">*</span><%} %>
                  </td>
            <!--</tr>-->
        <%}
    }
%> 
    
</table>
