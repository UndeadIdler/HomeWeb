

/*设置为主页*/
function _jcms_SetHome(obj, vrl) {
    vrl = encodeURI(vrl);
    try {
        document.body.style.behavior = 'url(#default#homepage)'; document.body.setHomePage(vrl);
    }
    catch (e) {
        if (window.netscape) {
            try {
                netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect");
            }
            catch (e) {
                alert("此操作被浏览器拒绝！\n请在浏览器地址栏输入“about:config”并回车\n然后将 [signed.applets.codebase_principal_support]的值设置为'true',双击即可。");
            }
            var prefs = Components.classes['@mozilla.org/preferences-service;1'].getService(Components.interfaces.nsIPrefBranch);
            prefs.setCharPref('browser.startup.homepage', vrl);
        } else {
            alert("您的浏览器不支持，请按照下面步骤操作：1.打开浏览器设置。2.点击设置网页。3.输入：" + vrl + "点击确定。");
        }
    }
}

/*加入收藏夹*/
function _jcms_addFavorite(sTitle, sURL) {
    sURL = encodeURI(sURL);
    try {
        window.external.addFavorite(sURL, sTitle);
    }
    catch (e) {
        try {
            window.sidebar.addPanel(sTitle, sURL, "");
        }
        catch (e) {
            alert("你可以按Ctrl+D键收藏本页！");
        }
    }
}

function ShowSubMenu(id, num) //显示下拉
 {
   if(typeof(submenu_style)=="undefined")
    {
      submenu_style=2;  //1表示纵向下拉，2表示横向下拉,其他数值则关闭
    }
   switch(submenu_style)
    {
      case 1:
       document.write('<link rel="stylesheet" type="text/css" href="/template/cn/css/dropmenu.css" />');
       if(document.all){addHover("MainMenu","li","hover")}
      break;

     case 2:
       document.write('<link rel="stylesheet" type="text/css" href="/template/cn/css/submenu.css" />');
       HorisontalSubMenu(id,num);
     break;
   }
 }

function addHover(id,tag,classname) //IE增加hover效果
 {
    var sfEls =$i(id).getElementsByTagName(tag);
    for (var i=0; i<sfEls.length;i++) 
        {
          sfEls[i].onmouseover=function() 
            {
             this.className+=" "+classname;
            }
          sfEls[i].onmouseout=function() {
          this.className=this.className.replace(new RegExp("( ?|^)"+classname+"\\b"),"");
        }
      }
 }


function HorisontalSubMenu(id,num)
 {
   var classname="hover";
   var MenuItem=document.getElementsByName("MainMenuItem");
   var firstA;
   if(MenuItem.length<1)
   {
     return;
   }
   for(i=0;i<MenuItem.length;i++)
    {
      firstA=MenuItem[i].getElementsByTagName("a")[0];
      firstA.className="menu_"+num;
      MenuItem[i].className=MenuItem[i].className.replace(new RegExp("( ?|^)"+classname+"\\b"),"");
      if(MenuItem[i].getElementsByTagName("a")[0].id=="Menu_"+id)
       {
         MenuItem[i].className+=" "+classname;
         firstA.className="menu_current_"+num;
       }
      firstA.onmouseover=function(){HorisontalSubMenu(this.id.replace("Menu_",""),num)};
    }
 }

function FontZoom(Size,LineHeight,Id)
 {
   var Obj=$i(Id);
   Obj.style.fontSize=Size; 
   Obj.style.lineHeight=LineHeight; 
 }

//图片放大
function previewImgShow(obj,src,MaxWidth,MaxHeight)
{
if(!obj)return;
var floatObj=$i("floatPreviewImg");
floatObj.innerHTML="<img src='"+src+"' id='PreviewImg' style='cursor:pointer' />";
floatObj.style.left=getOffsetLeft(obj)+"px";
floatObj.style.top=getOffsetTop(obj)+"px";
floatObj.style.display="";
setPicRange($i("PreviewImg"),MaxWidth,MaxHeight);
}

function getOffsetTop(obj) {

var n = obj.offsetTop;
while (obj = obj.offsetParent) n+= obj.offsetTop;
return n;

}

function getOffsetLeft(obj) {

var n = obj.offsetLeft;
while (obj = obj.offsetParent) n+= obj.offsetLeft;
return n;
}

function setPicRange(obj,maxW,maxH)
{
 if(obj.width>maxW || obj.height>maxH )
 {
  if(obj.width/obj.height>maxW/maxH  )
   obj.width=maxW;
  else 
   obj.height=maxH;
 }
}


//滚动插件
function PARoll(a)
{
	this.TheA = a;
	this.TheA.IsPlay = 1;
	this.$(a.box).style.overflow = "hidden";
                if(a.width!="")
                 {this.$(a.box).style.width = a.width;}
                if(a.height!="")
                 {this.$(a.box).style.height = a.height; }
	this.$(a.box2).innerHTML=this.$(a.box1).innerHTML;
	this.$(a.box).scrollTop=this.$(a.box).scrollHeight;
        this.$(this.TheA.box).scrollTop=0;
	this.Marquee();
	this.$(a.box).onmouseover=function() {eval(a.objname+".clearIntervalRoll();");}
	this.$(a.box).onmouseout=function() {eval(a.objname+".setTimeoutRoll();")}
}
PARoll.prototype.$ = function(Id)
{
	return $i(Id);
}
PARoll.prototype.getV = function(){ 
alert(this.$(this.TheA.box2).offsetWidth-this.$(this.TheA.box).scrollLeft);
alert(this.$(this.TheA.box2).offsetWidth);
alert(this.$(this.TheA.box).scrollLeft);}
PARoll.prototype.Marquee = function()
{
	this.MyMar=setTimeout(this.TheA.objname+".Marquee();",this.TheA.speed);
	if(this.TheA.IsPlay == 1)
	{
		if(this.TheA.direction == "top")
		{
			if(this.$(this.TheA.box).scrollTop>=this.$(this.TheA.box2).offsetHeight)
				this.$(this.TheA.box).scrollTop-=this.$(this.TheA.box2).offsetHeight;
			else{
				this.$(this.TheA.box).scrollTop++;
			}
		}
		
		if(this.TheA.direction == "down")
		{
			if(this.$(this.TheA.box1).offsetTop-this.$(this.TheA.box).scrollTop>=0)
				this.$(this.TheA.box).scrollTop+=this.$(this.TheA.box2).offsetHeight;
			else{
				this.$(this.TheA.box).scrollTop--;
			}
		}
		if(this.TheA.direction == "left")
		{
			if(this.$(this.TheA.box2).offsetWidth-this.$(this.TheA.box).scrollLeft<=0)
				this.$(this.TheA.box).scrollLeft-=this.$(this.TheA.box1).offsetWidth;
			else{
				this.$(this.TheA.box).scrollLeft++;
			}
		}
		
		if(this.TheA.direction == "right")
		{
			if(this.$(this.TheA.box).scrollLeft<=0)
				this.$(this.TheA.box).scrollLeft+=this.$(this.TheA.box2).offsetWidth;
			else{
				this.$(this.TheA.box).scrollLeft--;
			}
		}

	}
}
PARoll.prototype.clearIntervalRoll = function()
{
  this.TheA.IsPlay = 0;
}
PARoll.prototype.setTimeoutRoll = function()
{
   this.TheA.IsPlay = 1;
}

//基础函数



function Trim(str)  //去除空格 
 { 
  var Astr=str.split('');
  var str1="";
  for(i=0;i<Astr.length;i++)
   {
    str1+=Astr[i].replace(" ","");
   }
  return str1;
 }


function ShowItem(id,url)
 {
    var obj=$i(id);
    if(url!="#" || obj==null)
     {
       return;
     }
    if(obj.style.display=="none")
     {
      obj.style.display="";
     }
   else
     {
      obj.style.display="none";
     }
 }

function IsChecked(obj)  //检测radid或checkbox是否有选择
{
 var k=0;
 for(k=0;k<obj.length;k++) 
  { 
   if(obj[k].checked) 
    {
     return true;
    }
  }
 return false;
} 



function IsNum(str)  //是否是数字
 {
   var str1="0123456789";
   var Astr=str.split('');
   for(i=0;i<Astr.length;i++)
    {
      if(str1.indexOf(Astr[i])<0)
       {
        return false;
       }
    }
  return true;

 }

function IsDate(str)   
 {  
 
  var reg1=/^(\d{1,2})\/(\d{1,2})\/(\d{4})$/; 
  var reg2=/^(\d{1,2})\/(\d{1,2})\/(\d{4}) (\d{1,2}):(\d{1,2}):(\d{1,2})$/; 
  var reg3=/^(\d{1,2})\/(\d{1,2})\/(\d{4}) (\d{1,2}):(\d{1,2}):(\d{1,2}) ([a-zA-Z]{0,2})$/; 
  var reg4=/^(\d{4})-(\d{1,2})-(\d{1,2})$/;    
  var reg5=/^(\d{4})-(\d{1,2})-(\d{1,2}) (\d{1,2}):(\d{1,2}):(\d{1,2})$/;   
  if(str=="")
   {
     return  false;  
   } 
  if(!reg1.test(str) && !reg2.test(str) && !reg3.test(str) && !reg4.test(str) && !reg5.test(str))
   {    
      return  false;   
   }   
   return true;   
  }   



function replaceAll(str,str1,str2)
{
  str=str.toLowerCase();
  while(str.indexOf(str1)>= 0)
  {
   str=str.replace(str1,str2);
  }
  return str;
}




