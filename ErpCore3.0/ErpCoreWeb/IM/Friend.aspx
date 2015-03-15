<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Friend.aspx.cs" Inherits="IM_Friend" %>
<%@ Import Namespace="ErpCoreModel" %>
<%@ Import Namespace="ErpCoreModel.Base" %>
<%@ Import Namespace="ErpCoreModel.Framework" %>
<%@ Import Namespace="ErpCoreModel.UI" %>
<%@ Import Namespace="ErpCoreModel.IM" %>
<%@ Import Namespace="System.Collections.Generic" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <link href="../lib/ligerUI/skins/ligerui-icons.css" rel="stylesheet" type="text/css" />
    <script src="../lib/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/core/base.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerGrid.js" type="text/javascript"></script> 
    <script src="../lib/ligerUI/js/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerMenu.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerMenuBar.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/plugins/ligerToolBar.js" type="text/javascript"></script>
    <script src="../js/Hashtable.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function() {
            $("#toptoolbar").ligerToolBar({ items: [
                { text: '增加', click: onAdd, icon: 'add' },
                { line: true },
                { text: '删除', click: onDelete, icon: 'delete' },
                { line: true },
                { text: '发消息', click: onMsg }
            ]
            });
        });

        function onAdd() {
            parent.$.ligerDialog.open({ title: '新建', url: '../IM/AddFriend.aspx', name: 'winAddRec', height: 500, width: 500, showMax: true, showToggle: true, showMin: true, isResize: true, modal: false, slide: false,
                buttons: [
                { text: '确定', onclick: function(item, dialog) {
                    var fn = dialog.frame.onSelect || dialog.frame.window.onSelect;
                    var data = fn();
                    if (!data) {
                        $.ligerDialog.warn(data);
                        return false;
                    }
                    //alert(data.id);
                    $.post(
                    'Friend.aspx',
                    {
                        Action: 'Add',
                        fid: data.id
                    },
                     function(data) {
                         if (data == "" || data == null) {
                             dialog.close();
                             grid.loadData(true);
                             return true;
                         }
                         else {
                             parent.$.ligerDialog.warn(data);
                             return false;
                         }
                     },
                    'text');
                }
                },
                { text: '取消', onclick: function(item, dialog) {
                    dialog.close();
                }
                }
             ], isResize: true
            });
        }
        function onDelete() {
            var row = grid.getSelectedRow();
            if (row == null) {
                $.ligerDialog.alert('请选择好友!');
                return;
            }
            var dlgconfirm = parent.$.ligerDialog.confirm('确认删除？', function(yes) {
                if (yes) {
                    $.post(
                    'Friend.aspx',
                    {
                        Action: 'Delete',
                        delid: row.id
                    },
                     function(data) {
                         if (data == "" || data == null) {
                             dlgconfirm.close();
                             grid.loadData(true);
                             return true;
                         }
                         else {
                             parent.$.ligerDialog.warn(data);
                             return false;
                         }
                     },
                    'text');
                }
            });
        }
        function onMsg() {
            var row = grid.getSelectedRow();
            if (row == null) {
                parent.$.ligerDialog.alert('请选择好友!');
                return;
            }
            ShowMsgDlg(row.Friend_id);
        }
        
    </script>
    <style type="text/css">
    #menu1,.l-menu-shadow{top:30px; left:50px;}
    #menu1{  width:200px;}
    </style>
    
    <script type="text/javascript">
        var grid;
        $(function ()
        {
            grid = $("#gridTable").ligerGrid({
            columns: [
                { display: '', name: 'Friend',align: 'left' }
                ],
                url: 'Friend.aspx?Action=GetData',
                dataAction: 'server',
                usePager: false,
                width: '100%', height: '100%',
                groupColumnName: 'group', groupColumnDisplay: '',
                headerRowHeight:'0',
                onSelectRow: function (data, rowindex, rowobj)
                {
                    //$.ligerDialog.alert('1选择的是' + data.id);
                },
                onDblClickRow: onDblClickRow ,
                onLoaded: onLoaded
            });
        });

        var hashMsgDlg = new Hashtable(); //消息对话框哈希表
        function onDblClickRow(data, rowindex, rowobj) {
            //$.ligerDialog.alert('选择的是' + data.CustomerID);
            ShowMsgDlg(data.Friend_id);
        }
        function ShowMsgDlg(Friend_id) {
            var dlgname = 'dlg' + Friend_id;
            if (hashMsgDlg.contains(dlgname)) {
                hashMsgDlg._hash[dlgname].show();
                return;
            }

            var dlg = parent.$.ligerDialog.open({ title: '消息', url: '../IM/SendMsg.aspx?to_id=' + Friend_id, name: dlgname, height: 500, width: 500, showMax: true, showToggle: true, showMin: true, isResize: true, modal: false, slide: false
            });
            hashMsgDlg.add(dlgname, dlg);
        }
        
        //消息到达声音只响一次
        var m_bIsPlaySound = false;
        function playSound() {
            if (m_bIsPlaySound)
                return;
            m_bIsPlaySound = true;
            document.getElementById("WindowsMediaPlayer1").controls.play();
        }


        //表格装载完数据
        var m_bIsLoaded = false;
        function onLoaded() {
            m_bIsLoaded = true;
            grid.toggleLoading(false);
            return true;
        }
        //定时获取好友状态及消息数目
        function UpdateState() {
            //等待表格第一次数据装载完成再开始
            if (!m_bIsLoaded)
                return;
            $.getJSON(
                'Friend.aspx',
                {
                    Action: 'GetFriendState'
                },
                function(data) {
                    var arrNewRow = new Array();
                    var rowData = grid.getData();
                    for (var idx = 0; idx < data.Rows.length; idx++) {
                        var id = data.Rows[idx].id;
                        var bIsNew = true;
                        for (var idx2 = 0; idx2 < rowData.length; idx2++) {
                            if (rowData[idx2]["id"] == id) {
                                var rdata = grid.getRowObj(idx2);
                                grid.updateRow(data.Rows[idx], rdata);
                                bIsNew = false;
                            }
                        }
                        if (bIsNew) {
                            arrNewRow.push(data.Rows[idx]);
                        }
                    }
                    //新加行
                    for (var i = 0; i < arrNewRow.length; i++) {
                        grid.addRow(arrNewRow[i]);
                    }
                    //如果有新消息，播放声音
                    if (data.HasNewMsg == "1") {
                        playSound();
                    }
                }
            );            
            
            window.setTimeout(UpdateState, 1000);
        }
        //每隔1秒更新一次
        window.setTimeout(UpdateState, 1000);
    </script>
</head>
<body style="padding:6px; overflow:hidden;"> 
  <div id="toptoolbar"></div> 
   <div id="gridTable" style="margin:0; padding:0"></div>
   
   <div style="display:none">
   <object classid="clsid:6BF52A52-394A-11D3-B153-00C04F79FAA6" id="WindowsMediaPlayer1" width="1" height="1">
        <param name="URL" value="../images/msg.wav">
        <param name="rate" value="1">
        <param name="balance" value="0">
        <param name="currentPosition" value="0">
        <param name="defaultFrame" value>
        <param name="playCount" value="1">
        <param name="autoStart" value="0">
        <param name="currentMarker" value="0">
        <param name="invokeURLs" value="-1">
        <param name="baseURL" value>
        <param name="volume" value="50">
        <param name="mute" value="0">
        <param name="uiMode" value="full">
        <param name="stretchToFit" value="0">
        <param name="windowlessVideo" value="0">
        <param name="enabled" value="-1">
        <param name="enableContextMenu" value="-1">
        <param name="fullScreen" value="0">
        <param name="SAMIStyle" value>
        <param name="SAMILang" value>
        <param name="SAMIFilename" value>
        <param name="captioningID" value>
        <param name="enableErrorDialogs" value="0">
    </object>
   </div>
   
</body>
</html>
