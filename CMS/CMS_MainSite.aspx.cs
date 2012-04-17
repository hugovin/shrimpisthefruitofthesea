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


public partial class CMS_MainSite : ContentGroup
{
    protected void Page_Load(object sender, EventArgs e)
    {

        Session["ResourceAbout"] = false;
        if (Convert.ToBoolean(Session["authenticated"]) != true)
        {
            Response.Redirect("CMS_Login.aspx");
        }
        Session["CurrentPage"] = "CMS_MainSite.aspx";
        if (Request["GroupId"] != null)
        {
            Session["contId"] = Convert.ToInt32(Request["GroupId"]);
        }
        Session["PageTitle"] = "";
        Session["TemplateChose"] = false;
        Session["NewPageTemplate"] = false;

    }

}
