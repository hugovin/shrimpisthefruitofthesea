<%@ Control Language="C#" AutoEventWireup="true" CodeFile="summary.ascx.cs" Inherits="CheckoutStep_Summary" %>



<div id="print2">
<asp:Panel ID="ShippingLocationSummaryPanel" CssClass="contShipping2" runat="server">
    <div><h1>Order Details</h1></div><br  />
    <div class="summaryTitle"><p>Shipping To:</p></div>
    <asp:PlaceHolder ID="ShippingSummaryPlaceholder" runat="server"></asp:PlaceHolder>
   
    <div class="controlProfile">
        <asp:LinkButton ID="EditShippingButton" Text="Edit" OnClick="EditLocationsButton_Clicked" runat="server"><img src="<% = Global.globalSiteImagesPath %>/buttonEdit.jpg" alt="Edit" /></asp:LinkButton>
    </div>
</asp:Panel>

<asp:Panel ID="ShippingOptionSummaryPanel" CssClass="contShipping2" Visible="false" runat="server">
    <div class="summaryTitle"><p>Shipping Option:</p></div> 
    <asp:PlaceHolder ID="ShippingOptionSummaryPlaceholder" runat="server"></asp:PlaceHolder>
    <div class="controlProfile"><asp:LinkButton ID="EditShippingOptionButton" Text="Edit" OnClick="EditShippingOptionButton_Clicked" runat="server"><img src="<% = Global.globalSiteImagesPath %>/buttonEdit.jpg" alt="Edit" /></asp:LinkButton></dov>
</asp:Panel>

<asp:Panel ID="PaymentSummaryPanel" CssClass="contShipping2" runat="server">
    <div class="summaryTitle"><p>Billing Information:</p></div>  
    <asp:PlaceHolder ID="PaymentSummaryPlaceholder" runat="server"></asp:PlaceHolder>
    <div class="controlProfile">
        <asp:LinkButton ID="EditPaymentButton" Text="Edit"  OnClick="EditPaymentButton_Clicked" runat="server"><img src="<% = Global.globalSiteImagesPath %>/buttonEdit.jpg" alt="Edit" /></asp:LinkButton>
    </div>
</asp:Panel>

<asp:Panel ID="BillingLocationSummaryPanel" CssClass="contShipping2" runat="server">
    <div class="summaryTitle"><p>Billing To:</p></div>  
    <asp:PlaceHolder ID="BillingSummaryPlaceholder" runat="server"></asp:PlaceHolder>
    <div class="controlProfile"><asp:LinkButton ID="EditBillingButton" Text="Edit" OnClick="EditLocationsButton_Clicked" runat="server"><img src="<% = Global.globalSiteImagesPath %>/buttonEdit.jpg" alt="Edit" /></asp:LinkButton></div>
</asp:Panel>

<asp:Panel ID="ItemsSummaryPanel" CssClass="contShipping2" runat="server">
    <div class="summaryTitle"><p>Items Being Purchased</p></div>
    <asp:PlaceHolder ID="ItemsSummaryPlaceholder" runat="server"></asp:PlaceHolder>
    <div class="controlProfile"><asp:LinkButton ID="EditItemsButton" Text="Edit"  OnClick="EditItemsButton_Clicked" runat="server"><img src="<% = Global.globalSiteImagesPath %>/buttonEdit.jpg" alt="Edit" /></asp:LinkButton></div>
</asp:Panel>

<div class="contControlShipping">
    <div class="summaryTitle"><p>Comments / Special Instructions:</p></div>
    <p>
    If you are a tax exempt entity, please include your Tax ID in your comments below.  We will remove the tax from your order after verification.
    </p>

    <textarea id="SumaryComents" name="SumaryComents" cols="66" rows="10" runat="server"></textarea>   
    <br />
    <div style="text-align:center; margin-bottom:20px;"><asp:CheckBox ID="SalesRepFlagCheckbox" runat="server" Text="Yes, a sales rep helped me with this order" /></div>
    <div class="controlSummary">
        <asp:LinkButton ID="ContinueButton" Text="Place Order" OnClientClick="getScreen()" OnClick="ContinueButton_Clicked" runat="server"><img src="<% = Global.globalSiteImagesPath %>/buttonPlaceOrder.jpg" alt="Place Order" /></asp:LinkButton>
    </div>  
</div>

    <span id="emailTo"></span>
    
</div>

     <script language="javascript" type="text/javascript">
     
        var textComeFrom2 = '';
        var registerEmail2 = '';

            var cont = document.getElementById("print2").innerHTML;
            document.getElementById("emailTo").value = cont;

      
      function getScreen()
      {
        document.getElementById("emailTo").value = "";
        var cont = document.getElementById("print2").innerHTML;    
        document.getElementById("emailTo").value=cont;  
      }
      

     </script>



    