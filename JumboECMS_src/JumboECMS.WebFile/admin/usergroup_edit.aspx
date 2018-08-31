<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="usergroup_edit.aspx.cs" Inherits="JumboECMS.WebFile.Admin._usergroup_edit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   name="robots" content="noindex, nofollow" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>编辑用户组</title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link type="text/css" rel="stylesheet" href="../_data/global.css" />
<link type="text/css" rel="stylesheet" href="css/common.css" />
<script type="text/javascript">
$(document).ready(function(){
	$.formValidator.initConfig({onError:function(msg){alert(msg);}});
	$("#txtGroupName").formValidator({tipid:"tipGroupName",onshow:"请输入用户组名",onfocus:"建议输入4-6个汉字",oncorrect:"OK"}).InputValidator({min:6,max:12,onerror:"你输入的长度不正确,请确认"});
	$("#GroupSet3").formValidator({tipid:"tipGroupSet3",onshow:"起止小时，|隔开",onfocus:"格式例如：0|23",oncorrect:"OK"}).RegexValidator({regexp:"^[0-9]{1}\\|[0-9]{1,2}$",onerror:"格式有误"});
	$("#GroupSet5").formValidator({tipid:"tipGroupSet5",onshow:"0为不限制",onfocus:"0为不限制",oncorrect:"OK"}).RegexValidator({regexp:"^\\d{1,3}$",onerror:"请填写1000以内的数字"});
	$("#GroupSet6").formValidator({tipid:"tipGroupSet6",onshow:"0为不限制",onfocus:"0为不限制",oncorrect:"OK"}).RegexValidator({regexp:"^\\d{1,3}$",onerror:"请填写1000以内的数字"});
	$("#GroupSet11").formValidator({tipid:"tipGroupSet11",onshow:"0为不限制",onfocus:"0为不限制",oncorrect:"OK"}).RegexValidator({regexp:"^\\d{1,3}$",onerror:"请填写1000以内的数字"});
	$("#GroupSet14").formValidator({tipid:"tipGroupSet14",onshow:"0为不限制",onfocus:"0为不限制",oncorrect:"OK"}).RegexValidator({regexp:"^\\d{1,3}$",onerror:"请填写1000以内的数字"});
	$("#GroupSet17").formValidator({tipid:"tipGroupSet17",onshow:"0为不限制",onfocus:"0为不限制",oncorrect:"OK"}).RegexValidator({regexp:"^\\d{1,3}$",onerror:"请填写1000以内的数字"});
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
		<tr>
			<th> 分组名称 </th>
			<td><asp:TextBox ID="txtGroupName" runat="server" Width="100px" MaxLength="20" CssClass="ipt"></asp:TextBox>
				<span id="tipGroupName" style="width:200px;"> </span></td>
		</tr>
		<tr>
			<th> 是否允许登陆 </th>
			<td><asp:RadioButtonList ID="rblIsLogin" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="1">是</asp:ListItem>
					<asp:ListItem Value="0" Selected>否</asp:ListItem>
				</asp:RadioButtonList>
			</td>
		</tr>
		<tr style="display:none;">
			<th> 是否可以修改密码 </th>
			<td><asp:RadioButtonList ID="GroupSet0" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="1">是</asp:ListItem>
					<asp:ListItem Value="0">否</asp:ListItem>
				</asp:RadioButtonList>
			</td>
		</tr>
		<tr style="display:none;">
			<th> 是否可以修改资料 </th>
			<td><asp:RadioButtonList ID="GroupSet1" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="1">是</asp:ListItem>
					<asp:ListItem Value="0">否</asp:ListItem>
				</asp:RadioButtonList>
			</td>
		</tr>
		<tr style="display:none;">
			<th> 是否可以找回密码 </th>
			<td><asp:RadioButtonList ID="GroupSet2" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="1">是</asp:ListItem>
					<asp:ListItem Value="0">否</asp:ListItem>
				</asp:RadioButtonList>
			</td>
		</tr>
		<tr style="display:none;">
			<th> 定时开关的时间 </th>
			<td><asp:TextBox ID="GroupSet3" runat="server" Width="50px" CssClass="ipt">0|23</asp:TextBox>
				<span id="tipGroupSet3" style="width:200px;"> </span></td>
		</tr>
		<tr style="display:none;">
			<th> 是否定时登陆 </th>
			<td><asp:RadioButtonList ID="GroupSet4" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="1">是</asp:ListItem>
					<asp:ListItem Value="0">否</asp:ListItem>
				</asp:RadioButtonList>
			</td>
		</tr>
		<tr style="display:none;">
			<th> 注册多少分钟后允许投票 </th>
			<td><asp:TextBox ID="GroupSet5" runat="server" maxlength="6" Width="50px" CssClass="ipt">0</asp:TextBox>
				<span id="tipGroupSet5" style="width:200px;"> </span></td>
		</tr>
		<tr style="display:none;">
			<th> 注册多少分钟后可以做某某事 </th>
			<td><asp:TextBox ID="GroupSet6" runat="server" maxlength="6" Width="50px" CssClass="ipt">0</asp:TextBox>
				<span id="tipGroupSet6" style="width:200px;"> </span></td>
		</tr>
		<tr style="display:none;">
			<th> 是否可以做某某事 </th>
			<td><asp:RadioButtonList ID="GroupSet7" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="1">是</asp:ListItem>
					<asp:ListItem Value="0">否</asp:ListItem>
				</asp:RadioButtonList>
			</td>
		</tr>
		<tr style="display:none;">
			<th> 做某某事需要审核 </th>
			<td><asp:RadioButtonList ID="GroupSet8" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="1">是</asp:ListItem>
					<asp:ListItem Value="0">否</asp:ListItem>
				</asp:RadioButtonList>
			</td>
		</tr>
		<tr>
			<th> 可以发布信息 </th>
			<td><asp:RadioButtonList ID="GroupSet9" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="1">是</asp:ListItem>
					<asp:ListItem Value="0">否</asp:ListItem>
				</asp:RadioButtonList>
			</td>
		</tr>
		<tr>
			<th> 需要审核 </th>
			<td><asp:RadioButtonList ID="GroupSet10" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="1">是</asp:ListItem>
					<asp:ListItem Value="0">否</asp:ListItem>
				</asp:RadioButtonList>
			</td>
		</tr>
		<tr style="display:none;">
			<th> 可发布的数量 </th>
			<td><asp:TextBox ID="GroupSet11" runat="server" maxlength="6" Width="50px" CssClass="ipt">0</asp:TextBox>
				<span id="tipGroupSet11" style="width:200px;"> </span></td>
		</tr>
		<tr>
			<th> 可以收藏信息 </th>
			<td><asp:RadioButtonList ID="GroupSet12" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="1">是</asp:ListItem>
					<asp:ListItem Value="0">否</asp:ListItem>
				</asp:RadioButtonList>
			</td>
		</tr>
		<tr style="display:none;">
			<th> 需要审核 </th>
			<td><asp:RadioButtonList ID="GroupSet13" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="1">是</asp:ListItem>
					<asp:ListItem Value="0">否</asp:ListItem>
				</asp:RadioButtonList>
			</td>
		</tr>
		<tr>
			<th> 可收藏的数量 </th>
			<td><asp:TextBox ID="GroupSet14" runat="server" maxlength="6" Width="50px" CssClass="ipt">0</asp:TextBox>
				<span id="tipGroupSet14" style="width:200px;"> </span></td>
		</tr>
		<tr style="display:none;">
			<th> 是否可以 </th>
			<td><asp:RadioButtonList ID="GroupSet15" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="1">是</asp:ListItem>
					<asp:ListItem Value="0">否</asp:ListItem>
				</asp:RadioButtonList>
			</td>
		</tr>
		<tr style="display:none;">
			<th> 需要审核 </th>
			<td><asp:RadioButtonList ID="GroupSet16" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Value="1">是</asp:ListItem>
					<asp:ListItem Value="0">否</asp:ListItem>
				</asp:RadioButtonList>
			</td>
		</tr>
		<tr style="display:none;">
			<th> 数量 </th>
			<td><asp:TextBox ID="GroupSet17" runat="server" maxlength="6" Width="50px" CssClass="ipt">0</asp:TextBox>
				<span id="tipGroupSet17" style="width:200px;"> </span></td>
		</tr>
	</table>
	<div class="buttonok">
		<asp:Button ID="btnSave" runat="server" Text="确定" CssClass="btnsubmit" />
		<input id="btnReset" type="button" value="取消" class="btncancel" OnClick="parent.JumboECMS.Popup.hide();" />
	</div>
</form>
<script type="text/javascript">_jcms_SetDialogTitle();</script>
</body>
</html>
