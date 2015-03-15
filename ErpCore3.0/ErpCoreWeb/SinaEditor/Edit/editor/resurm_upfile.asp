<!--#include file="resurm_upload.inc"-->

<style>
td{font-size:9pt;line-height:120%;color:#353535} 
body{font-size:9pt;line-height:120%} 

a:link          { color: #000000; text-decoration: none }
a:visited       { color: #000000; text-decoration: none }
a:active        { color: #000000; text-decoration: none }
a:hover         { color: #336699; text-decoration: none; position: relative; right: 0px; top: 1px }
</style>

<%
On Error Resume Next
set upload=new upload_file
if upload.form("act")="uploadfile" then
	filepath= trim(upload.form("filepath"))
	filelx=trim(upload.form("filelx"))
	
	i=0
	for each formName in upload.File
		set file=upload.File(formName)
 
 fileExt=lcase(file.FileExt)	'得到的文件扩展名不含有.
 if file.filesize<100 then
 	response.write "<script>alert('请先选择你要上传的文件！');history.back();</script>"
	response.end
 end if
 
 if fileExt<>"jpg" and fileExt<>"gif" and fileExt<>"bmp" then
 response.Write("<script>alert('不允许上传该类型的文件！');history.back();</script>")
 response.End()
 end if

 randomize
 ranNum=int(90000*rnd)+10000
 filename=cstr(filepath)&cstr(year(date()))&month(date())&day(date())&hour(now())&minute(now())&second(now())&ranNum&"."&fileExt

 if file.FileSize>0 then

  file.SaveToFile Server.mappath( FileName)

 end if
 set file=nothing
	next
	set upload=nothing

if err then
response.Write("<script>alert('文件上传错误！\n错误描述："&err.description&"');history.back();</script>")
else
response.write("<script>window.parent.document.getElementById('imgpath').value='"&FileName&"';window.parent.chk_imgpath();</script>")
end if
end if
%>
