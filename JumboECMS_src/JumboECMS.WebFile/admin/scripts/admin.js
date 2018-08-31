//判断权限
function ajaxChkPower(_power){
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"power=" + _power + "&time="+(new Date().getTime()),
		url:		"ajax.aspx?oper=chkadminpower",
		error:		function(XmlHttpRequest,textStatus, errorThrown) { if(XmlHttpRequest.responseText.length>0){alert(XmlHttpRequest.responseText);}},
		success:	function(data){
			if(data.result == "1")
				$(".adminbar").show();
			else
				$(".adminbar").hide();
		}
	});
}

function chkLogout(){
	$.ajax({
		type:		"get",
		dataType:	"json",
		url:		"ajax.aspx?oper=logout&time="+(new Date().getTime()),
        	error:		function(XmlHttpRequest,textStatus, errorThrown) { if(XmlHttpRequest.responseText.length>0){alert(XmlHttpRequest.responseText);}},
		success:	function(d){
			if(d.result=="1")
				top.location.href = 'login.aspx';
		}
	});
}
function ajaxClearSystemCache()
{
	top.JumboECMS.Loading.show("正在更新，请等待...");
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"oper=ajaxClearSystemCache",
		url:		"ajax.aspx?time="+(new Date().getTime()),
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
				break;
			}
		}
	});
}
function ajaxCreateSystemCount()
{
	top.JumboECMS.Loading.show("正在更新，时间可能会比较长...");
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"oper=ajaxCreateSystemCount",
		url:		"ajax.aspx?time="+(new Date().getTime()),
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
				break;
			}
		}
	});
}

function ajaxCreateIndexPage()
{
	top.JumboECMS.Loading.show("正在更新，请等待...");
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"oper=ajaxCreateIndexPage",
		url:		"ajax.aspx?time="+(new Date().getTime()),
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
				break;
			}
		}
	});
}
function ajaxCreateHTML(categoryid)
{
	top.JumboECMS.Loading.show("正在更新，请等待...");
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"oper=ajaxCreateHTML&categoryid="+categoryid,
		url:		"ajax.aspx?time="+(new Date().getTime()),
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
				top.JumboECMS.Alert(d.returnval, "1");
				break;
			}
		}
	});
}
function ajaxEmailServerExport()
{
	top.JumboECMS.Loading.show("正在导出，请等待...",260,80);
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"oper=ajaxEmailServerExport",
		url:		"emailserver_ajax.aspx?time="+(new Date().getTime()),
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
				ajaxList(1);
				break;
			}
		}
	});
}
function ajaxEmailServerImport()
{
	top.JumboECMS.Loading.show("正在导出，请等待...",260,80);
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"oper=ajaxEmailServerImport",
		url:		"emailserver_ajax.aspx?time="+(new Date().getTime()),
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
				ajaxList(1);
				break;
			}
		}
	});
}
function ajaxCreateJavascript()
{
	top.JumboECMS.Loading.show("正在更新，请等待...");
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"oper=ajaxCreateJavascript",
		url:		"javascript_ajax.aspx?time="+(new Date().getTime()),
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
				break;
			}
		}
	});
}
function ajaxCreateRssMap()
{
	top.JumboECMS.Loading.show("正在更新，请等待...");
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"oper=ajaxCreateRssMap",
		url:		"ajax.aspx?time="+(new Date().getTime()),
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
				break;
			}
		}
	});
}
function ajaxCreateSiteMap()
{
	top.JumboECMS.Loading.show("正在更新，请等待...");
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"oper=ajaxCreateSiteMap",
		url:		"ajax.aspx?time="+(new Date().getTime()),
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
				break;
			}
		}
	});
}
/*更新include包含文件*/
function ajaxTemplateIncludeUpdateFore(pid,source)
{
	if(source==null)source="";
	top.JumboECMS.Loading.show("正在更新，请等待...");
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"time="+(new Date().getTime()),
		url:		site.Dir + "admin/templateinclude_ajax.aspx?oper=updatefore&pid="+pid+"&source="+source,
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
				break;
			}
		}
	});
}
function ajaxModuleUpdateFore()
{
	top.JumboECMS.Loading.show("正在更新，请等待...");
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"time="+(new Date().getTime()),
		url:		"modules_ajax.aspx?oper=updatefore",
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
				break;
			}
		}
	});
}
/*更新站内搜索索引*/
function ajaxCreateSearchIndex(create)
{
	top.JumboECMS.Loading.show("更新过程可能比较缓慢，请耐心等待...");
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"oper=ajaxCreateSearchIndex&create="+create,
		url:		"ajax.aspx?time="+(new Date().getTime()),
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
				break;
			}
		}
	});
}