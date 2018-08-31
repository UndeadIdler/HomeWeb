<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="link_edit.aspx.cs" Inherits="JumboECMS.WebFile.Admin._link_edit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title></title>
<link   type="text/css" rel="stylesheet" href="css/common.css" />
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link   type="text/css" rel="stylesheet" href="../_data/global.css" />

<script type="text/javascript">
$(document).ready(function(){
	$.formValidator.initConfig({onError:function(msg){alert(msg);}});
	$("#txtTitle").formValidator({tipid:"tipTitle",onshow:"请输入网站名称",onfocus:"请输入4-30个字符(2-15个汉字)",oncorrect:"OK"})
		.InputValidator({min:4,max:30,onerror:"请输入4-30个字符(2-15个汉字)"})
		.AjaxValidator({
			type : "get",
			data:		"id=<%=id%>",
			url:		"link_ajax.aspx?oper=checkname&time="+(new Date().getTime()),
			datatype : "json",
			success : function(d){	
				if(d.result == "1")
					return true;
				else
					return false;
			},
			buttons: $("#btnSave"),
			error: function(){alert("服务器繁忙，请稍后再试");},
			onerror : "该网站名称已经存在",
			onwait : "正在校验标题的合法性，请稍候..."
		}).DefaultPassed();
	$("#txtOrderNum").formValidator({tipid:"tipOrderNum",onshow:"请输入数字,值越大越靠前",onfocus:"请输入数字,值越大越靠前",oncorrect:"OK"}).RegexValidator({regexp:"^\([0-9]+)$",onerror:"请输入数字,值越大越靠前"});
});
//插入上传附件
function AttachmentOperater(path,type,size){
	$("#txtImg").val(path);
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
				<th> 网站名称 </th>
				<td><asp:TextBox ID="txtTitle" runat="server" MaxLength="30" CssClass="ipt"></asp:TextBox>
				<span id="tipTitle" style="width:200px;"> </span></td>
			</tr>
			<tr>
				<th> 网站地址 </th>
				<td><asp:TextBox ID="txtUrl" runat="server" Width="280px" CssClass="ipt">http://</asp:TextBox>
				</td>
			</tr>
			<tr style="display:none;">
				<th> 网站简介 </th>
				<td align="left"><asp:TextBox ID="txtInfo" runat="server" Width="280px" CssClass="ipt"></asp:TextBox></td>
			</tr>
			<tr>
				<th> 网站权重 </th>
				<td>
					<asp:TextBox ID="txtOrderNum" runat="server" MaxLength="6" Width="40px" CssClass="ipt">0</asp:TextBox>
				<span id="tipOrderNum" style="width:200px;"> </span></td>
			</tr>
			<tr style="display:none;">
				<th> 状态 </th>
				<td><asp:RadioButtonList ID="rblState" runat="server" RepeatDirection="Horizontal">
						<asp:ListItem Value="1" Selected="True">通过</asp:ListItem>
						<asp:ListItem Value="0">不通过</asp:ListItem>
					</asp:RadioButtonList>
				</td>
			</tr>
		    <tr>
			    <th> 链接类型 </th>
			    <td><asp:DropDownList ID="ddlTypeId" runat="server"> </asp:DropDownList>
			    </td>
		    </tr>
			<tr style="display:none;">
				<th> 显示风格 </th>
				<td><asp:RadioButtonList ID="rblStyle" runat="server" RepeatDirection="Horizontal">
						<asp:ListItem Value="0" Selected="True">文本</asp:ListItem>
						<asp:ListItem Value="1">图片</asp:ListItem>
					</asp:RadioButtonList> </td>
			</tr>
			<tr>
				<th> 网站LOGO </th>
				<td align="left"><asp:TextBox ID="txtImg" runat="server" Width="97%" CssClass="ipt"></asp:TextBox>
				</td>
			</tr>
		    <tr>
			    <th> 上传LOGO图 </th>
			    <td><iframe id="frm_upload" src="link_upload.aspx" width="100%" height="30" scrolling="no" frameborder="0"></iframe></td>
		    </tr>
		</table>
	<div class="buttonok">
		<asp:Button ID="btnSave" runat="server" Text="确定" CssClass="btnsubmit" />
		<input id="btnReset" type="button" value="取消" class="btncancel" onclick="parent.JumboECMS.Popup.hide();" />
	</div>
</form>
</body>
</html>
