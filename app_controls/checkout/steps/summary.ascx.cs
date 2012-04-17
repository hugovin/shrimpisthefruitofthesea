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
using Org.BouncyCastle.Utilities.Collections;

public partial class CheckoutStep_Summary : System.Web.UI.UserControl
{
    private Cart CurrentCart = null;
    public CheckoutStep_Summary() { }
    public CheckoutStep_Summary(Cart c) { this.CurrentCart = c; }

    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.CurrentCart == null) throw new CartException("Could not load cart data");

	Session.Remove(Constants.SessionKeys.SUMMARY_EDIT);

        string locationsummarypath = String.Format("{0}/locationsummary.ascx", Constants.UserControls.CHECKOUT_CONTROLS_DIR);
        string paymentsummarypath = String.Format("{0}/paymentsummary.ascx", Constants.UserControls.CHECKOUT_CONTROLS_DIR);
        string shippingoptionsummarypath = String.Format("{0}/shippingoptionsummary.ascx", Constants.UserControls.CHECKOUT_CONTROLS_DIR);
        string itemsummarypath = String.Format("{0}/itemsummary.ascx", Constants.UserControls.CHECKOUT_CONTROLS_DIR);
        BillingSummaryPlaceholder.Controls.Add(Helper.LoadUserControl(Page, locationsummarypath, CurrentCart.BillingLocation));
        ShippingSummaryPlaceholder.Controls.Add(Helper.LoadUserControl(Page, locationsummarypath, CurrentCart.ShippingLocation));
        ShippingOptionSummaryPlaceholder.Controls.Add(Helper.LoadUserControl(Page, shippingoptionsummarypath, CurrentCart));
        PaymentSummaryPlaceholder.Controls.Add(Helper.LoadUserControl(Page, paymentsummarypath, CurrentCart.Payment));
        ItemsSummaryPlaceholder.Controls.Add(Helper.LoadUserControl(Page, itemsummarypath, CurrentCart));

        if (CurrentCart.Payment != null && CurrentCart.Payment.PaymentType == (int)PaymentType.PO)
        {
            //ContinueButton.Text = "Place Order and Send Us Your PO";
        }
    }

    public class LisTorchDescription
    {
        public string SKU;
        public string Description;
        public LisTorchDescription()
        {
            this.SKU = "";
            this.Description = "";
        }
        public LisTorchDescription(string sku, string desc)
        {
            this.SKU = sku;
            this.Description = desc;
        }

    }
    protected void ContinueButton_Clicked(object sender, EventArgs e)
    {
        string cont = "";// Request["emailTo"].ToString();

        sendEmail send = new sendEmail();

        bool success = true;
        int orderIdEmail = 0;
        // Submit Order        

        CartDB db = new CartDB();
        if (CurrentCart.Completed == false)
        {
            int order_id, payment_id;
            success = db.Order_Post_Cart(CurrentCart, CartUsers.GetLoginID(), SalesRepFlagCheckbox.Checked ? 1 : 0, out order_id, out payment_id,Convert.ToString(SumaryComents.Value));
            success = db.CartUpdatePaymentOrderIDs(CurrentCart.PaymentId, order_id, payment_id);
            if (Session["TorchInCart"] != null && bool.Parse(Session["TorchInCart"].ToString()) == true)
            {
                ArrayList listSkusDesc = new ArrayList();
                ArrayList listSkus = new ArrayList();
                int orderId = Convert.ToInt32(Session["orderIdForTorch"]);
                SiteProduct product = new SiteProduct();
                DataSet torchData = product.Get_TorchDescription(this.CurrentCart.CartId);
                foreach (DataTable table in torchData.Tables)
                {
                    foreach (DataRow Confdetail in table.Rows)
                    {
                        listSkus.Add(Confdetail["sku"].ToString());                        
                        listSkusDesc.Add(new LisTorchDescription(Confdetail["sku"].ToString(), Confdetail["description"].ToString())); 
                    }
                }
                HashSet hs = new HashSet();
                hs.AddAll(listSkus);
                listSkus.Clear();
                listSkus.AddRange(hs);

                foreach(string sku in listSkus)
                {
                   string finalDescription = "";
                   foreach (LisTorchDescription sku2 in listSkusDesc)
                   {
                       if(sku.Equals(sku2.SKU))
                       {
                           finalDescription += "|" + sku2.Description + "|";
                          
                       }
                   }
                   product.Add_Torch_Description(orderId, finalDescription, sku);
                }
                Session["TorchInCart"] = null;
                
                Session["orderIdForTorch"] = null;

            }

            if (CartUsers.IsUserLoggedIn(Session) && CurrentCart.BillingLocation != null)
            {
                int login_id = CartUsers.GetLoginID();
                db.LoginUpdBillingAddress(login_id, CurrentCart.BillingLocation.BusinessName, CurrentCart.BillingLocation.Address1, CurrentCart.BillingLocation.Address2, CurrentCart.BillingLocation.City, CurrentCart.BillingLocation.StateCode, CurrentCart.BillingLocation.PostalCode, CurrentCart.BillingLocation.CountryCode, CurrentCart.BillingLocation.Phone);
            }

        }
        CartDB de = new CartDB();
	
        cont = cont.Replace("\n\r", ""); //before making any substitution, check that there are no new lines in the code. Windows
        cont = cont.Replace("\n", ""); // Unix: note that this code will not change anyting in windows, due to the first line

        cont = cont.Replace("<img src=\"images/buttonEdit.jpg\" alt=\"Edit\" />", "");// Why will I want and edit button in the mail ?"<img src=\"http://www.edresources.com/images/buttonEdit.jpg\" alt=\"Edit\" />");
        cont = cont.Replace("type=\"checkbox\"", "type=\"hidden\"");
        cont = cont.Replace("textarea","div style='display:none;'");
        cont = cont.Replace("TEXTAREA", "div style='display:none;'");
        cont = cont.Replace("Comments", "");
        cont = cont.Replace("/ Special Instructions:", ""); 
        
	 cont = System.Text.RegularExpressions.Regex.Replace(cont, "<img class=\"first-child", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
     cont = System.Text.RegularExpressions.Regex.Replace(cont, "last-child\" alt=\"Edit\">", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
	 cont = System.Text.RegularExpressions.Regex.Replace(cont,"last-child\" alt=Edit src=\"images/buttonEdit.jpg\">","",System.Text.RegularExpressions.RegexOptions.IgnoreCase);
	 cont = System.Text.RegularExpressions.Regex.Replace(cont,"last-child\" alt=Edit src=\"images2/buttonEdit.jpg\">","",System.Text.RegularExpressions.RegexOptions.IgnoreCase);
	 cont = System.Text.RegularExpressions.Regex.Replace(cont,"last-child\" alt=\"Place Order\" src=\"images/buttonPlaceOrder.jpg\">","<br>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

       cont = cont.Replace("Special Instructions:", "<b>Special Instructions:</b>");
        if (!SalesRepFlagCheckbox.Checked) {
            cont = cont.Replace("Yes, a sales rep helped me with this order", " "); 
        }
        if (success)
        {
            DataSet data2 = new DataSet();
            data2 = de.Get_OrderId_By_Email(CartUsers.GetLoginID());
            foreach (DataTable table2 in data2.Tables)
            {
                foreach (DataRow row2 in table2.Rows)
                {
                    orderIdEmail = Convert.ToInt32(row2["OrderID"]);
                }
            }

            
            Session["orderpoid"] = orderIdEmail;

            if (CurrentCart.Payment.PaymentType == (int)PaymentType.CC)
            {
                send.toEmail(cont, CurrentCart.BillingLocation.Email, orderIdEmail);
                CurrentCart.MoveNextStep();

            }
            else
            {
                send.toEmail(cont, CurrentCart.BillingLocation.Email, orderIdEmail);
                CurrentCart.MoveToStep((int)Constants.CheckoutStep.PurchaseOrder);
            }

            Response.Redirect(Constants.Pages.CHECKOUT);
        }
    }

    private void SetSummaryEdit()
    {
        if (Session[Constants.SessionKeys.SUMMARY_EDIT] == null) Session.Add(Constants.SessionKeys.SUMMARY_EDIT, true);
        else Session[Constants.SessionKeys.SUMMARY_EDIT] = true;
    }

    protected void EditLocationsButton_Clicked(object sender, EventArgs e)
    {
        SetSummaryEdit();
        CurrentCart.MoveToStep((int)Constants.CheckoutStep.Locations);
        Response.Redirect(Constants.Pages.CHECKOUT);
    }

    protected void EditShippingOptionButton_Clicked(object sender, EventArgs e)
    {
        SetSummaryEdit();
        CurrentCart.MoveToStep((int)Constants.CheckoutStep.ShippingOptions);
        Response.Redirect(Constants.Pages.CHECKOUT);
    }

    protected void EditPaymentButton_Clicked(object sender, EventArgs e)
    {
        CurrentCart.MoveToStep((int)Constants.CheckoutStep.Payment);
        Response.Redirect(Constants.Pages.CHECKOUT);
    }

    protected void EditItemsButton_Clicked(object sender, EventArgs e)
    {
        SetSummaryEdit();
        Response.Redirect(Constants.Pages.CART);
    }


}
