<%@ Page Language="C#" AutoEventWireup="true" Codebehind="default.aspx.cs" Inherits="JumboECMS.WebFile.Admin._default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html   xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
<title>管理中心 -  <%=site.Version %></title>
<script type="text/javascript" src="../_libs/jquery.tools.pack.js"></script>
<script type="text/javascript" src="../_data/global.js"></script>
<link   type="text/css" rel="stylesheet" href="../_data/global.css" />
<link   type="text/css" rel="stylesheet" href="css/index.css" />
<script type="text/javascript" src="scripts/admin.js"></script>
<script type="text/javascript">
    var _height_top = 98;
    var _height_bottom = 31;
    var _menuid = (q("menuid") != "") ? q("menuid") : "3";
    var _menuNum = 0; //初始信息
    var _linkarr = new Array();
    _linkarr[0] = null;
    _linkarr[1] = null;
    $(document).ready(function () {
        ajaxChkPower('0000');
        ShowTopTabs(_menuid);
        ShowLeftMenus(_menuid);
        ajaxChkVersion();
        setInterval("setTime()", 1000); //当前时间
        JumboECMS.Event.add(window, "scroll", resizeHeight);
        JumboECMS.Event.add(window, "resize", resizeHeight);
    });
    function resizeHeight() { $i("IframeOper").style.height = (_jcms_GetViewportHeight() - _height_top - _height_bottom - 4) + "px"; $i("ajaxMenuBody").style.height = (_jcms_GetViewportHeight() - _height_top - _height_bottom - 28) + "px"; }
    function setTime() {
        var dt = new Date();
        var arr_week = new Array("星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六");
        var strWeek = arr_week[dt.getDay()];
        var strHour = dt.getHours();
        var strMinutes = dt.getMinutes();
        var strSeconds = dt.getSeconds();
        if (strMinutes < 10) strMinutes = "0" + strMinutes;
        if (strSeconds < 10) strSeconds = "0" + strSeconds;
        var strYear = dt.getFullYear() + "年";
        var strMonth = (dt.getMonth() + 1) + "月";
        var strDay = dt.getDate() + "日";
        var strTime = strHour + ":" + strMinutes + ":" + strSeconds;
        $('#time').html("<span>现在是：" + strYear + strMonth + strDay + "&nbsp;" + strTime + "&nbsp;&nbsp;" + strWeek + "</span>");
    }
    function ShowLeftMenus(n) {
        _linkarr[0] = null; 
        _linkarr[1] = null;
        $('#ajaxMenuBody').html("<br/><br/><img src='images/index-loading.gif' />");
        IframeOper.location.href = 'loading.htm';
        $.ajax({
            type: "get",
            dataType: "json",
            data: "m=" + n + "&time=" + (new Date().getTime()),
            url: "ajax.aspx?oper=leftmenu",
            error: function (XmlHttpRequest, textStatus, errorThrown) { if(XmlHttpRequest.responseText.length>0){alert(XmlHttpRequest.responseText);} },
            success: function (d) {
                switch (d.result) {
                    case '-1':
                        JumboECMS.Alert(d.returnval, "0", "window.location='login.aspx';");
                        break;
                    case '0':
                        JumboECMS.Alert(d.returnval, "0");
                        break;
                    case '1':
                        _menuNum = d.recordcount;
                        $("#ajaxMenuBody").setTemplateElement("tplMenuBody", null, { filter_data: false });
                        $("#ajaxMenuBody").processTemplate(d);
                        BindParentClick();//先绑定事件
                        ShowSubMenu(1);//然后再展开
                        break;
                }
            }
        });
    }

    function ShowTopTabs(n) {
        $('#coolnav td:even').addClass('mouseout');
        $('#coolnav td#toptab' + n).addClass('selected');
        $('#coolnav td').hover(function () { if (!$(this).hasClass('blank')) $(this).addClass('mouseover'); },
        function () { if (!$(this).hasClass('blank')) $(this).removeClass('mouseover'); });
        $('#coolnav td').click(function () {
		    $('#coolnav td').each(function () {$(this).removeClass('selected');});
		    $(this).addClass('selected');
		});
    }

    function $Go2Page(url, n) {
        _linkarr[n - 1] = url;
        if (url.length > 1) {
            IframeOper.location.href = url;
        }
    }
    /* sid从1开始 */
    function ShowSubMenu(sid) {
        IframeOper.location.href = 'loading.htm';
        $('.submenu').hide();
        $('.imgmenu').removeClass('menu-title1').addClass('menu-title0');
        $('#submenu' + sid).show();
        $('#imgmenu' + sid).addClass('menu-title1').removeClass('menu-title0');
        $('#submenu' + sid + ' td.golink').unbind('click').bind('click',function () {
            $('#submenu' + sid + ' td.menu-content1').removeClass('menu-content1');
            $(this).addClass('menu-content1');
            if($(this).parent().hasClass('depth1')) {
                $('#submenu' + sid + ' tr.depth2').hide();
                $('#submenu' + sid + ' tr.depth3').hide();
                $('#submenu' + sid + ' tr.depth4').hide();
                $('#submenu' + sid + ' tr.depth5').hide();
            }
            $Go2Page($(this).attr('url'), sid);
        });
        if (_linkarr[sid - 1] == null) {
            $('#submenu' + sid + ' tr.depth1 td').eq(0).trigger("click");
        }
        else
            $Go2Page(_linkarr[sid - 1], sid);
    }
    function BindParentClick() {
        bindparentclick(1);
        bindparentclick(2);
    }
    function bindparentclick(n) {
        $('#submenu' + n + ' td.parent').click(function () {
            if ($(this).attr('rel') == "close") {//先关闭其他的，确保depth大于1哦
                $('#submenu' + n + ' tr.depth2').hide();
                $('#submenu' + n + ' tr.depth3').hide();
                $('#submenu' + n + ' tr.depth4').hide();
                $('#submenu' + n + ' tr.depth5').hide();

                $('#submenu' + n + ' td.parent').attr('rel', 'close');
                $('#submenu' + n + ' td.group' + $(this).attr('groupid')).parent().show();
                $(this).attr('rel', 'open');
                $(this).parent().next().find('td').trigger("click");
            }
        });
    }
</script>
</head>
<body onload="resizeHeight()">
<div class="top">
	<table width="100%" border="0" cellspacing="0" cellpadding="0">
		<tr>
			<td width="8" background="images/index-top-l.gif"><img src="images/index-top-l.gif" width="8" height="98" /></td>
			<td height="98" background="images/index-top-c.gif"><table width="100%" border="0" cellspacing="0" cellpadding="0">
					<tr>
						<td height="30"><table width="100%" height="30" border="0" cellspacing="0" cellpadding="0">
								<tr>
									<td align="left"><strong>浙江创源</strong></td>
									<td align="right" valign="top"><input type="button" title="安全退出" value="" class="close-0" onclick="chkLogout();" onmouseover="this.className='close-1'" onmouseout="this.className='close-0'" /></td>
								</tr>
							</table></td>
					</tr>
					<tr>
						<td height="60">
							<div class="floatleft"><a href="../" target="_blank"><img src="images/index-logo.png" height="56" /></a></div>
							<div id="coolnav" class="floatleft" style="overflow:hidden;">
								<table border="0" cellpadding="0" cellspacing="0">
									<tr>
										<td id="toptab3"><div style="cursor:pointer" onclick="ShowLeftMenus(3);"><img src="images/index-nav-img-1.gif" /><br/>后台首页</div></td>
										<td id="toptab1" class="adminbar" style="display:none"><div style="cursor:pointer" onclick="ShowLeftMenus('1');"><img src="images/index-nav-img-3.gif" /><br/>用户管理</div></td>
										<td id="toptab2" class="hide"><div style="cursor:pointer" onclick="ShowLeftMenus('2');"><img src="images/index-nav-img-4.gif" /><br/>模板管理</div></td>
										<td id="toptab4" class="hide"><div style="cursor:pointer" onclick="ShowLeftMenus('4');"><img src="images/index-nav-img-5.gif" /><br/>邮件群发</div></td>
										<td id="toptab5"><div style="cursor:pointer" onclick="ShowLeftMenus('5');"><img src="images/index-nav-img-7.gif" /><br/>内容管理</div></td>
										<td id="toptab0"><div style="cursor:pointer" onclick="ShowLeftMenus('0');"><img src="images/index-nav-img-2.gif" /><br/>系统管理</div></td>
									</tr></table>
							</div>
						</td>
					</tr>
					<tr>
						<td height="8"></td>
					</tr>
				</table></td>
			<td width="8" background="images/index-top-r.gif"><img src="images/index-top-r.gif" width="8" height="98" /></td>
		</tr>
	</table>
</div>
<textarea id="tplMenuBody" style="display:none">
<table width="231" border="0" align="center" cellpadding="0" cellspacing="0">
{#foreach $T.table as record}
	<tr>
		<td><table width="100%" border="0" cellspacing="0" cellpadding="0">
			<tr>
				<td height="27" id="imgmenu{$T.record.no}" class="menu-title1 imgmenu" onclick="ShowSubMenu({$T.record.no});">{$T.record.title}</td>
			</tr>
			<tr>
				<td><div name="SubMenu" id="submenu{$T.record.no}" class="submenu" style="display: none;">
					<table width="100%" border="0" cellspacing="0" cellpadding="0">
					{#foreach $T.record.table as record2}
					{#if $T.record2.url == "" || $T.record2.url == "#"}
						<tr class="depth{$T.record2.depth}" {#if $T.record2.depth >1} style="display:none;"{#/if}><td height="27" class="parent group{$T.record2.groupid}" url="{$T.record2.url}" groupid="{$T.record2.groupid}" rel="close"><span>{$T.record2.title}</span></td></tr>
					{#else}
						<tr class="depth{$T.record2.depth}" {#if $T.record2.depth >1} style="display:none;"{#/if}><td height="27" class="golink group{$T.record2.groupid}" url="{$T.record2.url}"><span>{$T.record2.title}</span></td></tr>
					{#/if}
					{#/for}
					</table>
				</div></td>
			</tr>
		</table></td>
	</tr>
{#/for}
</table>
</textarea>
<div class="side">
	<table width="232" height="100%" border="0" cellpadding="0" cellspacing="0">
		<tr>
			<td bgcolor="#4B6995" style="width:1px;"></td>
			<td width="231" height="100%" valign="top">
				<div id="menuDiv">
					<table width="231" height="100%" border="0" cellpadding="0" cellspacing="0" align="right" class="menu-box">
						<tr><td height="14" class="menu-top-0"></td></tr>
						<tr><td valign="top" id="ajaxMenuBody" height="*" align="center"></td></tr>
						<tr><td height="14" class="menu-bottom-0"></td></tr>
					</table>
				</div>
			</td>
		</tr>
	</table>
</div>
<div class="main">
	<table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
		<tr>
			<td width="*" height="100%" valign="top"><table width="100%" height="100%" border="0" cellpadding="0" cellspacing="2">
					<tr>
						<td width="100%" height="100%" valign="top"><iframe name="IframeOper" id="IframeOper" height="100%" width="100%" frameborder="0" marginheight="0" marginwidth="0" src=""></iframe></td>
					</tr>
				</table></td>
			<td bgcolor="#4B6995" style="width:1px;"></td>
		</tr>
	</table>
</div>
<div class="bottom">
	<table width="100%" border="0" cellspacing="0" cellpadding="0">
		<tr>
			<td width="8" background="images/index-bottom-l.gif" style="line-height:31px;"><img src="images/index-bottom-l.gif" width="8" height="31" /></td>
			<td height="31" background="images/index-bottom-c.gif" style="line-height:31px;">
				<div class="floatleft">当前管理员：<%=AdminName%></div>
				<div class="floatright" id="time"></div>
			</td>
			<td width="8" background="images/index-bottom-r.gif" style="line-height:31px;"><img src="images/index-bottom-r.gif" width="8" height="31" /></td>
		</tr>
	</table>
</div>
</body>
</html>
