<%@ Page Language="C#" AutoEventWireup="True" Codebehind="login.aspx.cs" Inherits="JumboECMS.WebFile.Passport._login" %>
<!doctype html>
<!--STATUS OK-->
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta http-equiv="X-UA-Compatible" content="IE=7">
<title>用户登录 <%=site.Name1%></title>
<link href="css/<%=base.PassportTheme %>/basic.css" rel="stylesheet" type="text/css" media="screen">
<link rel="stylesheet" type="text/css" href="css/<%=base.PassportTheme %>/login.css"/>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link   type="text/css" rel="stylesheet" href="../_data/global.css" />
<script type="text/javascript">if (top!=self)top.location=self.location;</script>
<style> 
.psp-main .psp-form em.c0{display: none;}
.psp-main .psp-form span.em{display: none;}
</style>

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
<div id="wrapper" class="login-wrap">
  <div id="bd">
    <div class="psp-title">
      <h2>用户登录<span class="theme"><a class="default" href="javascript:void(0);" title="蓝色风格" onclick="JumboECMS.Cookie.set('passport_theme','default');location.reload();"></a><a class="green" href="javascript:void(0);" title="绿色风格" onclick="JumboECMS.Cookie.set('passport_theme','green');location.reload();"></a></span></h2>
    </div>
    <div class="psp-wrap">
      <div class="psp-main cls">
        <div class="psp-container">
          <div class="login-icon">
            <p class="t">还没有帐号？</p>
            <p class="reg"><a href="register.aspx" class="btn-reg">立即注册</a></p>
            <p class="t oauth-navbar">使用合作网站帐号登录</p>
            <p class="icon-list oauth-navbar"><a id="oauth_sina" href="#" class="icons icon-sina"></a><a id="oauth_tencent" href="#" class="icons icon-tencent"></a><a id="oauth_renren" href="#" class="icons icon-renren"></a><a id="oauth_baidu" href="#" class="icons icon-baidu"></a><a id="oauth_kaixin" href="#" class="icons icon-kaixin"></a></p>
          </div>
        </div>
        <div class="psp-sidebar">
          <div class="login-con">
            <form id="loginform" class="psp-form">
              <ul>
                <li><em class="c0">使用将博帐号登录</em></li>
                <li>
                  <label class="k" style="width:80px;" for="txtUserName">帐&nbsp;&nbsp;号：</label>
                  <span class="v">
                  <input type="text" name="txtUserName" id="txtUserName" class="psp-text login-width" value="" />
                  </span> </li>
                <li>
                  <label class="k" style="width:80px;" for="txtUserPass">密&nbsp;&nbsp;码：</label>
                  <span class="v">
                  <input type="password" name="txtUserPass" id="txtUserPass" class="psp-text" />
                  </span> </li>
                <li>
                  <label class="k" style="width:80px;" for="txtUserCode">验证码：</label>
                  <span class="v">
                  <input class="psp-text" type="text" style="width:80px" id="txtUserCode" name="txtUserCode" maxlength="4" size="4" style="ime-mode:disabled;" onpaste="return false" />
                  <img class="vcode-img" id="imgCode" onclick="_jcms_GetRefreshCode('imgCode',26,36);" src="" align="absmiddle" /><a href="javascript:void(0);" id="_vcode_txt" onclick="_jcms_GetRefreshCode('imgCode',26,36);return false;">换一换</a> </span> </li>
              </ul>
              <div class="btn">
                <input type="button" class="btn-login" onclick="chkLogin();" id="btnLogin" value="登录" />
                <a class="right" href="getpassword.aspx" tabindex="-1">忘记密码?</a>
                <input type="hidden" id="txtRefer" name="txtRefer" value="<%=Referer%>" />
                <input type="hidden" name="LoginType" value="7" />
              </div>
            </form>
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
	_jcms_GetRefreshCode('imgCode',26,36);
	var act = q('act');
	if(act != "logout")
		sendRequest(1);
	else
		chkLogout();

});
var gSubmitTimes = 0;
function oLogin()
{
}
function chkLogin(){
	if(gSubmitTimes>4){
		alert('请想想密码再来');
		return;
	}
	var txtUserName="";
	var uPass="";
	var uCode="";
	txtUserName=$("#txtUserName").val();
	uPass=$("#txtUserPass").val();
	uCode=$("#txtUserCode").val();
	if(txtUserName==""){
		alert('请填写用户名');
		$("#txtUserName").focus();
		return;
	}
	if(uPass==""){
		alert('请填写密码');
		$("#txtUserPass").focus();
		return;
	}
	if(uCode==""){
		alert('请填写验证码');
		$("#txtUserCode").focus();
		return;
	}
	var typeNum;
	var rbType=$("#loginform input");
	for(var i=0;i<rbType.length;i++)
	{
		if(rbType[i].checked && rbType[i].type=="radio" && rbType[i].name=="LoginType"){
			typeNum=rbType[i].value;
		}
	}
	$.ajax({
		type:		"post",
		dataType:	"html",
		url:		"ajax.aspx?oper=login&time="+(new Date().getTime()),
		data:		"name="+encodeURIComponent(txtUserName)+"&pass="+encodeURIComponent(uPass)+"&code="+encodeURIComponent(uCode)+"&type="+typeNum,
        error:		function(XmlHttpRequest,textStatus, errorThrown) { if(XmlHttpRequest.responseText.length>0){alert(XmlHttpRequest.responseText);}},
		success:	function(d){
			if (d== "ok")
				top.location.href=$("#txtRefer").val();
			else
			{
				gSubmitTimes=gSubmitTimes+1;
				alert(d);
				_jcms_GetRefreshCode('imgCode',26,36);
			}
		}
	});
}
function chkLogout(){
	top.location.href="logout.aspx";
}
function sendRequest(p){
	if(p==0)
	{
		oLogin();
		return;
	}
	$.ajax({
		url:		site.Dir+"user/ajax.aspx?time="+(new Date().getTime()),
		type:		"get",
		dataType:	"json",
		success:	function(d){
			if(d.result=="1")
				if(q('refer')!="")
					top.location.href=decodeURIComponent(q('refer'));
				else
					top.location.href=site.Dir + 'user/index.aspx';
			else
				oLogin();
		}
	});
}
</script>
</body>
</html>
