<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="slide_edit.aspx.cs" Inherits="JumboECMS.WebFile.Admin._slide_edit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>幻灯位编辑</title>
<link   type="text/css" rel="stylesheet" href="css/common.css" />
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link   type="text/css" rel="stylesheet" href="../_data/global.css" />
<script type="text/javascript">
$(document).ready(function(){
	$.formValidator.initConfig({onError:function(msg){alert(msg);}});
	$("#txtTitle").formValidator({tipid:"tipTitle",onshow:"请输入幻灯位名称",onfocus:"请输入幻灯位名称",oncorrect:"OK"}).InputValidator({min:1,onerror:"请输入幻灯位名称"});
	$("#txtWidth").formValidator({tipid:"tipWidth",onshow:"请填写数字",onfocus:"请填写数字"}).RegexValidator({regexp:"^\([0-9]+)$",onerror:"请填写数字"});
	$("#txtHeight").formValidator({tipid:"tipHeight",onshow:"请填写数字",onfocus:"请填写数字"}).RegexValidator({regexp:"^\([0-9]+)$",onerror:"请填写数字"});
	$("#txtTitle").formValidator({tipid:"tipTitle",onshow:"请输入幻灯位名称",onfocus:"请输入幻灯位名称",oncorrect:"OK"}).InputValidator({min:1,onerror:"请输入幻灯位名称"});
	$("#txtImg1").formValidator({tipid:"tipImg1",onshow:"请输入图1地址",onfocus:"请输入图1地址",oncorrect:"OK"}).InputValidator({min:1,onerror:"请输入图1地址"});
});
//插入上传附件
function AttachmentOperater(number,path,type,size){
	$("#txtImg"+number).val(path);
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
      <th> 幻灯位名称 </th>
      <td><asp:TextBox ID="txtTitle" runat="server" MaxLength="30" CssClass="ipt"></asp:TextBox>
        <span id="tipTitle" style="width:200px;"> </span></td>
    </tr>
    <tr>
      <th> 幻灯图宽度 </th>
      <td><asp:TextBox ID="txtWidth" runat="server" Width="60px" MaxLength="4" CssClass="ipt">0</asp:TextBox>
        <span id="tipWidth" style="width:80px"></span> </td>
    </tr>
    <tr>
      <th> 幻灯图高度 </th>
      <td><asp:TextBox ID="txtHeight" runat="server" Width="60px" MaxLength="4" CssClass="ipt">0</asp:TextBox>
        <span id="tipHeight" style="width:80px"></span> </td>
    </tr>
    <tr>
      <th>图1地址</th>
      <td><asp:TextBox ID="txtImg1" runat="server" MaxLength="150" Width="97%" CssClass="ipt"></asp:TextBox>
      <span id="tipImg1" style="width:80px"></span></td>
    </tr>
    <tr>
      <th> 上传图1 </th>
      <td><iframe id="frm_upload" src="slide_upload.aspx?number=1" width="100%" height="30" scrolling="no" frameborder="0"></iframe></td>
    </tr>
    <tr>
      <th> 图1链接</th>
      <td><asp:TextBox ID="txtUrl1" runat="server" MaxLength="150" Width="97%" CssClass="ipt"></asp:TextBox>
        <span id="tipUrl1" style="width:200px"></span> </td>
    </tr>
    <tr>
      <th>图2地址</th>
      <td><asp:TextBox ID="txtImg2" runat="server" MaxLength="150" Width="97%" CssClass="ipt"></asp:TextBox>
      </td>
    </tr>
    <tr>
      <th> 上传图2 </th>
      <td><iframe id="Iframe1" src="slide_upload.aspx?number=2" width="100%" height="30" scrolling="no" frameborder="0"></iframe></td>
    </tr>
    <tr>
      <th> 图2链接</th>
      <td><asp:TextBox ID="txtUrl2" runat="server" MaxLength="150" Width="97%" CssClass="ipt"></asp:TextBox>
        <span id="tipUrl2" style="width:200px"></span> </td>
    </tr>
    <tr>
      <th>图3地址</th>
      <td><asp:TextBox ID="txtImg3" runat="server" MaxLength="150" Width="97%" CssClass="ipt"></asp:TextBox>
      </td>
    </tr>
    <tr>
      <th> 上传图3 </th>
      <td><iframe id="Iframe2" src="slide_upload.aspx?number=3" width="100%" height="30" scrolling="no" frameborder="0"></iframe></td>
    </tr>
    <tr>
      <th> 图3链接</th>
      <td><asp:TextBox ID="txtUrl3" runat="server" MaxLength="150" Width="97%" CssClass="ipt"></asp:TextBox>
        <span id="tipUrl3" style="width:200px"></span> </td>
    </tr>
    <tr>
      <th>图4地址</th>
      <td><asp:TextBox ID="txtImg4" runat="server" MaxLength="150" Width="97%" CssClass="ipt"></asp:TextBox>
      </td>
    </tr>
    <tr>
      <th> 上传图4 </th>
      <td><iframe id="Iframe3" src="slide_upload.aspx?number=4" width="100%" height="30" scrolling="no" frameborder="0"></iframe></td>
    </tr>
    <tr>
      <th> 图4链接</th>
      <td><asp:TextBox ID="txtUrl4" runat="server" MaxLength="150" Width="97%" CssClass="ipt"></asp:TextBox>
        <span id="tipUrl4" style="width:200px"></span> </td>
    </tr>
    <tr>
      <th>图5地址</th>
      <td><asp:TextBox ID="txtImg5" runat="server" MaxLength="150" Width="97%" CssClass="ipt"></asp:TextBox>
      </td>
    </tr>
    <tr>
      <th> 上传图5 </th>
      <td><iframe id="Iframe4" src="slide_upload.aspx?number=5" width="100%" height="30" scrolling="no" frameborder="0"></iframe></td>
    </tr>
    <tr>
      <th> 图5链接</th>
      <td><asp:TextBox ID="txtUrl5" runat="server" MaxLength="150" Width="97%" CssClass="ipt"></asp:TextBox>
        <span id="tipUrl5" style="width:200px"></span> </td>
    </tr>
    <tr style="display:none;">
      <th> 状态 </th>
      <td><asp:RadioButtonList ID="rblState" runat="server" RepeatDirection="Horizontal">
          <asp:ListItem Value="1" Selected="True">通过</asp:ListItem>
          <asp:ListItem Value="0">不通过</asp:ListItem>
        </asp:RadioButtonList>
      </td>
    </tr>
  </table>
  <div class="buttonok">
    <asp:Button ID="btnSave" runat="server" Text="确定" CssClass="btnsubmit" />
    <input id="btnReset" type="button" value="取消" class="btncancel" onclick="parent.JumboECMS.Popup.hide();" />
  </div>
</form>
</body>
</html>
