<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="adv_view.aspx.cs" Inherits="JumboECMS.WebFile.Admin._adv_view" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>广告预览</title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link type="text/css" rel="stylesheet" href="../_data/global.css" />
<link type="text/css" rel="stylesheet" href="css/common.css" />
</head>
<body>
<form id="form1" runat="server">
<asp:Literal ID="Literal1" runat="server"></asp:Literal>
<table class="formtable mrg10T">
	<tr>
		<th> aspx模版调用代码</th>
		<td>
            <asp:TextBox ID="txtASPXTmpTag" runat="server" CssClass="ipt" Width="97%"></asp:TextBox></td>
	</tr>
	<tr>
		<th> shtm模版调用代码</th>
		<td>
            <asp:TextBox ID="txtSHTMTmpTag" runat="server" CssClass="ipt" Width="97%"></asp:TextBox></td>
	</tr>
	<tr>
		<th> 其他模版调用代码</th>
		<td>
            <asp:TextBox ID="txtJSTmpTag" runat="server" CssClass="ipt" Width="97%"></asp:TextBox></td>
	</tr>
</table>

	<div class="buttonok">
		<input id="btnReset" type="button" value="返回" class="btncancel" onclick="parent.JumboECMS.Popup.hide();" />
	</div>
</form>
<script type="text/javascript">_jcms_SetDialogTitle();</script>
</body>
</html>
