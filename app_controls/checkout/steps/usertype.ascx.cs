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

public partial class CheckoutStep_Usertype : System.Web.UI.UserControl
{
    private Cart CurrentCart = null;
    public CheckoutStep_Usertype() { }
    public CheckoutStep_Usertype(Cart c) { this.CurrentCart = c; }
    public string message = "";
    public string style = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        NewUserButton.Text="<img src='" + Global.globalSiteImagesPath + "/btnNewUser.jpg' alt='New' />";
        if (this.CurrentCart == null) throw new CartException("Could not load cart data");

        if (Session["NewUserlogin"] != null)
        {
            if(Convert.ToBoolean(Session["NewUserlogin"]) == true)
            {
                message = "Thanks,<br>Please Login";
                style = "questionBoxYou3";
            }
            else
            {
                message = "YOU ARE?";
                style = "questionBoxYou";
            }
        }
    }

    protected void NewUserButton_Clicked(object sender, EventArgs e)
    {
        Response.Redirect("newAccount.aspx?Checkout=1");
    }

    protected void ExistingUserButton_Clicked(object sender, EventArgs e)
    {
        Response.Redirect(Constants.Pages.LOGIN);
    }
}
