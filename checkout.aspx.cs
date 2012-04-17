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

using System.Text;
using uc_Right;

public partial class CheckoutPage : System.Web.UI.Page
{
    private Cart CurrentCart;
    private string ErrorMessage;

    protected void Page_Load(object sender, EventArgs e)
    {
	 if (Request.IsSecureConnection == false)
        {
     		
            //string redirect = Request.Url.ToString().Replace("http://", "https://");
            //Response.Redirect(redirect);
            //return;
        }
      
	if (Request.QueryString["UserCreated"] != null)
        {
            Session["NewUserlogin"] = true;
        }
        else
        {
            Session["NewUserlogin"] = false;
        }

        //-- Left Menu Active
        if (Request.QueryString["am"] != null && Request.QueryString["am"] != "")
        {
            try { SiteConstants.LeftMenuActive = Convert.ToInt32(Request.QueryString["am"].ToString()); }
            catch { SiteConstants.LeftMenuActive = 4; }
        }
        else { SiteConstants.LeftMenuActive = 4; }
        //--
        SetChildPage("checkout.aspx");


        if (this.LoadCart() == false)
        {
            Response.Redirect(Constants.Pages.CART);
        }

        int step_to_move = 0;
        if (Request.QueryString["move"] != null && Int32.TryParse(Request.QueryString["move"], out step_to_move) && step_to_move >= 0 && step_to_move < CurrentCart.CheckoutStep && CurrentCart.CheckoutStep < 5)
        {
            CurrentCart.MoveToStep(step_to_move);
        }

        this.DisplaySteps();
        this.DisplaySummary();
        if(CurrentCart.CheckoutStep < 5) this.LoadControls();
        this.RouteCheckout();


    }

    private void LoadControls()
    {
        boxContact boxContact = (boxContact)(Page.LoadControl("boxContact.ascx"));
        PlaceHolder_boxContact.Controls.Add(boxContact);
        //---------------------------------
        //uc_FeatureProduct ucFeatureProduct = (uc_FeatureProduct)(Page.LoadControl("uc_FeatureProduct.ascx"));
        //ucFeatureProduct.intSubjId = 0;
        //PlaceHolder_uc_FeatureProduct.Controls.Add(ucFeatureProduct);
        ////-------------------------------
        //uc_Specials ucSpecials = (uc_Specials)(Page.LoadControl("uc_Specials.ascx"));
        //ucSpecials.intSubjId = 0;
        //PlaceHolder_uc_Specials.Controls.Add(ucSpecials);
        ////-------------------------------
        //uc_BestSellers ucBestSellers = (uc_BestSellers)(Page.LoadControl("uc_BestSellers.ascx"));
        //ucBestSellers.intSubjId = 0;
        //PlaceHolder_uc_BestSellers.Controls.Add(ucBestSellers);
    }

    private void SetChildPage(string name)
    {
        Session["CurrentChilPage"] = name;
    }

    private bool LoadCart()
    {
        try
        {
            CurrentCart = Cart.GetCartFromSession(Session);
            if (CurrentCart.HasItems == false) throw new CartException("Cart is empty");
            if (CurrentCart.Dirty) CurrentCart.Clean();
            return true;
        }
        catch (CartException ex)
        {
            ErrorMessage = ex.Message;
            return false;
        }
    }

    private void DisplaySteps()
    {
        string control_path = String.Format("{0}checkoutsteps.ascx", Constants.UserControls.CHECKOUT_CONTROLS_DIR);
        UserControl c = Helper.LoadUserControl(this, control_path, CurrentCart);
        if(c != null) StepsPlaceholder.Controls.Add(c);
    }

    private void DisplaySummary()
    {
        if (CurrentCart.CheckoutStep < 5)
        {
            string control_path = String.Format("{0}receiptcolumn.ascx", Constants.UserControls.CHECKOUT_CONTROLS_DIR);
            UserControl c = Helper.LoadUserControl(this, control_path, CurrentCart);
            SummaryPlaceholder.Controls.Add(c);
        }
    }

    private void RouteCheckout()
    {
        try
        {
            string control_path = "";

            switch (CurrentCart.CheckoutStep)
            {
                case 0:
                    // If not logged in, ask the user type

                    if (CartUsers.IsUserLoggedIn(Session) == false)
                    {
                        control_path = String.Format("{0}usertype.ascx", Constants.UserControls.CHECKOUT_STEPS_DIR);
                    }
                    else
                    {
                        CurrentCart.MoveToStep(1);
                        Response.Redirect(Constants.Pages.CHECKOUT);
                    }
                    break;

                case 1:
                    control_path = String.Format("{0}locations.ascx", Constants.UserControls.CHECKOUT_STEPS_DIR);
                    break;

                case 2:
                    control_path = String.Format("{0}shipping.ascx", Constants.UserControls.CHECKOUT_STEPS_DIR);
                    break;

                case 3:
                    control_path = String.Format("{0}payment.ascx", Constants.UserControls.CHECKOUT_STEPS_DIR);
                    break;

                case 4:
                    control_path = String.Format("{0}summary.ascx", Constants.UserControls.CHECKOUT_STEPS_DIR);
                    break;

                case 5:
                    control_path = String.Format("{0}thankyou.ascx", Constants.UserControls.CHECKOUT_STEPS_DIR);
                    break;

                case 6:
                    control_path = String.Format("{0}po.ascx", Constants.UserControls.CHECKOUT_STEPS_DIR);
                    break;

                default:
                    Response.Redirect(Constants.Pages.CART);
                    break;
            }

            UserControl c = Helper.LoadUserControl(this, control_path, CurrentCart);
            if (c != null) ControlPlaceholder.Controls.Add(c);
        }
        catch (CartException ex)
        {
            ErrorMessage = ex.Message;
            Response.Write("there was an errror...123");
        }
    }

}
