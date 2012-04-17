<%@ Page Language="C#" MasterPageFile="~/home.master" AutoEventWireup="true" CodeFile="requestAQuote.aspx.cs" Inherits="requestAQuote" Title="Request A Quote" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HomeContent_Master" Runat="Server">

    <script language="javascript"  type="text/javascript">
         arrayTitles = new Array();
         arrayQuantity = new Array();
         arrayPlatform = new Array();
         var fieldCont = 0;
        
        function getNewField(newOne,qtyToField){
            qtyToField = qtyToField || 1;
            printNewFields(newOne,qtyToField);
        }
        function AddNewField(){
            
        }
        function hideModal()
        {
             document.getElementById('mbThanksReqQuote').style.visibility = 'hidden';
        }
        
        function printNewFields(newOne,qtyToField){
            
            //alert(qtyToField);
            fieldCont ++;
            saveInformation();
            document.getElementById('fields').innerHTML += '<div id="singleSpaceNumber'+fieldCont+'"><div class="cellRegProdInfo"><span class="cellList" id="fieldNumber'+fieldCont+'"><p>'+fieldCont+'.</p></span><div class="cellTitle"><input type="text" size="38" name="Titles" id="Title_'+fieldCont+'" value="'+newOne+'" onBlur="cleanTitles(\'Title_'+fieldCont+'\')"/></div><div class="cellPlataform">'+getPlataform()+'</div><div class="cellQty"><input type="text" size="2" maxlength="3" value="'+qtyToField+'" name="Qty" id="Qty_'+fieldCont.toString()+'" onBlur="validQuantity(\'Qty_'+fieldCont.toString()+'\')"/></div><div class="cellButton"> <a href="#" onClick="deleteQuote('+fieldCont+'); return false;">Delete</a> </div></div></div>';
            inputInformation();
            fillNumbers();
        }
        function fillNumbers(){
            internalCont=1;
            var SpanList=document.getElementsByTagName('span');
            for(var i=0;i<SpanList.length;i++){
               if(SpanList[i].id.indexOf('fieldNumber',0)!=-1) SpanList[i].innerHTML=i+1;
            }    
        }
        function saveInformation(){
        arrayTitles = new Array();
        arrayQuantity = new Array();
        arrayPlatform = new Array();
            internalCont=1;
            for(i = 1; i<fieldCont; i++){
                if(document.getElementById('Title_'+i) != null){
                    arrayTitles[internalCont] = document.getElementById('Title_'+i).value;
                    arrayQuantity[internalCont] = document.getElementById('Qty_'+i).value;
                    arrayPlatform[internalCont] = document.getElementById('Platform_'+i).selectedIndex;
                    internalCont++;
                }
            }
        }
        function inputInformation(){
            internalCont=1;
            for(i = 1; i<fieldCont; i++){
                if(document.getElementById('Title_'+i) != null){
                    document.getElementById('Title_'+i).value = arrayTitles[internalCont];
                    document.getElementById('Qty_'+i).value = arrayQuantity[internalCont];
                    document.getElementById('Platform_'+i).selectedIndex = arrayPlatform[internalCont];
                    internalCont++;
                }
            }
        }
        function getPlataform(){
            plataformString = '';
            plataformString +='<select id="Platform_'+fieldCont.toString()+'" name="Platforms" tabindex="-1">'
            plataformString +='<option>I Don\'t Know</optino>';
            plataformString +='<option>Windows 98</option>';
            plataformString +='<option>Windows 2000</option>';
            plataformString +='<option>Windows XP</option>';
            plataformString +='<option>Windows Vista</option>';
            plataformString +='<option>Mac Classic</option>';
            plataformString +='<option>Mac OSX</option>';
            plataformString +='<option>Mac / Intel</option>';
            plataformString +='<option>Other OS</option>';
            plataformString +='<option>Web Based</option>';
            plataformString +='</select>'
            return plataformString;
        }
        function getPlataformOld(i){
            plataformString = '';
            plataformString +='<select id="Platform_'+i+'" name="Platform_'+i+'" tabindex="-1">'
            plataformString +='<option value="1">I Don\'t Know</optino>';
            plataformString +='<option value="2">Windows 98</option>';
            plataformString +='<option value="3">Windows 2000</option>';
            plataformString +='<option value="4">Windows XP</option>';
            plataformString +='<option value="5">Windows Vista</option>';
            plataformString +='<option value="6">Mac Classic</option>';
            plataformString +='<option value="7">Mac OSX</option>';
            plataformString +='<option value="8">Mac / Intel</option>';
            plataformString +='<option value="9">Other OS</option>';
            plataformString +='<option value="10">Web Based</option>';
            plataformString +='</select>'
            return plataformString;
        }
        function deleteQuote(number){
                var DivWithInfo=document.getElementById('singleSpaceNumber'+number);
                DivWithInfo.parentNode.removeChild(DivWithInfo);
                fillNumbers();                  
        }
        
        function validQuantity(objectId){
            var box = document.getElementById(objectId);
            if(isNaN(box.value)){
                box.value = "1";
            }
        }
        function cleanTitles(id){
            document.getElementById(id).value = stripCommas(document.getElementById(id).value);
        }
        
        function stripCommas(numString) {
            var re = /,/g;
             return numString.replace(re," ");
        }
      
    </script> 
    <script language="javascript" type="text/javascript" src="js/request.js"></script>

<div id="print">    
<div id="cont">	
<form id="Form1" action="#" method="post" name="Form1" onsubmit="return validForm();" runat="server">
<asp:Literal runat="server" ID="userGUIDTextBox"></asp:Literal>
		   	<div class="mainAccount">
			 <h1>Request a Quote</h1><br />
		  	<div id="boxReqQuote">
			<div>	
					<p>Tired of price comparisons on the web? Let us do the work for you! We will go out of our way to give you the best price
with the service to back it up! Call us at (800) 624-2926 or fill out the form below and our educational specialists will 
give you a quote for the best price based on the quantities you require.</p>
					<h6>* Required Information</h6>
			</div>
		</div> <!-- End boxReqQuote -->
		<div class="boxReqProdInfo">
			<div class="titleReqProdInfo"><p><strong>Product information</strong></p></div>
			<hr />
			
			<div class="contentFormRegPro">
			<div class="formRegProdInfo">
				<div class="cellRegProdInfo">
					<div class="cellList"><p></p></div>
					<div class="cellTitle"><p>Title / Description:</p></div>
  					<div class="cellPlataform"><p>Platform</p></div>
					<div class="cellQty"><p>Qty</p></div>
					<div class="cellButton"> <a href="#"></a></div>
				</div>
				<div id="fields">
				</div>
				<div id="errorQTy" style="color:Red; visibility:hidden;">* At least 1 element has to be added to process your request</div>
			</div>
			<!-- <div class="btnAddMore"><a href="" onclick="getNewField(''); return false;"><img src="<% = Global.globalSiteImagesPath %>/buttonAddMoreOpt.jpg"/></a></div>	-->
			<div class="btnAddMore"><input type="image" src="<% = Global.globalSiteImagesPath %>/buttonAddMoreOpt.jpg" name="AddProduct" onclick="getNewField('')"></div>
			</div>
		
		
		</div> <!-- End boxProductInfo -->
		
		<div style="clear: both;"></div>
		
		<div class="boxContInfo">
			<div class="titleReqProdInfo"><p><strong>contact information</strong></p></div>
			<hr />
			<div class="errorRequir alert" id="errorGlobal" name="errorGlobal" style="display: none;">
                            <p>
                                <strong>ERROR:</strong> CORRECT ITEMS IN RED</p>
            </div>
            <% 
                Response.Write(completePost); 
            %>

			<div class="contentFormRegPro">
					<div class="formCell">
						<div id="ins1" class="formLabel"><p>*Full Name:</p></div>
					    <div class="formInput">
					        <% Response.Write("<input type=\"text\" size=\"35\" name=\"FullName\" id=\"FullName\" onblur=\"eraseSpaces('FullName');\" value=\"" + _user_fullname + "\"/>");%>
					    </div>
					    <div id="error1" class="formAlert alert" style="display:none;"><p><-Required</p></div>
					</div>
					<div class="formCell">
						<div id="ins2" class="formLabel"><p>*Job Title:</p></div>
					    <div class="formInput">
					        <select name="JobTitle" id="JobTitle">
					        <asp:PlaceHolder ID="Options" runat="server"></asp:PlaceHolder>
                            </select>
					    </div>
					    <div class="formAlert alert" id="error2" style="display:none;"><p><-Required</p></div>
					</div>
					<div class="formCell">
						<div id="ins3" class="formLabel"><p>*School / District:</p></div>
					    <% Response.Write("<div class=\"formInput\"><input type=\"text\" size=\"35\" value=\""+_user_schoolname+"\"name=\"BldgName\" id=\"BldgName\" onblur=\"eraseSpaces('BldgName');\"/></div>"); %>
					    
					    <div class="formAlert alert" id="error3" style="display:none;"><p><-Required</p></div>
					</div>
					<div class="formCell">
						<div id="ins4" class="formLabel"><p>*Considering Purchase For:</p></div>
					    <div class="formInput">
					    <select id="ConPurchFor" name="ConPurchFor">
                                <option value=""/>
                                <option value="District" >District</option>
                                <option value="Classroom">Classroom</option>
                                <option value="Home">Home</option>
                                <option value="Lab">Lab</option>
                                <option value="School">School</option>
                                <option value="Other">Other</option>
                        </select>
					    </div>
					    <div class="formAlert alert" id="error4" style="display:none;"><p><-Required</p></div>
					</div>
					<div class="formCell">
						<div id="ins5" class="formLabel"><p>*Address Line 1:</p></div>
					    <div class="formInput">
					            <% Response.Write("<input type=\"text\" size=\"35\" name=\"Address1\" id=\"Address1\" onblur=\"eraseSpaces('Address1');\" value=\""+_user_add1+"\"/>"); %>
					    </div>
					    <div class="formAlert alert" id="error5" style="display:none;"><p><-Required</p></div>
					</div>
					<div class="formCell">
						<div class="formLabel"><p>Address Line 2:</p></div>
					    <div class="formInput">
					            <% Response.Write("<input type=\"text\" size=\"35\" name=\"Address1\" id=\"Address2\" onblur=\"eraseSpaces('Address2');\" value=\""+_user_add2+"\"/>"); %>
					    </div>
					    <div class="formAlert"></div>
					</div>
					<div class="formCell">
						<div class="formLabel"><p>City:</p></div>
					    <div class="formInput">
					    <% Response.Write("<input type=\"text\" size=\"35\" name=\"City\" id=\"City\" value=\""+_user_city+"\" onblur=\"eraseSpaces('City');\"/>"); %>
					    </div>
					    <div class="formAlert"></div>
					</div>
					<div class="formCell">
						<div class="formLabel"><p>State / Province / Region:</p></div>
					    <div class="formInput">
					            <% Response.Write("<input type=\"text\" size=\"35\" name=\"State\" id=\"State\" value=\""+_user_state+"\"onblur=\"eraseSpaces('State');\"/>"); %>
					    </div>
					    <div class="formAlert"></div>
					</div>
					<div class="formCell">
						<div id="ins6" class="formLabel"><p>*ZIP / Postal Code:</p></div>
					    <div class="formInput">
					        <% Response.Write("<input type=\"text\" size=\"35\" name=\"Zip\" id=\"Zip\" onblur=\"eraseASpaces('Zip');\" value=\"" + _user_zip + "\"/>"); %>
					    </div>
					    <div class="formAlert alert" id="error6" style="display:none;"><p><-Required</p></div>
					</div>
					<div class="formCell">
						<div class="formLabel"><p>Country:</p></div>
					    <div class="formInput">
					    <select id="Country" name="Country">
                            <option value="US">United States</option>
                            <option value="CA">Canada</option>
                        </select>
					    </div>
					    <div class="formAlert"></div>
					</div>
					<div class="formCell">
						<div id="ins7" class="formLabel"><p>*Phone Number:</p></div>
					    <div class="formInput"><div class="formInput">
					            <% Response.Write("<input type=\"text\" size=\"35\" name=\"Phone\" id=\"Phone\" value=\""+_user_phone+"\"/>"); %>
					    </div></div>
					    <div class="formAlert alert" id="error7" style="display:none;"><p><-Required</p></div>
					</div>
					<div class="formCell">
						<div id="ins8" class="formLabel"><p>*Email Address:</p></div>
					    <div class="formInput">
					            <% Response.Write("<input type=\"text\" size=\"35\" name=\"Email\" id=\"Email\" onblur=\"eraseASpaces('Email');\" value=\""+_user_email+"\"/>"); %>
					    </div>
					    <div class="formAlert alert" id="error8" style="display:none;"><p><-Email Invalid</p></div>
					</div>
					<div class="formCell">
						<div class="formLabel"><p>Notes / Comments:</p></div>
					    <div class="formInput"> <textarea rows="6" cols="26" name="Notes" id="Notes" wrap="virtual" onblur="eraseSpaces('Notes');"></textarea></div>
					    
					    <div class="formAlert"></div>
					</div>
			</div>
			
			
			<!-- <a href="#mbThanksReqQuote" rel="type:element" id="mb11" class="mb">HTML</a> -->
			
  			<div class="btnReaqQuo">
                  <asp:ImageButton ID="btnImageQuote" src="images/btnReaAQuo.jpg" runat="server" onclick="btnImageQuote_Click" OnClientClick="return validForm();"/>
  			</div>
		
		</div>
		
		   <div id="mbThanksReqQuote" style="visibility:hidden; position:absolute; top:40%; left:35%"> <!-- class="mbHidden" -->
                <div class="quote">
                    <div class="quoteTop">
                        <div class="popTitle">Thank You!</div>
                        <div class="popClose" onclick="hideModal();" ></div>
                    </div>
                    <div class="quoteBody">
                        <div><h1>Request a Quote</h1><br /><p>Your request has been submitted</p></div>
                        <div style="float:right; padding-top:50px;"><a href="#"><img src="<% = Global.globalSiteImagesPath %>/buttonContinueShopping.jpg" onclick="hideModal()"></a></div>
                        <div class="clear"></div>
                    </div>
                    <div class="quoteTButt">
                    </div>
                </div>
            </div>
		
			
</div> <!-- End div mainAccount -->
</form>
</div>
</div>
<% 
    /*if (_quote_session != null)
    {
        foreach (string quoteName in _quote_session)
        {
            Response.Write("<script language=\"javascript\" type=\"text/javascript\">getNewField('" + quoteName + "');</script>");
        }
    }
    else {
            Response.Write("<script language=\"javascript\" type=\"text/javascript\">getNewField('',1);</script>");
    }*/
    if (_quoteThis != null)
    {
        for (int i = 0; i < _quoteThis.GetLength(1); i++)
        {
            Response.Write("<script language=\"javascript\" type=\"text/javascript\">getNewField('"+_quoteThis[0,i]+"',"+_quoteThis[1,i]+");</script>");
        }
    }
    else 
    {
        Response.Write("<script language=\"javascript\" type=\"text/javascript\">getNewField('',1);</script>");
    }
%>

<%if (FineREQ == true) { Response.Write("<script type=\"text/javascript\">document.getElementById('mbThanksReqQuote').style.visibility = 'visible';</script>"); } %>      
<%if (error == true) { Response.Write("<script type=\"text/javascript\">document.getElementById('errorQTy').style.visibility = 'visible';</script>"); } %>      

</asp:Content>

