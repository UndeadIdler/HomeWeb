<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="JumboECMS.WebFile.Plus.Search._index" %>
<html>
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8">
<title>站内搜索 - <%= site.Name1%></title>
<script type="text/javascript" src="<%= site.Dir%>_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="<%= site.Dir%>_data/global.js"></script>
<link rel="stylesheet" type="text/css" href="style/style.css"/>
<script type="text/javascript">
$(document).ready(function() {
	ajaxPluginSearchList(<%= PageSize%>,1);
});
function ajaxPluginSearchList(pagesize,page)
{
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"time="+(new Date().getTime()),
		url:		"ajax.aspx?oper=ajaxGetContentList&type=<%= ChannelType%>&mode=<%= Mode%>&k="+encodeURIComponent('<%= Keywords%>')+"&pagesize="+pagesize+"&page="+page,
		error:		function(XmlHttpRequest,textStatus, errorThrown){if(XmlHttpRequest.responseText!=""){alert('请输入其他的关键字试试');}},
		success:	function(d){
			if(d.recordcount != -1)
			{
				$("#ajaxTotalCount").text(d.recordcount);
				$("#ajaxEventTime").text(d.eventtime);
				$("#ajaxPluginSearchList").setTemplateElement("tplList", null, {filter_data: false});
				$("#ajaxPluginSearchList").processTemplate(d);
			}
		}
	});
}
</script>
</HEAD>
<body>
<div id="top"><a href="<%= site.Url%><%= site.Dir%>"><IMG id="logo" alt="<%= site.Name1%>" src="style/logo.gif" border="0"></a>
	<div id="ss">
		<form action="default.aspx" name="formsearch" method="get">
		<div id="pd">
			<span id="ajaxChannelType"></span>
			<script>BindModuleRadio("ajaxChannelType",q("type"));</script>
		</div>
		<div id="search">
			<input name="k" type="text" maxlength="128" size="128" id="tbKeyWord" class="input" value="<%= Keywords%>" />
			<span id="rfvKeyWord" style="color:Red;display:none;">请输入您要检索的关键字</span>
			<input type="image" class="float-left" src="style/btn_search.gif" alt="" border="0" />
			&nbsp; <span id="ajaxMode"></span>
			<script>BindModeRadio("ajaxMode",q("mode"));</script></div>
		</form><!--分词:<%=SplitWords%>-->
	</div>
</div>
<div id="adr"><span class="float-right right10"> 找到相关内容<span id="ajaxTotalCount">0</span>篇，用时<span id="ajaxEventTime">0</span>毫秒</span>&nbsp;&nbsp;&nbsp;&nbsp;<a href="<%= site.Url%><%= site.Dir%>"><%= site.Name1%></a> 
	&gt; 搜索 &gt; “<span class="cRed"><%= Keywords%></span>”的搜索结果</div>
<textarea id="tplList" style="display:none">
{#foreach $T.table as record}
<div class="jg">
	<h1><a target='_blank' href='{$T.record.url}'>{$T.record.title}</a></h1>
	{$T.record.summary}
	<br /><span class="cQING"> {$T.record.adddate}  </span> </div>
{#/for}
<P></P>
<P></P>
<div>{$T.pagebar}</div>
</textarea>
<div id="ajaxPluginSearchList"></div>
<P></P>
<P></P>
<div id="bottom"> <span id="bq"><%= site.Name1%>&nbsp;&nbsp;版权所有</span></div>
</form>
</body>
</html>
