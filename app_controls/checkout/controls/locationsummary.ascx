<%@ Control Language="C#" AutoEventWireup="true" CodeFile="locationsummary.ascx.cs" Inherits="CheckoutControls_LocationSummary" %>
<div class="mainPay">
    <p class="titleProfile"><strong><asp:Literal ID="BusinessNameLiteral" runat="server"></asp:Literal></strong></p>
    <p class="infoProfile">
        <span class="fullname"><asp:Literal ID="FullNameLiteral" runat="server"></asp:Literal></span><br />
        <span class="address1"><asp:Literal ID="AddressLine1Literal" runat="server"></asp:Literal></span><br />
        <span class="address1"><asp:Literal ID="AddressLine2Literal" runat="server"></asp:Literal></span><br />
        <span class="city"><asp:Literal ID="CityLiteral" runat="server"></asp:Literal></span>, <span class="state"><asp:Literal ID="StateLiteral" runat="server"></asp:Literal></span><br />
        <span class="postal"><asp:Literal ID="ZipCodeLiteral" runat="server"></asp:Literal></span><br />
    </p>	
    <p class="contactProfile"><span class="phone"><asp:Literal ID="PhoneNumberLiteral" runat="server"></asp:Literal></span><br />
        <span class="email"><asp:Literal ID="EmailLiteral" runat="server"></asp:Literal></span>
    </p>
</div> 