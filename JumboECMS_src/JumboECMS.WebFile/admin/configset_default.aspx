<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="configset_default.aspx.cs" Inherits="JumboECMS.WebFile.Admin._config_index" %>
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
<link rel="stylesheet" href="../_libs/jquery.tabs/style.css" type="text/css">
<!--[if lte IE 7]>
<link rel="stylesheet" href="../_libs/jquery.tabs/style-ie.css" type="text/css">
<![endif]-->
<script type="text/javascript">
$(document).ready(function(){
	$('.tip-r').jtip({gravity: 'r',fade: false});
	$.formValidator.initConfig({onError:function(msg){alert(msg);}});
	$("#txtName1").formValidator({tipid:"tipName1",onshow:"请输入网站中文名称",onfocus:"请输入网站中文名称"}).InputValidator({min:1,onerror:"请输入网站中文名称"});
	$("#txtName2").formValidator({tipid:"tipName2",onshow:"请输入网站英文名称",onfocus:"请输入网站英文名称"}).InputValidator({min:1,onerror:"请输入网站英文名称"});
<%if (base.Edition == "All")
    { %>
	$("#txtUrl").formValidator({empty:true,tipid:"tipUrl",onshow:"如果不用多个域名请留空！",onfocus:"以http://开头，结尾不要加/。如:http://www.jumboecms.net",oncorrect:"OK",onempty:"如果不用多个域名请留空！"}).RegexValidator({regexp:"domain",datatype:"enum",onerror:"以http://开头，结尾不要加/"});
<%} %>
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
	<br />
	<div>
		<div>
			<ul class="tabs-nav">
				<li class="tabs-selected"><a href="configset_default.aspx"><span>基本参数</span></a></li>
			</ul>
		</div>
		<div class="tabs-container">
			<table class="formtable mrg10T">
				<tr>
					<th> 网站中文名称</th>
					<td><asp:TextBox ID="txtName1" runat="server" MaxLength="80" Width="550px" CssClass="ipt"></asp:TextBox>
						<span id="tipName1" style="width:200px;"> </span></td>
				</tr>
				<tr style="display:<%=(base.Edition == "All")?"":"none"%>">
					<th> 网站主域名 </th>
					<td><asp:TextBox ID="txtUrl" runat="server" MaxLength="60" Width="300px" CssClass="ipt"></asp:TextBox>
						<span id="tipUrl" style="width:200px;"> </span></td>
				</tr>
				<tr>
					<th> 网站中文关键字 </th>
					<td><asp:TextBox ID="txtKeywords1" runat="server" MaxLength="50" Width="97%" CssClass="ipt"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<th> 网站中文描述 </th>
					<td><asp:TextBox ID="txtDescription1" runat="server" MaxLength="500" Width="97%" CssClass="ipt"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<th> 中文备案号 </th>
					<td><asp:TextBox ID="txtICP1" runat="server" MaxLength="100" Width="200px" CssClass="ipt"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<th> 网站英文名称 </th>
					<td><asp:TextBox ID="txtName2" runat="server" MaxLength="80" Width="550px" CssClass="ipt"></asp:TextBox>
						<span id="tipName2" style="width:200px;"> </span></td>
				</tr>
				<tr>
					<th> 网站英文关键字 </th>
					<td><asp:TextBox ID="txtKeywords2" runat="server" MaxLength="50" Width="97%" CssClass="ipt"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<th> 网站英文描述 </th>
					<td><asp:TextBox ID="txtDescription2" runat="server" MaxLength="500" Width="97%" CssClass="ipt"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<th> 英文备案号 </th>
					<td><asp:TextBox ID="txtICP2" runat="server" MaxLength="200" Width="200px" CssClass="ipt"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<th> 允许会员注册 </th>
					<td><asp:RadioButtonList ID="rblAllowReg" runat="server" RepeatDirection="Horizontal">
							<asp:ListItem Value="1">是</asp:ListItem>
							<asp:ListItem Value="0">否</asp:ListItem>
						</asp:RadioButtonList>
					</td>
				</tr>
				<tr style="display:none">
					<th> 需要邮件激活 </th>
					<td><asp:RadioButtonList ID="rblCheckReg" runat="server" RepeatDirection="Horizontal">
							<asp:ListItem Value="1">是</asp:ListItem>
							<asp:ListItem Value="0">否</asp:ListItem>
						</asp:RadioButtonList>
					</td>
				</tr>
			</table>
		</div>
	</div>
	<div class="buttonok">
		<asp:Button ID="Button1" runat="server" Text="保存" CssClass="btnsubmit" OnClick="Button1_Click" />
	</div>
</form>
</body>
</html>
