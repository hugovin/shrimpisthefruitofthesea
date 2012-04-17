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

public partial class CheckoutControls_ShippingOptionSummary : System.Web.UI.UserControl
{
    private Cart CurrentCart = null;
    public CheckoutControls_ShippingOptionSummary() { }
    public CheckoutControls_ShippingOptionSummary(Cart c) { this.CurrentCart = c; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.CurrentCart == null) throw new CartException("Could not load shipment data");

        string label = "";
        foreach (ShippingOption option in ShippingOption.GetActiveShippingOptions())
        {
            if(option.Code == Constants.ShippingTypes.SHIPPING[CurrentCart.ShippingOption])
            {
                label = option.Label;
            }
        }

        ShippingOptionNameLiteral.Text = label;
        ShippingOptionPriceLiteral.Text = Helper.Dollar(CurrentCart.Shipping);
    }
}
