<%@ Page Language="C#" MasterPageFile="~/cart.master" AutoEventWireup="true" enableeventvalidation="false" CodeFile="cart.aspx.cs" Inherits="CartPage" Title="Cart" %>
<%@ Reference Control="~/boxContact.ascx" %>
<%@ Reference Control="~/uc_FeatureProduct.ascx" %>
<%@ Reference Control="~/uc_Specials.ascx" %>
<%@ Reference Control="~/uc_BestSellers.ascx" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadPlaceholder" Runat="Server">

    <script type="text/javascript" charset="utf-8" src="static/js/framework/onload.js"></script>
    <script type="text/javascript" charset="utf-8" src="static/js/framework/mootools-extensions.js"></script>
    <script type="text/javascript" charset="utf-8" src="static/js/framework/utils.js"></script>
    <script type="text/javascript" charset="utf-8" src="static/js/site/cart.js"></script>
    <script language="javascript" type="text/javascript" src="js/slideitmoo-1.1.js"></script>
    <style type="text/css">
        div.container { padding:20px; }
        div.button_row { padding:20px 0;} 
    </style>
    
    <script language="javascript" type="text/javascript">

            window.addEvents({
                'domready': function(){
                    /* thumbnails example , div containers */
                    try{
                    new SlideItMoo({
                                overallContainer: 'SlideItMoo_outer',
                                elementScrolled: 'SlideItMoo_inner',
                                thumbsContainer: 'SlideItMoo_items',		
                                itemsVisible:3,
                                itemsSelector: '.SlideItMoo_element',
                                itemWidth: 165,
                                showControls:1});
                                }catch(e){
                   // alert("Ivan se paseo en todo");
                 }  
                },
                'load': function(){
                    /* banner rotator example */
                    try{	
                    new SlideItMoo({overallContainer: 'SlideItMoo_banners_outer',
                                    elementScrolled: 'SlideItMoo_banners_inner',
                                    thumbsContainer: 'SlideItMoo_banners_items',		
                                    itemsVisible:0,
                                    itemsSelector: '.banner',
                                    showControls:0,
                                    autoSlide: 3000,
                                    transition: Fx.Transitions.Bounce.easeOut,
                                    duration: 1800,
                                    direction:-1});
                                    
                    /* info rotator example */	
                 }catch(e){
                    //alert("Ivan se paseo en todo");
                 }   
                }
            });
        </script>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyPlaceholder" Runat="Server">

    <script language="javascript" type="text/javascript">

        function hideModal()
        {
             document.getElementById('mbThanksReqQuote').style.visibility = 'hidden';
             
        }
        </script>
        
        
    <div id="cont">
        <span class="Apple-style-span" 
            style="border-collapse: separate; color: rgb(0, 0, 0); font-family: 'Times New Roman'; font-size: 16px; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: 2; text-align: auto; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-border-horizontal-spacing: 0px; -webkit-border-vertical-spacing: 0px; -webkit-text-decorations-in-effect: none; -webkit-text-size-adjust: auto; -webkit-text-stroke-width: 0px; ">
        <span class="Apple-style-span" 
            style="font-family: 'Lucida Grande'; font-size: 11px; text-indent: -12px; -webkit-text-size-adjust: none; ">
        </span></span><div id="mainAbout" class="main-content-resource">
            <asp:Panel ID="MessagePanel" Visible="false" runat="server"></asp:Panel>

            <h1>Your Cart </h1><br /> 
            
            <asp:Panel ID="CartDisplayPanel" runat="server">
                
                <asp:Panel ID="StudentItemMessagePanel" Visible="false" runat="server">
                  <p>  Your shopping cart contains products that require Academic Proof.  You will have to provide Academic Proof before we ship your products.</p>
                </asp:Panel>
                
                <div class="cartBtn">
                    <div id="checkoutBtn2">
                    <asp:LinkButton ID="CheckoutTopButton" Text="Checkout" OnClick="CheckoutButton_Clicked" runat="server"><img src="<% = Global.globalSiteImagesPath %>/checkoutBtn.jpg" alt="Checkout" /></asp:LinkButton>
                    </div>
                </div>

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
                                <div class="prodImg" style="overflow:hidden">
                                    <div id="boxContImage" ><asp:Literal ID="ProductImageLiteral" Text="" runat="server"></asp:Literal> </div>                      
                             </div>
                                <div class="prodCartDesc">
                                    <h2><asp:Literal ID="ProductTitleLiteral" Text="<%#Bind('Title')%>" runat="server"></asp:Literal></h2>
                                    <p>
                                        <em>by: </em>
                                        <asp:Literal ID="CompanyNameLinkLiteral" Text="" runat="server"></asp:Literal>
                                        <br />Item #: <asp:Literal ID="ProductItemNumberLiteral" Text="<%#Bind('ProductSKU')%>" runat="server"></asp:Literal>
                                    </p>
                                    
                                    <asp:Literal ID="WishlistLiteral" runat="server"></asp:Literal>                                                        
                                </div>
                                <div class="prodQua">
                                   <asp:TextBox ID="QtyTextbox" CssClass="quantity" Width="30" MaxLength="4" Text="<%#Bind('Quantity')%>" runat="server"></asp:TextBox>
                                    <div><asp:LinkButton ID="UpdateQtyButton" Text="Update" CommandName="UpdateQuantity" runat="server"></asp:LinkButton></div>
                                    <div><asp:LinkButton ID="RemoveItemButton" Text="Remove" CommandName="RemoveItem" runat="server"></asp:LinkButton></div>
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
                            <!--</div>-->
                        </FooterTemplate>
                    </asp:Repeater>
                <!--</div>-->
                
                <asp:Panel ID="GridPanel" Visible="false" runat="server">
                    <asp:GridView ID="ItemsGrid" Width="500" AutoGenerateColumns="false" runat="server">
                        <Columns>
                            <asp:BoundField Visible="false" DataField="CartItemId" />
                            <asp:BoundField Visible="false" DataField="ProductTitleId" />
                            <asp:BoundField Visible="false" DataField="ProductSKU" />
                            <asp:TemplateField HeaderText="Product">
                                <ItemTemplate>
                                    <div class="prodImg"><asp:Literal ID="ProductImageLiteral" Text="" runat="server"></asp:Literal></div>
                                    <div class="prodCartDesc">
                                        <h2><asp:Literal ID="ProductTitleLiteral" Text="<%#Bind('Title')%>" runat="server"></asp:Literal></h2>
                                        <p>
                                            <em>by: </em>
                                            <asp:Literal ID="CompanyNameLinkLiteral" Text="" runat="server"></asp:Literal>
                                            Item #: <asp:Literal ID="ProductItemNumberLiteral" Text="<%#Bind('ProductSKU')%>" runat="server"></asp:Literal>
                                        </p>
                                        <asp:LinkButton ID="AddToWishlistButton" Text="Add to Wishlist" CommandName="AddToWishlist" runat="server"></asp:LinkButton>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quantity">
                                <ItemTemplate>
                                    <asp:TextBox ID="QtyTextbox" CssClass="quantity" Width="30" MaxLength="4" Text="<%#Bind('Quantity')%>" runat="server"></asp:TextBox>
                                    <div><asp:LinkButton ID="UpdateQtyButton" Text="Update" CommandName="UpdateQuantity" runat="server"></asp:LinkButton></div>
                                    <div><asp:LinkButton ID="RemoveItemButton" Text="Remove" CommandName="RemoveItem" runat="server"></asp:LinkButton></div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Unit Price" DataField="UnitPriceDollar" />
                            <asp:BoundField HeaderText="Total" DataField="SubTotalDollar" />
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
                
                <div class="total_row">
                    <p style="text-align:right">Subtotal: <span class="value"><asp:Literal ID="TotalValueLiteral" runat="server"></asp:Literal></span></p>
                </div>
                
                <div class="cartBtn">
                    <asp:LinkButton ID="QuoteThisButton" Text="Quote This" Visible="false" OnClick="QuoteThisButton_Clicked" runat="server"><img src="<% = Global.globalSiteImagesPath %>/quote.jpg" alt="Quote This" /></asp:LinkButton>
                    <div id="checkoutB">
                    <asp:LinkButton ID="CheckoutBottomButton" Text="Checkout" OnClick="CheckoutButton_Clicked" runat="server"><img src="<% = Global.globalSiteImagesPath %>/checkoutBtn.jpg" alt="Checkout" /></asp:LinkButton>
                    </div>
                </div>
            </asp:Panel>
            
            <asp:Panel ID="CartEmptyPanel" Visible="false" runat="server">
                <P>You have an empty cart.</P>
            </asp:Panel>
            
            <asp:Panel ID="DebugPanel" Visible="false" runat="server">
            </asp:Panel>
            
            <asp:Panel ID="RelatedProductsPanel" Visible="false" runat="server">
                <div id="resultControls1">
                    <br />
                    <br />
                    <div id="RelPro" runat="server">
                    <h2>
                        Similar Products</h2>
                    <div id="SlideItMoo_outer" style="width:540px;">
                        <div id="SlideItMoo_inner">
                            <div id="SlideItMoo_items">
                            <asp:PlaceHolder ID="PlaceHolser_Slide" runat="server"></asp:PlaceHolder>
                            </div>
                        </div>
                    </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
        

        
        
        <div id="sidebar-content">
            <asp:PlaceHolder ID="PlaceHolder_boxContact" runat="server"></asp:PlaceHolder>
            <!-- End div boxContact-->
            <asp:PlaceHolder ID="PlaceHolder_uc_FeatureProduct" runat="server"></asp:PlaceHolder>
           	
		    <!-- End div boxProducts-->
		    <asp:PlaceHolder ID="PlaceHolder_uc_Specials" runat="server"></asp:PlaceHolder>
	        <!-- End div boxSpecials-->
		    <asp:PlaceHolder ID="PlaceHolder_uc_BestSellers" runat="server"></asp:PlaceHolder>					
            <!-- End div boxBestSellers-->
        </div>
        

    </div>

<div id="mbThanksReqQuote" style="visibility:hidden; position:absolute; top:40%; left:35%"> <!-- class="mbHidden" -->
                <div class="quote">
                    <div class="quoteTop">
                        <div class="popTitle">Please Contact Customer Service</div>
                        <div class="popClose" onclick="hideModal();" ></div>
                    </div>
                    <div class="quoteBody">
                        <div><h1>Order Weight has been reached</h1><br /><p>Please contact Customer Service at <a href="mailto:<% Response.Write(Session["siteCustEmail"]);%>"><% Response.Write(Session["siteCustEmail"]);%></a> for accurate shipping charges because of the weight and/or quantity you are ordering.  </p></div>
                        <div style="float:right; padding-top:50px;"><a href="#"><img src="<% = Global.globalSiteImagesPath %>/buttonContinueShopping.jpg" onclick="hideModal()"></a></div>
                        <div class="clear"></div>
                    </div>
                    <div class="quoteTButt">
                    </div>
                </div>
            </div>
            


            
 <%if (overweigth == true) { Response.Write("<script type=\"text/javascript\">document.getElementById('mbThanksReqQuote').style.visibility = 'visible';document.getElementById('checkoutB').style.visibility = 'hidden';document.getElementById('checkoutBtn2').style.visibility = 'hidden';</script>"); } %>      
</asp:Content>

