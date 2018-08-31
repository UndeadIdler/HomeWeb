<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="emailgroup_edit.aspx.cs" Inherits="JumboECMS.WebFile.Admin._emailgroup_edit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>编辑邮件组</title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link type="text/css" rel="stylesheet" href="../_data/global.css" />
<link type="text/css" rel="stylesheet" href="css/common.css" />


<script type="text/javascript">
$(document).ready(function(){
	$.formValidator.initConfig({onError:function(msg){alert(msg);}});
	$("#txtGroupName").formValidator({tipid:"tipGroupName",onshow:"请输入邮件组名",onfocus:"建议输入4-6个汉字",oncorrect:"OK"}).InputValidator({min:6,max:12,onerror:"你输入的长度不正确,请确认"});
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
			<th> 分组名称 </th>
			<td><asp:TextBox ID="txtGroupName" runat="server" Width="100px" MaxLength="20" CssClass="ipt"></asp:TextBox>
				<span id="tipGroupName" style="width:200px;"> </span></td>
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
