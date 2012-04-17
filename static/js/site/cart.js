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
var requested_wishlistId;
var requested_productName;

function AddToCart(titleid, sku, productName, wishlistId)
{
    var request = _cart_build_request(titleid, sku, wishlistId);
    var data = "title_id="+encodeURI(titleid)+"&sku="+encodeURI(sku)+"&w="+encodeURI(wishlistId);
    cart_display_message("Adding To Cart...");
    requested_titleid = titleid;
    requested_sku = sku;
    requested_wishlistId = wishlistId;
    requested_productName = productName
    request.send(data);    
}
function SaveSelectionInfo(scartid,ssku) {

    var cartid = scartid;
    var state = document.getElementById('States').value;
    var x = document.getElementById('States').selectedIndex;
    var statedes = document.getElementById('States').options[x].text;

    var grade = document.getElementById('GRADE').value;
    var w = document.getElementById('GRADE').selectedIndex;
    var gradedes = document.getElementById('GRADE').options[w].text;    
    
    var sku = ssku;
    var subject_all = document.getElementById('SUBJECT_ALL').checked;
    var subject_mth = document.getElementById('SUBJECT_MTH').checked;
    var subject_sci = document.getElementById('SUBJECT_SCI').checked;
    var subject_soc = document.getElementById('SUBJECT_SOC').checked;
    var subject_des = '';
    if (subject_all) { subject_des = 'All Subjects'; }
    if (subject_mth) { subject_des = 'Math'; }
    if (subject_sci) { subject_des = 'Science'; }
    if (subject_soc) { subject_des = 'Social Studies'; }

    var spanish = document.getElementById('ELL_EDITION').checked;
    var ell = 'NO';
    if (spanish) { ell = 'YES'; }

    var whiteboard = document.getElementById('WHITEBRD-BRND').value;
    var m = document.getElementById('WHITEBRD-BRND').selectedIndex;
    var whiteboarddes = document.getElementById('WHITEBRD-BRND').options[m].text;
    var repsystem = document.getElementById('LRS-BRND').value;
    var n = document.getElementById('LRS-BRND').selectedIndex;
    var repsystemdes = document.getElementById('LRS-BRND').options[n].text;
    if (repsystemdes.indexOf("Select") != -1) {
    	repsystemdes = "None";
    }
    if (whiteboarddes.indexOf("Select") != -1) {
    	whiteboarddes = "None";
    }

    var desdata = 'State:' + statedes + ',Grade:' + gradedes + ',Subject:' + subject_des + ',Ell:' + ell + ',Whiteboard:' + whiteboarddes + ',Learner Response System:' + repsystemdes;

    var data = "cid=" + encodeURI(cartid) + "&sta=" + encodeURI(state) + "&sku=" + encodeURI(sku) + "&spa=" + encodeURI(ell) + "&whb=" + encodeURI(whiteboard) + "&res=" + encodeURI(repsystem) + "&des=" + encodeURI(desdata);
    var xmlhttp;
    if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
        xmlhttp = new XMLHttpRequest();
    }
    else {// code for IE6, IE5
        xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
    }
    xmlhttp.onreadystatechange = function() {
        if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
            document.getElementById("MyResponse").innerHTML = xmlhttp.responseText;
        }
    }
    xmlhttp.open("GET", "saveAdditionalInfo.aspx?" + data, true);
    xmlhttp.send();


}
function AddToCartProduct(titleid, sku, productName, wishlistId, quantity) {
	if (titleid == '91663') {
		var price = $('conf_price').get('html');
		if (price.indexOf("Setup") == -1) {
			//sku = $('newSku').get('value');
			var request = _cart_build_request(titleid, sku, wishlistId);
			var data = "title_id=" + encodeURI(titleid) + "&sku=" + encodeURI(sku) + "&w=" + encodeURI(wishlistId) + "&q=" + encodeURI(quantity);
			cart_display_message("Adding To Cart...");
			requested_titleid = titleid;
			requested_sku = sku;
			requested_wishlistId = wishlistId;
			requested_productName = productName
			request.send(data);
			SaveSelectionInfo(titleid, sku);
		} else { 
		    alert('Product requires configuration before adding it to the cart.')
		}
	} else {
	var request = _cart_build_request(titleid, sku, wishlistId);
	var data = "title_id=" + encodeURI(titleid) + "&sku=" + encodeURI(sku) + "&w=" + encodeURI(wishlistId) + "&q=" + encodeURI(quantity);
	cart_display_message("Adding To Cart...");
	requested_titleid = titleid;
	requested_sku = sku;
	requested_wishlistId = wishlistId;
	requested_productName = productName
	request.send(data);	    
	}

  
}

function _cart_build_request(titleid, sku, wishlistId)
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

        if(is_success || is_student)
        {
            cart_display_message("Success...");
            if(is_student)
            {
                cart_display_popup("success", "Item successfully added to cart. <br><br><b style=\"color:RED\"> Note: Academic Proof is Required</b>");
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
    if(!$chk(msg)){
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
        msg.setStyles({"position":"fixed", "z-index":"5"});
        body.grab(msg, 'top');
    }
    msg.fade('hide');
    msg.empty();
    
    var msgQuoteTop = new Element("div", {"class":"quoteTop"});
    msg.grab(msgQuoteTop);
    
    var close_button = new Element("div", {"class":"popClose"});
    close_button.addEvent('click', cart_close_popup);
    msgQuoteTop.grab(close_button);
    

    if(popup_type == 'success')
    {
    
            var titleProduct = new Element ("div", {"class":"popTitle"});
            var ptitleProduct = new Element ("p");
            titleProduct.grab(ptitleProduct);
            ptitleProduct.set('html', "Product Selected");
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
            
           var pcontChangePass  = new Element ("p", {"class":"clear:both;"}); 
           //pcontChangePass.set('html', "<br /><strong>Note</strong> - The product you have selected will require <a href=\"#\" class=\"linkInfo\">verification of student status</a> a before the order can be shipped.");
           contChangePass.grab(pcontChangePass);
           
           var checkout = new Element("div",{"class":"btncheck2"});
           contChangePass.grab(checkout);
           
           var linkCart = new Element("a",{"href":"cart.aspx"});
           checkout.grab(linkCart);  
           
           var btnCheckout = new Element ("img",{"src":"images/checkoutBtn2.png", "width":"129", "height":"32"});
           linkCart.grab(btnCheckout);
        
 
            
            
           var continue_shopping = new Element("div",{"class":"btnContinue2"});
//           continue_shopping.set('html', "<img src=\"images/buttonContinueShopping2.jpg\" width=\"129\" height=\"25\" style=\"float:right\"/>");
continue_shopping.set('html', "<img src=\"images/buttonContinueShopping2.jpg\" width=\"129\" height=\"25\" onclick=\"window.location.reload()\" style=\"float:right\"/>");
           continue_shopping.addEvent('click', cart_close_popup);
           contChangePass.grab(continue_shopping);
    }
    
    if(popup_type == 'bulk')
    {

		
			var titleProduct = new Element ("div", {"class":"popTitle"});
            var ptitleProduct = new Element ("p");
            titleProduct.grab(ptitleProduct);
            ptitleProduct.set('html', "");
            msgQuoteTop.grab(titleProduct);
            
            var quoteBody = new Element("div", {"class":"quoteBody"});
            msg.grab(quoteBody);
            
            var contChangePass = new Element("div", {"class":"contChangePass"});
            quoteBody.grab(contChangePass);
            
            
            var h1contChangePass  = new Element ("h1");
            h1contChangePass.set('html', "You have selected a product that is bulk shipped");
            contChangePass.grab(h1contChangePass);
            
            var item = new Element("p");
            item.set('html', "Item #: " + requested_sku);
            contChangePass.grab(item);
            
            var copy = new Element("p");
            copy.set('html', "" + message);
            contChangePass.grab(copy);
            
           var pcontChangePass  = new Element ("p", {"class":"clear:both;"}); 
           pcontChangePass.set('html', "<br />This item will be added to your cart, it will be sent directly yo ER for pricing and an account rep will get back to you with the shipping costs.");
           contChangePass.grab(pcontChangePass);


	   var checkout = new Element("div",{"class":"btnBulkRequest"});
           contChangePass.grab(checkout);

	
           var linkCart = new Element("a",{"href":"requestaquote.aspx?title=" + requested_productName}); // Link to request a quote
           checkout.grab(linkCart); 	
			
           var btnRequest = new Element("img",{"src":"images/btnRequestQuoteCartbulK.png"});
           linkCart.grab(btnRequest);

            
           var continue_shopping = new Element("div",{"class":"btnBulkContinue"});
           continue_shopping.set('html', "<img src=\"images/buttonContinueShopping2.jpg\"/>");
           continue_shopping.addEvent('click', cart_close_popup);
           contChangePass.grab(continue_shopping);
    }
    
    if(popup_type == 'fail')
    {

		
		var titleProduct = new Element ("div", {"class":"popTitle"});
            var ptitleProduct = new Element ("p");
            titleProduct.grab(ptitleProduct);
            ptitleProduct.set('html', "Fail");
            msgQuoteTop.grab(titleProduct);
            
            var quoteBody = new Element("div", {"class":"quoteBody"});
            msg.grab(quoteBody);
            
            var contChangePass = new Element("div", {"class":"contChangePass"});
            quoteBody.grab(contChangePass);
            
            
            var h1contChangePass  = new Element ("h1");
            h1contChangePass.set('html', "Fail");
            contChangePass.grab(h1contChangePass);
            
            var item = new Element("p");
            item.set('html', "Item #: " + requested_sku);
            contChangePass.grab(item);
            
            var copy = new Element("p");
            copy.set('html', "" + message);
            contChangePass.grab(copy);
            
           var pcontChangePass  = new Element ("p", {"class":"clear:both;"}); 
           pcontChangePass.set('html', "<br />Fail");
           contChangePass.grab(pcontChangePass);
        
           var linkCart = new Element("a",{"href":"cart.aspx"});
           contChangePass.grab(linkCart);   
            
           var checkout = new Element("div",{"class":"btncheck2"});
           checkout.set('html', "<input type=\"image\" src=\"images/checkoutBtn2.png\" width=\"129\" height=\"32\"/>");
           linkCart.grab(checkout);
            
           var continue_shopping = new Element("div",{"class":"btnContinue2"});
           continue_shopping.set('html', "<img src=\"images/buttonContinueShopping2.jpg\"/>");
           continue_shopping.addEvent('click', cart_close_popup);
           contChangePass.grab(continue_shopping);
    }
    
    var divClear = new Element ("div", {"class":"clear"});
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



