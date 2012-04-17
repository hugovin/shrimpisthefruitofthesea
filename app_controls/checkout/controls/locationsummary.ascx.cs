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

public partial class CheckoutControls_LocationSummary : System.Web.UI.UserControl
{
    private CartLocation Location = null;
    public CheckoutControls_LocationSummary() { }
    public CheckoutControls_LocationSummary(CartLocation c) { this.Location = c; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Location == null) throw new CartException("Could not display location");

        BusinessNameLiteral.Text = Location.BusinessName;
        FullNameLiteral.Text = Location.FullName;
        AddressLine1Literal.Text = Location.Address1;
        AddressLine2Literal.Text = Location.Address2;
        CityLiteral.Text = Location.City;
        StateLiteral.Text = Location.StateCode;
        ZipCodeLiteral.Text = Location.PostalCode;
        PhoneNumberLiteral.Text = Location.Phone;
        if (Helper.StringExists(Location.Email) == false) EmailLiteral.Visible = false;
        EmailLiteral.Text = Location.Email;
    }
}
