<%@ Control ClassName="uc_WhatsNew" Language="C#" AutoEventWireup="true" CodeBehind="~/uc_WhatsNew.ascx.cs" CodeFile="uc_WhatsNew.ascx.cs" Inherits="uc_Right.uc_WhatsNew" %>

<div runat="server" id="div_Wrapper" visible="false">
    <div id="boxFeaturedProd">
        <div class="boxTitleProducts">
            Whats New
        </div>
        <div class="boxMainProducts">
            <asp:PlaceHolder ID="Product1" runat="server"></asp:PlaceHolder>
            <!-- End div boxProduct-->
            <asp:PlaceHolder ID="Product2" runat="server"></asp:PlaceHolder>
            <!-- End div boxProduct-->
            <div class="clear"></div>
            <div class="bottonMore plusSignProducts">
                <a href="WhatsNew.aspx?cp=5">
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