<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="attachment_default2.aspx.cs" Inherits="JumboECMS.WebFile.Admin.Attachment._default2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>文件批量上传</title>
<style type="text/css">
body{margin: 0px;background-color:#fff;}
</style>
<script type="text/javascript">
function GetUploadFiles(filelist){
	parent.AttachmentOperater(filelist);
	window.location.reload();
}
</script>
<script type="text/javascript" src="../_libs/swfobject.js"></script>
</head>
<body scroll="no" id="body">
<form id="form1" runat="server">
<div id="swfDiv"></div>
<script type="text/javascript">
var flashvars = {};
flashvars.url = encodeURIComponent("<%=UploadPage %>");
flashvars.fileFilter = "<%=UploadFileType %>";
flashvars.maxSize = "<%=UploadFileSizeLimit %>";
flashvars.maxNumber = "8";
var params = {};
params.quality = "high";
params.bgcolor = "#FFFFFF";
params.allowScriptAccess = "sameDomain";
params.allowfullscreen = "true";
var attributes = {};
attributes.id="FileUploadApp";
attributes.name="FileUploadApp";
swfobject.embedSWF("<%=site.Dir %>statics/flex3/admin/FileUpload.swf", "swfDiv", "510", "340", "9.0.0", "<%=site.Dir %>statics/flex3/expressInstall.swf", flashvars, params, attributes); 
</script>
</form>
</html>
