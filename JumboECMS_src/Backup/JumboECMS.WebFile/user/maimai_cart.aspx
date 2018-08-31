<%@ Page Language="C#" AutoEventWireup="true" Codebehind="maimai_cart.aspx.cs" Inherits="JumboECMS.WebFile.User._maimai_cart" %>
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
<script type="text/javascript" src="../statics/member/common.js"></script>
<script type="text/javascript" src="js/fore.common.js"></script>
<link   type="text/css" rel="stylesheet" href="../statics/member/style.css" />
<script type="text/javascript">
var pagesize=50;
var page=thispage();
var TotalMoney = 0;//总价格
$(document).ready(function(){
	ajaxBindUserData('BindOtherData(_jcms_UserData)');//绑定会员数据
	$('#cartForm').ajaxForm({
		beforeSubmit: JumboECMS.AjaxFormSubmit,
		success :function(data){
			JumboECMS.Eval(data);
		}
	}); 
	$.formValidator.initConfig({onError:function(msg){}});
	$("#txtTrueName")
		.formValidator({tipid:"tipTrueName",onshow:"2-8个汉字",onfocus:"2-8个汉字",oncorrect:"OK"})
		.InputValidator({min:4,max:16,onerror:"2-8个汉字"})
		.RegexValidator({regexp:"chinese",datatype:"enum",onerror:"请输入中文"});
	$("#txtAddress")
		.formValidator({empty:true,tipid:"tipAddress",onshow:"",onfocus:"请填写详细的地址",oncorrect:"OK"})
		.InputValidator({min:10,onerror: "填的信息不够详细!"});
	$("#txtZipCode").formValidator({tipid:"tipZipCode",onshow:"请输入邮编",onfocus:"请输入邮编,可以为空",oncorrect:"OK"}).InputValidator({min:6,max:6,onerror:"邮政编码为6位,请确认"}).RegexValidator({regexp:"zipcode",datatype:"enum",onerror:"格式不正确"});
	$("#txtMobileTel").formValidator({tipid:"tipMobileTel",onshow:"请输入你的手机号码",onfocus:"请输入你的手机号码",oncorrect:"OK"}).InputValidator({min:11,max:11,onerror:"必须是11位的,请确认"}).RegexValidator({regexp:"mobile",datatype:"enum",onerror:"不是正确的手机号码"});
	$("#chkAccept").formValidator({tipid:"tipAccept",onshow:"",onfocus:"请确认是否要提交",oncorrect:"OK"}).InputValidator({min:1,onerror:"请确认是否要提交"});
});
function BindOtherData(data){
	$("#txtTrueName").val(data.truename);
	$("#txtAddress").val(data.provincecity.replace(/\-/g, ' ') + ' '+ data.address);
	$("#txtZipCode").val(data.zipcode);
	$("#txtMobileTel").val(data.mobiletel);
	$("#txtTelephone").val(data.telephone);
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
};
$(document).ready(function() {
	ajaxList(page);
});
function ajaxList(currentpage)
{
	if(currentpage!=null) page=currentpage;
	TotalMoney = 0;//初始为0
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"page="+currentpage+"&pagesize="+pagesize+"&time="+(new Date().getTime()),
		url:		"ajax.aspx?oper=ajaxGetCartList",
		error:		function(XmlHttpRequest,textStatus, errorThrown){if(XmlHttpRequest.responseText!=""){if(XmlHttpRequest.responseText.length>0){alert(XmlHttpRequest.responseText);}}},
		success:	function(d){
			switch (d.result)
			{
			case '0':
				JumboECMS.Alert(d.returnval, "0");
				break;
			case '1':
				if(d.table.length == 0)
				{
					$("#message_box").show();
					$("#mainarea").hide();
				}
				else
				{
					$("#message_box").hide();
					$("#mainarea").show();
					$("#ajaxList").setTemplateElement("tplList", null, {filter_data: false});
					$("#ajaxList").processTemplate(d);
					for(var i=0;i!=d.table.length;i++)
					{
						TotalMoney+=parseFloat(d.table[i].totalprice);
					}
					$("#ajaxTotal1").text(d.table.length);
					$("#ajaxTotal2").text(formatCurrency(TotalMoney));
				}
				break;
			}
		}
	});
}
function ConfirmDelete(id){
	JumboECMS.Confirm("确定要从购物车里移除该商品吗?", "ajaxDelete("+id+")");
}

function ajaxDelete(id)
{
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"id="+id,
		url:		"ajax.aspx?oper=ajaxDeleteCart&time="+(new Date().getTime()),
		success:	function(d){
			switch (d.result)
			{
			case '0':
				JumboECMS.Alert(d.returnval, "0");
				break;
			case '1':
				ajaxList(page);
				break;
			}
		}
	});
}
function ajaxSetBuyCount(productid,buycount){
	if(buycount<1)return;
	if(buycount><%=site.ProductMaxBuyCount%>)return;
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"productid="+productid+"&buycount="+buycount,
		url:		"ajax.aspx?oper=ajaxSetBuyCount&time="+(new Date().getTime()),
		error:		function(XmlHttpRequest,textStatus, errorThrown){if(XmlHttpRequest.responseText.length>0){alert(XmlHttpRequest.responseText);} },
		success:	function(d){
			switch (d.result)
			{
			case '0':
				JumboECMS.Alert(d.returnval, "0");
				break;
			case '1':
				ajaxList(page);
				break;
			}
		}
	});
}

</script>
</head>
<body>
<!--#include file="include/header.htm"-->
<div id="wrap">
  <div id="main">
    <!--#include file="include/left_menu.htm"-->
    <script type="text/javascript">$('#bar-maimai-head').addClass('currently');$('#bar-maimai li.small').show();</script>
	<div id="message_box" style="display:none;">
		<dl class="message">
			<dt><span class="tl"></span><span class="tr"></span>友情提醒</dt>
			<dd><span class="em">您的购物车里暂无任何商品</span><br>
				您还是先去逛逛吧。<br><br>
				<br>
			</dd>
			<dd class="message_b"><span class="bl"></span><span class="br"></span></dd>
		</dl>
	</div>
    <div id="mainarea" style="display:none;">
      <table class="helptable mrg10T">
        <tr>
          <td><ul>
              <li>每样商品最多只能购买<span class="red b"><%=site.ProductMaxBuyCount %></span>件。</li>
              <li>订单一旦提交后就无法修改，且24小时内无法删除，请慎重操作。</li>
            </ul></td>
        </tr>
      </table>
      <!--二级菜单-->
      <div class="nav_two">
        <ul>
          <li class="currently"><a>购物车</a></li>
        </ul>
        <div class="clear"></div>
      </div>
      <!--右侧列表-->
      <div id="list2">
        <textarea id="tplList" style="display:none">
        <table width="100%" cellpadding="0" cellspacing="0">
	<tr>
		<th width="*" align="left">商品信息</th>
		<th scope="col" style="width:80px;">单价(元)</th>
		<th scope="col" style="width:120px;">数量</th>
		<th scope="col" style="width:100px;">小计(元)</th>
		<th scope="col" style="width:30px;">操作</th>
	</tr>
	{#foreach $T.table as record}
	<tr>
		<td align="left">
			<div style="float:left;margin-left:15px" class="photo48"><a href="{$T.record.productlink}" target="_blank" title="{$T.record.productname}"><img src="{$T.record.productimg}" onerror="this.src=site.Dir+'statics/common/nophoto.jpg'" alt="{$T.record.productname}" width="48" height="48" /></a></div>
			<div style="float:left;margin-left:10px;" ><a href="{$T.record.productlink}" target="_blank">{$T.record.productname}</a></div>
		</td>
		<td align="center">{formatCurrency($T.record.unitprice)}</td>
		<td align="center"><a href="javascript:void(0)" onclick="ajaxSetBuyCount({$T.record.productid},{$T.record.buycount}-1)">-</a> {$T.record.buycount} <a style="margin-left:5px" href="javascript:void(0)" onclick="ajaxSetBuyCount({$T.record.productid},{$T.record.buycount}+1)">+</a>
</td>
		<td align="center">{formatCurrency($T.record.totalprice)}</td>
		<td align="center">
           		<a href="javascript:void(0);" onclick="ConfirmDelete({$T.record.id})">删除</a>
		</td>
	</tr>
	{#/for}
</table></textarea>
        <div id="ajaxList"></div>
        <div class="clear"></div>
        <table style="width:780px;margin:20px;font-size:16px;font-weight:bold;">
          <tr>
            <td><span class="left">商品总计:<span id="ajaxTotal2" class="u_money">0</span> 元</span></td>
          </tr>
        </table>
        <div class="clear"></div>
      </div>
      <div class="nav_two">
        <ul>
          <li class="currently"><a>收货信息</a></li>
        </ul>
        <div class="clear"></div>
      </div>
      <div>
        <form id="cartForm" name="form1" method="post" action="ajax.aspx?oper=ajaxSetCart2Order">

          <table border="0" cellspacing="4" cellpadding="4" align="center" id="studio" width="700">
            <tr>
              <td height="30" align="right" width="120">收货人：</td>
              <td width="580"><input type="text" class="inputss" style="width:120px;" name="txtTrueName" id="txtTrueName" />
                <span id="tipTrueName" style="width: 200px"></span></td>
            </tr>
            <tr>
              <td height="30" align="right">收货地址：</td>
              <td><input type="text" class="inputss" style="width:320px;" name="txtAddress" id="txtAddress" />
                <span id="tipAddress" style="width: 200px"></span></td>
            </tr>
            <tr>
              <td height="30" align="right">邮政编码：</td>
              <td><input type="text" class="inputss" style="width:90px;" name="txtZipCode" id="txtZipCode" />
                <span id="tipZipCode" style="width: 200px"></span></td>
            </tr>
            <tr>
              <td height="30" align="right">手机号码：</td>
              <td><input type="text" class="inputss" style="width:180px;" name="txtMobileTel" id="txtMobileTel" />
                <span id="tipMobileTel" style="width: 200px"></span></td>
            </tr>
            <tr>
              <td height="30" align="right">我已经确认：</td>
              <td><input name="chkAccept" id="chkAccept" type="checkbox" value="checkbox" />
                <span id="tipAccept" style="width: 200px"></span></td>
            </tr>
            <tr>
              <td colspan="2" align="center" valign="bottom"><input type="submit" id="btnSave" value="提交订单" class="button" />
              </td>
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
