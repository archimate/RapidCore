<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendMsg.aspx.cs" Inherits="IM_SendMsg" %>
<%@ Import Namespace="ErpCoreModel" %>
<%@ Import Namespace="ErpCoreModel.Base" %>
<%@ Import Namespace="ErpCoreModel.Framework" %>
<%@ Import Namespace="ErpCoreModel.UI" %>
<%@ Import Namespace="ErpCoreModel.IM" %>
<%@ Import Namespace="System.Collections.Generic" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>    
    <link href="../lib/ligerUI/skins/Aqua/css/ligerui-all.css" rel="stylesheet" type="text/css" />
    <script src="../lib/jquery/jquery-1.5.2.min.js" type="text/javascript"></script>
    <script src="../lib/ligerUI/js/core/base.js" type="text/javascript"></script>
   <script src="../lib/ligerUI/js/plugins/ligerLayout.js" type="text/javascript"></script>
   
        <script type="text/javascript">
            var to_id = '<%=m_FriendUser.Id%>';
            
            $(function() {

                $("#layout1").ligerLayout();
            });

            function AddContent(sLine) {
                document.getElementById('content').innerHTML = document.getElementById('content').innerHTML + "<br>" + sLine;
                document.getElementById('msg_end').scrollIntoView();
            }
            
            function btSend_onclick() {
                if (document.getElementById('txtMsg').value == "")
                    return;
                var sLine = "<span style='color:Blue'>我 " + GetCurTime() + "</span><br>";
                sLine += document.getElementById('txtMsg').value.replace("\n", "<br>");
                AddContent(sLine);
                
                var sMsg = document.getElementById('txtMsg').value;

                $.post(
                    'SendMsg.aspx',
                    {
                        Action: 'Send',
                        to_id: to_id,
                        msg: sMsg
                    },
                     function(data) {
                         if (data == "" || data == null) {
                             return true;
                         }
                         else {
                             parent.$.ligerDialog.warn("发送失败！");
                             return false;
                         }
                     },
                    'text');
                
                document.getElementById('txtMsg').value = "";
            }

            //获取当前时间
            function GetCurTime() {
                Date.prototype.pattern = function(fmt) {
                    var o = {
                        "M+": this.getMonth() + 1, //月份        
                        "d+": this.getDate(), //日        
                        "h+": this.getHours() % 12 == 0 ? 12 : this.getHours() % 12, //小时        
                        "H+": this.getHours(), //小时        
                        "m+": this.getMinutes(), //分        
                        "s+": this.getSeconds(), //秒        
                        "q+": Math.floor((this.getMonth() + 3) / 3), //季度        
                        "S": this.getMilliseconds() //毫秒        
                    };
                    var week = {
                        "0": "\u65e5",
                        "1": "\u4e00",
                        "2": "\u4e8c",
                        "3": "\u4e09",
                        "4": "\u56db",
                        "5": "\u4e94",
                        "6": "\u516d"
                    };
                    if (/(y+)/.test(fmt)) {
                        fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
                    }
                    if (/(E+)/.test(fmt)) {
                        fmt = fmt.replace(RegExp.$1, ((RegExp.$1.length > 1) ? (RegExp.$1.length > 2 ? "\u661f\u671f" : "\u5468") : "") + week[this.getDay() + ""]);
                    }
                    for (var k in o) {
                        if (new RegExp("(" + k + ")").test(fmt)) {
                            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
                        }
                    }
                    return fmt;
                }

                var now = new Date();
                return now.pattern("yyyy-MM-dd hh:mm:ss");
            }


            //定时获取好友消息
            function UpdateMessage() {

                $.post(
                    'SendMsg.aspx',
                    {
                        Action: 'GetMessage',
                        to_id: to_id
                    },
                    function(data) {
                        if (data == null || data == "") {
                            return false;
                        }
                        else {
                            AddContent(data);
                            return true;
                        }
                    },
                    'text'
                );

                window.setTimeout(UpdateMessage, 1000);
            }
            //每隔1描述更新一次
            window.setTimeout(UpdateMessage, 1000);
        </script> 
    <style type="text/css">
        body{ padding:10px; margin:0;}
        #layout1{  width:100%; margin:40px;  height:400px;
                   margin:0; padding:0;} 
        #accordion1 { height:270px;}
         h4{ margin:20px;}
    </style>

</head>
<body style="padding:10px">
     <div id="layout1">
            <div position="center" title="<%=m_FriendUser.Name %>" style="position:absolute; height:100%; width:100%; overflow:auto">
            <div id="content"></div>
            <div id="msg_end" style="height:0px; overflow:hidden"></div>
            </div>
            <div position="bottom">
                <table><tr><td>
                <textarea id="txtMsg" name="txtMsg" rows=3 cols=55></textarea>
                </td><td valign=top>
                <input type="button" id="btSend" name="btSend" value="发送" style="width:70px; height:30px" onclick="return btSend_onclick()" />
                </td></tr></table>
            </div>
        </div> 
           
     <div style="display:none;"/>

</body>
</html>
