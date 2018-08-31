<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admin_add.aspx.cs" Inherits="JumboECMS.WebFile.Admin._admin_add" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   name="robots" content="noindex, nofollow" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>添加管理员</title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link type="text/css" rel="stylesheet" href="../_data/global.css" />
<link type="text/css" rel="stylesheet" href="css/common.css" />


<script type="text/javascript">
$(document).ready(function(){
	$.formValidator.initConfig({onError:function(msg){alert(msg);}});
	$("#txtAdminName").formValidator({tipid:"tipAdminName",onshow:"请输入登录名",onfocus:"6-14个字符",oncorrect:"OK"}).InputValidator({min:6,max:14,onerror:"6-14个字符"}).RegexValidator({regexp:"username",datatype:"enum",onerror:"汉字或字母开头,不支持符号"})
		.AjaxValidator({
		type : "get",
		data:		"id=0",
		url:		"admin_ajax.aspx?oper=checkadminname&time="+(new Date().getTime()),
		datatype : "json",
		success : function(d){	
			if(d.result == "1")
				return true;
			else
				return false;
		},
		buttons: $("#btnSave"),
		error: function(){alert("服务器繁忙，请稍后再试");},
		onerror : "该登录名已经存在",
		onwait : "正在校验登录名的合法性，请稍候..."
	}).DefaultPassed();
	$("#txtAdminPass1").formValidator({tipid:"tipAdminPass1",onshow:"请输入密码",onfocus:"密码6-14位",oncorrect:"OK"}).InputValidator({min:6,max:14,onerror:"密码6-14位,请确认"});
	$("#txtAdminPass2").formValidator({tipid:"tipAdminPass2",onshow:"请输入重复密码",onfocus:"密码必须一致",oncorrect:"OK"}).InputValidator({min:6,max:14,onerror:"密码6-14位,请确认"}).CompareValidator({desID:"txtAdminPass1",operateor:"=",onerror:"两次密码不一致"});
});
/*最后的表单验证*/
function CheckFormSubmit() {
    if ($.formValidator.PageIsValid('1')) {
        JumboECMS.Loading.show("正在处理，请等待...");
        return true;
    } else {
        return false;
    }
}
    </script>
</head>
<body>
<form id="form1" runat="server" onsubmit="return CheckFormSubmit()">
	<table class="formtable mrg10T">
		<tr style="display:none;">
			<th> 前台用户名 </th>
			<td>
                <asp:Label ID="lblUserName" runat="server" Text=""></asp:Label></td>
		</tr>
		<tr>
			<th> 管理登陆名 </th>
			<td><asp:TextBox ID="txtAdminName" runat="server" Width="120px" MaxLength="20" ReadOnly="True" CssClass="ipt"></asp:TextBox>
				<span id="tipAdminName" style="width:200px;"> </span></td>
		</tr>
		<tr>
			<th> 密&nbsp; 码 </th>
			<td><asp:TextBox ID="txtAdminPass1" runat="server" TextMode="Password" Width="120px"
                    MaxLength="20" CssClass="ipt"></asp:TextBox>
				<span id="tipAdminPass1" style="width:200px;"> </span></td>
		</tr>
		<tr>
			<th> 确认密码 </th>
			<td><asp:TextBox ID="txtAdminPass2" runat="server" TextMode="Password" Width="120px"
                    MaxLength="20" CssClass="ipt"></asp:TextBox>
				<span id="tipAdminPass2" style="width:200px;"> </span></td>
		</tr>
		<tr>
			<th> 状&nbsp; 态 </th>
			<td><asp:RadioButtonList ID="rbtnAdminState" runat="server" RepeatColumns="2">
					<Items>
						<asp:ListItem Value="1" Selected="True">启用</asp:ListItem>
						<asp:ListItem Value="0">禁用</asp:ListItem>
					</Items>
				</asp:RadioButtonList>
			</td>
		</tr>
	</table>
	<div class="buttonok">
		<asp:Button ID="btnSave" runat="server" Text="确定" CssClass="btnsubmit" />
		<input id="btnReset" type="button" value="取消" class="btncancel" onclick="parent.JumboECMS.Popup.hide();" />
	</div>
</form>
<script type="text/javascript">_jcms_SetDialogTitle();</script>
</body>
</html>
