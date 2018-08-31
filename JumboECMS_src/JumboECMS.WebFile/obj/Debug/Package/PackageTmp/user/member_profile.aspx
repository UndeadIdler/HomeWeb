<%@ Page Language="C#" AutoEventWireup="true" Codebehind="member_profile.aspx.cs" Inherits="JumboECMS.WebFile.User._member_profile" %>
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
	ajaxBindUserData('BindOtherData(_jcms_UserData)');//绑定会员数据
	$('#member_profileForm').ajaxForm({
		beforeSubmit: JumboECMS.AjaxFormSubmit,
		success :function(data){
			JumboECMS.Eval(data);
		}
	}); 
	$.formValidator.initConfig({onError:function(msg){alert(msg);}});
	$("#txtNickName")
		.formValidator({tipid:"tipNickName",onshow:"2-6个汉字",onfocus:"2-6个汉字",oncorrect:"OK"})
		.InputValidator({min:4,max:12,onerror:"2-6个汉字"})
		.RegexValidator({regexp:"chinese",datatype:"enum",onerror:"咱中国人就用纯的中文呗"});
	$("#txtTrueName")
		.formValidator({tipid:"tipTrueName",onshow:"2-6个汉字",onfocus:"2-6个汉字",oncorrect:"OK"})
		.InputValidator({min:4,max:12,onerror:"2-6个汉字"})
		.RegexValidator({regexp:"chinese",datatype:"enum",onerror:"咱中国人就用纯的中文呗"});
	$("#txtIDCard")
		.formValidator({empty:true,tipid:"tipIDCard",onshow:"认真填写，填完不可修改",onfocus:"认真填写，填完不可修改",oncorrect:"OK"})
		.RegexValidator({regexp:"^([0-9a-zA-Z]{6,30})$",onerror:"6-30位数字,部分含有字母"});
	$("#txtWorkUnit")
		.formValidator({empty:true,tipid:"tipWorkUnit",onshow:"",onfocus:"比如：远洋科技",oncorrect:"OK"})
		.InputValidator({min:1,onerror: "必填项!"});
	$("#txtAddress")
		.formValidator({empty:true,tipid:"tipAddress",onshow:"",onfocus:"比如：北京市某某区某某路",oncorrect:"OK"})
		.InputValidator({min:1,onerror: "必填项!"});
	$("#txtZipCode").formValidator({empty:true,tipid:"tipZipCode",onshow:"请输入邮编,可以为空",onfocus:"请输入邮编,可以为空",oncorrect:"OK"}).InputValidator({min:6,max:6,onerror:"邮政编码为6位,请确认"}).RegexValidator({regexp:"zipcode",datatype:"enum",onerror:"格式不正确"});
	$("#txtMobileTel").formValidator({empty:true,tipid:"tipMobileTel",onshow:"请输入你的手机号码",onfocus:"要填必须填正确",oncorrect:"OK"}).InputValidator({min:11,max:11,onerror:"必须是11位的,请确认"}).RegexValidator({regexp:"mobile",datatype:"enum",onerror:"不是正确的手机号码"});
	$("#txtTelephone").formValidator({empty:true,tipid:"tipTelephone",onshow:"请输入你的联系电话",onfocus:"要填必须填正确",oncorrect:"OK",onempty:"真的不想留电话了吗？"}).RegexValidator({regexp:"^[[0-9]{3}-|\[0-9]{4}-]?([0-9]{8}|[0-9]{7})?$",onerror:"格式例如：010-88666688"});
	$("#txtQQ").formValidator({empty:true,tipid:"tipQQ",onshow:"请输入你的QQ号,可以为空",onfocus:"请输入你的QQ号,可以为空",oncorrect:"OK",onempty:"你真的不想留QQ吗？"}).InputValidator({min:5,max:14,onerror:"位数不正确,请确认"}).RegexValidator({regexp:"qq",datatype:"enum",onerror:"格式不正确"});
	$("#txtMSN").formValidator({empty:true,tipid:"tipMSN",onshow:"请输入你的MSN帐号,可以为空",onfocus:"请输入你的MSN帐号,可以为空",oncorrect:"OK",onempty:"你真的不想留MSN吗？"}).RegexValidator({regexp:"email",datatype:"enum",onerror:"格式不正确"});
});
function BindOtherData(data){
	if(data.idcard =="")
		$(".iddisplay").show();
	$("input[name=rblSex][value="+data.sex+"]").attr("checked",true);
	$("#txtBirthday").val(data.birthday);
	$("#txtNickName").val(data.nickname);
	$("#txtTrueName").val(data.truename);
	$("#txtSignature").val(data.signature);
	$("#ddlIDType").attr("value",data.idtype);
	$("#txtIDCard").val(data.idcard);
	var _provincecity = data.provincecity + '-未知';
	$('#divProvinceCity').provincecity({defProvinceVal: _provincecity.split('-')[0],defCityVal: _provincecity.split('-')[1]});
	$("#txtWorkUnit").val(data.workunit);
	$("#txtAddress").val(data.address);
	$("#txtZipCode").val(data.zipcode);
	$("#txtMobileTel").val(data.mobiletel);
	$("#txtTelephone").val(data.telephone);
	$("#txtQQ").val(data.qq);
	$("#txtMSN").val(data.msn);
	$("#txtHomePage").val(data.homepage);
}
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
          <li class="currently"><a href="member_profile.aspx">基本资料</a></li>
          <li><a href="member_password.aspx">修改密码</a></li>
          <li><a href="member_avatar.aspx">修改头像</a></li>
        </ul>
        <div class="clear"></div>
      </div>
      <div>
        <form id="member_profileForm" name="form1" method="post" action="ajax.aspx?oper=ajaxChangeProfile">
          <table border="0" cellspacing="4" cellpadding="4" align="center" id="studio">
            <tr>
              <td width="120" height="30" align="right">性别：</td>
              <td width="410"><input type="radio" name="rblSex" value="1">
                男&nbsp;
                <input type="radio" name="rblSex" value="2">
                女&nbsp;
                <input type="radio" name="rblSex" value="0" checked>
                保密</td>
            </tr>
            <tr>
              <td height="30" align="right">昵称：</td>
              <td><input type="text" class="inputss" style="width:120px;" name="txtNickName" id="txtNickName" />
                <span id="tipNickName" style="width: 200px"></span></td>
            </tr>
            <tr>
              <td height="30" align="right">个性签名：</td>
              <td><input type="text" class="inputss" style="width:200px;" name="txtSignature" id="txtSignature" maxlength="20" /></td>
            </tr>
            <tr>
              <td height="30" align="right">姓名：</td>
              <td><input type="text" class="inputss" style="width:120px;" name="txtTrueName" id="txtTrueName" />
                <span id="tipTrueName" style="width: 200px"></span></td>
            </tr>
            <tr>
              <td height="30" align="right">出生日期：</td>
              <td><input name="txtBirthday" type="text" style="width:90px;" value="1980-01-01" maxlength="20" id="txtBirthday" class="Wdate input" onfocus="WdatePicker({isShowClear:false,readOnly:true,skin:'blue'})" readonly="readonly" /></td>
            </tr>
            <tr class="iddisplay" style="display:none">
              <td height="30" align="right">证件类型：</td>
              <td><select name="ddlIDType" id="ddlIDType">
                  <option value="1" selected="selected">身份证</option>
                  <option value="2">军官证</option>
                  <option value="3">学生证</option>
                  <option value="4">其他证件</option>
                </select></td>
            </tr>
            <tr class="iddisplay" style="display:none">
              <td height="30" align="right">证件号码：</td>
              <td><input type="text" class="inputss" style="width:180px;" name="txtIDCard" id="txtIDCard" />
                <span id="tipIDCard" style="width: 200px"></span></td>
            </tr>
            <tr>
              <td height="30" align="right">所在地区：</td>
              <td id="divProvinceCity"><select id="selProvince" name="selProvince" style="width:100px;">
                </select>
                <select id="selCity" name="selCity" style="width:100px;">
                </select></td>
            </tr>
            <tr>
              <td height="30" align="right">工作单位：</td>
              <td><input type="text" class="inputss" style="width:180px;" name="txtWorkUnit" id="txtWorkUnit" />
                <span id="tipWorkUnit" style="width: 200px"></span></td>
            </tr>
            <tr>
              <td height="30" align="right">联系地址：</td>
              <td><input type="text" class="inputss" style="width:180px;" name="txtAddress" id="txtAddress" />
                <span id="tipAddress" style="width: 200px"></span></td>
            </tr>
            <tr>
              <td height="30" align="right">邮政编码：</td>
              <td><input type="text" class="inputss" style="width:180px;" name="txtZipCode" id="txtZipCode" />
                <span id="tipZipCode" style="width: 200px"></span></td>
            </tr>
            <tr>
              <td height="30" align="right">手机号码：</td>
              <td><input type="text" class="inputss" style="width:180px;" name="txtMobileTel" id="txtMobileTel" />
                <span id="tipMobileTel" style="width: 200px"></span></td>
            </tr>
            <tr>
              <td height="30" align="right">联系电话：</td>
              <td><input type="text" class="inputss" style="width:180px;" name="txtTelephone" id="txtTelephone" />
                <span id="tipTelephone" style="width: 200px"></span></td>
            </tr>
            <tr>
              <td height="30" align="right">QQ：</td>
              <td><input type="text" class="inputss" style="width:180px;" name="txtQQ" id="txtQQ" />
                <span id="tipQQ" style="width: 200px"></span></td>
            </tr>
            <tr>
              <td height="30" align="right">MSN：</td>
              <td><input type="text" class="inputss" style="width:180px;" name="txtMSN" id="txtMSN" />
                <span id="tipMSN" style="width: 200px"></span></td>
            </tr>
            <tr>
              <td height="30" align="right">个人网站：</td>
              <td><input type="text" class="inputss" style="width:200px;" name="txtHomepage" maxlength="100" id="txtHomePage" /></td>
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
