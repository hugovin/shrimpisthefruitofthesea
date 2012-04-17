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
using System.Net.Mail;

public partial class shareMailer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["mailList"] != null && Request["from"] != null && Request["urls"] != null)
        {
            char[] delimiterChars = {' ', ',', '\t', '\n'};
            string lista = Request["mailList"].ToString();
            string comeFrom = Request["from"].ToString();
            string dirmessage = Request["urls"];
            string[] listOfEmails = lista.Split(delimiterChars);
            try {
                foreach (string email in listOfEmails)
                {
                    if (email != "")
                    {
                        MailMessage message = new MailMessage("custserv@edresources.com", email, "EdResources!", "EdResources! " + dirmessage + ". Sent by: " + comeFrom);		
                        SmtpClient emailClient = new SmtpClient("localhost");
                        emailClient.Send(message);
                    }
                }
                Response.Write("0|Email sent.");
            }catch(Exception exp){
                Response.Write("2|"+exp.Message+".");
            }
            
        }
        else {
            Response.Write("1|Param missing.");
        }
        
        
    }
}
