<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="javascript_edit.aspx.cs" Inherits="JumboECMS.WebFile.Admin._javascriptedit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   name="robots" content="noindex, nofollow" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>自定义外站调用</title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link type="text/css" rel="stylesheet" href="../_data/global.css" />
<link type="text/css" rel="stylesheet" href="css/common.css" />
<script type="text/javascript">
$(document).ready(function(){
	$.formValidator.initConfig({onError:function(msg){alert(msg);}});
	$("#txtTitle").formValidator({tipid:"tipTitle",onshow:"请输入名称",onfocus:"请输入名称",oncorrect:"合法"}).InputValidator({min:8,max:30,onerror:"请输入8-30个字符或4-15个汉字,请确认"});
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
                    <li><span style="color:Red">每个本站的链接地址前务必带上{site.Url}</span></li>
                    <li>更多功能请查阅《模板和标签.doc》</li>
                </ul>
            </td>
        </tr>
    </table>
<form id="form1" runat="server" onsubmit="return CheckFormSubmit()">
	<table class="formtable mrg10T">
		<tr>
			<th> 名称 </th>
			<td><asp:TextBox ID="txtTitle" runat="server" MaxLength="20" Width="225px" CssClass="ipt"></asp:TextBox>
				<span id="tipTitle" style="width:150px;"> </span></td>
		</tr>
		<tr>
			<th> 模板内容 </th>
			<td> <asp:TextBox ID="txtTemplateContent" runat="server" Width="97%" CssClass="ipt" 
                    TextMode="MultiLine" Height="271px"></asp:TextBox>
				<span id="tipCode" style="width:150px;"> </span></td>
		</tr>
		<tr style="display:none;">
			<th> 代码名 </th>
			<td> <asp:TextBox ID="txtCode" runat="server" Width="97%" CssClass="ipt"></asp:TextBox>
				<span id="Span1" style="width:1px;display:none;"> </span></td>
		</tr>
		</table>
	<div class="buttonok">
		<asp:Button ID="btnSave" runat="server" Text="确定" CssClass="btnsubmit" 
            onclick="btnSave_Click" />
		<input id="btnReset" type="button" value="取消" class="btncancel" OnClick="parent.JumboECMS.Popup.hide();" />
	</div>
</form>
<script type="text/javascript">_jcms_SetDialogTitle();</script>
</body>
</html>
