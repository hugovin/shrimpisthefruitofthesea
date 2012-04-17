<%@ Control Language="C#" AutoEventWireup="true" CodeFile="receiptcolumn.ascx.cs" Inherits="CheckoutControl_ReceiptColumn" %>

<div id="boxOrderSummary">
    <div class="boxTitleProducts">Order Summary</div>
    <div class="boxMainProducts">

        <div class="orderSumm">
            <div class="cellOrderSumm">
                <div class="orderSummItem"><p><span>Item Total:</span></p></div>
                <div class="orderSummPrice"><p><span><asp:Literal ID="ItemTotalLiteral" runat="server"></asp:Literal></span></p></div>
            </div> 
            <div class="cellOrderSumm">
                <div class="orderSummItem"><p>Estimated Tax:</p></div>
                <div class="orderSummPrice"><p><asp:Literal ID="TaxLiteral" runat="server"></asp:Literal></p></div>
            </div> 
            
            <div style="clear:both"></div> 
            <div class="lineDottedOrderSumm"></div>
            
        	<div class="cellOrderSummTax">
                <div class="orderSummItem"><p>SubTotal:</p></div>
                <div class="orderSummPrice"><p><asp:Literal ID="SubTotalLiteral" runat="server"></asp:Literal></p></div>
            </div>     
                       
            <div class="cellOrderSumm">
                <div class="orderSummItem"><p>Shipping:</p></div>
                <div class="orderSummPrice"><p><asp:Literal ID="ShippingLiteral" runat="server"></asp:Literal></p></div>
            </div>

            <div style="clear:both"></div>  
            <hr class="lineSolidOrderSumm">
          
            <div class="cellOrderSummTotal">
                <div class="orderSummItem"><p><span>Order Total:</span></p></div>
                <div class="orderSummPrice"><p><span><strong><asp:Literal ID="OrderTotalLiteral" runat="server"></asp:Literal></strong></span></p></div>
            </div>                        
        </div>
    <div style="clear:both"></div>
    </div> 
    <!-- End div boxMainProducts-->
    <div class="boxBottonProducts"></div>	
</div>
<div style="padding-top:1em"></div>