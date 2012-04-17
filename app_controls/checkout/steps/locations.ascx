<%@ Control Language="C#" AutoEventWireup="true" CodeFile="locations.ascx.cs" Inherits="CheckoutStep_Locations" %>



<asp:Panel ID="BillingPanel" CssClass="boxItemForPurch" runat="server">
    <div class="boxItemMain1">
        <div class="titleShippAddress"><p>Enter Billing Address</p></div>
        <div class="reqInf"><p>* Required Fields</p></div>
        <div class="mainFormShipping">
            <div class="formCellAddress">
                <asp:Label ID="Label1" CssClass="formLabelAddress" runat="server">Business Name</asp:Label>
                <div class="formInput"><asp:TextBox ID="BillingBusinessNameTextbox" runat="server"></asp:TextBox></div>
                
            </div>
            <div class="formCellAddress">
                <asp:Label ID="Label2" CssClass="formLabelAddress" AssociatedControlID="BillingFullnameTextbox" runat="server">Full Name *</asp:Label>
                <div class="formInput"><asp:TextBox ID="BillingFullnameTextbox" runat="server"></asp:TextBox></div>
                <div class="formAlert"><asp:RequiredFieldValidator ValidationGroup="Locations" Display="Dynamic"  ID="BillingFullnameRequiredValidator" ControlToValidate="BillingFullnameTextbox" runat="server"><span class="error">< Required</span></asp:RequiredFieldValidator></div>
            </div>
            <div class="formCellAddress">
                <asp:Label ID="Label3" CssClass="formLabelAddress" AssociatedControlID="BillingAddressLine1Textbox" runat="server">Address Line 1 *</asp:Label>
                <div class="formInput"><asp:TextBox ID="BillingAddressLine1Textbox" runat="server"></asp:TextBox></div>
                <div class="formAlert"><asp:RequiredFieldValidator ValidationGroup="Locations" Display="Dynamic"  ID="BillingAddressLine1RequiredValidator" ControlToValidate="BillingAddressLine1Textbox" runat="server"><span class="error">< Required</span></asp:RequiredFieldValidator></div>
            </div>
            <div class="formCellAddress">
                <asp:Label ID="Label4" CssClass="formLabelAddress" AssociatedControlID="BillingAddressLine2Textbox" runat="server">Address Line 2</asp:Label>
                <div class="formInput"><asp:TextBox ID="BillingAddressLine2Textbox" runat="server"></asp:TextBox></div>
                <div class="formAlert"></div>
            </div>
            <div class="formCellAddress">
                <asp:Label ID="Label5" CssClass="formLabelAddress" AssociatedControlID="BillingCityTextbox" runat="server">City *</asp:Label>
                <div class="formInput"><asp:TextBox ID="BillingCityTextbox" runat="server"></asp:TextBox></div>
                <div class="formAlert"><asp:RequiredFieldValidator ValidationGroup="Locations" Display="Dynamic"  ID="BillingCityRequiredValidator" ControlToValidate="BillingCityTextbox" runat="server"><span class="error">< Required</span></asp:RequiredFieldValidator></div>
            </div>
            <div class="formCellAddress">
                <asp:Label ID="Label6" CssClass="formLabelAddress" AssociatedControlID="BillingStateDropdown" runat="server">State *</asp:Label>
                <div class="formInput"><asp:DropDownList ID="BillingStateDropdown" runat="server"></asp:DropDownList></div>
                <div class="formAlert"><asp:RequiredFieldValidator ValidationGroup="Locations" Display="Dynamic"  ID="BillingStateRequiredValidator" ControlToValidate="BillingStateDropdown" runat="server"><span class="error">< Required</span></asp:RequiredFieldValidator></div>
            </div>
            <div class="formCellAddress">
                <asp:Label ID="BillingPostalCodeLabel" CssClass="formLabelAddress" AssociatedControlID="BillingPostalCodeTextbox" runat="server">Zip Code *</asp:Label>
                <div class="formInput"><asp:TextBox ID="BillingPostalCodeTextbox" runat="server"></asp:TextBox></div>
                <div class="formAlert">
                <asp:RegularExpressionValidator ValidationExpression="\d{5}(-\d{4})?" ValidationGroup="Locations" Display="Dynamic" ID="BillingPostalCodeLengthValidator" ControlToValidate="BillingPostalCodeTextbox" runat="server"><span class="error">< Required</span></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ValidationGroup="Locations" Display="Dynamic"  ID="BillingPostalCodeRequiredValidator" ControlToValidate="BillingPostalCodeTextbox" runat="server"><span class="error">< Required</span></asp:RequiredFieldValidator></div>
            </div>
            <div class="formCellAddress">
                <asp:Label ID="Label8" CssClass="formLabelAddress" AssociatedControlID="BillingPhoneTextbox" runat="server">Phone Number *</asp:Label>
                <div class="formInput"><asp:TextBox ID="BillingPhoneTextbox" runat="server"></asp:TextBox></div>
                <div class="formAlert"><asp:RequiredFieldValidator ValidationGroup="Locations" Display="Dynamic"  ID="BillingPhoneRequiredValidator" ControlToValidate="BillingPhoneTextbox" runat="server"><span class="error">< Required</span></asp:RequiredFieldValidator></div>
            </div>
            <div class="formCellAddress">
                <asp:Label ID="Label9" CssClass="formLabelAddress" AssociatedControlID="BillingEmailTextbox" runat="server">Email *</asp:Label>
                <div class="formInput"><asp:TextBox ID="BillingEmailTextbox" runat="server"></asp:TextBox></div>
                <div class="formAlert">
                    <asp:RequiredFieldValidator ValidationGroup="Locations" Display="Dynamic"  ID="BillingEmailRequiredValidator" ControlToValidate="BillingEmailTextbox" runat="server"><span class="error">< Required</span></asp:RequiredFieldValidator>
                    <asp:CustomValidator ValidationGroup="Locations" Display="Dynamic"  ID="EmailValidValidator" runat="server"><span class="error">Valid Email Address Required</span></asp:CustomValidator>    
                </div>
            </div>
            <div class="formCellAddress">
                <asp:Label ID="Label10" CssClass="formLabelAddress" AssociatedControlID="BillingEmailConfirmationTextbox" runat="server">Re-enter Email *</asp:Label>
                <div class="formInput"><asp:TextBox ID="BillingEmailConfirmationTextbox" runat="server"></asp:TextBox></div>
                <div class="formAlert">
                    <asp:RequiredFieldValidator ValidationGroup="Locations" Display="Dynamic"  ID="BillingEmailConfirmationRequiredValidator" ControlToValidate="BillingEmailConfirmationTextbox" runat="server"><span class="error">< Required</span></asp:RequiredFieldValidator>
                    <asp:CustomValidator ValidationGroup="Locations" Display="Dynamic"  ID="EmailConfirmationValidator" runat="server"><span class="error">Email and Email Confirmation Does Not Match</span></asp:CustomValidator>    
                </div>
            </div>
        </div>
    </div>
    
</asp:Panel>

<div class="dividerLineBox"></div>

<div class="boxItemMain3">
    <div class="controlShipAddreSame">
    <asp:CheckBox ID="SameShipping" Text="<p>Shipping Address same as Billing Address</p>" runat="server"/></div>
    
    <asp:Panel ID="AddressBookPanel" Visible="false" runat="server">
        <div class="btnAddressBook">
            <div class="contAddressBook"><a id="addressBookButton" href="#" onclick="popMyAddress(); return false"><img src="images/buttonAddressBook.jpg" alt="Address Book"/></a></div>
  		    <script type="text/javascript">
  		    	function popMyAddress(){
  		    	    var buttonCoords = $("addressBookButton").getCoordinates();
  		    	    
		            $('myAddress').toggleClass('hidden');
		            
		            var addressCoords = $('myAddress').getCoordinates();
		            
		            $('myAddress').setStyles({
		                'top':buttonCoords.bottom,
		                'left':buttonCoords.left - addressCoords.width + 150
		            });
	            }
			    var box = {};
			    window.addEvent('domready', function(){
				    box = new MultiBox('popNewAddress', {descClassName: 'multiBoxDesc', useOverlay: true});
			    });
		    </script>
            <div><p>Select a Saved Shipping Address</p></div>
        </div>       
    </asp:Panel>                                                                                                                                    
   
    <div id="myAddress" style="width:542px; position:absolute;" class="hidden">
        <div class="roundcont" style="width:542px;">
            <div class="tabPop"><img src="images/popNewAddreTab.gif" width="23" height="20"/></div> 
            <div style="background-color:#eee; clear:both">
                <div class="roundtop"><img src="images/popNewAddreCorner1.jpg" alt="" width="5" height="5" class="corner" /></div>   
                <div id="btnWhatIsClose" onclick="popMyAddress();"><p>Close</p></div>
                <asp:Panel ID="AddressBookContentsPanel" Visible="false" runat="server"></asp:Panel>
                <div style="clear:both"></div>
                <div class="roundbottom" style="clear:both"><img src="images/popNewAddreCorner3.jpg" alt="" width="5" height="5" class="corner" /></div>
            </div>
        </div>
    </div>
    <div class="clear"></div> 
</div>

<div class="dividerLineBox"></div>

<asp:Panel ID="ShippingPanel" CssClass="boxItemForPurch" runat="server">
<asp:Panel ID="MessagePanel" runat="server"></asp:Panel>
    <div class="boxItemMain2">
        <div class="titleShippAddress"><p>Enter a Shipping Address</p></div>
        
        <asp:Panel ID="AddressBookAddressPanel" runat="server"></asp:Panel>
        
        <asp:Panel ID="ShippingAddressFieldsPanel" runat="server">
            <div class="reqInf"><p>* Required Fields</p></div>
            <div class="mainFormShipping">
                <asp:HiddenField ID="ShippingAddressBookLocationID" runat="server" />
                <div class="formCellAddress">
                    <asp:Label ID="Label11" CssClass="formLabelAddress" AssociatedControlID="ShippingBusinessNameTextbox" runat="server">Business Name</asp:Label>
                    <div class="formInput"><asp:TextBox ID="ShippingBusinessNameTextbox" runat="server"></asp:TextBox></div>                    
                </div>
                <div class="formCellAddress">
                    <asp:Label ID="Label12" CssClass="formLabelAddress" AssociatedControlID="ShippingFullnameTextbox" runat="server">Full Name *</asp:Label>
                    <div class="formInput"><asp:TextBox ID="ShippingFullnameTextbox" runat="server"></asp:TextBox></div>
                    <div class="formAlert"><asp:RequiredFieldValidator ValidationGroup="Locations" Display="Dynamic"   ID="ShippingFullnameRequiredValidator" ControlToValidate="ShippingFullnameTextbox" runat="server"><span class="error">< Required</span></asp:RequiredFieldValidator></div>
                </div>
                <div class="formCellAddress">
                    <asp:Label ID="Label13" CssClass="formLabelAddress" AssociatedControlID="ShippingAddressLine1Textbox" runat="server">Address Line 1 *</asp:Label>
                    <div class="formInput"><asp:TextBox ID="ShippingAddressLine1Textbox" runat="server"></asp:TextBox></div>
                    <div class="formAlert"><asp:RequiredFieldValidator ValidationGroup="Locations" Display="Dynamic"   ID="ShippingAddressLine1RequiredValidator" ControlToValidate="ShippingAddressLine1Textbox" runat="server"><span class="error">< Required</span></asp:RequiredFieldValidator></div>
                </div>
                <div class="formCellAddress">
                    <asp:Label ID="Label14" CssClass="formLabelAddress" AssociatedControlID="ShippingAddressLine2Textbox" runat="server">Address Line 2</asp:Label>
                    <div class="formInput"><asp:TextBox ID="ShippingAddressLine2Textbox" runat="server"></asp:TextBox></div>
                    <div class="formAlert"></div>
                </div>
                <div class="formCellAddress">
                    <asp:Label ID="Label15" CssClass="formLabelAddress" AssociatedControlID="ShippingCityTextbox" runat="server">City *</asp:Label>
                    <div class="formInput"><asp:TextBox ID="ShippingCityTextbox" runat="server"></asp:TextBox></div>
                    <div class="formAlert"><asp:RequiredFieldValidator ValidationGroup="Locations" Display="Dynamic"   ID="ShippingCityRequiredValidator" ControlToValidate="ShippingCityTextbox" runat="server"><span class="error">< Required</span></asp:RequiredFieldValidator></div>
                </div>
                <div class="formCellAddress">
                    <asp:Label ID="Label16" CssClass="formLabelAddress" AssociatedControlID="ShippingStateDropdown" runat="server">State *</asp:Label>
                    <div class="formInput"><asp:DropDownList ID="ShippingStateDropdown" runat="server"></asp:DropDownList></div>
                    <div class="formAlert"><asp:RequiredFieldValidator ValidationGroup="Locations" Display="Dynamic"   ID="ShippingStateRequiredValidator" ControlToValidate="ShippingStateDropdown" runat="server"><span class="error">< Required</span></asp:RequiredFieldValidator></div>
                </div>
                <div class="formCellAddress">
                    <asp:Label ID="ShippingPostalCodeLabel" CssClass="formLabelAddress" AssociatedControlID="ShippingPostalCodeTextbox" runat="server">Zip Code *</asp:Label>
                    <div class="formInput"><asp:TextBox ID="ShippingPostalCodeTextbox" runat="server"></asp:TextBox></div>
                    <div class="formAlert">
                    <asp:RegularExpressionValidator ValidationExpression="\d{5}(-\d{4})?" ValidationGroup="Locations" Display="Dynamic" ID="ShippingPostalCodeLengthValidator" ControlToValidate="ShippingPostalCodeTextbox" runat="server"><span class="error">< Required</span></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ValidationGroup="Locations" Display="Dynamic"   ID="ShippingPostalCodeRequiredValidator" ControlToValidate="ShippingPostalCodeTextbox" runat="server"><span class="error">< Required</span></asp:RequiredFieldValidator></div>
                </div>
                <div class="formCellAddress">
                    <asp:Label ID="Label18" CssClass="formLabelAddress" AssociatedControlID="ShippingPhoneTextbox" runat="server">Phone Number*</asp:Label>
                    <div class="formInput"><asp:TextBox ID="ShippingPhoneTextbox" runat="server"></asp:TextBox></div>
                    <div class="formAlert"><asp:RequiredFieldValidator ValidationGroup="Locations" Display="Dynamic"   ID="ShippingPhoneRequiredValidator" ControlToValidate="ShippingPhoneTextbox" runat="server"><span class="error">< Required</span></asp:RequiredFieldValidator></div>
                </div>
                <div class="container" style="text-align:center">
                    <asp:LinkButton ID="SaveLocationButton" CausesValidation="false" OnClick="SaveLocationButton_Clicked" runat="server"><img src="images/btnPlusSavetoAdd.jpg" alt="Save to Address Book" /></asp:LinkButton>
                </div>
            </div>
        </asp:Panel>
   
    </div>
</asp:Panel>
<%
    ShippingBusinessNameTextbox.Text = "Texto";
 %>

<div class="dividerLineBox"></div>
<div class="boxItemMain2">
    <div class="btnContinue"><asp:LinkButton ID="ContinueButton" ValidationGroup="Locations" Display="Dynamic"  Text="Continue" OnClientClick="CheckSameShipping()" OnClick="ContinueButton_Clicked" runat="server"><img src="images/buttonContinue.jpg" alt="Continue" /></asp:LinkButton></div>
</div>