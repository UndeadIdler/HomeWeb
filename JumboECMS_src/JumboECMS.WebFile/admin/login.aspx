<%@ Page Language="C#" AutoEventWireup="true" Codebehind="login.aspx.cs" Inherits="JumboECMS.WebFile.Admin._login" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   name="robots" content="noindex, nofollow" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>管理中心</title>
<script type="text/javascript" src="../_libs/swfobject.js"></script>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<script type="text/javascript" src="scripts/admin.js"></script>
<script type="text/javascript">if (top!=self)top.location=self.location;</script>
<script type="text/javascript">
<!--
function CheckBrowser() 
{
	var app=navigator.appName;
	var verStr=navigator.appVersion;
	if (app.indexOf('Netscape') != -1) {
		alert("友情提示：\n    你使用的是Netscape浏览器，可能会导致无法使用后台的部分功能。建议您使用 IE6.0 或以上版本。");
	} 
	else if (app.indexOf('Microsoft') != -1) {
	if (verStr.indexOf("MSIE 3.0")!=-1 || verStr.indexOf("MSIE 4.0") != -1 || verStr.indexOf("MSIE 5.0") != -1 || verStr.indexOf("MSIE 5.1") != -1)
		alert("友情提示：\n    您的浏览器版本太低，可能会导致无法使用后台的部分功能。建议您使用 IE6.0 或以上版本。");
	}
}
//浏览器兼容访问DOM 
function thisMovie(movieName) {  
	if (navigator.appName.indexOf("Microsoft") != -1) {      
		return window[movieName];
	}    
	else {       
		return document[movieName];   
	} 
}
function ajaxChkLogin(uName,uPass,cookieday,screenwidth){
	$.ajax({
		type:		"post",
		dataType:	"html",
		url:		"ajax.aspx?oper=login&time="+(new Date().getTime()),
		data:		"name="+uName+"&pass="+encodeURIComponent(uPass)+"&type="+cookieday+"&screenwidth="+screenwidth,
		error:		function(XmlHttpRequest,textStatus, errorThrown) { thisMovie("LoginFlexApp")._LoginCallBack("\"网络堵塞,稍后再试\"");},
		success:	function(d){
			//thisMovie("LoginFlexApp")._LoginCallBack(d);//内置调用
			if(d=="ok")
				top.location.href = 'default.aspx';
			else
				alert(d);
		}
	});
}

function sendRequest(p){
	if(p==0)
	{
		return;
	}
	$.ajax({
		url:		"ajax.aspx?time="+(new Date().getTime()),
		type:		"get",
		dataType:	"json",
		error:		function(XmlHttpRequest,textStatus, errorThrown) { if(XmlHttpRequest.responseText.length>0){alert(XmlHttpRequest.responseText);}},
		success:	function(d){
			if(d.result=="1")
				top.location.href = 'default.aspx';
		}
	});
}
//-->
</script>
</head>
<style type="text/css" media="screen"> 
html, body, #swfDiv { height:100%; } 
body { margin:0; padding:0; overflow:hidden; }
</style>

<body scroll="no" id="body">
<div id="swfDiv"></div>
<script type="text/javascript">
//CheckBrowser();
//sendRequest(1);

var flashvars = {};
flashvars.Copyright = 'Powered By JumboECMS';
var params = {};
params.quality = "high";
params.bgcolor = "#869ca7";
params.allowScriptAccess = "sameDomain";
params.allowfullscreen = "true";
var attributes = {};
attributes.id="LoginFlexApp";
attributes.name="LoginFlexApp";
swfobject.embedSWF(site.Dir + "statics/flex3/admin/AdminLogin.swf", "swfDiv", "100%", "100%", "9.0.0", site.Dir + "statics/flex3/expressInstall.swf", flashvars, params, attributes); 
</script>
</body>
</html>