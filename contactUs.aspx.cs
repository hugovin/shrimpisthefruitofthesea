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

public partial class contactUs : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //--
        string CurrentChilPage = "";
        if (Session["CurrentChilPage"] != null)
            CurrentChilPage = Session["CurrentChilPage"].ToString();
        else
            CurrentChilPage = "contactUs.aspx";
        //--
        Site site = new Site();
        string userToken = site.getUserGUID();
        contactLiteral.Text = "<iFrame id=\"contactFrame\" name=\"contactFrame\" src=\"http://" + Global.globalMySiteUrl + "/general/contact_us.aspx?Token=" + userToken + "\" frameborder=\"0\" width=\"100%\" height=\"450\"></iFrame>";
        Main_MasterPage main = (Main_MasterPage)Page.Master;
        main._site_breadLink += "<li><a href=#>" + "About " + SiteConstants.SiteName + "</a></li><li class=\"last\"><a href=#><strong>" + "Contact Us" + "</strong></a></li>";
        main.pageTitleBar = "Contact Us - " + main.pageTitleBar;
        //this.URLRedirect();
    }
    private void URLRedirect()
    {
        if (Request["recurl"] == null)
        { //If this is null means that the redirect haven't started
            if (Request["asm"] != null && Request["am"] != null)
            {
                Response.Redirect("Contact_Us." + Request["am"].ToString() + "html" + Request["asm"].ToString());
            }
            else {
                Response.Redirect("Contact_Us.html");
            }
            
        }
    }
}
