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

public partial class CheckoutControl_ReceiptColumn : System.Web.UI.UserControl
{
    private Cart CurrentCart = null;
    public CheckoutControl_ReceiptColumn() { }
    public CheckoutControl_ReceiptColumn(Cart c) { this.CurrentCart = c; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.CurrentCart == null) throw new CartException("Could not load cart data");

        this.LoadData();

        switch (this.CurrentCart.CheckoutStep)
        {
            case 0:
                TaxLiteral.Text = "--";
                SubTotalLiteral.Text = "--";
                ShippingLiteral.Text = "--";
                OrderTotalLiteral.Text = "--";
                break;
            case 1:
                TaxLiteral.Text = "--";
                SubTotalLiteral.Text = "--";
                ShippingLiteral.Text = "--";
                OrderTotalLiteral.Text = "--";
                break;
            case 2:
                ShippingLiteral.Text = "--";
                OrderTotalLiteral.Text = "--";
                break;       
        }
    }

    private void LoadData()
    {
        // Item Total + Estimated Tax + Shipping + Handling = Order Total
        // Where, Estimated Tax = Item Total * Tax Rate
        // OR:
        // Item Total * (1 + Tax Rate) + Shipping + Handling = Order Total
        ItemTotalLiteral.Text = Helper.Dollar(CurrentCart.SubTotal);
        ShippingLiteral.Text = Helper.Dollar(CurrentCart.Shipping + CurrentCart.Handling);
        //HandlingLiteral.Text = Helper.Dollar(CurrentCart.Handling);
        TaxLiteral.Text = Helper.Dollar(CurrentCart.Tax);
        SubTotalLiteral.Text = Helper.Dollar(CurrentCart.SubTotal + CurrentCart.Tax);
        OrderTotalLiteral.Text = Helper.Dollar(CurrentCart.Total);
    }
}
