<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="category_modify3.aspx.cs" Inherits="JumboECMS.WebFile.Admin.category_modify3" %>
<%@ Register Assembly="JumboECMS.FCKeditorV2" Namespace="JumboECMS.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   name="robots" content="noindex, nofollow" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>单页管理</title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link type="text/css" rel="stylesheet" href="../_data/global.css" />
<link type="text/css" rel="stylesheet" href="css/common.css" />
<script type="text/javascript">
//插入上传图片
function AttachmentOperater0(path,type,size){
	$('#txtImg').val(path);
}
//插入上传附件
function AttachmentOperater(path,type,size){
	var editType="FCKeditor";
	var html;
	if (editType == "FCKeditor"){
		var oEditor = FCKeditorAPI.GetInstance('FCKeditor1');
	}
	html = '<br /><a href="'+path+'" target="_blank"><img src="'+path+'" border="0"></a><br />';
	oEditor.InsertHtml(html);
}
var PhotoInput = 'txtImg';
function FillPhoto(photo){
	$('#'+PhotoInput).val(photo);
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
			<th> 页面名称 </th>
			<td><asp:Label ID="lblTitle" runat="server" Text=""></asp:Label></td>
		</tr>
		<tr>
			<th> 页面简介</th>
			<td><asp:TextBox ID="txtInfo" runat="server" Height="120px" TextMode="MultiLine" Width="97%" CssClass="ipt"></asp:TextBox>
			</td>
		</tr>
        <tr>
          <th>页面关键词</th>
          <td><asp:TextBox ID="txtKeywords" runat="server" Width="97%" CssClass="ipt"></asp:TextBox>
          </td>
        </tr>
		<tr>
			<th> 页面标志图</th>
			<td><asp:TextBox ID="txtImg" runat="server" MaxLength="100" Width="97%" CssClass="ipt"></asp:TextBox>
				<a href="javascript:void(0);" onclick="PhotoInput = 'txtImg';WinFullOpen('cut2thumbs_upload.aspx?photo='+encodeURIComponent($('#txtImg').val()));">
                <img src="images/createthumbs.png" align="absmiddle" style="border:0px;" />
				</a> </td>
		</tr>
		<tr>
			<th>上传标志图</th>
			<td><iframe id="frm_upload" src="attachment_default.aspx?number=0" width="100%" height="30" scrolling="no" frameborder="0"></iframe></td>
		</tr>
		<tr>
			<th>页面内容</th>
			<td><asp:CheckBox ID="chkSaveRemotePhoto" runat="server" Text="远程图片本地化" Checked="true" />
			<br /><FCKeditorV2:FCKeditor ID="FCKeditor1" runat="server" ToolbarSet="Jumboe" Height="350px" Width="100%"> </FCKeditorV2:FCKeditor>
				<br />
				<asp:Label ID="txtContentMsg" runat="server" ForeColor="Red"></asp:Label>
			</td>
		</tr>
		<tr>
			<th>上传内容图片</th>
			<td><iframe id="frm_upload" src="attachment_default.aspx" width="100%" height="30" scrolling="no" frameborder="0"></iframe></td>
		</tr>
	</table>
	<div class="buttonok">
		<asp:Button ID="btnSave" runat="server" Text="确定" CssClass="btnsubmit" 
            onclick="btnSave_Click" />
	</div>
</form>
</body>
</html>
