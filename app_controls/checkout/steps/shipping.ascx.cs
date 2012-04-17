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

using System.Collections.Generic;

public partial class CheckoutStep_Shipping : System.Web.UI.UserControl
{
    private Cart CurrentCart = null;
    public CheckoutStep_Shipping() { }
    public CheckoutStep_Shipping(Cart c) { this.CurrentCart = c; }
    public bool checkthis = false;
    public bool Lowweight = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.CurrentCart == null) throw new CartException("Could not load cart data");

        if (!IsPostBack)
        {
            this.BuildShippingOptions();
        }
    }

    private void BuildShippingOptions()
    {
        double weight = CurrentCart.ItemsWeight;
        if(weight <= 1)
        {
            Lowweight = true;
        }
        WeightLiteral.Text = String.Format("{0} lbs", CurrentCart.ItemsWeight);
        foreach (ShippingOption option in ShippingOption.GetActiveShippingOptions())
        {
            double cost = 0;
            try
            {
                if (UPS.CalculateShippingCost(CurrentCart.ShippingLocation, option.Code, weight, Constants.ShippingTypes.MIN_SHIPPING, out cost))
                {
                    AddShippingOption(option, cost);
                }
            }
            catch (UPSException ex)
            {
            }
        }

        ShippingOptionRadio.SelectedIndex = ShippingOptionRadio.Items.IndexOf(ShippingOptionRadio.Items.FindByValue(CurrentCart.ShippingOption.ToString()));

        if (ShippingOptionRadio.Items.Count <= 0)
        {
            NoOptionPanel.Visible = true;
            ShippingOptionRadio.Visible = false;
        }

    }

    private void AddShippingOption(ShippingOption option, double cost)
    {
        ShippingOptionRadio.Items.Add(new ListItem(String.Format("{0} - {1}", option.Label, Helper.Dollar(cost)), option.ShippingOptionID.ToString()));
    }


    protected void ContinueButton_Clicked(object sender, EventArgs e)
    {
        bool success = false;
        string check= Convert.ToString(ShippingOptionRadio.SelectedValue);
        int shipping_option;
        if (check != "")
        {
            if (Int32.TryParse(ShippingOptionRadio.SelectedValue, out shipping_option))
            {
                success = true;
            }

            if (success)
            {
                CartDB db = new CartDB();
                if (db.CartUpdateShippingOption(CurrentCart.CartId, shipping_option))
                {
                    CurrentCart.MoveNextStep();
                    Response.Redirect(Constants.Pages.CHECKOUT);
                }
            }
        }
        else {
            checkthis = true;
        }
    }



        /*
        foreach(ShippingOption option in cms.GetShippingOptions())
        {
            ShippingOptionRadio.Items.Add(new ListItem(option.Name, String.Format("{0} - ${1}", option.Name, option.Cost)));
        }
        */

        /*string shipping_option = "Standard";
        decimal shipping_cost = (decimal)10.00;

        string label = String.Format("{0} - ${1}", shipping_option, shipping_cost);
        
        ShippingOptionRadio.Items.Add(new ListItem(label, "1"));
        ShippingOptionRadio.SelectedIndex = 0;*/

        /*
        double weight = CurrentCart.ItemsWeight;

        List<double> costs = new List<double>();
        
        
        double cost = 0;
        if (UPS.CalculateShippingCost(CurrentCart.ShippingLocation, Constants.ShippingTypes.GROUND, weight, 6.00, out cost))
        {
            costs.Add(cost);
            string label = String.Format("{0} - ${1}", "Standard Ground", cost);
            ShippingOptionRadio.Items.Add(new ListItem(label, "0"));
            ShippingOptionRadio.SelectedIndex = 0;
        }

        ViewState["costs"] = costs;
    }

    protected void ContinueButton_Clicked(object sender, EventArgs e)
    {
        int index;

        List<double> costs;
        if (ViewState["costs"] != null)
        {
            costs = (List<double>)ViewState["costs"];
        }
        else
        {
            costs = new List<double>();
        }

        if (Int32.TryParse(ShippingOptionRadio.SelectedValue, out index) && costs.Count > index)
        {
            CartDB db = new CartDB();
            if (db.CartUpdateShipping(CurrentCart.CartId, index, (decimal)costs[index]))
            {
			if (Session[Constants.SessionKeys.SUMMARY_EDIT] != null && (bool)Session[Constants.SessionKeys.SUMMARY_EDIT] == true)
			{
			    Session.Remove(Constants.SessionKeys.SUMMARY_EDIT);
			    db.CartUpdateDirty(CurrentCart.CartId, true);
			    CurrentCart.MoveToStep(Constants.CheckoutStep.Summary);
			    Response.Redirect(Constants.Pages.CHECKOUT);
			    return;
			}


                CurrentCart.MoveNextStep();
                Response.Redirect(Constants.Pages.CHECKOUT);
            }
        }
    }
         * */
}
