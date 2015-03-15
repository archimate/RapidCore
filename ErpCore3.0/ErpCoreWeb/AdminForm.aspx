<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminForm.aspx.cs" Inherits="AdminForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>ErpCore 导航主页</title>
    <link href="lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" /> 
    <script src="lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>    
    <script src="lib/ligerUI/js/ligerui.min.js" type="text/javascript"></script> 
        <script type="text/javascript">
            var tab = null;
            var accordion = null;
            var tree = null;
            var actionNode = null;
            $(function () {

                //布局
                $("#layout1").ligerLayout({ leftWidth: 190, height: '100%', heightDiff: -34, space: 4, onHeightChanged: f_heightChanged });

                var height = $(".l-layout-center").height();

                //Tab
                $("#framecenter").ligerTab({ height: height });

                //面板
                $("#accordion1").ligerAccordion({ height: height - 24, speed: null });

                $(".l-link").hover(function () {
                    $(this).addClass("l-link-over");
                }, function () {
                    $(this).removeClass("l-link-over");
                });

                //树菜单
                var menuCatalog;
                menuCatalog = $.ligerMenu({ top: 100, left: 100, width: 120, items:
                    [
                    { text: '新建', click: onAddCatalog, icon: 'add' },
                    { text: '修改', click: onEditCatalog, icon: 'modify' },
                    { text: '删除', click: onDeleteCatalog, icon: 'delete' }
                    ]
                });
                function onAddCatalog(item, i) {
                    if (actionNode == null) return;
                    var sUrl = "";
                    if (actionNode.data.name == "nodeWorkflowCatalog")
                        sUrl = 'Workflow/AddCatalog.aspx?B_Company_id=' + actionNode.data.B_Company_id;
                    else if (actionNode.data.name == "nodeReportCatalog")
                        sUrl = 'Report/AddCatalog.aspx?B_Company_id=' + actionNode.data.B_Company_id;
                    else if (actionNode.data.name == "nodeViewRoot")
                        sUrl = 'View/AddSingleViewRecord.aspx?vid={B52A150C-DD32-427E-B1CC-4583226BA583}&ParentId=&UIColCount=1';
                    else if (actionNode.data.name == "nodeDesktopGroupRoot")
                        sUrl = 'View/AddSingleViewRecord.aspx?vid={43024A43-8148-40C5-AF3A-B5A81A001DBF}&ParentId=&UIColCount=1';

                    $.ligerDialog.open({ title: '新建', url: sUrl, name: 'winAddRec', height: 200, width: 400, modal: false,
                        buttons: [
                            { text: '确定', onclick: function (item, dialog) {
                                var ret = document.getElementById('winAddRec').contentWindow.onSubmit();
                            }
                            },
                            { text: '取消', onclick: function (item, dialog) { dialog.close(); } }
                         ], isResize: true
                    });
                }
                function onEditCatalog(item, i) {
                    if (actionNode == null) return;
                    if (actionNode.data.id == null || actionNode.data.id == "") {
                        alert("请选择目录！");
                        return;
                    }
                    var sUrl = "";
                    if (actionNode.data.name == "nodeWorkflowCatalog")
                        sUrl = 'Workflow/EditCatalog.aspx?id=' + actionNode.data.id + '&B_Company_id=' + actionNode.data.B_Company_id;
                    else if (actionNode.data.name == "nodeReportCatalog")
                        sUrl = 'Report/EditCatalog.aspx?id=' + actionNode.data.id + '&B_Company_id=' + actionNode.data.B_Company_id;
                    else if (actionNode.data.name == "nodeViewCatalog")
                        sUrl = 'View/EditSingleViewRecord.aspx?vid={B52A150C-DD32-427E-B1CC-4583226BA583}&ParentId=&UIColCount=1&id=' + actionNode.data.id;
                    else if (actionNode.data.name == "nodeDesktopGroup")
                        sUrl = 'View/EditSingleViewRecord.aspx?vid={43024A43-8148-40C5-AF3A-B5A81A001DBF}&ParentId=&UIColCount=1&id=' + actionNode.data.id;

                    $.ligerDialog.open({ title: '新建', url: sUrl, name: 'winEditRec', height: 200, width: 400, modal: false,
                        buttons: [
                            { text: '确定', onclick: function (item, dialog) {
                                var ret = document.getElementById('winEditRec').contentWindow.onSubmit();
                            }
                            },
                            { text: '取消', onclick: function (item, dialog) { dialog.close(); } }
                         ], isResize: true
                    });
                }

                function onDeleteCatalog(item, i) {
                    if (actionNode == null) return;
                    if (actionNode.data.id == null || actionNode.data.id == "") {
                        alert("请选择目录！");
                        return;
                    }
                    var action = "";
                    if (actionNode.data.name == "nodeWorkflowCatalog")
                        action = 'DelWorkflowCatalog';
                    else if (actionNode.data.name == "nodeReportCatalog")
                        action = 'DelReportCatalog';
                    else if (actionNode.data.name == "nodeViewCatalog")
                        action = 'DelViewCatalog';
                    else if (actionNode.data.name == "nodeDesktopGroup")
                        action = 'DelDesktopGroup';

                    $.ligerDialog.confirm('确认删除' + actionNode.data.text + '？', function (yes) {
                        if (yes) {
                            $.post(
                            'AdminForm.aspx',
                            {
                                Action: action,
                                delid: actionNode.data.id,
                                B_Company_id: actionNode.data.B_Company_id
                            },
                             function (data) {
                                 if (data == "" || data == null) {
                                     tree.remove(actionNode.target);
                                     return true;
                                 }
                                 else {
                                     $.ligerDialog.warn(data);
                                     return false;
                                 }
                             },
                            'text'
                            );
                        }
                    });
                }


                //树节点
                var treedata =
                [
                    { isexpand: "false", text: "数据库", name: "nodeDatabase", children: [
	                    { url: "Database/Table/TablePanel.aspx", text: "表" }
                    ]
                    },
                    { isexpand: "false", text: "工作流", name: "nodeWorkflowRoot",
                        children: []
                    },
                    { isexpand: "false", text: "报表", name: "nodeReportRoot",
                        children: []
                    },
                    { isexpand: "false", text: "菜单", name: "nodeMenuRoot",
                        url: 'Menu/MenuPanel.aspx',
                        children: []
                    },
                    { isexpand: "false", text: "视图", name: "nodeViewRoot",
                        url: 'View/ViewPanel.aspx',
                        children: []
                    },
                    { isexpand: "false", text: "桌面组", name: "nodeDesktopGroupRoot",
                        url: '',
                        children: []
                    },
                    { isexpand: "false", text: "安全性", name: "nodeSecurityRoot",
                        children: []
                    }
                ];
                //树
                $("#tree1").ligerTree({
                    data: treedata,
                    checkbox: false,
                    slide: false,
                    nodeWidth: 120,
                    attribute: ['nodename', 'url'],
                    onSelect: function (node) {
                        if (!node.data.url) return;
                        var tabid = $(node.target).attr("tabid");
                        if (!tabid) {
                            tabid = new Date().getTime();
                            $(node.target).attr("tabid", tabid)
                        }
                        f_addTab(tabid, node.data.text, node.data.url);
                    },
                    onBeforeExpand: function (node) {
                        if (node.data.children && node.data.children.length == 0) {
                            if (node.data.name == "nodeReportRoot") {
                                tree.loadData(node.target, 'AdminForm.aspx',
                                {
                                    Action: 'GetReportCompany'
                                }
                                );
                            }
                            else if (node.data.name == "nodeReportCompany") {
                                tree.loadData(node.target, 'AdminForm.aspx',
                                {
                                    Action: 'GetReportCatalog',
                                    B_Company_id: node.data.id
                                }
                                );
                            }
                            else if (node.data.name == "nodeReportCatalog") {
                                tree.loadData(node.target, 'AdminForm.aspx',
                                {
                                    Action: 'GetReportCatalog',
                                    pid: node.data.id,
                                    B_Company_id: node.data.B_Company_id
                                }
                                );
                            }
                            else if (node.data.name == "nodeWorkflowRoot") {
                                tree.loadData(node.target, 'AdminForm.aspx',
                                {
                                    Action: 'GetWorkflowCompany'
                                }
                                );
                            }
                            else if (node.data.name == "nodeWorkflowCompany") {
                                tree.loadData(node.target, 'AdminForm.aspx',
                                {
                                    Action: 'GetWorkflowCatalog',
                                    B_Company_id: node.data.id
                                }
                                );
                            }
                            else if (node.data.name == "nodeWorkflowCatalog") {
                                tree.loadData(node.target, 'AdminForm.aspx',
                                {
                                    Action: 'GetWorkflowCatalog',
                                    pid: node.data.id,
                                    B_Company_id: node.data.B_Company_id
                                }
                                );
                            }
                            else if (node.data.name == "nodeViewRoot") {
                                tree.loadData(node.target, 'AdminForm.aspx',
                                {
                                    Action: 'GetViewCatalog'
                                }
                                );
                            }
                            else if (node.data.name == "nodeViewCatalog") {
                                tree.loadData(node.target, 'AdminForm.aspx',
                                {
                                    Action: 'GetViewCatalog',
                                    pid: node.data.id
                                }
                                );
                            }
                            else if (node.data.name == "nodeDesktopGroupRoot") {
                                tree.loadData(node.target, 'AdminForm.aspx',
                                {
                                    Action: 'GetDesktopGroup',
                                    pid: node.data.id
                                }
                                );
                            }
                            else if (node.data.name == "nodeSecurityRoot") {
                                tree.loadData(node.target, 'AdminForm.aspx',
                                {
                                    Action: 'GetSecurityCompany'
                                }
                                );
                            }
                            else if (node.data.name == "nodeSecurityCompany") {
                                tree.loadData(node.target, 'AdminForm.aspx',
                                {
                                    Action: 'GetSecurityCatalog',
                                    B_Company_id: node.data.id
                                }
                                );
                            }
                            else if (node.data.name == "nodeSecurityAccess") {
                                tree.loadData(node.target, 'AdminForm.aspx',
                                {
                                    Action: 'GetSecurityAccess',
                                    B_Company_id: node.data.B_Company_id
                                }
                                );
                            }
                        }
                    },
                    onContextmenu: function (node, e) {
                        actionNode = node;
                        if (node.data.name == "nodeWorkflowCatalog"
                        || node.data.name == "nodeReportCatalog"
                        || node.data.name == "nodeViewRoot"
                        || node.data.name == "nodeViewCatalog"
                        || node.data.name == "nodeDesktopGroupRoot"
                        || node.data.name == "nodeDesktopGroup")
                            menuCatalog.show({ top: e.pageY, left: e.pageX });
                        return false;
                    }
                });

                tab = $("#framecenter").ligerGetTabManager();
                accordion = $("#accordion1").ligerGetAccordionManager();
                tree = $("#tree1").ligerGetTreeManager();
                $("#pageloading").hide();

            });
            function f_heightChanged(options)
            {
                if (tab)
                    tab.addHeight(options.diff);
                if (accordion && options.middleHeight - 24 > 0)
                    accordion.setHeight(options.middleHeight - 24);
            }
            function f_addTab(tabid,text, url)
            { 
                tab.addTabItem({ tabid : tabid,text: text, url: url });
            }

            function reloadActionNodeChildren() {
                if (actionNode == null) return;
                reloadNodeChildren(actionNode);
            }
            function reloadNodeChildren(node) {
                if (node == null) return;
                var len = node.data.children.length;
                for (var i = 0; i < len; i++)
                    tree.remove(node.data.children[i]);

                var action = "";
                if (node.data.name == "nodeWorkflowCatalog")
                    action = 'GetWorkflowCatalog';
                else if (node.data.name == "nodeReportCatalog")
                    action = 'GetReportCatalog';
                else if (node.data.name == "nodeViewRoot")
                    action = 'GetViewCatalog';
                else if (node.data.name == "nodeDesktopGroupRoot")
                    action = 'GetDesktopGroup';

                var pid = '';
                if (node.data && node.data.id)
                    pid = node.data.id;
                var B_Company_id = '';
                if (node.data && node.data.B_Company_id)
                    B_Company_id = node.data.B_Company_id;
                tree.loadData(node.target, 'AdminForm.aspx',
                {
                    Action: action,
                    pid: pid,
                    B_Company_id: B_Company_id
                }
                );
            }
            function updateActionNode() {
                if (actionNode == null) return;
                updateNode(actionNode);
            }
            function updateNode(node) {
                if (node == null) return;
                var action = "";
                if (node.data.name == "nodeWorkflowCatalog")
                    action = 'GetWorkflowCatalogName';
                else if (node.data.name == "nodeReportCatalog")
                    action = 'GetReportCatalogName';
                else if (node.data.name == "nodeViewCatalog")
                    action = 'GetViewCatalogName';
                else if (node.data.name == "nodeDesktopGroup")
                    action = 'GetDesktopGroupName';

                $.get("AdminForm.aspx",
                {
                    Action: action,
                    id: node.data.id,
                    B_Company_id: node.data.B_Company_id
                },
                function (result) {
                    tree.update(node.target, { text: result });
                });
            }


            function onOkAddSingleViewRecord() {
                if (actionNode == null) return;
                if (actionNode.data.name == "nodeViewRoot"
                        || actionNode.data.name == "nodeDesktopGroupRoot") {
                    reloadNodeChildren(actionNode);
                    actionNode = null;
                }
                $.ligerDialog.close();
            }
            function onCancelAddSingleViewRecord() {

                $.ligerDialog.close();
            }
            function onOkEditSingleViewRecord() {
                if (actionNode == null) return;
                updateNode(actionNode);
                $.ligerDialog.close();
            }
            function onCancelEditSingleViewRecord() {

                $.ligerDialog.close();
            }

     </script> 
<style type="text/css"> 
    body,html{height:100%;}
    body{ padding:0px; margin:0;   overflow:hidden;}  
    .l-link{ display:block; height:26px; line-height:26px; padding-left:10px; text-decoration:underline; color:#333;}
    .l-link2{text-decoration:underline; color:white; margin-left:2px;margin-right:2px;}
    .l-layout-top{background:#102A49; color:White;}
    .l-layout-bottom{ background:#E5EDEF; text-align:center;}
    #pageloading{position:absolute; left:0px; top:0px; background:white url('loading.gif') no-repeat center; width:100%; height:100%;z-index:99999;}
    .l-link{ display:block; line-height:22px; height:22px; padding-left:16px;border:1px solid white; margin:4px;}
    .l-link-over{ background:#FFEEAC; border:1px solid #DB9F00;} 
    .l-winbar{ background:#2B5A76; height:30px; position:absolute; left:0px; bottom:0px; width:100%; z-index:99999;}
    .space{ color:#E7E7E7;}
    /* 顶部 */ 
    .l-topmenu{ margin:0; padding:0; height:31px; line-height:31px; background:url('lib/images/top.jpg') repeat-x bottom;  position:relative; border-top:1px solid #1D438B;  }
    .l-topmenu-logo{ color:#E7E7E7; padding-left:35px; line-height:26px;background:url('lib/images/topicon.gif') no-repeat 10px 5px;}
    .l-topmenu-welcome{  position:absolute; height:24px; line-height:24px;  right:30px; top:2px;color:#070A0C;}
    .l-topmenu-welcome a{ color:#E7E7E7; text-decoration:underline} 

 </style>
</head>
<body style="padding:0px;background:#EAEEF5;">  
<div id="pageloading"></div> 
<!-- 
<div id="topmenu" class="l-topmenu">
    <div class="l-topmenu-logo">ErpCore 导航主页</div>
</div>
-->
  <div id="layout1" style="width:99.2%; margin:0 auto; margin-top:4px; "> 
        <div position="left"  title="主要菜单" id="accordion1"> 
             <div title="功能列表" class="l-scroll">
                 <ul id="tree1" style="margin-top:3px;">
            </div>
        </div>
        <div position="center" id="framecenter"> 
            <div tabid="home" title="我的主页" style="height:300px" >
                <iframe frameborder="0" name="home" id="home" src="welcome.htm"></iframe>
            </div> 
        </div> 
        
    </div>
    
    <div  style="height:32px; line-height:32px; text-align:center;">
            Copyright © 2011-2020 北京君友世纪 www.8088net.com
    </div>
    <div style="display:none"></div>
    
</body>
</html>
