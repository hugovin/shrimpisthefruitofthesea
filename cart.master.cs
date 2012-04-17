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
using uc_Left_Menu;
using System.Text.RegularExpressions;

public partial class CartMasterPage : System.Web.UI.MasterPage
{
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
    //--Variable BreadCrumb
    private string site_breadLink;
    private string site_breadName;
    private string site_Privacy;
    private string site_Term;
    //public string site_Cust_Email = Session["siteCustEmail"];
    //
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
    public string _site_Privacy
    {
        get { return site_Privacy; }
        set { site_Privacy = value; }
    }
    public string _site_Term
    {
        get { return site_Term; }
        set { site_Term = value; }
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
            CurrentChilPage = "cart.aspx";
        //--
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //--Login
        if (Session[SiteConstants.UserValidLogin] == null)
        {
            Session[SiteConstants.UserValidLogin] = false;
        }
        string _ulo = Request["ulo"];
        if (_ulo == "0")
        {
            Logout();
            Response.Redirect("home.aspx");
        }
        //--
        GetVars();
        //--
        Left_Menu uc_left_menu = (Left_Menu)(Page.LoadControl("Left_Menu.ascx"));
        uc_left_menu._ActiveMenu = SiteConstants.LeftMenuActive;
        uc_left_menu._Finder = LeftMenu_Finder;
        uc_left_menu._Subject = LeftMenu_Subject;
        uc_left_menu._Browse = LeftMenu_Browse;
        uc_left_menu._Resourcecenter = LeftMenu_Resourcecenter;
        uc_left_menu._Aboutus = LeftMenu_Aboutus;
        LeftMenuPlaceHolder.Controls.Add(uc_left_menu);
        //--
        LoadHeadMenu();
        if (Session["CurrentChilPage"] != null && (Session["CurrentChilPage"].ToString()) == "cart.aspx")
        {
			//LoadBread();
            LoadAdds();
        }
        else
        { 
			LoadBread(); 
		}
        LoadSiteContact();
        LoadResourceCenter();
        LoadAboutUs();
        LoadSite();

    }

    protected void LoadBread()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("<div id=\"bread\"><div class=\"subMenuAccount\"><ul> ");
        sb.AppendLine("<li class=\"first\"><a href=\"home.aspx\">Home</a></li> ");
        //sb.AppendLine("<li class=\"last\">" + this._site_breadLink + "</li></ul></div>");
        //sb.AppendLine(this._site_breadLink + "</ul></div>");
        sb.AppendLine("<li class=\"last\"><a href=\"#\" onClick=\"return false;\"><strong>Shopping Cart</strong></a></li>" + "</ul></div>");
        sb.AppendLine("<div class=\"iconSubmenu\"><div class=\"linkPrinter\"><p><a href=\"javascript:imprime()\"><img src=\"" + Global.globalSiteImagesPath + "/printer.jpg\" alt=\"\" /> Print</a></p></div>");

        sb.AppendLine("<form action=\"print.aspx\" name=\"imprCont\" method=\"post\" target=\"_blank\">");
            sb.AppendLine("<input type=\"hidden\" name=\"contenido\">");
        sb.AppendLine("</form>");

        sb.AppendLine("<div class=\"clear\"></div></div></div>");

        PlaceHolder_bread.Controls.Add(new LiteralControl(sb.ToString()));
    }

    protected void LoadHeadMenu()
    {
        Header head = new Header();
        DataSet dsContentGroup = new DataSet();
        StringBuilder sb = new StringBuilder();

        dsContentGroup = head.getAllContentGroups();
        sb.AppendLine("<div id=\"topRight\" >");
        sb.AppendLine("<ul>");

        foreach (DataTable table in dsContentGroup.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
				// Add top header. 
                sb.AppendLine("<li><a href=\"home.aspx?ci=" + row["ContId"].ToString() + "\" class=\"" + (row["ContId"].ToString() == ContentId ? "current" : "") + "\">" + row["ContTitle"].ToString() + "</a></li>");
            }
        }
        sb.AppendLine("<li><a href=\"requestaquote.aspx\" class=\"request\">Request a <b>Quote</b></a></li>");
        sb.AppendLine("</ul>");
        sb.AppendLine("</div>");
        PlaceHolder_topRight.Controls.Add(new LiteralControl(sb.ToString()));
        head = null;
    }

    protected void LoadAdds()
    {
        Header head = new Header();
        DataSet dsAdds = new DataSet();
        StringBuilder sb = new StringBuilder();
        dsAdds = head.getAllAdds();
        int cont = 0;
        sb.AppendLine("<div id=\"bread\">");
        // print the adds
        string path = "";

        foreach (DataTable table in dsAdds.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                if (!Regex.IsMatch(row["AddsImage"].ToString(), @"^(ft|htt)p(s?)://([\w-]+\.)+[\w-]+(/[\w- ./]*)+\.(?:gif|jpg|jpeg|png|GIF|JPEG|JPG|PNG|Gif|Jpg|Jpeg|Png)$"))
                {
                    path = Global.globalSiteImagesPath + "/";
                }
                else
                {
                    path = "";
                }
                if (cont >= 1) sb.AppendLine("<div class=\"breadDivider\"></div>");
                sb.AppendLine("<div class=\"breadAdd\"><a href=\"" + row["AddsLink"].ToString() + "\"><img src=\"" + path + row["AddsImage"].ToString() + "\" border=0 /></a></div>");
                cont++;
            }
        }
        sb.AppendLine("</div>");
        PlaceHolder_bread.Controls.Add(new LiteralControl(sb.ToString()));
        sb = null;
    }

    protected void LoadSiteContact()
    {
        Footer footer = new Footer();
        DataSet dsContact = new DataSet();
        StringBuilder sb = new StringBuilder();
        dsContact = footer.GetAllSiteContact();
        foreach (DataTable table in dsContact.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                sb.AppendLine("<h4><img src='" + Global.globalSiteImagesPath + "/" + row["SiteContImage"].ToString() + "' width='91' height='36'  /></h4>");
                sb.AppendLine("<div>");
                sb.AppendLine("<h3>" + row["SiteContTitle"].ToString() + "</h3>");
                sb.AppendLine("<p>" + row["SiteContAddress"].ToString() + "</p>");
                sb.AppendLine("<h3>Email</h3>");
                sb.AppendLine("<p><span>Customer Service:</span><a href='mailto:" + row["SiteContEmailCus"].ToString() + "'>" + row["SiteContEmailCus"].ToString() + "</a>");
                sb.AppendLine("<br />");
                sb.AppendLine("<span>Sales:</span> <a href='mailto:" + row["SiteContEmailSal"].ToString() + "'>" + row["SiteContEmailSal"].ToString() + "</a>");
                sb.AppendLine("</p>");
                sb.AppendLine("</div>");
                //break;
            }
        }

        PlaceHolder_SiteContact.Controls.Add(new LiteralControl(sb.ToString()));
        sb = null;
    }

    protected void LoadResourceCenter()
    {
        Footer footer = new Footer();
        DataSet dsResource = new DataSet();
        StringBuilder sb = new StringBuilder();
        int asm = 1;
        dsResource = footer.Get_LeftMenu_All_ResourceCenter();
        foreach (DataTable table in dsResource.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                if ((row["tempsitepage"].ToString() == "requestACatalog.aspx") && (Session[SiteConstants.UserValidLogin] != null && ((bool)Session[SiteConstants.UserValidLogin]) == false))
                {
                    sb.AppendLine("<li><a href=\"#htmlElement\" id=\"mb15\" class=\"mb\" rel=\"type:element\" onClick=\"followLink='" + row["tempsitepage"].ToString() + "?id=" + row["geneid"].ToString() + "&TypeGen=1&am=2&asm=" + asm.ToString() + "';clear_follow();\">" + row["genetitle"].ToString() + "</a></li>");
                }
                else
                {
                    sb.AppendLine("<li><a href=\"" + row["tempsitepage"].ToString() + "?id=" + row["geneid"].ToString() + "&TypeGen=1&am=2&asm=" + asm.ToString() + "\">" + row["genetitle"].ToString() + "</a></li>");
                }
                asm++;
            }
        }

        PlaceHolder_ResourceCenter.Controls.Add(new LiteralControl(sb.ToString()));
        dsResource = null;
        sb = null;
    }

    protected void LoadAboutUs()
    {
        Footer footer = new Footer();
        DataSet dsAboutUs = new DataSet();
        StringBuilder sb = new StringBuilder();
        int asm = 1;
        dsAboutUs = footer.Get_LeftMenu_All_AboutUs();
        foreach (DataTable table in dsAboutUs.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                sb.AppendLine("<li><a href=\"" + row["tempsitepage"].ToString() + "?id=" + row["geneid"].ToString() + "&am=3&asm=" + asm.ToString() + "\">" + row["genetitle"].ToString() + "</a></li>");
                asm++;
            }
        }

        PlaceHolder_AboutUs.Controls.Add(new LiteralControl(sb.ToString()));
        dsAboutUs = null;
        sb = null;
    }

    protected void LoadSite()
    {
        Footer footer = new Footer();
        DataSet dsSite = new DataSet();

        dsSite = footer.GetSiteInformation();
        foreach (DataTable table in dsSite.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                _site_Phone = row["sitephone"].ToString();
                _site_Copy = row["sitecopy"].ToString();
                _site_Name = row["sitename"].ToString();
                _site_Description = row["sitedescription"].ToString();
                _site_TagLine = row["sitetagline"].ToString();
                _site_Url = row["siteurl"].ToString();
                _site_Privacy = row["siteprivacy"].ToString();
                _site_Term = row["siteterm"].ToString();

                SiteConstants.SiteName = _site_Name;
            }
        }


        dsSite = null;

    }
    //-- Login Methods
    protected void btn_Login_Click(object sender, ImageClickEventArgs e)
    {
        //This login methods has no reason, but I leave it here for reference.

        /*string user = Request["lu"];
        string pass = Request["lp"];
        string klogin = Request["kl"];
        Session[SiteConstants.UserValidLogin] = false;

        if ((user != "" && user != null) && (pass != "" && pass != null))
        {
            Login login = new Login();

            if (login.UserLogin(user, pass))
            {
                Session[SiteConstants.UserFullName] = login._Fullname;
                Session[SiteConstants.UserFirstName] = login._Firstname;
                Session[SiteConstants.UserLastName] = login._Lastname;
                Session[SiteConstants.UserZipCode] = login._Zipcode;
                Session[SiteConstants.UserValidLogin] = true;
            }
            else
            {
                Session[SiteConstants.UserFullName] = null;
                Session[SiteConstants.UserFirstName] = null;
                Session[SiteConstants.UserLastName] = null;
                Session[SiteConstants.UserZipCode] = null;
                Session[SiteConstants.UserValidLogin] = false;
                _logError = 1;
            }
        }
        string url = Request.ServerVariables["URL"];*/
        //Response.Redirect(url);
    }
    protected void Logout()
    {
        Login login = new Login();
        login.UserLogout();
        Session["quoteSession"] = null;
    }
}
