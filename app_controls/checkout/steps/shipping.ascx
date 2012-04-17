<%@ Control Language="C#" AutoEventWireup="true" CodeFile="shipping.ascx.cs" Inherits="CheckoutStep_Shipping" %>


<div class="boxItemForPurch">
    <div class="boxItemMain1">
        <h1>Choose shipping option</h1>
        <br />

        <p>We are currently only offering UPS Ground shipping.  Please call one of our customer service representatives if you would like an expedited shipment.</p>
<!--        <p><%if(SiteConstants.SiteName == "ER"){%>Educational Resources <%  } %><%if(SiteConstants.SiteName == "SB"){%>Sunburst <%  } %>is currently only offering UPS Ground shipping.  Please call one of our customer service representatives if you would like an expedited shipment.</p>-->
    </div>
</div>

<div class="dividerLineBox"></div>

<div class="boxItemForPurch">
    <div class="mainShipping">
        <div id="Sweight" class="optionChooseShipping">Shipping <asp:Literal ID="WeightLiteral" runat="server"></asp:Literal></div>
        <asp:Panel ID="NoOptionPanel" Visible="false" runat="server">
            <p>There was an error calculating shipping information for your shipping address.<br />Please <a href="checkout.aspx?move=1">Go Back</a> and edit your shipping address.</p>
        </asp:Panel>
        <asp:RadioButtonList ID="ShippingOptionRadio" runat="server"></asp:RadioButtonList>
        <div id="ErrorShip" style="visibility:hidden">
            <br />
            <b style="color:Red">You have to select a shipping option</b>
        </div>
        <div class="clear"></div>
    </div>
</div>

<div class="dividerLineBox"></div>
<div class="boxItemMain2">
    <div class="btnContinue"><asp:LinkButton ID="ContinueButton" Text="Continue" OnClick="ContinueButton_Clicked" runat="server"><img src="<% = Global.globalSiteImagesPath %>/buttonContinue.jpg" alt="Continue" /></asp:LinkButton></div>
</div>
<%if (checkthis == true) { Response.Write("<script type=\"text/javascript\">document.getElementById('ErrorShip').style.visibility = 'visible';</script>"); } %>              
<%if (Lowweight == true) { Response.Write("<script type=\"text/javascript\">document.getElementById('Sweight').style.visibility = 'hidden';</script>"); } %>              