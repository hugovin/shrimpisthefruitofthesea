// Mootools Extensions
// String.trim, ltrim, rtrim
// Array.insert
// (c) Jordan Sherer 2008

Array.prototype.getFirst = function(){
    if(this.length > 0)
    {
        return this[0];
    }
}

Array.prototype.insert = function(obj, index){
    lastIndex = this.length - 1;
    if(index > lastIndex){ index = lastIndex; }
    this.splice(index, 0, obj);
};

String.prototype.trim = function (c) {
    if(!c)
    {
        return this.replace(/^\s*/, "").replace(/\s*$/, "");
    }
    else
    {
        var reBegining = RegExp("^"+c.escapeRegExp()+"*");
        var reEnding = RegExp(c.escapeRegExp()+"*$");
        return this.replace(reBegining, "").replace(reEnding, "");
    }
};

String.prototype.ltrim = function (c) {
    if(!c)
    {
        return this.replace(/^\s*/, "");
    }
    else
    {
        var reBegining = RegExp("^"+c.escapeRegExp()+"*");
        return this.replace(reBegining, "");
    }
};

String.prototype.rtrim = function (c) {
    if(!c)
    {
        return this.replace(/\s*$/, "");
    }
    else
    {
        var reEnding = RegExp(c.escapeRegExp()+"*$");
        return this.replace(reEnding, "");
    }
};

String.prototype.replaceRegex = function(searchTerm, replaceWith, ignoreCase ){
    var regex = "/"+ searchTerm +"/g";
	if( ignoreCase ) regex += "i";
	return this.replace(eval(regex), replaceWith);
};

function fix_ie6_first_last_child()
{
    $$("body *:first-child").each(function(el){
        el.addClass("first-child");
        });
    $$("body *:last-child").each(function(el){
        el.addClass("last-child");
        });
}
if(Browser.Engine.trident && Browser.Engine.version <= 6){ addOnLoad(fix_ie6_first_last_child); }

function get_a()
{
    $(document).getElements('a[rel^=popup]').each(function(a){
        add_popup(a);
    });
}
//addOnLoad(get_a);

/*
adapted from
moopop: unobtrusive javascript popups via late binding using mootools 1.11 
copyright (c) 2007 by gonchuki - http://blog.gonchuki.com
*/
function add_popup(el)
{
    el.addEvent('click', function(e){ new Event(e).stop(); popup(el); }.bind(this));
    var size = el.getAttribute('rel').match(/\[(\d+),\s*(\d+)\]/) || ['', this.width, this.height];
    if (size[1]) el.setAttribute('popupprops', 'width=' + size[1] + ', height=' + size[2] );
}
  
function popup(el)
{
    window.open(el.href, '', el.getAttribute('popupprops') || '');
}

function get_viewport_size()
{
    var viewportwidth;
    var viewportheight;
    if (typeof window.innerWidth != 'undefined')
    {
        viewportwidth = window.innerWidth;
        viewportheight = window.innerHeight;
    }
     
    // IE6 in standards compliant mode (i.e. with a valid doctype as the first line in the document)
    else if (typeof document.documentElement != 'undefined' && typeof document.documentElement.clientWidth != 'undefined' && document.documentElement.clientWidth != 0)
    {
        viewportwidth = document.documentElement.clientWidth;
        viewportheight = document.documentElement.clientHeight;
    }
    // older versions of IE
    else
    {
        viewportwidth = document.getElementsByTagName('body')[0].clientWidth;
        viewportheight = document.getElementsByTagName('body')[0].clientHeight;
    }
    
    return {height:viewportheight, width:viewportwidth};
}

function get_scroll_position()
{
    var scrOfX = 0, scrOfY = 0;
    if( typeof( window.pageYOffset ) == 'number' )
    {
        //Netscape compliant
        scrOfY = window.pageYOffset;
        scrOfX = window.pageXOffset;
    }
    else if( document.body && ( document.body.scrollLeft || document.body.scrollTop ) )
    {
        //DOM compliant
        scrOfY = document.body.scrollTop;
        scrOfX = document.body.scrollLeft;
    }
    else if( document.documentElement && ( document.documentElement.scrollLeft || document.documentElement.scrollTop ) )
    {
        //IE6 standards compliant mode
        scrOfY = document.documentElement.scrollTop;
        scrOfX = document.documentElement.scrollLeft;
    }
    return {x:scrOfX, y:scrOfY};
}

Window.getScroll = get_scroll_position;
Window.getViewport = get_viewport_size;


