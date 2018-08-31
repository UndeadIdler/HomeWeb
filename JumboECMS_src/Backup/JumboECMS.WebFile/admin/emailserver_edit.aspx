<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="emailserver_edit.aspx.cs" Inherits="JumboECMS.WebFile.Admin._emailserver_edit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>发件人信息</title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link type="text/css" rel="stylesheet" href="../_data/global.css" />
<link type="text/css" rel="stylesheet" href="css/common.css" />


<script type="text/javascript">
$(document).ready(function(){
	$.formValidator.initConfig({onError:function(msg){alert(msg);}});
	$("#txtFromAddress").formValidator({tipid:"tipFromAddress",onshow:"请输入发送人邮箱地址",onfocus:"推荐使用sina邮箱",oncorrect:"OK"}).RegexValidator({regexp:"email",datatype:"enum",onerror:"格式不正确"})
		.AjaxValidator({
		type : "get",
		data:		"id=<%=id%>",
		url:		"emailserver_ajax.aspx?oper=checkname&time="+(new Date().getTime()),
		datatype : "json",
		success : function(d){	
			if(d.result == "1")
				return true;
			else
				return false;
		},
		buttons: $("#btnSave"),
		error: function(){alert("服务器繁忙，请稍后再试");},
		onerror : "该邮箱已经存在",
		onwait : "正在校验代码名的合法性，请稍候..."
	}).DefaultPassed();
	$("#txtFromPwd").formValidator({tipid:"tipSystemPassword",onshow:"请输入正确的密码",onfocus:"请输入正确的密码",oncorrect:"OK"}).InputValidator({min:1,onerror:"必填"});
	$("#txtFromName").formValidator({tipid:"tipFromName",onshow:"请输入发件人署名",onfocus:"如：知远防务新闻",oncorrect:"OK"}).InputValidator({min:1,onerror:"请输入发件人署名"});
	$("#txtFromPwd").formValidator({tipid:"tipFromPwd",onshow:"请输入正确的密码",onfocus:"请输入正确的密码",oncorrect:"OK"}).InputValidator({min:1,onerror:"必填"});
	$("#txtSmtpHost").formValidator({tipid:"tipSmtpHost",onshow:"请输入smtp服务器",onfocus:"如：smtp.sina.com",oncorrect:"OK"}).InputValidator({min:5,onerror:"请输入smtp服务器"});
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
    <table class="helptable mrg10T">
        <tr>
            <td>
                <ul>
                    <li>发件人邮箱必须支持smtp才能发信</li>
                    <li>如果“测试收件人”没能收到以“邮箱配置测试邮件(请删)”为标题的邮件，说明邮箱已经被封</li>
                </ul>
            </td>
        </tr>
    </table>
<form id="form1" runat="server" onsubmit="return CheckFormSubmit()">
	<table cellspacing="0" cellpadding="0" width="100%" class="formtable">
		<tr>
			<th> 发件人邮箱 </th>
			<td><asp:TextBox ID="txtFromAddress" runat="server" MaxLength="80" Width="300px" CssClass="ipt"></asp:TextBox>
				<span id="tipFromAddress" style="width:200px;"> </span></td>
		</tr>
		<tr>
			<th> 发件人署名 </th>
			<td><asp:TextBox ID="txtFromName" runat="server" MaxLength="20" Width="300px" CssClass="ipt">知远防务新闻</asp:TextBox>
				<span id="tipFromName" style="width:200px;"> </span></td>
		</tr>
		<tr>
			<th> 发件人密码 </th>
			<td><asp:TextBox ID="txtFromPwd" runat="server" MaxLength="30" Width="300px" TextMode="Password" CssClass="ipt"></asp:TextBox>
				<span id="tipFromPwd" style="width:200px;"> </span></td>
		</tr>
		<tr>
			<th> smtp服务器 </th>
			<td><asp:TextBox ID="txtSmtpHost" runat="server" MaxLength="50" Width="300px" CssClass="ipt"></asp:TextBox>
				<span id="tipSmtpHost" style="width:200px;"> </span></td>
		</tr>
		<tr>
			<th> 测试收件人 </th>
			<td><asp:TextBox ID="txtToAddress" runat="server" MaxLength="80" Width="300px" CssClass="ipt">4259024@qq.com</asp:TextBox>
			</td>
		</tr>
	</table>
	<div class="buttonok">
		<asp:Button ID="btnSave" runat="server" Text="确定" CssClass="btnsubmit" 
            onclick="btnSave_Click" />
		<input id="btnReset" type="button" value="取消" class="btncancel" onclick="top.JumboECMS.Popup.hide();" />
	</div>
</form>
<script type="text/javascript">_jcms_SetDialogTitle();</script>
</body>
</html>
