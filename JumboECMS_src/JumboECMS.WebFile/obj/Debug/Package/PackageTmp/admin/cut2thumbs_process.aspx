<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cut2thumbs_process.aspx.cs" Inherits="JumboECMS.WebFile.Admin._cut2thumbs_process" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml" >
<head>
<title></title>
<style type="text/css">
body {margin: 0px;}
</style>
<script type="text/javascript">
function $(obj){return typeof(obj)=="object" ? "obj":document.getElementById(obj);}
</script>
</head>
<body>
<form id="form1" runat="server">
	<div>
		<asp:Label ID="Label1" runat="server" Text="x:"></asp:Label>
		<asp:TextBox ID="x" runat="server" Width="27px" CssClass="ipt">0</asp:TextBox>
		<asp:Label ID="Label2" runat="server" Text="y:"></asp:Label>
		<asp:TextBox ID="y" runat="server" Width="27px" CssClass="ipt">0</asp:TextBox>
		<asp:Label ID="Label3" runat="server" Text="w:"></asp:Label>
		<asp:TextBox ID="w" runat="server" Width="33px">120</asp:TextBox>
		<asp:Label ID="Label4" runat="server" Text="h:"></asp:Label>
		<asp:TextBox ID="h" runat="server" Width="33px">95</asp:TextBox>
		<asp:Button ID="Button1" runat="server" Text="裁减" OnClick="Button1_Click" />
		<asp:HiddenField ID="tow" runat="server" Value="0" />
		<asp:HiddenField ID="toh" runat="server" Value="0" />
		<asp:HiddenField ID="PhotoUrl" runat="server" Value="temp.jpg" />
	</div>
</form>
</body>
</html>
