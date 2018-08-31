<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="executesql_default.aspx.cs" Inherits="JumboECMS.WebFile.Admin._executesql_index" %>
<%@ Register assembly="JumboECMS.WebControls" namespace="JumboECMS.WebControls" tagprefix="Jumboe" %>
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

<script type="text/javascript">
(function(){
	$(document).ready(function(){
		$('#executesqlForm').ajaxForm({
			beforeSubmit: JumboECMS.AjaxFormSubmit,
			success :function(data){
				JumboECMS.Eval(data);
			}
		}); 
	});
	//Form验证
	JumboECMS.AjaxFormSubmit=function(item){
		top.JumboECMS.Loading.show("正在处理，请稍等...");
		return true;
	};
})();
    </script>
</head>
<body>
<table class="helptable mrg10T">
	<tr>
		<td><ul>
				<li>请输入合法的SQl语句,多条语句用GO换行分开</li>
				<li>SqlServer与Access语法不一样</li>
			</ul></td>
	</tr>
</table>
<form id="executesqlForm" name="form1" method="post" action="executesql_ajax.aspx?oper=executesql">
	<table class="formtable mrg10T">
		<tr>
			<th> SQL语句 </th>
			<td><textarea name="txtSQLContent" id="txtSQLContent" style="height:220px;width:97%;" class="ipt"></textarea>
			</td>
		</tr>
	</table>
	<div class="buttonok">
		<input type="submit" value="执行" id="btnSaveContent" class="btnsubmit" />
	</div>
</form>
</body>
</html>
