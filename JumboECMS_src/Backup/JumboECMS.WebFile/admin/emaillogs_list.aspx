<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="emaillogs_list.aspx.cs" Inherits="JumboECMS.WebFile.Admin._emaillogs_list" %>
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

<script type="text/javascript" src="scripts/admin.js"></script>
<script language="javascript">
var pagesize=15;
var page=thispage();
$(document).ready(function(){
	ajaxList(page);
});
function ajaxList(currentpage)
{
	if(currentpage!=null) page=currentpage;
	top.JumboECMS.Loading.show("正在加载数据,请等待...",260,80);
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"page="+currentpage+"&pagesize="+pagesize+"&time="+(new Date().getTime()),
		url:		"emaillogs_ajax.aspx?oper=ajaxGetList",
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
function Confirmclear(){
	top.JumboECMS.Confirm("确定要清空吗?", "IframeOper.ajaxClear()");
}
function ajaxClear(){
	top.JumboECMS.Loading.show("正在清空...",260,80);
	$.ajax({
		type:		"get",
		dataType:	"json",
		url:		"emaillogs_ajax.aspx?oper=clear&time="+(new Date().getTime()),
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
</script>

</head>
<body>
    <div class="topnav">
        <span class="preload1"></span><span class="preload2"></span>
        <ul id="topnavbar">
            <li class="topmenu"><a href="javascript:void(0);" onclick="Confirmclear();" id="operater1" class="top_link"><span>清空日志</span></a>
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
		<th scope="col" width="80">发送员</th>
		<th scope="col" width="*">发送行为</th>
		<th scope="col" style="width:140px;">发送IP</th>
		<th scope="col" style="width:140px;">发送时间</th>
	</tr>
</thead>
<tbody>
	{#foreach $T.table as record}
	<tr>
		<td align="center">{$T.record.id}</td>
		<td align="center"><a>{$T.record.adminname}</a></td>
		<td align="left">标题：{$T.record.sendtitle}<br />收件人：{$T.record.sendusers}</td>
		<td align="left">{$T.record.sendip}</td>
		<td align="left">{$T.record.sendtime}</td>
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
