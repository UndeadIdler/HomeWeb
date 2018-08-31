<%@ Page Language="C#" AutoEventWireup="True" Codebehind="getpassword.aspx.cs" Inherits="JumboECMS.WebFile.Passport._getpassword" %>
<!doctype html>
<!--STATUS OK-->
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta http-equiv="X-UA-Compatible" content="IE=7">
<title>找回密码 <%=site.Name1%></title>
<link href="css/<%=base.PassportTheme %>/basic.css" rel="stylesheet" type="text/css" media="screen">
<link rel="stylesheet" type="text/css" href="css/<%=base.PassportTheme %>/forgot-passwd.css"/>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link   type="text/css" rel="stylesheet" href="../_data/global.css" />
</head>
<body>
<div id="wrapper" class="">
  <div id="header">
    <div class=""></div>
    <div class="sub-global-header">
      <div class="sub-global-header-inner">
        <div class="logo"><a href="<%=site.Dir %>"><%=site.Name1 %></a></div>
        <ul class="cate-nav">
        </ul>
        <ul class="login-nav">
          <li><a href="register.aspx">注册</a>|</li>
          <li><a href="login.aspx">登陆</a>|</li>
          <li><a href="<%=site.Dir %>">返回首页</a>|</li>
          <li class="collect"><a href="javascript:void(0);" onClick="return _jcms_addFavorite('<%=site.Url %>', '<%=site.Name1 %>')">收藏本站</a></li>
        </ul>
      </div>
    </div>
  </div>
  <div id="bd" class="bd">
    <div class="psp-title">
      <h2>找回密码<span class="theme"><a class="default" href="javascript:void(0);" title="蓝色风格" onclick="JumboECMS.Cookie.set('passport_theme','default');location.reload();"></a><a class="green" href="javascript:void(0);" title="绿色风格" onclick="JumboECMS.Cookie.set('passport_theme','green');location.reload();"></a></span></h2>
    </div>
    <div class="psp-wrap">
      <div class="psp-main fp-wraper cls">
        <ul class="fp-nav">
          <li id="emailnav" class="current">用邮箱找回</li>
          <li id="mobilenav">用手机号码找回</li>
        </ul>
        <div id="lost_pass_wrapper" class="fp-main">
          <form action="ajax.aspx?oper=ajaxResetPassword" method="post" class="psp-form" id="resetPwdForm" name="lostpassForm">
            <ul>
              <li>
                <label class="k">邮　箱：</label>
                <span class="v">
                <input type="text" class="psp-text" name="txtEmail" id="txtEmail" />
                <em id="tipEmail"></em></span> </li>
              <li>
                <label class="k">用户名：</label>
                <span class="v">
                <input type="text" class="psp-text" name="txtUserName" id="txtUserName"/>
                <em id="tipUserName"></em></span> </li>
            </ul>
            <input type="submit" value="" name="submit" class="next" />
          </form>
        </div>
      </div>
      <p class="Email">忘记登录邮箱或手机号？请联系客服</p>
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
</div>
<script type="text/javascript">
$(document).ready(function(){
	$('#resetPwdForm').ajaxForm({
		beforeSubmit: JumboECMS.AjaxFormSubmit,
		success :function(data){
			JumboECMS.Eval(data);
		}
	}); 
	$.formValidator.initConfig({onError:function(msg){/*alert(msg);*/}});
	$("#txtEmail")
		.formValidator({tipid:"tipEmail",onshow:"请输入注册时的邮箱",onfocus:"请输入注册时的邮箱"})
		.InputValidator({min:8,max:50,onerror:"你输入的邮箱非法,请确认"})
		.RegexValidator({regexp:"email",datatype:"enum",onerror:"格式不正确"});
	$("#txtUserName")
		.formValidator({tipid:"tipUserName",onshow:"输入注册时的用户名",onfocus:"输入注册时的用户名"})
		.InputValidator({min:4,max:20,onerror:"你输入的用户名非法,请确认"})
		.RegexValidator({regexp:"username",datatype:"enum",onerror:"输入不正确"});
});
//Form验证
JumboECMS.AjaxFormSubmit=function(item){
	try{
		if($.formValidator.PageIsValid('1'))
		{
			JumboECMS.Loading.show("正在处理，请稍等...");
			return true;
		}else{
			return false;
		}
	}catch(e){
		return false;
	}
}
</script>
</body>
</html>
