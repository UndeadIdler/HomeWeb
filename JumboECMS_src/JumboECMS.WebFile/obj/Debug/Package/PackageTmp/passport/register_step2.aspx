<%@ Page Language="C#" AutoEventWireup="True" Codebehind="register_step2.aspx.cs" Inherits="JumboECMS.WebFile.Passport._register_step2" %>
<!doctype html>
<!--STATUS OK-->
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta http-equiv="X-UA-Compatible" content="IE=7">
<title>邮件激活 - 新用户注册 <%=site.Name1%></title>
<link href="css/<%=base.PassportTheme %>/basic.css" rel="stylesheet" type="text/css" media="screen">
<link rel="stylesheet" type="text/css" href="css/<%=base.PassportTheme %>/register.css"/>
<link rel="stylesheet" type="text/css" href="css/<%=base.PassportTheme %>/active.css"/>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link   type="text/css" rel="stylesheet" href="../_data/global.css" />
</head>
<body>
<div id="header">
  <div class=""></div>
  <div class="sub-global-header">
    <div class="sub-global-header-inner">
      <div class="logo"><a href="<%=site.Dir %>"><%=site.Name1 %></a></div>
      <ul class="cate-nav">
      </ul>
      <ul class="login-nav">
        <li><a href="<%=site.Dir %>">返回首页</a>|</li>
        <li class="collect"><a href="javascript:void(0);" onClick="return _jcms_addFavorite('<%=site.Url %>', '<%=site.Name1 %>')">收藏本站</a></li>
      </ul>
    </div>
  </div>
</div>
<div id="wrapper">
  <div id="bd">
    <div id="divRegStep2">
      <div class="psp-title">
        <h2>邮件激活<span class="theme"><a class="default" href="javascript:void(0);" title="蓝色风格" onclick="JumboECMS.Cookie.set('passport_theme','default');location.reload();"></a><a class="green" href="javascript:void(0);" title="绿色风格" onclick="JumboECMS.Cookie.set('passport_theme','green');location.reload();"></a></span></h2>
      </div>
      <div id="reg" class="psp-wrap">
        <div class="psp-main-active cls">
          <div id="divEMailActivate">
            <dl class="active-main">
              <dt>马上验证邮箱，激活你的帐号！</dt>
              <dd>验证信息已经发送到你的邮箱 <b class="register-username"><%=_UserName%></b></dd>
              <dd>请在48小时内点击邮件里的确认链接，激活你的帐号。</dd>
              <dd class="btn"><a href="javascript:void(0);" id="btnGoMailbox" class="btns btn-t1"><span><strong>去邮箱激活</strong></span></a></dd>
            </dl>
            <dl class="active-other">
              <form id="activeForm" name="form1" method="post" action="ajax.aspx?oper=ajaxSendMailAgain">
                <dt>没有收到验证邮件？</dt>
                <dd>请尝试到广告、垃圾邮件里找找看。</dd>
                <dd>有任何问题可以联系我们的客服 </dd>
                <dd>确认没有收到验证邮件，请
                  <input type="submit" id="Submit1" value="重新发送验证邮件" />
                </dd>
                <input type="hidden" name="txtEmail" id="txtEmail" value="<%=_Email%>" />
                <input type="hidden" name="txtUserName" id="txtUserName" value="<%=_UserName%>" />
                <input type="hidden" name="txtUserSign" id="txtUserSign" value="<%=_UserSign%>" />
              </form>
            </dl>
          </div>
        </div>
      </div>
    </div>
  </div>
</div>
<div id="footer">
  <div class="copyright">
    <div class="copyright-inner">
      
      <div class="copyright-info">
        <p> <span>Copyright &copy; 2007-2012</span> <span><%=site.Name1%>版权所有</span> <a href="http://www.miibeian.gov.cn" target="_blank"><%=site.ICP1%></a> </p>
        <p> Powered by <strong><a href="http://www.jumboecms.net/" target="_blank">JumboECMS</a></strong> <%=site.Version%></p>
      </div>
    </div>
  </div>
</div>
<script type="text/javascript">
$(document).ready(function(){
	$('#activeForm').ajaxForm({
		beforeSubmit: JumboECMS.AjaxFormSubmit,
		success :function(data){
			JumboECMS.Eval(data);
		}
	}); 
});
//Form验证
JumboECMS.AjaxFormSubmit=function(item){
	JumboECMS.Loading.show("正在处理，请稍等...");
	return true;
}
/**
 * 配置能跳转的邮箱列表
 */
var MAIL_SITE_CONF = {
	'qq.com'      : 'http://mail.qq.com',
	'baidu.com'   : 'http://email.baidu.com',
	'sohu.com'    : 'http://mail.sohu.com',
	'gmail.com'   : 'https://mail.google.com',
	'yahoo.com.cn': 'http://mail.cn.yahoo.com',
	'yahoo.cn'    : 'http://mail.cn.yahoo.com',
	'yahoo.com'   : 'https://mail.yahoo.com',
	'msn.com'     : 'https://mail.live.com',
	'live.cn'     : 'https://mail.live.com',
	'live.com'    : 'https://login.live.com',
	'foxmail.com' : 'http://mail.foxmail.com',
	'tom.com'     : 'http://mail.tom.com',
	'139.com'     : 'http://mail.10086.cn',
	'wo.com.cn'   : 'http://mail.wo.com.cn',
	'189.cn'      : 'http://webmail10.189.cn/webmail',
	'163.com'     : 'http://mail.163.com',
	'hotmail.com' : 'http://www.hotmail.com',
	'126.com'     : 'http://mail.126.com'
};
//点按钮去邮箱
$("#btnGoMailbox").click(function(){ 
	 var mailHost    = $('#txtEmail').val().split('@')[1].toLowerCase();
	 var userMailURL = MAIL_SITE_CONF[mailHost] || 'http://mail.' +mailHost;
	 if (!window.open(userMailURL)) {
	 	window.location = userMailURL;
	 }
});

</script>
</body>
</html>
