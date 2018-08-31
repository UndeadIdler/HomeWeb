<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="templateinclude_edit.aspx.cs" Inherits="JumboECMS.WebFile.Admin._templateinclude_edit" %>
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
	$("#txtTitle").formValidator({tipid:"tipTitle",onshow:"请输入模块名称",onfocus:"请输入6-20个字符",oncorrect:"OK"}).InputValidator({min:6,max:20,onerror:"请输入6-20个字符"});
	$("#txtSort").formValidator({tipid:"tipSort",onshow:"数字越大，优先权越高",onfocus:"0为最低",oncorrect:"OK"}).RegexValidator({regexp:"^\\d{1,3}$",onerror:"请填写1000以内的数字"});
	$("#txtSource").formValidator({tipid:"tipSource",onshow:"文件名可包含字母、数字和下划线",onfocus:"请把文件放相应的方案目录下",oncorrect:"OK"}).RegexValidator({regexp:"^[_a-zA-Z0-9]+\.htm$",onerror:"后缀为.htm"});
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
			<th> 模块名称 </th>
			<td> <asp:TextBox ID="txtTitle" runat="server" MaxLength="20" Width="180px" CssClass="ipt"></asp:TextBox>
				<span id="tipTitle" style="width:200px;"> </span></td>
		</tr>
		<tr>
			<th> 文件简介</th>
			<td><asp:TextBox ID="txtInfo" runat="server" Width="300px" CssClass="ipt"></asp:TextBox></td>
		</tr>
		<tr>
			<th> 优先级</th>
			<td><asp:TextBox ID="txtSort" runat="server" Width="39px" CssClass="ipt">0</asp:TextBox>
				<span id="tipSort" style="width:200px;"> </span></td>
		</tr>
		<tr>
			<th> 文件名称 </th>
			<td> <asp:TextBox ID="txtSource" runat="server" Width="120px" CssClass="ipt"></asp:TextBox>
				<span id="tipSource" style="width:200px;"> </span></td>
		</tr>
		<tr>
			<th> 是否需要编译</th>
			<td><asp:RadioButtonList ID="rblNeedBuild" runat="server" EnableViewState="False" RepeatColumns="2">
					<Items>
						<asp:ListItem Value="1" Selected="True">是</asp:ListItem>
						<asp:ListItem Value="0">否</asp:ListItem>
					</Items>
				</asp:RadioButtonList></td>
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
