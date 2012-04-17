/*public enum AddResponse
{
    None=0,
    Success=1,
    Bulk=2,
    Student=4,
    Failure=8
}*/

var CART_NONE       = 0;
var CART_SUCCESS    = 1;
var CART_BULK       = 2;
var CART_STUDENT    = 4;
var CART_FAILURE    = 8;

var requested_titleid;
var requested_sku;

function AddToCart(titleid, sku)
{
    var request = _cart_build_request(titleid, sku);
    var data = "title_id="+encodeURI(titleid)+"&sku="+encodeURI(sku);
    cart_display_message("Adding To Cart...");
    requested_titleid = titleid;
    requested_sku = sku;
    request.send(data);    
}

function _cart_build_request(titleid, sku)
{
    var r = new Request({
        "url": "addtocart.aspx", 
        "method": "post",
        "noCache": true,
        "onSuccess": _cart_request_success,
        "onFailure": _cart_request_failure
    });
    return r;
}

function _cart_request_success(responseText, responseXML)
{
    try
    {
        var response_code = parseInt(responseText);
        var is_success = (response_code & CART_SUCCESS) == CART_SUCCESS;
        var is_student = (response_code & CART_STUDENT) == CART_STUDENT;
        var is_bulk = (response_code & CART_BULK) == CART_BULK;
        
        if(is_success)
        {
            cart_display_message("Success...");
            if(is_student)
            {
                cart_display_popup("success", "Item successfully added to cart. This item, however, is subject to student pricing.");
            }
            else
            {
                cart_display_popup("success", "Item successfully added to cart");
            }
        }
        else if(!is_success && is_bulk)
        {
            cart_display_message("Failed (Bulk Item)...");
            cart_display_popup("bulk", "This item is a bulk item and cannot be added to your cart");
        }
        else if(!is_success && !is_bulk)
        {
            cart_display_message("Failed (Unknown)...");
            cart_display_popup("fail", "There was an unknown error adding this item to your cart");
        }
        
        setTimeout(function() { cart_hide_message(); }, 1000); 
    }
    catch(e)
    {}
}

function _cart_request_failure(xhr)
{
    cart_display_message("Error Adding Item...");
    setTimeout(function() { cart_hide_message(); }, 2000); 
}


/////////////////////////////////////////////////////////////////
function cart_display_message(message)
{
    var body = $(document.body);
    var msg = $("cart_msg_container");
    if(!$chk(msg))
    {
        msg = new Element("div", {"id":"cart_msg_container"});
        msg.setStyles({
            "position":"absolute",
            "top":0,
            "left":0, 
            "background-color":"#000000", 
            "color":"#FFFFFF"
            });
        body.grab(msg, 'top');
    }
    msg.fade('out');
    msg.set('html', message);
    setTimeout(function(){ msg.fade('in'); }, 500);
}

function cart_hide_message()
{
    var msg = $("cart_msg_container");
    if(!$chk(msg)) return;
    
    msg.fade('out');
}

/////////////////////////////////////////////////////

function cart_display_popup(popup_type, message)
{
    var body = $(document.body);
    var msg = $("cart_popup_container");
    
    
    if(!$chk(msg))
    {
        msg = new Element("div", {"id":"cart_popup_container"});
        msg.setStyles({"position":"absolute"});
        body.grab(msg, 'top');
    }
    msg.fade('hide');
    msg.empty();
    
    var msgQuoteTop = new Element("div", {"class":"quoteTop"});
    msg.grab(msgQuoteTop);
    
    var close_button = new Element("div", {"class":"popClose"});
    close_button.addEvent('click', cart_close_popup);
    msgQuoteTop.grab(close_button);
    
    var titleProduct = new Element ("div", {"class":"popTitle"});
    var ptitleProduct = new Element ("p");
    titleProduct.grab(ptitleProduct);
    ptitleProduct.set('html', "Product added to cart");
    msgQuoteTop.grab(titleProduct);
    
    var quoteBody = new Element("div", {"class":"quoteBody"});
    msg.grab(quoteBody);
    
    var contChangePass = new Element("div", {"class":"contChangePass"});
    quoteBody.grab(contChangePass);
    
    
    var h1contChangePass  = new Element ("h1");
    h1contChangePass.set('html', "The product you have selected has been added to your cart.");
    contChangePass.grab(h1contChangePass);
    
    var item = new Element("p");
    item.set('html', "Item #: " + requested_sku);
    contChangePass.grab(item);
    
    var copy = new Element("p");
    copy.set('html', "" + message);
    contChangePass.grab(copy);
    
   var pcontChangePass  = new Element ("p"); 
   pcontChangePass.set('html', "<br /><strong>Note</strong> - The product you have selected will require <a href=\"#\" class=\"linkInfo\">verification of student status</a> a before the order can be shipped.");
   contChangePass.grab(pcontChangePass);
   
    //var checkout = new Element("input",{"class":"btncheck", "type":"image", "src":"images/checkoutBtn2.png", "width":"129", "height":"32"});
    //contChangePass.grab(checkout);
    
   // var continue_shopping = new Element("input",{"class":"btnContinue", "type":"image", "src":"images/buttonContinueShopping2.jpg", "width":"129", "height":"25"});
    //contChangePass.grab(continue_shopping);
    
    //var divClear = new Element ("div");
    //divClear.set('html', {"class":"clear"});
    //contChangePass.grab(divClear);

    if(popup_type == 'success')
    {
        
        var checkout = new Element("input",{"class":"btncheck", "type":"image", "src":"images/checkoutBtn2.png", "width":"129", "height":"32"});
        contChangePass.grab(checkout);   
        
        var continue_shopping = new Element("input",{"class":"btnContinue", "type":"image", "src":"images/buttonContinueShopping2.jpg", "width":"129", "height":"25"});
        continue_shopping.addEvent('click', cart_close_popup);
        contChangePass.grab(continue_shopping);
    }
    
    if(popup_type == 'bulk')
    {
        var continue_shopping = new Element("input",{"class":"btnContinue", "type":"image", "src":"images/buttonContinueShopping2.jpg", "width":"129", "height":"25"});
        continue_shopping.addEvent('click', cart_close_popup);
        contChangePass.grab(continue_shopping);

        var quote = new Element("a", {"href":"#quote"});
        quote.set('html', "Get a Quote");
        quote.addEvent('click', cart_close_popup);
        contChangePass.grab(quote);
    }
    
    if(popup_type == 'fail')
    {
        var continue_shopping = new Element("input",{"class":"btnContinue", "type":"image", "src":"images/buttonContinueShopping2.jpg", "width":"129", "height":"25"});
        continue_shopping.addEvent('click', cart_close_popup);
        contChangePass.grab(continue_shopping); 
    }
    
    var divClear = new Element ("div");
    divClear.set('html', {"class":"clear"});
    contChangePass.grab(divClear);

    var quoteTButt = new Element("div", {"class":"quoteTButt"});
    msg.grab(quoteTButt);
    
       
    cart_popup_center(msg);
    
    setTimeout(function(){ msg.fade('in'); }, 1000);
}

/////////////////////////////////////////////////////////////////////////

function cart_popup_center(popup)
{
    var viewport = Window.getViewport();
    var scroll = Window.getScroll();
    
    var coords = popup.getCoordinates();
    
    popup.setStyle("top", viewport.height / 2 - coords.height / 2);
    popup.setStyle("left", viewport.width / 2 - coords.width / 2);
}

function cart_close_popup(evt)
{
    var msg = $("cart_popup_container");
    if(!$chk(msg)) return;
    
    if(evt.stop != undefined) evt.stop();
    
    msg.fade('out');
    setTimeout(function() { msg.empty(); }, 1000);
}



