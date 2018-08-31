<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="userorder_list.aspx.cs" Inherits="JumboECMS.WebFile.Admin._userorder_list" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   name="robots" content="noindex, nofollow" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title></title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link type="text/css" rel="stylesheet" href="../_data/global.css" />
<link type="text/css" rel="stylesheet" href="css/common.css" />
<script type="text/javascript" src="scripts/common.js"></script>

<script type="text/javascript">
var pagesize=15;
var page=thispage();
$(document).ready(function(){
	ajaxList(page);
});
function ajaxList(currentpage)
{
	if(currentpage!=null) page=currentpage;
	top.JumboECMS.Loading.show("正在加载数据,请等待...");
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"page="+currentpage+"&pagesize="+pagesize+"&time="+(new Date().getTime()),
		url:		"userorder_ajax.aspx?oper=ajaxGetList",
		error:		function(XmlHttpRequest,textStatus, errorThrown){top.JumboECMS.Loading.hide();if(XmlHttpRequest.responseText.length>0){alert(XmlHttpRequest.responseText);} },
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				top.JumboECMS.Alert(d.returnval, "0", "top.window.location='login.aspx';");
				break;
			case '0':
				top.JumboECMS.Alert(d.returnval, "0");
				break;
			case '1':
				top.JumboECMS.Loading.hide();
				$("#ajaxList").setTemplateElement("tplList", null, {filter_data: true});
				$("#ajaxList").processTemplate(d);
				$("#ajaxPageBar").html(d.pagerbar);
				break;
			}
		}
	});
}
function ajaxCheck(ordernum){
	top.JumboECMS.Loading.show("正在处理...");
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"ordernum="+ordernum,
		url:		"userorder_ajax.aspx?oper=ajaxCheck&time="+(new Date().getTime()),
		error:		function(XmlHttpRequest,textStatus, errorThrown){top.JumboECMS.Loading.hide();if(XmlHttpRequest.responseText.length>0){alert(XmlHttpRequest.responseText);} },
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				top.JumboECMS.Alert(d.returnval, "0", "top.window.location='login.aspx';");
				break;
			case '0':
				top.JumboECMS.Alert(d.returnval, "0");
				break;
			case '1':
				top.JumboECMS.Message(d.returnval, "1");
				ajaxList(page);
				break;
			}
		}
	});
}
function ajaxDelete(ordernum){
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"ordernum="+ordernum,
		url:		"userorder_ajax.aspx?oper=ajaxDelete&time="+(new Date().getTime()),
		error:		function(XmlHttpRequest,textStatus, errorThrown){if(XmlHttpRequest.responseText.length>0){alert(XmlHttpRequest.responseText);} },
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				top.JumboECMS.Alert(d.returnval, "0", "top.window.location='login.aspx';");
				break;
			case '0':
				top.JumboECMS.Alert(d.returnval, "0");
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
<textarea id="tplList" style="display:none">
<table class="cooltable">
<thead>
	<tr>
		<th scope="col" style="width:40px;">ID</th>
		<th scope="col" style="width:160px;">订单号</th>
		<th scope="col" style="width:100px;">订单总金额</th>
		<th scope="col" style="width:70px;">状态</th>
		<th scope="col" width="*">会员名</th>
		<th scope="col" style="width:160px;">订单生成时间</th>
		<th scope="col" style="width:120px;">操作</th>
	</tr>
</thead>
<tbody>
	{#foreach $T.table as record}
	<tr>
		<td align="center">{$T.record.id}</td>
		<td align="center">{$T.record.ordernum}</td>
		<td align="center">{formatCurrency($T.record.money)}</td>
		<td align="center">
           		{#if $T.record.state == "0"}<font color="red">未付款</font>{#/if}
           		{#if $T.record.state == "1"}<font color="green">已付款</font>{#/if}
           		{#if $T.record.state == "2"}<font color="blue">已发货</font>{#/if}
           		{#if $T.record.state == "3"}<font color="black">已成交</font>{#/if}
		</td>
		<td align="center">{$T.record.username}</td>
		<td align="center">{$T.record.ordertime}</td>
		<td align="center">
			{#if $T.record.state == "0"}
			<a href="javascript:void(0);" onclick="top.JumboECMS.Confirm('确定要作废该订单?', 'IframeOper.ajaxDelete(\'{$T.record.ordernum}\')');">作废订单</a>
			{#else}
			<font color='#cccccc'>作废订单</font>
			{#/if}
			{#if $T.record.state == "0" || $T.record.state == "1"}
           		<a href="javascript:void(0);" onclick="top.JumboECMS.Confirm('你确定已收到对方汇款/转账，并准备发货吗?', 'IframeOper.ajaxCheck(\'{$T.record.ordernum}\')');">准备发货</a>
			{#else}
			<font color='#cccccc'>准备发货</font>
			{#/if}
		</td>
	</tr>
	{#/for}
</tbody>
</table>
</textarea>
    <div id="ajaxList" class="mrg10T" style="width:100%;clear:both;"></div>
    <div id="ajaxPageBar" class="pages">
    </div>
</body>
</html>
