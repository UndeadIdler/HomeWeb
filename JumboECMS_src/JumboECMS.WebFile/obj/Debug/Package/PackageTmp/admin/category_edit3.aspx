<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="category_edit3.aspx.cs" Inherits="JumboECMS.WebFile.Admin.category_edit3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   name="robots" content="noindex, nofollow" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>单页栏目编辑</title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link type="text/css" rel="stylesheet" href="../_data/global.css" />
<link type="text/css" rel="stylesheet" href="css/common.css" />
<script type="text/javascript" src="../_libs/pinyin.js"></script>
<script type="text/javascript">
$(document).ready(function(){
	if(q('parentid')!='') $('#ddlParentId').val(q('parentid'));
	$('.tip-r').jtip({gravity: 'r',fade: false});
	$.formValidator.initConfig({onError:function(msg){alert(msg);}});
	$("#txtTitle").formValidator({tipid:"tipTitle",onshow:"请输入栏目名称",onfocus:"推荐使用4个汉字",oncorrect:"OK"}).InputValidator({min:4,max:30,onerror:"请输入4-30个字符(2-15个汉字)"});
	$("#txtAliasPage").formValidator({tipid:"tipAliasPage",onshow:"前台路径。如/aa/bb/cc.html",onfocus:"前台路径。如/aa/bb/cc.html",oncorrect:"OK"}).RegexValidator({regexp:"^\(/[_\-a-zA-Z0-9\.]+(/[_\-a-zA-Z0-9\.]+)*\.(aspx|htm(l)?|shtm(l)?))$",onerror:"格式错误"});
	$("#txtSortRank").formValidator({tipid:"tipSortRank",onshow:"请填写数字",onfocus:"请填写数字",oncorrect:"OK"}).RegexValidator({regexp:"^\([0-9]+)$",onerror:"请填写数字"});
});
function ajaxChinese2Pinyin(chinese,t)
{
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"chinese="+encodeURIComponent(chinese)+"&t="+t+"&time="+(new Date().getTime()),
		url:		"ajax.aspx?oper=ajaxChinese2Pinyin",
		error:		function(XmlHttpRequest,textStatus, errorThrown){JumboECMS.Loading.hide();if(XmlHttpRequest.responseText.length>0){alert(XmlHttpRequest.responseText);} },
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				JumboECMS.Alert(d.returnval, "0", "top.window.location='login.aspx';");
				break;
			case '0':
				JumboECMS.Alert(d.returnval, "0");
				break;
			case '1':
				$("#txtFolder").val(d.returnval);
				break;
			}
		}
	});
}
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
			<th> 栏目名称 </th>
			<td><asp:TextBox ID="txtTitle" runat="server" MaxLength="40" Width="300px" CssClass="ipt"></asp:TextBox>
				<span id="tipTitle" style="width:200px;"> </span></td>
		</tr>
		<tr style="display:none;">
			<th>序号</th>
			<td><asp:TextBox ID="txtSortRank" runat="server" MaxLength="3" Width="40px" CssClass="ipt">0</asp:TextBox>
				<span id="tipSortRank" style="width:200px;"> </span><br /><span class="red">在同级栏目下请勿重复</span></td>
		</tr>
		<tr>
			<th> 父级栏目 </th>
			<td><asp:DropDownList ID="ddlParentId" runat="server"> </asp:DropDownList>
				(一旦选择就无法修改，请慎重) </td>
		</tr>
		<tr>
			<th> 模板 </th>
			<td><asp:DropDownList ID="ddlTemplateId" runat="server"> </asp:DropDownList>
			</td>
		</tr>
		<tr>
			<th> 栏目简介</th>
			<td><asp:TextBox ID="txtInfo" runat="server" Height="97px" TextMode="MultiLine" Width="97%" CssClass="ipt"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<th> 前台路径 </th>
			<td><asp:TextBox ID="txtAliasPage" runat="server" Width="97%" CssClass="ipt"></asp:TextBox>
				<br /><span id="tipAliasPage" style="width:400px;"></span></td>
		</tr>
	</table>
	<div class="buttonok">
		<asp:Button ID="btnSave" runat="server" Text="确定" CssClass="btnsubmit" 
            onclick="btnSave_Click" />
		<input id="btnReset" type="button" value="取消" class="btncancel" onclick="parent.JumboECMS.Popup.hide();" />
	</div>
</form>
<script type="text/javascript">_jcms_SetDialogTitle();</script>
</body>
</html>
