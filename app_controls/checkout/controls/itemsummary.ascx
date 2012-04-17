<%@ Control Language="C#" AutoEventWireup="true" CodeFile="itemsummary.ascx.cs" Inherits="CheckoutControls_ItemSummary" %>
<table class="contFormSummary">

    <asp:Repeater ID="ItemsDataRepeater" runat="server">
        <HeaderTemplate>
            <tr class="formSummary">
                <th width="258" align="left" class="formDescSummary">Item</th>
                <th width="25" align="left" class="formQtySummary">Qty</th>
                <th width="66" align="left" class="formUnitSummary">Unit Price</th>
                <th width="73" align="left" class="formTotalSummary">Total Price</th>
            </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr class="formSummary" bgcolor="#e4e4df">
                <td class="formDescSummary"><p><asp:Literal ID="ItemNameLiteral" Text="<%#Bind('Title')%>" runat="server"></asp:Literal></p></td>
                <td class="formQtySummary"><p><asp:Literal ID="ItemQuantityLiteral" Text="<%#Bind('Quantity')%>" runat="server"></asp:Literal></p></td>
                <td class="formUnitSummary" style="text-align:center"><p><asp:Literal ID="ItemUnitPriceLiteral" Text="<%#Bind('UnitPrice')%>" runat="server"></asp:Literal></p></td>
                <td class="formTotalSummary" style="text-align:right"><p><asp:Literal ID="ItemTotalPriceLiteral"  Text="<%#Bind('SubTotal')%>" runat="server"></asp:Literal></p></td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
<table class="totalTable">
    <tr class="itemsPrice">
        <td width="258"></td>
        <td width="13"></td>
        <td align="right" class="cellItem"><p>Subtotal:</p></td>        
        <td width="67" class="cellPrice" style="text-align:right"><p><asp:Literal ID="SubTotalLiteral" runat="server"></asp:Literal></p></td>
      </tr>  
    <tr class="itemsPrice">
            <td></td>        
            <td></td>        
            <td align="right"  class="cellItem" id="TaxLabel" runat="server"><p>Tax:</p></td>                        
            <td width="67"  class="cellPrice" style="text-align:right"><p><asp:Literal ID="TaxLiteral" runat="server"></asp:Literal></p></td>
          </tr>      
    <tr class="itemsPrice">
                <td></td>
            <td></td>
            <td align="right"  class="cellItem" id="ShippingLabel" runat="server"><p>Shipping:</p></td>               
            <td width="67" class="cellPrice" style="text-align:right"><p><asp:Literal ID="ShippingLiteral" runat="server"></asp:Literal></p></td>
        </tr>    
    <tr class="itemsPrice">
                <td></td>
            <td></td>
            <td align="right"  class="cellItem" id="TotalLabel" runat="server"><p><span><strong>Total Price:</strong></span></p></td>            
            <td width="67" class="cellPrice" style="text-align:right"><p><span><strong><asp:Literal ID="TotalPriceLiteral" runat="server"></asp:Literal></strong></span></p></td>
        </tr> 
</table>

