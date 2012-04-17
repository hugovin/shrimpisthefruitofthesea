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


public partial class Test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SiteConstants sitecons = new SiteConstants();
        Response.Write("<br/>Loginid:");
        //Response.Write(sitecons.ulogin());
        Response.Write("<br/>User:");
        Response.Write(Session[SiteConstants.UserFullName]);
        Response.Write("<br/>");
        Response.Write(Session[SiteConstants.UserValidLogin]);
        Response.Write("<br/>");
       
    }
    protected void logout_Click(object sender, EventArgs e)
    {
         Login login = new Login();
        login.UserLogout();
    }
}
