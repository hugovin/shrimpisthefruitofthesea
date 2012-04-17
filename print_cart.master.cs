using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;

public partial class Print : System.Web.UI.MasterPage
{
    private string CurrentChilPage = "";
    private string ContentId = "";
    private string SiteId = "";
    private string strFolder = SiteConstants.imagesPath;
    //-- Login Vars
    public int _logError = 0;
    //--
    //Public Vars for Left Menu
    public bool LeftMenu_Finder = true;
    public bool LeftMenu_Subject = true;
    public bool LeftMenu_Browse = true;
    public bool LeftMenu_Resourcecenter = true;
    public bool LeftMenu_Aboutus = false;
    //Properties
    private string site_Phone;
    private string site_Copy;
    private string site_TagLine = SiteConstants.SiteTagLine;
    private string site_Description;
    private string site_Url;
    private string site_Name;
    private bool user_session;
    private string site_breadLink;
    private string site_breadName;
    // - Title Variable
    public string pageTitleBar = SiteConstants.SiteName;

    //--
    public bool User_Session
    {
        get { return user_session; }
        set { user_session = value; }
    }
    public string _site_Phone
    {
        get { return site_Phone; }
        set { site_Phone = value; }
    }
    public string _site_Copy
    {
        get { return site_Copy; }
        set { site_Copy = value; }
    }
    public string _site_TagLine
    {
        get { return site_TagLine; }
        set { site_TagLine = value; }
    }
    public string _site_Description
    {
        get { return site_Description; }
        set { site_Description = value; }
    }
    public string _site_Url
    {
        get { return site_Url; }
        set { site_Url = value; }
    }
    public string _site_Name
    {
        get { return site_Name; }
        set { site_Name = value; }
    }

    //--
    public string _site_breadLink
    {
        get { return site_breadLink; }
        set { site_breadLink = value; }
    }
    public string _site_breadName
    {
        get { return site_breadName; }
        set { site_breadName = value; }
    }

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
            CurrentChilPage = "home.aspx";
        //--
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //--
        //--////this.URLRedirect();
        GetVars();        
    }
    private void URLRedirect()
    {
        if (Request["recurl"] == null)
        { //If this is null means that the redirect haven't started
            Response.Redirect("Print");
        }
    }    
}

