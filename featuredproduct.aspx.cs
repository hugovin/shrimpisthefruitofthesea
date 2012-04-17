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

public partial class featuredproduct : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Main_MasterPage main = (Main_MasterPage)Page.Master;
        main.pageTitleBar = "Featured Products - " + main.pageTitleBar;
        main._site_breadLink += "<li class=\"last\"><a href=# onClick=\"return false;\"><strong>Feature Products</strong></a></li>";
        //this.URLRedirect();
    }
    private void URLRedirect()
    {
        if (Request["recurl"] == null)
        { //If this is null means that the redirect haven't started
            Response.Redirect("Feature_Products.html");
        }
    }
}
