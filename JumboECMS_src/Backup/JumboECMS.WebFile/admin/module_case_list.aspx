<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="module_case_list.aspx.cs" Inherits="JumboECMS.WebFile.Admin._module_case_list" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title></title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link   type="text/css" rel="stylesheet" href="../_data/global.css" />
<script type="text/javascript" src="scripts/common.js"></script>
<script type="text/javascript" src="scripts/admin.js"></script>
<link   type="text/css" rel="stylesheet" href="css/common.css" />
<script type="text/javascript">
var mtype = '<%=MainModule.Type%>';//模块类型
var module = '&module=<%=MainModule.Type%>';//模块类型
var pagesize= <%=AdminScreenWidth%> <1280 ? 10 :15;
</script>
<script type="text/javascript" src="scripts/content.js"></script>
</head>
<body>
<div class="topnav"> <span class="preload1"></span><span class="preload2"></span>
	<ul id="topnavbar">
		<li class="topmenu"><a href="javascript:void(0);" class="top_link"><span
            class="down">批量操作</span></a>
			<ul class="sub">
				<li><a href="javascript:void(0);" onclick="operater('pass')">审核内容</a></li>
				<li><a href="javascript:void(0);" onclick="operater('nopass')">取消审核</a></li>
				<li><a href="javascript:void(0);" onclick="operater('top')">设为推荐</a></li>
				<li><a href="javascript:void(0);" onclick="operater('notop')">取消推荐</a></li>
				<li><a href="javascript:void(0);" onclick="operater('sdel')">放入回收站</a></li>
				<li><a href="javascript:void(0);" onclick="operater('del')">直接删除</a></li>
			</ul>
		</li>
		<li class="topmenu"><a href="javascript:void(0);" onclick="top.JumboECMS.Popup.show('module_<%=MainModule.Type%>_edit.aspx?id=0'+categoryid,-1,-1,true)" class="top_link"><span>添加<%=MainModule.ItemName%></span></a></li>
		<li class="topmenu"><a href="javascript:void(0);" onclick="ajaxCreateHTML(<%=CategoryId %>);" class="top_link"><span>前台一键更新</span></a></li>
	</ul>
</div>
<script type="text/javascript">topnavbarStuHover();</script>
<div style=" margin:0px auto; width: 98%;">
	<a id="menu-s" href="javascript:void(0);" onclick="s='';ajaxList(1);FormatFontWeight();">不限</a> 
	<a id="menu-s0" href="javascript:void(0);" onclick="s='&s=0';ajaxList(1);FormatFontWeight();">新的</a> 
	<a id="menu-s-1" href="javascript:void(0);" onclick="s='&s=-1';ajaxList(1);FormatFontWeight();">待审</a> 
	<a id="menu-s1" href="javascript:void(0);" onclick="s='&s=1';ajaxList(1);FormatFontWeight();">已发布</a> |
	<a id="menu-isimg" href="javascript:void(0);" onclick="isimg='';ajaxList(1);FormatFontWeight();">不限</a> 
	<a id="menu-isimg1" href="javascript:void(0);" onclick="isimg='&isimg=1';ajaxList(1);FormatFontWeight();">有图</a> 
	<a id="menu-isimg-1" href="javascript:void(0);" onclick="isimg='&isimg=-1';ajaxList(1);FormatFontWeight();">无图</a> |
	<a id="menu-istop" href="javascript:void(0);" onclick="istop='';ajaxList(1);FormatFontWeight();">不限</a> 
	<a id="menu-istop1" href="javascript:void(0);" onclick="istop='&istop=1';ajaxList(1);FormatFontWeight();">推荐</a> 
	<a id="menu-istop-1" href="javascript:void(0);" onclick="istop='&istop=-1';ajaxList(1);FormatFontWeight();">不推荐</a>
</div>
<textarea id="tplList" style="display:none">
<table class="cooltable">
<thead>
	<tr>
		<th align="center" scope="col" style="width:40px;"><input onclick="checkAllLine()" id="checkedAll" name="checkedAll" type="checkbox" title="全部选择/全部不选" /></th>
		<th scope="col" style="width:60px;">ID</th>
		<th scope="col" width="*">标题</th>
		<th scope="col" style="width:150px;">前台发布时间</th>
		<th scope="col" style="width:66px;">状态</th>
		<th scope="col" style="width:90px;">操作</th>
	</tr>
	</thead>
<tbody>
	{#foreach $T.table as record}
	<tr>
		<td align="center"><input class="checkbox" name="selectID" type="checkbox" value='{$T.record.id}' />
		</td>
		<td align="center">{$T.record.id}</td>
		<td align="left">&nbsp;
			{#if $T.record.firstpage != ""}
			<a href="{$T.record.firstpage}" target="_blank">{$T.record.title}</a>
			{#else}
			<a href="../<%=MainModule.Type%>/view.aspx?id={$T.record.id}&preview=1" target="_blank">{$T.record.title}</a>
			{#/if}
		</td>
		<td align="center">{formatDate($T.record.adddate,'yyyy-MM-dd HH:mm:ss')}</td>
		<td align="center" class="oper">{formatContentOper($T.record.ispass,$T.record.id,'pass')}{formatContentOper($T.record.isimg,$T.record.id,'img')}{formatContentOper($T.record.istop,$T.record.id,'top')}
		</td>
		<td align="center" class="oper">
			<a href="javascript:void(0);" onclick="top.JumboECMS.Popup.show('module_<%=MainModule.Type%>_edit.aspx?id={$T.record.id}'+categoryid,-1,-1,true)">修改</a>
			<a href="javascript:void(0);" onclick="ConfirmDelete({$T.record.id})">删除</a>
		</td>
	</tr>
	{#/for}
</tbody>
</table></textarea>
<div id="ajaxList" class="mrg10T" style="width:100%;clear:both;"></div>
<div id="ajaxPageBar" class="pages"> </div>
</body>
</html>
