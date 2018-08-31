<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="myinfo_password.aspx.cs" Inherits="JumboECMS.WebFile.Admin._myinfo_password" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   name="robots" content="noindex, nofollow" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title></title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link type="text/css" rel="stylesheet" href="../_data/global.css" />
<link type="text/css" rel="stylesheet" href="css/common.css" />


<script type="text/javascript">
$(document).ready(function(){
	$.formValidator.initConfig({onError:function(msg){alert(msg);}});
	$("#txtOldPass").formValidator({tipid:"tipOldPass",onshow:"请输入旧密码",onfocus:"请正确输入旧密码",oncorrect:"OK",defaultvalue:""}).InputValidator({min:6,onerror:"旧密码不低于6位"});
	$("#txtNewPass1").formValidator({tipid:"tipNewPass1",onshow:"请输入密码",onfocus:"密码6-14位",oncorrect:"OK"}).InputValidator({min:6,max:14,onerror:"密码6-14位"});
	$("#txtNewPass2").formValidator({tipid:"tipNewPass2",onshow:"请输入重复密码",onfocus:"密码必须一致",oncorrect:"OK"}).InputValidator({min:6,max:14,onerror:"密码6-14位,请确认"}).CompareValidator({desID:"txtNewPass1",operateor:"=",onerror:"两次密码不一致"});
});
    </script>
<script type="text/javascript">
function chkPost()
{
    if($.formValidator.PageIsValid('1'))
        ajaxChangePass();
}
function ajaxChangePass()
{
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"oldpass="+encodeURIComponent($("#txtOldPass").val())+"&newpass="+encodeURIComponent($("#txtNewPass1").val()),
		url:		"myinfo_ajax.aspx?oper=changepass&time="+(new Date().getTime()),
		error:		function(XmlHttpRequest,textStatus, errorThrown) { if(XmlHttpRequest.responseText.length>0){alert(XmlHttpRequest.responseText);}},
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				top.JumboECMS.Alert(d.returnval, "0", "top.window.location='login.aspx';");
				break;
			case '0':
				top.JumboECMS.Alert(d.returnval, "0");
				break;
			case '1':
				top.JumboECMS.Message(d.returnval, "1");
				break;
			}
			$("#txtOldPass").val("");
			$("#txtNewPass1").val("");
			$("#txtNewPass2").val("");
		}
	});
}
</script>
</head>
<body>
<table class="helptable mrg10T">
	<tr>
		<td><ul>
				<li>建议密码修改正确后退出系统后重新登录</li>
			</ul></td>
	</tr>
</table>
<form id="form1">
	<table class="formtable mrg10T">
		<tr>
			<th> 旧 密 码 </th>
			<td><input name="txtOldPass" type="password" id="txtOldPass" style="width:160px;" class="ipt" />
				<span id="tipOldPass" style="width:200px;"> </span></td>
		</tr>
		<tr>
			<th> 新 密 码 </th>
			<td><input name="txtNewPass1" type="password" id="txtNewPass1" style="width:160px;" class="ipt" />
				<span id="tipNewPass1" style="width:200px;"> </span></td>
		</tr>
		<tr>
			<th> 密码确认 </th>
			<td><input name="txtNewPass2" type="password" id="txtNewPass2" style="width:160px;" class="ipt" />
				<span id="tipNewPass2" style="width:200px;"> </span></td>
		</tr>
	</table>
	<div class="buttonok">
		<input type="button" onclick="chkPost();" value="修改" id="btnSaveContent" class="btnsubmit" />
	</div>
</form>
</body>
</html>
