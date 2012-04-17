using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using System.Net.Mail;

/// <summary>
/// Summary description for sendEmail
/// </summary>
public class sendEmail
{
    private string SiteContTitle;
    private string SiteContAddress;
    private string SiteContEmailCus;
    private string SiteContEmailSal;
    private string SiteConPhone;
    private string SiteCopy;
    private string SiteURL;
    private string SiteTagLine;


	public sendEmail()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    public void toEmail(string cont,string mail,int orderid)
    {
    	
        string ProofLabel = "<br>Some Products Require Proof to Qualify for Academic Pricing.<br>";
        StringBuilder sbEmail = new StringBuilder();
        LoadSiteContact();
        sbEmail.AppendLine("<head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" /><title>ER</title>");
        sbEmail.AppendLine("<link rel=\"stylesheet\" type=\"text/css\" href=\""+ SiteConstants.SiteUrl +"/" + Global.globalSiteStylePath + "/global.css\"/>");
        sbEmail.AppendLine("<link rel=\"stylesheet\" type=\"text/css\" href=\""+ SiteConstants.SiteUrl +"/css/mailStyle.css\"/></head>");
        sbEmail.AppendLine("<body style=\"font-family:Arial, Helvetica, sans-serif;\">");
        sbEmail.AppendLine("<table width=\"800px\" bgcolor=\"#22517f\" cellpadding=\"0\" cellspacing=\"0\" >");
        sbEmail.AppendLine("<tr><td valign=\"top\" height=\"20\" colspan=\"3\"></td></tr>");
        sbEmail.AppendLine("<tr><td width=\"25\"></td><td valign=\"top\"><table cellpadding=\"0\" cellspacing=\"0\" width=\"743\">");
        sbEmail.AppendLine("<tr valign=\"top\" height=\"10px\"><td valign=\"top\">");
        sbEmail.AppendLine("<img src=\""+ SiteConstants.SiteUrl +"/" + Global.globalSiteImagesPath + "/mail1_03.jpg\" width=\"743\" height=\"10\" style=\"margin:0 0 -3px 0;\"/>");
        sbEmail.AppendLine("</td></tr><tr valign=\"top\"><td><table width=\"743\" bgcolor=\"#FFFFFF\"><tr><td width=\"20\"></td>");
        sbEmail.AppendLine("<td width=\"236\"><img src=\""+ SiteConstants.SiteUrl +"/" + Global.globalSiteImagesPath + "/logoER.png\" width=\"236\" height=\"59\"/></td>");
        sbEmail.AppendLine("<td width=\"467\"></td></tr><tr><td ></td><td colspan=\"2\"><table width=\"690\" cellpadding=\"0\" cellspacing=\"0\">");
        sbEmail.AppendLine("<tr><td width=\"25\">&nbsp;</td><td ><table><tr><td><p>Thank you! <br /><br /> We will process your order as soon as possible. <br /><br />We will be in contact with you shortly if we need any additional information.<br />" + ProofLabel + "<br /> We appreciate your business!</p>");
        sbEmail.AppendLine(cleanButton(cont));
        sbEmail.AppendLine("</td></tr></table><table width=\"656\" style=\"font-size:12px\"><tr><td width=\"648\">");
        sbEmail.AppendLine("<img src=\""+ SiteConstants.SiteUrl +"/" + Global.globalSiteImagesPath + "/txtEducationalExperts.jpg\" width=\"127\" height=\"15\" style=\"margin:15px 0 -5px\"/>");
        sbEmail.AppendLine("<em>" + SiteTagLine + "</em></td>");
        sbEmail.AppendLine("</tr></table><table width=\"656\"><tr><td width=\"330\" height=\"121\" style=\"font-size:12px;\">");
        sbEmail.AppendLine("<img src=\""+ SiteConstants.SiteUrl +"/" + Global.globalSiteImagesPath + "/usaFlag.png\" width=\"91\" height=\"36\" style=\"margin:0;\"/>");

        sbEmail.AppendLine("<h3 style=\"margin:0;\">" + SiteContTitle + "</h3><p>" + SiteContAddress + "</p>");
        sbEmail.AppendLine("</td><td width=\"338\" style=\"font-size:12px;\"><br /><h3 style=\"margin:0;\">Email</h3>");
        sbEmail.AppendLine("<p><span>Customer Service:</span> <a href=\"mailto:" + SiteContEmailCus + "\">" + SiteContEmailCus + "</a><br />");
        sbEmail.AppendLine("<span>Sales:</span> <a href=\"mailto:" + SiteContEmailSal + "\">" + SiteContEmailSal + "</a></p></td>");
        sbEmail.AppendLine("</tr></table><table width=\"654\"><tr><td>");
        sbEmail.AppendLine("<p style=\"font-size:12px\">" + SiteCopy + "<br />" + SiteConPhone + "&nbsp; Toll Free</p></td></tr></table>");

        sbEmail.AppendLine("</td></tr></table></td></tr></table></td></tr><tr height=\"10\" valign=\"top\"><td valign=\"top\">");
        sbEmail.AppendLine("<img src=\""+ SiteConstants.SiteUrl +"/" + Global.globalSiteImagesPath + "/mail1_06.jpg\" width=\"743\" height=\"10\" style=\"margin:0;\"/></td>");
        sbEmail.AppendLine("</tr></table></td><td width=\"25\"></td></tr><tr><td valign=\"top\" height=\"20\" colspan=\"3\"></td>");
        sbEmail.AppendLine("</tr></table></div></body></html>");
        sbEmail.AppendLine("</tr></table></div></body></html>");
        try
        {
        	
            MailMessage message = new MailMessage(SiteContEmailCus, mail, "" + Global.globalSiteNameAbr + " Web Order Num: " + orderid, sbEmail.ToString());
            SmtpClient emailClient = new SmtpClient("localhost");
            message.From = new MailAddress(SiteContEmailCus, "" + Global.globalSiteNameAbr + " Customer Service");
	    	message.Bcc.Add(SiteContEmailCus);
            message.IsBodyHtml = true;
            emailClient.Send(message);
        }
        catch (Exception ex)
        {
           
        }			                              
                                	
                                    
    }
    public string cleanButton(string data)
    {
        String data2 = data;
        int first = 1;
		int second = 1;
		try{
		    first = data2.IndexOf("<img ", first);
		    second = data2.IndexOf(">", first);
		    //Response.Write("<br>"); Response.Write(first.ToString()); Response.Write("<br>"); Response.Write(second.ToString());
		    data2 = data2.Remove(first, (second - first) + 1);
		    first = second;
		    first = data2.IndexOf("<img ", first);
		    second = data2.IndexOf(">", first);
		    //Response.Write("<br>"); Response.Write(first.ToString()); Response.Write("<br>"); Response.Write(second.ToString());
		    data2 = data2.Remove(first, (second - first) + 1);
		    first = second;
		    first = data2.IndexOf("<img ", first);
		    second = data2.IndexOf(">", first);
		    //Response.Write("<br>"); Response.Write(first.ToString()); Response.Write("<br>"); Response.Write(second.ToString());
		    data2 = data2.Remove(first, (second - first)+1);
		    first = second;
		    first = data2.IndexOf("<img ", first);
		    second = data2.IndexOf(">", first);
		    //Response.Write("<br>"); Response.Write(first.ToString()); Response.Write("<br>"); Response.Write(second.ToString());
		    data2 = data2.Remove(first, (second - first)+1);
		    first = second;
		    first = data2.IndexOf("<img ", first);
		    second = data2.IndexOf(">", first);
		    //Response.Write("<br>"); Response.Write(first.ToString()); Response.Write("<br>"); Response.Write(second.ToString());
		    data2 = data2.Remove(first, (second - first)+1);
	data2 = data2.Replace("<div class=\"controlProfile\">", "<div class=\"controlProfile\" style=\"visibility:hidden\">");
    //data2 = data2.Replace("<IMG src=\"" + Global.globalSiteImagesPath + "/buttonEdit.jpg\" alt=\"Edit\">", "<br>");
    //data2 = data2.Replace("alt=\"Edit\"", "alt=\"Edit\" style=\"display:none;\"");
    //data2 = data2.Replace("alt=Edit", "alt=\"Edit\" style=\"display:none;\"");
    //data2 = data2.Replace("<img", "<input type=\"hidden\"");
        //data2 = data2.Replace("<IMG class=\"first-child last-child\" alt=\"Place Order\" src=\"" + Global.globalSiteImagesPath + "/buttonPlaceOrder.jpg\">","<br>");
	data2 = data2.Replace("Yes, a sales rep helped me with this order","");
        data2 = data2.Replace("checkbox", "hidden");
        }
         catch (Exception ex)
        {
           
        }		
        return data2;
    }

    public void erroOnpage(string error)
    {
        try
        {            	    
	    MailMessage message = new MailMessage("web_errors@edresources.com","web_errors@edresources.com", "Error On Page", error + "<br>" + HttpContext.Current.Request.UrlReferrer +  "<br>" + HttpContext.Current.Request.Url.AbsoluteUri);
	    message.To.Add("luisangeld@lat10.com");
            SmtpClient emailClient = new SmtpClient("localhost");	
	    message.IsBodyHtml = true;
            emailClient.Send(message);
        }
        catch (Exception ex)
        {

        }
    }
    protected void LoadSiteContact()
    {
        Footer footer = new Footer();
        DataSet dsContact = new DataSet();
        DataSet dsSite = new DataSet();
        dsContact = footer.GetAllSiteContact();
        dsSite = footer.GetSiteInformation();
        foreach (DataTable table in dsContact.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                SiteContTitle       = row["SiteContTitle"].ToString();
                SiteContAddress     = row["SiteContAddress"].ToString();
                SiteContEmailCus    = row["SiteContEmailCus"].ToString();
                SiteContEmailSal    = row["SiteContEmailSal"].ToString();

            }
        }
         foreach (DataTable table in dsSite.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                SiteConPhone = row["sitephone"].ToString();
                SiteCopy = row["sitecopy"].ToString();
                SiteURL = row["siteurl"].ToString();
                SiteTagLine = row["sitetagline"].ToString();
            }
        }
    }//end LoadSiteContact
} //class end