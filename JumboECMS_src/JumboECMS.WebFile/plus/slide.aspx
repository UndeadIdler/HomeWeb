<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="slide.aspx.cs" Inherits="JumboECMS.WebFile.Plus._slide" %>
function $i(el)
{
	if(typeof el=='string')
		return document.getElementById(el);
	else if(typeof el=='object')
		return el;
} 
var Image=new Array();
var Pics="<%=Pics%>";
var Links="<%=Links%>";
var Titles="";
var Alts="";
var Apic<%=ID%>=Pics.split('|');
var ALink<%=ID%>=Links.split('|');
var ATitle<%=ID%>=Titles.split('|');
var AAlts<%=ID%>=Alts.split('|');
var text_height=0;
var Total_Item=Apic<%=ID%>.length;
var SlideHtml;
for(i=0;i<Total_Item;i++)
{
	Image.src = Apic<%=ID%>[i]; 
}
 
 
 
 
var speed_<%=ID%>=3500;
var slide_currentitem_<%=ID%>=0; 
var Title="",Link="";
var slide_Time_<%=ID%>;
function LoadSlideBox_<%=ID%>()
{
	SlideHtml='<a href="#" id="slide_link_<%=ID%>" target="_self"><img class="slide_image" style="display:block;filter:revealTrans(duration=1,transition=20);display:none;border:0px solid #cccccc;width:<%=Width%>px;height:<%=Height%>px"  name="slide_pic_<%=ID%>" id="slide_pic_<%=ID%>"></a>';
	SlideHtml+='<ul  class="slide_item" style="display:none">';
	for(i=0;i<Total_Item;i++)
	{
		SlideHtml+='<li id="slide_num_<%=ID%>_'+i+'" onclick="Change_Num_<%=ID%>('+i+')">'+(i+1)+'</li>';
	}
	SlideHtml+='</ul>';
	document.write("<div id='slide_box_<%=ID%>' class='slide_box' style='width:<%=Width%>px;height:<%=Height%>px'>"+SlideHtml+"</div><div style='width:<%=Width%>px;text-align:center;display:none' id='slide_title_<%=ID%>'></div>");
	document.images["slide_pic_<%=ID%>"].src=Apic<%=ID%>[0];
	document.images["slide_pic_<%=ID%>"].style.display="inline";
 
 
	Title="<a href='"+ALink<%=ID%>[0]+"' class='slide_title' target='_self' title=\""+AAlts<%=ID%>[0]+"\">"+ATitle<%=ID%>[0]+"</a>";
	$i("slide_link_<%=ID%>").href=ALink<%=ID%>[0];
	if(text_height!="0")
	{
		$i("slide_title_<%=ID%>").style.display="";
		$i("slide_title_<%=ID%>").innerHTML=Title;
	}
 
 
	$i("slide_num_<%=ID%>_0").className="current";
	slide_Time_<%=ID%>=setInterval(nextAd_<%=ID%>,speed_<%=ID%>);
	$i("slide_box_<%=ID%>").onmouseover=function() {clearInterval(slide_Time_<%=ID%>)}
	$i("slide_box_<%=ID%>").onmouseout=function() {slide_Time_<%=ID%>=setInterval(nextAd_<%=ID%>,speed_<%=ID%>)}
 
}
 
function setTransition_<%=ID%>()
{
	if (document.all)
	{
		document.images["slide_pic_<%=ID%>"].filters.revealTrans.Transition=23;
		document.images["slide_pic_<%=ID%>"].filters.revealTrans.apply();
	}
}
 
 
function playTransition_<%=ID%>()
{
	if(document.all)
	document.images["slide_pic_<%=ID%>"].filters.revealTrans.play()
}
 
 
function Control_Num_<%=ID%>(Currentnum)
{
	for(i=0;i<Apic<%=ID%>.length;i++)
	{
		$i("slide_num_<%=ID%>_"+i).className="";
	}
	$i("slide_num_<%=ID%>_"+Currentnum).className="current";
}
function Change_Num_<%=ID%>(Currentnum)
 {
  for(i=0;i<Total_Item;i++)
  {
   $i("slide_num_<%=ID%>_"+i).className="";
  }
  $i("slide_num_<%=ID%>_"+Currentnum).className="current";
  slide_currentitem_<%=ID%>=Currentnum;
  ShowFocus_<%=ID%>(Currentnum);
 }
 
function nextAd_<%=ID%>()
{
  if(Apic<%=ID%>.length<=1)
   {
     clearInterval(slide_Time_<%=ID%>);
     return;
   }
 
  if(slide_currentitem_<%=ID%><Apic<%=ID%>.length-1)
    {
      slide_currentitem_<%=ID%>++;
    }
  else 
   {
     slide_currentitem_<%=ID%>=0;
   }
 ShowFocus_<%=ID%>(slide_currentitem_<%=ID%>);
}
 
 
function ShowFocus_<%=ID%>(slide_currentitem_<%=ID%>)
 {
  if(ATitle<%=ID%>.length>slide_currentitem_<%=ID%>)
   {
     if(ALink<%=ID%>.length>slide_currentitem_<%=ID%>)
      {
       Title="<a href='"+ALink<%=ID%>[slide_currentitem_<%=ID%>]+"' class='slide_title' target='_self' title=\""+AAlts<%=ID%>[slide_currentitem_<%=ID%>]+"\">"+ATitle<%=ID%>[slide_currentitem_<%=ID%>]+"</a>";
      }
     else
      {
       Title=ATitle<%=ID%>[slide_currentitem_<%=ID%>]; 
      }
   }
  else
   {
       Title=""; 
   }
 
  if(ALink<%=ID%>.length>slide_currentitem_<%=ID%>)
   {
      Link=ALink<%=ID%>[slide_currentitem_<%=ID%>];
   }
  else
   {
      Link="#";
   }
 
  $i("slide_link_<%=ID%>").href=Link;
  $i("slide_title_<%=ID%>").innerHTML=Title;
 
  setTransition_<%=ID%>();
  document.images["slide_pic_<%=ID%>"].src=Apic<%=ID%>[slide_currentitem_<%=ID%>];
  $i("slide_title_<%=ID%>").innerHTML=Title;
  playTransition_<%=ID%>();
  Control_Num_<%=ID%>(slide_currentitem_<%=ID%>);
 }
 
LoadSlideBox_<%=ID%>();

