<%@ Page Language="C#" AutoEventWireup="true" Codebehind="customer_index.aspx.cs" Inherits="JumboECMS.WebFile.User._customer_index" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-

transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   name="robots" content="noindex, nofollow" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>个人中心 - <%=site.Name1%></title>
<script type="text/javascript" src="../../_data/json/_systemcount.js"></script>
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
}
</script>
</head>
<body>
<!--#include file="include/header.htm"-->
<div id="wrap">
	<div id="main">
		<!--#include file="include/left_menu.htm"-->
		<script type="text/javascript">$('#bar-customer-head').addClass('currently');$('#bar-customer li.small').show();</script>
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
					</div>
					<div class="composer" style="float:left">
						<h3 class="index_name"><span class="u_username"></span>（<span class="u_groupname"></span>）</h3>
						<p>绑定邮箱：<span class="u_email"></span></p>
					</div>
				</div>
				<div class="clear"></div>
				<div class="nav_two">
					<ul>
						<li><a>用户等级说明</a></li>
					</ul>
					<div class="clear"></div>
				</div>
				<div class="inportant_remind">
					<ul>
						<li><span class="b">游客：</span><span>可浏览下载本站不受限制的内容。</span></li>
						<li><span class="b">普通用户：</span><span>可浏览下载本站受限制的内容。</span></li>
					</ul>
				</div>
				<div class="clear"></div>
			</div>
		</div>
		<div id="bottom"></div>
	</div>
	<div class="clear"></div>
</div>
<!--#include file="include/footer.htm"-->
</body>
</html>
