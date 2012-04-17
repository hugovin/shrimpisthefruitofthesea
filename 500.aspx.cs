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
using System.Web.Mail;

public partial class _500 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    	SmtpMail.SmtpServer = "localhost";

        MailMessage mail = new MailMessage();

        mail.To = "web_errors@edresources.com";
        mail.From = "Support";
        mail.BodyFormat = MailFormat.Text;
        mail.Subject = "Error 500";
        mail.Body = "An HTTP error 500 occurred on " + DateTime.Now.ToString("MM/dd/yyyy hh:mm ss tt");

        SmtpMail.Send(mail);
        
        boxContact boxContact = (boxContact)(Page.LoadControl("boxContact.ascx"));
        PlaceHolder_boxContact.Controls.Add(boxContact);
        uc_NewsNinfo uc_news = (uc_NewsNinfo)(Page.LoadControl("uc_NewsNinfo.ascx"));
        PlaceHolder_uc_NewsNinfo.Controls.Add(uc_news);
    }
}
