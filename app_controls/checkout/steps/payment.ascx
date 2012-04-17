<%@ Control Language="C#" AutoEventWireup="true" CodeFile="payment.ascx.cs" Inherits="CheckoutStep_Payment" %>

<style type="text/css">
    .hidden { display:none; visibility:hidden; }
</style>

<script type="text/javascript">
    function toggle_cc()
    {
        CreditCardPanel.toggleClass('hidden');      
    }
    
    addOnLoad(toggle_cc);
    
    function hideBoxWhatisNew(){
	
		document.getElementById("boxWhatisThis").style.visibility = 'hidden';

	}
	function showCreditCard()
	{
	    CreditCardPanel.toggleClass('hidden');
        document.getElementById("cardDiv").style.visibility = 'visible';   
	}
    
    function NoOverload()
    {
        document.getElementById("pagenew").value = "1";
    }

    
</script>



<div class="boxItemForPurch">
	<div class="boxYouAre">
    	<div class="sideLeftBoxYou"></div>
        <div class="contQuestionYou">
        	<div class="questionBoxYou2"><p>PAY WITH?</p></div>
        </div>
        <div class="contControlBoxYou">
       	  <div class="controlBoxYou">
            <div class="controlYouAre"><asp:LinkButton ID="CreditCardButton" Text="<img src='images/bn_creditCard.jpg' alt='Credit Card' />" OnClientClick="showCreditCard(); return false" runat="server"></asp:LinkButton></div>           
            <div class="controlYouAre"><asp:LinkButton ID="PurchaseOrderButton" CausesValidation="false" Text="<img src='images/bn_purchaseOrder.jpg' alt='Purchase Order' />" OnClick="PurchaseOrderButton_Clicked" runat="server"></asp:LinkButton></div>           
          </div>
        </div>
        <div class="sideRightBoxYou"></div>
        <div class="clear"></div>
    </div>
</div>   

<div id="cardDiv" style="visibility:hidden">
<asp:Panel ID="CreditCardPanel" CssClass="container" runat="server">    
    <div class="dividerLineBox"></div>

    <asp:CustomValidator ID="CreditCardNumberRequiredServerValidator" ControlToValidate="CreditCardNumberTextbox" runat="server"><span class="error">Credit Card Required</span></asp:CustomValidator>
    <asp:CustomValidator ID="CreditCardNumberValidServerValidator" ControlToValidate="CreditCardNumberTextbox" runat="server"><span class="error">Invalid Credit Card Number</span></asp:CustomValidator>
    <asp:CustomValidator ID="CreditCardCVVRequiredServerValidator" ControlToValidate="CVVTextbox" runat="server"><span class="error">Credit Card CVV Required</span></asp:CustomValidator>
    <asp:CustomValidator ID="CreditCardExpirationValidServerValidator" runat="server"><span class="error">Credit Card Has Expired</span></asp:CustomValidator>
    
    <asp:RequiredFieldValidator ID="CreditCardTypeRequiredValidator" ControlToValidate="CreditCardTypeDropdown" Display="Dynamic" runat="server"><span class="error">Credit Card Type Required</span></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="CreditCardRequiredValidator" ControlToValidate="CreditCardNumberTextbox" Display="Dynamic" runat="server"><span class="error">Credit Card Number Required</span></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="CreditCardCVVRequiredValidator" ControlToValidate="CVVTextbox" Display="Dynamic" runat="server"><span class="error">Credit Card CVV Required</span></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="CreditCardExpMonthRequiredValidator" ControlToValidate="MonthDropdown" Display="Dynamic" runat="server"><span class="error">Expiration Month Required</span></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="CreditCardExpYearRequiredValidator" ControlToValidate="YearDropdown" Display="Dynamic" runat="server"><span class="error">Expiration Year Required</span></asp:RequiredFieldValidator>
    
    <div class="boxItemForPurch"> 
        <div class="boxItemMain2">                    
            <h1>PAY WITH CREDIT CARD</h1><br />
            <div class="boxPayCard1">
                <div class="cellPay">
                	<asp:Label ID="Label1" CssClass="cellPayLabel" AssociatedControlID="CreditCardTypeDropdown" Text="Credit Card Type" runat="server"></asp:Label>
                    <div style="margin:2px 0 0 0;">        
                        <asp:DropDownList ID="CreditCardTypeDropdown" runat="server">
                            <asp:ListItem Text="Visa" Value="VISA"></asp:ListItem>
                            <asp:ListItem Text="MasterCard" Value="MC"></asp:ListItem>
                            <asp:ListItem Text="American Express" Value="AMEX"></asp:ListItem>
                            <asp:ListItem Text="Discover" Value="DISC"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="cellPay">
                	<asp:Label ID="Label2" CssClass="cellPayLabel" AssociatedControlID="CreditCardNumberTextbox" Text="Card Number" runat="server"></asp:Label>
                    <div style="margin:2px 0 0 0;">     
                        <asp:TextBox ID="CreditCardNumberTextbox" runat="server"></asp:TextBox>
                    </div>
              </div>
                <div class="cellPay">
                	<asp:Label ID="Label3" CssClass="cellPayLabel" AssociatedControlID="CVVTextbox" runat="server">Security Code</asp:Label>
                    <div style="margin:2px 0 0 0;">
                        <asp:TextBox ID="CVVTextbox" MaxLength="4" Columns="4" runat="server"></asp:TextBox>
                        <a id="btnWhatisCode" href="#" onclick="popCodeSecurity(); return false">   What is This?</a>
                        
                           <script type="text/javascript">
  		    	                function popCodeSecurity(){
  		    	                var buttonCoords = $("btnWhatisCode").getCoordinates();
              		    	    
		                        $('boxWhatisThis').toggleClass('hidden');
            		            
		                        var addressCoords = $('boxWhatisThis').getCoordinates();
            		            
		                        $('boxWhatisThis').setStyles({
		                            'top':buttonCoords.bottom - 70,
		                            'left':buttonCoords.left - addressCoords.width + 560
		                        });
	                        }
			                    //window.addEvent('domready', function(){
				                //var box = new MultiBox('popCodeSecurity', {descClassName: 'multiBoxDesc', useOverlay: 'true'});
			                    //});
		                </script>
                        
                        
                    </div>
                </div>
                <div class="cellPay">
                	<asp:Label ID="Label4" CssClass="cellPayLabel" AssociatedControlID="MonthDropdown" Text="Expiration Date" runat="server"></asp:Label>
                    <div style="margin:2px 0 0 0;">        
                        <asp:DropDownList ID="MonthDropdown" runat="server"></asp:DropDownList>
                        <asp:DropDownList ID="YearDropdown" runat="server"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="clear"></div>
        </div>
    </div>

    <div class="dividerLineBox"></div>
    <div class="boxItemMain2">
        <div class="btnContinue"><asp:LinkButton ID="ContinueButton" Text="Continue to Order Summary" OnClientClick="NoOverload();" OnClick="CreditCardButton_Clicked" runat="server"><img src="<% = Global.globalSiteImagesPath %>/buttonContinue.jpg" alt="Continue" /></asp:LinkButton></div>
    </div>
    <input id="pagenew" value="25" name="pagenew" type="hidden" />
    
   <!-- -->
  
   
    <div id="boxWhatisThis" class="hidden" style="position:absolute">
                        <div class="roundcont">
                             <div class="tabLeft"><img src="<% = Global.globalSiteImagesPath %>/popNewAddreTab1.gif" width="23" height="20" /></div>
                          	 <div class="contWhatisThis">
                             <div class="roundtop"><img src="<% = Global.globalSiteImagesPath %>/popNewAddreCorner1.jpg" alt="" width="5" height="5" class="corner" /></div>
                             <div class="boxUserAddress1">
                                <div class="topWhatisThis"></div>
                                <div class="mainWhatisThis">
                                     <div class="contMainWhatisThis">
                                         <div>                                            	
                                         <p><span>What is This?</span><p>
                                         <div class="lineWhatisThis"></div>
                                         <p><strong>Credit Cart Security code for Visa, MasterCard, Discover and American Express</strong></p>
                                         </div>
                                         <div class="creditCart">
                                        <img src="<% = Global.globalSiteImagesPath %>/card1.jpg" width="120" height="76">
                                        <p>The card validation number is printed on your Visa, MasterCasd & Discover cards isignature area of the back of the 							  	card. (This is the last 3 digits located AFTER the credit card number in the signature area of the card).</p> 
                                        <div style="clear:both"></div>
                                     </div>  
                                         <div class="creditCart">
                                        <img src="<% = Global.globalSiteImagesPath %>/card2.jpg" width="120" height="76">
                                        <p>The card validation number is printed on your Visa, MasterCasd & Discover cards isignature area of the back of the 	 		   						card. (This is the last 3 digits located AFTER the credit card number in the signature area of the card).</p> 
                                    <div style="clear:both"></div>
                                    </div>                         
                                         <div style="clear:both"></div>
                                    </div>      
                                </div>
                                <div class="bottonWhatisThis"></div>
                             </div>
                             <div id="btnWhatIsClose" onclick="hideBoxWhatisNew()"><p>Close</p></div>
                             <div class="roundbottom"><img src="<% = Global.globalSiteImagesPath %>/popNewAddreCorner3.jpg" alt="" width="5" height="5" class="corner" />                            </div>
                           </div>
                         </div>
                    </div>
    
</asp:Panel>


</div>

<%if (error == true) { Response.Write("<script type=\"text/javascript\">document.getElementById('cardDiv').style.visibility = 'visible';</script>"); } %>      