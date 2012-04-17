using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class CheckoutStep_Locations : System.Web.UI.UserControl
{
    private Cart CurrentCart = null;
    public CheckoutStep_Locations() { }
    public CheckoutStep_Locations(Cart c) { this.CurrentCart = c; }
    private int logId = 0;

    bool UserLoggedIn;
    bool IsCanada = false;
    string CountryCode = "US";

    string address_block_path = String.Format("{0}address_block.ascx", Constants.UserControls.CHECKOUT_CONTROLS_DIR);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.CurrentCart == null) throw new CartException("Could not load cart data");
        
        UserLoggedIn = CartUsers.IsUserLoggedIn(Session);

        SaveLocationButton.Visible = UserLoggedIn;

        EmailValidValidator.ServerValidate += new ServerValidateEventHandler(EmailValidValidator_ServerValidate);
        EmailConfirmationValidator.ServerValidate += new ServerValidateEventHandler(EmailConfirmationValidator_ServerValidate);
        PopulateStateControls();

        if (UserLoggedIn)
        {
            PopulateAddressBook();
            PopulateSavedAddresses();
        }else
        {
            PopulateStateControls();
            LoadData();
        }

		  try
        {
            ShippingBusinessNameTextbox.Text = Request.QueryString["sbn"].ToString();
            ShippingFullnameTextbox.Text = Request.QueryString["sfn"].ToString();
            ShippingAddressLine1Textbox.Text = Request.QueryString["saa"].ToString();
            ShippingAddressLine2Textbox.Text = Request.QueryString["sab"].ToString();
            ShippingCityTextbox.Text = Request.QueryString["sct"].ToString();
            string sssd = Request.QueryString["ssd"].ToString();
            ShippingStateDropdown.SelectedIndex = ShippingStateDropdown.Items.IndexOf(ShippingStateDropdown.Items.FindByValue(sssd));
            ShippingPostalCodeTextbox.Text = Request.QueryString["spc"].ToString();
            ShippingPhoneTextbox.Text = Request.QueryString["spt"].ToString();}
		catch (Exception exc)
        {
        }
        

        try
        {
            if (Session["RedirectFromSaveAddress"].ToString() == "yes")
            {
                MessagePanel.Controls.Add(new LiteralControl("<div style='margin: 0; float: right;'>Address Saved <img src='images/AddressSaved.jpg' alt='Adress saved'></div>"));
		  		Session["RedirectFromSaveAddress"]="no";
            }
        }
        catch (Exception RedirEX)
        {
             Session["RedirectFromSaveAddress"]="no"; //create the Session entry and set it to no. 
        }



        //
        RegisterScripts();
    }

    void EmailConfirmationValidator_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = BillingEmailTextbox.Text.ToLower() == BillingEmailConfirmationTextbox.Text.ToLower();
    }

    void EmailValidValidator_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = Helper.IsValidEmail(BillingEmailTextbox.Text);
    }

    private void RegisterScripts()
    {
        string events = @"<script type='text/javascript' src='static/js/site/checkout.js'></script>";
        Page.RegisterStartupScript("PageEvents", events);

        string script = String.Format("<script type=\"text/javascript\">{0}</script>", Helper.GenerateScriptToAccessControls(Page.Controls));
        Page.RegisterStartupScript("ControlRegistration", script);
    }

    private void PopulateStateControls()
    {
        BuildStateList(ref ShippingStateDropdown, IsCanada);
        BuildStateList(ref BillingStateDropdown, IsCanada);
        if (IsCanada)
        {
            BillingPostalCodeLabel.Text = ShippingPostalCodeLabel.Text = "Postal Code *";
        }
    }

    private void PopulateAddressBook()
    {
        if (UserLoggedIn)
        {
            int login_id = CartUsers.GetLoginID();

            LoginAddress[] addresses = LoginAddress.GetAddressesFromLoginID(login_id);
            if (addresses.Length >= 1)
            {
                AddressBookPanel.Visible = true;
                AddressBookContentsPanel.Visible = true;

                AddressBookContentsPanel.Controls.Clear();

                foreach (LoginAddress address in addresses)
                {
                    AddressBookContentsPanel.Controls.Add(Helper.LoadUserControl(Page, address_block_path, address));
                }

                return;
            }
        }

        AddressBookPanel.Visible = false;
        AddressBookContentsPanel.Visible = false;
    }

   private void PopulateSavedAddresses()
   {
       string state = "";
       DataSet userd = new DataSet();
       User use = new User();            
       if (UserLoggedIn)
       {
           int login_id = CartUsers.GetLoginID();
           
           CartLocation BillingLocation = null;
           BillingLocation = CartLocation.GetBillingLocationByUserID(login_id);

           if (BillingLocation != null)
           {
               BillingBusinessNameTextbox.Text = BillingLocation.BusinessName;
               BillingFullnameTextbox.Text = BillingLocation.FullName;
               BillingAddressLine1Textbox.Text = BillingLocation.Address1;
               BillingAddressLine2Textbox.Text = BillingLocation.Address2;
               BillingCityTextbox.Text = BillingLocation.City;
               if (BillingLocation.StateCode == "")
               {
                   userd = use.Get_User_State(login_id);
                   logId = Convert.ToInt32(login_id);
                   foreach (DataTable table in userd.Tables)
                   {
                       foreach (DataRow row in table.Rows)
                       {
                           state = row["State"].ToString();
                       }
                   }
                   if (state.Length > 3)
                   {
                       state = "";
                   }
               }
               else
               {
                   state = BillingLocation.StateCode;
               }
               BillingStateDropdown.SelectedIndex = BillingStateDropdown.Items.IndexOf(BillingStateDropdown.Items.FindByValue(state));
               BillingPostalCodeTextbox.Text = BillingLocation.PostalCode;
               BillingPhoneTextbox.Text = BillingLocation.Phone;
               BillingEmailTextbox.Text = BillingEmailConfirmationTextbox.Text = BillingLocation.Email;
           }
       }
   }

    private void BuildStateList(ref DropDownList DropDown, bool canada)
    {
        if (!canada)
        {
            DropDown.Items.Add(new ListItem("Choose a State", ""));
            DropDown.Items.Add(new ListItem("Alabama (AL)", "AL"));
            DropDown.Items.Add(new ListItem("Alaska (AK)", "AK"));
            DropDown.Items.Add(new ListItem("Arizona (AZ)", "AZ"));
            DropDown.Items.Add(new ListItem("Arkansas (AR)", "AR"));
            DropDown.Items.Add(new ListItem("California (CA)", "CA"));
            DropDown.Items.Add(new ListItem("Colorado (CO)", "CO"));
            DropDown.Items.Add(new ListItem("Connecticut (CT)", "CT"));
            DropDown.Items.Add(new ListItem("Delaware (DE)", "DE"));
            DropDown.Items.Add(new ListItem("District of Columbia (DC)", "DC"));
            DropDown.Items.Add(new ListItem("Florida (FL)", "FL"));
            DropDown.Items.Add(new ListItem("Georgia (GA)", "GA"));
            DropDown.Items.Add(new ListItem("Guam (GU)", "GU"));
            DropDown.Items.Add(new ListItem("Hawaii (HI)", "HI"));
            DropDown.Items.Add(new ListItem("Idaho (ID)", "ID"));
            DropDown.Items.Add(new ListItem("Illinois (IL)", "IL"));
            DropDown.Items.Add(new ListItem("Indiana (IN)", "IN"));
            DropDown.Items.Add(new ListItem("Iowa (IA)", "IA"));
            DropDown.Items.Add(new ListItem("Kansas (KS)", "KS"));
            DropDown.Items.Add(new ListItem("Kentucky (KY)", "KY"));
            DropDown.Items.Add(new ListItem("Louisiana (LA)", "LA"));
            DropDown.Items.Add(new ListItem("Maine (ME)", "ME"));
            DropDown.Items.Add(new ListItem("Maryland (MD)", "MD"));
            DropDown.Items.Add(new ListItem("Massachusetts (MA)", "MA"));
            DropDown.Items.Add(new ListItem("Michigan (MI)", "MI"));
            DropDown.Items.Add(new ListItem("Minnesota (MN)", "MN"));
            DropDown.Items.Add(new ListItem("Mississippi (MS)", "MS"));
            DropDown.Items.Add(new ListItem("Missouri (MO)", "MO"));
            DropDown.Items.Add(new ListItem("Montana (MT)", "MT"));
            DropDown.Items.Add(new ListItem("Nebraska (NE)", "NE"));
            DropDown.Items.Add(new ListItem("Nevada (NV)", "NV"));
            DropDown.Items.Add(new ListItem("New Hampshire (NH)", "NH"));
            DropDown.Items.Add(new ListItem("New Jersey (NJ)", "NJ"));
            DropDown.Items.Add(new ListItem("New Mexico (NM)", "NM"));
            DropDown.Items.Add(new ListItem("New York (NY)", "NY"));
            DropDown.Items.Add(new ListItem("North Carolina (NC)", "NC"));
            DropDown.Items.Add(new ListItem("North Dakota (ND)", "ND"));
            DropDown.Items.Add(new ListItem("Ohio (OH)", "OH"));
            DropDown.Items.Add(new ListItem("Oklahoma (OK)", "OK"));
            DropDown.Items.Add(new ListItem("Oregon (OR)", "OR"));
            DropDown.Items.Add(new ListItem("Pennyslvania (PA)", "PA"));
            DropDown.Items.Add(new ListItem("Puerto Rico (PR)", "PR"));
            DropDown.Items.Add(new ListItem("Rhode Island (RI)", "RI"));
            DropDown.Items.Add(new ListItem("South Carolina (SC)", "SC"));
            DropDown.Items.Add(new ListItem("South Dakota (SD)", "SD"));
            DropDown.Items.Add(new ListItem("Tennessee (TN)", "TN"));
            DropDown.Items.Add(new ListItem("Texas (TX)", "TX"));
            DropDown.Items.Add(new ListItem("Utah (UT)", "UT"));
            DropDown.Items.Add(new ListItem("Vermont (VT)", "VT"));
            DropDown.Items.Add(new ListItem("Virginia (VA)", "VA"));
            DropDown.Items.Add(new ListItem("Virgin Islands (VI)", "VI"));
            DropDown.Items.Add(new ListItem("Washington (WA)", "WA"));
            DropDown.Items.Add(new ListItem("West Virginia (WV)", "WV"));
            DropDown.Items.Add(new ListItem("Wisconsin (WI)", "WI"));
            DropDown.Items.Add(new ListItem("Wyoming (WY)", "WY"));
        }
        else
        {
            DropDown.Items.Add(new ListItem("Choose a Province"));
            DropDown.Items.Add(new ListItem("Alberta (AB)", "AB"));
            DropDown.Items.Add(new ListItem("British Columbia (BC)", "BC"));
            DropDown.Items.Add(new ListItem("Manitoba (MB)", "MB"));
            DropDown.Items.Add(new ListItem("New Brunswick (NB)", "NB"));
            DropDown.Items.Add(new ListItem("Newfoundland and Labrador (NL)", "NL"));
            DropDown.Items.Add(new ListItem("Northwest Territories (NT)", "NT"));
            DropDown.Items.Add(new ListItem("Nova Scotia (NS)", "NS"));
            DropDown.Items.Add(new ListItem("Nunavut (NU)", "NU"));
            DropDown.Items.Add(new ListItem("Prince Edward Island (PE)", "PE"));
            DropDown.Items.Add(new ListItem("Saskatchewan (SK)", "SK"));
            DropDown.Items.Add(new ListItem("Ontario (ON)", "ON"));
            DropDown.Items.Add(new ListItem("Quebec (QC)", "QC"));
            DropDown.Items.Add(new ListItem("Yukon (YT)", "YT"));
        }
    }

    private void LoadData()
    {
        string state = "";
        DataSet userd = new DataSet();
        User use = new User(); 
        CartLocation BillingLocation = CurrentCart.BillingLocation;
        CartLocation ShippingLocation = CurrentCart.ShippingLocation;

        if (BillingLocation != null)
        {
            BillingBusinessNameTextbox.Text = BillingLocation.BusinessName;
            BillingFullnameTextbox.Text = BillingLocation.FullName;
            BillingAddressLine1Textbox.Text = BillingLocation.Address1;
            BillingAddressLine2Textbox.Text = BillingLocation.Address2;
            BillingCityTextbox.Text = BillingLocation.City;
            if (BillingLocation.StateCode == "")
            {
                userd = use.Get_User_State(logId);
                foreach (DataTable table in userd.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        state = row["State"].ToString();
                    }
                }
                if (state.Length > 3)
                {
                    state = "";
                }
            }
            else
            {
                state = BillingLocation.StateCode;
            }
            BillingStateDropdown.SelectedIndex = BillingStateDropdown.Items.IndexOf(BillingStateDropdown.Items.FindByValue(state));
            BillingPostalCodeTextbox.Text = BillingLocation.PostalCode;
            BillingPhoneTextbox.Text = BillingLocation.Phone;
            BillingEmailTextbox.Text = BillingEmailConfirmationTextbox.Text = BillingLocation.Email;
        }

        if (ShippingLocation != null)
        {
            if (BillingLocation.StateCode == "")
            {
                userd = use.Get_User_State(logId);
                foreach (DataTable table in userd.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        state = row["State"].ToString();
                    }
                }
                if (state.Length > 3)
                {
                    state = "";
                }
            }
            else
            {
                state = BillingLocation.StateCode;
            }
            ShippingBusinessNameTextbox.Text = ShippingLocation.BusinessName;
            ShippingFullnameTextbox.Text = ShippingLocation.FullName;
            ShippingAddressLine1Textbox.Text = ShippingLocation.Address1;
            ShippingAddressLine2Textbox.Text = ShippingLocation.Address2;
            ShippingCityTextbox.Text = ShippingLocation.City;
            ShippingStateDropdown.SelectedIndex = ShippingStateDropdown.Items.IndexOf(ShippingStateDropdown.Items.FindByValue(state));
            ShippingPostalCodeTextbox.Text = ShippingLocation.PostalCode;
            ShippingPhoneTextbox.Text = ShippingLocation.Phone;
        }
    }

    protected void SaveLocationButton_Clicked(object sender, EventArgs e)
    {
        string ShippingBusinessName = ShippingBusinessNameTextbox.Text;
        string ShippingFullName = ShippingFullnameTextbox.Text;
        string ShippingAddress1 = ShippingAddressLine1Textbox.Text;
        string ShippingAddress2 = ShippingAddressLine2Textbox.Text;
        string ShippingCity = ShippingCityTextbox.Text;
        string ShippingStateCode = ShippingStateDropdown.SelectedValue;
        string ShippingPostalCode = ShippingPostalCodeTextbox.Text;
        string ShippingPhone = ShippingPhoneTextbox.Text;

        CartDB db = new CartDB();

        bool success = false;

        int address_book_id;
        if (Helper.StringExists(ShippingAddressBookLocationID.Value) && Int32.TryParse(ShippingAddressBookLocationID.Value, out address_book_id))
        {
            LoginAddress address = LoginAddress.GetAddressFromLoginAddressID(address_book_id);
            if (address != null && address.LoginID == CartUsers.GetLoginID())
            {
                if (db.LoginUpdAddress(address.LoginAddressID, address.LoginID, ShippingBusinessName, ShippingFullName, ShippingAddress1, ShippingAddress2, ShippingCity, ShippingStateCode, "", ShippingPostalCode, CountryCode, ShippingPhone, 0))
                {
                    success = true;
                }
            }
        }
        else
        {
            int login_address_id;
            if (db.LoginAddAddress(CartUsers.GetLoginID(), ShippingBusinessName, ShippingFullName, ShippingAddress1, ShippingAddress2, ShippingCity, ShippingStateCode, "", ShippingPostalCode, CountryCode,ShippingPhone, out login_address_id))
            {
                ShippingAddressBookLocationID.Value = login_address_id.ToString();
                success = true;
            }
        }

        if (success)
        {
            //MessagePanel.Controls.Add(new LiteralControl("Address Saved"));
            MessagePanel.Controls.Add(new LiteralControl("<div style='margin: 0; float: right;'><img src='images/AddressSaved.jpg' alt='Adress saved'></div>"));
            PopulateAddressBook();
            Session["RedirectFromSaveAddress"] = "yes"; 
            string sbn=ShippingBusinessNameTextbox.Text;
            string sfn=ShippingFullnameTextbox.Text;
            string saa=ShippingAddressLine1Textbox.Text;
            string sab=ShippingAddressLine2Textbox.Text;
            string sct=ShippingCityTextbox.Text;
            string ssd=ShippingStateDropdown.Text;
            string spc=ShippingPostalCodeTextbox.Text;
            string spt=ShippingPhoneTextbox.Text;
            Response.Redirect("checkout.aspx"+"?sbn=" + sbn +"&sfn=" + sfn +"&saa=" + saa +"&sab=" + sab +"&sct=" + sct +"&ssd=" + ssd +"&spc=" + spc +"&spt=" +spt +"");
		    // For testing only
			//Response.Redirect("http://webtest.edresources.com:8080/checkout.aspx");
              
        }
    }

    protected void ContinueButton_Clicked(object sender, EventArgs e)
    {
        bool success = false;

	if(Page.IsValid){
             
           int login_Uid = CartUsers.GetLoginID();           
           CartLocation BillLocation = null;
           BillLocation = CartLocation.GetBillingLocationByUserID(login_Uid);

        CartDB db = new CartDB();
        db.CartDeleteAllLocations(CurrentCart.CartId);

        string BillingBusinessName = BillingBusinessNameTextbox.Text;
        string BillingFullName = BillingFullnameTextbox.Text;
        string BillingAddress1 = BillingAddressLine1Textbox.Text;
        string BillingAddress2 = BillingAddressLine2Textbox.Text;
        string BillingCity = BillingCityTextbox.Text;
        string BillingStateCode = BillingStateDropdown.SelectedValue;
        string BillingPostalCode = BillingPostalCodeTextbox.Text;
        string BillingPhone = BillingPhoneTextbox.Text;
        string BillingEmail = BillingEmailTextbox.Text;
        string BillingEmailConfirmation = BillingEmailConfirmationTextbox.Text;

        string ShippingBusinessName = ShippingBusinessNameTextbox.Text;
        string ShippingFullName = ShippingFullnameTextbox.Text;
        string ShippingAddress1 = ShippingAddressLine1Textbox.Text;
        string ShippingAddress2 = ShippingAddressLine2Textbox.Text;
        string ShippingCity = ShippingCityTextbox.Text;
        string ShippingStateCode = ShippingStateDropdown.SelectedValue;
        string ShippingPostalCode = ShippingPostalCodeTextbox.Text;
        string ShippingPhone = ShippingPhoneTextbox.Text;

        int address_book_id;
        if (Helper.StringExists(ShippingAddressBookLocationID.Value) && Int32.TryParse(ShippingAddressBookLocationID.Value, out address_book_id))
        {
            LoginAddress address = LoginAddress.GetAddressFromLoginAddressID(address_book_id);
            if (address != null && address.LoginID == CartUsers.GetLoginID())
            {
                db.LoginUpdAddress(address.LoginAddressID, address.LoginID, ShippingBusinessName, ShippingFullName, ShippingAddress1, ShippingAddress2, ShippingCity, ShippingStateCode, "", ShippingPostalCode, CountryCode,ShippingPhone, 0);
            }
        }
        try
        {
            Session["UTitle"] = BillLocation.UTitle;
            Session["AreaCode"] = BillLocation.AreaCode;
            Session["PhoneExt"] = BillLocation.PhoneExt;
            Session["Fax"] = BillLocation.Fax;
        }
        catch (Exception erro)
        {
            Session["UTitle"] = "";
            Session["AreaCode"] = "";
            Session["PhoneExt"] = "";
            Session["Fax"] = "";
        }
        try
        {
            success = db.CartAddLocation(CurrentCart.CartId, CartDB.LocationType.Billing, BillingBusinessName, BillingFullName, BillingAddress1, BillingAddress2, BillingCity, BillingStateCode, BillingPostalCode, CountryCode, BillingPhone, BillingEmail, BillLocation.UTitle, BillLocation.AreaCode, BillLocation.PhoneExt, BillLocation.Fax);
            success = db.CartAddLocation(CurrentCart.CartId, CartDB.LocationType.Shipping, ShippingBusinessName, ShippingFullName, ShippingAddress1, ShippingAddress2, ShippingCity, ShippingStateCode, ShippingPostalCode, CountryCode, ShippingPhone, "", BillLocation.UTitle, BillLocation.AreaCode, BillLocation.PhoneExt, BillLocation.Fax);
        }
        catch (Exception cartErr)
        {
            success = db.CartAddLocation(CurrentCart.CartId, CartDB.LocationType.Billing, BillingBusinessName, BillingFullName, BillingAddress1, BillingAddress2, BillingCity, BillingStateCode, BillingPostalCode, CountryCode, BillingPhone, BillingEmail,"","", "", "");
            success = db.CartAddLocation(CurrentCart.CartId, CartDB.LocationType.Shipping, ShippingBusinessName, ShippingFullName, ShippingAddress1, ShippingAddress2, ShippingCity, ShippingStateCode, ShippingPostalCode, CountryCode, ShippingPhone, "", "", "", "", "");
        }

        if (success)
        {

			if (Session[Constants.SessionKeys.SUMMARY_EDIT] != null && (bool)Session[Constants.SessionKeys.SUMMARY_EDIT] == true)
			{
			    Session.Remove(Constants.SessionKeys.SUMMARY_EDIT);
			    db.CartUpdateDirty(CurrentCart.CartId, true);
			    CurrentCart.MoveToStep(Constants.CheckoutStep.Summary);
			    Response.Redirect(Constants.Pages.CHECKOUT);
			    return;
			}

            CurrentCart.MoveNextStep();
            Response.Redirect(Constants.Pages.CHECKOUT);
        }
    }
    }

}
