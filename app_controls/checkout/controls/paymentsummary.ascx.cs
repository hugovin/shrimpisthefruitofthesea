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

public partial class CheckoutControls_PaymentSummary : System.Web.UI.UserControl
{
    private CartPayment Payment = null;
    public CheckoutControls_PaymentSummary() { }
    public CheckoutControls_PaymentSummary(CartPayment c) { this.Payment = c; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Payment == null) throw new CartException("Could not load payment data");

        if (Payment.PaymentType == (int)PaymentType.CC)
        {
            CreditCardPanel.Visible = true;
            POPanel.Visible = false;

            CreditCardTypeLiteral.Text = String.Format("{0}", Payment.CCType);
            CreditCardNumberLiteral.Text = "***********"+String.Format("{0}", Payment.CCLastFourDigits);
        }
        else if (Payment.PaymentType == (int)PaymentType.PO)
        {
            CreditCardPanel.Visible = false;
            POPanel.Visible = true;
        }
    }
}
