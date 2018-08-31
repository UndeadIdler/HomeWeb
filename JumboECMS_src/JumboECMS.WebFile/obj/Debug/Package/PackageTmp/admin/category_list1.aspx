<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="category_list1.aspx.cs" Inherits="JumboECMS.WebFile.Admin.category_list1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   name="robots" content="noindex, nofollow" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>中文版栏目</title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link type="text/css" rel="stylesheet" href="../_data/global.css" />
<link type="text/css" rel="stylesheet" href="css/common.css" />
<script type="text/javascript" src="scripts/common.js"></script>
<script type="text/javascript">
var pagesize=15;
var page=thispage();
$(document).ready(function(){
	$('.tip-r').jtip({gravity: 'r',fade: false});
	ajaxList(page);

});
function ajaxList(currentpage)
{
	if(currentpage!=null) page=currentpage;
	JumboECMS.Loading.show("正在加载数据,请等待...");
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"page="+currentpage+"&pagesize="+pagesize+"&time="+(new Date().getTime()),
		url:		"category_ajax.aspx?oper=ajaxGetList&lan=cn",
		error:		function(XmlHttpRequest,textStatus, errorThrown){JumboECMS.Loading.hide();if(XmlHttpRequest.responseText.length>0){alert(XmlHttpRequest.responseText);} },
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				JumboECMS.Alert(d.returnval, "0", "top.window.location='login.aspx';");
				break;
			case '0':
				JumboECMS.Alert(d.returnval, "0");
				break;
			case '1':
				JumboECMS.Loading.hide();
				$("#ajaxList").setTemplateElement("tplList", null, {filter_data: true});
				$("#ajaxList").processTemplate(d);
				//ActiveCoolTable();
				break;
			}
		}
	});
}
function move(id,isUp){
	JumboECMS.Loading.show("正在处理...");
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"id="+id+"&up="+isUp,
		url:		"category_ajax.aspx?oper=move&time="+(new Date().getTime()),
		error:		function(XmlHttpRequest,textStatus, errorThrown){JumboECMS.Loading.hide();if(XmlHttpRequest.responseText.length>0){alert(XmlHttpRequest.responseText);} },
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				JumboECMS.Alert(d.returnval, "0", "top.window.location='login.aspx';");
				break;
			case '0':
				JumboECMS.Alert(d.returnval, "0");
				break;
			case '1':
				JumboECMS.Loading.hide();
				ajaxList(page);
				break;
			}
		}
	});
}
function ConfirmDelete(id){
	JumboECMS.Confirm("确定要删除此分类吗?", "ajaxDelete("+id+")");
}
function ajaxDelete(id){
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"id="+id,
		url:		"category_ajax.aspx?oper=ajaxDelete&time="+(new Date().getTime()),
		error:		function(XmlHttpRequest,textStatus, errorThrown){JumboECMS.Loading.hide();if(XmlHttpRequest.responseText.length>0){alert(XmlHttpRequest.responseText);} },
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				JumboECMS.Alert(d.returnval, "0", "top.window.location='login.aspx';");
				break;
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
<div class="topnav"> <span class="preload1"></span><span class="preload2"></span>
	<ul id="topnavbar">
		<li class="topmenu"><a href="javascript:void(0);" class="top_link"><span

            class="down">添加栏目</span></a>

			<ul class="sub">
				<li><a href="javascript:void(0);" onclick="JumboECMS.Popup.show('category_edit1.aspx?id=0&lan=cn',-1,-1,false)" title="包含子类，不可以直接在此分类上发布内容">父级栏目</a></li>
				<li><a href="javascript:void(0);" onclick="JumboECMS.Popup.show('category_edit2.aspx?id=0&lan=cn',-1,-1,false)" title="不包含子类">终极栏目</a></li>
				<li><a href="javascript:void(0);" onclick="JumboECMS.Popup.show('category_edit3.aspx?id=0&lan=cn',-1,-1,false)" title="">单页栏目</a></li>
				<li><a href="javascript:void(0);" onclick="JumboECMS.Popup.show('category_edit4.aspx?id=0&lan=cn',-1,-1,false)" title="">外链栏目</a></li>

			</ul>

		</li>

	</ul>
	<script>
	topnavbarStuHover();
    </script>
</div>
<textarea id="tplList" style="display:none">
<table class="cooltable">
<thead>
	<tr>
		<th scope="col" style="width:40px;">ID</th>
		<th scope="col" width="*">栏目名称</th>
		<th scope="col" style="width:28px;">预览</th>
		<th scope="col" style="width:90px;">类型</th>
		<th scope="col" style="width:60px;">内容模型</th>
		<th scope="col" style="width:40px;">排序</th>
		<th scope="col" style="width:90px;">操作</th>
	</tr>
</thead>
<tbody>
	{#foreach $T.table as record}
	<tr>
		<td align="center">{$T.record.id}</td>
		<td align="left">
           		{#if $T.record.codelength < 10}
                	<img src="images/li0{$T.record.codelength}.gif" />
            		{#else}
                	<img src="images/li{$T.record.codelength}.gif" />
			{#/if}
			<span title="当前页模板：{$T.record.templatename}，详细页模板：{$T.record.contenttempname}">{$T.record.title}</span>
		</td>
		<td align="center"><a href="{$T.record.firstpage}" target="_blank"><img src="images/preview.gif" /></a></td>
		<td align="center">{$T.record.typename}</td>
		<td align="center">{$T.record.modulename}</td>
		<td align="center" class="oper">
			<a href="javascript:void(0)" onclick="move({$T.record.id},1)">↑</a><a style="margin-left:5px" href="javascript:void(0)" onclick="move({$T.record.id},-1)">↓</a>
		</td>
		<td align="center" class="oper">
			<a href="javascript:void(0);" onclick="JumboECMS.Popup.show('category_edit{$T.record.typeid}.aspx?id={$T.record.id}&lan=cn',-1,-1,true)">修改</a>
			<a href="javascript:void(0);" onclick="ConfirmDelete({$T.record.id})">删除</a>
		</td>
	</tr>
	{#/for}
</tbody>
</table>
</textarea>
<div id="ajaxList" class="mrg10T" style="width:100%;clear:both;"></div>
<script type="text/javascript">_jcms_SetDialogTitle();</script>
</body>
</html>
