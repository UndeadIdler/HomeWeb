<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="JumboECMS.WebFile.Admin._home" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta   http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta   name="robots" content="noindex, nofollow" />
<meta   http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>后台首页</title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link type="text/css" rel="stylesheet" href="../_data/global.css" />
<link type="text/css" rel="stylesheet" href="css/common.css" />
<script type="text/javascript" src="scripts/admin.js"></script>
</head>
<body>
<div id="temporarydiv" style="display:none;"></div>
<div style="margin:10px;">
  <div>
	<table class="form_head">
		<tr>
			<td class="actions"><table cellpadding="0" cellspacing="0" border="0">
					<tr>
						<td class="active"><span>前台更新</span></td>
				</table></td>
		</tr>
	</table>
	<table cellspacing="0" cellpadding="0" width="100%" class="form_table">
		<tr>
			<th>中文版页面更新</th>
			<td><input type="button" value="更新" class="btnsubmit" onclick="ajaxCreateHTML(1);" />	
			</td>
			<th>英文版页面更新</th>
			<td><input type="button" value="更新" class="btnsubmit" onclick="ajaxCreateHTML(2);" />
			</td>
		</tr>
	</table>
	</div>
</div>
</body>
</html>
