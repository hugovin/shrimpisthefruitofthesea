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

public partial class emailsignup : System.Web.UI.Page
{
    //--    
    private string CurrentChilPage = "";
    private string ContentId = "";
    private string SiteId = "";
    private string strFolder = "images";
    private int TitleId = 0;
    private string sTitleId;
    //user Variables
    public string _user_email = "";
    public string _user_fullname = "";
    public string _user_schoolname = "";
    public string _user_add1 = "";
    public string _user_add2 = "";
    public string _user_city = "";
    public string _user_state = "";
    public string _user_zip = "";
    public string _user_phone = "";
    public string _user_title = "";
    public string _user_country = "";
    //--
    public string formHeader = "";
    //--
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
            CurrentChilPage = "emailsignup.aspx";
        //--

        //-- Left Menu Active
        if (Request.QueryString["am"] != null && Request.QueryString["am"] != "")
        {
            try { SiteConstants.LeftMenuActive = Convert.ToInt32(Request.QueryString["am"].ToString()); }
            catch { SiteConstants.LeftMenuActive = 4; }
        }
        //--
        //this.URLRedirect();
        getUserInfo();
    }
    private void getUserInfo()
    {
        DataSet siteCQ = new DataSet();
        SiteCatalogQuote siteCQObject = new SiteCatalogQuote();
        Site s = new Site();
        siteCQ = siteCQObject.getUserIndo(s.getUserGUID().ToString());
        foreach (DataTable table in siteCQ.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                _user_email = row["Email"].ToString();
                _user_fullname = row["Fullname"].ToString();
                _user_schoolname = row["BldgName"].ToString();
                _user_add1 = row["Adress1"].ToString();
                _user_add2 = row["Adress2"].ToString();
                _user_city = row["City"].ToString();
                _user_state = row["State"].ToString();
                _user_country = row["Country"].ToString();
                _user_zip = row["Zip"].ToString();
                _user_phone = row["Phone"].ToString();
                _user_title = row["Title"].ToString();
            }
        }

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[SiteConstants.UserValidLogin] != null && (bool)Session[SiteConstants.UserValidLogin] == false)
        {
            Response.Redirect("Home.aspx");
        }
        GetVars();
        Site s = new Site();
        Main_MasterPage main = (Main_MasterPage)Page.Master;
        main.pageTitleBar = "Email Sign Up - " + main.pageTitleBar;
        string userGuid = s.getUserGUID();
        main._site_breadLink += "<li class=\"last\"><a href=# onClick=\"return false;\"><strong>My Account</strong></a></li>";
        catalogIframe.Text = "<iframe src=\"https://" + Global.globalMySiteUrl + "/Requests/CatRequest.aspx?Frame=1&Token=" + userGuid + "\" frameborder=\"0\" height=\"480\" width=\"100%\"></iframe>";
    }
    private void URLRedirect()
    {
        if (Request["recurl"] == null)
        { //If this is null means that the redirect haven't started
            Response.Redirect("Email_Sign_Up.html");
        }
    }
}
