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

public partial class CheckoutStep_Payment : System.Web.UI.UserControl
{
    private Cart CurrentCart = null;
    public CheckoutStep_Payment() { }
    public CheckoutStep_Payment(Cart c) { this.CurrentCart = c; }
    public bool error = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.CurrentCart == null) throw new CartException("Could not load cart data");
        CreditCardButton.Text = "<img src='" + Global.globalSiteImagesPath + "/bn_creditCard.jpg' alt='Credit Card' />";
        PurchaseOrderButton.Text = "<img src='" + Global.globalSiteImagesPath + "/bn_purchaseOrder.jpg' alt='Credit Card' />";
        if (Request["pagenew"] == null)
        {

                LoadDropdowns();
        }

        CreditCardNumberValidServerValidator.ServerValidate += new ServerValidateEventHandler(CreditCardValidValidator_ServerValidate);
        CreditCardNumberRequiredServerValidator.ServerValidate += new ServerValidateEventHandler(CreditCardNumberRequiredServerValidator_ServerValidate);
        CreditCardExpirationValidServerValidator.ServerValidate += new ServerValidateEventHandler(CreditCardExpirationValidServerValidator_ServerValidate);

        if (CurrentCart.Payment != null && CurrentCart.Payment.PaymentType == (int)PaymentType.CC)
        {
            ShowCreditCardDiv();
        }

	string contentid = Helper.IsString(Session["ContentId"], "");
	if(contentid == "3" || contentid == "4")
		PurchaseOrderButton.Visible = false;
        string script = String.Format("<script type=\"text/javascript\">{0}</script>", Helper.GenerateScriptToAccessControls(Page.Controls));
        Page.RegisterStartupScript("ControlRegistration", script);
    }

    void CreditCardExpirationValidServerValidator_ServerValidate(object source, ServerValidateEventArgs args)
    {
        int month, year;
        if(Int32.TryParse(MonthDropdown.SelectedValue, out month) && Int32.TryParse(YearDropdown.SelectedValue, out year))
        {
            DateTime expiration;
            if (DateTime.TryParse(String.Format("{0}/{1}/01", year, month), out expiration))
            {
                if (expiration.Year == DateTime.Today.Year)
                {
                    if ((expiration.Month >= DateTime.Today.Month))
                    {
                        args.IsValid = true;
                        return;
                    }
                }
                else {
                    if (expiration.Year >= DateTime.Today.Year)
                    {
                        args.IsValid = true;
                        return;
                    }

                }
            }
        }

        args.IsValid = false;
    }

    void CreditCardNumberRequiredServerValidator_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = args.Value.Length > 0;
    }

    protected void CreditCardValidValidator_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = Helper.StringExists(CreditCardNumberTextbox.Text) && Helper.IsCreditCardValid(CreditCardNumberTextbox.Text);
    }

    private void LoadDropdowns()
    {
        for (int i = 1; i <= 12; i++)
        {
            string month = DateTime.Parse(String.Format("2009/{0}/01", i)).ToString("MMM");

            MonthDropdown.Items.Add(new ListItem(String.Format("{0} ({1})", month, i), i.ToString()));
        }

        int stop = DateTime.Today.Year + 15;
        for (int i = DateTime.Today.Year; i < stop; i++)
        {
            YearDropdown.Items.Add(new ListItem(i.ToString()));
        }
    }

    protected void PurchaseOrderButton_Clicked(object sender, EventArgs e)
    {
        CartDB db = new CartDB();

        bool success = false;
        if (CurrentCart.PaymentId > 0)
        {
            success = db.CartUpdatePayment(CurrentCart.PaymentId, CurrentCart.CartId, PaymentType.PO, "", "", "", "", "", "", "", null);
        }
        else
        {
            success = db.CartAddPayment(CurrentCart.CartId, PaymentType.PO, "", "", "", "", "", "", "", null);
        }

        if (success)
        {
            CurrentCart.MoveNextStep();
            Response.Redirect(Constants.Pages.CHECKOUT);
        }
    }

    private void ShowCreditCardDiv()
    {
        Page.RegisterStartupScript("ShowCreditCardDiv", "<script type='text/javascript'>addOnLoad(function(){ toggle_cc(); });</script>");
    }

    protected void CreditCardButton_Clicked(object sender, EventArgs e)
    {
	ShowCreditCardDiv();

    if (Page.IsValid)
    {
        string cc = CreditCardNumberTextbox.Text;
        string cvv = CVVTextbox.Text;
        string exp = String.Format("{0}/{1}", MonthDropdown.SelectedValue, YearDropdown.SelectedValue);
        string last_four = cc.Substring(cc.Length - 4);
        string cctype = CreditCardTypeDropdown.SelectedValue;

        CartDB db = new CartDB();
        bool success = false;

        if (CurrentCart.PaymentId > 0)
        {
            success = db.CartUpdatePayment(CurrentCart.PaymentId, CurrentCart.CartId, PaymentType.CC, cctype, last_four, Encrypt(cc), Encrypt(exp), Encrypt(cvv), "", "", null);
        }
        else
        {
            success = db.CartAddPayment(CurrentCart.CartId, PaymentType.CC, cctype, last_four, Encrypt(cc), Encrypt(exp), Encrypt(cvv), "", "", null);
        }

        if (success)
        {
            CurrentCart.MoveNextStep();
            Response.Redirect(Constants.Pages.CHECKOUT);
        }


    }
    else
    {
        error = true;
    }
    }

    private string Encrypt(string what)
    {
        string pubkeypath = System.IO.Path.GetFullPath(ConfigurationManager.AppSettings[Constants.ConfigKeys.PGP_PUB_KEY]);
        return ER.Common.Encryption.PGPMessage.EncryptMessage(what, pubkeypath, true, true);
    }


    protected void CreditCardButton_Click(object sender, EventArgs e)
    {
        CreditCardPanel.Visible = true;
    }
}
