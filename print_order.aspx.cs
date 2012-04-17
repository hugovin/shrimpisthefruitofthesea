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

public partial class PrintOrderPage : System.Web.UI.Page
{
    Cart CurrentCart;
    public int orderid = 0;
    public int POrder = 0;


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            this.CurrentCart = Cart.GetCartFromSession(Session);
            if (this.CurrentCart == null) throw new CartException("Cannot load cart data from session");
            if (this.CurrentCart.BillingLocation == null) throw new CartException("Incomplete Cart (Billing)");
            if (this.CurrentCart.ShippingLocation == null) throw new CartException("Incomplete Cart (Shipping)");
            if (this.CurrentCart.Payment == null) throw new CartException("Incomplete Cart (Payment)");

            if (this.CurrentCart.Dirty) this.CurrentCart.Clean();



            string locationsummarypath = String.Format("{0}/locationsummary.ascx", Constants.UserControls.CHECKOUT_CONTROLS_DIR);
            string paymentsummarypath = String.Format("{0}/paymentsummary.ascx", Constants.UserControls.CHECKOUT_CONTROLS_DIR);
            string shippingoptionsummarypath = String.Format("{0}/shippingoptionsummary.ascx", Constants.UserControls.CHECKOUT_CONTROLS_DIR);
            string itemsummarypath = String.Format("{0}/itemsummary.ascx", Constants.UserControls.CHECKOUT_CONTROLS_DIR);

            BillingSummaryPlaceholder.Controls.Add(Helper.LoadUserControl(Page, locationsummarypath, CurrentCart.BillingLocation));
            ShippingSummaryPlaceholder.Controls.Add(Helper.LoadUserControl(Page, locationsummarypath, CurrentCart.ShippingLocation));
            ShippingOptionSummaryPlaceholder.Controls.Add(Helper.LoadUserControl(Page, shippingoptionsummarypath, CurrentCart));
            orderid = Convert.ToInt32(Request.QueryString["id"]);
            POrder = Convert.ToInt32(Request.QueryString["po"]);
            //PaymentSummaryPlaceholder.Controls.Add(Helper.LoadUserControl(Page, paymentsummarypath, CurrentCart.Payment));
            ItemsSummaryPlaceholder.Controls.Add(Helper.LoadUserControl(Page, itemsummarypath, CurrentCart));



        }
        catch (CartException ex)
        {
            Page.Controls.Add(new LiteralControl("There was an error loading your cart"));
        }
    }
}
