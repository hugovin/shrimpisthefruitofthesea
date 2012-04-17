<%@ Control Language="C#" AutoEventWireup="true" CodeFile="address_block.ascx.cs" Inherits="CheckoutControls_AddressBlock" %>

<div class="boxUserAddress">
    <div class="boxTopAddress"></div>       
    <div class="boxContAddress">
        <div class="mainAddress">
            <div>                                            	
                <p><span><asp:Literal ID="BuildingNameLiteral" runat="server"></asp:Literal></span><p>
                <div style="border-top:dotted 1px #979797; margin:8px 0 14px 0;"></div>
                <p><strong><asp:Literal ID="AddressNameLiteral" runat="server"></asp:Literal></strong></p>
                <p><asp:Literal ID="Address1Literal" runat="server"></asp:Literal></p>
                <p><asp:Literal ID="Address2Literal" runat="server"></asp:Literal></p>
                <p><asp:Literal ID="CityLiteral" runat="server"></asp:Literal>, <asp:Literal ID="StateLiteral" runat="server"></asp:Literal> <asp:Literal ID="ZipLiteral" runat="server"></asp:Literal></p>
                <p><asp:Literal ID="PhoneLiteral" runat="server"></asp:Literal></p>
            </div> 
            <div class="controlMainAddress2">
                <div><asp:LinkButton ID="ChooseButton" runat="server"><img src="<% = Global.globalSiteImagesPath %>/buttonShipToAddre.jpg" alt="Ship to this Address" /></asp:LinkButton></div>
                <div class="btnUserAddress1"><asp:LinkButton ID="EditButton" runat="server"><img src="<% = Global.globalSiteImagesPath %>/buttonEdit.jpg" alt="Edit" /></asp:LinkButton></div>
                <div class="btnUserAddress2"><asp:LinkButton ID="DeleteButton" OnClick="DeleteButton_Clicked" runat="server"><img src="<% = Global.globalSiteImagesPath %>/buttonDelete.jpg" alt="Delete" /></asp:LinkButton></div>
                <div class="clear"></div>
            </div>
        </div>         
    </div>       
    <div class="boxBottonAddress"></div>       
</div>