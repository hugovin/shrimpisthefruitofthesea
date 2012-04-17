<%@ Page Language="C#" AutoEventWireup="true" CodeFile="print_order.aspx.cs" Inherits="PrintOrderPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Print Order</title>
    </head>
    <body>
        <form id="PageForm" runat="server">
            <h1>Order Details</h1>
            <a href="#close" onclick="window.close()">Close</a>
            <a href="#print" onclick="window.print()">Print</a>
            
            <asp:Panel ID="ShippingLocationSummaryPanel" runat="server">
                <h3>Shipping To</h3>
                <asp:PlaceHolder ID="ShippingSummaryPlaceholder" runat="server"></asp:PlaceHolder>
            </asp:Panel>

            <asp:Panel ID="ShippingOptionSummaryPanel" Visible="false" runat="server">
                <h3>Shipping Option</h3>
                <asp:PlaceHolder ID="ShippingOptionSummaryPlaceholder" runat="server"></asp:PlaceHolder>
            </asp:Panel>

            <asp:Panel ID="PaymentSummaryPanel" runat="server">
                <h3>Payment Information</h3>
                ER OrderId #<%Response.Write(orderid); %><br />
                PO #<%Response.Write(POrder); %>;
             </asp:Panel>

            <asp:Panel ID="BillingLocationSummaryPanel" Visible="false" runat="server">
                <h3>Billing Location</h3>
                <asp:PlaceHolder ID="BillingSummaryPlaceholder" runat="server"></asp:PlaceHolder>
             </asp:Panel>

            <asp:Panel ID="ItemsSummaryPanel" runat="server">
                <h3>Items Being Purchased</h3>
                <asp:PlaceHolder ID="ItemsSummaryPlaceholder" runat="server"></asp:PlaceHolder>
             </asp:Panel>
        </form>
    </body>
</html>
