<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="templateinclude_list.aspx.cs" Inherits="JumboECMS.WebFile.Admin._templateinclude_list" %>
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
<script type="text/javascript" src="scripts/admin.js"></script>
<script type="text/javascript">
var pid = joinValue('pid');
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
		data:		"page="+currentpage+"&pagesize="+pagesize+"&time="+(new Date().getTime()) + pid,
		url:		"templateinclude_ajax.aspx?oper=ajaxGetList",
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
				break;
			}
		}
	});
}
function ConfirmDelete(id){
	JumboECMS.Confirm("确定要删除吗?", "ajaxDelete("+id+")");
}
function ajaxDelete(id){
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"id="+id,
		url:		"templateinclude_ajax.aspx?oper=ajaxDelete&time="+(new Date().getTime()) + pid,
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
    <div class="topnav"> <span class="preload1"></span><span class="preload2"></span>
	<ul id="topnavbar">
		<li class="topmenu"><a href="javascript:void(0);" onclick="top.JumboECMS.Popup.show('templateinclude_edit.aspx?id=0'+pid,800,280,true)" id="operateradd" class="top_link"><span>增加文件</span></a>
	</ul>
	<script>
	topnavbarStuHover();
    </script>
</div>
<table class="maintable mrg10T">
	<tr>
		<th> 前台批量更新</th>
		<td align="left"><input type="button" value="执行" class="btnsubmit" onclick="ajaxTemplateIncludeUpdateFore(q('pid'));" />
		</td>
	</tr>
</table>
<textarea id="tplList" style="display:none">
<table class="cooltable">
<thead>
	<tr>
		<th scope="col" style="width:60px;">ID</th>
		<th scope="col" width="*">模块名称</th>
		<th scope="col" style="width:200px;">文件名称</th>
		<th scope="col" style="width:60px;">需要编译</th>
		<th scope="col" style="width:50px;">优先级</th>
		<th scope="col" style="width:200px;">操作</th>
	</tr>
</thead>
<tbody>
	{#foreach $T.table as record}
	<tr>
		<td align="center">{$T.record.id}</td>
		<td align="center"><a>{$T.record.title}</a></td>
		<td align="left"><a>{$T.record.source}</a></td>
		<td align="center">
			{#if $T.record.needbuild == "1"}
			是
			{#else}
			<font color='red'>否</font>
			{#/if}
		</td>
		<td align="center">{$T.record.sort}</td>
		<td align="center">
			<a href="javascript:void(0);" onclick="top.JumboECMS.Popup.show('templateinclude_edit.aspx?id={$T.record.id}'+ pid,800,280,true)">属性设置</a>
			<a href="javascript:void(0);" onclick="top.JumboECMS.Popup.show('templateinclude_edittemplate.aspx?source={$T.record.source}{pid}',-1,-1,true)">在线编辑</a>
			<a href="javascript:void(0);" onclick="ajaxTemplateIncludeUpdateFore(q('pid'),'{$T.record.source}')">前台更新</a>
			<a href="javascript:void(0);" onclick="ConfirmDelete({$T.record.id})">删除</a>
		</td>
	</tr>
	{#/for}
</tbody>
</table>
</textarea>
<div id="ajaxList" class="mrg10T" style="width:100%;clear:both;"></div>

</body>
</html>
