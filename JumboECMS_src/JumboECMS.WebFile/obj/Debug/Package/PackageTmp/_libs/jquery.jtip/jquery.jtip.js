(function($) {
$.fn.jtip = function(opts) {

opts = $.extend({fade: false, gravity: 'r'}, opts || {});
var tip = null, cancelHide = false;

this.hover(function() {
if($("#jtip").is("div")){ return; }//防止排得很挤时频繁出现而突兀
if($(this).attr('tip') && $(this).attr('tip') !="")
{
$.data(this, 'cancel.jtip', true);
var tip = $.data(this, 'active.jtip');
if (!tip) {
tip = $('<div id="jtip" class="jtip"><div id="jtip-inner" class="jtip-inner">' + $(this).attr('tip') + '</div></div>');
tip.css({position: 'absolute', zIndex: 1000});
$(this).attr('title', '');
$.data(this, 'active.jtip', tip);
}
var pos = $.extend({}, $(this).offset(), {width: this.offsetWidth, height: this.offsetHeight});
tip.remove().css({top: 0, left: 0, visibility: 'hidden', display: 'block'}).appendTo(document.body);
var actualWidth = tip[0].offsetWidth, actualHeight = tip[0].offsetHeight;
switch (opts.gravity.charAt(0)) {
case 'b':
//tip.css({top: pos.top + pos.height, left: pos.left + pos.width});
tip.css({top: pos.top + pos.height, left: pos.left});
break;
case 't':
//tip.css({top: pos.top - actualHeight, left: pos.left + pos.width});
tip.css({top: pos.top - actualHeight, left: pos.left});
break;
case 'l':
tip.css({top: pos.top + pos.height, left: pos.left - actualWidth});
break;
case 'r':
//tip.css({top: pos.top + pos.height, left: pos.left + pos.width});
tip.css({top: pos.top + pos.height, left: pos.left});
break;
}
if (opts.fade) {
tip.css({opacity: 0, display: 'block', visibility: 'visible'}).animate({opacity: 1});
} else {
tip.css({visibility: 'visible'});
}
bgiframe = $('<iframe class="bgiframe" frameborder="0" scrolling="no" src="" style="display:block;position:absolute;z-index:999;'+
'top:'+($('#jtip').offset().top)+'px;'+
'left:'+($('#jtip').offset().left)+'px;'+
'width:'+(tip[0].offsetWidth-2)+'px;'+
'height:'+(tip[0].offsetHeight-2)+'px;'+
'"></iframe>');
bgiframe.appendTo(document.body);
}

}, function() {
if($(this).attr('tip') && $(this).attr('tip') !="")
{
$.data(this, 'cancel.jtip', false);
var self = this;
setTimeout(function() {
if ($.data(this, 'cancel.jtip')) return;
var tip = $.data(self, 'active.jtip');
if (opts.fade) {
tip.stop().fadeOut(function() { $(this).remove(); });
} else {
tip.remove();
}
$('iframe.bgiframe').remove();
}, 2);
}
});

};
})(jQuery);
