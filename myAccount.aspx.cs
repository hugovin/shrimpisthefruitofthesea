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

public partial class myAccount : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ////this.URLRedirect();
        if (Session[SiteConstants.UserFullName] == "" || Session[SiteConstants.UserFullName] == null)
        {
            Response.Redirect("home.aspx");
        }
        Site s = new Site();
        iFrameAccount.Text = "<iframe src=\"https://" + Global.globalMySiteUrl + "/user/account.aspx?Frame=1&Token="+s.getUserGUID()+"\" frameborder=\"0\" id=\"iframeMyAccount\" width=\"100%\" height=\"845\"></iframe>";
        Main_MasterPage main = (Main_MasterPage)Page.Master;
        main.pageTitleBar = "My Account - " + main.pageTitleBar;
        main._site_breadLink += "<li><a href=\"#\" onClick=\"location.href=location.href\"><strong>" + "My Account" + "</strong></a></li>";
        Session["CurrentChilPage"] = "myAccount.aspx";
    }

    private void URLRedirect()
    {
        if (Request["recurl"] == null)
        { //If this is null means that the redirect haven't started
            Response.Redirect("My_Account.html");
        }
    }
}
