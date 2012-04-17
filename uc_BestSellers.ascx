<%@ Control ClassName="uc_BestSellers" Language="C#" AutoEventWireup="true" CodeBehind="~/uc_BestSellers.ascx.cs" CodeFile="uc_BestSellers.ascx.cs" Inherits="uc_Right.uc_BestSellers" %>


<div runat="server" id="div_Wrapper" visible="false">
    <div id="boxBestSellers">
        <div class="boxTitleProducts">
            Best Sellers
        </div>
        <div class="boxMainProducts">
            <ul class="colorLinks">
                <asp:PlaceHolder ID="Product2" runat="server"></asp:PlaceHolder>
            </ul>
            <div class="bottonMore plusSignProducts">
                <a href="Classification.aspx?cp=3">
                    <img src="<% = Global.globalSiteImagesPath %>/plus_sign.gif" width="8" height="8" />
                    more</a></div>
        </div>
        <!-- End div boxMainProducts-->
        <div class="boxBottonProducts">
        </div>
    </div>
</div>
