Delegate = {
    create: function (obj, func, params){
        var f = function(){
             return func.call(this,params);
        };
        return f;
    }
}

function selectlock(id){
    //document.getElementById('lockx').style.background=document.getElementById(id).style.background;
    document.getElementById('lockbig').style.background=document.getElementById(id).style.background;
}

// We add the "invoke"-Method to Arrays
Array.implement({
	
	invoke: function(fn, args){
		var result = [];
		for (var i = 0, l = this.length; i < l; i++){
			if(this[i] && this[i][fn]){
				result.push(args ? this[i][fn].pass(args, this[i])() : this[i][fn]());
				}
		}
		return result;
	}
	
});

var i;
var firstTime;
var showRefineSearch;
var myArray;
var vHeight;

window.addEvent('domready',init);

function init(){
   // alert("domready");
	i=false;
	showRefineSearch=true;
	firstTime=true;
	var els = $$('div.advanceMed');
	myArray = [new Fx.Tween(els[0]),new Fx.Tween(els[1]),new Fx.Tween(els[2]),new Fx.Tween(els[3])];
	try
	{
		$('link').addEvent('click',Delegate.create(window,onClickHandler));
	}
	catch(err)
	{		
	}
	$('search').addEvent('click',Delegate.create(window,onClickHandler));
	myArray[0].addEvent("onComplete",Delegate.create(window,onCompleteFunction));
	//vHeight=getViewPort().height;
	//getNavElements();
}


function onClickHandler(){
   
    if(firstTime){
        i =(i==true)?false:true; 
        if(showRefineSearch==false){
            document.getElementById('refineSearch').style.visibility="hidden" ;
            document.getElementById('search').style.visibility="visible" ;
            myArray.invoke('start', ['height', '70']);
            showRefineSearch=true;
        }else{
            showRefineSearch=false;
            myArray.invoke('start', ['height', '190']);
        }
       // myArray.invoke('start', ['height', i ? '190' : '70']);
        changeimage('advanced');
        $('link').removeEvent('click',Delegate.create(window,onClickHandler));
        //alert('remove');
        firstTime=false;
    }
}

function onCompleteFunction(e){
    //alert("completo");
    $('link').addEvent('click',Delegate.create(window,onClickHandler));
    //alert('add');
    if(!showRefineSearch){
         document.getElementById('search').style.visibility="hidden" ;
         document.getElementById('refineSearch').style.position="absolute";
         document.getElementById('refineSearch').style.visibility="visible";
    }
   firstTime=true;
}

function changeimage(id){
    bgimageopen = 'url(images/tabMenuOpen.jpg)';
    bgimageclose = 'url(images/tabMenu.jpg)';
    strimg = document.getElementById('link2').style.backgroundImage;//=images/abr.gif)
        if ((strimg.indexOf('tabMenuOpen.jpg') != -1 )){
            document.getElementById('link2').style.backgroundImage = bgimageclose;
         }
        else{
         document.getElementById('link2').style.backgroundImage = bgimageopen; 
   }      
}


/*var myNavs;
var firster=true;
var body=document.getElementsByTagName("body")[0];

function getMyPosition(tagger){
    var naver=tagger.getElementsByTagName("div")[0];
    var ScrollTop = document.body.scrollTop;
    vHeight=getViewPort().height;
    if (ScrollTop == 0){
        if (window.pageYOffset){
            ScrollTop = window.pageYOffset;
        }else{
            ScrollTop = (document.body.parentElement) ? document.body.parentElement.scrollTop : 0;
        }
    }
    
    if(naver!=null || naver!=undefined){
        var intY = getAbsoluteY(naver);
	    if(((intY-ScrollTop)+naver.offsetHeight) > vHeight){
	        //naver.style.marginTop=(intY.top-ScrollTop)-naver.offsetHeight;
	        if(naver.style==undefined){
	            var myStyle = naver.style;
                myStyle.marginTop = (-(naver.offsetHeight-50)).toString()+"px"; 
                naver.setAttribute("style", myStyle);
            }else{
                naver.style.marginTop=(-(naver.offsetHeight-50)).toString()+"px";
            }
	    }
	}
}

function defaultPosition(tagger){
    var naver=tagger.getElementsByTagName("div")[0];
    naver.style.marginTop = "0px";
}

function getAbsoluteY(elm) {  
    var y = 0;  
    if (elm && typeof elm.offsetParent != "undefined") {  
        while (elm && typeof elm.offsetTop == "number") {  
            y += elm.offsetTop;  
            elm = elm.offsetParent;  
        }  
    }  
    return y;   
}

function getViewPort(){
    var viewportwidth;
    var viewportheight;
    var obj=new Object();
    if (typeof window.innerWidth != 'undefined'){
      viewportwidth = window.innerWidth,
      viewportheight = window.innerHeight
    }else if (typeof document.documentElement != 'undefined' && typeof document.documentElement.clientWidth != 'undefined' && document.documentElement.clientWidth != 0)
    {
       viewportwidth = document.documentElement.clientWidth,
       viewportheight = document.documentElement.clientHeight
    }else{
       viewportwidth = document.getElementsByTagName('body')[0].clientWidth,
       viewportheight = document.getElementsByTagName('body')[0].clientHeight
    }
    obj.width=viewportwidth;
    obj.height=viewportheight;
    return obj;
}

function getNavElements(){
    //myNavs=new Array();
    var num=0;
    var navs=document.getElementsByTagName("li");
    for(var i=0;i<navs.length;++i){
        if(navs[i].id=="li"+num){
            //alert(num);
            navs[i].onmouseover=Delegate.create(navs[i],onNavOver);
            navs[i].onmouseout=Delegate.create(navs[i],onNavOut);
            ++num;
        }    
    }
}

function onNavOver(){
    //alert("El id: "+this.id+"||"+this);
    var estrellita=document.getElementById(this.id);
    if(estrellita!=null || estrellita!=undefined){
        getMyPosition(estrellita);
    }
}

function onNavOut(){
    //alert("El id: "+this.id+"||"+this);
    var estrellita=document.getElementById(this.id);
    if(estrellita!=null || estrellita!=undefined){
        defaultPosition(estrellita);
    }
}*/

