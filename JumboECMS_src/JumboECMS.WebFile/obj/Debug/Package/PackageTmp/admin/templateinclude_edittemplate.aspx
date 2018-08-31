<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="templateinclude_edittemplate.aspx.cs" Inherits="JumboECMS.WebFile.Admin._templateinclude_edittemplate" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   name="robots" content="noindex, nofollow" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>模板在线编辑</title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link type="text/css" rel="stylesheet" href="../_data/global.css" />
<link type="text/css" rel="stylesheet" href="css/common.css" />
<script type="text/javascript">
$(document).ready(function(){
	$.formValidator.initConfig({onError:function(msg){alert(msg);}});
	$("#txtTemplateContent").formValidator({tipid:"tipTemplateContent",onshow:"",onfocus:"请输入内容",oncorrect:"OK"}).InputValidator({min:5,onerror:"模板内容太少了吧"});
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
                    <li>如果你对html知识不熟悉，请勿擅自修改。</li>
                </ul>
            </td>
        </tr>
    </table>
<form id="form1" runat="server" onsubmit="return CheckFormSubmit()">
	<table class="formtable mrg10T">
		<tr>
			<td>对应模板文件:<asp:Label ID="lblTemplateFile" runat="server" Text=""></asp:Label><br />
			<br />
			<asp:TextBox ID="txtTemplateContent" runat="server" Width="100%" TextMode="MultiLine"
                    Height="310px"></asp:TextBox>
				<span id="tipTemplateContent" style="width:200px;"> </span>
                
            </td>
		</tr>
	</table>
	<div class="buttonok">
		<asp:Button ID="btnSave" runat="server" Text="确定" CssClass="btnsubmit" OnClick="btnSave_Click" />
		<input id="btnReset" type="button" value="取消" class="btncancel" onclick="parent.JumboECMS.Popup.hide();" />
	</div>
</form>
<script type="text/javascript">_jcms_SetDialogTitle();</script>
</body>
</html>
