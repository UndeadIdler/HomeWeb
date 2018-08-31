<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="email_list.aspx.cs" Inherits="JumboECMS.WebFile.Admin._email_list" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title></title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link type="text/css" rel="stylesheet" href="../_data/global.css" />
<link type="text/css" rel="stylesheet" href="css/common.css" />
<script type="text/javascript" src="scripts/common.js"></script>

<script language="javascript">
function ajaxTopNav()
{
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"time="+(new Date().getTime()),
		url:		"emailgroup_ajax.aspx?oper=ajaxGetList",
		error:		function(XmlHttpRequest,textStatus, errorThrown) { if(XmlHttpRequest.responseText.length>0){alert(XmlHttpRequest.responseText);}},
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
				$("#ajaxTopNav").setTemplateElement("tplTopNav", null, {filter_data: true});
				$("#ajaxTopNav").processTemplate(d);
				break;
			}
		}
	});
}
var gid = joinValue('gid');//邮箱组ID
var keys=joinValue('keys');//关键字
var pagesize=15;
var page=thispage();
$(document).ready(function(){
	ajaxTopNav();
	ajaxList(page);

});
function ajaxList(currentpage)
{
	if(currentpage!=null) page=currentpage;
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"page="+currentpage+"&pagesize="+pagesize+"&time="+(new Date().getTime()),
		url:		"email_ajax.aspx?oper=ajaxGetList"+gid+keys,
        error:		function(XmlHttpRequest,textStatus, errorThrown) { if(XmlHttpRequest.responseText.length>0){alert(XmlHttpRequest.responseText);}},
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
				$("#ajaxList").setTemplateElement("tplList", null, {filter_data: true});
				$("#ajaxList").processTemplate(d);
				ActiveCoolTable();
				$("#ajaxPageBar").html(d.pagerbar);
				break;
			}
		}
	});
}
function operater(act,groupid){
	var ids = JoinSelect("selectID");
	if(ids=="")
	{
		top.JumboECMS.Alert("没有任何选择项", "0"); 
		return;
	}
	top.JumboECMS.Loading.show("正在处理...",260,80);
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"ids="+ids+"&togid="+groupid,
		url:		"email_ajax.aspx?oper=ajaxBatchOper&act="+act+"&time="+(new Date().getTime()),
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
function ConfirmDelete(id){
	top.JumboECMS.Confirm("确定要删除吗?", "IframeOper.ajaxDelete("+id+")");
}
function ajaxDelete(id){
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"id="+id,
		url:		"email_ajax.aspx?oper=ajaxDelete&time="+(new Date().getTime()),
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
function move2group(groupid){
	operater("move2group",groupid);
}
</script>
</head>
<body>
<textarea id="tplTopNav" style="display:none">
<div class="topnav"> <span class="preload1"></span><span class="preload2"></span>
	<ul id="topnavbar">
		<li class="topmenu"><a href="javascript:void(0);" class="top_link"><span
            class="down">批量操作</span></a>
			<ul class="sub">
				<li><a href="javascript:void(0);" onclick="operater('pass')">启用</a></li>
				<li><a href="javascript:void(0);" onclick="operater('nopass')">禁用</a></li>
				<li><a class="fly" href="javascript:void(0);" id="move2group">转移分组</a>
					<ul>
						{#foreach $T.table as record}
						<li><a href="javascript:void(0);" onclick="move2group({$T.record.id})"> {$T.record.groupname}</a></li>
						{#/for}
					</ul>
				</li>
				<li><a href="javascript:void(0);" onclick="operater('del')">直接删除</a></li>
			</ul>
		</li>
		<li class="topmenu"><a href="javascript:void(0);" onclick="top.JumboECMS.Popup.show('email_edit.aspx?id=0',450,230,true)" class="top_link"><span>添加联系人</span></a></li>
		<li class="topmenu"><a href="javascript:void(0);" onclick="top.JumboECMS.Popup.show('email_batchimport.aspx?id=0',-1,-1,false)" class="top_link"><span>批量导入</span></a></li>
	</ul>
	<script>
	topnavbarStuHover();
        </script>
</div>
</textarea>
    <div id="ajaxTopNav"></div>
<textarea id="tplList" style="display:none">
<table class="cooltable">
<thead>
	<tr>
		<th align="center" scope="col" style="width:40px;"><input onclick="checkAllLine()" id="checkedAll" name="checkedAll" type="checkbox" title="全部选择/全部不选" /></th>
		<th scope="col" style="width:60px;">ID</th>
		<th style="width:150px;">署名/昵称</th>
		<th width="*">邮箱名</th>
		<th style="width:150px;">邮箱组</th>
		<th scope="col" style="width:40px;">状态</th>
		<th scope="col" style="width:80px;">操作</th>
	</tr>
</thead>
<tbody>
	{#foreach $T.table as record}
	<tr>
		<td align="center"><input class="checkbox" name="selectID" type="checkbox" value='{$T.record.id}' /></td>
		<td align="center">{$T.record.id}</td>
		<td align="left">
			{$T.record.nickname}
		</td>
		<td align="left">
			{$T.record.emailaddress}
		</td>
		<td align="center"><a>{$T.record.groupname}</a></td>
		<td align="center">
			{#if $T.record.state == "1"}
			启用
			{#else}
			<font color='red'>已禁</font>
			{#/if}
		</td>
		<td align="center" class="oper">
           		<a href="javascript:void(0);" onclick="top.JumboECMS.Popup.show('email_edit.aspx?id={$T.record.id}',450,230,true)">编辑</a>
			<a href="javascript:void(0);" onclick="ConfirmDelete({$T.record.id})">删除</a>
		</td>
	</tr>
	{#/for}
</tbody>
</table>
</textarea>
    <div id="ajaxList" class="mrg10T" style="width:100%;clear:both;"></div>
    <div id="ajaxPageBar" class="pages"></div>
</body>
</html>
