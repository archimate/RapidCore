<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Desktop.aspx.cs" Inherits="Desktop_Desktop" %>
<%@ Import Namespace="ErpCoreModel" %>
<%@ Import Namespace="ErpCoreModel.Base" %>
<%@ Import Namespace="ErpCoreModel.Framework" %>
<%@ Import Namespace="ErpCoreModel.UI" %>
<%@ Import Namespace="ErpCoreModel.Report" %>
<%@ Import Namespace="System.Collections.Generic" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%=ConfigurationManager.AppSettings["ProductName"] %></title>
    <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" /> 
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerDrag.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerResizable.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerMenu.js" type="text/javascript"></script>
    <script src="../js/Hashtable.js" type="text/javascript"></script>

    <script type="text/javascript">
        function btModPwd_onclick() {
            var winModPwd = $.ligerDialog.open(
            { url: '../ModPwd.aspx', height: 300, width: 450, isResize: true, modal: false, title: '登录', slide: false
                //        , buttons: [
                //            { text: '确定', onclick: function (item, Dialog, index) {
                //                win.hide();
                //            }
                //            }
                //        ]
            });
        }
    </script>
    <style  type="text/css">
    <!--
    .trans_msg
        {
        filter:alpha(opacity=100,enabled=1) revealTrans(duration=.2,transition=1) blendtrans(duration=.2);
        }
    -->
    </style>
    <style type="text/css">
        .l-case-title
        {
            font-weight: bold;
            margin-top: 20px;
            margin-bottom: 20px;
        }
        body, html
        {
            width: 100%;
            height: 100%;
        }
        *
        {
            margin: 0;
            padding: 0;
        }
        #winlinks
        {
            position: absolute;
            left: 20px;
            top: 20px;
            width: 100%;
        }
        #winlinks ul
        {
            position: relative;
        }
        #winlinks li
        {
            width: 70px;
            cursor: pointer;
            height: 80px;
            position: absolute;
            z-index: 101;
            list-style: none;
            text-align: center;
        }
        #winlinks li img
        {
            width: 64px;
            height: 64px;
        }
        #winlinks li span
        {
            background: none repeat scroll 0 0 rgba(0, 0, 0, 0.3);
            border-radius: 10px 10px 10px 10px;
            display: block;
            font-size: 12px;
            margin-top: 1px;
            color: White;
            line-height: 18px;
            text-align: center;
        }
        #winlinks li.l-over div.bg
        {
            display: block;
        }
        #winlinks li div.bg
        {
            display: none;
            position: absolute;
            top: -2px;
            left: -2px;
            z-index: 0;
            width: 75px;
            height: 64px;
            -webkit-border-radius: 5px;
            -moz-border-radius: 5px;
            border-radius: 5px;
            background: #000;
            opacity: 0.1;
            filter: alpha(opacity=10);
        }
        #grouptitle li span
        {
            background: none repeat scroll 0 0 rgba(0, 0, 0, 0.3);
            border-radius: 10px 10px 10px 10px;
            display: block;
            font-size: 12px;
            margin-top: 1px;
            color: White;
            line-height: 18px;
            text-align: center;
        }
        .l-taskbar-task-icon
        {
            top: 3px; left: 6px; background-image:none;
        } 
        .l-taskbar-task-content{ margin-left:30px;}
        .l-taskbar-task-icon img
        {
            width: 22px;
            height: 22px;
        }
        .close
        {
            position:absolute;
            left:50px;
        } 
    </style>
</head>
<body style="overflow: hidden; background: url(<%=m_sBackImg %>) no-repeat  center center;">
    <%
      if (Session["User"] != null)
      {%>
    <div style="float: right; margin-right: 30px; margin-top: 15px;" >
        <input id="btModPwd" type="button" style="width:100px" value="修改密码" onclick="return btModPwd_onclick()" /></div>
    <%} %>
    <div id="winlinks">
        <ul>
        </ul>
    </div>
    <div id="grouptitle" style="position:absolute;bottom:75px;  width:250px; margin-left:45%">
    <ul><li><span id="spanTitle"><%=(m_CurGroup!=null)?m_CurGroup.Name:"主桌面" %></span></li></ul>
    </div>
    <script type="text/javascript">
        var CurGroup = '<%=(m_CurGroup!=null)?m_CurGroup.Name:"主桌面" %>';
    </script>
    <div style="position:absolute;bottom:50px;  width:250px; margin-left:45%">
    <input type="radio" id="rdDefault" name="rdDesktopGroup" <%if(m_guidCurGroupId==Guid.Empty){ %>checked<%} %> onclick="onClickDesktopGroup('<%=Guid.Empty %>')" 
     onMouseOver="$('#spanTitle').text('主桌面');" onMouseOut="$('#spanTitle').text(CurGroup);"/>&nbsp;
    <%
    if (Session["User"] != null)
    {
        List<CBaseObject> lstObjGp = m_DesktopGroupMgr.GetList();
        foreach (CBaseObject obj in lstObjGp)
        {
            CDesktopGroup group = (CDesktopGroup)obj;
      %>
    <input type="radio"  id="rd_<%=group.Id %>" name="rdDesktopGroup" <%if(m_guidCurGroupId==group.Id){ %>checked<%} %> onclick="onClickDesktopGroup('<%=group.Id %>')" 
     onMouseOver="$('#spanTitle').text('<%=group.Name %>');" onMouseOut="$('#spanTitle').text(CurGroup);"/>&nbsp;
    <%}
    } %>
    </div>

</body>
<script type="text/javascript">
    function onClickDesktopGroup(groupId) {
        window.location = "Desktop.aspx?GroupId=" + groupId;
    } 
</script>
<script type="text/javascript">
    var LINKWIDTH = 120, LINKHEIGHT = 120, TASKBARHEIGHT = 43;
    var winlinksul =  $("#winlinks ul");
    var win ;
    //var hashWin = new Hashtable(); //窗体哈希表
    function f_open(url, title, icon,mtype,width,height) {
        //如果窗体已经打开，则直接显示
//        if (hashWin.contains(url)) {
//            hashWin._hash[url].show();
//            return;
//        }
        ///////////
        //width==0 默认窗体大小, ==-1 最大化, ==-2新开窗口
        var w=width;
        var h=height;
        if(w<=0) w=1000;
        if(h<=0) h=500;
        
        if(width==-2){
            window.open(url);
            return null;
        }
        
        if(mtype=='AddDesktopApp')
        {
            win= $.ligerDialog.open(
            { name: 'winAddDesktopApp', url: url, height: h,width: w, showMax: true, showToggle: true,  isResize: true, modal: false, title: title, slide: false
                , buttons: [
                    { text: '确定', onclick: function (item, Dialog, index) {
                        var ret = document.getElementById('winAddDesktopApp').contentWindow.onSubmit();
                    }},
                    { text: '取消', onclick: function (item, Dialog, index) {
                        //Dialog.hide();
                        Dialog.close();
                    }}
                ]
            });
        }
        else
        {
            var bShowMin=true;
            if(mtype=='0')
                bShowMin=false;
            win= $.ligerDialog.open(
            {  url: url, height: h,width: w, showMax: true, showToggle: true, showMin: bShowMin, isResize: true, modal: false, title: title, slide: false
            });
            
            if(mtype=='AdminForm'){
                win.max();
            }
            if(width==-1){
                win.max();
            }
        }
        var task = jQuery.ligerui.win.tasks[win.id];
        if (task) {
            $(".l-taskbar-task-icon:first", task).html('<img src="' + icon + '" />');
        }
        //记录窗体到哈希表
        //hashWin.add(url,win);
        
        return win;
    }
    <%if(Session["User"]==null)
    { %>
    var links = [];
    <%}
    else
    { %>
    var links = [
        <%
        if(m_guidCurGroupId==Guid.Empty)//主桌面
        {
            if(((CUser)Session["User"]).IsRole("管理员"))
            { %>
            { icon: '../<%=Global.GetDesktopIconPathName() %>/MenuIcon/admin.png', title: '系统管理', url: '../AdminForm.aspx',mtype:'AdminForm',winw:'800',winh:'500' },
            <% 
            }%>
        
//            { icon: '../<%=Global.GetDesktopIconPathName() %>/MenuIcon/IM.png', title: '我的好友', url: '../IM/Friend.aspx',mtype:'IM',winw:'250',winh:'500' },
        <%
        }
        
        List<CBaseObject> lstMenu = GetOrderMenu();
        foreach(CBaseObject obj in lstMenu)
        {
            if(typeof(CUserMenu)==obj.GetType())
            {
                 CUserMenu UserMenu = (CUserMenu)obj;
               if(UserMenu.UI_DesktopGroup_id!=m_guidCurGroupId)
                   continue;
               CMenu menu = (CMenu)Global.GetCtx(Session["TopCompany"].ToString()).MenuMgr.Find(UserMenu.UI_Menu_id);
               if (menu == null)
                   continue;
                
               string sIconUrl = "default.png";
               if (menu.IconUrl != "")
                   sIconUrl = menu.IconUrl;
               string url=menu.Url;
               if(menu.MType==enumMenuType.CatalogMenu)
                   url="SelectMenu.aspx?pid="+menu.Id.ToString();
               else if(menu.MType==enumMenuType.ViewMenu)
               {
                   CView view = (CView)Global.GetCtx(Session["TopCompany"].ToString()).ViewMgr.Find(menu.UI_View_id);
                   if (view == null)
                       continue;
                   if(view.VType==enumViewType.Single)
                       url="../View/SingleView.aspx?vid="+view.Id.ToString();
                   else if(view.VType==enumViewType.MasterDetail)
                       url="../View/MasterDetailView.aspx?vid="+view.Id.ToString();
                   else 
                       url="../View/MultMasterDetailView.aspx?vid="+view.Id.ToString();
               }
               else if(menu.MType== enumMenuType.WindowMenu)
               {
               }
               else if(menu.MType== enumMenuType.ReportMenu)
               {
                    url="../Report/RunReport.aspx?id="+menu.RPT_Report_id.ToString();
               }
               string sItem = string.Format("{{ icon: '../{0}/MenuIcon/{1}', title: '{2}', url: '{3}',mtype:'{4}',winw:'{5}',winh:'{6}' }},",
                    Global.GetDesktopIconPathName(), sIconUrl, menu.Name, url,(int)menu.MType,menu.OpenwinWidth,menu.OpenwinHeight);
                Response.Write(sItem);
            }
            else if(typeof(CRoleMenu)==obj.GetType())
            {
                 CRoleMenu RoleMenu = (CRoleMenu)obj;
                if (RoleMenu.UI_DesktopGroup_id != m_guidCurGroupId)
                    continue;
                CMenu menu = (CMenu)Global.GetCtx(Session["TopCompany"].ToString()).MenuMgr.Find(RoleMenu.UI_Menu_id);
                if (menu == null)
                    continue;
                
               string sIconUrl = "default.png";
               if (menu.IconUrl != "")
                   sIconUrl = menu.IconUrl;
               string url=menu.Url;
               if(menu.MType==enumMenuType.CatalogMenu)
                   url="SelectMenu.aspx?pid="+menu.Id.ToString();
               else if(menu.MType==enumMenuType.ViewMenu)
               {
                   CView view = (CView)Global.GetCtx(Session["TopCompany"].ToString()).ViewMgr.Find(menu.UI_View_id);
                   if (view == null)
                       continue;
                   if(view.VType==enumViewType.Single)
                       url="../View/SingleView.aspx?vid="+view.Id.ToString();
                   else if(view.VType==enumViewType.MasterDetail)
                       url="../View/MasterDetailView.aspx?vid="+view.Id.ToString();
                   else 
                       url="../View/MultMasterDetailView.aspx?vid="+view.Id.ToString();
               }
               else if(menu.MType== enumMenuType.WindowMenu)
               {
               }
               else if(menu.MType== enumMenuType.ReportMenu)
               {
                    url="../Report/RunReport.aspx?id="+menu.RPT_Report_id.ToString();
               }
               string sItem = string.Format("{{ icon: '../{0}/MenuIcon/{1}', title: '{2}', url: '{3}',mtype:'{4}',winw:'{5}',winh:'{6}' }},",
                    Global.GetDesktopIconPathName(), sIconUrl, menu.Name, url,(int)menu.MType,menu.OpenwinWidth,menu.OpenwinHeight);
                Response.Write(sItem);
            }
            else if(typeof(CDesktopApp)==obj.GetType())
            {
               CDesktopApp App = (CDesktopApp)obj;
               
               string sIconUrl = "default.png";
               if (App.IconUrl != "")
                   sIconUrl = App.IconUrl;
               string sItem = string.Format("{{id:'{0}', icon: '../{1}/MenuIcon/{2}', title: '{3}', url: '{4}',mtype:'DesktopApp',winw:'{5}',winh:'{6}' }},",
                    App.Id,Global.GetDesktopIconPathName(), sIconUrl, App.Name, App.Url,App.OpenwinWidth,App.OpenwinHeight);
                Response.Write(sItem);
            }
        }
        %>
        { icon: '../<%=Global.GetDesktopIconPathName() %>/MenuIcon/order.png', title: '菜单排序', url: 'OrderMenu.aspx?GroupId=<%=Request["GroupId"] %>',mtype:'Order',winw:'600',winh:'400' },
        { icon: '../<%=Global.GetDesktopIconPathName() %>/MenuIcon/add.png', title: '添加应用', url: 'AddDesktopApp.aspx?GroupId=<%=Request["GroupId"] %>',mtype:'AddDesktopApp',winw:'350',winh:'300' }
        ];
    <%} %>
             
    function onResize() {
        var linksHeight = $(window).height() - TASKBARHEIGHT;
        var winlinks = $("#winlinks");
        winlinks.height(linksHeight);
        var colMaxNumber = parseInt(linksHeight / LINKHEIGHT);//一列最多显示几个快捷方式
        for (var i = 0, l = links.length; i < l; i++) {
            var link = links[i];
            var jlink = $("li[linkindex=" + i + "]", winlinks);
            var top = (i % colMaxNumber) * LINKHEIGHT, left = parseInt(i / colMaxNumber) * LINKWIDTH;
            if (isNaN(top) || isNaN(left)) continue;
            jlink.css({ top: top, left: left });
        }

    }
    function linksInit() {
        for (var i = 0, l = links.length; i < l; i++) {
            var link = links[i];
            var jlink;
            jlink = $("<li></li>");
            jlink.attr("linkindex", i);
            jlink.append("<img src='" + link.icon + "' />");
            jlink.append("<span>" + link.title + "</span>");
            jlink.append("<div id='linkbg"+i+"' class='bg'></div>");
            jlink.append("<img id='close"+i+"' style='display:none;width:16px;height:16px;' src='close.png' onclick='return onCloseApp("+i+")'/>");
            jlink.hover(function () {
                $(this).addClass("l-over");
                var linkindex = $(this).attr("linkindex");
                var link = links[linkindex];
                if(link.mtype=='DesktopApp')
                    document.getElementById('close'+$(this).attr("linkindex")).style.display='block';
            }, function () {
                $(this).removeClass("l-over");
                document.getElementById('close'+$(this).attr("linkindex")).style.display='none';
            }).click(function (e) {
                var linkindex = $(this).attr("linkindex");
                var link = links[linkindex];
                if(!bCloseApp)
                    f_open(link.url, link.title, link.icon,link.mtype,link.winw,link.winh);
                bCloseApp=false;
            });
            jlink.appendTo(winlinksul);
        }

    }
    var bCloseApp=false;
    function onCloseApp(i)
    {
        bCloseApp=true;
        var link = links[i];
        $.ligerDialog.confirm('确认删除？', function(yes) {
            if (yes) {
                $.post(
                    'Desktop.aspx',
                    {
                        Action: 'DelDesktopApp',
                        delid: link.id
                    },
                     function(data) {
                         if (data == "" || data == null) {
                             window.location.reload();
                             return true;
                         }
                         else {
                             $.ligerDialog.warn(data);
                             return false;
                         }
                     },
                    'text');
             }
           });
        return false;
    }

    $(window).resize(onResize);
    $.ligerui.win.removeTaskbar = function () { }; //不允许移除
    $.ligerui.win.createTaskbar(); //页面加载时创建任务栏

    linksInit();
    onResize();

    <%if (Session["User"] != null)
    {%>
    $(function ()
    {
        menu1 = $.ligerMenu({ top: 100, left: 100, width: 120, items:
        [
        { text: '背景', click: onMenuitemBg }
        ]
        });

        $("#winlinks").bind("contextmenu", function (e)
        {
            menu1.show({ top: e.pageY, left: e.pageX });
            return false;
        });

    });
    <%} %>
    function onMenuitemBg(item, i)
    {
        var win = $.ligerDialog.open(
            { url: 'SelectBg.aspx',height: 500,  width: 600,  isResize: true, modal: false, title: '选择背景', slide: false
                //        , buttons: [
                //            { text: '确定', onclick: function (item, Dialog, index) {
                //                win.hide();
                //            }
                //            }
                //        ]
            });
    }
    
    function onSelectBg(src, filename) {
        $.post(
            'Desktop.aspx',
            {
                Action: 'SetBackImg',
                BackImg: filename
            },
             function(data) {
                 if (data == "" || data == null) {
                     document.body.style.background='url('+src+') no-repeat  center center;';
                     return true;
                 }
                 else {
                     $.ligerDialog.warn(data);
                     return false;
                 }
             },
             'text');
    }
    
    
    //定时更新用户在线状态
    function UpdateOnlineState() {

        $.post(
            'Desktop.aspx',
            {
                Action: 'UpdateOnlineState'
            },
            function(data) {
                if (data == "" || data == null) {
                    return false;
                }
                else {
                    return true;
                }
            },
            'text'
        );

        window.setTimeout(UpdateOnlineState, 10000);
    }
    <%if (Session["User"] != null)
    {%>
    //每隔10秒更新一次
    window.setTimeout(UpdateOnlineState, 10000);
    <%} %>
    //登录
    <%if (Session["User"] == null)
    {%>
    var winLogin = $.ligerDialog.open(
            { url: '../Login.aspx',height: 250,  width: 450,  isResize: true, modal: true, title: '登录', slide: false
                //        , buttons: [
                //            { text: '确定', onclick: function (item, Dialog, index) {
                //                win.hide();
                //            }
                //            }
                //        ]
            });
    <%} %>
</script>
</html>
<!--修正ie6下png图不透明问题-->
<script src="../js/DD_belatedPNG_0.0.8a.js" type="text/javascript"></script>     
<script type="text/javascript">
    if ($.browser.msie && ($.browser.version == "6.0")) {
        DD_belatedPNG.fix('div, ul, img, li, input , a');
    }    
</script>