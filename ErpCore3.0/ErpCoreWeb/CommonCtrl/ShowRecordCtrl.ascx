<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShowRecordCtrl.ascx.cs" Inherits="CommonCtrl_ShowRecordCtrl" %>
<%@ Import Namespace="ErpCoreModel" %>
<%@ Import Namespace="ErpCoreModel.Base" %>
<%@ Import Namespace="ErpCoreModel.Framework" %>
<%@ Import Namespace="ErpCoreModel.UI" %>
<%@ Import Namespace="System.Collections.Generic" %>
 
    

<table cellpadding="0" cellspacing="0" class="l-table-edit" >
<% 
    int iUICol = 0;
    //foreach (CColumnInView civ in m_View.ColumnInViewMgr.GetList())
    foreach(CBaseObject objCol in m_Table.ColumnMgr.GetList())
    {

        //CColumn col = (CColumn)m_Table.ColumnMgr.Find(civ.FW_Column_id);
        CColumn col = (CColumn)objCol;
        if (col == null)
            continue;
        //判断禁止权限字段
        bool bReadOnly = false;
        if (m_sortRestrictColumnAccessType.ContainsKey(col.Id))
        {
            AccessType accessType = m_sortRestrictColumnAccessType[col.Id];
            if (accessType == AccessType.forbide)
                continue;
            else if (accessType == AccessType.read)
                bReadOnly = true;
        }
        //
        //判断隐藏的字段
        bool bHideColumn = false;
        if (m_sortHideColumn.ContainsKey(col.Code))
            bHideColumn = true;

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
        
        //字段默认值
        CColumnDefaultValInView cdviv = m_View.ColumnDefaultValInViewMgr.FindByColumn(col.Id);
        if (cdviv!=null && cdviv.ReadOnly == true)
            bReadOnly = true;
        
        //界面控件列
        if (iUICol % m_iUIColCount == 0)
            Response.Write("<tr>");

        if (m_BaseObject == null)
        {
            if (bHideColumn)
            {
                continue;
            }
        %>
            <!--</tr>-->
                <td align="right" class="l-table-edit-td"><%=col.Name%>:</td>
                <td align="left" class="l-table-edit-td">
                <%if (col.ColType == ColumnType.string_type)
                  {
                    Response.Write(GetColumnDefaultVal(cdviv,col));
                    }
                  else if (col.ColType == ColumnType.text_type)
                  {
                    Response.Write(GetColumnDefaultVal(cdviv,col));
                  }
                  else if (col.ColType == ColumnType.int_type)
                  { 
                    Response.Write(GetColumnDefaultVal(cdviv,col));
                  }
                  else if (col.ColType == ColumnType.long_type)
                  { 
                    Response.Write(GetColumnDefaultVal(cdviv,col));
                  }
                  else if (col.ColType == ColumnType.bool_type)
                  {
                      string defval = GetColumnDefaultVal(cdviv, col).ToLower();
                      if (defval == "1" || defval == "true")
                          Response.Write("是");
                      else
                          Response.Write("否");
                  }
                  else if (col.ColType == ColumnType.numeric_type)
                  {
                    Response.Write(GetColumnDefaultVal(cdviv,col));
                  }
                  else if (col.ColType == ColumnType.guid_type)
                  { 
                    Response.Write(GetColumnDefaultVal(cdviv,col)) ;
                  }
                  else if (col.ColType == ColumnType.datetime_type)
                  { 
                      Response.Write(GetColumnDefaultVal(cdviv,col)) ;
                  }
                  else if (col.ColType == ColumnType.ref_type)
                  {
                      Response.Write( GetColumnRefDefaultVal(cdviv,col));                      
                  }
                  else if (col.ColType == ColumnType.enum_type)
                  {
                      Response.Write(GetColumnDefaultVal(cdviv,col));
                  }
                  else if (col.ColType == ColumnType.object_type)
                  {
                  }
                  else if (col.ColType == ColumnType.path_type)
                  {
                  } %>
                </td>
                <td align="left" style="width:50px"></td>
            <!--</tr>-->
        <%}
        else
        {
            if (bHideColumn)
            {
                continue;
            }
        %>
                    
            <!--</tr>-->
                <td align="right" class="l-table-edit-td"><%=col.Name %>:</td>
                <td align="left" class="l-table-edit-td">
                <%if (col.ColType == ColumnType.string_type)
                  { %>
                <%=m_BaseObject.GetColValue(col) %>
                <%}
                  else if (col.ColType == ColumnType.text_type)
                  {%>
                      <%=m_BaseObject.GetColValue(col) %>
                <%}
                  else if (col.ColType == ColumnType.int_type)
                  { %>
                  <%=m_BaseObject.GetColValue(col) %>
                  <%}
                  else if (col.ColType == ColumnType.long_type)
                  { %>
                  <%=m_BaseObject.GetColValue(col) %>
                  <%}
                  else if (col.ColType == ColumnType.bool_type)
                  { %>
                  <%if(Convert.ToBoolean(m_BaseObject.GetColValue(col)))
                        Response.Write("是");
                     else
                        Response.Write("否");
                   %>  
                  <%}
                  else if (col.ColType == ColumnType.numeric_type)
                  { %>
                  <%=m_BaseObject.GetColValue(col) %>
                  <%}
                  else if (col.ColType == ColumnType.guid_type)
                  { %>
                  <%=m_BaseObject.GetColValue(col) %>
                  <%}
                  else if (col.ColType == ColumnType.datetime_type)
                  {
                      DateTime dtime = (DateTime)m_BaseObject.GetColValue(col);
                      if (m_sortShowTimeColumn.ContainsKey(col.Code))
                          Response.Write(dtime.ToString());
                      else
                          Response.Write(dtime.ToShortDateString());
                  }
                  else if (col.ColType == ColumnType.ref_type)
                  {%>
                      <%=m_BaseObject.GetRefShowColVal(col) %>
                  <%
                  }
                  else if (col.ColType == ColumnType.enum_type)
                  {
                      %>
                    <%=m_BaseObject.GetColValue(col) %>
                    <%
                  }
                  else if (col.ColType == ColumnType.object_type)
                  {
                  }
                  else if (col.ColType == ColumnType.path_type)
                  {
                      object objVal = m_BaseObject.GetColValue(col);
                      if (objVal != null)
                      {
                          string sVal = objVal.ToString();
                          string[] arr = sVal.Split("|".ToCharArray());
                          string sUrl = "";
                          if (arr.Length ==2)
                          {
                              sUrl = arr[0];
                              string sPath = Server.MapPath(ConfigurationManager.AppSettings["VirtualDir"]);
                              sPath = col.UploadPath.ToLower().Replace(sPath.ToLower(), ConfigurationManager.AppSettings["VirtualDir"]);
                              sPath = sPath.Replace('\\', '/');
                              if(sPath.Length>0 && sPath[sPath.Length-1]!='/')
                                  sPath += '/';
                              
                              bool bIsImg = false;
                              int idx = sUrl.LastIndexOf('.');
                              if (idx > -1)
                              {
                                  string sExt = sUrl.Substring(idx);
                                  sExt = sExt.ToLower();
                                  if (sExt == ".jpg" || sExt == ".png" || sExt == ".bmp" || sExt == ".jpeg" || sExt == ".gif")
                                      bIsImg = true;
                              }
                              sUrl = sPath + sUrl;

                              string sContent = "";
                              if (bIsImg)
                              {
                                  sContent = string.Format("<img src='{0}' style='width:100px;height:120px;'>", sUrl);
                              }
                              else
                              {
                                  sContent = string.Format("<a href='{0}'>{1}</a>", sUrl, arr[1]);
                              }

                              Response.Write(sContent);
                          }
                      }
                  } %>
                </td>
                <td align="left" style="width:50px"></td>
            <!--</tr>-->
        <%}
        iUICol++;
    }
%> 
    
</table>
