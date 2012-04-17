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

public partial class CheckoutControls_CheckoutSteps : System.Web.UI.UserControl
{
    private Cart CurrentCart = null;
    public CheckoutControls_CheckoutSteps() { }
    public CheckoutControls_CheckoutSteps(Cart c) { this.CurrentCart = c; }

    string step_on = @"" +
    "<div class=\"controlCheckOut\">" + 
    "    <div class=\"numControl\"><p>{0}</p></div>" + 
    "    <div class=\"lineControl{1}\"></div>" + 
    "    <div class=\"stepControl\"><p>{2}</p></div>" + 
    "</div>";

    string step_off = @"" +
    "<div class=\"controlCheckOut\">" +
    "    <div class=\"numControlOff\"><p>{0}</p></div>" +
    "    <div class=\"lineControl{1}\"></div>" +
    "    <div class=\"stepControlOff\"><p>{2}</p></div>" +
    "</div>";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.CurrentCart == null) throw new CartException("Could not load cart data");

        for (int i = 1; i < 6; i++)
        {
            bool islink = i <= this.CurrentCart.CheckoutStep && this.CurrentCart.CheckoutStep < 5;
            bool isactive = i == this.CurrentCart.CheckoutStep;
            StepsPlaceholder.Controls.Add(new LiteralControl(BuildStep(i, islink, isactive))); //BuildLabel(i, islink, isactive)));
            if (i != 5)
            {
                StepsPlaceholder.Controls.Add(new LiteralControl("<div class=\"vlineCheck\"><img src=\"" + Global.globalSiteImagesPath + "/dividerLineCheck.jpg\"/></div>"));
            }
        }
    }

    private string BuildStep(int step_number, bool link, bool on)
    {
        string line_control = step_number > 1 ? step_number.ToString() : "";
        string step_name = Helper.IsString(Constants.Labels.CHECKOUT_STEPS[step_number], "");
        string label = link && !on ? String.Format("<a href=\"checkout.aspx?move={1}\">{0}</a>", step_name, step_number) : step_name;
        return String.Format(on ? step_on : step_off, step_number, line_control, label);
    }

    private string BuildLabel(int number, bool link, bool active)
    {
        if (link && !active)
        {
            return String.Format("<div class='step link'><a href='{0}?move={1}'>{1} - {2}</a></div>", Constants.Pages.CHECKOUT, number, Constants.Labels.CHECKOUT_STEPS[number]);
        }
        else if (active)
        {
            return String.Format("<div class='step'><strong>{0} - {1}</strong></div>", number, Constants.Labels.CHECKOUT_STEPS[number]);
        }
        else
        {
            return String.Format("<div class='step'>{0} - {1}</div>", number, Constants.Labels.CHECKOUT_STEPS[number]);
        }
    }
}
