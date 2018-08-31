<%@ Page Language="C#" AutoEventWireup="true" Codebehind="index.aspx.cs" Inherits="JumboECMS.WebFile.User._index" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   name="robots" content="noindex, nofollow" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>个人中心 - <%=site.Name1%></title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link   type="text/css" rel="stylesheet" href="../_data/global.css" />
<script type="text/javascript" src="js/fore.common.js"></script>
<link   type="text/css" rel="stylesheet" href="../statics/member/style.css" />
<script type="text/javascript">
$(document).ready(function(){
	ajaxBindUserData('BindOtherData(_jcms_UserData)');//绑定会员数据
});
function BindOtherData(data){
	$(".index_name").html(data.username+"（"+data.groupname+"）");
	if(data.sex=="2")
		$(".u_sex").text("女");
	if(data.sex=="0")
		$(".u_sex").text("保密");
	if(data.isvip=="1")
		$(".u_vipdate").text("VIP有效期："+data.vipdate);
	$(".u_birthday").text(data.birthday);
	$(".u_nickname").text(data.nickname);
	$(".u_signature").text(data.signature);
	$(".u_truename").text(data.truename);
	$(".u_provincecity").text(data.provincecity);
	if(data.idtype=="1")
		$(".u_idtype").text("身份证");
	else if(data.idtype=="2")
		$(".u_idtype").text("军官证");
	else if(data.idtype=="3")
		$(".u_idtype").text("学生证");
	else
		$(".u_idtype").text("其他证件");
	$(".u_idcard").text(data.idcard);
	$(".u_workunit").text(data.workunit);
	$(".u_address").text(data.address);
	$(".u_zipcode").text(data.zipcode);
	$(".u_mobiletel").text(data.mobiletel);
	$(".u_telephone").text(data.telephone);
	$(".u_qq").html(data.qq);
	$(".u_msn").html(data.msn);
	$(".u_homepage").html("<a href='"+data.homepage+"' target='_blank'>"+data.homepage+"</a>");

}
</script>
</head>
<body>
<!--#include file="include/header.htm"-->
<div id="wrap">
	<div id="main">
		<!--#include file="include/left_menu.htm"-->
		<script type="text/javascript">$('#bar-index-head').addClass('currently');$('#bar-index li.small').show();</script>
		<div id="mainarea">
			<div id="content">
				<div class="composer_header">
					<div style="float:left;width:137px;">
						<div class="ar_r_t">
							<div class="ar_l_t">
								<div class="ar_r_b">
									<div class="ar_l_b"><img id="u_myface_l" src="../_data/avatar/<%=UserId%>_l.jpg" onerror="this.src='../_data/avatar/0_l.jpg'" width="120" height="120" /></div>
								</div>
							</div>
						</div>
						<div style="float:left;width:137px;">
							<ul class="u_setting" >
								<li><a href="member_profile.aspx">修改资料</a></li>
								<li><a href="member_password.aspx">修改密码</a></li>
							</ul>
						</div>
					</div>
					<div class="composer" style="float:left">
						<h3 class="index_name"><span class="u_username"></span>（<span class="u_groupname"></span>）</h3>
						<p>绑定邮箱：<span class="u_email"></span></p>
					</div>
				</div>
				<div class="clear"></div>
				<!--我的概况-->
				<div class="inportant_remind">
					<h1>个人资料<span></span></h1>
					<ul>
						<li><span class="b">性别：</span><span class="u_sex">男</span></li>
						<li><span class="b">昵称：</span><span class="u_nickname"></span></li>
						<li><span class="b">真实姓名：</span><span class="u_truename"></span></li>
						<li><span class="b">出生日期：</span><span class="u_birthday"></span></li>
						<li><span class="b">所在地区：</span><span class="u_provincecity"></span></li>
						<li><span class="b">证件号码：</span><span class="u_idcard"></span></li>
						<li><span class="b">工作单位：</span><span class="u_workunit"></span></li>
						<li><span class="b">联系地址：</span><span class="u_address"></span></li>
						<li><span class="b">邮政编码：</span><span class="u_zipcode"></span></li>
						<li><span class="b">手机号码：</span><span class="u_mobiletel"></span></li>
						<li><span class="b">联系电话：</span><span class="u_telephone"></span></li>
						<li><span class="b">QQ：</span><span class="u_qq"></span></li>
						<li><span class="b">MSN：</span><span class="u_msn"></span></li>
					</ul>
				</div>
				<div class="clear"></div>
			</div>
			<!--/content-->
			<div id="sidebar">
			</div>
		</div>
	</div>
	<div class="clear"></div>
</div>
<!--#include file="include/footer.htm"-->
</body>
</html>
