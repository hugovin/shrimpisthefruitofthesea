﻿<%@ Control ClassName="uc_FeatureProduct" Language="C#" AutoEventWireup="true" CodeBehind="uc_FeatureProduct.ascx.cs"
    CodeFile="uc_FeatureProduct.ascx.cs" Inherits="uc_Right.uc_FeatureProduct" %>
    
    
<div runat="server" id="div_Wrapper" visible="false">
    <div id="boxFeaturedProd">
        <div class="boxTitleProducts">
            Featured Product
        </div>
        <div class="boxMainProducts">
            <asp:PlaceHolder ID="Product1" runat="server"></asp:PlaceHolder>
            <!-- End div boxProduct-->
            <asp:PlaceHolder ID="Product2" runat="server"></asp:PlaceHolder>
            <!-- End div boxProduct-->
            <div class="clear"></div>
            <div class="bottonMore plusSignProducts">
                <a href="Classification.aspx?cp=1">
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
