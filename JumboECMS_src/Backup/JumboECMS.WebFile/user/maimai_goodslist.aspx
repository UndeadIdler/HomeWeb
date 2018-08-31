<%@ Page Language="C#" AutoEventWireup="true" Codebehind="maimai_goodslist.aspx.cs" Inherits="JumboECMS.WebFile.User._maimai_goodslist" %>
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
var mode = joinValue('mode');
var pagesize=5;
var page=thispage();
$(document).ready(function(){
	ajaxBindUserData();//绑定会员数据
	if(q("mode")=="")
		aLoad('');
	else
		aLoad(q("mode"));
});
function aLoad(s) {
	page = "1";
	if(s == "new")
		mode = "&mode=new";
	else if(s == "old")
		mode = "&mode=old";
	else
		mode = "";
	ajaxList(page);
}

function ajaxList(currentpage)
{
	//设置选项卡
	for(i=1; i<4; i++)
	{
		$i("tab"+i).className = "";
        
	}
	if(mode == "&mode=new")
		$i("tab1").className = "currently";
	else if(mode == "&mode=old")
		$i("tab2").className = "currently";
	else
		$i("tab3").className = "currently";

	if(currentpage!=null) page=currentpage;
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"page="+currentpage+"&pagesize="+pagesize+"&time="+(new Date().getTime()),
		url:		"ajax.aspx?oper=ajaxGetGoodsList"+mode,
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
</script>
</head>
<body>
<!--#include file="include/header.htm"-->
<div id="wrap">
  <div id="main">
    <!--#include file="include/left_menu.htm"-->
    <script type="text/javascript">$('#bar-maimai-head').addClass('currently');$('#bar-maimai li.small').show();</script>
    <div id="mainarea">
      <!--二级菜单-->
      <div class="nav_two">
        <ul>
          <li class="currently"><a>我的商品</a></li>
        </ul>
        <div class="clear"></div>
      </div>
      <!--三级菜单-->
      <div id="nav_box">
        <ul class="nav_three">
          <li id="tab3"><a href="javascript:;" onclick="aLoad('')">全部</a></li>
          <li id="tab2"><a href="javascript:;" onclick="aLoad('old')">已付款</a></li>
          <li id="tab1"><a href="javascript:;" onclick="aLoad('new')">未付款</a></li>
        </ul>
        <div class="clear"></div>
      </div>
      <!--右侧列表-->
      <div id="list">
<textarea id="tplList" style="display:none">
        <table width="100%" cellpadding="0" cellspacing="0">
	<tr>
		<td style="width:80px;font-weight:bold;" align="center">商品图片</td>
		<td width="*" style="font-weight:bold;" align="left">商品信息</td>
		<td style="width:80px;font-weight:bold;" align="center">单价(元)</td>
		<td style="width:120px;font-weight:bold;" align="center">数量</td>
		<td style="width:100px;font-weight:bold;" align="center">总价(元)</td>
		<td style="width:60px;font-weight:bold;" align="center">状态</td>
	</tr>
	{#foreach $T.table as record}
	<tr>
		<td style="width:80px;" align="center"><div class="photo48"><a href="{$T.record.productlink}" target="_blank" title="{$T.record.productname}"><img src="{$T.record.productimg}" onerror="this.src=site.Dir+'statics/common/nophoto.jpg'" alt="{$T.record.productname}" width="48" height="48" /></a></div></td>
		<td width="*" align="left"><a href="{$T.record.productlink}" target="_blank">{$T.record.productname}</a></td>
		<td align="center">{formatCurrency($T.record.unitprice)}</td>
		<td align="center">{$T.record.buycount}</td>
		<td align="center">{formatCurrency($T.record.totalprice)}</td>
		<td align="center">
          		{#if $T.record.state == "0"}<font color="red">未付款</font>{#/if}
           		{#if $T.record.state == "1"}<font color="green">已付款</font>{#/if}
           		{#if $T.record.state == "2"}<font color="blue">已发货</font>{#/if}
           		{#if $T.record.state == "3"}<font color="black">已成交</font>{#/if}

		</td>
	</tr>
	{#/for}
</textarea>
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
