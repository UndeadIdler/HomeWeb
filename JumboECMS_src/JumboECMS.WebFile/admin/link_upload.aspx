﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="link_upload.aspx.cs" Inherits="JumboECMS.WebFile.Admin._link_upload" %>

<%@ Register Assembly="JumboECMS.WebControls" Namespace="JumboECMS.WebControls" TagPrefix="Jumboe" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>文件上传</title>
<style type="text/css">
body{margin: 0px;}
</style>
<script type="text/javascript">
function OnCompleted(callBack){
	var filename = callBack.split('|')[0];
	var filesize = callBack.split('|')[1];
	var s = filename.lastIndexOf(".");
	var e = filename.substring(s+1).toLowerCase();
	parent.AttachmentOperater(filename,e,filesize);
	window.location.reload();
}
</script>

</head>
<body>
    <form id="form1" runat="server">
    <div id="SWFUpload">
        <Jumboe:FlashUpload ID="flashUpload" runat="server" FileTypeDescription="*.*" UploadPage="/FlashUpload.asx"
            Args="" UploadFileSizeLimit="1800000">
        </Jumboe:FlashUpload>
    </div>
    </form>
</body>
</html>
