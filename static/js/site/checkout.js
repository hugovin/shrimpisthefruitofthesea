 function escapeHTMLEncode(str) {
  var div = document.createElement('div');
  var text = document.createTextNode(str);
  div.appendChild(text);
  return div.innerHTML;
 }

function CopyLocation()
{
    if(SameShipping.checked)
    {
        /*
        ShippingBusinessNameTextbox.value = BillingBusinessNameTextbox.value;
        ShippingFullnameTextbox.value = BillingFullnameTextbox.value;
        ShippingAddressLine1Textbox.value = BillingAddressLine1Textbox.value;
        ShippingAddressLine2Textbox.value = BillingAddressLine2Textbox.value;
        ShippingCityTextbox.value = BillingCityTextbox.value;
        ShippingStateDropdown.value = BillingStateDropdown.value;
        ShippingPostalCodeTextbox.value = BillingPostalCodeTextbox.value;
        ShippingPhoneTextbox.value = BillingPhoneTextbox.value;
        */
        
        if( BillingFullnameTextbox.value != '' && 
            BillingAddressLine1Textbox.value != '' && 
            BillingCityTextbox.value != '' && 
            BillingStateDropdown.value != '' && 
            BillingPostalCodeTextbox.value != '' && 
            BillingPhoneTextbox.value != '')
            {
                DisplayAddressBookSelection("", BillingBusinessNameTextbox.value, BillingFullnameTextbox.value, BillingAddressLine1Textbox.value, BillingAddressLine2Textbox.value, BillingCityTextbox.value, BillingStateDropdown.value, BillingPostalCodeTextbox.value, BillingPhoneTextbox.value, true);            
            }
            else
            {
                SameShipping.checked = false;
                alert('You must complete the billing address before copying');
            }
    }
    else
    {
        HideAddressBookAddressPanel();
        ShowShippingFields();
        //ClearShippingLocation();
    }
}

function CheckSameShipping()
{
    if(SameShipping.checked)
    {
        CopyLocation();
    }
}

function SetShippingLocation(id, businessname, fullname, address1, address2, city, state, zip, phone)
{
    ShippingAddressBookLocationID.value = id;
    ShippingBusinessNameTextbox.value = businessname;
    ShippingFullnameTextbox.value = fullname;
    ShippingAddressLine1Textbox.value = address1;
    ShippingAddressLine2Textbox.value = address2;
    ShippingCityTextbox.value = city;
    ShippingStateDropdown.value = state;
    ShippingPostalCodeTextbox.value = zip;
    ShippingPhoneTextbox.value = phone;
}

function ShowAddressBookAddressPanel()
{
    AddressBookAddressPanel.removeClass('hidden');
}

function HideAddressBookAddressPanel()
{
    AddressBookAddressPanel.addClass('hidden');
}

function HideAddressBookPopup()
{
    $('myAddress').addClass('hidden');
}

function ShowShippingFields()
{
    ShippingAddressFieldsPanel.removeClass('hidden');
}

function HideShippingFields()
{
    ShippingAddressFieldsPanel.addClass('hidden');
}

function EditAddressBookSelection(id, businessname, fullname, address1, address2, city, state, zip, phone, same)
{
    DisplayAddressBookSelection(id, businessname, fullname, address1, address2, city, state, zip, phone, same);
    HideAddressBookAddressPanel();
    HideAddressBookPopup();
    ShowShippingFields();
}

function DisplayAddressBookSelection(id, businessname, fullname, address1, address2, city, state, zip, phone, same)
{
    id = escapeHTMLEncode(id);
    businessname = escapeHTMLEncode(businessname);
    fullname = escapeHTMLEncode(fullname);
    address1 = escapeHTMLEncode(address1);
    address2 = escapeHTMLEncode(address2);
    city = escapeHTMLEncode(city);
    state = escapeHTMLEncode(state);
    zip = escapeHTMLEncode(zip);
    phone = escapeHTMLEncode(phone);

    if(same != undefined && same != null) SetIsSame(same);
    else SetIsSame(false);
    
    SetShippingLocation(id, businessname, fullname, address1, address2, city, state, zip, phone);
    ShowAddressBookAddressPanel();
    HideAddressBookPopup();
    HideShippingFields();
    
    AddressBookAddressPanel.empty();

    var p_businessname = new Element("p", {"class":"businessnameShipp"});
    p_businessname.set('html', businessname);
    AddressBookAddressPanel.grab(p_businessname);

    var p_fullname = new Element("p");
    p_fullname.set('html', fullname);
    AddressBookAddressPanel.grab(p_fullname);

    var p_address1 = new Element("p");
    p_address1.set('html', address1);
    AddressBookAddressPanel.grab(p_address1);

    var p_address2 = new Element("p");
    p_address2.set('html', address2);
    AddressBookAddressPanel.grab(p_address2);

    var p_city = new Element("p");
    p_city.set('html', city);
    AddressBookAddressPanel.grab(p_city);

    var p_state = new Element("p");
    p_state.set('html', state);
    AddressBookAddressPanel.grab(p_state);

    var p_zip = new Element("p");
    p_zip.set('html', zip);
    AddressBookAddressPanel.grab(p_zip);

    var p_phone = new Element("p");
    p_phone.set('html', phone);
    AddressBookAddressPanel.grab(p_phone);
    
    var input_button = new Element("a", {"href":"#edit", "class":"btnEditAdd"});
    input_button.set('html', "");
    input_button.addEvent('click', function(e){ e.stop(); ShowShippingFields(); SetIsSame(false); HideAddressBookAddressPanel(); }); 
    AddressBookAddressPanel.grab(input_button);
    
    var input_button = new Element("a", {"href":"#new", "class":"btnNewAdd"});
    input_button.set('html', "");
    input_button.addEvent('click', function(e){ e.stop(); ShowShippingFields(); SetIsSame(false); ClearShippingFields(); HideAddressBookAddressPanel(); }); 
    AddressBookAddressPanel.grab(input_button);
}

function SetIsSame(checked)
{
    SameShipping.checked = checked;
}

function ClearShippingFields()
{
    ShippingAddressBookLocationID.value = '';
    ShippingBusinessNameTextbox.value = '';
    ShippingFullnameTextbox.value = '';
    ShippingAddressLine1Textbox.value = '';
    ShippingAddressLine2Textbox.value = '';
    ShippingCityTextbox.value = '';
    ShippingStateDropdown.value = '';
    ShippingPostalCodeTextbox.value = '';
    ShippingPhoneTextbox.value = '';
    /*ShippingBusinessNameTextbox.disabled = true;
    ShippingFullnameTextbox.disabled = true;
    ShippingAddressLine1Textbox.disabled = true;
    ShippingAddressLine2Textbox.disabled = true;
    ShippingCityTextbox.disabled = true;
    ShippingStateDropdown.disabled = true;
    ShippingPostalCodeTextbox.disabled = true;
    ShippingPhoneTextbox.disabled = true;
    ShippingEmailTextbox.disabled = true;*/
}

function RegisterLocationsEvents()
{
   SameShipping.addEvent('click', CopyLocation);
}
addOnLoad(RegisterLocationsEvents);