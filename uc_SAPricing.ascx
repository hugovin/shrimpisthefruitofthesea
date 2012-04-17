<%@ Control ClassName="uc_SAPricing" Language="C#" AutoEventWireup="true" CodeBehind="~/uc_SAPricing.ascx.cs"  CodeFile="uc_SAPricing.ascx.cs" Inherits="uc_Right.uc_SAPricing" %>

<div runat="server" id="div_Wrapper" visible="false">
<div id="boxFeaturedProd">
    <div class="boxTitleProductsSpecial">
        Special Academic Pricing</div>
    <div class="boxMainProducts">
         <asp:PlaceHolder ID="Product1" runat="server">
         </asp:PlaceHolder>
    </div>
   
        <!-- End div boxProduct-->
        <div class="bottonMore plusSignProducts">
            <a href="#">
                <img src="<% = Global.globalSiteImagesPath %>/plus_sign.gif" width="8" height="8" />
                more</a>
        </div>
        <div class="clear">
        </div>
</div>
</div>
    <!-- End div boxMainProducts-->
