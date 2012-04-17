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

public partial class loginValidation : System.Web.UI.Page
{
    /// <summary>
    /// Page Created for login information and validation
    /// </summary>
    private string CurrentChilPage = "";
    private string ContentId = "";
    private string SiteId = "";
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
    private string site_TagLine;
    private string site_Description;
    private string site_Url;
    private string site_Name;
    private bool user_session;
    private string site_breadLink;
    private string site_breadName;

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
            CurrentChilPage = "loginValidation.aspx";
        //--
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.GetVars();
        try
        {
            if (Request["username"] != null && Request["password"] != null)
            {
                string user = Request["username"].ToString();
                string pass = Request["password"].ToString();
                Session[SiteConstants.UserValidLogin] = false;

                if ((user != "" && user != null) && (pass != "" && pass != null))
                {
                    Login login = new Login();
                    //Response.Write(login.UserLogin(user, pass));
                    if (login.UserLogin(user, pass))
                    {
                        // START: Added June 10, 2009 by Jordan Sherer
                        Session[SiteConstants.UserLoginName] = login._UserLoginName;
                        // END
                        Session[SiteConstants.UserFullName] = login._Fullname;
                        Session[SiteConstants.UserFirstName] = login._Firstname;
                        Session[SiteConstants.UserLastName] = login._Lastname;
                        Session[SiteConstants.UserZipCode] = login._Zipcode;
                        Session[SiteConstants.UserValidLogin] = true;
                        Response.Write(true);
                        Response.Write("|Login success");
                    }
                    else
                    {
                        Session[SiteConstants.UserFullName] = null;
                        Session[SiteConstants.UserFirstName] = null;
                        Session[SiteConstants.UserLastName] = null;
                        Session[SiteConstants.UserZipCode] = null;
                        Session[SiteConstants.UserValidLogin] = false;
                        Response.Write(false);
                        Response.Write("|Login failed");
                        _logError = 1;
                    }
                }
            }
            else
            {
                Response.Write("False|Param missing.");
            }
        }
        catch (Exception exep)
        {
            Response.Write("False|" + exep);
        }
    }
}
