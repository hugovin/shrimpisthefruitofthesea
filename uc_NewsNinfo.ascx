<%@ Control ClassName="uc_NewsNinfo" Language="C#" AutoEventWireup="true" CodeBehind="~/uc_NewsNinfo.ascx.cs" CodeFile="uc_NewsNinfo.ascx.cs" Inherits="uc_Right.uc_NewsNinfo" %>

<div runat="server" id="div_Wrapper" visible="false">
<div id="boxFeaturedProd">
    <div class="boxTitleProductsSpecial"> News &amp; Info</div>
    <div class="boxMainProducts">
        <div id="Product1" class="boxProductSpecial">
            <div>
                    <asp:PlaceHolder ID="Product" runat="server"></asp:PlaceHolder>
                           
            </div>
        </div>
        <!-- End div boxProduct-->
        <div class="bottonMore contSeeMore">
            <a href="generic_b.aspx?id=<%Response.Write(newid);%>&TypeGen=1">
                <img src="<% = Global.globalSiteImagesPath %>/plus_sign.gif" width="8" height="8" /> see more news and info</a>
        </div>
        <div class="clear">
        </div>
     </div>
     <div class="boxBottonProducts"></div>
</div>
</div>
    <!-- End div boxMainProducts--> 