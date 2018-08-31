<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="javascript_code.aspx.cs" Inherits="JumboECMS.WebFile.Admin._javascriptcode" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   name="robots" content="noindex, nofollow" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>外站调用</title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link type="text/css" rel="stylesheet" href="../_data/global.css" />
<link type="text/css" rel="stylesheet" href="css/common.css" />
<script type="text/javascript">
function copyToClipBoard(){
	var clipBoardContent = $("#txtCode").val();
	window.clipboardData.setData("Text",clipBoardContent);
	alert("复制成功");
	top.JumboECMS.Popup.hide();
}
</script>
</head>
<body>
<form id="form1" runat="server">
	<table class="formtable mrg10T">
		<tr>
		<th>输出效果</th>
			<td><asp:Literal ID="ltlCode" runat="server"></asp:Literal></td>
		</tr>
		<tr>
		<th>调用代码</th>
			<td class="mid"><asp:TextBox ID="txtCode" runat="server" Width="99%" Height="40px" TextMode="MultiLine"></asp:TextBox>
			</td>
		</tr>
	</table>
	<div class="buttonok">
		<input id="btnCopy" type="button" value="复制代码" class="btnsubmit" onclick="copyToClipBoard()" />
		<input id="btnReset" type="button" value="关闭窗口" class="btncancel" OnClick="parent.JumboECMS.Popup.hide();" />
	</div>
</form>
<script type="text/javascript">_jcms_SetDialogTitle();</script>
</body>
</html>
