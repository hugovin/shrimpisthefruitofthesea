<%@ Control Language="C#" AutoEventWireup="true" CodeFile="paymentsummary.ascx.cs" Inherits="CheckoutControls_PaymentSummary" %>

<asp:Panel ID="CreditCardPanel" CssClass="mainPay" runat="server">
    <p class="infoProfile">Credit Card Type: <span><asp:Literal ID="CreditCardTypeLiteral" runat="server"></asp:Literal></span>
        <br /><br />
        Credit Card Number: <span><asp:Literal ID="CreditCardNumberLiteral" runat="server"></asp:Literal></span><br />
    </p>
</asp:Panel>

<asp:Panel ID="POPanel" CssClass="mainPay" Visible="false" runat="server">
    <p class="infoProfile">Purchase Order</p>
</asp:Panel>