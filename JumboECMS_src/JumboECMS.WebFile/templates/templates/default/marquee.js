Marquee = function(el){  
    el.parentNode.style.overflow = "hidden";  
    el.parentNode.style.position = "relative";  
      
    var tab = el.getElementsByTagName("table")[0];  
    el.parentNode.appendChild(tab);  
    el.parentNode.removeChild(el);  
    el = tab;  
    el.style.position = "absolute";  
    el.style.left = "0px";  
      
    pfun = this;  
      
    this.pauseflag = false;  
    el.onmouseover = function(){pfun.pause()};  
    el.onmouseout = function(){pfun.resume()};  
      
    this.direction = -1;  
      
    this.timer = window.setInterval(function(){  
        if(!pfun.pauseflag){  
            var left = parseInt(el.style.left);  
              
            /* 
             * table ���ܹ���10��ͼƬһ���ſ��� 
             * ����770px,����ʹ��540px�� 
             * ԭ���ǿ��ǵ����3��ͼƬ����ȫ���ɹ��� 
             * �������һ�μ�϶��ʾ�հ� 
             */  
            if(left < 0 && left <= -540){  
                pfun.direction = 1  
            }else if(left >= 0){  
                pfun.direction = -1;  
            }  
            el.style.left = (left + (5*pfun.direction))+"px";  
        }  
    }, 200);  
}  
  
Marquee.prototype = {  
    pause : function(){  
        this.pauseflag = true;  
    },  
      
    resume : function(){  
        this.pauseflag = false;  
    }  
}  
  
/* 
 * check is firefox 2.x 
 */  
if (/Firefox[\/\s](\d+\.\d+)/.test(navigator.userAgent)){  
    var ffversion=new Number(RegExp.$1) // capture x.x portion and store as a number  
     if (ffversion>=2 && ffversion <= 3){  
         var els = document.getElementsByTagName("marquee");  
         for(var i = 0 ; i < els.length; i++)  
             new Marquee(els[i]);  
     }  
}  