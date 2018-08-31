<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admin_edit.aspx.cs" Inherits="JumboECMS.WebFile.Admin._admin_edit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   name="robots" content="noindex, nofollow" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>编辑管理员信息</title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link type="text/css" rel="stylesheet" href="../_data/global.css" />
<link type="text/css" rel="stylesheet" href="css/common.css" />
</head>
<body>
<form id="form1" runat="server">
	<table class="formtable mrg10T">
		<tr>
			<th> 登陆名 </th>
			<td><asp:TextBox ID="txtAdminName" runat="server" Width="120px" MaxLength="20" ReadOnly="True" CssClass="ipt"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<th> 新密码 </th>
			<td><asp:TextBox ID="txtAdminPass" runat="server" TextMode="Password" Width="120px"
                    MaxLength="20" CssClass="ipt"></asp:TextBox>(留空表示不改)
			</td>
		</tr>
		<tr>
			<th> 状&nbsp; 态 </th>
			<td><asp:RadioButtonList ID="rbtnAdminState" runat="server" RepeatColumns="2">
					<Items>
						<asp:ListItem Value="1" Selected="True">启用</asp:ListItem>
						<asp:ListItem Value="0">禁用</asp:ListItem>
					</Items>
				</asp:RadioButtonList>
			</td>
		</tr>
	</table>
	<div class="buttonok">
		<asp:Button ID="btnSave" runat="server" Text="确定" CssClass="btnsubmit" />
		<input id="btnReset" type="button" value="取消" class="btncancel" OnClick="parent.JumboECMS.Popup.hide();" />
	</div>
</form>
<script type="text/javascript">_jcms_SetDialogTitle();</script>
</body>
</html>
