<%@ Page Language="C#" MasterPageFile="~/Print_cart.master" AutoEventWireup="true" CodeFile="wishlist_share.aspx.cs" Inherits="print" Title="Untitled Page" %>
    <%@ Reference Control="~/uc_FeatureProduct.ascx" %>
    <%@ Reference Control="~/uc_Specials.ascx" %>
<%@ Reference Control="~/boxContactPrint.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolder_boxContact" Runat="Server">
    <asp:PlaceHolder ID="PlaceHolder_boxContactPrint" runat="server"></asp:PlaceHolder>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HomeContent_Master" Runat="Server">          	    
    <div id="cont">
        <div id="mainAbout" class="main-content-resource">
            <asp:Panel ID="MessagePanel" Visible="false" runat="server"></asp:Panel>

            <h1>My Wishlist</h1><br /> 

            <asp:Panel ID="WishlistEmptyPanel" Visible="false" runat="server">
               <p>This Wishlist is Empty</p>
            </asp:Panel>

            <asp:Panel ID="WishlistDisplayPanel" runat="server">
                
                <asp:Panel ID="StudentItemMessagePanel" Visible="false" runat="server">
                </asp:Panel>

                <!--<div class="cart">-->
                    <asp:Repeater ID="ItemsList" runat="server">
                        <HeaderTemplate>
                            <div class="cartGrayBar">
                                <div class="prodImg">Product</div>
                                <div class="prodCartDesc">&nbsp;</div>
                                <div class="prodQua">Quantity</div>
                                <div class="prodPrice">Unit Price</div>
                                <div class="prodTotal">Total</div>
                            </div>
                            <!--<div class="items">-->
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="prodCart">
                                <div class="prodImg" style="overflow:hidden"><asp:Literal ID="ProductImageLiteral" Text="" runat="server"></asp:Literal></div>
                                <div class="prodCartDesc">
                                    <h2>
                                        <asp:Literal ID="ProductTitleLiteral" Text="<%#Bind('Title')%>" Visible="false" runat="server"></asp:Literal>
                                        <asp:Literal ID="ProductTitleLinkLiteral" runat="server"></asp:Literal>
                                    </h2>
                                    <p>
                                        <em>by: </em>
                                        <asp:Literal ID="CompanyNameLinkLiteral" Text="" runat="server"></asp:Literal>
                                        <br />Item #: <asp:Literal ID="ProductItemNumberLiteral" Text="<%#Bind('ProductSKU')%>" runat="server"></asp:Literal>
                                    </p>             
                                </div>
                                <div class="prodQua">
                                    <p><asp:Literal ID="QuantityLiteral" Text="<%#Bind('Quantity')%>" runat="server"></asp:Literal></p>
                                </div>
                                <div class="prodPrice">
                                    <p><asp:Literal ID="UnitPriceDollarLiteral" Text="<%#Bind('UnitPriceDollar')%>" runat="server"></asp:Literal></p>
                                </div>
                                <div class="prodTotal">
                                    <p><span><asp:Literal ID="SubTotalDollarLiteral" Text="<%#Bind('SubTotalDollar')%>" runat="server"></asp:Literal></span></p>
                                </div>
                            </div>
                        </ItemTemplate>
                        <FooterTemplate>
                        </FooterTemplate>
                    </asp:Repeater>
                <!--
                <div class="total_row" style="margin: 20px 0px 10px 0pt;">
                    <p style="text-align:right">Subtotal: <span class="value"><asp:Literal ID="TotalValueLiteral" runat="server"></asp:Literal></span></p>
                </div>            
                -->
            
            </asp:Panel>
        </div>
    </div>
</asp:Content>

