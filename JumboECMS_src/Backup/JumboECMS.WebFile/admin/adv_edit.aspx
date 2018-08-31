<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="adv_edit.aspx.cs" Inherits="JumboECMS.WebFile.Admin._adv_edit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>广告管理</title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link type="text/css" rel="stylesheet" href="../_data/global.css" />
<link type="text/css" rel="stylesheet" href="css/common.css" />
<script type="text/javascript">
$(document).ready(function(){
	$('.tip-t').jtip({gravity: 't',fade: false});
	$('.tip-b').jtip({gravity: 'b',fade: false});
	$("input[@type='text']").attr("AutoComplete", "off");
	$.formValidator.initConfig({onError:function(msg){alert(msg);}});
	$("#txtTitle").formValidator({tipid:"tipTitle",onshow:"请填写广告位名称",onfocus:"请填写广告位名称"}).InputValidator({min:4,onerror:"请填写广告位名称"});
	$("#txtWidth").formValidator({tipid:"tipWidth",onshow:"对img、flash、iframe有效",onfocus:"请填写数字"}).RegexValidator({regexp:"^\([0-9]+)$",onerror:"请填写数字"});
	$("#txtHeight").formValidator({tipid:"tipHeight",onshow:"对img、flash、iframe有效",onfocus:"请填写数字"}).RegexValidator({regexp:"^\([0-9]+)$",onerror:"请填写数字"});

});
window.isIE = (navigator.appName == "Microsoft Internet Explorer");
//插入预览附件地址
function AttachmentSelected(path,elementid)
{
	$("#"+elementid).val(path);
}
//插入上传附件
function AttachmentOperater(path,type,size){
	$("#txtPicurl").val(path);
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
			<th> 广告位名称 </th>
			<td><asp:TextBox ID="txtTitle" runat="server" MaxLength="150" Width="97%" CssClass="ipt"></asp:TextBox>
				<span id="tipTitle" style="width:200px"></span>
			</td>
		</tr>
		<tr>
			<th> 链接URL<input type="button" class="tip-t" tip="只对图片广告有效" /></th>
			<td><asp:TextBox ID="txtUrl" runat="server" MaxLength="150" Width="97%" CssClass="ipt"></asp:TextBox>
				<span id="tipUrl" style="width:200px"></span>
			</td>
		</tr>
		<tr>
			<th> 广告类型 </th>
			<td><asp:DropDownList ID="ddlAdvType" runat="server"> </asp:DropDownList>
			</td>
		</tr>
		<tr style="display:none;">
			<th> 发布时间 </th>
			<td><asp:TextBox ID="txtAddDate" runat="server" CssClass="ipt"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<th> 广告宽度 </th>
			<td><asp:TextBox ID="txtWidth" runat="server" Width="60px" MaxLength="4" CssClass="ipt">0</asp:TextBox>
			<span id="tipWidth" style="width:80px"></span>
			</td>
		</tr>
		<tr>
			<th> 广告高度 </th>
			<td><asp:TextBox ID="txtHeight" runat="server" Width="60px" MaxLength="4" CssClass="ipt">0</asp:TextBox>
			<span id="tipHeight" style="Height:80px"></span>
			</td>
		</tr>
		<tr>
			<th>广告图URL</th>
			<td><asp:TextBox ID="txtPicurl" runat="server" MaxLength="150" Width="97%" CssClass="ipt"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<th> 上传广告图 </th>
			<td><iframe id="frm_upload" src="adv_upload.aspx?type=adv" width="100%" height="30" scrolling="no" frameborder="0"></iframe></td>
		</tr>
		<tr>
			<th>简介/内容<input type="button" class="tip-t" tip="1、如果是嵌入代码，请将代码内容填入此处，此时，“链接URL”无效<br>2、如果是植入网页，请将网页地址填入此处，此时，“链接URL”无效<br>3、是图片、动画时“广告图URL”才有效，此处留空即可" /></th>
			<td><asp:TextBox ID="txtContent" runat="server" Height="200px" TextMode="MultiLine" Width="97%" CssClass="ipt"></asp:TextBox>
			</td>
		</tr>
		<tr style="display:none;">
			<th> 浏览状态 </th>
			<td><asp:RadioButtonList ID="rbtState" runat="server" RepeatColumns="4">
					<Items>
						<asp:ListItem Value="1">启用</asp:ListItem>
						<asp:ListItem Value="0" Selected="True">停用</asp:ListItem>
					</Items>
				</asp:RadioButtonList>
			</td>
		</tr>

	</table>
	<div class="buttonok">
		<asp:Button ID="btnSave" runat="server" Text="确定" CssClass="btnsubmit" 
            OnClick="btnSave_Click" />
		<input id="btnReset" type="button" value="取消" class="btncancel" onclick="history.go(-1);" />
	</div>
</form>
<script type="text/javascript">_jcms_SetDialogTitle();</script>
</body>
</html>
