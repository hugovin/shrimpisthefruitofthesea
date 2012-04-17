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

public partial class CheckoutStep_ThankYou : System.Web.UI.UserControl
{
    private Cart CurrentCart = null;
    public CheckoutStep_ThankYou() { }
    public CheckoutStep_ThankYou(Cart c) { this.CurrentCart = c; }
    public bool confirmation = false;
    public string orderid = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.CurrentCart == null) throw new CartException("Could not load cart data");

        if (this.CurrentCart.Completed == false)
        {
            CartDB db = new CartDB();
            db.CartUpdateCompleted(CurrentCart.CartId, true);
	    db.CartDeleteAllByUserID(CurrentCart.UserId);
        confirmation = true;
        orderid = Session["orderpoid"].ToString();
        }

        if (!CartUsers.IsUserLoggedIn(Session))
        {
            CreateAccountPanel.Visible = true;
        }
        if (Session["SAPER"] != null)
        {
            if (Convert.ToBoolean(Session["SAPER"]) != false)
            {
                StudentVerificationPanel.Visible = true;
            }
        }
	Session["SAPER"] = null;

    }

    protected void ContinueButton_Clicked(object sender, EventArgs e)
    {
        Response.Redirect("home.aspx");
    }
}
