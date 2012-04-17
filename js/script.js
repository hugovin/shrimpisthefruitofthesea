var accordion=function(){
	var tm=sp=10;
	function slider(n){this.nm=n; this.arr=[];}
	slider.prototype.init=function(t,c,k){
		var a,h,s,l,i; a=document.getElementById(t); 
		this.sl=k?k:''; 
		this.container = a;
		h=a.getElementsByTagName('dt'); 
		s=a.getElementsByTagName('dd'); 
		this.l=h.length;
		for(i=0;i<this.l;i++){var d=h[i]; this.arr[i]=d; 
		d.onclick=new Function(this.nm+'.pro(this)'); 
		if(c==i){d.className=this.sl}else{d.className = 'closed';}
		}
		l=s.length;
		var active;
		if(c < l){ active = s[c];
		}
		for(i=0;i<l;i++){var d=s[i]; 
		d.mh=d.offsetHeight; 
		if(c!=i){ d.style.height=0; 
		d.style.display='none'; }
		}
	}
	slider.prototype.open=function(index){
	    h=this.container.getElementsByTagName('dt'); s=this.container.getElementsByTagName('dd');
	    for(i=0;i<this.l;i++){ if(index == i){ this.pro(h[index]); }}
	}
	slider.prototype.pro=function(d){
		for(var i=0;i<this.l;i++){
			var h=this.arr[i], s=h.nextSibling; 
			s=s.nodeType!=1?s.nextSibling:s; 
			clearInterval(s.tm);
			if(h==d&&s.style.display=='none'){
				s.style.display=''; 
				s.style.overflow = "visible"; 
				su(s,1); h.className=this.sl}
			else if(s.style.display==''){ 
			s.style.overflow = "hidden";
			su(s,-1); h.className='closed';}
		}
		
	}
	function su(c,f){c.tm=setInterval(function(){sl(c,f)},tm)}
	function sl(c,f){
		var h=c.offsetHeight, m=c.mh, d=f==1?m-h:h; 
		c.style.height=h+(Math.ceil(d/sp)*f)+'px';
		c.style.visibility='hidden';
		if(f==1&&h>=m){
			c.className='open'; 
			c.style.visibility='visible';
			clearInterval(c.tm)
			}else if(f!=1&&h==1){
				c.style.display='none'; 
				c.className = 'closed'; 
				clearInterval(c.tm)}
	}
	return{slider:slider}
}();



var DDSPEED = 10;
var DDTIMER = 10;
var OFFSET = -10;
var ZINT = 100;

function ddMenu(id,d){
  var h = document.getElementById(id + '-ddheader');
  var c = document.getElementById(id + '-ddcontent');
  clearInterval(c.timer);
  if(d == 1){
    clearTimeout(h.timer);
    c.style.display = 'block';
    if(c.maxh && c.maxh <= c.offsetHeight){return}
    else if(!c.maxh){
      c.style.left = (h.offsetWidth + OFFSET) + 'px';
      c.style.height = 'auto';
      c.maxh = c.offsetHeight;
      c.style.height = '0px';
    }
    ZINT = ZINT + 1;
    c.style.zIndex = ZINT;
    c.timer = setInterval(function(){ddSlide(c,1)},DDTIMER);
  }else{
    h.timer = setTimeout(function(){ddCollapse(c)},50);
  }
}

function ddCollapse(c){
  c.timer = setInterval(function(){ddSlide(c,-1)},DDTIMER);
}

function cancelHide(id){
  var h = document.getElementById(id + '-ddheader');
  var c = document.getElementById(id + '-ddcontent');
  clearTimeout(h.timer);
  clearInterval(c.timer);
  if(c.offsetHeight < c.maxh){
    c.timer = setInterval(function(){ddSlide(c,1)},DDTIMER);
  }
}

function ddSlide(c,d){
  var currh = c.offsetHeight;
  var dist;
  if(d == 1){
    dist = Math.round((c.maxh - currh) / DDSPEED);
  }else{
    dist = Math.round(currh / DDSPEED);
  }
  if(dist <= 1 && d == 1){
    dist = 1;
  }
  c.style.height = currh + (dist * d) + 'px';
    if(currh > (c.maxh - 2) && d == 1){
    clearInterval(c.timer);
  }else if(dist < 1 && d != 1){
    clearInterval(c.timer);
    c.style.display = 'none';
  }
}