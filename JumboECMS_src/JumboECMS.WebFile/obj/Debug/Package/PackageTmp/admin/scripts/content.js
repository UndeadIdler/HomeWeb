var categoryid = joinValue('categoryid');//栏目ID
var k=joinValue('k');//关键字
var f=joinValue('f');//检索字段
var s=joinValue('s');//检索状态
var isimg=joinValue('isimg');
var istop=joinValue('istop');
var isfocus=joinValue('isfocus');
var d=joinValue('d');//检索时间
var page=thispage();
$(document).ready(function(){
	ajaxList(page);
	FormatFontWeight();
});
function ajaxList(currentpage)
{
	if(currentpage!=null) page=currentpage;
	$.ajax({
		type:		"get",
		dataType:	"json",
		data:		"page="+currentpage+"&pagesize="+pagesize+"&time="+(new Date().getTime()),
		url:		"module_" + mtype + "_ajax.aspx?oper=ajaxGetList"+categoryid+k+f+s+d+isimg+istop+isfocus,
		error:		function(XmlHttpRequest,textStatus, errorThrown) { if(XmlHttpRequest.responseText.length>0){alert(XmlHttpRequest.responseText);}},
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				top.JumboECMS.Alert(d.returnval, "0", "top.window.location='"+site.Dir+"admin/login.aspx';");
				break;
			case '0':
				top.JumboECMS.Alert(d.returnval, "0");
				break;
			case '1':
				$("#ajaxList").setTemplateElement("tplList", null, {filter_data: false});
				$("#ajaxList").processTemplate(d);
				$("#ajaxPageBar").html(d.pagerbar);
				ActiveCoolTable();
				break;
			}
		}
	});
}
function FormatFontWeight(){
	$("#menu-s0").attr('class', s =='&s=0' ? 'menu1':'menu0');
	$("#menu-s1").attr('class', s =='&s=1' ? 'menu1':'menu0');
	$("#menu-s-1").attr('class', s =='&s=-1' ? 'menu1':'menu0');
	$("#menu-s").attr('class', (s =='' || s =='&s=') ? 'menu1':'menu0');
	$("#menu-isimg1").attr('class', isimg =='&isimg=1' ? 'menu1':'menu0');
	$("#menu-isimg2").attr('class', isimg =='&isimg=2' ? 'menu1':'menu0');
	$("#menu-isimg-1").attr('class', isimg =='&isimg=-1' ? 'menu1':'menu0');
	$("#menu-isimg").attr('class', (isimg =='' || isimg =='&isimg=') ? 'menu1':'menu0');
	$("#menu-istop1").attr('class', istop =='&istop=1' ? 'menu1':'menu0');
	$("#menu-istop-1").attr('class', istop =='&istop=-1' ? 'menu1':'menu0');
	$("#menu-istop").attr('class', (istop =='' || istop =='&istop=') ? 'menu1':'menu0');
}
function operater(act){
	var ids = JoinSelect("selectID");
	if(ids=="")
	{
		top.JumboECMS.Alert("没有任何选择项", "0"); 
		return;
	}
	ajaxBatchOper(act,ids);
}
function ajaxBatchOper(act,ids){
	top.JumboECMS.Loading.show("正在处理...");
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"ids="+ids,
		url:		"module_" + mtype + "_ajax.aspx?oper=ajaxBatchOper&act="+act+"&time="+(new Date().getTime())+categoryid,
		error:		function(XmlHttpRequest,textStatus, errorThrown){top.JumboECMS.Loading.hide();if(XmlHttpRequest.responseText.length>0){alert(XmlHttpRequest.responseText);} },
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				top.JumboECMS.Alert(d.returnval, "0", "top.window.location='"+site.Dir+"admin/login.aspx';");
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
		url:		"module_" + mtype + "_ajax.aspx?oper=ajaxDelete&time="+(new Date().getTime())+categoryid,
		error:		function(XmlHttpRequest,textStatus, errorThrown){if(XmlHttpRequest.responseText.length>0){alert(XmlHttpRequest.responseText);} },
		success:	function(d){
			switch (d.result)
			{
			case '-1':
				top.JumboECMS.Alert(d.returnval, "0", "top.window.location='"+site.Dir+"admin/login.aspx';");
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
var PublicID="";
//批量内容操作
function formatContentOper(_value, _id, _type)
{
	var _str;
	switch(_type){
		case 'img':
			_str = '<img title="'+formatIsImg(_value)+'" src="'+site.Dir+'admin/images/ico_isimg'+_value+'.gif" border="0" />';
			break;
		case 'top':
			if(_value==1)
				_str = '<a href="javascript:void(0)" title="取消推荐" onclick="ajaxBatchOper(\'notop\','+_id+')"><img src="'+site.Dir+'admin/images/ico_istop'+_value+'.gif" border="0" /></a>';
			else
				_str = '<a href="javascript:void(0)" title="设为推荐" onclick="ajaxBatchOper(\'top\','+_id+')"><img src="'+site.Dir+'admin/images/ico_istop'+_value+'.gif" border="0" /></a>';
			break;
		case 'focus':
			if(_value==1)
				_str = '<a href="javascript:void(0)" title="取消焦点" onclick="ajaxBatchOper(\'nofocus\','+_id+')"><img src="'+site.Dir+'admin/images/ico_isfocus'+_value+'.gif" border="0" /></a>';
			else
				_str = '<a href="javascript:void(0)" title="设为焦点" onclick="ajaxBatchOper(\'focus\','+_id+')"><img src="'+site.Dir+'admin/images/ico_isfocus'+_value+'.gif" border="0" /></a>';

			break;
		default:
			if(_value==1)
				_str = '<img alt="已发布" src="'+site.Dir+'admin/images/ico_ispass'+_value+'.gif" border="0" />';
			if(_value==-1)
				_str = '<img alt="待审" src="'+site.Dir+'admin/images/ico_ispass'+_value+'.gif" border="0" />';
			if(_value==0)
				_str = '<img alt="新的" src="'+site.Dir+'admin/images/ico_ispass'+_value+'.gif" border="0" />';
			break;
	}
	return _str;
}