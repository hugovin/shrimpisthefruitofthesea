<%@ Control ClassName="uc_RelatedProducts" Language="C#" AutoEventWireup="true" CodeBehind="~/uc_RelatedProducts.ascx.cs" CodeFile="uc_RelatedProducts.ascx.cs" Inherits="uc_Right.uc_RelatedProducts" %>

<div id="div_wraper" runat="server" visible="false">
<div id="boxFeaturedProd">
    <div class="boxTitleProducts">
        Related Products
    </div>
    <div class="boxMainProducts">
         <asp:PlaceHolder ID="Product1" runat="server">
         </asp:PlaceHolder>
        <!-- End div boxProduct-->
         <asp:PlaceHolder ID="Product2" runat="server">
         </asp:PlaceHolder>
        <!-- End div boxProduct-->
        <div class="clear"></div>
        <div class="bottonMore plusSignProducts">
            <a href="relatedproduct.aspx?p=<% = intProductId  %>">
                <img src="<% = Global.globalSiteImagesPath %>/plus_sign.gif" width="8" height="8" />
                more</a>
        </div>
    </div>
    <!-- End div boxMainProducts-->
    <div class="boxBottonProducts">
    </div>
</div>
</div>
<!-- End div boxProducts-->
