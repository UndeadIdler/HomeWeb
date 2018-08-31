<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="javascript_list.aspx.cs" Inherits="JumboECMS.WebFile.Admin._javascript_list" %>
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
var pagesize=15;
var page=thispage();
$(document).ready(function(){
	ajaxList(page);
});
function ajaxList(currentpage)
{
	if(currentpage!=null) page=currentpage;
	window.JumboECMS.Loading.show("正在加载数据,请等待...");
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"page="+currentpage+"&pagesize="+pagesize+"&time="+(new Date().getTime()),
		url:		"javascript_ajax.aspx?oper=ajaxGetList",
		error:		function(XmlHttpRequest,textStatus, errorThrown){window.JumboECMS.Loading.hide();if(XmlHttpRequest.responseText.length>0){alert(XmlHttpRequest.responseText);} },
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
				window.JumboECMS.Loading.hide();
				$("#ajaxList").setTemplateElement("tplList", null, {filter_data: true});
				$("#ajaxList").processTemplate(d);
				break;
			}
		}
	});
}
function ConfirmDelete(id){
	top.JumboECMS.Confirm("确定要删除吗?", "IframeOper.ajaxDelete("+id+")");
}
function ajaxDelete(id){
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"id="+id,
		url:		"javascript_ajax.aspx?oper=ajaxDelete&time="+(new Date().getTime()),
		error:		function(XmlHttpRequest,textStatus, errorThrown){window.JumboECMS.Loading.hide();if(XmlHttpRequest.responseText.length>0){alert(XmlHttpRequest.responseText);} },
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
		<li class="topmenu"><a href="javascript:void(0);" onclick="top.JumboECMS.Popup.show('javascript_edit.aspx?id=0',-1,-1,true)" id="operater2" class="top_link"><span>新增调用</span></a> </li>
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
			<th scope="col" width="*">自定义外站调用名称</th>
			<th scope="col" style="width:120px;">操作</th>
		</tr>
</thead>
<tbody>
		{#foreach $T.table as record}
		<tr>
			<td align="center">{$T.record.id}</td>
			<td align="center">{$T.record.title}</td>
			<td align="center">
				<a href="javascript:void(0);" onclick="top.JumboECMS.Popup.show('javascript_edit.aspx?id={$T.record.id}',-1,-1,true)">修改</a>
				<a href="javascript:void(0);" onclick="top.JumboECMS.Popup.show('javascript_code.aspx?id={$T.record.id}',-1,-1,true)"><font color="red">调用效果</font></a>
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
