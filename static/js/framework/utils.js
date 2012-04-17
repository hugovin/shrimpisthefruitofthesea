// Miscelaneous helper functions
// (c) Jordan Sherer 2008

// Useful function for hiding a specific class, called "hide_if_no_js"
// Combine this with a style in the css .hide_if_no_js { display:none; visibility:hidden; }
function show_hidden_javascript()
{
    $$('.hide_if_no_js').each(function(el){ el.removeClass('hide_if_no_js'); });
}

function focusText(sender, defaultText)
{
    /*
    This function allows you to pass an input field (i.e., type=[text] or textarea) with default text.
    The default text will be populated in the value and title attributes. 
    When the field is focused, if the text hasn't changed, this function will erase the default text
    */
    var el = $(sender);
    if(defaultText == undefined){ defaultText = ""; }
    if(el.value != undefined)
    {
        defaultText = el.value;
        el.value = "";
        if(el.title == "")
        {
            el.title = defaultText;
        }
        el.removeClass('focus_text');
        el.onblur = function()
                    {
                        if(el.value == "")
                        {
                            el.addClass('focus_text');
                            el.value = defaultText;
                        }
                        else
                        {
                            el.onfocus = function(){};
                            el.onblur = function(){};
                        }
                    };
    }
    else if(el.innerText != undefined)
    {
        defaultText = el.innerText;
        el.innerText = "";
        if(el.title == "")
        {
            el.title = defaultText;
        }
        el.removeClass('focus_text');
        el.onblur = function()
                    {
                        if(el.innerText == "")
                        {
                            el.addClass('focus_text');
                            el.innerText = defaultText;
                        }
                        else
                        {
                            el.onfocus = function(){};
                            el.onblur = function(){};
                        }
                    };
    }
}

function addFocusText(){
    $$(".focus_text").each(function(el){
        if(el.value != undefined)
        {
            defaultText = el.value;
        }
        else if(el.innerText != undefined)
        {
            defaultText = el.innerText;
        }
        else
        {
            defaultText = "";
        }
        
        if(el.title != defaultText)
        {
            el.title = defaultText;
        }
        
        el.onfocus = function() { focusText(el, defaultText); };
    });
}//addOnLoad(addFocusText);

// A function used to count the characters in a textbox, and then display the amount of characters left in the limit
function countChars(sender, limit)
{
    try
    {
        var value = sender.value;
        var sid = sender.id;
        
        if(value.length > limit){ sender.value = value.substr(0,limit); }
        
        var charsleft = limit - value.length;
        charsleft = (charsleft >= 0) ? charsleft : 0;

        text = charsleft + " characters left.";
        var countsid = sid + '_count';

        if($(countsid) == undefined)
        {
            var newEl = new Element('p', {id:countsid, className:"sub"});
            newEl.set("text", text);
            newEl.injectAfter($(sender));
        }
        else
        {
            $(countsid).innerHTML = text;
        }
    }catch(ex){}
}

// A function used to add the countChars function to the keyup event of a text entry
function addCountChars()
{
    $$(".count").each(function(el){
        maxlen = el.getProperty('maxlength');
        if(maxlen > 0)
        {
            el.onkeyup = function(){ countChars(el, maxlen); };
        }
    });
}//addOnLoad(addCountChars);


function Mod10(ccNumb) {  // v2.0
    var valid = "0123456789"  // Valid digits in a credit card number
    var len = ccNumb.length;  // The length of the submitted cc number
    var iCCN = parseInt(ccNumb);  // integer of ccNumb
    var sCCN = ccNumb.toString();  // string of ccNumb
    sCCN = sCCN.replace (/^\s+|\s+$/g,'');  // strip spaces
    var iTotal = 0;  // integer total set at zero
    var bNum = true;  // by default assume it is a number
    var bResult = false;  // by default assume it is NOT a valid cc
    var temp;  // temp variable for parsing string
    var calc;  // used for calculation of each digit

    // Determine if the ccNumb is in fact all numbers
    for (var j=0; j<len; j++)
    {
      temp = "" + sCCN.substring(j, j+1);
      if (valid.indexOf(temp) == "-1"){bNum = false;}
    }

    // if it is NOT a number, you can either alert to the fact, or just pass a failure
    if(!bNum)
    {
      /*alert("Not a Number");*/
      bResult = false;
    }

    // Determine if it is the proper length 
    if((len == 0)&&(bResult))
    {  // nothing, field is blank AND passed above # check
      bResult = false;
    }
    else
    {  // ccNumb is a number and the proper length - let's see if it is a valid card number
        if(len >= 15)
        {  // 15 or 16 for Amex or V/MC
            for(var i=len;i>0;i--)
            {  // LOOP throught the digits of the card
                calc = parseInt(iCCN) % 10;  // right most digit
                calc = parseInt(calc);  // assure it is an integer
                iTotal += calc;  // running total of the card number as we loop - Do Nothing to first digit
                i--;  // decrement the count - move to the next digit in the card
                iCCN = iCCN / 10;                               // subtracts right most digit from ccNumb
                calc = parseInt(iCCN) % 10 ;    // NEXT right most digit
                calc = calc *2;                                 // multiply the digit by two
                // Instead of some screwy method of converting 16 to a string and then parsing 1 and 6 and then adding them to make 7,
                // I use a simple switch statement to change the value of calc2 to 7 if 16 is the multiple.
                switch(calc)
                {
                    case 10: calc = 1; break;       //5*2=10 & 1+0 = 1
                    case 12: calc = 3; break;       //6*2=12 & 1+2 = 3
                    case 14: calc = 5; break;       //7*2=14 & 1+4 = 5
                    case 16: calc = 7; break;       //8*2=16 & 1+6 = 7
                    case 18: calc = 9; break;       //9*2=18 & 1+8 = 9
                    default: calc = calc;           //4*2= 8 &   8 = 8  -same for all lower numbers
                }                                               
                
                iCCN = iCCN / 10;  // subtracts right most digit from ccNum
                iTotal += calc;  // running total of the card number as we loop
            }  // END OF LOOP
            
            if ((iTotal%10)==0)
            {  // check to see if the sum Mod 10 is zero
                bResult = true;  // This IS (or could be) a valid credit card number.
            }
            else
            {
                bResult = false;  // This could NOT be a valid credit card number
            }
        }
    }
}




function openPopup(url,w,h){
	var winl = (screen.width - w) / 2;
	var wint = (screen.height - h) / 2;
	popupWin=window.open(url,'popup','status=no,menubar=no,width='+w+',height='+h+',top='+wint+',left='+winl)
}

function popup(mylink, windowname){
	if (! window.focus) 
		return true;
		var href;
	if (typeof(mylink) == 'string')
		href=mylink;
	else   href=mylink.href;window.open(href, windowname, '');
		return false;
}
