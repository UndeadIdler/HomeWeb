<%@ Page Language="C#" AutoEventWireup="True" Codebehind="register.aspx.cs" Inherits="JumboECMS.WebFile.Passport._register" %>
<!doctype html>
<!--STATUS OK-->
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta http-equiv="X-UA-Compatible" content="IE=7">
<title>新用户注册 <%=site.Name1%></title>
<link href="css/<%=base.PassportTheme %>/basic.css" rel="stylesheet" type="text/css" media="screen">
<link rel="stylesheet" type="text/css" href="css/<%=base.PassportTheme %>/register.css"/>
<link rel="stylesheet" type="text/css" href="css/<%=base.PassportTheme %>/active.css"/>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link   type="text/css" rel="stylesheet" href="../_data/global.css" />
<script type="text/javascript" src="../_libs/my97datepicker4.6/WdatePicker.js"></script>
<style>
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
<div id="wrapper">
  <div id="bd">
    <div id="divRegStep1">
      <div class="psp-title">
        <h2>用户注册<span class="theme"><a class="default" href="javascript:void(0);" title="蓝色风格" onclick="JumboECMS.Cookie.set('passport_theme','default');location.reload();"></a><a class="green" href="javascript:void(0);" title="绿色风格" onclick="JumboECMS.Cookie.set('passport_theme','green');location.reload();"></a></span></h2>
      </div>
      <div id="reg" class="psp-wrap">
        <div class="psp-main cls">
          <div class="psp-container wi">
            <form id="registerForm" action="ajax.aspx?oper=ajaxRegister" method="post" class="psp-form">
              <ul>
                <li>
                  <label class="k" for="txtEmail">Email邮箱：</label>
                  <span class="v">
                  <input autocomplete="off" class="psp-text" style="width:180px" type="text" name="txtEmail" id="txtEmail" maxlength="60" value="" />
                  </span><em id="tipEmail"></em> </li>
                <li>
                  <label class="k" for="txtUserName">用户名：</label>
                  <span class="v">
                  <input autocomplete="off" class="psp-text" style="width:180px" type="text" name="txtUserName" id="txtUserName" maxlength="30" value=""  />
                  </span><em id="tipUserName"></em> </li>
                <li>
                  <label class="k" for="txtPass1">密&nbsp;&nbsp;码：</label>
                  <span class="v">
                  <input class="psp-text" style="width:180px" type="password" maxlength="20" name="txtPass1" id="txtPass1" value="" />
                  </span><em id="tipPass1"></em> </li>
                <li>
                  <label class="k" for="txtPass2">确认密码：</label>
                  <span class="v">
                  <input class="psp-text" style="width:180px" type="password" maxlength="20" name="txtPass2" id="txtPass2" value="" />
                  </span><em id="tipPass2"></em></li>
                <li style="display:none;">
                  <label class="k" for="">性&nbsp;&nbsp;别：</label>
                  <span class="v">
                    <span class="sns-radio"><input type="radio" class="sex" value="1" name="rblSex" checked="checked"> 男 </span>
                    <span class="sns-radio"><input type="radio" class="sex" value="2" name="rblSex"> 女 </span>
                  </span> </li>
                <li style="display:none;">
                  <label class="k" for="txtBirthday">出生日期：</label>
                  <span class="v">
                  <input class="psp-text Wdate" style="width:90px" type="text" name="txtBirthday" id="txtBirthday" onfocus="WdatePicker({isShowClear:false,readOnly:true,skin:'blue'})" readonly="readonly" value="1980-01-01" />
                  </span><em id="tipsBirthday"></em></li>
                <li>
                  <label class="k" for="txtCode">验证码：</label>
                  <span class="v">
                  <input class="psp-text" style="width:80px" type="text" id="txtCode" name="txtCode" maxlength="4" size="4" style="ime-mode:disabled;" onpaste="return false" />
                  <img class="vcode-img" id="imgCode" onclick="_jcms_GetRefreshCode('imgCode',26,36);" src="" align="absmiddle" /><a href="javascript:void(0);" id="_vcode_txt" onclick="_jcms_GetRefreshCode('imgCode',26,36);return false;">换一换</a> <em id="tipCode"></em></span> </li>
                <li style="display:none;">
                  <label class="k" for="chkAccept">接受协议：</label>
                  <span class="v">
                  <input name="chkAccept" id="chkAccept" type="checkbox" value="checkbox" checked="checked" />
                  我接受&lt;&lt;注册协议&gt;&gt; </span><em id="tipsAccept"></em></li>
                <li>
                  <input type="submit" value="立即注册" id="btn_reg_submit" class="btn-reg" />
                </li>
                <li></li>
              </ul>
            </form>
          </div>
          <div class="psp-sidebar">
            <form action="" method="get" class="psp-form">
              <ul>
                <li><em class="pl c0 lh">已有帐号？请直接登录</em></li>
                <li> <a href="login.aspx" class="btns btn-t1 signin"><span>马上登录</span></a> </li>
              </ul>
            </form>
            <div class="other-signin oauth-navbar">
              <p>使用合作网站帐号登录</p>
              <div class="other-list"> <a id="oauth_sina" href="#" class="sina">新浪微博</a> <a id="oauth_tencent" href="#" class="tencent">QQ</a> <a id="oauth_renren" href="#" class="renren">人人网</a> <a id="oauth_baidu" href="#" class="baidu">百度</a> <a id="oauth_kaixin" href="#" class="kaixin">开心网</a> </div>
            </div>
            <div class="show-box" style="display:none">
              <p></p>
              <div class="show-main"> </div>
            </div>
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
	$('#registerForm').ajaxForm({
		beforeSubmit: JumboECMS.AjaxFormSubmit,
		success :function(data){
			JumboECMS.Eval(data);
		}
	}); 
	$.formValidator.initConfig({onError:function(msg){/*alert(msg);*/}});
	$("#txtEmail")
		.formValidator({tipid:"tipEmail",onshow:"请输入常用的邮箱",onfocus:"请输入常用的邮箱"})
		.InputValidator({min:8,max:50,onerror:"邮箱非法,请确认"})
		.RegexValidator({regexp:"email",datatype:"enum",onerror:"格式不正确"})
		.AjaxValidator({
		type : "get",
		url:		"ajax.aspx?oper=checkemail&id=0&time="+(new Date().getTime()),
		datatype : "json",
		success : function(d){	
			if(d.result == "1")
				return true;
			else
				return false;
		},
		buttons: $("#btnSave"),
		error: function(){alert("服务器繁忙，请稍后再试");},
		onerror : "邮箱已被注册",
		onwait : "正在校验邮箱的合法性，请稍候..."
	});
	$("#txtUserName")
		.formValidator({tipid:"tipUserName",onfocus:"支持中英文、数字、下划线"})
		.InputValidator({min:4,max:20,onerror:"4-20个字符"})
		.RegexValidator({regexp:"username",datatype:"enum",onerror:"必须以汉字或字母开头"})
		.AjaxValidator({
		type : "get",
		url:		"ajax.aspx?oper=checkname&id=0&time="+(new Date().getTime()),
		datatype : "json",
		success : function(d){	
			if(d.result == "1")
				return true;
			else
				return false;
		},
		buttons: $("#btnSave"),
		error: function(){alert("服务器繁忙，请稍后再试");},
		onerror : "用户名已被注册",
		onwait : "正在校验用户名的合法性，请稍候..."
	});
	$("#txtPass1").formValidator({tipid:"tipPass1",onfocus:"请输入6-14位密码"}).InputValidator({min:6,max:14,onerror:"密码6-14位"});
	$("#txtPass2").formValidator({tipid:"tipPass2",onfocus:"两次密码必须一致"}).InputValidator({min:6,max:14,onerror:"密码6-14位,请确认"}).CompareValidator({desID:"txtPass1",operateor:"=",onerror:"两次密码不一致"});
	$("#txtCode").formValidator({tipid:"tipCode",onfocus:"验证码必须填写"}).InputValidator({min:4,max:4,onerror:"4位验证码"});
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
