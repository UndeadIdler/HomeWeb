<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="email_batchimport.aspx.cs" Inherits="JumboECMS.WebFile.Admin._email_batchimport" %>
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
<script type="text/javascript">
function FormatMailList(id)
{
	var _val = $('#'+id).val();
	$('#'+id).val(formatmail(_val));
}
var formatmail = function(list){
	var _val = list;
	_val =_val.replace(/\r\n/g,"\n");
	_val =_val.replace(/;/g,"\n");
	_val =_val.replace(/[,]+/g,",");
	_val =_val.replace(/,\n/g,"\n");
	_val =_val.replace(/[\n]+/g,"\n");
	_val =_val.replace(/ /g,"");
	return _val;
}
var ReplaceAll = function(strOrg, strFind, strReplace) {
    var index = 0;
    while (strOrg.indexOf(strFind, index) != -1) {
        strOrg = strOrg.replace(strFind, strReplace);
        index = strOrg.indexOf(strFind, index);
    }
    return strOrg;
} 
var _success = 0;
function BatchImport(){
	_success = 0;
	var Mails = $('#txtFromMailList').val();
	if(Mails.length<5)
		alert("请输入联系人");
	else
		ajaxBatchImport(Mails);
}

function ajaxBatchImport(Mails){
	Mails = Mails.replace(/\n/g,";");
	var isOnce = true;//是否单次导入
	var thismails = Mails;
	var lastmails = "";
	var mailcount = thismails.replace(/[^;]/g,"").length + 1;
	if(mailcount==0) return;
	if(Mails.length<5) return;
	if ( mailcount > 30 ){//最多一次导入30个
		var e = [];
                var s = Mails.split('\n');
                for (var i = 0; i < 30; i++)
			e[e.length] = s[i];
		thismails = e.join(";");
		lastmails = Mails.substring(thismails.length+1);
		isOnce = false;
	}
	JumboECMS.Loading.show("联系人导入中，请稍等...",260,80);
	$.ajax({
		type:		"post",
		dataType:	"json",
		data:		"mails="+thismails,
		url:		"email_ajax.aspx?oper=batchimport&time="+(new Date().getTime()),
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
				_success+=parseInt(d.returnval);
				if(!isOnce)
					ajaxBatchImport(lastmails);
				else{
					JumboECMS.Alert(_success+"个地址导入成功", "1");
					try{parent.ajaxList(parent.page);}
					catch(e) {top.IframeOper.ajaxList(top.IframeOper.page);}
				}
				break;
			}
		}
	});
}
</script>
</head>
<body>
<table class="helptable mrg10T">
        <tr>
            <td>
                <ul>
                    <li>一行表示一个联系人</li>
                    <li>昵称和邮箱地址使用,隔开</li>
                </ul>
            </td>
        </tr>
</table>
<form id="batchimportForm" runat="server">
	<table cellspacing="0" cellpadding="0" width="100%">
		<tr>
			<td><textarea onblur="FormatMailList('txtFromMailList');" name="txtFromMailList" id="txtFromMailList" style="height:240px;width:99%;" class="ipt"></textarea></td>
		</tr>
	</table>
	<div class="buttonok">
		<input type="button" value="导入" id="btnSaveContent" class="btnsubmit" onClick="BatchImport();" />
		<input id="btnReset" type="button" value="取消" class="btncancel" onclick="top.JumboECMS.Popup.hide();" />
	</div>
</form>
</body>
</html>
