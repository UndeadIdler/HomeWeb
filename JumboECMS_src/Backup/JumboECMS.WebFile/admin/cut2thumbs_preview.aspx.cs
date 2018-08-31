/*
 * 程序中文名称: 将博内容管理系统企业版
 * 
 * 程序英文名称: JumboECMS
 * 
 * 程序版本: 1.4.x
 * 
 * 程序作者: 将博
 * 
 * 官方网站: http://www.jumboecms.net/
 * 
 */

using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using JumboECMS.Common;
namespace JumboECMS.WebFile.Admin
{
    public partial class _cut2thumbs_preview : JumboECMS.UI.AdminCenter
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ModuleType = q("module").Trim();
            Admin_Load("", "html", ModuleType);
            if (!Page.IsPostBack)
            {
                string TempPhoto = q("tphoto");
                System.Drawing.Image originalImage = System.Drawing.Image.FromFile(Server.MapPath(TempPhoto));
                int imgWidth = originalImage.Width;
                int imgHeight = originalImage.Height;
                originalImage.Dispose();
                TempPhoto += "?" + System.DateTime.Now.ToString("yyyyMMddHHmmss");
                string ToWidth = q("tow");
                string ToHeight = q("toh");
                string printhtml = "";
                printhtml += "<html xmlns:v>\r\n";
                printhtml += "<body>\r\n";
                printhtml += "<style>\r\n";
                printhtml += "#tbHole td{background:black;filter:alpha(opacity=60);-moz-opacity:0.6;}\r\n";
                printhtml += "#tbHole img{width:1;height:1;visibility:hidden}\r\n";
                printhtml += "v\\:*{Behavior:url(#default#VML)}\r\n";
                printhtml += "</style>\r\n";
                printhtml += "<div id='bxHole' onselectstart='return(false)' ondragstart='return(false)' onmousedown='return(false)' oncontextmenu='return(false)' style='position:absolute;left:0;top:0;width:" + Convert.ToString(imgWidth + 4) + ";height:" + Convert.ToString(imgHeight + 4) + ";border:1px solid #808080;background:url(" + TempPhoto + ")'>\r\n";
                printhtml += "    <table id='tbHole' cellpadding='0' cellspacing='0' width='100%' height='100%' style='position:absolute'>\r\n";
                printhtml += "        <tr height='1'><!--高度为0时居中，否则top为其值-->\r\n";
                printhtml += "            <td width='1'><img></td><!--宽度为0时居中，否则left为其值-->\r\n";
                printhtml += "            <td width='" + ToWidth + "'><img></td>\r\n";
                printhtml += "            <td><img></td>\r\n";
                printhtml += "        </tr>\r\n";
                printhtml += "        <tr height='" + ToHeight + "'>\r\n";
                printhtml += "            <td><img></td>\r\n";
                printhtml += "            <td onmousedown=$('bxHole').dragStart(event,0) style='background:transparent;filter:;-moz-opacity:1;cursor:move;border:1px dashed white !important'><img></td>\r\n";
                printhtml += "            <td><img></td>\r\n";
                printhtml += "        </tr>\r\n";
                printhtml += "        <tr>\r\n";
                printhtml += "            <td><img></td>\r\n";
                printhtml += "            <td><img></td>\r\n";
                printhtml += "            <td><img></td>\r\n";
                printhtml += "        </tr>\r\n";
                printhtml += "    </table>\r\n";
                printhtml += "    <img id='bxHoleMove1' src='images/41_ie7ub8xprga8.gif' onmousedown=$('bxHole').dragStart(event,1) style='cursor:nw-resize;position:absolute;width:5;height:5;border:1px dashed white;background:#BCBCBC'>\r\n";
                printhtml += "    <img id='bxHoleMove2' src='images/41_ie7ub8xprga8.gif' onmousedown=$('bxHole').dragStart(event,2) style='cursor:sw-resize;position:absolute;width:5;height:5;border:1px dashed white;background:#BCBCBC'>\r\n";
                printhtml += "    <img id='bxHoleMove3' src='images/41_ie7ub8xprga8.gif' onmousedown=$('bxHole').dragStart(event,3) style='cursor:nw-resize;position:absolute;width:5;height:5;border:1px dashed white;background:#BCBCBC'>\r\n";
                printhtml += "    <img id='bxHoleMove4' src='images/41_ie7ub8xprga8.gif' onmousedown=$('bxHole').dragStart(event,4) style='cursor:sw-resize;position:absolute;width:5;height:5;border:1px dashed white;background:#BCBCBC'> </div>\r\n";
                //
                printhtml += "<div id='bxImgHoleShow' style='position:absolute;left:" + (imgWidth + 12) + "px;top:0px;width:" + ToWidth + "px;height:" + ToHeight + "px;border:1px solid #808080;overflow:hidden;'></div>\r\n";
                printhtml += "<div id='resultTxtShow'></div>\r\n";
                printhtml += "</body>\r\n";
                printhtml += "</html>\r\n";
                printhtml += "<script>\r\n";
                printhtml += "function $(obj){\r\n";
                printhtml += "    return typeof(obj)=='object'?'obj':document.getElementById(obj);\r\n";
                printhtml += "}\r\n";
                printhtml += "function GetScrollTop() {\r\n";
                printhtml += "	if (self.pageYOffset){return self.pageYOffset;}\r\n";
                printhtml += "	else if (document.documentElement && document.documentElement.scrollTop){return document.documentElement.scrollTop;}\r\n";
                printhtml += "	else if (document.body){return document.body.scrollTop;}\r\n";
                printhtml += "}\r\n";
                printhtml += "bxHole_ini();\r\n";
                printhtml += "window.onscroll=function(){$('bxImgHoleShow').style.top = GetScrollTop();}\r\n";
                printhtml += "function bxHole_ini(){\r\n";
                printhtml += "    var bx=$('bxHole'),tb=$('tbHole')\r\n";
                //
                printhtml += "    $('bxImgHoleShow').innerHTML=\"<\"+(document.all?'v:image':'img')+\" id='imgHoleShow' src='" + TempPhoto + "' style='position:absolute;left:0;top:0;width:" + imgWidth + "px;height:" + imgHeight + "px;' />\";\r\n";
                printhtml += "    bx.w0=tb.rows[0].cells[1].offsetWidth;\r\n";
                printhtml += "    bx.h0=tb.rows[1].offsetHeight;\r\n";
                //
                printhtml += "    bx.w_img=$('imgHoleShow').offsetWidth;\r\n";
                //
                printhtml += "    bx.h_img=$('imgHoleShow').offsetHeight;\r\n";
                //printhtml += "    bx.w_img=" + ToWidth + ";\r\n";
                //printhtml += "    bx.h_img=" + ToHeight + ";\r\n";
                printhtml += "    bx.dragStart=function(e,dragType){\r\n";
                printhtml += "        bx.dragType=dragType;\r\n";
                printhtml += "        bx.px=tb.rows[0].cells[0].offsetWidth;\r\n";
                printhtml += "        bx.py=tb.rows[0].offsetHeight;\r\n";
                printhtml += "        bx.pw=tb.rows[0].cells[1].offsetWidth;\r\n";
                printhtml += "        bx.ph=tb.rows[1].offsetHeight;\r\n";
                printhtml += "        bx.sx=e.screenX;\r\n";
                printhtml += "        bx.sy=e.screenY;\r\n";
                printhtml += "    }\r\n";
                printhtml += "    bx.onmouseup=function(){\r\n";
                printhtml += "        if(bx.dragType==null)\r\n";
                printhtml += "            return;\r\n";
                printhtml += "        var w=tb.rows[0].cells[1].offsetWidth;\r\n";
                printhtml += "        var h=tb.rows[1].offsetHeight;\r\n";
                printhtml += "        bx.dragType=null;\r\n";
                printhtml += "        if(w/h>bx.w0/bx.h0)\r\n";
                printhtml += "            tb.rows[0].cells[1].style.width=h*bx.w0/bx.h0;\r\n";
                printhtml += "        else\r\n";
                printhtml += "            tb.rows[1].style.height=w*bx.h0/bx.w0;\r\n";
                printhtml += "        bx.setTip();\r\n";
                printhtml += "    }\r\n";
                printhtml += "    bx.onmousemove=function(e){\r\n";
                printhtml += "        var x,y,w,h;\r\n";
                printhtml += "        if(bx.dragType==null)\r\n";
                printhtml += "            return;\r\n";
                printhtml += "        if(e==null)\r\n";
                printhtml += "            e=event;\r\n";
                printhtml += "        x=Math.max(bx.px+e.screenX-bx.sx,1);\r\n";
                printhtml += "        y=Math.max(bx.py+e.screenY-bx.sy,1);\r\n";
                printhtml += "        w=Math.min(bx.pw+e.screenX-bx.sx,tb.offsetWidth-bx.px-1);\r\n";
                printhtml += "        h=Math.min(bx.ph+e.screenY-bx.sy,tb.offsetHeight-bx.py-1);\r\n";
                printhtml += "        if(bx.dragType==0){\r\n";
                printhtml += "            x=Math.min(x,tb.offsetWidth-bx.pw-1);\r\n";
                printhtml += "            y=Math.min(y,tb.offsetHeight-bx.ph-1);\r\n";
                printhtml += "            w=bx.pw;\r\n";
                printhtml += "            h=bx.ph;\r\n";
                printhtml += "        }\r\n";
                printhtml += "        if(bx.dragType==1||bx.dragType==4)\r\n";
                printhtml += "            w=bx.pw+bx.px-x;\r\n";
                printhtml += "        if(bx.dragType==1||bx.dragType==2)\r\n";
                printhtml += "            h=bx.ph+bx.py-y;\r\n";
                printhtml += "        if(bx.dragType==2||bx.dragType==3)\r\n";
                printhtml += "            x=bx.px;\r\n";
                printhtml += "        if(bx.dragType==3||bx.dragType==4)\r\n";
                printhtml += "            y=bx.py;\r\n";
                printhtml += "        w=Math.max(w,bx.w0/2);//最小宽，bx.w0/2表示一半\r\n";
                printhtml += "        h=Math.max(h,bx.h0/2);//最小高，bx.h0/2表示一半\r\n";
                printhtml += "        if(bx.dragType==1||bx.dragType==4)\r\n";
                printhtml += "            x=bx.pw+bx.px-w;\r\n";
                printhtml += "        if(bx.dragType==1||bx.dragType==2)\r\n";
                printhtml += "            y=bx.ph+bx.py-h;\r\n";
                printhtml += "        tb.rows[0].cells[0].style.width=x;\r\n";
                printhtml += "        tb.rows[0].cells[1].style.width=w;//改变宽\r\n";
                printhtml += "        tb.rows[0].style.height=y;\r\n";
                printhtml += "        tb.rows[1].style.height=h;//改变高\r\n";
                printhtml += "        $('bxHole').setTip();\r\n";
                printhtml += "    }\r\n";
                printhtml += "    bx.setTip=function(){\r\n";
                printhtml += "        var x=tb.rows[0].cells[0].offsetWidth;\r\n";
                printhtml += "        var y=tb.rows[0].offsetHeight;\r\n";
                printhtml += "        var w=tb.rows[0].cells[1].offsetWidth;\r\n";
                printhtml += "        var h=tb.rows[1].offsetHeight;\r\n";
                //
                printhtml += "        var img=$('imgHoleShow');\r\n";
                printhtml += "        var per;\r\n";

                printhtml += "        $('bxHoleMove1').style.left=$('bxHoleMove4').style.left=x-3+'px';\r\n";
                printhtml += "        $('bxHoleMove1').style.top=$('bxHoleMove2').style.top=y-3+'px';\r\n";
                printhtml += "        $('bxHoleMove2').style.left=$('bxHoleMove3').style.left=x+w-4+'px';\r\n";
                printhtml += "        $('bxHoleMove3').style.top=$('bxHoleMove4').style.top=y+h-4+'px';\r\n";

                printhtml += "        if(w/h>bx.w0/bx.h0)\r\n";
                printhtml += "            w=h*bx.w0/bx.h0;\r\n";
                printhtml += "        else\r\n";
                printhtml += "            h=w*bx.h0/bx.w0;\r\n";
                printhtml += "        per=bx.h0/h;\r\n";
                //
                printhtml += "        img.style.width=per*bx.w_img;\r\n";
                //
                printhtml += "        img.style.height=per*bx.h_img;\r\n";
                //
                printhtml += "        img.style.left=-x*per;\r\n";
                //
                printhtml += "        img.style.top=-y*per;\r\n";
                printhtml += "        parent.topFrame.$('x').value = Math.round((x-1));\r\n";
                printhtml += "        parent.topFrame.$('y').value = Math.round((y-1));\r\n";
                printhtml += "        parent.topFrame.$('w').value = Math.round(w);\r\n";
                printhtml += "        parent.topFrame.$('h').value = Math.round(h);\r\n";
                printhtml += "    }\r\n";
                printhtml += "    //bx.setTip();\r\n";
                printhtml += "}\r\n";
                printhtml += "</script>\r\n";
                Response.Write(printhtml);
            }
        }
    }
}
