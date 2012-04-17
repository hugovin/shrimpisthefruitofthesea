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

public partial class CheckoutControls_AddressBlock : System.Web.UI.UserControl
{
    private LoginAddress Address = null;
    public CheckoutControls_AddressBlock() { }
    public CheckoutControls_AddressBlock(LoginAddress a) { this.Address = a; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Address == null) return;

        LoadData();

        string on_select = String.Format("DisplayAddressBookSelection('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}'); return false", Address.LoginAddressID, Address.AddressName, Address.BldgName, Address.Address1, Address.Address2, Address.City, Address.StateCode, Address.Zip, Address.Phone);
        ChooseButton.OnClientClick = on_select;
        ChooseButton.CausesValidation = false;

        string on_edit = String.Format("EditAddressBookSelection('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}'); return false", Address.LoginAddressID, Address.AddressName, Address.BldgName, Address.Address1, Address.Address2, Address.City, Address.StateCode, Address.Zip, Address.Phone);
        EditButton.OnClientClick = on_edit;
        EditButton.CausesValidation = false;

        string on_delete = "return confirm('Are you sure you want to delete this address?');";
        DeleteButton.OnClientClick = on_delete;
        DeleteButton.CausesValidation = false;
    }

    private void LoadData()
    {
        AddressNameLiteral.Text = Address.AddressName;
        BuildingNameLiteral.Text = Address.BldgName;
        Address1Literal.Text = Address.Address1;
        Address2Literal.Text = Address.Address2;
        CityLiteral.Text = Address.City;
        StateLiteral.Text = Address.StateCode;
        ZipLiteral.Text = Address.Zip;
        PhoneLiteral.Text = Address.Phone;
    }

    protected void DeleteButton_Clicked(object sender, EventArgs e)
    {
        CartDB db = new CartDB();
        if (db.LoginDeleteAddress(Address.LoginAddressID))
        {
            this.Controls.Clear();
            Response.Redirect("checkout.aspx");
        }
    }
}
