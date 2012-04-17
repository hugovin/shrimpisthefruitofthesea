<%@ Control ClassName="boxContact" Language="C#" AutoEventWireup="true" CodeBehind="boxContact.ascx.cs"
    CodeFile="boxContact.ascx.cs" Inherits="uc_Right.boxContact" %>
<div class="boxContact">
    <div class="boxContactTop">
    </div>
    <div class="MainBoxContact">
        <div class="titleContact">
            <img src="<% = Global.globalSiteImagesPath %>/titleContact.gif" /></div>
        <asp:PlaceHolder ID="PlaceHolder_Contact" runat="server"></asp:PlaceHolder>
        <div class="bottonMore1 plusSignContact">
            <a href="contactUs.aspx">
                <img src="<% = Global.globalSiteImagesPath %>/plus_sign-grey.gif" width="8" height="8" />
                more</a>
        </div>
        <hr class="blueDotts" />
        <!-- end cvSupport -->
        <a href="#Purchasing" id="A2" class="mb" title="" rel="type:element">
            <!--<img src="<% = Global.globalSiteImagesPath %>/bottonPurchasing.gif" width="127" height="27" />-->
            <div class="buttonPush">
            </div>
        </a>
    </div>
    <div class="boxContactBotton">
    </div>
</div>
