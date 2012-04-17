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

public partial class ForgotPassword : System.Web.UI.Page
{
    private string CurrentChilPage = "";
    private string ContentId = "";
    private string SiteId = "";
    public string newOne = "";
    private void GetVars()
    {
        //---
        if (Session["SiteId"] != null)
            SiteId = Session["SiteId"].ToString();

        ContentId = Request["ci"];
        if (ContentId != null)
            Session["ContentId"] = ContentId;
        else
            ContentId = Session["ContentId"].ToString();
        //--
        if (Session["CurrentChilPage"] != null)
            CurrentChilPage = Session["CurrentChilPage"].ToString();
        else
            CurrentChilPage = "requestaquote.aspx";
        //--
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        Main_MasterPage main = (Main_MasterPage)Page.Master;
        main.pageTitleBar = "New Account - " + main.pageTitleBar;
        main._site_breadLink += "<li><a href=#><strong>" + "My Account" + "</strong></a></li>";
        Session["CurrentChilPage"] = "ForgotPassword.aspx";
        if (Request["recurl"] == null)
        {
            //Response.Redirect("New_Account");
        }
    }
}
