<%@ Page Language="C#" AutoEventWireup="true" Codebehind="member_password.aspx.cs" Inherits="JumboECMS.WebFile.User._member_password" %>
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
<script type="text/javascript" src="../_libs/my97datepicker4.6/WdatePicker.js"></script>
<script type="text/javascript">
$(document).ready(function(){
	ajaxBindUserData();//绑定会员数据
	$('#changepassForm').ajaxForm({
		beforeSubmit: JumboECMS.AjaxFormSubmit,
		success :function(data){
			JumboECMS.Eval(data);
		}
	}); 
	$.formValidator.initConfig({onError:function(msg){alert(msg);}});
	$("#txtOldPass").formValidator({tipid:"tipOldPass",onshow:"请输入6-14位密码",onfocus:"请输入6-14位密码",oncorrect:"OK"}).InputValidator({min:6,max:14,onerror:"密码6-14位"});
	$("#txtNewPass1").formValidator({tipid:"tipNewPass1",onshow:"请输入6-14位密码",onfocus:"请输入6-14位密码",oncorrect:"OK"}).InputValidator({min:6,max:14,onerror:"密码6-14位"});
	$("#txtNewPass2").formValidator({tipid:"tipNewPass2",onshow:"两次密码必须一致",onfocus:"两次密码必须一致",oncorrect:"OK"}).InputValidator({min:6,max:14,onerror:"密码6-14位,请确认"}).CompareValidator({desID:"txtNewPass1",operateor:"=",onerror:"两次密码不一致"});
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
</head>
<body>
<!--#include file="include/header.htm"-->
<div id="wrap">
  <div id="main">
    <!--#include file="include/left_menu.htm"-->
    <script type="text/javascript">$('#bar-member-head').addClass('currently');$('#bar-member li.small').show();</script>
    <div id="mainarea">
      <div class="nav_two">
        <ul>
          <li><a href="member_profile.aspx">基本资料</a></li>
          <li class="currently"><a href="member_password.aspx">修改密码</a></li>
          <li><a href="member_avatar.aspx">修改头像</a></li>
        </ul>
        <div class="clear"></div>
      </div>
      <div>
        <form id="changepassForm" name="form1" method="post" action="ajax.aspx?oper=ajaxChangePassword">
          <table border="0" cellspacing="4" cellpadding="4" align="center" id="studio">
            <tr>
              <td width="110" height="30" align="right">旧密码：</td>
              <td width="410"><input type="password" class="inputss" style="width:180px;" name="txtOldPass" id="txtOldPass" />
                <span id="tipOldPass" style="width: 200px"></span></td>
            </tr>
            <tr>
              <td height="30" align="right">新密码：</td>
              <td><input type="password" class="inputss" style="width:180px;" name="txtNewPass1" id="txtNewPass1" />
                <span id="tipNewPass1" style="width: 200px"></span></td>
            </tr>
            <tr>
              <td height="30" align="right">确认密码：</td>
              <td><input type="password" class="inputss" style="width:180px;" name="txtNewPass2" id="txtNewPass2" />
                <span id="tipNewPass2" style="width: 200px"></span></td>
            </tr>
            <tr>
              <td colspan="2" align="center" valign="bottom"><input type="submit" id="btnSave" value="确定修改" class="button" />
                <a href="index.aspx">取消</a></td>
            </tr>
          </table>
        </form>
        <div class="clear"></div>
      </div>
    </div>
  </div>
  <div class="clear"></div>
</div>
<!--#include file="include/footer.htm"-->
</body>
</html>
