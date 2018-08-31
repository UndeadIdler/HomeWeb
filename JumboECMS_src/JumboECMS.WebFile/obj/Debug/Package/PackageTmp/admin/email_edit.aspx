<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="email_edit.aspx.cs" Inherits="JumboECMS.WebFile.Admin._email_edit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>编辑联系人</title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link type="text/css" rel="stylesheet" href="../_data/global.css" />
<link type="text/css" rel="stylesheet" href="css/common.css" />


<script type="text/javascript">
$(document).ready(function(){
	$.formValidator.initConfig({onError:function(msg){alert(msg);}});
	$("#txtNickName").formValidator({tipid:"tipNickName",onshow:"请输入昵称",onfocus:"昵称2-14个字符",oncorrect:"OK"}).InputValidator({min:2,max:14,onerror:"昵称2-14个字符"}).RegexValidator({regexp:"username",datatype:"enum",onerror:"汉字或字母开头,不支持符号"});
	$("#txtEmailAddress")
		.formValidator({tipid:"tipEmailAddress",onshow:"请输入邮箱",onfocus:"8-50个字符",oncorrect:"OK"})
		.InputValidator({min:8,max:50,onerror:"邮箱非法,请确认"})
		.RegexValidator({regexp:"email",datatype:"enum",onerror:"格式不正确"})
		.AjaxValidator({
		type : "get",
		data:		"id=<%=id%>",
		url:		"email_ajax.aspx?oper=checkemail&time="+(new Date().getTime()),
		datatype : "json",
		success : function(d){	
			if(d.result == "1")//说明不存在
				return true;
			else
				return false;
		},
		buttons: $("#btnSave"),
		error: function(){alert("服务器繁忙，请稍后再试");},
		onerror : "邮箱已被使用",
		onwait : "正在校验邮箱的合法性，请稍候..."
	});
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
	<table cellspacing="0" cellpadding="0" width="100%" class="formtable">
		<tr>
			<th> 邮&nbsp; 箱 </th>
			<td><asp:TextBox ID="txtEmailAddress" runat="server" Width="180px" MaxLength="50" CssClass="ipt"></asp:TextBox>
				<span id="tipEmailAddress" style="width:200px;"> </span></td>
		</tr>
		<tr>
			<th> 昵&nbsp; 称 </th>
			<td><asp:TextBox ID="txtNickName" runat="server" Width="120px" MaxLength="20" CssClass="ipt"></asp:TextBox>
				<span id="tipNickName" style="width:200px;"> </span></td>
		</tr>
		<tr>
			<th> 状&nbsp; 态 </th>
			<td><asp:RadioButtonList ID="rbtState" runat="server" RepeatColumns="2">
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
		<input id="btnReset" type="button" value="取消" class="btncancel" onclick="top.JumboECMS.Popup.hide();" />
	</div>
</form>
<script type="text/javascript">_jcms_SetDialogTitle();</script>
</body>
</html>
