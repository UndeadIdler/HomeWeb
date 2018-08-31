<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="adv_list.aspx.cs" Inherits="JumboECMS.WebFile.Admin._adv_list" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>广告列表</title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link type="text/css" rel="stylesheet" href="../_data/global.css" />
<link type="text/css" rel="stylesheet" href="css/common.css" />
<script type="text/javascript" src="scripts/common.js"></script>
<script type="text/javascript">
var type = joinValue('type');//类别
var pagesize=15;
var page=thispage();
$(document).ready(function(){
	ajaxList(page);
});
function ajaxList(currentpage)
{
	if(currentpage!=null) page=currentpage;
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"page="+currentpage+"&pagesize="+pagesize+"&time="+(new Date().getTime()),
		url:		"adv_ajax.aspx?oper=ajaxGetList"+type,
		error:		function(XmlHttpRequest,textStatus, errorThrown) { if(XmlHttpRequest.responseText.length>0){alert(XmlHttpRequest.responseText);}},
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				top.JumboECMS.Alert(d.returnval, "0", "top.location.href='login.aspx';");
				break;
			case '0':
				top.JumboECMS.Alert(d.returnval, "0");
				break;
			case '1':
				$("#ajaxList").setTemplateElement("tplList", null, {filter_data: true});
				$("#ajaxList").processTemplate(d);
				ActiveCoolTable();
				$("#ajaxPageBar").html(d.pagerbar);
				break;
			}
		}
	});
}
function ConfirmDel(id){
	top.JumboECMS.Confirm("确定要删除吗?", "IframeOper.ajaxDelete("+id+")");
}
function ajaxDelete(id){
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"id="+id,
		url:		"adv_ajax.aspx?oper=ajaxDelete&time="+(new Date().getTime()),
		error:		function(XmlHttpRequest,textStatus, errorThrown){if(XmlHttpRequest.responseText.length>0){alert(XmlHttpRequest.responseText);} },
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				top.JumboECMS.Alert(d.returnval, "0", "top.location.href='login.aspx';");
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
function ajaxState(id, _state){
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"id="+id+"&state="+_state,
		url:		"adv_ajax.aspx?oper=ajaxState&time="+(new Date().getTime()),
		error:		function(XmlHttpRequest,textStatus, errorThrown){if(XmlHttpRequest.responseText.length>0){alert(XmlHttpRequest.responseText);} },
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				top.JumboECMS.Alert(d.returnval, "0", "top.location.href='login.aspx';");
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
function ajaxBatchUpdate(){
	$.ajax({
		type:		"get",
		dataType:	"json",
		url:		"adv_ajax.aspx?oper=ajaxBatchUpdate&time="+(new Date().getTime()),
		error:		function(XmlHttpRequest,textStatus, errorThrown){if(XmlHttpRequest.responseText.length>0){alert(XmlHttpRequest.responseText);} },
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				top.JumboECMS.Alert(d.returnval, "0", "top.location.href='login.aspx';");
				break;
			case '0':
				top.JumboECMS.Alert(d.returnval, "0");
				break;
			case '1':
				top.JumboECMS.Message(d.returnval, "1");
				break;
			}
		}
	});
}
function formatAdvState(state) {
	if(state == "-1")return "已删";
	var _state = "";
	switch(state){
		case "0":
			_state = "停用";
			break;
		case "1":
			_state = "启用";
			break;
	}
	return _state;
}
</script>
</head>
<body>
<form id="form1" runat="server">
<div class="topnav"> <span class="preload1"></span><span class="preload2"></span>
	<ul id="topnavbar">
		<li class="topmenu"><a href="adv_edit.aspx?id=0&type=<%=type%>" class="top_link"><span>添加广告</span></a></li>
		<li class="topmenu"><a href="javascript:void(0)" onclick="ajaxBatchUpdate()" class="top_link"><span>批量更新</span></a></li>
	</ul>
	<script>
	topnavbarStuHover();
        </script>
</div>
<textarea id="tplList" style="display:none">
<table class="cooltable">
<thead>
	<tr>
		<th scope="col" style="width:60px;">ID</th>
		<th scope="col" width="*">广告位名称</th>
		<th scope="col" style="width:30px;">类型</th>
		<th scope="col" style="width:150px;">添加时间</th>
		<th scope="col" style="width:30px;">状态</th>
		<th scope="col" style="width:220px;">操作</th>
	</tr>
	</thead>
<tbody>
	{#foreach $T.table as record}
	<tr>
		<td align="center">{$T.record.id}</td>
		<td align="left">&nbsp;{$T.record.title}</td>
		<td align="center"><img title="{$T.record.classname}" src="images/adv_{$T.record.advtype}.gif" border="0" /></td>
		<td align="center">{$T.record.adddate}</td>
		<td align="center"><img title="{formatAdvState($T.record.state)}" src="images/ico_state{$T.record.state}.gif" border="0" /></td>
		<td align="center" class="oper">
			<input type="button" value="预览" class="oper2_1" onclick="top.JumboECMS.Popup.show('adv_view.aspx?id={$T.record.id}',-1,-1,true)" />
			<input type="button" value="修改" class="oper2_1" onclick="location.href='adv_edit.aspx?id={$T.record.id}&page={page}{type}'" />
			<input type="button" value="删除" class="oper2_1" onclick="ConfirmDel({$T.record.id})" />
			{#if $T.record.state == "1"}
			<input type="button" value="停用" class="oper2_1" onclick="ajaxState({$T.record.id}, 0)" />
			{#else}
			<input type="button" value="启用" class="oper2_1" onclick="ajaxState({$T.record.id}, 1)" />
			{#/if}

		</td>
	</tr>
	{#/for}
</tbody>
</table></textarea>
<div id="ajaxList" class="mrg10T" style="width:100%;clear:both;"></div>
<div id="ajaxPageBar" class="pages"> </div>
</form>
</body>
</html>
