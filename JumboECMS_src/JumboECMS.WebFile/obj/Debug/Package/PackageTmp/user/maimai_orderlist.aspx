<%@ Page Language="C#" AutoEventWireup="true" Codebehind="maimai_orderlist.aspx.cs" Inherits="JumboECMS.WebFile.User._maimai_orderlist" %>
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
var sdate = joinValue('sdate');
var pagesize=5;
var page=thispage();
$(document).ready(function(){
	ajaxBindUserData();//绑定会员数据
	aLoad('');
});
function aLoad(s) {
	page = "1";
	if(s == "1w")
		sdate = "&sdate=1w";
	else if(s == "1m")
		sdate = "&sdate=1m";
	else if(s == "1y")
		sdate = "&sdate=1y";
	else
		sdate = "";
	ajaxList(page);
}
function ajaxList(currentpage)
{
	//设置选项卡
	for(i=1; i<5; i++)
	{
		$i("tab"+i).className = "";
        
	}
	if(sdate == "&sdate=1w")
		$i("tab1").className = "currently";
	else if(sdate == "&sdate=1m")
		$i("tab2").className = "currently";
	else if(sdate == "&sdate=1y")
		$i("tab3").className = "currently";
	else
		$i("tab4").className = "currently";
	if(currentpage!=null) page=currentpage;
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"page="+currentpage+"&pagesize="+pagesize+"&time="+(new Date().getTime()),
		url:		"ajax.aspx?oper=ajaxGetOrderList"+sdate,
		error:		function(XmlHttpRequest,textStatus, errorThrown){if(XmlHttpRequest.responseText!=""){if(XmlHttpRequest.responseText.length>0){alert(XmlHttpRequest.responseText);}}},
		success:	function(d){
			switch (d.result)
			{
			case '0':
				JumboECMS.Alert(d.returnval, "0");
				break;
			case '1':
				$("#ajaxList").setTemplateElement("tplList", null, {filter_data: false});
				$("#ajaxList").processTemplate(d);
				$("#ajaxPageBar").html(d.pagerbar);
				break;
			}
		}
	});
}
function ajaxDelete(ordernum)
{
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"ordernum="+ordernum,
		url:		"ajax.aspx?oper=ajaxDeleteOrder&time="+(new Date().getTime()),
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
function ajaxPay(ordernum)
{
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"ordernum="+ordernum,
		url:		"ajax.aspx?oper=ajaxPayOrder&time="+(new Date().getTime()),
		success:	function(d){
			switch (d.result)
			{
			case '0':
				JumboECMS.Alert(d.returnval, "0");
				break;
			case '1':
				ajaxBindUserData();//扣钱了，刷新一下
				ajaxList(page);
				JumboECMS.Message(d.returnval, "1");
				break;
			}
		}
	});
}
function ajaxFinish(ordernum)
{
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"ordernum="+ordernum,
		url:		"ajax.aspx?oper=ajaxFinishOrder&time="+(new Date().getTime()),
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
function ajaxGetGoodsList(ordernum)
{
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"ordernum="+ordernum+"&time="+(new Date().getTime()),
		url:		"ajax.aspx?oper=ajaxGetGoodsList",
		error:		function(XmlHttpRequest,textStatus, errorThrown){if(XmlHttpRequest.responseText!=""){if(XmlHttpRequest.responseText.length>0){alert(XmlHttpRequest.responseText);}}},
		success:	function(d){
			switch (d.result)
			{
			case '0':
				JumboECMS.Alert(d.returnval, "0");
				break;
			case '1':
				$("#ajaxGetGoods_"+ordernum).setTemplateElement("tplList2", null, {filter_data: false});
				$("#ajaxGetGoods_"+ordernum).processTemplate(d);
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
    <div id="mainarea">
      <table class="helptable mrg10T">
        <tr>
          <td><ul>
              <li>您可以选择网银支付支付或通过邮局汇款，之后请联系客服</li>
              <li>订单24小时内无法删除，当未支付的订单达到<span class="red b"><%=site.ProductMaxOrderCount %></span>个，就不能再下新的订单</li>
            </ul></td>
        </tr>
      </table>
      <!--二级菜单-->
      <div class="nav_two">
        <ul>
          <li class="currently"><a>我的订单</a></li>
          <li><a href="maimai_cart.aspx"><span>购物车</span></a></li>
        </ul>
        <div class="clear"></div>
      </div>
      <!--三级菜单-->
      <div id="nav_box">
        <ul class="nav_three">
          <li id="tab4"><a href="javascript:;" onclick="aLoad('')">全部</a></li>
          <li id="tab3"><a href="javascript:;" onclick="aLoad('1y')">今年</a></li>
          <li id="tab2"><a href="javascript:;" onclick="aLoad('1m')">当月</a></li>
          <li id="tab1" style="display:none"><a href="javascript:;" onclick="aLoad('1w')">本周</a></li>
        </ul>
        <div class="clear"></div>
      </div>
      <!--右侧列表-->
      <div id="list">
        <textarea id="tplList2" style="display:none">
        <table width="100%" cellpadding="0" cellspacing="0">
	{#foreach $T.table as record}
	<tr class="noborder">
		<td align="left">
			<div style="float:left;margin-left:15px" class="photo48"><a href="{$T.record.productlink}" target="_blank" title="{$T.record.productname}"><img src="{$T.record.productimg}" onerror="this.src=site.Dir+'statics/common/nophoto.jpg'" alt="{$T.record.productname}" width="48" height="48" /></a></div>
			<div style="float:left;margin-left:10px;" ><a href="{$T.record.productlink}" target="_blank">{$T.record.productname}</a></div>
		</td>
		<td style="width:80px;" align="center">{formatCurrency($T.record.unitprice)}</td>
		<td style="width:60px;" align="center">{$T.record.buycount}</td>
	</tr>
	{#/for}
</table></textarea>
        <textarea id="tplList" style="display:none">
        <table width="680" cellpadding="0" cellspacing="0">
	<tr>
		<td width="300" align="left">商品信息</td>
		<td style="width:80px;" align="center">单价(元)</td>
		<td style="width:60px;" align="center">数量</td>
		<td style="width:100px;" align="center">合计(元)</td>
		<td style="width:80px;" align="center">交易状态</td>
		<td style="width:60px;" align="center">交易操作</td>
	</tr>
	{#foreach $T.table as record}
	<tr>
		<td colspan="6">&nbsp;订单号:{$T.record.ordernum}&nbsp;&nbsp;</td>
	</tr>
	<tr>
		<td colspan="3" width="*"><div id="ajaxGetGoods_{$T.record.ordernum}"><img src='../statics/loading.gif' /></div><script type="text/javascript">ajaxGetGoodsList('{$T.record.ordernum}');</script></td>
		<td align="center"><span style="font-weight:bold;font-size:12px">{formatCurrency($T.record.money)}</span></td>
		<td align="center">
          		{#if $T.record.state == "0"}<font color="red">未付款</font>{#/if}
           		{#if $T.record.state == "1"}<font color="green">已付款</font>{#/if}
           		{#if $T.record.state == "2"}<font color="blue">已发货</font>{#/if}
           		{#if $T.record.state == "3"}<font color="black">已成交</font>{#/if}

		</td>
		<td align="center">
			{#if $T.record.state == "0"}
			<a href="javascript:void(0);" onclick="JumboECMS.Confirm('确定要作废该订单吗?', 'ajaxDelete(\'{$T.record.ordernum}\')');">作废订单</a>
			{#else}
			<font color='#cccccc'>作废订单</font>
			{#/if}
			<br />
			{#if $T.record.state == "2"}
			<a href="javascript:void(0);" onclick="JumboECMS.Confirm('确定已收到商品了吗?', 'ajaxFinish(\'{$T.record.ordernum}\')');">确认收货</a>
			{#else}
			<font color='#cccccc'>确认收货</font>
			{#/if}
		</td>
	</tr>
	{#/for}
</table></textarea>
        <div id="ajaxList"></div>
        <div id="ajaxPageBar" class="pages"> </div>
        <div class="clear"></div>
      </div>
      <div class="clear"></div>
    </div>
  </div>
  <div class="clear"></div>
</div>
<!--#include file="include/footer.htm"-->
</body>
</html>
