<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="feedback_config.aspx.cs" Inherits="JumboECMS.WebFile.Admin._feedback_config" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   name="robots" content="noindex, nofollow" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>参数设置</title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link type="text/css" rel="stylesheet" href="../_data/global.css" />
<link   type="text/css" rel="stylesheet" href="css/common.css" />
<script type="text/javascript">
$(document).ready(function(){
	$.formValidator.initConfig({onError:function(msg){alert(msg);}});
	$("#txtPostTimer").formValidator({tipid:"tipPostTimer",onshow:"请填写数字",onfocus:"请填写数字"}).RegexValidator({regexp:"^\([0-9]+)$",onerror:"请填写数字"});
    $("#txtPageSize").formValidator({tipid:"tipPageSize",onshow:"请填写数字",onfocus:"请填写数字"}).RegexValidator({regexp:"^\([0-9]+)$",onerror:"请填写数字"});

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
  <table class="formtable mrg10T">
    <tr>
      <th> 游客可以留言 </th>
      <td><asp:RadioButtonList ID="rblGuestPost" runat="server" RepeatDirection="Horizontal">
          <asp:ListItem Value="0" Selected="True">否</asp:ListItem>
          <asp:ListItem Value="1">是</asp:ListItem>
        </asp:RadioButtonList></td>
    </tr>
    <tr>
      <th> 需要管理员审核 </th>
      <td><asp:RadioButtonList ID="rblNeedCheck" runat="server" RepeatDirection="Horizontal">
          <asp:ListItem Value="0">否</asp:ListItem>
          <asp:ListItem Value="1" Selected="True">是</asp:ListItem>
        </asp:RadioButtonList></td>
    </tr>
    <tr>
      <th> 留言间隔周期 </th>
      <td><asp:TextBox ID="txtPostTimer" runat="server" Width="60px" CssClass="ipt"></asp:TextBox>
        秒 <span id="tipPostTimer" style="width:200px;"> </span></td>
    </tr>
    <tr>
      <th> 前台每页留言数 </th>
      <td><asp:TextBox ID="txtPageSize" runat="server" Width="60px" CssClass="ipt"></asp:TextBox>
        条 <span id="tipPageSize" style="width:200px;"> </span></td>
    </tr>
  </table>
  <div class="buttonok">
    <asp:Button ID="Button1" runat="server"
                    Text="保存" CssClass="btnsubmit" OnClick="Button1_Click" />
  </div>
</form>
<script type="text/javascript">_jcms_SetDialogTitle();</script>
</body>
</html>
