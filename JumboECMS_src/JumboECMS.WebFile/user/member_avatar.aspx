<%@ Page Language="C#" AutoEventWireup="true" Codebehind="member_avatar.aspx.cs" Inherits="JumboECMS.WebFile.User._member_avatar" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   name="robots" content="noindex, nofollow" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>个人中心 - <%=site.Name1%></title>
<script type="text/javascript" src="../_libs/swfobject.js"></script>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link   type="text/css" rel="stylesheet" href="../_data/global.css" />
<script type="text/javascript" src="js/fore.common.js"></script>
<link   type="text/css" rel="stylesheet" href="../statics/member/style.css" />
</head>
<body>
<script type="text/javascript">
$(document).ready(function(){
	ajaxBindUserData();//绑定会员数据
});
function refreshPhoto() {
	$("#u_myface_m").attr("src","../_data/avatar/<%=UserId%>_m.jpg?" + (new Date().getTime()));
	$("#u_myface_l").attr("src","../_data/avatar/<%=UserId%>_l.jpg?" + (new Date().getTime()));
}
function save_success(data) {
	if(data=="ok") {
		//showAvatarEditor();
		//refreshPhoto();
		alert('制作成功');
		window.location.reload();
	}
	else
		alert(data);
}
</script>
<!--#include file="include/header.htm"-->
<div id="wrap">
  <div id="main">
    <!--#include file="include/left_menu.htm"-->
    <script type="text/javascript">$('#bar-member-head').addClass('currently');$('#bar-member li.small').show();</script>
    <div id="mainarea">
      <div class="nav_two">
        <ul>
          <li><a href="member_profile.aspx">基本资料</a></li>
          <li><a href="member_password.aspx">修改密码</a></li>
          <li class="currently"><a href="member_avatar.aspx">修改头像</a></li>
        </ul>
        <div class="clear"></div>
      </div>
      <div>
        <table style="width:100%;" border="0" cellspacing="4" cellpadding="4" id="studio">
          <tr>
            <td width="135" align="center" valign="top"><div class="ar_r_t">
                <div class="ar_l_t">
                  <div class="ar_r_b">
                    <div class="ar_l_b"><img id="u_myface_l" src="" onerror="this.src='../_data/avatar/<%=UserId%>_l.jpg'" width="120" height="120" /></div>
                  </div>
                </div>
              </div>
              <br />
              <div style="width:100%;text-align: center;">
              <span>当前形象</span></td>
            <td width="20"></td>
            <td height="360"><div id="swfDiv"></div>
              <script type="text/javascript">
function showAvatarEditor(){
	var flashvars = {};
	flashvars.ServiceUrl = "<%=ServiceUrl%>";
	flashvars.UserId = "<%=UserId%>";
	flashvars.UserSign = "<%=UserSign%>";
	flashvars.FileFilter = "<%=FileFilter%>";
	flashvars.MaxSize = "<%=MaxSize%>";
	var params = {};
	params.quality = "high";
	params.bgcolor = "#ffffff";
	params.allowScriptAccess = "sameDomain";
	params.allowfullscreen = "true";
	var attributes = {};
	attributes.id="AvatarFlexApp";
	attributes.name="AvatarFlexApp";
	swfobject.embedSWF("../statics/flex3/user/AvatarEditor.swf", "swfDiv", "440", "340", "9.0.0", "../statics/flex3/expressInstall.swf", flashvars, params, attributes); 
}
showAvatarEditor();
</script>
            </td>
          </tr>
        </table>
        <div class="clear"></div>
      </div>
    </div>
  </div>
  <div class="clear"></div>
</div>
<!--#include file="include/footer.htm"-->
</body>
</html>
